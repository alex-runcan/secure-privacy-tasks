import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@environments/environment';
import { map, Observable } from 'rxjs';
import { PRODUCTS_PATH } from '@api/constants/path.constants';
import { ProductResponseDto } from '@api/dtos/product-reponse.dto';
import { ProductModel } from '@app/types/product.model';
import { AddProductDto } from '@api/dtos/add-product.dto';
import { ProductGridSearchParamsModel } from '@app/types/products-grid-search-params.model';
import { ProductSearchResponseModel } from '@app/types/product-search-response.model';
import { ProductSerachResponseDto } from '@api/dtos/product-search-response.dto';

@Injectable({
  providedIn: 'root',
})
export class ProductsService {
  private apiBaseUrl = environment.apiBaseUrl;

  constructor(private httpClient: HttpClient) {}

  public getProducts(
    searchParams: ProductGridSearchParamsModel
  ): Observable<ProductSearchResponseModel> {
    var queryParams = {
      params: new HttpParams()
        .set('pageIndex', searchParams.pageIndex ?? 1)
        .set('pageSize', searchParams.pageSize ?? 10),
    };
    if (searchParams.priceSort) {
      queryParams.params = queryParams.params.set(
        'priceSort',
        searchParams.priceSort
      );
    }

    if (searchParams.ratingFilter) {
      queryParams.params = queryParams.params.append(
        'ratingFilter',
        searchParams.ratingFilter
      );
    }

    return this.httpClient
      .get<ProductSerachResponseDto>(
        `${this.apiBaseUrl}/${PRODUCTS_PATH}`,
        queryParams
      )
      .pipe(
        map(
          (products: ProductSerachResponseDto) =>
            products as ProductSearchResponseModel
        )
      );
  }

  public addProduct(product: ProductModel): Observable<void> {
    return this.httpClient.post<void>(
      `${this.apiBaseUrl}/${PRODUCTS_PATH}`,
      product as AddProductDto
    );
  }
}
