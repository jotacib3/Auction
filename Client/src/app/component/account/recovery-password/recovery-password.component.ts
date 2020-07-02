import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '../../../../../node_modules/@angular/forms';
import { PaginatedResult } from '../../../models/pagination';
import { User } from '../../../models/user';
import { ActivatedRoute, Router } from '../../../../../node_modules/@angular/router';
import { AuthService } from '../../../services/auth.service';
import { NzNotificationService } from '../../../../../node_modules/ng-zorro-antd';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-recovery-password',
  templateUrl: './recovery-password.component.html',
  styleUrls: ['./recovery-password.component.css']
})
export class RecoveryPasswordComponent implements OnInit, OnDestroy {

   validateForm: FormGroup;
   paginated: PaginatedResult<User[]> = new PaginatedResult<User[]>();

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }
    this.userService.postPassword('auth/ForgotPassword', this.validateForm.value).subscribe(data => {
      this.notification.success('Email', ' Please check you email');
      this.router.navigate(['/forgot-password'], {relativeTo: this.route}); },
      error => {
    });
  }

  constructor(private fb: FormBuilder, private route: ActivatedRoute, private router: Router,
              private notification: NzNotificationService, public auth: AuthService,
              private userService: UserService) {
  }

  ngOnInit(): void {
    document.body.className = 'login-background';
    this.validateForm = this.fb.group({
      email: [ null, [ Validators.email, Validators.required] ]
    });

  }

  ngOnDestroy() {
    document.body.className = '';
  }

}
