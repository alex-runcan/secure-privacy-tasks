import { Injectable } from '@angular/core';
import { ProductsService } from '@api/services/products.service';
import { ProductModel } from '@app/types/product.model';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import * as ProductActions from '@store/products/actions';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { catchError, exhaustMap, map, mergeMap, of, tap } from 'rxjs';

@Injectable()
export class ProductsEffects {
  constructor(
    private actions$: Actions,
    private productsService: ProductsService,
    private notificationService: NzNotificationService
  ) {}

  getProducts$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.getProducts),
      mergeMap((action) => {
        return this.productsService.getProducts(action.searchParams).pipe(
          map((products: ProductModel[]) =>
            ProductActions.getProductsSuccess({ products })
          ),
          catchError((error) =>
            of(ProductActions.getProductsFailure({ error: error.message }))
          )
        );
      })
    )
  );

  addProduct$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.addProduct),
      mergeMap((action) => {
        return this.productsService.addProduct(action.product).pipe(
          map((_) => ProductActions.addProductSuccess()),
          catchError((error) =>
            of(ProductActions.addProductFailure({ error: error.message }))
          )
        );
      })
    )
  );

  addProductSuccess$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.addProductSuccess),
      tap(() =>
        this.notificationService.success(
          'Success',
          'The product was successfully added!'
        )
      ),
      exhaustMap(() =>
        of(
          ProductActions.getProducts({
            searchParams: {},
          })
        )
      )
    )
  );

  addProductError$ = createEffect(() =>
    this.actions$.pipe(
      ofType(ProductActions.addProductFailure),
      exhaustMap((action) => {
        this.notificationService.error('Error', action.error);
        return of(action);
      })
    )
  );
}