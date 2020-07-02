import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Http, Response , RequestOptions, Headers } from '@angular/http';
import { EnvironmentUrlService} from './environment-url.service';
import { Observable } from 'rxjs';
import { AuthHttp } from '../../../node_modules/angular2-jwt';
import { Vehicle } from '../models/vehicle';
import { PaginatedResult } from '../models/pagination';
import { ExceptionHandlerService } from '../security/exception-handler.service';
import { Offer } from '../models/offer';

@Injectable({
  providedIn: 'root'
})
export class RepositoryService {

  constructor(private http: Http, private envUrl: EnvironmentUrlService, private authHttp: AuthHttp,
              private error: ExceptionHandlerService) { }

  // for register metod
  public getReg(route: string) {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress)).map(response => response.json())
               .catch(this.error.handleError);
  }
  public get(route: string) {
    return this.authHttp.get(this.createCompleteRoute(route, this.envUrl.urlAddress)).map(response => response.json())
               .catch(this.error.handleError);
  }

  public getPublicationsFilters(body?: any, page?: number, itemPerPage?: number) {
    const paginatedResult: PaginatedResult<Vehicle[]> = new PaginatedResult<Vehicle[]>();
    let queryString = '';

    if (page != null && itemPerPage != null) {
      queryString = `?pageNumber=${page}&pageSize=${itemPerPage}`;
    }
    const route = `${this.createCompleteRoute(
                         'publications/filter', this.envUrl.urlAddress)}${queryString}`;
    return this.authHttp
      .post(route, body, this.generateHeaders())
// tslint:disable-next-line: deprecation
      .map( (response: Response) => {
        paginatedResult.result = response.json();

        if (response.headers.get('Pagination') != null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }

        return paginatedResult;
      }).catch(this.error.handleError);
  }

  public getPublications( id?: string, role?: string, page?: number, itemPerPage?: number) {

    const paginatedResult: PaginatedResult<Vehicle[]> = new PaginatedResult<Vehicle[]>();
    let queryString = '';

    if (page != null && itemPerPage != null) {
      queryString = `?id=${id}&role=${role}&pageSize=${itemPerPage}&pageSize=${itemPerPage}`;
    }
    const route = `${this.createCompleteRoute(
                         'publications', this.envUrl.urlAddress)}${queryString}`;
    return this.authHttp
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

  public getOffers( page?: number, pageSize?: number, id?: string, role?: string) {
    const paginatedResult: PaginatedResult<Offer[]> = new PaginatedResult<Offer[]>();
    let queryString = '';

    if (page != null && pageSize != null) {
      queryString = `?pageNumber=${page}&pageSize=${pageSize}&id=${id}&role=${role}`;
    }

    const route = `${this.createCompleteRoute(
      'offers', this.envUrl.urlAddress)}${queryString}`;
    return this.authHttp.get(route)
    .map( (response: Response) => {
      paginatedResult.result = response.json();
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
      }

      return paginatedResult;
    }).catch(this.error.handleError
    );
  }

  public post(route: string, body) {
    return this.authHttp.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body)
               .catch(this.error.handleError);
  }

  public update(route: string, body) {
    return this.authHttp.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders())
               .catch(this.error.handleError);
  }

  public getPhoto(name: string) {
       return this.createCompleteRoute(name, 'http://localhost:1316/Uploads');
  }

  public delete(route: string) {
    return this.authHttp.delete(this.createCompleteRoute(route, this.envUrl.urlAddress))
               .catch(this.error.handleError);
  }

  private createCompleteRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }

  private generateHeaders() {
    const headers = new Headers({'Content-Type': 'application/json'});
    return new RequestOptions({ headers: headers });

  }
}
