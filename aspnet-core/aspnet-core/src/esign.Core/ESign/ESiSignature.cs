using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiSignature")]
    public class ESiSignature : FullAuditedEntity<long>, IEntity<long>
    {
        [StringLength(250)]
        public string SignaturePath { get; set; }
        public long? UserId { get; set; }
    }
}
