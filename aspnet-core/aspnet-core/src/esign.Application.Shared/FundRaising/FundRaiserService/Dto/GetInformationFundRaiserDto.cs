using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class GetInformationFundRaiserDto
    {
        public long Id { get; set; }
        public string  ImageUrl { get; set; }
        public string AccountLogin { get; set; }
        public string StatusAccount { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
    }
}
