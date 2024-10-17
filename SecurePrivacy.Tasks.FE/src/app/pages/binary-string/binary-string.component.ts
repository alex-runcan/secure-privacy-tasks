import { Component } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { AppState } from '@store/app/app.state';
import * as BinaryStringActions from '@store/binary-string/actions';
import {
  isRequestPendingSelector,
  resultSelector,
} from '@store/binary-string/selectors';
import { Observable } from 'rxjs';

@Component({
  selector: 'binary-strings-page',
  templateUrl: './binary-string.component.html',
  styleUrl: './binary-string.component.scss',
})
export class BinaryStringsPageComponent {
  protected input: string = '';
  protected isRequestPending$: Observable<boolean>;
  protected resultMessage$: Observable<string>;

  constructor(private store: Store<AppState>) {
    this.isRequestPending$ = this.store.pipe(select(isRequestPendingSelector));
    this.resultMessage$ = this.store.pipe(select(resultSelector));
  }

  protected validate() {
    this.store.dispatch(
      BinaryStringActions.validateInput({
        input: this.input,
      })
    );
  }
}
