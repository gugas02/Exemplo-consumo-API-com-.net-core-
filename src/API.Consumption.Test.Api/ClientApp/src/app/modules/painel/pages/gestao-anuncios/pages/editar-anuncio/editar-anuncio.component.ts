import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { HeadTitle } from 'src/infra/constants/system';
import { MessageService } from 'src/infra/services/message.service';
import { BaseFormComponent } from 'src/app/shared/base-form.component';
import { Validations } from 'src/app/shared/validations';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';
import { AnunciosService } from '../../services/anuncios.service';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-editar-anuncio',
  templateUrl: './editar-anuncio.component.html',
})
export class EditarAnuncioComponent extends BaseFormComponent implements OnInit {

  form: FormGroup;
  marcaOptions: any[] = [];
  modeloOptions: any[] = [];
  versaoOptions: any[] = [];

  constructor(
    private readonly titleService: Title,
    private readonly anunciosService: AnunciosService,
    private readonly apiService: ApiService,
    private readonly messageService: MessageService,
    private readonly spinner: NgxSpinnerService,
    private readonly fb: FormBuilder,
    private route: ActivatedRoute,
    private readonly router: Router,
  ) {
    super();
  }

  ngOnInit(): void {
    this.titleService.setTitle(HeadTitle.BACKOFFICE);

    this.form = this.fb.group({
      brand: [null, [Validators.required]],
      model: [null, [Validators.required]],
      version: [null, [Validators.required]],
      year: [null, [Validators.required]],
      km: [null, [Validators.required]],
      observation: [null, [Validators.required]],
    });

    this.obterMarcas();

    this.spinner.show();
    const id = this.route.snapshot.paramMap.get('id');
    this.anunciosService.obterPorId(id)
      .subscribe((result: any) => this.successGetById(result),
        error => this.messageService.error(error))
        .add(() => this.spinner.hide());
  }

  successGetById(response: any) {
    this.form.controls.brand.setValue(response.brand.split(';')[0]);
    this.obterModelos(this.form.value.brand);
    this.form.controls.model.setValue(response.model.split(';')[0]);
    this.obterVersao(this.form.value.model);
    this.form.controls.version.setValue(response.version.split(';')[0]);
    this.form.controls.year.setValue(response.year);
    this.form.controls.km.setValue(response.km);
    this.form.controls.observation.setValue(response.observation);
  }

  submit(): void {
    const brand = this.marcaOptions.filter(x => x.id == this.form.value.brand)[0];
    const model = this.modeloOptions.filter(x => x.id == this.form.value.model)[0];
    const version = this.versaoOptions.filter(x => x.id == this.form.value.version)[0];

    const data = {
      id: this.route.snapshot.paramMap.get('id'),
      brand: brand.id + ';' + brand.name,
      model: model.id + ';' + model.name,
      version: version.id + ';' + version.name,
      year: this.form.value.year,
      km: this.form.value.km,
      observation: this.form.value.observation,
    };

    this.spinner.show();
    this.anunciosService.editar(data)
      .subscribe(result => this.success(result), error => this.messageService.error(error))
      .add(() => this.spinner.hide());
  }

  obterMarcas() {
    this.spinner.show();
    this.apiService.obterMarcas()
      .subscribe((result: any) => this.successObterMarcas(result),
        error => this.messageService.error(error))
        .add(() => this.spinner.hide());
  }

  successObterMarcas(result) {
    this.marcaOptions = result;
  }

  obterModelos(idMarca) {
    this.spinner.show();
    this.apiService.obterModelos(idMarca)
      .subscribe((result: any) => this.successObterModelos(result),
        error => this.messageService.error(error))
        .add(() => this.spinner.hide());
  }

  successObterModelos(result) {
    this.modeloOptions = result;
  }

  obterVersao(idModelo) {
    this.spinner.show();
    this.apiService.obterVersao(idModelo)
      .subscribe((result: any) => this.successObterVersao(result),
        error => this.messageService.error(error))
        .add(() => this.spinner.hide());
  }

  successObterVersao(result) {
    this.versaoOptions = result;
  }


  success(result: any): void {
    Swal.fire({
      type: 'success',
      title: 'Parabéns',
      text: 'Anúncio editado com sucesso',
      confirmButtonText: 'OK'
    });
    this.router.navigate(['/painel/gestao-anuncios/anuncios']);
  }
}
