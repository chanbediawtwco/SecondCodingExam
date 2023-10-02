import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CustomerHistoryService {

  constructor(private _httpClient: HttpClient) { }
  getCustomerHistory(pageNumber: number, customerId: number){
    return this._httpClient.get<any>(`${environment.uri}/customers/history/get/${pageNumber}/${customerId}`);
  }
  deleteCustomerHistory(customerHistoryId: number){
    return this._httpClient.delete<any>(`${environment.uri}/customers/history/delete/${customerHistoryId}`);
  }
}
