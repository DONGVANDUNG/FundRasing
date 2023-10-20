﻿using Abp.Domain.Repositories;
using Abp.UI;
using esign.Entity;
using esign.FundRaising.FundRaiserService;
using esign.FundRaising.FundRaiserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using esign.Authorization.Users;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Twilio.Rest.Api.V2010.Account;

namespace esign.FundRaising
{
    public class FundRaiserAppService : esignAppServiceBase, IFundRaiser
    {
        private readonly IRepository<Funds, int> _mstSleFundRepo;
        private readonly IRepository<FundTransactions, int> _mstSleFundTransactionRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundDetailContent, int> _mstSleDetailConentRepo;
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;
        private readonly IRepository<UserAccount, int> _mstSleUserAccountRepo;
        private readonly IRepository<FundRaiser, int> _mstSleFundRaiserRepo;
        public FundRaiserAppService(IRepository<Funds, int> mstSleFundRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo,
            IRepository<FundDetailContent, int> mstSleDetailConentRepo,
            IRepository<UserWarning, int> mstSleUserWarningRepo,
            IRepository<UserAccount, int> mstSleUserAccountRepo,
            IRepository<FundRaiser, int> mstSleFundRaiserRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleDetailConentRepo = mstSleDetailConentRepo;
            _mstSleUserWarningRepo = mstSleUserWarningRepo;
            _mstSleUserAccountRepo = mstSleUserAccountRepo;
            _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
        }
        public async Task CloseFundRaising(int fundId)
        {
            try
            {
                var fund =await _mstSleFundRepo.FirstOrDefaultAsync(s => s.Id == fundId);
                if (fund != null)
                {
                    fund.Status = 3;
                }
               await _mstSleFundRepo.UpdateAsync(fund);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình thêm mới");
            }
        }

        public async Task CreateFundRaising(CreateOrEditFundRaisingDto input)
        {

            try
            {
                Funds fundInsert = new Funds();
                fundInsert.FundRaiserId = AbpSession.UserId;
                fundInsert.FundRaisingDay = DateTime.Now;
                fundInsert.FundName = input.FundName;
                fundInsert.FundImageUrl = input.ImageUrl;
                fundInsert.FundTitle = input.TitleFund;
                fundInsert.AmountOfMoney = input.AmountOfMoney;
                fundInsert.FundEndDate = input.FundEndDate;
                fundInsert.Status = 1;
                var fundId = await _mstSleFundRepo.InsertAndGetIdAsync(fundInsert);
                FundDetailContent detailContent = new FundDetailContent();
                ObjectMapper.Map(input.ContentOfFund, detailContent);
                detailContent.FundId = fundId;
                await _mstSleDetailConentRepo.InsertAsync(detailContent);
            }
            catch(Exception ex)
            {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình thêm mới");
            }
        }

        public async Task UpdateFundRaising(CreateOrEditFundRaisingDto input)
        {

            try
            {
                var fund = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                if(fund != null)
                {
                    fund.FundRaiserId = AbpSession.UserId;
                    fund.FundRaisingDay = DateTime.Now;
                    fund.FundName = input.FundName;
                    fund.FundImageUrl = input.ImageUrl;
                    fund.FundTitle = input.TitleFund;
                    fund.AmountOfMoney = input.AmountOfMoney;
                    fund.FundEndDate = input.FundEndDate;
                    await _mstSleFundRepo.UpdateAsync(fund);
                    FundDetailContent detailContent = new FundDetailContent();
                    ObjectMapper.Map(input.ContentOfFund, detailContent);
                    detailContent.FundId = fund.Id;
                    await _mstSleDetailConentRepo.UpdateAsync(detailContent);
                }
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình thêm mới");
            }
        }

        public async Task ExtendTimeOfFundRaising(DateTime timeExtend,int fundId)
        {
            var fund = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == fundId);
            if(fund != null)
            {
                fund.FundEndDate = timeExtend;
                fund.Status = 2;
                await _mstSleFundRepo.UpdateAsync(fund);
            }
        }

        public async Task<List<TransactionOfFundForDto>> getListTransactionForFund(int fundId)
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
                                   }).ToListAsync();
            return await listTransaction;
        }

        public async Task<List<UserWarningForDto>> getListWarningOfUser()
        {
            var listWarning = (from userWarning in _mstSleUserWarningRepo.GetAll()
                               join user in _mstSleUserRepo.GetAll() on userWarning.UserId equals user.Id
                               select new UserWarningForDto
                               {
                                   Id = userWarning.Id,
                                   Content = userWarning.ContentWarning,
                                   LevelWarning = userWarning.LevelWarning == 1 ? "Cảnh cáo" : userWarning.LevelWarning == 2 ? "Khẩn cấp" : "Khóa",
                                   CreatedWarning = userWarning.CreationTime
                               }).ToListAsync();
            return await listWarning;
        }

        public async Task RegisterAccountFundRaising(RegisterInforFundRaiserDto input)
        {
            try
            {
                User newUser = new User();
                ObjectMapper.Map(input, newUser);
                var userId = await _mstSleUserRepo.InsertAndGetIdAsync(newUser);
                var fundRaiser = new FundRaiser();
                fundRaiser.UserId = userId;
                fundRaiser.Phone = input.Phone;
                fundRaiser.Email = input.Email;
                fundRaiser.Position = input.Position;
                fundRaiser.CompanyName = input.Company;
                fundRaiser.Country = input.Country;
                fundRaiser.Introduce = input.Introduce;
                fundRaiser.FundPackageId = input.FundPackageId;
                var funRaiserId = await _mstSleFundRaiserRepo.InsertAndGetIdAsync(fundRaiser);
                var userAccount = new UserAccount();
                userAccount.FundRaiserId = funRaiserId;
                userAccount.Status = true;
                userAccount.UserId = userId;
                userAccount.UserNameLogin = input.Email;
                userAccount.Password = "123243";
                userAccount.LevelWarning = 0;
            }
            catch  (Exception ex)
            {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình đăng ký");
            }
        }

        public async Task UpdateImageUrlForFund(string imageUrl,int fundId)
        {
            try
            {
                var fund = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == fundId);
                if(fund != null)
                {
                    fund.FundImageUrl = imageUrl;
                    await _mstSleFundRepo.UpdateAsync(fund);
                }
            }
            catch(Exception ex) {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình cập nhật");
            }
        }
        public async Task UpdateInformation(UpdateInformationFundRaiserDto input)
        {

            var user = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId);
            if (user != null)
            {
                ObjectMapper.Map(input, user);
                await _mstSleUserRepo.UpdateAsync(user);
                var userAccount = await _mstSleUserAccountRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                userAccount.UserNameLogin = input.UserNameLogin;
                //userAccount.Password = input.Password;
                await _mstSleUserAccountRepo.UpdateAsync(userAccount);
            }
        }
    }
}