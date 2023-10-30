using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using esign.Authorization.Users;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.Admin;
using esign.FundRaising.Admin.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using esign.FundRaising.UserFundRaising.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Fax;

namespace esign.FundRaising
{
    public class AdminFundRaisingAppService : esignAppServiceBase, IAdminFundRaising
    {
        private readonly IRepository<Funds, int> _mstSleFundRepo;
        private readonly IRepository<FundRaiser, int> _mstSleFundRaiserRepo;
        private readonly IRepository<FundDetailContent, int> _mstSleFundDetailContentRepo;
        private readonly IRepository<FundRaisingTopic, int> _mstSleFundTopictRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<UserAccount, int> _mstSleUserAccountRepo;
        private readonly IRepository<FundTransactions, int> _mstSleTransactionRepo;
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;
        private readonly IRepository<GuestAccount, int> _mstSleGuestAccountRepo;


        public AdminFundRaisingAppService(IRepository<Funds> mstSleFundRepo, IRepository<FundRaiser, int>
            mstSleFundRaiserRepo, IRepository<FundDetailContent, int> mstSleFundDetailContentRepo,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo,
            IRepository<User, long> mstSleUserRepo,
            IRepository<UserAccount, int> mstSleUserAccountRepo,
            IRepository<FundTransactions, int> mstSleTransactionRepo,
            IRepository<GuestAccount, int> mstSleGuestAccountRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleUserAccountRepo = mstSleUserAccountRepo;
            _mstSleTransactionRepo = mstSleTransactionRepo;
            _mstSleGuestAccountRepo = mstSleGuestAccountRepo;
        }

        public async Task<TransactionOfFundForDto> getInforTransactionById(int transactionId)
        {
            var transaction = (from trans in _mstSleTransactionRepo.GetAll().Where(e => e.Id == transactionId)
                               join fund in _mstSleFundRepo.GetAll() on trans.FundId equals fund.Id
                               select new TransactionOfFundForDto
                               {
                                   Id = trans.Id,
                                   Content = trans.MessageToFund,
                                   Amount = trans.AmountOfMoney,
                                   FundName = fund.FundName,
                                   CreatedTime = trans.CreationTime,
                                   Receiver = trans.EmailReceiver,
                                   Sender = trans.EmailSender,
                               }).FirstOrDefaultAsync();
            return await transaction;
        }

        public async Task<PagedResultDto<GetInformationFundRaiserDto>> getListFundRaiser(GetAllFundRaiserForInputDto input)
        {
            var listFundRaiser = (from user in _mstSleUserRepo.GetAll().Where(e => e.TypeUser == 2).
                                  Where(e => input.Email == null || e.Email.Contains(input.Email))
                                  .Where(e => input.StatusAccount == null || e.IsActive == input.StatusAccount)
                                  join fundRaising in _mstSleFundRaiserRepo.GetAll().Where(e => input.Created == null || e.CreationTime == input.Created)

                                  on user.Id equals fundRaising.UserId
                                  select new GetInformationFundRaiserDto    
                                  {
                                      Id = fundRaising.Id,
                                      Description = fundRaising.Introduce,
                                      Name = fundRaising.Name,
                                      Position = fundRaising.Position,
                                      StatusAccount = user.IsActive == true ? "Hoạt động" : "Ngừng hoạt động",
                                      ImageUrl = user.ImageUrl
                                  });

            var totalCount = await listFundRaiser.CountAsync();
            var result = await listFundRaiser.PageBy(input).ToListAsync();

            return new PagedResultDto<GetInformationFundRaiserDto>(
                totalCount,
                result);
        }

        public async Task<PagedResultDto<GetFundRaisingViewForAdminDto>> getListFundRaising(FundRaisingInputDto input)
        {
            var listFundRaising = from fundRaising in _mstSleFundRepo.GetAll()
                                  join funRaiser in _mstSleFundRaiserRepo.GetAll() on fundRaising.FundRaiserId equals funRaiser.Id
                                  select new GetFundRaisingViewForAdminDto
                                  {
                                      Id = fundRaising.Id,
                                      FundName = fundRaising.FundName,
                                      FundFinishDay = fundRaising.FundRaisingDay,
                                      FundRaisingDay = fundRaising.FundRaisingDay,
                                      FundRaiser = funRaiser.Name,
                                      AmountOfMoney = fundRaising.AmountOfMoney,
                                      Status = fundRaising.Status == 3 ? "Đã đóng" : "Đang hoạt động"
                                  };
            var totalCount = await listFundRaising.CountAsync();
            return new PagedResultDto<GetFundRaisingViewForAdminDto>(
              totalCount,
              await listFundRaising.PageBy(input).ToListAsync());
        }

        public async Task<PagedResultDto<TransactionOfFundForDto>> getListTransactionForFund(TransactionForFundInputDto input)
        {
            var listTransaction = from transaction in _mstSleTransactionRepo.GetAll().Where(e => e.FundId == input.FundId)
                                  join user in _mstSleUserRepo.GetAll() on transaction.EmailSender equals user.Email
                                  //join fund in _mstSleFundRepo.GetAll() on transaction.FundId equals fund.Id
                                  select new TransactionOfFundForDto
                                  {
                                      Id = transaction.Id,
                                      Amount = transaction.AmountOfMoney,
                                      Content = transaction.MessageToFund,
                                      //FundName = fund.FundName,
                                      Receiver = user.UserName,
                                      Sender = user.UserLogin,
                                      CreatedTime = transaction.CreationTime
                                  };
            var totalCount = await listTransaction.CountAsync();
            return new PagedResultDto<TransactionOfFundForDto>(
              totalCount,
              await listTransaction.PageBy(input).ToListAsync());
        }


        public async Task<List<UserAccountForViewDto>> getListUserAccount()
        {
            var listAccount = (from account in _mstSleUserAccountRepo.GetAll()
                               join user in _mstSleUserRepo.GetAll() on account.FundRaiserId equals user.Id
                               select new UserAccountForViewDto
                               {
                                   Id = account.Id,
                                   Email = user.Email,
                                   LevelWarning = account.LevelWarning == 1 ? "Cảnh cáo" : account.LevelWarning == 2 ? "Báo động" : "Khóa",
                                   UserName = user.UserName,
                                   UserNameLogin = user.UserLogin
                               }).ToListAsync();
            return await listAccount;
        }

        public async void WarningAccountUser(string contentWarning, int userId)
        {
            var user = await _mstSleUserWarningRepo.FirstOrDefaultAsync(e => e.Id == userId);
            var accountUser = await _mstSleUserAccountRepo.FirstOrDefaultAsync(e => e.UserId == userId);
            accountUser.LevelWarning += 1;
            await _mstSleUserAccountRepo.UpdateAsync(accountUser);
            user.ContentWarning = contentWarning;
            user.LevelWarning += 1;
            await _mstSleUserWarningRepo.UpdateAsync(user);
        }
        public async Task<PagedResultDto<GetListFundPackageDto>> GetListFundPackage(FundPackageInputDto input)
        {
            var listFundPackage = from funPackage in _mstSleFundPackageRepo.GetAll().Where(e => e.Status == true)
                                  select new GetListFundPackageDto
                                  {
                                      Id = funPackage.Id,
                                      Discount = funPackage.Discount,
                                      PaymenFee = funPackage.PaymenFee,
                                      Description = funPackage.Description,
                                      Duration = funPackage.Duration,
                                      CreatedTime = funPackage.CreationTime
                                  };
            var totalCount = await listFundPackage.CountAsync();
            return new PagedResultDto<GetListFundPackageDto>
                (totalCount, await listFundPackage.PageBy(input).ToListAsync());
        }

        public async Task CreateOrEditFundPackage(CreateOrEditFundPackageDto input)
        {
            if (input.Id == null)
            {
                await CreateFundPackage(input);
            }
        }
        public async Task CreateFundPackage(CreateOrEditFundPackageDto input)
        {
            var checkExisted = _mstSleFundPackageRepo.GetAll().Where(e => e.PaymenFee == input.PaymenFee && e.Discount == input.Discount);
            if (checkExisted.Count() >= 1)
            {
                throw new UserFriendlyException("Đã tồn tại gói quỹ");
            }
            else
            {
                var fundPackage = new FundPackage();
                fundPackage.UserId = (int)AbpSession.UserId;
                fundPackage.PaymenFee = input.PaymenFee;
                fundPackage.Discount = input.Discount;
                fundPackage.Duration = input.Duration;
                fundPackage.Description = input.Description;
                fundPackage.Status = input.Status;
                await _mstSleFundPackageRepo.InsertAsync(fundPackage);
            }
        }
        public async Task UpdateFundPackage(CreateOrEditFundPackageDto input)
        {
            var fund = await _mstSleFundPackageRepo.FirstOrDefaultAsync(e => e.Id == input.Id && e.Status == true);
            if (fund == null)
            {
                fund.UserId = (int)AbpSession.UserId;
                fund.PaymenFee = input.PaymenFee;
                fund.Discount = input.Discount;
                fund.Duration = input.Duration;
                fund.Description = input.Description;
                fund.Status = input.Status;
                await _mstSleFundPackageRepo.UpdateAsync(fund);
            }
            else
            {
                throw new UserFriendlyException("Gói quỹ hiện tại đang được sử dụng bởi người dùng");
            }
        }
        public async Task DeleteFundPackage(int fundPackageId)
        {
            var fundPackage = await _mstSleFundPackageRepo.FirstOrDefaultAsync(e => e.Id == fundPackageId);

            var checkUsed = await _mstSleFundRaiserRepo.FirstOrDefaultAsync(e => e.FundPackageId == fundPackageId);
            if (checkUsed == null)
            {
                await _mstSleFundPackageRepo.DeleteAsync(fundPackage);
            }
            else
                throw new UserFriendlyException("Gói quỹ hiện tại đang được sử dụng bởi người dùng");
        }
        public FundPackageGetForEditDto getForEditFundPackage(int fundPackageId)
        {
            var fundPackage = _mstSleFundPackageRepo.FirstOrDefaultAsync(e => e.Id == fundPackageId);
            FundPackageGetForEditDto fundEdit = new FundPackageGetForEditDto();
            ObjectMapper.Map(fundPackage, fundEdit);
            return fundEdit;
        }
        public async Task<PagedResultDto<GetListAccountUserDto>> getAllListAccount(GuestAccountForInputDto input)
        {
            var guestAccount = from account in _mstSleGuestAccountRepo.GetAll()
                               select new GetListAccountUserDto
                               {
                                   Id = account.Id,
                                   UserName = account.UserName,
                                   Email = account.Email,
                                   Status = account.Status == true ? "Đang hoạt động" : "Ngừng hoạt động",
                                   Created = account.CreationTime
                               };
            var totalCount = await guestAccount.CountAsync();
            return new PagedResultDto<GetListAccountUserDto>(
                totalCount,
                await guestAccount.PageBy(input).ToListAsync()
                );
        }
    }
}
