using Microsoft.AspNetCore.Mvc;
using SecondCodingExam.Services;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Controllers
{
    [Route("customers/history")]
    public class CustomerHistoryController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerHistoryService _customerHistoryService;
        public CustomerHistoryController(ILogger<CustomersController> logger, 
            ICustomerHistoryService customerHistoryService)
        {
            _logger = logger;
            _customerHistoryService = customerHistoryService;
        }
        [HttpGet]
        [Route("get/{pagenumber}/{customerid}")]
        public async Task<IActionResult> GetCustomerHistory(int PageNumber, int CustomerId)
        {
            try
            {
                return Ok(await _customerHistoryService.GetCustomerHistory(PageNumber, CustomerId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpDelete]
        [Route("delete/{customerhistoryid}")]
        public async Task<IActionResult> DeleteCustomerHistory(int CustomerHistoryId)
        {
            try
            {
                await _customerHistoryService.DeleteCustomerHistory(CustomerHistoryId);
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
