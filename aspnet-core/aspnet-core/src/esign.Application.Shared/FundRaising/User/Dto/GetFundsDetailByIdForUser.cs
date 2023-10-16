using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.User.Dto
{
    public class GetFundsDetailByIdForUser : EntityDto<long>
    {
        public string FundTopic { get; set; }
        public string FundRaisingDay { get; set; }
        public string FundName { get; set;}
        public string ImageUrl { get; set; }
        public string CreaterFund { get; set; }
        public List<FundRaisingDetailForUser> FundDetail { get; set; }
    }
}
