import { Injectable } from '@angular/core';
import { BinaryStringService } from '@api/services/binary-string.service';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import * as BinaryStringActions from '@store/binary-string/actions';
import { catchError, map, mergeMap, of } from 'rxjs';

@Injectable()
export class BinaryStringEffects {
  constructor(
    private actions$: Actions,
    private binaryStringService: BinaryStringService,
    private notificationService: NzNotificationService
  ) {}

  validateString$ = createEffect(() =>
    this.actions$.pipe(
      ofType(BinaryStringActions.validateInput),
      mergeMap((action) => {
        return this.binaryStringService.validateBinaryString(action.input).pipe(
          map((result: string) =>
            BinaryStringActions.validateInputSuccess({ result })
          ),
          catchError((error) =>
            of(
              BinaryStringActions.validateInputFailure({ error: error.message })
            )
          )
        );
      })
    )
  );
}
