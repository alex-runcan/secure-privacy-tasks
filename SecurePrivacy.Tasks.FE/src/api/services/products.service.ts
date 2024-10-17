import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { map, Observable } from 'rxjs';
import { PRODUCTS_PATH } from '@api/constants/path.constants';
import { ProductResponseDto } from '@api/dtos/product-reponse.dto';
import { ProductModel } from '@app/types/product.model';
import { AddProductDto } from '@api/dtos/add-product.dto';
import { ProductGridSearchParamsModel } from '@app/types/products-grid-search-params.model';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private httpClient: HttpClient) {}

  public getProducts(
    params: ProductGridSearchParamsModel
  ): Observable<ProductModel[]> {
    return this.httpClient
      .get<ProductResponseDto[]>(`${this.apiBaseUrl}/${PRODUCTS_PATH}`)
      .pipe(
        map((products: ProductResponseDto[]) => products as ProductModel[])
      );
  }

  public addProduct(product: ProductModel): Observable<void> {
    return this.httpClient.post<void>(
      `${this.apiBaseUrl}/${PRODUCTS_PATH}`,
      product as AddProductDto
    );
  }
}
