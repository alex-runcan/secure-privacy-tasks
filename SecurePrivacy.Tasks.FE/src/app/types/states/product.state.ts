import { ProductModel } from '@app/types/product.model';
import { ProductGridSearchParamsModel } from '@app/types/products-grid-search-params.model';

export interface ProductsState {
  isLoading: boolean;
  products: ProductModel[];
  totalCount: number;
  error: string | undefined;
}
