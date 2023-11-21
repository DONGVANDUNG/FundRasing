using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("FundImage")]
    public class FundImage : FullAuditedEntity<long>
    {
        public long FundId { get; set; }
        public string ImageUrl { get; set; }
    }
}
