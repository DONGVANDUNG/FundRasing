using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiDocumentUser")]
    public class ESiDocumentUser : FullAuditedEntity<long>, IEntity<long>
    {
        public long? ApprovedByUserId { get; set; }
        [StringLength(50)]
        public string ApprovedByUserName { get; set; }
        public string Note { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public long? DocumentId { get; set; }
        [StringLength(10)]
        public string Status { get; set; }
        public long? Seq { get; set; }
        [StringLength(50)]
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime? DeadLineDate { get; set; }
        [StringLength(50)]
        public string SystemId { get; set; }
        public long? RefId { get; set; }
        public string SignatureString { get; set; }
        [StringLength(10)]
        public string RefType { get; set; }
        public long? ForwardByUserId { get; set; }
        [StringLength(50)]
        public string ForwardByUserName { get; set; }
        public DateTime? ForwardDate { get; set; }
        [StringLength(1)]
        public string IsDigitalSign { get; set; }
    }
}
