using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("FundRaisingTopic")]
    public class FundRaisingTopic : FullAuditedEntity<int>
    {
        public int FundId { get; set; }
        public string TopicName { get; set; }
        public string Descripton { get; set; }
        public bool? Status { get; set; }
    }
}
