import { BinaryStringState } from '@app/types/states/binary-string.state';
import { createReducer, on } from '@ngrx/store';
import * as BinaryStringActions from '@store/binary-string/actions';

export const initialState: BinaryStringState = {
  isRequestPending: false,
  input: '',
  result: '',
};

export const reducers = createReducer(
  initialState,
  on(BinaryStringActions.validateInput, (state, action) => ({
    ...state,
    input: action.input,
    isRequestPending: true,
  })),
  on(BinaryStringActions.validateInputSuccess, (state, action) => ({
    ...state,
    isRequestPending: false,
    result: action.result,
  })),
  on(BinaryStringActions.validateInputSuccess, (state, action) => ({
    ...state,
    isRequestPending: false,
  }))
);
