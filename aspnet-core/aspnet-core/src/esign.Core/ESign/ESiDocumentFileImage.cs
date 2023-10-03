using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiDocumentFileImage")]
    public class ESiDocumentFileImage : FullAuditedEntity<long>, IEntity<long>
    {
        public long? DocumentFileId { get; set; }
        [StringLength(250)]
        public string ServerFileName { get; set; }
        public long? FileSeq { get; set; }
        public long? ImageW { get; set; }
        public long? ImageH { get; set; }
        public long? DocumentId { get; set; }
        public long? RefId { get; set; }
        [StringLength(20)]
        public string SystemId { get; set; }
    }
}
