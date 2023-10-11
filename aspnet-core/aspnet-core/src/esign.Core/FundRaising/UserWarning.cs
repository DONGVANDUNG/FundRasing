using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    [Table("UserWarning")]
    public class UserWarning
    {
        public int Id { get; set; }
        public string ContentWarning { get; set; }
        public int LevelWarning { get; set; }
    }
}
