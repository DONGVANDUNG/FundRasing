using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising.FundRaiserService.Dto
{
    public class GetInformationFundRaiserDto
    {
        public int Id { get; set; }
        public string  ImageUrl { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }
    }
}
