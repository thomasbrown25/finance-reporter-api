using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finance_reporter_api.Dtos.CreditReport;
using finance_reporter_api.Utils;

namespace finance_reporter_api.Services.CreditReportService
{
    public class CreditReportService : ICreditReportService
    {
        private readonly IConfiguration _configuration;
        private readonly LoggerService.ILogger _logger;

        public CreditReportService(IConfiguration configuration, LoggerService.ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ServiceResponse<GetCreditReportDto>> GetCreditReports()
        {
            var response = new ServiceResponse<GetCreditReportDto> { Data = new GetCreditReportDto() };

            try
            {
                HttpClient client = new();
                var clientResponse = await client.GetAsync("https://api.equifax.com/personal/consumer-data-suite/v1/creditReport");

                response.Data.StringData = await clientResponse.Content.ReadAsStringAsync();
                response.Message = ResponseMessage.CreditReportGetSuccess;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                response.Success = false;
                response.Message = $"{ResponseMessage.CreditReportGetFailed}: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<GetCreditReportDto>> GetLatest()
        {
            var response = new ServiceResponse<GetCreditReportDto> { Data = new GetCreditReportDto() };

            try
            {
                HttpClient client = new();
                var clientResponse = await client.GetAsync("https://api.equifax.com/personal/consumer-data-suite/v1/creditReport/latest");

                response.Data.StringData = await clientResponse.Content.ReadAsStringAsync();
                response.Message = ResponseMessage.CreditReportGetSuccess;

            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                response.Success = false;
                response.Message = $"{ResponseMessage.CreditReportGetFailed}: {ex.Message}";
            }

            return response;
        }

    }
}