import { Component } from '@angular/core';
import { Constants } from 'src/app/constant';
import { ICustomerBenefitHistory } from 'src/app/interface/customer.benefit.history.interface';
import { ICustomerBenefit } from 'src/app/interface/customer.benefit.interface';
import { ICustomerHistory } from 'src/app/interface/customer.history.interface';
import { ICustomer } from 'src/app/interface/customer.interface';
import { BenefitsService } from 'src/app/service/benefits/benefits.service';
import { CustomersService } from 'src/app/service/customers/customers.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent {
  constructor(private _customerService: CustomersService,
    private _benefitService: BenefitsService) {}
  pageNumber: number = 0;
  pageCustomerHistoryNumber: number = 0;
  pageCustomerBenefitHistoryNumber: number = 0;
  newCustomer: ICustomer = {};
  edittedCustomer?: ICustomer;
  customerBenefit?: ICustomerBenefit;
  customers: ICustomer[] = [];
  customerHistories: ICustomerHistory[] = [];
  customerBenefitHistories: ICustomerBenefitHistory[] = [];
  isAddCustomerShown: boolean = false;
  hasError: boolean = false;

  ngOnInit(){
    this.getCustomers(this.pageNumber);
  }
  getCustomers(increments: number){
    this.pageNumber += increments;
    if (this.pageNumber < 1) {
      this.pageNumber = 1;
    }
    this._customerService.getCustomers(this.pageNumber)
    .subscribe(res => {
      this.customers = res;
      if (res.length <= 0) {
        this.pageNumber -= 1;
      }
    });
  }
  getCustomerInformation(customerId?: number){
    this.pageCustomerHistoryNumber = 0;
    this.getCustomerHistories(this.pageCustomerHistoryNumber, Number(customerId));
    this.getCustomersBenefits(Number(customerId));
  }
  getCustomerHistories(increments: number, customerId?: number){
    this.pageCustomerHistoryNumber += increments;
    if (this.pageCustomerHistoryNumber < 1) {
      this.pageCustomerHistoryNumber = 1;
    }
    this._customerService.getCustomerHistory(this.pageCustomerHistoryNumber, Number(customerId))
    .subscribe(res => {
      this.customerHistories = res;
      if (res.length <= 0) {
        this.pageCustomerHistoryNumber -= 1;
      }
    });
  }
  getCustomersBenefits(customerId: number){
    this._benefitService.getCustomersBenefits(customerId)
    .subscribe(res => {
      this.customerBenefit = res;
    });
  }
  getCustomersBenefitHistory(customerId?: number){
    this.pageCustomerBenefitHistoryNumber = 0;
    this.getCustomersBenefitHistories(this.pageCustomerHistoryNumber, Number(customerId));
  }
  getCustomersBenefitHistories(increments: number, customerId?: number){
    this.pageCustomerBenefitHistoryNumber += increments;
    if (this.pageCustomerBenefitHistoryNumber < 1) {
      this.pageCustomerBenefitHistoryNumber = 1;
    }
    this._benefitService.getCustomersBenefitHistory(Number(customerId), this.pageCustomerBenefitHistoryNumber)
    .subscribe(res => {
      this.customerBenefitHistories = res;
      if (res.length <= 0) {
        this.pageCustomerBenefitHistoryNumber -= 1;
      }
    });
  }
  deleteCustomer(customerId?: number){
    this._customerService.deleteCustomer(Number(customerId))
    .subscribe(res => {
      if (res == null) {
        window.location.reload();
      }
    })
  }
  deleteCustomerHistory(customerHistoryId?: number){
    this._customerService.deleteCustomerHistory(Number(customerHistoryId))
    .subscribe(res => {
      if (res == null) {
        window.location.reload();
      }
    })
  }
  deleteCustomerBenefitHistory(customerBenefitId?: number){
    this._benefitService.deleteCustomerBenefitHistory(Number(customerBenefitId))
    .subscribe(res => {
      if (res == null) {
        window.location.reload();
      }
    })
  }
  toggleAddCustomer(){
    this.isAddCustomerShown = !this.isAddCustomerShown;
  }
  addCustomer(){
    let benefitId: number = Number(this.newCustomer.benefitId);
    let basicSalary: number = Number(this.newCustomer.basicSalary);
    let birthdate = this.fixDatepickerBirthdateProblem(this.newCustomer.birthdate);

    this.hasError = isNaN(benefitId) 
    || this.newCustomer.firstname == Constants.string.Empty
    || this.newCustomer.lastname == Constants.string.Empty
    || isNaN(basicSalary)
    || this.newCustomer.birthdate == Constants.string.Empty;

    if (!this.hasError) {
      // Save the changes if the record has no error and is not from records
      if (this.edittedCustomer == undefined) {
        this.newCustomer.birthdate = birthdate;
        this._customerService.addCustomer(this.newCustomer)
        .subscribe(res => { 
          if(res == null) {
            window.location.reload();
          }
        });
      }

      // Save the changes if the record has no error and is from records
      if (this.edittedCustomer != undefined) {
        this.edittedCustomer.benefitId = this.newCustomer.benefitId;
        this.edittedCustomer.firstname = this.newCustomer.firstname;
        this.edittedCustomer.lastname = this.newCustomer.lastname;
        this.edittedCustomer.basicSalary = this.newCustomer.basicSalary;
        this.edittedCustomer.birthdate = birthdate;
        this._customerService.updateCustomer(this.edittedCustomer)
        .subscribe(res => { 
          if(res == null) {
            window.location.reload();
          }
        });
      }
    }
  }
  editCustomer(id?: number){
    let customer = this.findCustomerById(id);
    if (customer != undefined)
    {
      this.newCustomer.benefitId = customer.benefitId;
      this.newCustomer.firstname = customer.firstname;
      this.newCustomer.lastname = customer.lastname;
      this.newCustomer.basicSalary = customer.basicSalary;
      this.newCustomer.birthdate = customer.birthdate;
      this.edittedCustomer = customer;
    }
    this.isAddCustomerShown = true;
  }
  findCustomerById(id?: number){
    return this.customers.find((customer: ICustomer) => customer.id == id);
  }
  fixDatepickerBirthdateProblem(birthdate?: string){
    if (birthdate != null || birthdate != undefined){
      let newbirthdate = new Date(birthdate);
      return new Date(newbirthdate.getFullYear(), 
        newbirthdate.getMonth(), 
        newbirthdate.getDate(), 
        newbirthdate.getHours(), 
        newbirthdate.getMinutes() - 
        newbirthdate.getTimezoneOffset())
        .toISOString();
    }
    return "";
  }
}
