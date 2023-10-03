using System.Threading.Tasks;
using Abp.Application.Services;

namespace esign.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
