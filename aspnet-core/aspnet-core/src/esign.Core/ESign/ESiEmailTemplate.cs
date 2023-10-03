using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace esign.ESign
{
    [Table("ESiEmailTemplate")]
    public class ESiEmailTemplate : FullAuditedEntity<long>, IEntity<long>
    {
        [StringLength(50)]
        public string TemplateCode { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Bcc { get; set; }
        [StringLength(2500)]
        public string Message { get; set; }
    }
}
