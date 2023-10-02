import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculationsService {

  constructor(private _httpClient: HttpClient) { }
  getCalculations(customerId: number) {
    return this._httpClient.get<any>(`${environment.uri}/calculations/get/${customerId}`);
  }
}
