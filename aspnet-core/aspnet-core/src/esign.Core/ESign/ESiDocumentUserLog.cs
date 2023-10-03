using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiDocumentUserLog")]
    public class ESiDocumentUserLog: FullAuditedEntity<long>, IEntity<long>
    {
        public string Note { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public long? DocumentId { get; set; }
        [StringLength(10)]
        public string Status { get; set; }
        public DateTime? ApprovedDate { get; set; }
        [StringLength(50)]
        public string CreatedByName { get; set; }
        [StringLength(50)]
        public string ApprovedByUserName { get; set; }
        public long? ApprovedByUserId { get; set; }
        [StringLength(50)]
        public string ApprovedByName { get; set; }
        [StringLength(500)]
        public string RejectNote { get; set; }
        public long? DocumentUserId { get; set; }
    }
}
