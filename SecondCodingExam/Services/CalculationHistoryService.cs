using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Services
{
    public class CalculationHistoryService : ICalculationHistoryService
    {
        private readonly SecondCodingExamDbContext _context;
        private readonly IPaginationService _paginationService;
        public CalculationHistoryService(
            SecondCodingExamDbContext context, 
            IPaginationService paginationService)
        {
            _context = context;
            _paginationService = paginationService;
        }
        public async Task<IAsyncEnumerable<CalculationsHistory>> GetCalculationHistory(int CustomerId, int PageNumber)
            => await Task.FromResult(_context.CalculationsHistories
                .Where(Calculation => Calculation.CustomerId == CustomerId)
                        .Skip(_paginationService.GetPageNumber(PageNumber))
                        .Take(Constants.PageSize)
            .AsAsyncEnumerable());
    }
}
