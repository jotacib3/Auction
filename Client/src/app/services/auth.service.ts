import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { NzNotificationService } from '../../../node_modules/ng-zorro-antd';
import { tokenNotExpired, JwtHelper } from '../../../node_modules/angular2-jwt';
import { Observable } from '../../../node_modules/rxjs';
import { Http, Headers, RequestOptions, Response } from '../../../node_modules/@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { ExceptionHandlerService } from '../security/exception-handler.service';
import { TokenParams } from '../models/token-params';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  tokenParams: TokenParams;
  userToken: any;
  decodeToken: any;
  jwtHelper: JwtHelper = new JwtHelper();
// tslint:disable-next-line: deprecation
  constructor(private http: Http, private envUrl: EnvironmentUrlService,
              private notification: NzNotificationService, private error: ExceptionHandlerService ) {
                this.tokenParams = new TokenParams();
               }

  login(model: any) {
      console.log(model);
      return this.http.post(`${this.envUrl.urlAddress}/auth/login`, model, this.generateHeaders())
// tslint:disable-next-line: deprecation
        .map((response: Response) => {
          const user = response.json();
          if (user && user.tokenString) {
            localStorage.setItem('token', user.tokenString);
            this.decodeToken = this.jwtHelper.decodeToken(user.tokenString);
            console.log('Im decoded');
            console.log(this.decodeToken);
            this.userToken = user.tokenString;
          }
        }).catch(this.error.handleError);
  }

  register(model: any) {

    model.role = 'DISTRIBUTOR';
    return this.http.post(`${this.envUrl.urlAddress}/auth/register`, model, this.generateHeaders())
               .catch(this.error.handleError);
  }

  registerEmployee(model: any) {
    return this.http.post(`${this.envUrl.urlAddress}/auth/registerEmployee`, model, this.generateHeaders())
               .catch(this.error.handleError);
  }

  getRole() {
    return this.decodeToken.role;
  }

  getEmail() {
    return this.decodeToken.email;
  }

  getRoles() {
    return this.tokenParams;
  }

  getUserId () {
    return this.decodeToken.nameid;
  }

  logout() {
    this.userToken = null;
    localStorage.removeItem('token');
  }

  loggedIn() {
    return tokenNotExpired('token');
  }

  private generateHeaders() {
// tslint:disable-next-line: deprecation
    const headers = new Headers({'Content-Type': 'application/json'});
// tslint:disable-next-line: deprecation
    return new RequestOptions({ headers: headers });

  }

}

