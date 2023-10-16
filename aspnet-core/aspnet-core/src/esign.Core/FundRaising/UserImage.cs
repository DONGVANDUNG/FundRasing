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
    [Table("UserImage")]
    public class UserImage : FullAuditedEntity<long>, IEntity<long>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ImageUrl { get; set; }
    }
}
