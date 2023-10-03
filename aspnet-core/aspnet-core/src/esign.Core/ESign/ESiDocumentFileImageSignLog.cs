using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.ESign
{
    [Table("ESiDocumentFileImageSignLog")]
    public class ESiDocumentFileImageSignLog : FullAuditedEntity<long>, IEntity<long>
    {
        public long? DocumentFileImageId { get; set; }
        public long? PositionX { get; set; }
        public long? PositionY { get; set; }
        public long? PositionW { get; set; }
        public long? PositionH { get; set; }
    }
}
