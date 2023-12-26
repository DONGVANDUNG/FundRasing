using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using esign.Enitity;
using esign.FundRaising;
using esign.FundRaising.FundRaiserService.Dto;
using esign.FundRaising.UserFundRaising.Dto.Auction;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace esign.Web.Chat.SignalRNew
{
    public class AuctionHub : Hub
    {
        private readonly IUserAuction _userAppService;
        private readonly IRepository<AuctionItems, long> _auctionItemsRepo;
        private readonly IDapperRepository<AuctionItems, long> _dapperRepo;
        private readonly IUnitOfWorkManager _unitOfWorkManager;


        public AuctionHub(
           IUserAuction userAppService
         , IRepository<AuctionItems, long> auctionItemsRepo,
IDapperRepository<AuctionItems, long> dapperRepo,
IUnitOfWorkManager unitOfWorkManager)
        {
            _userAppService = userAppService;
            _auctionItemsRepo = auctionItemsRepo;
            _dapperRepo = dapperRepo;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task UpdateAmountOfAuction(float? amountAuction, long? auctionItemId, bool isPublic,long userId)
        {
            //auctionItem.AuctionPresentAmount = amountAuction;
            //await _auctionItemsRepo.UpdateAsync(auctionItem);
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                var auctionItem = await _auctionItemsRepo.FirstOrDefaultAsync(e => e.Id == auctionItemId);
                if(auctionItem.Status == 3)
                {
                    throw new UserFriendlyException("Vật phẩm đã kết thúc đấu giá");
                }
                UserAuction userAuction = new UserAuction
                {
                    AmountAuction = amountAuction,
                    AuctionItemId = auctionItemId,
                    IsPublic = isPublic,
                    UserId = userId
                };
                var amountJumnpMin = auctionItem.AmountJumpMin + auctionItem.AuctionPresentAmount;
                var amountJumnpMax = auctionItem.AmountJumpMax + auctionItem.AuctionPresentAmount;
                await _userAppService.UserAuction(userAuction);
                await Clients.All.SendAsync("updateAuction", auctionItem.AuctionPresentAmount, amountJumnpMin, amountJumnpMax);
                unitOfWork.Complete();
            }
        }

    }
}
