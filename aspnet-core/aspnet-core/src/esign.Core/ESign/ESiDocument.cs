using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiDocument")]
    public class ESiDocument : FullAuditedEntity<long>, IEntity<long>
    {
        [StringLength(250)]
        public string DocumentTitle { get; set; }
        [StringLength(10)]
        public string DocumentPath { get; set; }
        public string Description { get; set; }
        public long? OwnerId { get; set; }
        [StringLength(10)]
        public string Status { get; set; }
        [StringLength(50)]
        public string SystemId { get; set; }
        public long? RefId { get; set; }
        [StringLength(10)]
        public string RefType { get; set; }
    }
}
