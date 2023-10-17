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
    public class FundRaiser 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Introduce { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public int UserId { get; set; }
        public int FundPackageId { get; set; }
        public string CompanyName { get; set; }             
    }
}
