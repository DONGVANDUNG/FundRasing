using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Entity
{
    [Table("Funds")]
    public class Funds : FullAuditedEntity<int>
    {
        public int Id { get; set; }
        public int FundRaiserId { get; set; }
        public DateTime  FundRaisingDay { get; set; }
        public string FundName { get; set; }
        public string FundImageUrl { get; set; }
        public string FundTitle { get; set; }
        public string FundReason { get; set; }
        public string AmountOfMoney { get; set; }
        public DateTime FundEndDate { get; set; }
        public bool Status { get; set; }
        public bool IsOutStanding { get; set; }
    }
}
