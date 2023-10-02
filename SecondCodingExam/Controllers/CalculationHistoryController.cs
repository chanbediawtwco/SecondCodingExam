using Microsoft.AspNetCore.Mvc;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Controllers
{
    [Route("calculations/history")]
    public class CalculationHistoryController : Controller
    {
        private readonly ILogger<CalculationController> _logger;
        private readonly ICalculationHistoryService _calculatorHistoryService;
        public CalculationHistoryController(
            ILogger<CalculationController> logger,
            ICalculationHistoryService calculationHistoryService)
        {
            _logger = logger;
            _calculatorHistoryService = calculationHistoryService;
        }
        [Route("get/{customerid}/{pagenumber}")]
        public async Task<IActionResult> GetCalculationHistory(int CustomerId, int PageNumber)
        {
            try
            {
                return Ok(await _calculatorHistoryService.GetCalculationHistory(CustomerId, PageNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
    }
}
