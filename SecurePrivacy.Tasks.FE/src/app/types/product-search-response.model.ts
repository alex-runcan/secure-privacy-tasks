import { ProductModel } from './product.model';

export interface ProductSearchResponseModel {
  count: number;
  products: ProductModel[];
}
