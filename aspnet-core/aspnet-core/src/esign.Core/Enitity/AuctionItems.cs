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
        public string TitleAuction { get; set; }
        public string IntroduceItem { get; set; }
        public float? AmountJumpMin { get; set; }
        public float? AmountJumpMax { get; set; }
        public float? StartingPrice { get; set; }
        public float? AuctionPresentAmount { get; set; }
        public float? TargetAmountOfMoney { get; set; }
        public int? NumberOfParticipants { get; set; }
        public long? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsClose { get; set; }
        public int? Amount { get; set; }
        public bool? IsPublic { get; set; }
    }
}
