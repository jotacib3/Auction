import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';
import { Fuel } from '../../../../models/fuel';

@Component({
  selector: 'app-fuel-list',
  templateUrl: './fuel-list.component.html',
  styleUrls: ['./fuel-list.component.css']
})
export class FuelListComponent implements OnInit {

  fuels: Array<Fuel> = new Array<Fuel>();
  private fuelUrl = 'fuels';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.fuelUrl).subscribe(data => {this.fuels = data as Array<Fuel>; });
    }
    onAdd(fuel: Fuel) {
      this.repo.post(this.fuelUrl, fuel).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(fuel: Fuel) {
    this.repo.update(`${this.fuelUrl}/${fuel.id}`, fuel).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(fuel: Fuel) {
    this.repo.delete(`${this.fuelUrl}/${fuel.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
