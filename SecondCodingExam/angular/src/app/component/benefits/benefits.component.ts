import { Component } from '@angular/core';
import { Constants } from 'src/app/constant';
import { IBenefitHistory } from 'src/app/interface/benefit.history.interface';
import { IBenefit } from 'src/app/interface/benefit.interface';
import { BenefitsHistoryService } from 'src/app/service/benefits.history/benefits.history.service';
import { BenefitsService } from 'src/app/service/benefits/benefits.service';

@Component({
  selector: 'app-benefits',
  templateUrl: './benefits.component.html',
  styleUrls: ['./benefits.component.scss']
})
export class BenefitsComponent {
  constructor(private _benefitService: BenefitsService, private _benefitHistoryService: BenefitsHistoryService) {}
  benefits: IBenefit[] = [];
  benefitsHistories: IBenefitHistory[] = [];
  pageNumber: number = 0;
  benefitHistoryPageNumber: number = 1;
  isAddBenefitShown: boolean = false;
  hasError: boolean = false;
  newBenefit: IBenefit = {}
  edittedBenefit?: IBenefit;

  ngOnInit(){
    this.getBenefits(this.pageNumber);
  }
  
  getBenefits(increments: number){
    this.pageNumber += increments;
    if (this.pageNumber < 1) {
      this.pageNumber = 1;
    }
    this._benefitService.getBenefits(this.pageNumber)
    .subscribe(res => {
      this.benefits = res;
      if (res.length <= 0) {
        this.pageNumber -= 1;
      }
    });
  }

  getBenefitHistory(benefitId?: number){
    this.benefitHistoryPageNumber = 1;
    this.getHistory(0, Number(benefitId));
  }

  getHistory(increments: number, benefitId?: number){
    this.benefitHistoryPageNumber += increments;
    if (this.benefitHistoryPageNumber < 1) {
      this.benefitHistoryPageNumber = 1;
    }
    this._benefitHistoryService.getBenefitsHistory(Number(benefitId), this.benefitHistoryPageNumber)
      .subscribe(res => {
        this.benefitsHistories = res;
        if (res.length <= 0) {
          this.benefitHistoryPageNumber -= 1;
        }
      }
    );
  }

  toggleAddBenefit(){
    this.isAddBenefitShown = !this.isAddBenefitShown;
    this.initializeValue();
  }

  async addBenefit(){
    let guaranteedIssue: number = Number(this.newBenefit.guaranteedIssue);
    let maxAgeLimit: number = Number(this.newBenefit.maxAgeLimit);
    let minAgeLimit: number = Number(this.newBenefit.minAgeLimit);
    let maxRange: number = Number(this.newBenefit.maxRange);
    let minRange: number = Number(this.newBenefit.minRange);
    let increments: number = Number(this.newBenefit.increments);

    this.hasError = isNaN(guaranteedIssue) 
    || isNaN(maxAgeLimit)
    || isNaN(minAgeLimit)
    || isNaN(maxRange)
    || isNaN(minRange)
    || isNaN(increments);
    
    if (!this.hasError) {
      // Save the changes if the record has no error and is not from records
      if (this.edittedBenefit == undefined) {
        await this._benefitService.addBenefit(this.newBenefit)
        .subscribe(res => { 
          if(res == null) {
            window.location.reload();
          }
        });
      }

      // Save the changes if the record has no error and is from records
      if (this.edittedBenefit != undefined) {
        this.edittedBenefit.guaranteedIssue = this.newBenefit.guaranteedIssue;
        this.edittedBenefit.maxAgeLimit = this.newBenefit.maxAgeLimit;
        this.edittedBenefit.minAgeLimit = this.newBenefit.minAgeLimit;
        this.edittedBenefit.maxRange = this.newBenefit.maxRange;
        this.edittedBenefit.minRange = this.newBenefit.minRange;
        this.edittedBenefit.increments = this.newBenefit.increments;
        await this._benefitService.updateBenefit(this.edittedBenefit)
        .subscribe(res => { 
          if(res) {
            window.location.reload();
          }
        });
      }
    }
  }

  editBenefit(id?: number){
    let benefit = this.findBenefitById(id);
    if (benefit != undefined)
    {
      this.newBenefit.guaranteedIssue = benefit.guaranteedIssue;
      this.newBenefit.maxAgeLimit = benefit.maxAgeLimit;
      this.newBenefit.minAgeLimit = benefit.minAgeLimit;
      this.newBenefit.maxRange = benefit.maxRange;
      this.newBenefit.minRange = benefit.minRange;
      this.newBenefit.increments = benefit.increments;
      this.edittedBenefit = benefit;
    }
    this.isAddBenefitShown = true;
  }

  findBenefitById(id?: number){
    return this.benefits.find((benefit: IBenefit) => benefit.id == id);
  }

  deleteBenefit(id?: number){
    this._benefitService.deleteBenefit(Number(id))
    .subscribe(res => { return res });
    window.location.reload();
  }

  deleteBenefitHistory(id: number){
    this._benefitHistoryService.deleteBenefitHistory(id)
    .subscribe(res => { return res });
    window.location.reload();
  }

  initializeValue(){
    this.newBenefit.id = undefined;
    this.newBenefit.guaranteedIssue = Constants.string.Empty;
    this.newBenefit.maxAgeLimit = Constants.string.Empty;
    this.newBenefit.minAgeLimit = Constants.string.Empty;
    this.newBenefit.maxRange = Constants.string.Empty;
    this.newBenefit.minRange = Constants.string.Empty;
    this.newBenefit.increments = Constants.string.Empty;
  }
}
