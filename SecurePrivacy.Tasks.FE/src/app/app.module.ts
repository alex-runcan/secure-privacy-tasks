import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzIconModule, NZ_ICONS } from 'ng-zorro-antd/icon';

import { AppRoutingModule } from '@app/app-routing.module';
import { AppComponent } from '@app/app.component';

import {
  MenuUnfoldOutline,
  MenuFoldOutline,
  HomeOutline,
  CodeOutline,
  PlusOutline,
} from '@ant-design/icons-angular/icons';
import { NzFormModule } from 'ng-zorro-antd/form';
import { ProductsComponent } from './pages/products/products.component';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { reducers } from '@store/products/reducer';
import * as BinaryStringReducers from '@store/binary-string/reducer';
import { ProductsEffects } from '@store/products/effects';
import { HttpClientModule } from '@angular/common/http';
import { BinaryStringsPageComponent } from '@app/pages/binary-string/binary-string.component';
import { NzButtonComponent } from 'ng-zorro-antd/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddModalComponent } from '@app/pages/products/add-product-modal/add-product-modal.component';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzNotificationModule } from 'ng-zorro-antd/notification';
import { NZ_I18N, en_US } from 'ng-zorro-antd/i18n';
import { BinaryStringEffects } from '@store/binary-string/effects';

const icons = [
  MenuFoldOutline,
  MenuUnfoldOutline,
  HomeOutline,
  CodeOutline,
  PlusOutline,
];

@NgModule({
  declarations: [
    AppComponent,
    ProductsComponent,
    BinaryStringsPageComponent,
    AddModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    StoreModule.forFeature('products', reducers),
    EffectsModule.forFeature([ProductsEffects]),
    StoreModule.forFeature('binaryString', BinaryStringReducers.reducers),
    EffectsModule.forFeature([BinaryStringEffects]),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: !isDevMode(),
      autoPause: true,
      trace: false,
      traceLimit: 75,
      connectInZone: true,
    }),
    NzLayoutModule,
    NzMenuModule,
    NzIconModule,
    NzTableModule,
    NzButtonComponent,
    NzModalModule,
    NzFormModule,
    NzInputModule,
    NzNotificationModule,
    StoreModule.forRoot({}, {}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() }),
  ],
  providers: [
    { provide: NZ_ICONS, useValue: icons },
    { provide: NZ_I18N, useValue: en_US },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
