import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '../../../../../node_modules/@angular/forms';
import { ActivatedRoute, Router } from '../../../../../node_modules/@angular/router';
import { NzNotificationService } from '../../../../../node_modules/ng-zorro-antd';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit, OnDestroy {

  validateForm: FormGroup;

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }
    this.userService.postPassword('auth/ResetPassword', this.validateForm.value).subscribe(data => {
      this.notification.success('Registro',
       'Por favor espere a que se le envie un email confirmando que fue registrado o explicandole los motivos de lrechazo');
      this.router.navigate(['/login'], {relativeTo: this.route}); },
      error => {this.notification.error('', error);
    });
  }
  updateConfirmValidator(): void {
    /** wait for refresh value */
    Promise.resolve().then(() => this.validateForm.controls.checkPassword.updateValueAndValidity());
  }

  confirmationValidator = (control: FormControl): { [s: string]: boolean } => {
    if (!control.value) {
      return { required: true };
    } else if (control.value !== this.validateForm.controls.password.value) {
      return { confirm: true, error: true };
    }
    return {};
  }

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router,
              private notification: NzNotificationService, public userService: UserService) {
  }

  ngOnInit(): void {
    document.body.className = 'login-background';
    this.validateForm = this.fb.group({
      code: [null, [Validators.required]],
      email: [null, [Validators.email, Validators.required]],
      password: [null, [Validators.required]],
      checkPassword: [null, [Validators.required, this.confirmationValidator]]
    });
  }

  ngOnDestroy() {
    document.body.className = '';
  }

}
