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
        public long PostId { get; set; }
        public string ImageUrl { get; set; }
        public int? Size { get; set; }
    }
}
