using esign.FundRaising.FundRaiserService.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class CreateOrEditFundRaisingInputDto
    {
        public long? FundId { get; set; }
        public List<GetInforFileDto> File {  get; set; }
        public string PostTitle { get; set; }
        public string TargetIntroduce { get; set; }
        public string PostTopic { get; set; }
        //post Detail
        public string Purpose { get; set; }
        public string Note { get; set; }    

    }
}
