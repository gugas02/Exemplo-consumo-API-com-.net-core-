import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PainelComponent } from './painel.component';
import { GestaoAnunciosComponent } from './pages/gestao-anuncios/gestao-anuncios.component';
import { ListarAnunciosComponent } from './pages/gestao-anuncios/pages/listar-anuncio/listar-anuncio.component';
import { EditarAnuncioComponent } from './pages/gestao-anuncios/pages/editar-anuncio/editar-anuncio.component';
import { CadastrarAnuncioComponent } from './pages/gestao-anuncios/pages/cadastro-anuncio/cadastro-anuncio.component';

const routes: Routes = [
  {
    path: '', component: PainelComponent, children: [
      { path: '', redirectTo: 'gestao-anuncios', pathMatch: 'full' },
      {
        path: 'gestao-anuncios', component: GestaoAnunciosComponent, canActivate: [], children: [
          { path: '', redirectTo: 'anuncios', pathMatch: 'full' },
          { path: 'anuncios', component: ListarAnunciosComponent, canActivate: [] },
          { path: 'anuncios/cadastrar', component: CadastrarAnuncioComponent, canActivate: [] },
          { path: 'anuncios/editar/:id', component: EditarAnuncioComponent, canActivate: [] },
        ]
      }
    ],
    canActivate: [],
    resolve: {},
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ]
})
export class PainelRoutingModule { }
