using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiDocumentFileImageSign")]
    public class ESiDocumentFileImageSign : FullAuditedEntity<long>, IEntity<long>
    {
        public long? DocumentFileImageId { get; set; }
        public long? DocumentUserId { get; set; }
        [StringLength(50)]
        public string EmailSigner { get; set; }
        public long? RefId { get; set; }
        [StringLength(20)]
        public string SystemId { get; set; }
        public long? PositionX { get; set; }
        public long? PositionY { get; set; }
        public long? PositionW { get; set; }
        public long? PositionH { get; set; }
    }
}
