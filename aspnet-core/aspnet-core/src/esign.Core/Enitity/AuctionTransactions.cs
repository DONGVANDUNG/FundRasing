using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("AuctionTransactions")]
    public class AuctionTransactions : FullAuditedEntity<long>
    {
        public long? AuctionItemId { get; set; }
        public long? AuctioneerId{ get; set; }
        public float? OldAmount { get; set; }
        public float? NewAmount { get; set; }
        public DateTime? AuctionDate { get; set; }
        public bool? IsPublic { get; set; }
    }
}
