using Abp.Domain.Repositories;
using esign.Authorization.Users;
using esign.Enitity;
using esign.FundRaising;
using esign.FundRaising.FundRaiserService;
using esign.FundRaising.UserFundRaising.Dto.Auction;
using esign.Web.Chat.SignalR;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace esign.Web.Chat.SignalRNew
{
    public class AuctionHub : Hub
    {
        private readonly IHubContext<AuctionHub> _context;
        private readonly IUserFundRaising _userAppService;
        private readonly IRepository<Auction, long> _auctionRepo;
        private readonly IRepository<AuctionItems, long> _auctionItemsRepo;
        private readonly IHubContext<ChatHub> _chatHub;
        public AuctionHub(
            IRepository<Auction, long> auctionRepo,
            IHubContext<ChatHub> chatHub,
            IRepository<AuctionItems, long> auctionItemsRepo,
            IUserFundRaising userAppService)
        {
            _auctionRepo = auctionRepo;
            _chatHub = chatHub;
            _auctionItemsRepo = auctionItemsRepo;
            _userAppService = userAppService;
        }
        public async Task UpdateAmountOfAuction(float amountAuction, long auctionId, bool isPublic)
        {
            var auctionItem = await _auctionItemsRepo.FirstOrDefaultAsync(e => e.AuctionId == auctionId);
            auctionItem.AuctionPresentAmount = amountAuction;
            await _auctionItemsRepo.UpdateAsync(auctionItem);
            UserAuction userAuction = new UserAuction
            {
                AmountAuction = amountAuction,
                AuctionId = auctionId,
                IsPublic = isPublic
            };
            var amountJumnpMin = auctionItem.AmountJumpMin + auctionItem.AuctionPresentAmount;
            var amountJumnpMax = auctionItem.AmountJumpMax + auctionItem.AuctionPresentAmount;
            await _userAppService.UserAuction(userAuction);
            await Clients.All.SendAsync("updateAuction", auctionItem.AuctionPresentAmount, amountJumnpMin, amountJumnpMax);
        }
    }
}
