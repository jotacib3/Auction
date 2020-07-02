import { element } from 'protractor';
import { User } from './../../models/user';
import { UserService } from '../../services/user.service';
import { RepositoryService } from '../../services/repository.service';
import { NzNotificationService } from '../../../../node_modules/ng-zorro-antd';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';

@Component({
  selector: 'app-alta-employee',
  templateUrl: './alta-employee.component.html',
  styleUrls: ['./alta-employee.component.css']
})
export class AltaEmployeeComponent implements OnInit {

  private userUrl = 'user';
  users: User[];
  dic: { [key: number]: boolean } = {};

  constructor(private repo: RepositoryService, private notification: NzNotificationService, private userService: UserService) { }

  ngOnInit() {
    this.loadData();
    this.UpdateDictionary();
  }

  loadData() {
    this.userService.get(1, 10, 'EMPLOYEE').subscribe(data => {
        this.users = data.result;
        console.log(this.users);
      });
  }

  Authorize(userId) {
    this.userService.post(`${this.userUrl}/authorize/${userId}`, null).subscribe(() => {
      this.loadData();
      this.notification.success('Usuario', 'Se ha autorizado al usuario satisfactoriamente');
    });
  }

  Unauthorize(userId) {
    this.userService.delete(`${this.userUrl}/${userId}`).subscribe(() => {
      this.loadData();
      this.notification.success('Usuario', 'Se ha desautorizado al usuario satisfactoriamente');
    });
  }

  UpdateDictionary() {
     this.users.forEach(elem => {
       this.dic[elem.id] = false;
      });
  }

  Expand(userId) {
    this.dic[userId] = !this.dic[userId];
  }
}
