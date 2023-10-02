export interface ICustomerBenefit {
    id?: number,
    benefitId: number,
    customersCurrentBenefitsId?: number,
    guaranteedIssue: number,
    maxAgeLimit: number,
    minAgeLimit: number,
    maxRange: number,
    minRange: number,
    increments: number
}