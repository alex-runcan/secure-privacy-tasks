import { ProductResponseDto } from './product-reponse.dto';

export interface ProductSerachResponseDto {
  count: number;
  products: ProductResponseDto[];
}
