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
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;
        public FundRaiserAppService(IRepository<Funds, int> mstSleFundRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo,
            IRepository<UserWarning, int> mstSleUserWarningRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleUserWarningRepo = mstSleUserWarningRepo;
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
            var listWarning = (from userWarning in _mstSleUserWarningRepo.GetAll()
                              join user in _mstSleUserRepo.GetAll() on userWarning.UserId equals user.Id
                              select new UserWarningForDto
                              {
                                  Id = userWarning.Id,
                                  Content = userWarning.ContentWarning,
                                  LevelWarning = userWarning.LevelWarning == 1 ? "Cảnh cáo" : userWarning.LevelWarning == 2 ? "Khẩn cấp" : "Khóa",
                                  CreatedWarning = userWarning.CreationTime
                              }).ToList();
            return listWarning;
        }

        public void RegisterAccountFundRaising(RegisterInforFundRaiserDto input)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
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

        public bool UpdateInformation(CreateOrEditFundRaisingDto input)
        {
          
        }
    }
}
