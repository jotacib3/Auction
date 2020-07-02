import { Component, OnInit } from '@angular/core';
import { Model } from '../../../../models/model';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-model-list',
  templateUrl: './model-list.component.html',
  styleUrls: ['./model-list.component.css']
})
export class ModelListComponent implements OnInit {

  models: Array<Model> = new Array<Model>();
  private modelUrl = 'models';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.modelUrl).subscribe(data => {this.models = data as Array<Model>; });
    }
    onAdd(model: Model) {
      this.repo.post(this.modelUrl, model).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(model: Model) {
    this.repo.update(`${this.modelUrl}/${model.id}`, model).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(model: Model) {
    this.repo.delete(`${this.modelUrl}/${model.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
