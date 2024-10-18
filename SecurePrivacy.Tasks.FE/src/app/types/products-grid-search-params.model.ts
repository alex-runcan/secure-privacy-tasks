export interface ProductGridSearchParamsModel {
  pageIndex: number;
  pageSize: number;
  priceSort?: string | 'ascend' | 'descend' | null;
  ratingFilter?: number;
}
