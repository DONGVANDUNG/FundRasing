using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using esign.MultiTenancy.HostDashboard.Dto;

namespace esign.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}