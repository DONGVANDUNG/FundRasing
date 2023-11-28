using Abp.Domain.Repositories;
using esign.Enitity;
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
        private readonly IFundRaiser _fundRaiser;
        private readonly IRepository<Auction, long> _auctionRepo;
        private readonly IHubContext<ChatHub> _chatHub;
        public AuctionHub(
            IRepository<Auction, long> auctionRepo,
            IHubContext<ChatHub> chatHub,
            IFundRaiser fundRaiser)
        {
            _auctionRepo = auctionRepo;
            _chatHub = chatHub;
            _fundRaiser = fundRaiser;
        }
        public async Task UpdateAmountOfAuction(float amountAuction,long auctionId,bool isPublic)
        {
            var auction = await _auctionRepo.FirstOrDefaultAsync(e => e.Id == (long)amountAuction);
            auction.AuctionPresentAmount = amountAuction;
            await _auctionRepo.UpdateAsync(auction);
            UserAuction userAuction = new UserAuction
            {
                AmountAuction = amountAuction,
                AuctionId = auctionId,
                IsPublic = isPublic
            };
            var amountJumnpMin = auction.AmountJumpMin + auction.AuctionPresentAmount;
            var amountJumnpMax = auction.AmountJumpMax + auction.AuctionPresentAmount;
            await _fundRaiser.UserAuction(userAuction);
            await Clients.All.SendAsync("updateAuction", auction.AuctionPresentAmount, amountJumnpMin, amountJumnpMax);
        }
    }
}
