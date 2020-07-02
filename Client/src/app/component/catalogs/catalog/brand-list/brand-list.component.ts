import { Component, OnInit } from '@angular/core';
import { Brand } from '../../../../models/brand';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-brand-list',
  templateUrl: './brand-list.component.html',
  styleUrls: ['./brand-list.component.css']
})
export class BrandListComponent implements OnInit {

  brands: Array<Brand> = new Array<Brand>();
  private brandUrl = 'brands';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.brandUrl).subscribe(data => {this.brands = data as Array<Brand>; });
    }
    onAdd(brand: Brand) {
      this.repo.post(this.brandUrl, brand).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(brand: Brand) {
    this.repo.update(`${this.brandUrl}/${brand.id}`, brand).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(brand: Brand) {
    this.repo.delete(`${this.brandUrl}/${brand.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }


}
