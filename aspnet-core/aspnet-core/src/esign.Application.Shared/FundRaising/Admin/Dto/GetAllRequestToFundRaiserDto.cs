using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.Admin.Dto
{
    public class GetAllRequestToFundRaiserDto
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public DateTime? RequestTime { get; set; }
        public bool?  IsApprove { get; set; }
        public string UserName { get; set; }
    }
}
