import { Component, DestroyRef, inject, signal } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Subject, switchMap, tap } from 'rxjs';
import { finalize } from 'rxjs/operators';
import type { ClienteDto } from '../../core/models/cliente-dto';
import type { PagedClientesResponse } from '../../core/models/paged-response';
import { ClientesService, type ClientesEndpoint } from '../../core/services/clientes.service';
import { TelefonoFormatPipe } from '../../shared/pipes/telefono-format.pipe';

@Component({
  selector: 'app-clientes-list',
  standalone: true,
  imports: [TelefonoFormatPipe],
  templateUrl: './clientes-list.component.html',
  styleUrl: './clientes-list.component.scss'
})
export class ClientesListComponent {
  private readonly clientesApi = inject(ClientesService);
  private readonly destroyRef = inject(DestroyRef);
  private readonly reload$ = new Subject<void>();

  readonly loading = signal(false);
  readonly errorMessage = signal<string | null>(null);
  readonly data = signal<PagedClientesResponse | null>(null);
  readonly page = signal(1);
  readonly pageSize = signal(10);
  readonly source = signal<ClientesEndpoint>('ef');

  constructor() {
    this.reload$
      .pipe(
        tap(() => {
          this.loading.set(true);
          this.errorMessage.set(null);
        }),
        switchMap(() =>
          this.clientesApi
            .getClientesPaginated(this.page(), this.pageSize(), this.source())
            .pipe(finalize(() => this.loading.set(false)))
        ),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe({
        next: payload => this.data.set(payload),
        error: err => {
          const message =
            typeof err?.error === 'string'
              ? err.error
              : err?.error?.title ?? err?.message ?? 'No se pudieron cargar los clientes.';
          this.errorMessage.set(String(message));
        }
      });

    this.triggerLoad();
  }

  private triggerLoad(): void {
    this.reload$.next();
  }

  goToPage(nextPage: number): void {
    if (nextPage < 1) {
      return;
    }

    this.page.set(nextPage);
    this.triggerLoad();
  }

  changePageSize(size: number): void {
    if (size < 1) {
      return;
    }

    this.pageSize.set(size);
    this.page.set(1);
    this.triggerLoad();
  }

  setSource(source: ClientesEndpoint): void {
    this.source.set(source);
    this.page.set(1);
    this.triggerLoad();
  }

  trackByCliente(_index: number, cliente: ClienteDto): number {
    return cliente.idCliente;
  }

  totalPages(): number {
    const payload = this.data();
    if (!payload || payload.pageSize === 0) {
      return 1;
    }

    return Math.max(1, Math.ceil(payload.totalCount / payload.pageSize));
  }
}
