using System.Collections.Generic;
using esign.Editions.Dto;
using esign.MultiTenancy.Payments;

namespace esign.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}