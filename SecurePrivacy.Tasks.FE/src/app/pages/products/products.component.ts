import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import * as ProductActions from '@store/products/actions';
import { AppState } from '@store/app/app.state';
import { Observable } from 'rxjs';
import {
  areProductsLoadingSelector,
  productsSelector,
  productsTotalCountSelector,
} from '@store/products/selectors';
import { ProductModel } from '@app/types/product.model';
import { NzModalService } from 'ng-zorro-antd/modal';
import { AddModalComponent } from './add-product-modal/add-product-modal.component';
import { COLUMN_DEFINITIONS } from '@app/constants/products-columns-definition.constants';
import { NzTableQueryParams } from 'ng-zorro-antd/table';
import { ProductGridSearchParamsModel } from '@app/types/products-grid-search-params.model';

@Component({
  selector: 'products-page',
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss',
})
export class ProductsComponent {
  protected listOfColumns = COLUMN_DEFINITIONS;
  protected areProductsLoading$: Observable<boolean>;
  protected products$: Observable<ProductModel[]>;
  protected totalProductsCount$: Observable<number>;
  protected sortField: string | null = null;
  protected sortOrder: 'ascend' | 'descend' | null = null;
  protected searchParams: ProductGridSearchParamsModel = {
    pageIndex: 1,
    pageSize: 10,
  };

  constructor(
    private store: Store<AppState>,
    private modalService: NzModalService
  ) {
    this.areProductsLoading$ = this.store.pipe(
      select(areProductsLoadingSelector)
    );
    this.products$ = this.store.pipe(select(productsSelector));
    this.totalProductsCount$ = this.store.pipe(
      select(productsTotalCountSelector)
    );
  }

  protected openAddProductModal(): void {
    const modal = this.modalService.create({
      nzTitle: 'Add New Product',
      nzContent: AddModalComponent,
      nzFooter: null,
      nzCentered: true,
    });

    modal.afterClose.subscribe((result) => {
      if (!result) {
        return;
      }
      this.store.dispatch(
        ProductActions.addProduct({
          product: result,
          searchParams: this.searchParams,
        })
      );
    });
  }

  protected onQueryParamsChange(event: NzTableQueryParams) {
    this.searchParams = {
      pageIndex: event.pageIndex,
      pageSize: event.pageSize,
      priceSort: event.sort[0]?.value,
      ratingFilter: event.filter[0]?.value,
    };
    this.store.dispatch(
      ProductActions.getProducts({
        searchParams: this.searchParams,
      })
    );
  }
}
