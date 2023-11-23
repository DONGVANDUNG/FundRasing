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
    [Table("FundTransactions")]
    public class FundTransactions : FullAuditedEntity<int>
    {
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int FundId { get; set; }
        public string MessageToFund { get; set; }
        public float AmountOfMoney { get; set; }
        public float Commission { get; set; }
        public int UserId { get; set; }
        //public string TransactionCode { get; set; }
    }
}
