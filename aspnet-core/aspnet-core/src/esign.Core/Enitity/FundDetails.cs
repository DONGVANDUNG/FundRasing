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
                            
    public class FundDetails : FullAuditedEntity<long>
    {
        public long? FundId { get; set; }
        public string Purpose { get; set; }
        public string Target { get; set; }
        public string OrganizationName { get; set; }
        public string Note { get; set; }
    }
}
