using Abp.Application.Services;
using esign.Dto;
using esign.Logging.Dto;

namespace esign.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
