import { Validators } from '@angular/forms';

export const FORM_GROUP_VALIDATORS = {
  name: [null, [Validators.required]],
  description: [null],
  price: [null, [Validators.required, Validators.min(0)]],
  rating: [null, [Validators.required, Validators.min(1), Validators.max(5)]],
};
