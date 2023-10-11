using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    [Table("FundPackage")]
    public class FundPackage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public float PaymenFee { get; set; }
        public bool Status { get; set; }
    }
}
