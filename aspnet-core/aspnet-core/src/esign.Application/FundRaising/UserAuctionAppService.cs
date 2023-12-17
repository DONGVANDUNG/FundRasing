using Abp.Dapper.Repositories;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using esign.Authorization.Users;
using esign.Enitity;
using esign.FundRaising.FundRaiserService.Dto;
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
        private readonly IRepository<AuctionHistory, long> _mstAuctionTransactionRepo;
        private readonly IDapperRepository<AuctionItems, long> _dapperRepo;

        public UserAuctionAppService(IRepository<AuctionHistory, long> mstAuctionTransactionRepo, IRepository<AuctionItems, long> mstAuctionItemsRepo, IDapperRepository<AuctionItems, long> dapperRepo)
        {
            _mstAuctionTransactionRepo = mstAuctionTransactionRepo;
            _mstAuctionItemsRepo = mstAuctionItemsRepo;
            _dapperRepo = dapperRepo;
        }
        public async Task UserAuction(UserAuction input)
        {

            var auctionItem = _mstAuctionItemsRepo.FirstOrDefault(e => e.Id == input.AuctionItemId);
            var auctionTransaction = new AuctionHistory();
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
            auctionTransaction.AuctionItemId = input.AuctionItemId;
            auctionTransaction.AuctionDate = DateTime.Now;
            auctionTransaction.AuctioneerId = AbpSession.UserId;
            auctionTransaction.IsPublic = input.IsPublic;
            await _mstAuctionTransactionRepo.InsertAsync(auctionTransaction);
        }
    }
}
