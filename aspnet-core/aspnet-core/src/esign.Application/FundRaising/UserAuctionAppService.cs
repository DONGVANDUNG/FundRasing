using Abp.Domain.Repositories;
using Abp.UI;
using esign.Enitity;
using esign.FundRaising.UserFundRaising.Dto.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    public class UserAuctionAppService : esignAppServiceBase, IUserAuction
    {
        private readonly IRepository<AuctionItems, long> _mstAuctionItemsRepo;
        private readonly IRepository<AuctionTransactions, long> _mstAuctionTransactionRepo;

        public UserAuctionAppService(IRepository<AuctionTransactions, long> mstAuctionTransactionRepo, IRepository<AuctionItems, long> mstAuctionItemsRepo)
        {
            _mstAuctionTransactionRepo = mstAuctionTransactionRepo;
            _mstAuctionItemsRepo = mstAuctionItemsRepo;
        }
        public async Task UserAuction(UserAuction input)
        {
            var auctionItem = _mstAuctionItemsRepo.FirstOrDefault(e => e.AuctionId == input.AuctionId);
            var auctionTransaction = new AuctionTransactions();
            auctionTransaction.OldAmount = auctionItem.AuctionPresentAmount != null ? auctionItem.AuctionPresentAmount : auctionItem.StartingPrice;
            if (input.AmountAuction < auctionItem.AuctionPresentAmount || input.AmountAuction > auctionItem.AuctionPresentAmount + auctionItem.AmountJumpMax)
            {
                throw new UserFriendlyException("Mức đấu thầu không hợp lệ");
            }
            if (auctionItem != null)
            {
                auctionItem.AuctionPresentAmount = input.AmountAuction;
            }

            await _mstAuctionItemsRepo.UpdateAsync(auctionItem);
            auctionTransaction.NewAmount = auctionItem.AuctionPresentAmount;
            auctionTransaction.AuctionId = input.AuctionId;
            auctionTransaction.AuctionDate = DateTime.Now;
            auctionTransaction.AuctioneerId = AbpSession.UserId;
            auctionTransaction.IsPublic = input.IsPublic;
            await _mstAuctionTransactionRepo.InsertAsync(auctionTransaction);
        }
    }
}
