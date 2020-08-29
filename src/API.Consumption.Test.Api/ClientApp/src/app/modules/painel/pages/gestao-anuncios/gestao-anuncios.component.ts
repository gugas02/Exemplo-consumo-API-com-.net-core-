import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';

import { HeadTitle } from 'src/infra/constants/system';

@Component({
  selector: 'app-gestao-anuncios',
  templateUrl: './gestao-anuncios.component.html',
})


export class GestaoAnunciosComponent implements OnInit {

  constructor(private readonly titleService: Title) { }

  ngOnInit(): void {
    this.titleService.setTitle(HeadTitle.BACKOFFICE);
  }
}
