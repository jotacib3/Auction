import { RepositoryService } from '../../services/repository.service';
import { NzNotificationService } from '../../../../node_modules/ng-zorro-antd';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';

@Component({
  selector: 'app-column-graphic',
  templateUrl: './column-graphic.component.html',
  styleUrls: ['./column-graphic.component.css']
})
export class ColumnGraphicComponent implements OnInit {

  data: Object;
  publData: any;

  constructor(private repo: RepositoryService) { }

  ngOnInit() {
    this.GraphBrands();
  }

  loadData(target, title, subtitle, nameXs, nameYs) {
    this.repo.get(`graphics/${target}`).subscribe(data => {
      this.publData = data;
      this.data = {
        chart: {
          caption: title,
          subcaption: subtitle,
          xaxisname: nameXs,
          yaxisname: nameYs,
          theme: 'fusion',
        },
        data: this.publData
      };
    });
  }

  GraphBrands() {
    this.loadData('brands', 'Cantidad de vehículos publicados por marcas', '', 'marcas', 'cantidad');
  }

  GraphModels() {
    this.loadData('models', 'Cantidad de vehículos publicados por modelos', '', 'modelos', 'cantidad');
  }

  GraphLocations() {
    this.loadData('locations', 'Cantidad de vehículos publicados por localización', '', 'localización', 'cantidad');
  }
}

