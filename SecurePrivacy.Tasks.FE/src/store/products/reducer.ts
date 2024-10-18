import { ProductsState } from '@app/types/states/product.state';
import { createReducer, on } from '@ngrx/store';
import * as ProductActions from '@store/products/actions';

export const initialState: ProductsState = {
  isLoading: false,
  products: [],
  totalCount: 0,
  error: undefined,
};

export const reducers = createReducer(
  initialState,
  on(ProductActions.getProducts, (state, action) => ({
    ...state,
    isLoading: true,
  })),
  on(ProductActions.getProductsSuccess, (state, action) => ({
    ...state,
    isLoading: false,
    products: action.products.products,
    totalCount: action.products.count,
  })),
  on(ProductActions.getProductsFailure, (state, action) => ({
    ...state,
    isLoading: false,
    error: action.error,
  }))
);
