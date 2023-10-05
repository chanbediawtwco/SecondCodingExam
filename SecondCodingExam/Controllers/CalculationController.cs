using Microsoft.AspNetCore.Mvc;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Controllers
{
    [Route("calculations")]
    public class CalculationController : Controller
    {
        private readonly ILogger<CalculationController> _logger;
        private readonly ICalculationService _calculatorService;
        public CalculationController(
            ILogger<CalculationController> logger,
            ICalculationService calculationService)
        {
            _logger = logger;
            _calculatorService = calculationService;
        }
        [Route("get/{customerid}")]
        public async Task<IActionResult> GetCurrentCalculation(int CustomerId)
        {
            try
            {
                return Ok(await _calculatorService.GetCalculationsList(CustomerId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
    }
}
