import { User } from './../../models/user';
import { UserService } from '../../services/user.service';
import { RepositoryService } from '../../services/repository.service';
import { NzNotificationService } from '../../../../node_modules/ng-zorro-antd';
import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-distributor',
  templateUrl: './user-distributor.component.html',
  styleUrls: ['./user-distributor.component.css']
})
export class UserDistributorComponent implements OnInit {

  private userUrl = 'user';
  users: User[];
  isVisible: boolean;
  validateForm: FormGroup;
  userName: any;
  noTelefono: any;
  editUserId: any;

  constructor(private fb: FormBuilder,
    private repo: RepositoryService,
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
    this.userService.get(1, 10, 'DISTRIBUTOR').subscribe(data => this.users = data.result);
  }

    showModal(user: User) {
      this.isVisible = true;
      this.editUserId = user.id;
      this.validateForm.patchValue({
        userName: user.userName,
        password: user.email
      });
    }

    handleOk(): void {
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
