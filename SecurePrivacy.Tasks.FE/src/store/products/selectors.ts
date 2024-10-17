import { createSelector } from '@ngrx/store';
import { AppState } from '@store/app/app.state';

export const selectFeature = (state: AppState) => state.products;
export const areProductsLoadingSelector = createSelector(
  selectFeature,
  (state) => state.isLoading
);
export const productsSelector = createSelector(
  selectFeature,
  (state) => state.products
);
export const productsRetrievalErrorSelector = createSelector(
  selectFeature,
  (state) => state.error
);
