using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finance_reporter_api.Dtos.CreditReport;
using finance_reporter_api.Services.CreditReportService;
using finance_reporter_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace finance_reporter_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditReportController : ControllerBase
    {
        private readonly ICreditReportService _creditReportService;

        public CreditReportController(ICreditReportService creditReportService)
        {
            _creditReportService = creditReportService;
        }

        [Authorize]
        [HttpGet("")]
        public async Task<ActionResult<ServiceResponse<GetCreditReportDto>>> GetCreditReports()
        {
            var response = await _creditReportService.GetCreditReports();

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpGet("latest")]
        public async Task<ActionResult<ServiceResponse<GetCreditReportDto>>> GetLatest()
        {
            var response = await _creditReportService.GetLatest();

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}