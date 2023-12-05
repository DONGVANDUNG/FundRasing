using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("RequestToFundRaiser")]
    public class RequestToFundRaiser :FullAuditedEntity<long>
    {
        public long? UserId { get; set; }
        public DateTime? RequestTime { get; set; }
        public long? FundPackageId { get; set; }
        public DateTime? ApproveDate { get; set; }
        public bool? IsApprove { get; set; }
    }
}
