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
        //post Detail
        public string Purpose { get; set; }
        public string Note { get; set; }
    }
}
