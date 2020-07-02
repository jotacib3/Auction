import { Injectable } from '@angular/core';
import { Observable } from '../../../node_modules/rxjs';
import 'rxjs/add/observable/throw';

@Injectable({
  providedIn: 'root'
})
export class ExceptionHandlerService {

constructor() { }

public handleError(error: any) {
  const aplicationError = error.headers.get('Application-Error');
  if (aplicationError) {
// tslint:disable-next-line: deprecation
    return Observable.throw(aplicationError);
  }
  const serverError = error.json();
  let modelStateErrors = '';
  if (serverError) {
    for (const key in serverError) {
      if (serverError[key]) {
       modelStateErrors += serverError[key] + '\n';
      }
    }
  }
// tslint:disable-next-line: deprecation
  return Observable.throw(
    modelStateErrors || 'Server error'
  );
}

}
