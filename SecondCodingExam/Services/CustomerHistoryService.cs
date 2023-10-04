using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;
using SecondCodingExam.Validators;

namespace SecondCodingExam.Services
{
    public class CustomerHistoryService : ICustomerHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IAuditService _auditService;
        private readonly IBenefitHistoryService _benefitHistoryService;
        private readonly IAccountService _accountService;
        private readonly SecondCodingExamDbContext _context;
        private readonly IPaginationService _paginationService;
        private readonly ICalculationService _calculationService;
        public CustomerHistoryService(
            IMapper mapper,
            IAuditService auditService,
            IBenefitHistoryService benefitHistoryService,
            IAccountService accountService,
            SecondCodingExamDbContext context,
            IPaginationService paginationService,
            ICalculationService calculationService)
        {
            _mapper = mapper;
            _context = context;
            _auditService = auditService;
            _benefitHistoryService = benefitHistoryService;
            _accountService = accountService;
            _paginationService = paginationService;
            _calculationService = calculationService;

        }
        public async Task DeleteCustomerHistory(int CustomerHistoryId)
        {
            CustomersHistory Customer = await GetCustomerHistoryById(CustomerHistoryId);
            Customer.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        public async Task<IAsyncEnumerable<CustomersHistory>> GetCustomerHistory(int PageNumber, int CustomerId)
            => await Task.FromResult(_context.CustomersHistories
                .Where(CustomerHistories => CustomerHistories.CustomerId == CustomerId
                && !CustomerHistories.IsDeleted)
                .OrderByDescending(CustomerHistories => CustomerHistories.ModifiedDate)
                .Skip(_paginationService.GetPageNumber(PageNumber))
                .Take(Constants.PageSize)
                .AsAsyncEnumerable());
        private async Task<CustomersHistory> GetCustomerHistoryById(int CustomerHistoryId)
        {
            CustomersHistory? CustomersHistory = await _context.CustomersHistories
            .Where(CustomersHistory => CustomersHistory.Id == CustomerHistoryId && !CustomersHistory.IsDeleted)
            .FirstOrDefaultAsync();
            if (CustomersHistory == null) throw new Exception(Constants.CustomerNotFound);
            return CustomersHistory;
        }
        public async Task MapCustomerHistoryData(Customer Customer, CustomersCurrentBenefit CurrentBenefit, DateTime Timestamp)
        {
            User User = await _accountService.GetUserById(Customer.UserId);
            string UserFullname = await _accountService.GetUserFullname(User);
            await _benefitHistoryService.MapBenefitToBenefitHistory(CurrentBenefit, UserFullname, Timestamp);
            await _calculationService.MapPreviousCalculationsToHistory(Customer.Id, Timestamp);
            await MapCustomerToHistory(Customer, CurrentBenefit, UserFullname, Timestamp);
        }
        private async Task MapCustomerToHistory(Customer Customer, CustomersCurrentBenefit CurrentBenefit, string UserFullname, DateTime Timestamp)
        {
            CustomersHistory CustomerHistory = await Task.FromResult(_mapper.Map<CustomersHistory>(Customer));
            CustomerHistory.CustomersBenefitsHistoryId = await _benefitHistoryService.GetCustomersBenefitsHistoryId(CurrentBenefit.BenefitId);
            await _auditService.AddAuditStamp(CustomerHistory, UserFullname, Timestamp, true);
            _context.CustomersHistories.Add(CustomerHistory);
        }
    }
}
