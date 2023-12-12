using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("UserFundPackage")]
    public class UserFundPackage : FullAuditedEntity<long>
    {
        public long? UserId { get; set; }
        public long? FundPackageId { get; set; }
        public bool? IsExpired { get; set; }
        public DateTime? FundEndDate { get; set; }
    }
}
