using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Dto;
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
        public async Task<IAsyncEnumerable<BenefitsHistoryDto>> GetBenefitHistories(int BenefitId, int PageNumber)
        {
            List<BenefitsHistoryDto> BenefitsHistoryDtoList = new List<BenefitsHistoryDto>();
            var BenefitHistories = await GetBenefitHistoriesAsync(BenefitId, PageNumber);
            await foreach (var Benefit in BenefitHistories)
            {
                BenefitsHistoryDto BenefitDto = _mapper.Map<BenefitsHistoryDto>(Benefit);
                BenefitsHistoryDtoList.Add(BenefitDto);
            }
            return BenefitsHistoryDtoList.ToAsyncEnumerable();
        }
        public async Task<IAsyncEnumerable<CustomersBenefitsHistoryDto>> GetCustomersBenefitHistory(int CustomerId, int PageNumber)
        {
            List<CustomersBenefitsHistoryDto> CustomersBenefitsHistoryDtoList = new List<CustomersBenefitsHistoryDto>();
            var BenefitHistories = await GetCustomersBenefitHistoryAsync(CustomerId, PageNumber);
            await foreach (var Benefit in BenefitHistories)
            {
                CustomersBenefitsHistoryDto CustomersBenefitsHistoryDto = _mapper.Map<CustomersBenefitsHistoryDto>(Benefit);
                CustomersBenefitsHistoryDtoList.Add(CustomersBenefitsHistoryDto);
            }
            return CustomersBenefitsHistoryDtoList.ToAsyncEnumerable();
        }
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
        private async Task<IAsyncEnumerable<CustomersBenefitsHistory>> GetCustomersBenefitHistoryAsync(int CustomerId, int PageNumber)
            => await Task.FromResult(_context.CustomersBenefitsHistories
            .Where(CustomersBenefitsHistory => CustomersBenefitsHistory.CustomerId == CustomerId
            && !CustomersBenefitsHistory.IsDeleted)
            .Skip(_paginationService.GetPageNumber(PageNumber))
            .Take(Constants.PageSize)
            .AsAsyncEnumerable());
        private async Task<IAsyncEnumerable<BenefitsHistory>> GetBenefitHistoriesAsync(int BenefitId, int PageNumber)
            => await Task.FromResult(_context.BenefitsHistories
            .Where(BenefitHistory => BenefitHistory.BenefitId == BenefitId && !BenefitHistory.IsDeleted)
            .OrderByDescending(BenefitHistory => BenefitHistory.ModifiedDate)
            .Skip(_paginationService.GetPageNumber(PageNumber))
            .Take(Constants.PageSize)
            .AsAsyncEnumerable());
        private async Task<BenefitsHistory> GetBenefitHistoriesById(int BenefitId)
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
    }
}
