import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculationsHistoryService {

  constructor(private _httpClient: HttpClient) { }
  getCalculationsHistory(customerId: number, pageNumber: number) {  
    return this._httpClient.get<any>(`${environment.uri}/calculations/history/get/${customerId}/${pageNumber}`);
  }
}
