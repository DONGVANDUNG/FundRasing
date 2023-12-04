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
        public string TitleAuction { get; set; }
        //public string ItemName { get; set; }
        public string IntroduceItem { get; set; }
        //public int? AmountJumpMin { get; set; }
        //public int? AmountJumpMax { get; set; }
        //public int? StartingPrice { get; set;}
        public long? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public float? AuctionPresentAmount { get; set; }
        public bool? IsPublic { get; set; }
        public int? NumberOfParticipants { get; set; }
        public float? AmountTargetOfMoney { get; set; }
        public int? LimitedOfNumber { get; set; }
    }
}
