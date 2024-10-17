export interface FilterModel {
  key: string;
  value: number[];
}

export interface SortModel {
  key: string;
  value: 'ascend' | 'descend' | null;
}

export interface ProductGridSearchParamsModel {
  pageIndex?: number;
  pageSize?: number;
  sort?: SortModel[];
  filter?: FilterModel[];
}
