import { createSelector } from '@ngrx/store';
import { AppState } from '@store/app/app.state';

export const selectFeature = (state: AppState) => state.binaryString;
export const isRequestPendingSelector = createSelector(
  selectFeature,
  (state) => state.isRequestPending
);
export const resultSelector = createSelector(
  selectFeature,
  (state) => state.result
);
