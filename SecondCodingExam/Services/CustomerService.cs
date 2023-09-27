﻿using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Dto;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;
using System;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SecondCodingExam.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IAuditService _auditService;
        private readonly IAccountService _accountService;
        private readonly IBenefitService _benefitService;
        private readonly SecondCodingExamDbContext _context;
        private readonly IPaginationService _paginationService;
        private readonly ICalculationService _calculationService;
        private readonly IValidator<CustomerDto> _customerValidator;
        public CustomerService(
            IMapper mapper,
            IJwtService jwtService,
            IAuditService auditService,
            IBenefitService benefitService,
            IAccountService accountService,
            SecondCodingExamDbContext context,
            IPaginationService paginationService,
            ICalculationService calculationService,
            IValidator<CustomerDto> customerValidator)
        {
            _mapper = mapper;
            _context = context;
            _jwtService = jwtService;
            _auditService = auditService;
            _benefitService = benefitService;
            _accountService = accountService;
            _paginationService = paginationService;
            _customerValidator = customerValidator;
            _calculationService = calculationService;
        }
        public async Task AddNewCustomer(CustomerDto NewCustomer)
        {
            if (!await IsValidCustomerInformation(NewCustomer)) throw new Exception(Constants.InvalidInput);
            User User = await _accountService.GetUserById(await _jwtService.GetUserIdFromToken());
            Customer Customer = await Task.FromResult(_mapper.Map<Customer>(NewCustomer));
            Customer.UserId = User.Id;
            DateTime Timestamp = DateTime.Now;
            await _context.Customers.AddAsync(Customer);
            await AddAuditStampToCustomer(Customer, await _accountService.GetUserFullname(User), Timestamp, false);
            await SaveCurrentBenefit(await _benefitService.GetBenefitById(Convert.ToInt32(NewCustomer.BenefitId)), Timestamp, Customer.Id);
            CustomersCurrentBenefit CustomersCurrentBenefit = await _benefitService.GetCustomerCurrentBenefit(Customer.Id);
            await _context.SaveChangesAsync();
            await _calculationService.CalculateBenefits(CustomersCurrentBenefit, Customer);
        }
        public async Task UpdateCustomer(CustomerDto NewCustomerInformation)
        {
            if (!await IsValidCustomerInformation(NewCustomerInformation)) throw new Exception(Constants.InvalidInput);
            User User = await _accountService.GetUserById(await _jwtService.GetUserIdFromToken());
            Customer DbCustomer = await GetCustomerById(Convert.ToInt32(NewCustomerInformation.Id), User.Id);
            if (!await HasCustomerChanges(NewCustomerInformation, DbCustomer)) throw new Exception(Constants.NoChangesFound);
            CustomersCurrentBenefit CustomersCurrentBenefit = await _benefitService.GetCustomerCurrentBenefit(DbCustomer.Id);
            DateTime Timestamp = DateTime.Now;
            await MapCustomerHistoryData(DbCustomer, CustomersCurrentBenefit, Timestamp);
            DbCustomer = _mapper.Map<CustomerDto, Customer>(NewCustomerInformation, DbCustomer);
            await AddAuditStampToCustomer(DbCustomer, await _accountService.GetUserFullname(User), Timestamp, true, CustomersCurrentBenefit.BenefitId);
            await UpdateCurrentBenefit(CustomersCurrentBenefit, await _benefitService.GetBenefitById(Convert.ToInt32(NewCustomerInformation.BenefitId)), Timestamp);
            await _calculationService.CalculateBenefits(CustomersCurrentBenefit, DbCustomer);
        }
        public async Task DeleteCustomer(int CustomerId)
        {
            Customer Customer = await GetCustomerById(CustomerId, await _jwtService.GetUserIdFromToken());
            Customer.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        public async Task<IAsyncEnumerable<Customer>> GetAllCustomers(int PageNumber)
        {
            int UserId = await _jwtService.GetUserIdFromToken();
            return await Task.FromResult(_context.Customers
                        .Where(Customer => Customer.UserId == UserId && !Customer.IsDeleted)
                        .Skip(_paginationService.GetPageNumber(PageNumber))
                        .Take(Constants.PageSize)
                        .AsAsyncEnumerable());
        }
        private async Task<Customer> GetCustomerById(int CustomerId, int UserId)
        {
            Customer? Customer = await _context.Customers
            .Where(Customer => Customer.Id == CustomerId && Customer.UserId == UserId && !Customer.IsDeleted)
            .FirstOrDefaultAsync();
            if (Customer == null) throw new Exception(Constants.CustomerNotFound);
            return Customer;
        }
        private async Task<bool> IsValidCustomerInformation(CustomerDto CustomerInformation)
        {
            ValidationResult CustomerValidator = await _customerValidator.ValidateAsync(CustomerInformation);
            return CustomerValidator.IsValid;
        }
        private async Task<bool> HasCustomerChanges(CustomerDto NewCustomerInformation, Customer DbCustomer)
            => await Task.FromResult(NewCustomerInformation.Firstname != DbCustomer.Firstname
                || NewCustomerInformation.Lastname != DbCustomer.Lastname
                || NewCustomerInformation.BasicSalary != DbCustomer.BasicSalary
                || NewCustomerInformation.Birthdate != DbCustomer.Birthdate);
        private async Task SaveCurrentBenefit(Benefit Benefit, DateTime Timestamp, int CustomerId)
        {
            CustomersCurrentBenefit CurrentBenefit = _mapper.Map<CustomersCurrentBenefit>(Benefit);
            await _benefitService.SaveCurrentBenefitChanges(CurrentBenefit, Timestamp, CustomerId);
        }
        private async Task UpdateCurrentBenefit(CustomersCurrentBenefit CustomersCurrentBenefit, Benefit Benefit, DateTime Timestamp)
        {
            CustomersCurrentBenefit CurrentBenefit = _mapper.Map<Benefit, CustomersCurrentBenefit>(Benefit, CustomersCurrentBenefit);
            await _benefitService.SaveCurrentBenefitChanges(CurrentBenefit, Timestamp);
        }
        private async Task AddAuditStampToCustomer(Customer Customer, string UserFullname, DateTime Timestamp, bool IsModified, int? BenefitId = null)
        {
            await _auditService.AddAuditStamp(Customer, UserFullname, Timestamp, IsModified);
            if (BenefitId != null) await _auditService.AddAuditStampToCalculation(await _calculationService.GetCalculations(Convert.ToInt32(BenefitId), Customer.Id), Timestamp);
            await _context.SaveChangesAsync();
        }
        private async Task MapCustomerHistoryData(Customer Customer, CustomersCurrentBenefit CurrentBenefit, DateTime Timestamp)
        {
            User User = await _accountService.GetUserById(Customer.UserId);
            string UserFullname = await _accountService.GetUserFullname(User);
            await _benefitService.MapBenefitToBenefitHistory(CurrentBenefit, UserFullname, Timestamp);
            await _calculationService.MapPreviousCalculationsToHistory(Customer.Id, Timestamp);
            await MapCustomerToHistory(Customer, CurrentBenefit, UserFullname, Timestamp);
        }
        private async Task MapCustomerToHistory(Customer Customer, CustomersCurrentBenefit CurrentBenefit, string UserFullname, DateTime Timestamp)
        {
            CustomersHistory CustomerHistory = await Task.FromResult(_mapper.Map<CustomersHistory>(Customer));
            CustomerHistory.CustomersBenefitsHistoryId = await _benefitService.GetCustomersBenefitsHistoryId(CurrentBenefit.Id);
            await _auditService.AddAuditStamp(CustomerHistory, UserFullname, Timestamp, true);
            _context.CustomersHistories.Add(CustomerHistory);
        }
    }
}