using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    [Table("FundDetails")]

    public class FundDetails : FullAuditedEntity<long>, IEntity<long>
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public string TitlePart { get; set; }
        public string ContentPart { get; set; }
    }
}
