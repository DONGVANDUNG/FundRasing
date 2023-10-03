using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiNotification")]
    public class ESiNotification: FullAuditedEntity<long>, IEntity<long>
    {
        [StringLength(250)]
        public string DocumentTitle { get; set; }
        [StringLength(2500)]
        public string Description { get; set; }
        public long? ApprovedByUserId { get; set; }
        [StringLength(50)]
        public string ApprovedByUserName { get; set; }
        [StringLength(50)]
        public string ApprovedByName { get; set; }
        public long? DocumentId { get; set; }
        public long? SenderByUserId { get; set; }
        [StringLength(50)]
        public string SenderByUserName { get; set; }
        [StringLength(50)]
        public string SenderByName { get; set; }
        public long? DocumentUserId { get; set; }
        [StringLength(1)]
        public string IsRead { get; set; }
    }
}
