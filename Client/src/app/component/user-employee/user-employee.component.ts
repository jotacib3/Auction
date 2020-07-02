import { element } from 'protractor';
import { User } from './../../models/user';
import { UserService } from '../../services/user.service';
import { RepositoryService } from '../../services/repository.service';
import { NzNotificationService } from '../../../../node_modules/ng-zorro-antd';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-employee',
  templateUrl: './user-employee.component.html',
  styleUrls: ['./user-employee.component.css']
})
export class UserEmployeeComponent implements OnInit {

  private userUrl = 'user';
  users: User[];
  isVisible: boolean;
  validateForm: FormGroup;
  userName: any;
  noTelefono: any;
  editUserId: any;

  constructor(private fb: FormBuilder, private repo: RepositoryService,
    private notification: NzNotificationService,
    private userService: UserService) { }

  ngOnInit() {
    this.loadData();
    this.isVisible = false;
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
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
    this.userService.post(`${this.userUrl}/unauthorize/${userId}`, null).subscribe(() => {
      this.loadData();
      this.notification.success('Usuario', 'Se ha autorizado al usuario satisfactoriamente');
    });
  }

  showModal(user: User) {
    console.log(user);
    this.isVisible = true;
    this.editUserId = user.id;
    this.validateForm.patchValue({
      userName: user.userName,
      password: user.employeeData.cellNumber
    });
  }

  handleOk(): void {
    console.log('Button ok clicked!');
    this.isVisible = false;
    this.userName = this.validateForm.get('userName').value;
    this.noTelefono = this.validateForm.get('password').value;
    this.userService.update(`${this.userUrl}/${this.editUserId}`,
    `{"id": "${this.editUserId}", "userName": "${this.userName}", "phoneNumber": "${this.noTelefono}"}`).subscribe(() => {
      this.loadData();
      this.notification.success('Empleado', 'Se ha editado al empleado satisfactoriamente');
    });
    this.validateForm.reset();
  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
    this.validateForm.reset();
  }

  onDelete(userId) {
    this.userService.delete(`${this.userUrl}/${userId}`).subscribe(() => {
      this.loadData();
      this.notification.success('Empleado', 'Se ha eliminado al empleado satisfactoriamente');
    });
  }
}
