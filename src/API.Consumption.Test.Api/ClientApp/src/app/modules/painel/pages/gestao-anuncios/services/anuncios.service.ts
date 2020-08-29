import { GetGestorAnunciosView } from '../model/get-gestor-anuncios.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { GestorAnunciosView } from '../model/gestor-anuncios.model';
import { Advertisement} from 'src/infra/constants/system';
import { PaginacaoBase } from 'src/app/shared/models/paginacao.model';

@Injectable()
export class AnunciosService {

  constructor(private readonly http: HttpClient) { }

  obterTodos(data: GetGestorAnunciosView): Observable<PaginacaoBase<GestorAnunciosView>> {
    return this.http.post<PaginacaoBase<GestorAnunciosView>>(Advertisement.ADVERTISEMENT_PAGINATION_URI, data).pipe(take(1));
  }

  obterPorId(id): Observable<any> {
    return this.http.get<any>(Advertisement.ADVERTISEMENT_URI + '/' + id).pipe(take(1));
  }

  salvar(body: any): Observable<any> {
    return this.http.post<any>(Advertisement.ADVERTISEMENT_URI, body).pipe(take(1));
  }

  editar(body: any): Observable<any> {
    return this.http.put<any>(Advertisement.ADVERTISEMENT_URI + '/' + body.id, body).pipe(take(1));
  }

  delete(id: string): Observable<any> {
    return this.http.put<any>(Advertisement.ADVERTISEMENT_DELETE_URI + '/' + id, null).pipe(take(1));
  }

  obterNomes(): Observable<any> {
    return this.http.get<any>(Advertisement.ADVERTISEMENT_NAMES_URI).pipe(take(1));
  }
}
