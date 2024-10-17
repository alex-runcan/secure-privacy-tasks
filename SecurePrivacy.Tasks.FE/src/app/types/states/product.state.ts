import { ProductModel } from '@app/types/product.model';

export interface ProductsState {
  isLoading: boolean;
  products: ProductModel[];
  error: string | undefined;
}