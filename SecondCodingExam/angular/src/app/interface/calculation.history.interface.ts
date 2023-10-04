export interface ICalculationHistory {
    id?: number,
    customersCurrentBenefitsId?: number,
    customerId?: number,
    multiple?: number,
    benefitsAmountQuotation?: number,
    pendedAmount?: number,
    currentBenefit?: number,
    isBenefitPending: boolean,
    createdDate: Date,
    modifiedDate: Date
}