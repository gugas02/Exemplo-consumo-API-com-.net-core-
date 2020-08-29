
import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import Swal from 'sweetalert2';

import { HeadTitle } from 'src/infra/constants/system';
import { MessageService } from 'src/infra/services/message.service';
import { AnunciosService } from '../../services/anuncios.service';
import { GetGestorAnunciosView } from '../../model/get-gestor-anuncios.model';

@Component({
  selector: 'app-listar-anuncio',
  templateUrl: './listar-anuncio.component.html',
})

export class ListarAnunciosComponent implements OnInit {

  searchText: string;
  itemsPerPage: number;
  currentPage: any;
  pagination: GetGestorAnunciosView;
  pedidos: any[] = [];

  constructor(
    private readonly titleService: Title,
    private readonly messageService: MessageService,
    private readonly spinner: NgxSpinnerService,
    private readonly anuncioService: AnunciosService,
  ) {
    this.pagination = new GetGestorAnunciosView(1, 10, 'asc', () => {
      const data = this.pagination.dataParams;

      this.spinner.show();
      this.anuncioService.obterTodos(data).subscribe(
        result => this.pagination.fillResponse(result),
        error => this.messageService.error(error)
      ).add(() => this.spinner.hide());
    });
  }

  ngOnInit(): void {
    this.titleService.setTitle(HeadTitle.BACKOFFICE);
    this.itemsPerPage = 10;
    this.pagination.load();
  }

  obterNome(valor) {
    return valor.split(';')[1];
  }

  inativarModal(id) {
    Swal.fire({
      type: 'warning',
      title: 'Atenção',
      text: 'Tem certeza que deseja deletar esse funcionário?',
      confirmButtonText: 'Sim',
      cancelButtonText: 'Não',
      showCancelButton: true,
    }).then((isConfirm) => {
      if (isConfirm.value) {
        this.inativar(id);
      }
    });
  }

  inativar(id) {
    this.spinner.show();
    this.anuncioService.delete(id).subscribe(
      result => Swal.fire({
        type: 'warning',
        title: 'Atenção',
        text: 'Anúncio deletado com sucesso',
        confirmButtonText: 'Ok',
      }).then((isConfirm) => {
        this.pagination.load();
      }),
      error => this.messageService.error(error)
    ).add(() => this.spinner.hide());
  }
}
