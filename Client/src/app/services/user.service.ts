import { Injectable } from '@angular/core';
import { EnvironmentUrlService } from './environment-url.service';
import { PaginatedResult } from '../models/pagination';
import { User } from '../models/user';
import { AuthHttp } from '../../../node_modules/angular2-jwt';
import { Response, RequestOptions , Headers, Http} from '@angular/http';
import { Observable } from '../../../node_modules/rxjs';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import { ExceptionHandlerService } from '../security/exception-handler.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

// tslint:disable-next-line: deprecation
  constructor( private http: Http, private envUrl: EnvironmentUrlService, public autHttp: AuthHttp,
               private error: ExceptionHandlerService) { }

  public get(page?: number, itemPerPage?: number, role?: string) {
    const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
    let queryString = '';

    if (page != null && itemPerPage != null) {
      queryString = `?pageNumber=${page}&pageSize=${itemPerPage}`;
    }
    const route = `${this.createCompleteRoute(
                         'user', this.envUrl.urlAddress)}${queryString}&role=${role}`;
    return this.autHttp
      .get(route)
// tslint:disable-next-line: deprecation
      .map( (response: Response) => {
        paginatedResult.result = response.json();

        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return paginatedResult;
      }).catch(this.error.handleError);
  }

  public post(route: string, body: any) {
    return this.autHttp.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders())
              .catch(this.error.handleError);
  }

  public getById(route: string) {
    return this.autHttp.get(this.createCompleteRoute(route, this.envUrl.urlAddress), this.generateHeaders())
    .catch(this.error.handleError);
  }

  public postPhoto(route: string, body: any) {
    const formData: FormData = new FormData();
    formData.append('file', body);
    const miUrl = this.createCompleteRoute(route, this.envUrl.urlAddress);
    return this.http.post(miUrl, formData).map(res => res.ok)
              .catch(this.error.handleError);
  }

  public postPassword(route: string, body: any) {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders())
              .catch(this.error.handleError);
  }

  public update(route: string, body: any) {
    return this.autHttp.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders())
               .catch(this.error.handleError);
  }

  public delete(route: string) {
    return this.autHttp.delete(this.createCompleteRoute(route, this.envUrl.urlAddress))
               .catch(this.error.handleError);
  }

  private createCompleteRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }

  private generateHeaders() {
// tslint:disable-next-line: deprecation
    const headers = new Headers({'Content-Type': 'application/json'});
// tslint:disable-next-line: deprecation
    return new RequestOptions({ headers: headers });
  }
}
