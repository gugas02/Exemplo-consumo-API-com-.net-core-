import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { Api} from 'src/infra/constants/system';

@Injectable()
export class ApiService {

  constructor(private readonly http: HttpClient) { }

  obterMarcas(): Observable<any> {
    return this.http.get<any>(Api.API_BRANDS_URI).pipe(take(1));
  }

  obterModelos(idMarca): Observable<any> {
    return this.http.get<any>(Api.API_MODELS_URI + '/' + idMarca).pipe(take(1));
  }

  obterVersao(idModelo): Observable<any> {
    return this.http.get<any>(Api.API_VERSIONS_URI + '/' + idModelo).pipe(take(1));
  }
}
