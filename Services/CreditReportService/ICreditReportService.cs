using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finance_reporter_api.Dtos.CreditReport;
using finance_reporter_api.Utils;

namespace finance_reporter_api.Services.CreditReportService
{
    public interface ICreditReportService
    {
        Task<ServiceResponse<GetCreditReportDto>> GetCreditReports();
        Task<ServiceResponse<GetCreditReportDto>> GetLatest();
    }
}