using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("AuctionItems")]
    public class AuctionItems :FullAuditedEntity<long>
    {
        public string ItemName { get; set; }
        public long? AuctionId { get; set; }
        public string IntroduceItem { get; set; }
        public float? AmountJumpMin { get; set; }
        public float? AmountJumpMax { get; set; }
        public float? StartingPrice { get; set; }
        public float? AuctionPresentAmount { get; set; }
        public float? TargetAmountOfMoney { get; set; }
    }
}
