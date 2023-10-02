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
  getAllBenefits() {  
    return this._httpClient.get<any>(`${environment.uri}/benefit/get/all`);
  }
  getCustomersBenefits(customerId: number){
    return this._httpClient.get<any>(`${environment.uri}/benefit/current/customer/get/${customerId}`);;
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
}
