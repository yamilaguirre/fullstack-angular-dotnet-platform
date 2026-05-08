import type { ClienteDto } from './cliente-dto';

export interface PagedResponse<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
}

export type PagedClientesResponse = PagedResponse<ClienteDto>;
