using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("AuctionImages")]
    public class AuctionImages : FullAuditedEntity<long>
    {
        public long AuctionItemId { get; set; }
        public string ImageUrl { get; set; }
    }
}
