using System.Threading.Tasks;
using Abp.Application.Services;
using esign.MultiTenancy.Payments.PayPal.Dto;

namespace esign.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
