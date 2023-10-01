import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { ICustomer } from 'src/app/interface/customer.interface';

@Injectable({
  providedIn: 'root'
})
export class CustomersService {

  constructor(private _httpClient: HttpClient) { }
  getCustomers(pageNumber: number){
    return this._httpClient.get<any>(`${environment.uri}/customers/get/${pageNumber}`);
  }
  getCustomerHistory(pageNumber: number, customerId: number){
    return this._httpClient.get<any>(`${environment.uri}/customers/history/get/${pageNumber}/${customerId}`);
  }
  deleteCustomer(customerId: number){
    return this._httpClient.delete<any>(`${environment.uri}/customers/delete/${customerId}`);
  }
  deleteCustomerHistory(customerHistoryId: number){
    return this._httpClient.delete<any>(`${environment.uri}/customers/history/delete/${customerHistoryId}`);
  }
  addCustomer(Customer: ICustomer){
    return this._httpClient.post(`${environment.uri}/customers/add`, Customer);
  }
  updateCustomer(Customer: ICustomer){
    return this._httpClient.put(`${environment.uri}/customers/update`, Customer);
  }
}