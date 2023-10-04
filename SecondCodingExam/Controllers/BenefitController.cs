using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondCodingExam.Dto;
using SecondCodingExam.Services;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Controllers
{
    [Route("benefit")]
    public class BenefitController : Controller
    {
        private readonly IBenefitService _benefitService;
        private readonly ILogger<BenefitController> _logger;
        public BenefitController(
            ILogger<BenefitController> logger,
            IBenefitService benefitService)
        {
            _logger = logger;
            _benefitService = benefitService;
        }
        [HttpGet]
        [Route("get/{pagenumber}")]
        public async Task<IActionResult> GetBenefits(int PageNumber)
        {
            try
            {
                return Ok(await _benefitService.GetBenefitsAsync(PageNumber));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpGet]
        [Route("get/all")]
        public async Task<IActionResult> GetAllBenefits()
        {
            try
            {
                return Ok(await _benefitService.GetBenefitsAsync(null));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpGet]
        [Route("current/customer/get/{customerid}")]
        public async Task<IActionResult> GetCurrentCustomerBenefit(int CustomerId)
        {
            try
            {
                return Ok(await _benefitService.GetCustomerCurrentBenefitByDto(CustomerId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddBenefit([FromBody]BenefitDto NewBenefit)
        {
            try
            {
                await _benefitService.SaveBenefit(NewBenefit);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateBenefit([FromBody]BenefitDto UpdatedBenefit)
        {
            try
            {
                await _benefitService.UpdateBenefit(UpdatedBenefit);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
        [HttpDelete]
        [Route("delete/{benefitid}")]
        public async Task<IActionResult> DeleteBenefit(int BenefitId)
        {
            try
            {
                await _benefitService.DeleteBenefit(BenefitId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem();
            }
        }
    }
}
