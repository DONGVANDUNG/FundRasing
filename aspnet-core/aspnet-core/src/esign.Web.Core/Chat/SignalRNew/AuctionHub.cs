using esign.FundRaising.UserFundRaising.Dto.Auction;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace esign.Web.Chat.SignalRNew
{
    public class AuctionHub :Hub
    {
        public AuctionHub()
        {
        }
        public async Task UpdateAmountOfAuction(GetAllAuctionDto input)
        {
            //var signalRClient = GetSignalRClientOrNull(client);
            //if (signalRClient == null)
            //{
            //    continue;
            //}
            await Clients.All.SendAsync("EmitAmountAuction", input);
        }
        //private IClientProxy GetSignalRClientOrNull(IOnlineClient client)
        //{
        //    var signalRClient = _chatHub.Clients.Client(client.ConnectionId);
        //    if (signalRClient == null)
        //    {
        //        Logger.Debug("Can not get chat user " + client.UserId + " from SignalR hub!");
        //        return null;
        //    }

        //    return signalRClient;
        //}
    }
}
