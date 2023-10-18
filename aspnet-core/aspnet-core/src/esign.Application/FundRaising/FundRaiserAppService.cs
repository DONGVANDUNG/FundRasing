using Abp.Domain.Repositories;
using Abp.UI;
using esign.Entity;
using esign.FundRaising.FundRaiserService;
using esign.FundRaising.FundRaiserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using esign.Authorization.Users;

namespace esign.FundRaising
{
    public class FundRaiserAppService : esignAppServiceBase, IFundRaiser
    {
        private readonly IRepository<Funds, int> _mstSleFundRepo;
        private readonly IRepository<FundTransactions, int> _mstSleFundTransactionRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        public FundRaiserAppService(IRepository<Funds, int> mstSleFundRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
        }
        public void CloseFundRaising(int fundId)
        {
            try
            {
                var fund = _mstSleFundRepo.FirstOrDefault(s => s.Id == fundId);
                if (fund != null)
                {
                    fund.Status = 3;
                }
                _mstSleFundRepo.Update(fund);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình thêm mới");
            }
        }

        public bool CreateFundRaising(CreateOrEditFundRaisingDto input)
        {

            return true;
        }

        public bool UpdateFundRaising(CreateOrEditFundRaisingDto input)
        {
            return true;
        }

        public void ExtendTimeOfFundRaising(DateTime timeExtend)
        {
            throw new NotImplementedException();
        }

        public List<TransactionOfFundForDto> getListTransactionForFund(int fundId)
        {
            var listTransaction = (from transaction in _mstSleFundTransactionRepo.GetAll().Where(e => e.Id == fundId)
                                   join fund in _mstSleFundRepo.GetAll() on transaction.FundId equals fund.Id
                                   join user in _mstSleUserRepo.GetAll() on transaction.UserId equals user.Id
                                   select new TransactionOfFundForDto
                                   {
                                       Id = transaction.Id,
                                       Sender = user.Name,
                                       Amount = transaction.AmountOfMoney,
                                       Content = transaction.MessageToFund,
                                       FundName = fund.FundName
                                   }).ToList();
            return listTransaction;
        }

        public List<UserWarningForDto> getListWarningOfUser()
        {
            throw new NotImplementedException();
        }

        public bool RegisterAccountFundRaising(RegisterInforFundRaiserDto input)
        {
            throw new NotImplementedException();
        }

        public void UpdateImageUrlForFund(string imageUrl,int fundId)
        {
            try
            {
                var fund = _mstSleFundRepo.FirstOrDefault(e => e.Id == fundId);
                if(fund != null)
                {
                    fund.FundImageUrl = imageUrl;
                    _mstSleFundRepo.Update(fund);
                }
            }
            catch(Exception ex) {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình cập nhật");
            }
        }

        public bool UpdateInformation(GetInformationFundRaiserDto information)
        {
            throw new NotImplementedException();
        }
    }
}
