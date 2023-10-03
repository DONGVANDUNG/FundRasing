using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace esign.ESign
{
    [Table("ESiDocumentFileLog")]
    public class ESiDocumentFileLog : FullAuditedEntity<long>, IEntity<long>
    {
        public long? DocumentFileId { get; set; }
        public long? DocumentUserLogId { get; set; }
        public long? ApprovedByUserId { get; set; }
        public long? ApprovedByName { get; set; }
        public string ApprovedByUserName { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}
