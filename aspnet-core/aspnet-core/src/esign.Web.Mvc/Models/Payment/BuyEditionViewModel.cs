using System.Collections.Generic;
using esign.Editions;
using esign.Editions.Dto;
using esign.MultiTenancy.Payments;
using esign.MultiTenancy.Payments.Dto;

namespace esign.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
