import { BinaryStringState } from '@app/types/states/binary-string.state';
import { ProductsState } from '@app/types/states/product.state';

export interface AppState {
  products: ProductsState;
  binaryString: BinaryStringState;
}
