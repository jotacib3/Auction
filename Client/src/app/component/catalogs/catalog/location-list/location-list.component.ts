import { Component, OnInit } from '@angular/core';
import { Location } from '../../../../models/location';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-location-list',
  templateUrl: './location-list.component.html',
  styleUrls: ['./location-list.component.css']
})
export class LocationListComponent implements OnInit {

  locations: Array<Location> = new Array<Location>();
  private locationUrl = 'locations';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.locationUrl).subscribe(data => {this.locations = data as Array<Location>; });
    }
    onAdd(location: Location) {
      this.repo.post(this.locationUrl, location).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(location: Location) {
    this.repo.update(`${this.locationUrl}/${location.id}`, location).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(location: Location) {
    this.repo.delete(`${this.locationUrl}/${location.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
