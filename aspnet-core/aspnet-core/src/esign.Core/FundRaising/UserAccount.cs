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
    public class UserAccount : FullAuditedEntity<long>, IEntity<long>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserLogin { get; set; }
        public string Password { get; set; }
        public int? LevelWarning { get; set; }
        public bool? Status { get; set; }
    }
}
