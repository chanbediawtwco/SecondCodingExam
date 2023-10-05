using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondCodingExam.Data;
using SecondCodingExam.Dto;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Services
{
    public class CalculationHistoryService : ICalculationHistoryService
    {
        private readonly IMapper _mapper;
        private readonly SecondCodingExamDbContext _context;
        private readonly IPaginationService _paginationService;
        public CalculationHistoryService(
            IMapper mapper,
            SecondCodingExamDbContext context, 
            IPaginationService paginationService)
        {
            _mapper = mapper;
            _context = context;
            _paginationService = paginationService;
        }
        public async Task<IAsyncEnumerable<CalculationsHistoryDto>> GetCalculationHistory(int CustomerId, int PageNumber)
        {
            List<CalculationsHistoryDto> CalculationsHistoryDtoList = new List<CalculationsHistoryDto>();
            var CalculationsHistories = await GetCalculationHistoryAsync(CustomerId, PageNumber);
            await foreach (var CalculationsHistory in CalculationsHistories)
            {
                CalculationsHistoryDto CalculationsHistoryDto = _mapper.Map<CalculationsHistoryDto>(CalculationsHistory);
                CalculationsHistoryDtoList.Add(CalculationsHistoryDto);
            }
            return CalculationsHistoryDtoList.ToAsyncEnumerable();
        }
        public async Task<IAsyncEnumerable<CalculationsHistory>> GetCalculationHistoryAsync(int CustomerId, int PageNumber)
            => await Task.FromResult(_context.CalculationsHistories
                .Where(Calculation => Calculation.CustomerId == CustomerId)
                .OrderByDescending(Calculation => Calculation.ModifiedDate)
                .Skip(_paginationService.GetPageNumber(PageNumber))
                .Take(Constants.PageSize)
                .AsAsyncEnumerable());
    }
}
