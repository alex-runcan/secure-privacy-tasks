import { Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FORM_GROUP_VALIDATORS } from '@app/constants/add-product-modal.constants';
import { ProductModel } from '@app/types/product.model';
import { NzModalRef } from 'ng-zorro-antd/modal';

@Component({
  selector: 'add-modal-component',
  templateUrl: './add-product-modal.component.html',
  styleUrl: './add-product-modal.component.scss',
})
export class AddModalComponent {
  protected formGroup!: FormGroup;

  constructor(private fb: FormBuilder, private modal: NzModalRef) {}

  ngOnInit(): void {
    this.formGroup = this.fb.group(FORM_GROUP_VALIDATORS);
  }

  submitForm(): void {
    if (this.formGroup.valid) {
      const newProduct = {
        name: this.formGroup.value.name,
        description: this.formGroup.value.description,
        price: this.formGroup.value.price,
        rating: this.formGroup.value.rating,
      } as ProductModel;
      this.modal.close(newProduct);
    } else {
      Object.values(this.formGroup.controls).forEach((control) => {
        control.markAsDirty();
        control.updateValueAndValidity({ onlySelf: true });
      });
    }
  }

  cancel(): void {
    this.modal.destroy();
  }
}
