import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  constructor() { }
  handleError(error: HttpErrorResponse) {
    let message = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      message = `Error: ${error.error.message}`;
    } else {
      // server-side error
      message = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(message);
  }
}
