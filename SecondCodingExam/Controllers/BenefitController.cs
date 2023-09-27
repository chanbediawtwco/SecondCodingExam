using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondCodingExam.Dto;
using SecondCodingExam.Services.Interface;

namespace SecondCodingExam.Controllers
{
    [Route("benefit")]
    public class BenefitController : Controller
    {
        private readonly IBenefitService _benefitService;
        private readonly ILogger<BenefitController> _logger;
        public BenefitController(IBenefitService benefitService, 
            ILogger<BenefitController> logger)
        {
            _benefitService = benefitService;
            _logger = logger;

        }
        [HttpGet]
        [Route("get/{pagenumber}")]
        public async Task<IActionResult> GetBenefits(int PageNumber)
        {
            try
            {
                return Ok(await _benefitService.GetBenefits(PageNumber));
            }
            catch(Exception ex)
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
