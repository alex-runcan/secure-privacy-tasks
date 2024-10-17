import { createAction, props } from '@ngrx/store';

export const validateInput = createAction(
  '[Binary String] Validate Input',
  props<{ input: string }>()
);
export const validateInputSuccess = createAction(
  '[Binary String] Validate Input success',
  props<{ result: string }>()
);
export const validateInputFailure = createAction(
  '[Binary String] Validate Input failure',
  props<{ error: string }>()
);
