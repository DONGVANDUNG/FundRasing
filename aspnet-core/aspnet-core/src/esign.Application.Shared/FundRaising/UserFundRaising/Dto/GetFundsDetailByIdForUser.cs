﻿using Abp.Application.Services.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace esign.FundRaising
{
    public class GetFundsDetailByIdForUser 
    {
        public long? Id { get; set; }
        public string PostTitle { get; set; }
        public string FundName { get; set; }
        public string PostTopic { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationIntroduce { get; set; }
        public string PayFee { get; set; }
        public DateTime? FundRaisingDay { get; set; }
        public DateTime? FinishFundRaising { get; set; }
        public List<string> ListImageUrl { get; set; }
        public string ReasonOfFund { get; set; }
        public string Description { get; set; }
        public decimal? AmountDonatePresent { get; set; }
        public decimal? AmountDonateTarget { get; set; }
        public decimal? PercentAchieved { get; set; }
        public string ContentFund { get; set; }
        public decimal? PaymenFee { get; set; }
        public bool? IsPayeFee { get; set; }
        public int? DonateAmount{ get; set; }
        public string Introduce { get; set; }
        public string AddressOrgnization { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public long? FundId { get; set; }
        public int? IsCloseFund { get; set; }
    }
}
