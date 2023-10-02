export interface ICalculationHistory {
    id?: number,
    customersCurrentBenefitsId?: number,
    customerId?: number,
    multiple?: number,
    benefitsAmountQuotation?: number,
    pendedAmount?: number,
    currentBenefit?: string,
    createdDate: Date,
    modifiedDate: Date
}