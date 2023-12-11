using Abp.Application.Services;
using esign.FundRaising.FundRaiserService.Dto;
using esign.FundRaising.UserFundRaising.Dto.Auction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    public interface IUserAuction : IApplicationService
    {
        Task UserAuction(UserAuction input);

    }
}
