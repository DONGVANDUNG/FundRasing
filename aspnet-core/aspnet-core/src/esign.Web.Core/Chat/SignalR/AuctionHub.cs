using Abp.Domain.Repositories;
using esign.Enitity;
using esign.FundRaising;
using esign.FundRaising.UserFundRaising.Dto.Auction;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace esign.Web.Chat.SignalRNew
{
    public class AuctionHub : Hub
    {
        private readonly UserAuctionAppService _userAppService;
        private readonly IRepository<AuctionItems, long> _auctionItemsRepo;
        public AuctionHub(
           UserAuctionAppService userAppService
, IRepository<AuctionItems, long> auctionItemsRepo)
        {
            _userAppService = userAppService;
            _auctionItemsRepo = auctionItemsRepo;
        }
        public async Task UpdateAmountOfAuction(float amountAuction, long? auctionItemId, bool isPublic)
        {
            var auctionItem = _auctionItemsRepo.FirstOrDefault(e => e.Id == auctionItemId);
            auctionItem.AuctionPresentAmount = amountAuction;
            await _auctionItemsRepo.UpdateAsync(auctionItem);
            UserAuction userAuction = new UserAuction
            {
                AmountAuction = amountAuction,
                AuctionItemId = auctionItemId,
                IsPublic = isPublic
            };
            var amountJumnpMin = auctionItem.AmountJumpMin + auctionItem.AuctionPresentAmount;
            var amountJumnpMax = auctionItem.AmountJumpMax + auctionItem.AuctionPresentAmount;
            await _userAppService.UserAuction(userAuction);
            await Clients.All.SendAsync("updateAuction", 60000, 800000, 200000);
        }

    }
}
