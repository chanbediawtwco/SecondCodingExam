using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Services
{
    public class BenefitHistoryService : IBenefitHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IAuditService _auditService;
        private readonly IAccountService _accountService;
        private readonly SecondCodingExamDbContext _context;
        private readonly IPaginationService _paginationService;
        public BenefitHistoryService(
            IMapper mapper,
            IJwtService jwtService,
            IAuditService auditService,
            IAccountService accountService,
            SecondCodingExamDbContext context,
            IPaginationService paginationService)
        {
            _mapper = mapper;
            _context = context;
            _jwtService = jwtService;
            _auditService = auditService;
            _accountService = accountService;
            _paginationService = paginationService;
        }
        public async Task<BenefitsHistory> GetBenefitHistoriesById(int BenefitId)
        {
            BenefitsHistory? BenefitsHistory = await _context.BenefitsHistories
                .Where(BenefitHistory => BenefitHistory.Id == BenefitId && !BenefitHistory.IsDeleted)
                .FirstOrDefaultAsync();
            if (BenefitsHistory == null) throw new Exception(Constants.NoBenefitFound);
            return BenefitsHistory;
        }
        private async Task<CustomersBenefitsHistory> GetCustomersBenefitHistoryById(int CustomerBenefitHistoryId)
            => await _context.CustomersBenefitsHistories
            .Where(CustomersBenefitsHistory => CustomersBenefitsHistory.Id == CustomerBenefitHistoryId)
            .FirstAsync();
        public async Task<IAsyncEnumerable<BenefitsHistory>> GetBenefitHistories(int BenefitId, int PageNumber)
            => await Task.FromResult(_context.BenefitsHistories
            .Where(BenefitHistory => BenefitHistory.BenefitId == BenefitId && !BenefitHistory.IsDeleted)
            .OrderByDescending(BenefitHistory => BenefitHistory.ModifiedDate)
            .Skip(_paginationService.GetPageNumber(PageNumber))
            .Take(Constants.PageSize)
            .AsAsyncEnumerable());
        public async Task<IAsyncEnumerable<CustomersBenefitsHistory>> GetCustomersBenefitHistory(int CustomerId, int PageNumber)
            => await Task.FromResult(_context.CustomersBenefitsHistories
            .Where(CustomersBenefitsHistory => CustomersBenefitsHistory.CustomerId == CustomerId
            && !CustomersBenefitsHistory.IsDeleted)
            .Skip(_paginationService.GetPageNumber(PageNumber))
            .Take(Constants.PageSize)
            .AsAsyncEnumerable());
        public async Task<int> GetCustomersBenefitsHistoryId(int CurrentBenefitId)
            => await _context.CustomersBenefitsHistories.Where(CustomersBenefitsHistory => CustomersBenefitsHistory.CustomersCurrentBenefitsId == CurrentBenefitId)
            .Select(CustomersBenefitsHistory => CustomersBenefitsHistory.CustomersCurrentBenefitsId)
            .FirstOrDefaultAsync();
        public async Task DeleteBenefitHistory(int BenefitId)
        {
            User User = await _accountService.GetUserById(await _jwtService.GetUserIdFromToken());
            BenefitsHistory DbBenefit = await GetBenefitHistoriesById(BenefitId);
            DbBenefit.IsDeleted = true;
            await _auditService.AddAuditStamp(DbBenefit, await _accountService.GetUserFullname(User), DateTime.Now, true);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCustomerBenefitHistory(int CustomerBenefitHistoryId)
        {
            User User = await _accountService.GetUserById(await _jwtService.GetUserIdFromToken());
            CustomersBenefitsHistory DbCustomersBenefitsHistory = await GetCustomersBenefitHistoryById(CustomerBenefitHistoryId);
            DbCustomersBenefitsHistory.IsDeleted = true;
            await _auditService.AddAuditStamp(DbCustomersBenefitsHistory, await _accountService.GetUserFullname(User), DateTime.Now, true);
            await _context.SaveChangesAsync();
        }
        public async Task MapBenefitToBenefitHistory(CustomersCurrentBenefit CurrentBenefit, string UserFullname, DateTime Timestamp)
        {
            CustomersBenefitsHistory CustomersBenefitsHistory = _mapper.Map<CustomersBenefitsHistory>(CurrentBenefit);
            await _auditService.AddAuditStamp(CustomersBenefitsHistory, UserFullname, Timestamp, true);
            _context.CustomersBenefitsHistories.Add(CustomersBenefitsHistory);
            await _context.SaveChangesAsync();
        }
    }
}
