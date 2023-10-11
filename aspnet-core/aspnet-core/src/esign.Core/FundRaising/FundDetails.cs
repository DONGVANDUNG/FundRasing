using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    [Table("FundDetails")]

    public class FundDetails
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public string TitlePart { get; set; }
        public string ContentPart { get; set; }
    }
}
