using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("AuctionDeposit")]
    public class AuctionDeposit :FullAuditedEntity<long>
    {
        public long? UserId { get; set; }
        public long? AuctionItemId { get; set; }
        public float? DepositAmount { get; set; }
        public DateTime? DepositDate { get; set; }
        public bool? IsPayDeposit { get; set; }
    }
}
