using Microsoft.AspNetCore.Mvc;
using SecondCodingExam.Services;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Controllers
{
    [Route("benefit/history")]
    public class BenefitHistoryController : Controller
    {
        private readonly ILogger<BenefitController> _logger;
        private readonly IBenefitHistoryService _benefitHistoryService;
        public BenefitHistoryController(
            ILogger<BenefitController> logger,
            IBenefitHistoryService benefitHistoryService)
        {
            _logger = logger;
            _benefitHistoryService = benefitHistoryService;
        }
        [HttpGet]
        [Route("get/{benefitid}/{pagenumber}")]
        public async Task<IActionResult> GetBenefitHistory(int BenefitId, int PageNumber)
        {
            try
            {
                return Ok(await _benefitHistoryService.GetBenefitHistories(BenefitId, PageNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpGet]
        [Route("customer/get/{customerid}/{pagenumber}")]
        public async Task<IActionResult> GetCustomersBenefitHistory(int CustomerId, int PageNumber)
        {
            try
            {
                return Ok(await _benefitHistoryService.GetCustomersBenefitHistory(CustomerId, PageNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpDelete]
        [Route("delete/{benefitid}")]
        public async Task<IActionResult> DeleteBenefitHistory(int BenefitId)
        {
            try
            {
                await _benefitHistoryService.DeleteBenefitHistory(BenefitId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpDelete]
        [Route("customer/delete/{customerbenefithistoryid}")]
        public async Task<IActionResult> DeleteCustomerBenefitHistory(int CustomerBenefitHistoryId)
        {
            try
            {
                await _benefitHistoryService.DeleteCustomerBenefitHistory(CustomerBenefitHistoryId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
    }
}