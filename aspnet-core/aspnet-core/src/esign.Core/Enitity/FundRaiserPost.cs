using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.Enitity
{
    [Table("FundRaiserPost")]
    public class FundRaiserPost :FullAuditedEntity<long>
    {
        public string PostTitle { get; set; }
        public string PostTopic { get; set; }
        public long? UserId { get; set; }
        //public string ImageUrl { get; set; }
        public DateTime? FinishDay { get; set; }
        public bool? IsClose { get; set; }
        //Số tiền mục tiêu
        public float? AmountOfMoneyTarget { get; set; }
        public long? FundId { get; set; }
        //Số tiền hiện tại
        public float? AmountOfMoneyPresent { get; set; }
        //Mục tiêu
        public string TargetIntroduce { get; set; }
        //Tiến độ thực hiện
        public string ImplementationProgress { get; set; }
    }
}
