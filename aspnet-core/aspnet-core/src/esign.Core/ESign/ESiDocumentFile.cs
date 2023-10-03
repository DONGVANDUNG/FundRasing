using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiDocumentFile")]
    public class ESiDocumentFile : FullAuditedEntity<long>, IEntity<long>
    {
        public long? DocumentId { get; set; }
        [StringLength(250)]
        public string OriginalFileName { get; set; }
        [StringLength(250)]
        public string OriginalServerFileName { get; set; }
        [StringLength(250)]
        public string ServerFileName { get; set; }
        [StringLength(250)]
        public string ServerFileNamePreSign { get; set; }
        [StringLength(10)]
        public string FileType { get; set; }
        public float? FileSize { get; set; }
        [StringLength(250)]
        public string DocumentPath { get; set; }
        [StringLength(10)]
        public string IsLock { get; set; }
        public long? RefId { get; set; }
        [StringLength(20)]
        public string SystemId { get; set; }
    }
}
