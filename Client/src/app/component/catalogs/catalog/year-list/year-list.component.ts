import { Component, OnInit } from '@angular/core';
import { Year } from '../../../../models/year';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-year-list',
  templateUrl: './year-list.component.html',
  styleUrls: ['./year-list.component.css']
})
export class YearListComponent implements OnInit {

  years: Array<Year> = new Array<Year>();
  private yearUrl = 'years';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.yearUrl).subscribe(data => {this.years = data as Array<Year>; });
    }
    onAdd(year: Year) {
      this.repo.post(this.yearUrl, year).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(year: Year) {
    this.repo.update(`${this.yearUrl}/${year.id}`, year).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(year: Year) {
    this.repo.delete(`${this.yearUrl}/${year.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
