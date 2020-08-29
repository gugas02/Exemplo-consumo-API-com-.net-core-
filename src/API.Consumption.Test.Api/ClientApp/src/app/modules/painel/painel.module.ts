import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxMaskModule } from 'ngx-mask';
import {
  MatButtonModule,
  MatDialogModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatFormFieldModule,
  MatInputModule,
  MatFormField,
  MatAutocomplete,
  MatAutocompleteModule
} from '@angular/material';
import { SharedModule } from 'src/app/shared/shared.module';
import { PainelRoutingModule } from './painel-routing.module';
import { PainelComponent } from './painel.component';
import { ListarAnunciosComponent } from './pages/gestao-anuncios/pages/listar-anuncio/listar-anuncio.component';
import { GestaoAnunciosComponent } from './pages/gestao-anuncios/gestao-anuncios.component';
import { AnunciosService } from './pages/gestao-anuncios/services/anuncios.service';
import { CadastrarAnuncioComponent } from './pages/gestao-anuncios/pages/cadastro-anuncio/cadastro-anuncio.component';
import { EditarAnuncioComponent } from './pages/gestao-anuncios/pages/editar-anuncio/editar-anuncio.component';
import { ApiService } from './pages/gestao-anuncios/services/api.service';

const material = [
  MatDialogModule,
  MatButtonModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatFormFieldModule,
  MatInputModule,
  MatAutocompleteModule
];

@NgModule({
  declarations: [
    PainelComponent,
    CadastrarAnuncioComponent,
    EditarAnuncioComponent,
    GestaoAnunciosComponent,
    ListarAnunciosComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    PainelRoutingModule,
    NgxMaskModule.forRoot(),
    material
  ],
  providers: [
    AnunciosService,
    ApiService
  ],
  entryComponents: []
})
export class PainelModule { }
