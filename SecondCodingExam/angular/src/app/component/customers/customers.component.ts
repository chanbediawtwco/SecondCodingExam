import { Component } from '@angular/core';
import { Constants } from 'src/app/constant';
import { IBenefit } from 'src/app/interface/benefit.interface';
import { ICalculationHistory } from 'src/app/interface/calculation.history.interface';
import { ICalculation } from 'src/app/interface/calculation.interface';
import { ICustomerBenefitHistory } from 'src/app/interface/customer.benefit.history.interface';
import { ICustomerBenefit } from 'src/app/interface/customer.benefit.interface';
import { ICustomerHistory } from 'src/app/interface/customer.history.interface';
import { ICustomer } from 'src/app/interface/customer.interface';
import { BenefitsHistoryService } from 'src/app/service/benefits.history/benefits.history.service';
import { BenefitsService } from 'src/app/service/benefits/benefits.service';
import { CalculationsHistoryService } from 'src/app/service/calculations.history/calculations.history.service';
import { CalculationsService } from 'src/app/service/calculations/calculations.service';
import { CustomerHistoryService } from 'src/app/service/customer.history/customer.history.service';
import { CustomersService } from 'src/app/service/customers/customers.service';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent {
  constructor(private _customerService: CustomersService,
    private _benefitService: BenefitsService, 
    private _benefitHistoryService: BenefitsHistoryService,
    private _customerHistoryService: CustomerHistoryService,
    private _calculationsService: CalculationsService,
    private _calculationsHistoryService: CalculationsHistoryService) {}
  pageNumber: number = 0;
  pageCustomerHistoryNumber: number = 0;
  pageCustomerBenefitHistoryNumber: number = 0;
  pageCustomerCalculationHistoryNumber: number = 0;
  newCustomer: ICustomer = {};
  edittedCustomer?: ICustomer;
  customerBenefit?: ICustomerBenefit;
  benefits: IBenefit[] = [];
  customers: ICustomer[] = [];
  calculations: ICalculation[] = [];
  calculationsHistories: ICalculationHistory[] = [];
  customerHistories: ICustomerHistory[] = [];
  customerBenefitHistories: ICustomerBenefitHistory[] = [];
  isAddCustomerShown: boolean = false;
  hasError: boolean = false;
  forApproval: string = Constants.ForApproval;

  ngOnInit(){
    this.getCustomers(this.pageNumber);
    this.getAllBenefits();
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
  getAllBenefits(){
    return this._benefitService.getAllBenefits()
    .subscribe(res => {
      this.benefits = res;
    });
  }
  getCustomersBenefits(customerId: number){
    this._benefitService.getCustomersBenefits(customerId)
    .subscribe(res => {
      this.customerBenefit = res;
    });
  }
  getCustomerInformation(customerId?: number){
    this.pageCustomerHistoryNumber = 0;
    this.getCustomerHistories(this.pageCustomerHistoryNumber, Number(customerId));
    this.getCustomersBenefits(Number(customerId));
    this.getCustomerCalculation(customerId);
    this.getCustomerCalculationHistory(customerId);
  }
  getCustomerHistories(increments: number, customerId?: number){
    this.pageCustomerHistoryNumber += increments;
    if (this.pageCustomerHistoryNumber < 1) {
      this.pageCustomerHistoryNumber = 1;
    }
    this._customerHistoryService.getCustomerHistory(this.pageCustomerHistoryNumber, Number(customerId))
    .subscribe(res => {
      this.customerHistories = res;
      if (res.length <= 0) {
        this.pageCustomerHistoryNumber -= 1;
      }
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
    this._benefitHistoryService.getCustomersBenefitHistory(Number(customerId), this.pageCustomerBenefitHistoryNumber)
    .subscribe(res => {
      this.customerBenefitHistories = res;
      if (res.length <= 0) {
        this.pageCustomerBenefitHistoryNumber -= 1;
      }
    });
  }
  getCustomerCalculation(customerId?: number){
    this._calculationsService.getCalculations(Number(customerId))
    .subscribe(res => {
      this.calculations = res;
    });
  }
  getCustomerCalculationHistory(customerId?: number){
    this.pageCustomerCalculationHistoryNumber = 0;
    this.getCustomerCalculationHistories(this.pageCustomerHistoryNumber, Number(customerId));
  }
  getCustomerCalculationHistories(increments: number, customerId?: number){
    this.pageCustomerCalculationHistoryNumber += increments;
    if (this.pageCustomerCalculationHistoryNumber < 1) {
      this.pageCustomerCalculationHistoryNumber = 1;
    }
    this._calculationsHistoryService.getCalculationsHistory(Number(customerId), this.pageCustomerCalculationHistoryNumber)
    .subscribe(res => {
      this.calculationsHistories = res;
      if (res.length <= 0) {
        this.pageCustomerCalculationHistoryNumber -= 1;
      }
    });
  }
  assignBenefitIdToCustomer(benefitId?: number){
    this.newCustomer.benefitId = benefitId != undefined || benefitId != null ? benefitId.toString() : Constants.string.Empty;
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
    this._customerHistoryService.deleteCustomerHistory(Number(customerHistoryId))
    .subscribe(res => {
      if (res == null) {
        window.location.reload();
      }
    })
  }
  deleteCustomerBenefitHistory(customerBenefitId?: number){
    this._benefitHistoryService.deleteCustomerBenefitHistory(Number(customerBenefitId))
    .subscribe(res => {
      if (res == null) {
        window.location.reload();
      }
    })
  }
  toggleAddCustomer(){
    this.isAddCustomerShown = !this.isAddCustomerShown;
  }
  editCustomer(id?: number){
    let customer = this.findCustomerById(id);
    if (customer != undefined)
    {
      this.newCustomer.benefitId = this.customerBenefit?.benefitId?.toString();
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
