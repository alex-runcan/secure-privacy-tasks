import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from '@app/pages/products/products.component';
import { BinaryStringsPageComponent } from '@app/pages/binary-string/binary-string.component';

const routes: Routes = [
  { path: 'products', component: ProductsComponent },
  { path: 'binary-strings', component: BinaryStringsPageComponent },
  { path: '**', redirectTo: 'products' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
