import { Routes } from '@angular/router';

import { Pacientes } from './Home/pacientes/pacientes';

export const routes: Routes = [
  { path: 'pacientes', component: Pacientes },
  { path: '', redirectTo: 'pacientes', pathMatch: 'full' }
];
