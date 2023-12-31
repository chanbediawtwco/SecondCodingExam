export interface IBenefitHistory{
    id: number,
    userId: number,
    benefitId: number,
    guaranteedIssue: number,
    maxAgeLimit: number,
    minAgeLimit: number,
    maxRange: number,
    minRange: number,
    increments: number,
    isDeleted: boolean,
    benefitCreatedDate: Date,
    benefitCreatedBy: string,
    modifiedDate: Date,
    modifiedBy: string,
}