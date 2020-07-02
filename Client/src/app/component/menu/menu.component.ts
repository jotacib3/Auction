import { Component, OnInit } from '@angular/core';
import { TokenParams } from 'src/app/models/token-params';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from './../../models/user';
import { UserService } from '../../services/user.service';
import { NzNotificationService } from '../../../../node_modules/ng-zorro-antd';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  tokenParams: TokenParams;
  currentRole: string;
  email: string;
  employeeEnabled: boolean;
  distributorEnabled: boolean;
  admingmEnabled: boolean;
  adminamdgmEnabled: boolean;
  userId: any;
  userName: any;
  useremail: any;
  isVisible: boolean;
  validateForm: FormGroup;
  selected: boolean;
  noTelefono;
  user: User;

  constructor(
      private router: Router,
      private authService: AuthService,
      private fb: FormBuilder,
      private notification: NzNotificationService,
      private userService: UserService
  ) { }

  ngOnInit() {
    this.selected = true;
    this.isVisible = false;
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
    this.tokenParams = this.authService.getRoles();

      if (this.tokenParams) {
          this.userId = this.authService.getUserId();
          this.currentRole = this.authService.getRole();
          this.email = this.authService.getEmail();
          this.employeeEnabled = this.currentRole === TokenParams.ROLE_EMPLOYEE;
          this.distributorEnabled = this.currentRole === TokenParams.ROLE_DISTRIBUTOR;
          this.admingmEnabled = this.currentRole === TokenParams.ROLE_ADMIN_GM;
          this.adminamdgmEnabled = this.currentRole === TokenParams.ROLE_ADMIN_AMDGM;
      }
  }

  logout() {
      this.authService.logout();
      this.router.navigate(['login']);

  }

  changePassword() {
      this.router.navigate(['change-password']);
  }

  showModal() {
    this.isVisible = true;
    this.userService.getById(`user/${this.userId}`).subscribe(data => {
        console.log(data.json());
        this.userName = data.json().userName;
        this.noTelefono = data.json().employeeData.cellNumber;
        this.validateForm.patchValue({
            userName: this.userName,
            password: this.noTelefono
          });
    });
  }

  handleOk(): void {
    console.log('Button ok clicked!');
    this.isVisible = false;
    this.userName = this.validateForm.get('userName').value;
    this.noTelefono = this.validateForm.get('password').value;
    this.userService.update(`user/${this.userId}`,
    `{"id": "${this.userId}", "userName": "${this.userName}", "phoneNumber": "${this.noTelefono}"}`).subscribe(() => {
      this.notification.success('Empleado', 'Se ha editado al empleado satisfactoriamente');
    });
    this.validateForm.reset();
  }

  handleCancel(): void {
    this.isVisible = false;
    this.validateForm.reset();
  }

  onDelete(userId) {
    this.userService.delete(`user/${userId}`).subscribe(() => {
      this.notification.success('Empleado', 'Se ha eliminado al empleado satisfactoriamente');
    });
  }
}
