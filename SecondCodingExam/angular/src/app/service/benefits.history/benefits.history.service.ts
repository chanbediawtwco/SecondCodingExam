import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BenefitsHistoryService {

  constructor(private _httpClient: HttpClient) { }
  getBenefitsHistory(benefitId: number, pageNumber: number) {  
    return this._httpClient.get<any>(`${environment.uri}/benefit/history/get/${benefitId}/${pageNumber}`);
  }
  getCustomersBenefitHistory(customersCurrentBenefitsId: number, pageNumber: number){
    return this._httpClient.get<any>(`${environment.uri}/benefit/history/customer/get/${customersCurrentBenefitsId}/${pageNumber}`);
  }
  deleteBenefitHistory(benefitId: number) {
    return this._httpClient.delete(`${environment.uri}/benefit/history/delete/${benefitId}`);
  }
  deleteCustomerBenefitHistory(customerBenefitHistoryId: number){
    return this._httpClient.delete<any>(`${environment.uri}/benefit/history/customer/delete/${customerBenefitHistoryId}`);
  }
}
