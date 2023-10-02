export interface ICustomerBenefitHistory {
    id?: number,
    customersCurrentBenefitsId?: number,
    guaranteedIssue: number,
    maxAgeLimit: number,
    minAgeLimit: number,
    maxRange: number,
    minRange: number,
    increments: number
}