using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Dto;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;
using SecondCodingExam.Validators;
using System.Linq;

namespace SecondCodingExam.Services
{
    public class BenefitService : IBenefitService
    {
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IAuditService _auditService;
        private readonly IAccountService _accountService;
        private readonly SecondCodingExamDbContext _context;
        private readonly IPaginationService _paginationService;
        private readonly IValidator<BenefitDto> _benefitValidator;
        public BenefitService(
            IMapper mapper,
            IJwtService jwtService,
            IAuditService auditService,
            IAccountService accountService,
            SecondCodingExamDbContext context,
            IPaginationService paginationService,
            IValidator<BenefitDto> benefitValidator)
        {
            _mapper = mapper;
            _context = context;
            _jwtService = jwtService;
            _auditService = auditService;
            _accountService = accountService;
            _benefitValidator = benefitValidator;
            _paginationService = paginationService;
        }
        public async Task<Benefit> GetBenefitById(int BenefitId)
        {
            Benefit? Benefit = await _context.Benefits
            .Where(Benefit => Benefit.Id == BenefitId && !Benefit.IsDeleted)
            .FirstOrDefaultAsync();
            if (Benefit == null) throw new Exception(Constants.NoBenefitFound);
            return Benefit;
        }
        public async Task<CustomersCurrentBenefit?> GetCustomerCurrentBenefit(int CustomerId)
            => await _context.CustomersCurrentBenefits
            .Where(CustomersCurrentBenefit => CustomersCurrentBenefit.CustomerId == CustomerId
            && !CustomersCurrentBenefit.IsDeleted)
            .FirstOrDefaultAsync();
        public async Task<IAsyncEnumerable<Benefit>> GetBenefits(int PageNumber)
        {
            int UserId = await _jwtService.GetUserIdFromToken();
            return await Task.FromResult(_context.Benefits
                .Where(Benefit => Benefit.UserId == UserId && !Benefit.IsDeleted)
                .Skip(_paginationService.GetPageNumber(PageNumber))
                .Take(Constants.PageSize)
                .AsAsyncEnumerable());
        }
        private async Task<bool> IsValidBenefitInformation(BenefitDto Benefit)
        {
            ValidationResult BenefitValidator = await _benefitValidator.ValidateAsync(Benefit);
            return BenefitValidator.IsValid;
        }
        private async Task<bool> HasBenefitChanges(BenefitDto NewBenefit, Benefit DbBenefit)
            => await Task.FromResult(NewBenefit.GuaranteedIssue != DbBenefit.GuaranteedIssue
                || NewBenefit.MaxAgeLimit != DbBenefit.MaxAgeLimit
                || NewBenefit.MinAgeLimit != DbBenefit.MinAgeLimit
                || NewBenefit.MaxRange != DbBenefit.MaxRange
                || NewBenefit.MinRange != DbBenefit.MinRange
                || NewBenefit.Increments != DbBenefit.Increments);
        public async Task DeleteBenefit(int BenefitId)
        {
            User User = await _accountService.GetUserById(await _jwtService.GetUserIdFromToken());
            Benefit DbBenefit = await GetBenefitById(BenefitId);
            DbBenefit.IsDeleted = true;
            await _auditService.AddAuditStamp(DbBenefit, await _accountService.GetUserFullname(User), DateTime.Now, true);
            await _context.SaveChangesAsync();
        }
        public async Task SaveBenefit(BenefitDto NewBenefit)
        {
            if(!await IsValidBenefitInformation(NewBenefit)) throw new Exception(Constants.InvalidInput);
            User User = await _accountService.GetUserById(await _jwtService.GetUserIdFromToken());
            Benefit Benefit = _mapper.Map<Benefit>(NewBenefit);
            Benefit.UserId = User.Id;
            await _auditService.AddAuditStamp(Benefit, await _accountService.GetUserFullname(User), DateTime.Now, false);
            _context.Benefits.Add(Benefit);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.DetectChanges();
        }
        public async Task SaveCurrentBenefitChanges(CustomersCurrentBenefit CustomersCurrentBenefit, DateTime Timestamp, int? CustomerId = null)
        {
            // If the process is Saving
            // There will be a new Customer Id
            bool IsUpdating = CustomerId == null;
            string UserFullname = await _accountService.GetUserFullname(
                                    await _accountService.GetUserById(
                                        await _jwtService.GetUserIdFromToken()));
            await _auditService.AddAuditStamp(CustomersCurrentBenefit, UserFullname, Timestamp, IsUpdating);
            if (!IsUpdating)
            {
                CustomersCurrentBenefit.CustomerId = Convert.ToInt32(CustomerId);
                _context.CustomersCurrentBenefits.Add(CustomersCurrentBenefit);
            }
            await _context.SaveChangesAsync();
            _context.ChangeTracker.DetectChanges();
        }
        public async Task UpdateBenefit(BenefitDto UpdatedBenefit)
        {
            if(!await IsValidBenefitInformation(UpdatedBenefit)) throw new Exception(Constants.InvalidInput);
            User User = await _accountService.GetUserById(await _jwtService.GetUserIdFromToken());
            Benefit DbBenefit = await GetBenefitById(Convert.ToInt32(UpdatedBenefit.Id));
            if(!await HasBenefitChanges(UpdatedBenefit, DbBenefit)) throw new Exception(Constants.NoChangesFound);
            BenefitsHistory BenefitsHistory = _mapper.Map<BenefitsHistory>(DbBenefit);
            DateTime Timestamp = DateTime.Now;
            await _auditService.AddAuditStamp(BenefitsHistory, await _accountService.GetUserFullname(User), Timestamp, true);
            _context.BenefitsHistories.Add(BenefitsHistory);
            DbBenefit = _mapper.Map<BenefitDto, Benefit>(UpdatedBenefit, DbBenefit);
            DbBenefit.IsUpdated = true;
            await _auditService.AddAuditStamp(DbBenefit, await _accountService.GetUserFullname(User), Timestamp, true);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.DetectChanges();
        }
    }
}
