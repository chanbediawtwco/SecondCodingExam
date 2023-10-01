import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { IBenefit } from 'src/app/interface/benefit.interface';

@Injectable({
  providedIn: 'root'
})
export class BenefitsService {

  constructor(private _httpClient: HttpClient) { }
  getBenefits(pageNumber: number) {  
    return this._httpClient.get<any>(`${environment.uri}/benefit/get/${pageNumber}`);
  }
  getBenefitsHistory(benefitId: number, pageNumber: number) {  
    return this._httpClient.get<any>(`${environment.uri}/benefit/history/get/${benefitId}/${pageNumber}`);
  }
  getCustomersBenefits(customerId: number){
    return this._httpClient.get<any>(`${environment.uri}/benefit/current/customer/get/${customerId}`);;
  }
  getCustomersBenefitHistory(customersCurrentBenefitsId: number, pageNumber: number){
    return this._httpClient.get<any>(`${environment.uri}/benefit/history/customer/get/${customersCurrentBenefitsId}/${pageNumber}`);
  }
  addBenefit(newBenefit: IBenefit) {
    return this._httpClient.post<IBenefit>(`${environment.uri}/benefit/add`, newBenefit);
  }
  updateBenefit(Benefit: IBenefit) {
    return this._httpClient.put<IBenefit>(`${environment.uri}/benefit/update`, Benefit);
  }
  deleteBenefit(benefitId: number) {
    return this._httpClient.delete(`${environment.uri}/benefit/delete/${benefitId}`);
  }
  deleteBenefitHistory(benefitId: number) {
    return this._httpClient.delete(`${environment.uri}/benefit/history/delete/${benefitId}`);
  }
  deleteCustomerBenefitHistory(customerBenefitHistoryId: number){
    return this._httpClient.delete<any>(`${environment.uri}/benefit/customer/history/delete/${customerBenefitHistoryId}`);
  }
}
