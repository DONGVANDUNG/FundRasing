using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.ESign
{
    [Table("ESiDocumentForward")]
    public class ESiDocumentForward : FullAuditedEntity<long>, IEntity<long>
    {
        [StringLength(50)]
        public string ForwardUsername { get; set; }
        public long? ForwardUserId { get; set; }
        [StringLength(50)]
        public string ForwardName { get; set; }
        public long? DocumentId { get; set; }
    }
}
