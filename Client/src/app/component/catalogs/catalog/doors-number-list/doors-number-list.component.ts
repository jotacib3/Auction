import { Component, OnInit } from '@angular/core';
import { DoorsNumber } from '../../../../models/doors-number';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-doors-number-list',
  templateUrl: './doors-number-list.component.html',
  styleUrls: ['./doors-number-list.component.css']
})
export class DoorsNumberListComponent implements OnInit {

  doors: Array<DoorsNumber> = new Array<DoorsNumber>();
  private doorsUrl = 'doorsnumbers';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.doorsUrl).subscribe(data => {this.doors = data as Array<DoorsNumber>; });
    }
    onAdd(door: DoorsNumber) {
      this.repo.post(this.doorsUrl, door).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(door: DoorsNumber) {
    this.repo.update(`${this.doorsUrl}/${door.id}`, door).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(door: DoorsNumber) {
    this.repo.delete(`${this.doorsUrl}/${door.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
