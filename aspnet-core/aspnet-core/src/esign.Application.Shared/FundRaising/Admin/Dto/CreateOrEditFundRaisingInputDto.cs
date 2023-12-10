using Abp.Application.Services.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class CreateOrEditFundRaisingInputDto :EntityDto<long>
    {
        public int? FundId { get; set; }
        public string PostTitle { get; set; }
        public string TargetIntroduce { get; set; }
        public string PostTopic { get; set; }
        //post Detail
        public string Purpose { get; set; }
        public List<GetInforFileDto> File { get; set; }

    }
}
