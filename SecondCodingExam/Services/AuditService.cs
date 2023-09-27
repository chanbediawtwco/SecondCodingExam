using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Services
{
    public class AuditService : IAuditService
    {
        public async Task AddAuditStamp(Object Model, string UserFullname, DateTime Timestamp, bool IsModified)
        {
            switch (Model.GetType().Name)
            {
                case Constants.Benefit:
                    Benefit Benefit = (Benefit)Model;
                    if (IsModified)
                    {
                        Benefit.ModifiedBy = UserFullname;
                        Benefit.ModifiedDate = Timestamp;
                    }
                    else
                    {
                        Benefit.CreatedBy = UserFullname;
                        Benefit.CreatedDate = Timestamp;
                    }
                    break;
                case Constants.BenefitsHistory:
                    BenefitsHistory BenefitsHistory = (BenefitsHistory)Model;
                    if (IsModified)
                    {
                        BenefitsHistory.ModifiedBy = UserFullname;
                        BenefitsHistory.ModifiedDate = Timestamp;
                    }
                    else
                    {
                        BenefitsHistory.BenefitCreatedBy = UserFullname;
                        BenefitsHistory.BenefitCreatedDate = Timestamp;
                    }
                    break;
                case Constants.Calculation:
                    Calculation Calculation = (Calculation)Model;
                    if (IsModified)
                    {
                        Calculation.ModifiedDate = Timestamp;
                    }
                    else
                    {
                        Calculation.CreatedDate = Timestamp;
                    }
                    break;
                case Constants.CalculationsHistory:
                    CalculationsHistory CalculationsHistory = (CalculationsHistory)Model;
                    if (IsModified)
                    {
                        CalculationsHistory.ModifiedDate = Timestamp;
                    }
                    else
                    {
                        CalculationsHistory.CreatedDate = Timestamp;
                    }
                    break;
                case Constants.Customer:
                    Customer Customer = (Customer)Model;
                    if (IsModified)
                    {
                        Customer.ModifiedBy = UserFullname;
                        Customer.ModifiedDate = Timestamp;
                    }
                    else
                    {
                        Customer.CreatedBy = UserFullname;
                        Customer.CreatedDate = Timestamp;
                    }
                    break;
                case Constants.CustomersBenefitsHistory:
                    CustomersBenefitsHistory CustomersBenefitsHistory = (CustomersBenefitsHistory)Model;
                    if (IsModified)
                    {
                        CustomersBenefitsHistory.ModifiedBy = UserFullname;
                        CustomersBenefitsHistory.ModifiedDate = Timestamp;
                    }
                    else
                    {
                        CustomersBenefitsHistory.BenefitCreatedBy = UserFullname;
                        CustomersBenefitsHistory.BenefitCreatedDate = Timestamp;
                    }
                    break;
                case Constants.CustomersCurrentBenefit:
                    CustomersCurrentBenefit CustomersCurrentBenefit = (CustomersCurrentBenefit)Model;
                    if (IsModified)
                    {
                        CustomersCurrentBenefit.ModifiedBy = UserFullname;
                        CustomersCurrentBenefit.ModifiedDate = Timestamp;
                    }
                    CustomersCurrentBenefit.CreatedBy = UserFullname;
                    CustomersCurrentBenefit.CreatedDate = Timestamp;
                    break;
                case Constants.CustomersHistory:
                    CustomersHistory CustomersHistory = (CustomersHistory)Model;
                    if (IsModified)
                    {
                        CustomersHistory.ModifiedBy = UserFullname;
                        CustomersHistory.ModifiedDate = Timestamp;
                    }
                    else
                    {
                        CustomersHistory.CreatedBy = UserFullname;
                        CustomersHistory.CreatedDate = Timestamp;
                    }
                    break;
                default: break;
            }
        }
        public async Task AddAuditStampToCalculation(IAsyncEnumerable<Calculation> Calculations, DateTime Timestamp)
        {
            await foreach (Calculation Calculation in Calculations)
            {
                Calculation.IsRecalculated = true;
                Calculation.ModifiedDate = Timestamp;
            }
        }
    }
}
