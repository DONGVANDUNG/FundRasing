using esign.Editions;
using esign.Editions.Dto;
using esign.MultiTenancy.Payments;
using esign.Security;
using esign.MultiTenancy.Payments.Dto;

namespace esign.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
