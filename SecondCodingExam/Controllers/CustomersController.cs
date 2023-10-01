using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondCodingExam.Dto;
using SecondCodingExam.Models;
using SecondCodingExam.Services;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Controllers
{
    [Route("customers")]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly ICustomerService _customerService;
        public CustomersController(ILogger<CustomersController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }
        [HttpGet]
        [Route("get/{pagenumber}")]
        public async Task<IActionResult> GetAllCustomers(int PageNumber)
        {
            try
            {
                return Ok(await _customerService.GetAllCustomers(PageNumber));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpGet]
        [Route("history/get/{pagenumber}/{customerid}")]
        public async Task<IActionResult> GetCustomerHistory(int PageNumber, int CustomerId)
        {
            try
            {
                return Ok(await _customerService.GetCustomerHistory(PageNumber, CustomerId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCustomer([FromBody] CustomerDto NewCustomer)
        {
            try
            {
                await _customerService.AddNewCustomer(NewCustomer);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDto NewCustomerInformation)
        {
            try
            {
                await _customerService.UpdateCustomer(NewCustomerInformation);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpDelete]
        [Route("delete/{customerid}")]
        public async Task<IActionResult> DeleteCustomer(int CustomerId)
        {
            try
            {
                await _customerService.DeleteCustomer(CustomerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpDelete]
        [Route("history/delete/{customerhistoryid}")]
        public async Task<IActionResult> DeleteCustomerHistory(int CustomerHistoryId)
        {
            try
            {
                await _customerService.DeleteCustomerHistory(CustomerHistoryId);
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
