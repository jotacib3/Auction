import { Component, OnInit } from '@angular/core';
import { City } from '../../../../models/city';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-city-list',
  templateUrl: './city-list.component.html',
  styleUrls: ['./city-list.component.css']
})
export class CityListComponent implements OnInit {

  cities: Array<City> = new Array<City>();
  private cityUrl = 'cities';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.cityUrl).subscribe(data => {this.cities = data as Array<City>; });
    }
    onAdd(city: City) {
      this.repo.post(this.cityUrl, city).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(city: City) {
    this.repo.update(`${this.cityUrl}/${city.id}`, city).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(city: City) {
    this.repo.delete(`${this.cityUrl}/${city.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }
}
