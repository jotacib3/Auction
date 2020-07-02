import { Component, OnInit } from '@angular/core';
import { Transmission } from '../../../../models/transmission';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-transmission-list',
  templateUrl: './transmission-list.component.html',
  styleUrls: ['./transmission-list.component.css']
})
export class TransmissionListComponent implements OnInit {

  transmissions: Array<Transmission> = new Array<Transmission>();
  private transmissionUrl = 'transmissions';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.transmissionUrl).subscribe(data => {this.transmissions = data as Array<Transmission>; });
    }
    onAdd(transmission: Transmission) {
      this.repo.post(this.transmissionUrl, transmission).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(transmission: Transmission) {
    this.repo.update(`${this.transmissionUrl}/${transmission.id}`, transmission).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(transmission: Transmission) {
    this.repo.delete(`${this.transmissionUrl}/${transmission.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
