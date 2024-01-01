using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class GetListPostByFundRaisingDto
    {
        public long? Id { get; set; }
        public string PostTitle { get; set; }
        public string TargetIntroduce { get; set; }
        public string PostTopic { get; set; }
        public string PostCreated { get; set; }
        //post Detail
        public string Purpose { get; set; }
        public string Note { get; set; }
        public string FundName { get; set; }
        public decimal? AmountOfTarget { get; set; }
        public string FundEndDate { get; set; }
        public string FundRaisingDay { get; set; }
        public string Status { get; set; }
        public int? StatusFunds { get; set; }
    }
}
