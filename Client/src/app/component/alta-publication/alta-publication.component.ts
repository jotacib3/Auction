import { Vehicle } from './../../models/vehicle';
import { Component, OnInit } from '@angular/core';
import { NzNotificationService } from 'ng-zorro-antd';
import { RepositoryService } from '../../services/repository.service';
import { UserService } from '../../services/user.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-alta-publication',
  templateUrl: './alta-publication.component.html',
  styleUrls: ['./alta-publication.component.css']
})

// export class NzDemoInputNumberBasicComponent {
//   demoValue = 3;
// }
export class AltaPublicationComponent implements OnInit {

  private publicationUrl = 'publications';
  users: Array<User> = new Array<User>();
  publications: Vehicle[];
  private demovalue = 7;

  constructor(private repo: RepositoryService, private notification: NzNotificationService, private userService: UserService) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.repo.getPublications(null , 'ADMINGM', 1, 10).subscribe(data => {
      console.log(data.result);
      this.publications = data.result as Array<Vehicle>;
      console.log(this.publications);
    });

    this.userService.get(1, 10, 'EMPLOYEE').subscribe(data => {
      this.users = data.result as Array<User>;
    });
  }

  Authorize(publId) {
    this.repo.update(`${this.publicationUrl}/authorize/${publId}`, null).subscribe(() => {
      this.loadData();
      this.notification.success('Publicación', 'Se ha autorizado la publicación satisfactoriamente');
    });
  }

  Unauthorize(publId) {
    this.repo.delete(`${this.publicationUrl}/${publId}`).subscribe(() => {
      this.loadData();
      this.notification.success('Publicación', 'Se ha desautorizado la publicación satisfactoriamente');
    });
  }
}
