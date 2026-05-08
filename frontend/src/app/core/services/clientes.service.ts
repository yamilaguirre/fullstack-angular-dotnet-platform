import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import type { PagedClientesResponse } from '../models/paged-response';

export type ClientesEndpoint = 'ef' | 'sp';

@Injectable({ providedIn: 'root' })
export class ClientesService {
  private readonly http = inject(HttpClient);

  getClientesPaginated(
    page: number,
    pageSize: number,
    source: ClientesEndpoint = 'ef'
  ): Observable<PagedClientesResponse> {
    const segment = source === 'sp' ? 'sp' : 'ef';
    const url = `${environment.apiBaseUrl}/api/clientes/${segment}`;
    const params = new HttpParams({ fromObject: { page: String(page), pageSize: String(pageSize) } });
    return this.http.get<PagedClientesResponse>(url, { params });
  }
}
