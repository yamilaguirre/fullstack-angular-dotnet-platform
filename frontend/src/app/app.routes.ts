import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./features/clientes-list/clientes-list.component').then(m => m.ClientesListComponent)
  },
  { path: '**', redirectTo: '' }
];
