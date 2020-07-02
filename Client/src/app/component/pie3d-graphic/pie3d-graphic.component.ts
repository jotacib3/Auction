import { RepositoryService } from '../../services/repository.service';
import { NzNotificationService } from '../../../../node_modules/ng-zorro-antd';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';

@Component({
  selector: 'app-pie3d-graphic',
  templateUrl: './pie3d-graphic.component.html',
  styleUrls: ['./pie3d-graphic.component.css']
})
export class Pie3dGraphicComponent implements OnInit {

  pieGraphic: Object;
  graphicData: any;

  constructor(private repo: RepositoryService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.repo.get(`graphics/offers`).subscribe(data => {
      this.graphicData = data;
      console.log(this.graphicData);
      this.pieGraphic = {
        chart: {
          caption: 'Cantidad de ofertas por publicación',
          subcaption: '',
          showvalues: '1',
          showpercentintooltip: '0',
          enablemultislicing: '1',
          theme: 'fusion'
        },
        data: this.graphicData
      };
    });
  }
}
