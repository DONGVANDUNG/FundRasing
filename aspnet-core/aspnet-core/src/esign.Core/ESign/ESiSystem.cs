using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiSystem")]
    public class ESiSystem : FullAuditedEntity<long>, IEntity<long>
    {
        [StringLength(50)]
        public string SystemName { get; set; }
        [StringLength(500)]
        public string ResultUrl { get; set; }
        [StringLength(500)]
        public string ForwardUrl { get; set; }
        [StringLength(500)]
        public string RequestInfoUrl { get; set; }
    }
}
