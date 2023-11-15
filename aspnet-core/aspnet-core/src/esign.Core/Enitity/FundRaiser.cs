using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    [Table("FundRaiser")]
    public class FundRaiser : FullAuditedEntity<int>
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public string Introduce { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public long? UserId { get; set; }
        public int FundPackageId { get; set; }
        public string CompanyName { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
        public int Surplus { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
