using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("Auction")]
    public class Auction : FullAuditedEntity<long>
    {
        public string ImageUrlItem { get; set; }
        public string ItemName { get; set; }
        public string IntroduceItem { get; set; }
        public int AmountJump { get; set; }
        public int AmountMin { get; set; }
        public int AmountMax { get; set; }
        public int StartingPrice { get; set;}
        public long UserId { get; set; }
    }
}
