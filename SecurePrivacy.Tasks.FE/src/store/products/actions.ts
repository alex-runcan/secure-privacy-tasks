import { ProductSearchResponseModel } from '@app/types/product-search-response.model';
import { ProductModel } from '@app/types/product.model';
import { ProductGridSearchParamsModel } from '@app/types/products-grid-search-params.model';
import { createAction, props } from '@ngrx/store';

export const getProducts = createAction(
  '[Products] Get Products',
  props<{ searchParams: ProductGridSearchParamsModel }>()
);
export const getProductsSuccess = createAction(
  '[Products] Get Products success',
  props<{ products: ProductSearchResponseModel }>()
);
export const getProductsFailure = createAction(
  '[Products] Get Products failure',
  props<{ error: string }>()
);
export const addProduct = createAction(
  '[Products] Add Product',
  props<{ product: ProductModel; searchParams: ProductGridSearchParamsModel }>()
);
export const addProductSuccess = createAction(
  '[Products] Add Product success',
  props<{ searchParams: ProductGridSearchParamsModel }>()
);
export const addProductFailure = createAction(
  '[Products] Add Product failure',
  props<{ error: string }>()
);
