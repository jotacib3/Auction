import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '../../../../../node_modules/@angular/forms';
import { ActivatedRoute, Router } from '../../../../../node_modules/@angular/router';
import { AuthService } from '../../../services/auth.service';
import { NzNotificationService } from '../../../../../node_modules/ng-zorro-antd';
import { RepositoryService } from '../../../services/repository.service';
import { TokenParams } from '../../../models/token-params';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css']
})
export class PasswordChangeComponent implements OnInit , OnDestroy {

  validateForm: FormGroup;

  submitForm(): void {
    console.log('I get here');
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }
    this.validateForm.value.id = this.auth.getUserId();
    this.repo.post('auth/changepassword', this.validateForm.value).subscribe(data => {
         this.notification.success('success', 'Usted ha modificado su contraseña.');
         const role = this.auth.getRole();
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
    }}, error => {console.log(error); });
  }
  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() => this.validateForm.controls.confirmPassword.updateValueAndValidity());
  }

  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls.newPassword.value) {
      return { confirm: true, error: true };
    }
    return {};
  }

  constructor(private repo: RepositoryService, private fb: FormBuilder, private route: ActivatedRoute, private router: Router,
              private notification: NzNotificationService, public auth: AuthService) {
  }

  ngOnInit(): void {
    document.body.className = 'login-background';
    this.validateForm = this.fb.group({
      oldPassword: [null, [Validators.required]],
      newPassword: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      confirmPassword: [null, [Validators.required, this.confirmationValidator]]
    });
  }



  ngOnDestroy() {
    document.body.className = '';
  }

}
