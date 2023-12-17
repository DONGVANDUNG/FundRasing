using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("AuctionTransactionDeposit")]
    public class AuctionTransactionDeposit : FullAuditedEntity<long>
    {
        public long? AuctionItemId { get; set; }
        public string MessageContent { get; set; }
        public long? SenderId { get; set; }
        public long? ReceiverId { get; set; }
        public float? AmountOfMoney { get; set; }
    }
}
