using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("BankAccount")]
    public class BankAccount : FullAuditedEntity<long>
    {
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public string AccountName { get; set; }
        public long? UserId { get; set; }
        public float? Balance { get; set; }
        public string Unit { get; set;}

    }
}
