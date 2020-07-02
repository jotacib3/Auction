import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';
import { Pack } from '../../../../models/pack';

@Component({
  selector: 'app-pack-list',
  templateUrl: './pack-list.component.html',
  styleUrls: ['./pack-list.component.css']
})
export class PackListComponent implements OnInit {

  packs: Array<Pack> = new Array<Pack>();
  private packUrl = 'packs';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.packUrl).subscribe(data => {this.packs = data as Array<Pack>; });
    }
    onAdd(pack: Pack) {
      this.repo.post(this.packUrl, pack).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(pack: Pack) {
    this.repo.update(`${this.packUrl}/${pack.id}`, pack).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(pack: Pack) {
    this.repo.delete(`${this.packUrl}/${pack.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
