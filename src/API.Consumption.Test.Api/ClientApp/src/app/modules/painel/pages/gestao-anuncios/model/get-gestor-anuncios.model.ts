import { GestorAnunciosView } from './gestor-anuncios.model';
import { PaginacaoBase } from 'src/app/shared/models/paginacao.model';

export class GetGestorAnunciosView extends PaginacaoBase<GestorAnunciosView> {
    constructor(pageIndex: number = 1, pageSize: number = 10, sortType: string = 'asc', fnLoad?: () => void) {
        super(pageIndex, pageSize, sortType, fnLoad);
    }
    // TODO: implement props for filter here
}
