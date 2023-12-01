using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("InformationReceiveAuction")]
    public class InformationReceiveAuction :FullAuditedEntity<long>
    {
        public string ReceiveName { get; set; }
        public string Address { get; set; }
        public int? PaymentType { get; set; }
        public string Note { get; set; }
        public long? AuctionItemId { get; set; }
    }
}
