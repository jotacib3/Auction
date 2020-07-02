import { Injectable } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { TokenParams } from '../models/token-params';

@Injectable({
  providedIn: 'root'
})
export class AdminGmGuardService {

  constructor(public authService: AuthService, public router: Router) { }

  canActivate(): boolean {
      if (!this.authService.loggedIn()) {
          this.router.navigate(['login']);
          return false;
      }

      if (this.authService.getRole() !== TokenParams.ROLE_ADMIN_GM) {
          this.router.navigate(['login']);
          return false;
      }

      return true;
  }

}
