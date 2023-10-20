using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    [Table("UserAccount")]
    public class UserAccount : FullAuditedEntity<int>
    {
        public long UserId { get; set; }
        public string UserNameLogin { get; set; }
        public string Password { get; set; }
        public int? LevelWarning { get; set; }
        public bool? Status { get; set; }
        public int FundRaiserId { get; set; }
    }
}
