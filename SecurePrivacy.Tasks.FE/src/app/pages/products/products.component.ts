import { Component, OnInit } from '@angular/core';
import { select, Store } from '@ngrx/store';
import * as ProductActions from '@store/products/actions';
import { AppState } from '@store/app/app.state';
import { Observable } from 'rxjs';
import {
  areProductsLoadingSelector,
  productsSelector,
} from '@store/products/selectors';
import { ProductModel } from '@app/types/product.model';
import { NzModalService } from 'ng-zorro-antd/modal';
import { AddModalComponent } from './add-product-modal/add-product-modal.component';
import { COLUMN_DEFINITIONS } from '@app/constants/products-columns-definition.constants';
import { ProductGridSearchParamsModel } from '@app/types/products-grid-search-params.model';

@Component({
  selector: 'products-page',
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss',
})
export class ProductsComponent implements OnInit {
  protected listOfColumns = COLUMN_DEFINITIONS;
  protected areProductsLoading$: Observable<boolean>;
  protected products$: Observable<ProductModel[]>;
  protected pageIndex: number = 1;
  protected pageSize: number = 10;
  protected sortField: string | null = null;
  protected sortOrder: 'ascend' | 'descend' | null = null;

  constructor(
    private store: Store<AppState>,
    private modalService: NzModalService
  ) {
    this.areProductsLoading$ = this.store.pipe(
      select(areProductsLoadingSelector)
    );
    this.products$ = this.store.pipe(select(productsSelector));
  }

  ngOnInit(): void {
    this.store.dispatch(
      ProductActions.getProducts({
        searchParams: {} as ProductGridSearchParamsModel,
      })
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
      console.log(result);
      this.store.dispatch(ProductActions.addProduct({ product: result }));
    });
  }

  protected onQueryParamsChange(event: any) {
    this.store.dispatch(
      ProductActions.getProducts({
        searchParams: {},
      })
    );
  }
}
