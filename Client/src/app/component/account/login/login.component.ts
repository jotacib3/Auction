import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { NzNotificationService } from '../../../../../node_modules/ng-zorro-antd';
import { AuthService } from '../../../services/auth.service';
import { UserService } from '../../../services/user.service';
import { PaginatedResult } from '../../../models/pagination';
import { User } from '../../../models/user';
import { Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { TokenParams } from 'src/app/models/token-params';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  validateForm: FormGroup;
  passwordVisible = false;
  paginated: PaginatedResult<User[]> = new PaginatedResult<User[]>();

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }
     this.auth.login(this.validateForm.value).subscribe(data => {
      const role = this.auth.getRole();
      if (this.auth.decodeToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authorizationdecision'] !== 'True') {
          this.notification.error('Inicio de sesíon.', 'Su usuario no ha sido autorizado. Espere confirmación vía correo electrónico.');
          this.auth.logout();
          return;
      }
      switch (role) {
        case TokenParams.ROLE_EMPLOYEE: {
            this.router.navigate(['/user/employee']);
            break;
        }
        case TokenParams.ROLE_DISTRIBUTOR: {
            this.router.navigate(['/user/distributor']);
            break;
        }
        case TokenParams.ROLE_ADMIN_GM: {
            this.router.navigate(['/user/admin-gm']);
            break;
        }
        case TokenParams.ROLE_ADMIN_AMDGM: {
            this.router.navigate(['/user/admin-amdgm']);
            break;
        }
    }
    }, error => {
      this.notification.error('Inicio de sesión.', 'Su usuario o contraseña son incorrectos.');
    });
  }

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router,
              private notification: NzNotificationService, public auth: AuthService) {
  }

  ngOnInit(): void {
    document.body.className = 'login-background';
    this.validateForm = this.fb.group({
      email: [ null, [ Validators.email, Validators.required] ],
      password: [ null, [ Validators.required, Validators.minLength(6)] ]
    });

  }

  ngOnDestroy() {
    document.body.className = '';
  }

}
