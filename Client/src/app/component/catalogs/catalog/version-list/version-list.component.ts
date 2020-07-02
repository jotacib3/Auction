import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../../../services/repository.service';
import { NzNotificationService } from '../../../../../../node_modules/ng-zorro-antd';
import { Version } from '../../../../models/version';

@Component({
  selector: 'app-version-list',
  templateUrl: './version-list.component.html',
  styleUrls: ['./version-list.component.css']
})
export class VersionListComponent implements OnInit {

  versions: Array<Version> = new Array<Version>();
  private versionUrl = 'versions';

    constructor(
        private repo: RepositoryService,
        private notification: NzNotificationService
    ) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
      this.repo.get(this.versionUrl).subscribe(data => {this.versions = data as Array<Version>; });
    }
    onAdd(version: Version) {
      this.repo.post(this.versionUrl, version).subscribe(() => {
          this.loadData();
          this.notification.success('Catálogo', 'Se ha adicionado el catálogo satisfactoriamente');
      });
  }

  onEdit(version: Version) {
    this.repo.update(`${this.versionUrl}/${version.id}`, version).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha editado el catálogo satisfactoriamente');
  });
  }

  onDelete(version: Version) {
    this.repo.delete(`${this.versionUrl}/${version.id}`).subscribe(() => {
      this.loadData();
      this.notification.success('Catálogo', 'Se ha eliminado el catálogo satisfactoriamente');
  });
  }

}
