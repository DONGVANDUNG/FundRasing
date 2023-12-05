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
using esign.FundRaising.SendEmail;
using esign.FundRaising.SendEmail.Dto;
using esign.FundRaising.UserFundRaising.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Fax;

namespace esign.FundRaising
{
    public class AdminFundRaisingAppService : esignAppServiceBase, IAdminFundRaising
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        //private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<FundDetailContent, long> _mstSleFundDetailContentRepo;
        private readonly IRepository<FundRaisingTopic, int> _mstSleFundTopictRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundTransactions, long> _mstSleTransactionRepo;
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;
        private readonly IRepository<GuestAccount, int> _mstSleGuestAccountRepo;
        private readonly IRepository<FundImage, long> _mstSleFundImageRepo;
        private readonly IRepository<RequestToFundRaiser, long> _mstRequestToFundraiserRepo;
        private readonly ISendEmail _sendEmail;


        public AdminFundRaisingAppService(IRepository<Funds, long> mstSleFundRepo,
            //IRepository<FundRaiser, long>mstSleFundRaiserRepo,
            IRepository<FundDetailContent, long> mstSleFundDetailContentRepo,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo,
            IRepository<User, long> mstSleUserRepo,
            IRepository<FundTransactions, long> mstSleTransactionRepo,
            IRepository<GuestAccount, int> mstSleGuestAccountRepo,
            IRepository<FundImage, long> mstSleFundImageRepo,
            IRepository<RequestToFundRaiser, long> mstRequestToFundraiserRepo,
            ISendEmail sendEmail)
        {
            _mstSleFundRepo = mstSleFundRepo;
            //_mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailContentRepo = mstSleFundDetailContentRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleTransactionRepo = mstSleTransactionRepo;
            _mstSleGuestAccountRepo = mstSleGuestAccountRepo;
            _mstSleFundImageRepo = mstSleFundImageRepo;
            _mstRequestToFundraiserRepo = mstRequestToFundraiserRepo;
            _sendEmail = sendEmail;
        }

        public async Task<TransactionOfFundForDto> getInforTransactionById(int transactionId)
        {
            var transaction = (from trans in _mstSleTransactionRepo.GetAll().Where(e => e.Id == transactionId)
                               join fund in _mstSleFundRepo.GetAll() on trans.FundId equals fund.Id
                               select new TransactionOfFundForDto
                               {
                                   //Id = trans.Id,
                                   Content = trans.MessageToFund,
                                   Amount = trans.AmountOfMoney,
                                   FundName = fund.FundName,
                                   CreatedTime = trans.CreationTime,
                                   Receiver = trans.Receiver,
                                   UserDonate = trans.Sender,
                               }).FirstOrDefaultAsync();
            return await transaction;
        }

        public async Task<PagedResultDto<GetInformationFundRaiserDto>> getListFundRaiser(GetAllFundRaiserForInputDto input)
        {
            var listFundRaiser = (from user in _mstSleUserRepo.GetAll().Where(e => e.TypeUser == 3).
                                  Where(e => input.Email == null || e.Email.Contains(input.Email))
                                  .Where(e => input.StatusAccount == null || e.IsActive == input.StatusAccount)
                                  .Where(e => input.Created == null || e.FundRaiserDate == input.Created)

                                  select new GetInformationFundRaiserDto
                                  {
                                      Id = user.Id,
                                      //Description = fundRaising.Introduce,
                                      Name = user.Name,
                                      //Position = user.Position,
                                      StatusAccount = user.IsActive == true ? "Hoạt động" : "Ngừng hoạt động",
                                      //ImageUrl = user.ImageUrl
                                  });

            var totalCount = await listFundRaiser.CountAsync();
            var result = await listFundRaiser.PageBy(input).ToListAsync();

            return new PagedResultDto<GetInformationFundRaiserDto>(
                totalCount,
                result);
        }

        public async Task<PagedResultDto<GetFundRaisingViewForAdminDto>> getListFundRaising(FundRaisingInputDto input)
        {
            var listFundRaising = from fundRaising in _mstSleFundRepo.GetAll().Where(e => input.Filter == null || e.FundTitle.Contains(input.Filter)
                                   || e.FundName.Contains(input.Filter))
                                   .Where(e => input.IsPayFee == null || e.IsPayFee == input.IsPayFee)
                                   .Where(e => input.CreatedDate == null || e.FundRaisingDay == input.CreatedDate)
                                  select new GetFundRaisingViewForAdminDto
                                  {
                                      //Id = (int)fundRaising.Id,
                                      //FundName = fundRaising.FundName,
                                      //FundFinishDay = fundRaising.FundRaisingDay,
                                      //FundRaisingDay = fundRaising.FundRaisingDay,
                                      //AmountOfMoney = fundRaising.AmountOfMoney,
                                      //Status = fundRaising.Status == 3 ? "Đã đóng" : "Đang hoạt động",
                                      //ListImageUrl = _mstSleFundImageRepo.GetAll().Where(e => e.FundId == fundRaising.Id).Select(e => e.ImageUrl).ToList(),
                                      //FundStartDate = fundRaising.FundRaisingDay,
                                      //FundTitle = fundRaising.FundTitle,
                                      Unit = "Coin"
                                  };

            var totalCount = await listFundRaising.CountAsync();
            var result = await listFundRaising.PageBy(input).ToListAsync();
            return new PagedResultDto<GetFundRaisingViewForAdminDto>(
               totalCount,
               result);
        }

        public async Task<PagedResultDto<TransactionOfFundForDto>> getListTransactionForFund(TransactionForFundInputDto input)
        {
            var listTransaction = from transaction in _mstSleTransactionRepo.GetAll().Where(e => e.FundId == input.FundId)
                                  join user in _mstSleUserRepo.GetAll() on transaction.Sender equals user.Email
                                  join fund in _mstSleFundRepo.GetAll() on transaction.FundId equals fund.Id
                                  select new TransactionOfFundForDto
                                  {
                                      Id = transaction.Id,
                                      Amount = transaction.AmountOfMoney,
                                      Content = transaction.MessageToFund,
                                      FundName = fund.FundName,
                                      Receiver = user.UserName,
                                      UserDonate = user.UserLogin,
                                      CreatedTime = transaction.CreationTime
                                  };
            var totalCount = await listTransaction.CountAsync();
            return new PagedResultDto<TransactionOfFundForDto>(
              totalCount,
              await listTransaction.PageBy(input).ToListAsync());
        }


        //public async void WarningAccountUser(string contentWarning, int userId)
        //{
        //    var user = await _mstSleUserWarningRepo.FirstOrDefaultAsync(e => e.Id == userId);
        //    var accountUser = await _mstSleUserAccountRepo.FirstOrDefaultAsync(e => e.UserId == userId);
        //    accountUser.LevelWarning += 1;
        //    await _mstSleUserAccountRepo.UpdateAsync(accountUser);
        //    user.ContentWarning = contentWarning;
        //    user.LevelWarning += 1;
        //    await _mstSleUserWarningRepo.UpdateAsync(user);
        //}
        public async Task<PagedResultDto<GetListFundPackageDto>> GetListFundPackage(FundPackageInputDto input)
        {
            var listFundPackage = from funPackage in _mstSleFundPackageRepo.GetAll().Where(e => e.Status == true)
                                  .Where(e => input.CreatedDate == null || e.CreationTime == input.CreatedDate)
                                  .Where(e => input.TypePackage == null || e.Duration == input.TypePackage)
                                  select new GetListFundPackageDto
                                  {
                                      Id = funPackage.Id,
                                      //Discount = funPackage.Discount,
                                      PaymentFee = funPackage.PaymentFee.ToString() + "VND",
                                      Description = funPackage.Description,
                                      Duration = funPackage.Duration,
                                      CreatedTime = funPackage.CreationTime,
                                      Commission = funPackage.Commission.ToString() + "%/giao dịch"
                                  };
            var totalCount = await listFundPackage.CountAsync();
            return new PagedResultDto<GetListFundPackageDto>
                (totalCount, await listFundPackage.PageBy(input).ToListAsync());
        }

        public async Task CreateOrEditFundPackage(CreateOrEditFundPackageDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                await CreateFundPackage(input);
            }
            else
            {
                await UpdateFundPackage(input);
            }
        }
        public async Task CreateFundPackage(CreateOrEditFundPackageDto input)
        {
            var checkExisted = _mstSleFundPackageRepo.GetAll().Where(e => e.PaymentFee == input.PaymentFee && e.Duration == input.Duration);
            if (checkExisted.Count() >= 1)
            {
                throw new UserFriendlyException("Đã tồn tại gói quỹ");
            }
            else
            {
                var fundPackage = new FundPackage();
                fundPackage.PaymentFee = (float)input.PaymentFee;
                fundPackage.Duration = input.Duration;
                fundPackage.Description = input.Description;
                fundPackage.Commission = (float)input.Commission;
                fundPackage.Status = input.Status;
                await _mstSleFundPackageRepo.InsertAsync(fundPackage);
            }
        }
        public async Task UpdateFundPackage(CreateOrEditFundPackageDto input)
        {
            var fund = await _mstSleFundPackageRepo.FirstOrDefaultAsync(e => e.Id == input.Id && e.Status == true);
            var user = await _mstSleUserRepo.GetAll().Where(e => e.FundPackageId == input.Id).Select(re => re.Id).ToListAsync();
            if (fund != null && user.Count() == 0)
            {
                //fund.UserId = (int)AbpSession.UserId;
                fund.PaymentFee = (float)input.PaymentFee;
                //fund.Discount = input.Discount;
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
        public async Task<CreateOrEditFundPackageDto> getForEditFundPackage(int fundPackageId)
        {
            var fundPackage = await _mstSleFundPackageRepo.FirstOrDefaultAsync(e => e.Id == fundPackageId);
            CreateOrEditFundPackageDto fundEdit = new CreateOrEditFundPackageDto();
            ObjectMapper.Map(fundPackage, fundEdit);
            return fundEdit;
        }
        public async Task<PagedResultDto<GetListAccountUserDto>> getAllListFundRaiser(GuestAccountForInputDto input)
        {
            var listFundRaiser = from user in _mstSleUserRepo.GetAll().Where(e=>e.TypeUser == 3)
                               .Where(e => input.CreatedDate == null || e.CreationTime == input.CreatedDate)
                               .Where(e => input.Status == null || e.IsActive == input.Status)
                               join fundPackage in _mstSleFundPackageRepo.GetAll() on user.FundPackageId equals fundPackage.Id
                               select new GetListAccountUserDto
                               {
                                   Id = user.Id,
                                   UserName = user.UserName,
                                   Email = user.EmailAddress,
                                   Status = user.IsActive == true ? "Đang hoạt động" : "Ngừng hoạt động",
                                   Created = user.CreationTime,
                                   FundEndDate = user.FundRaiserDate,
                                   FundRaiserDate = user.FundRaiserDate,
                                   FundPackage = fundPackage.PaymentFee +"VND / "+fundPackage.Duration
                               };
            var totalCount = await listFundRaiser.CountAsync();
            return new PagedResultDto<GetListAccountUserDto>(
                totalCount,
                await listFundRaiser.PageBy(input).ToListAsync()
                );
        }
        public async Task<PagedResultDto<GetAllRequestToFundRaiserDto>> getAllRequestToFundRaiser(RequestToFundRaiserInputDto input)
        {
            var listRequest = from request in _mstRequestToFundraiserRepo.GetAll()
                              join user in _mstSleUserRepo.GetAll() on request.UserId equals user.Id
                              select new GetAllRequestToFundRaiserDto
                              {
                                  Id = request.Id,
                                  UserId = user.Id,
                                  RequestTime = request.RequestTime,
                                  UserName = user.UserName,
                                  IsApprove = request.IsApprove
                              };
            var totalCount = await listRequest.CountAsync();
            return new PagedResultDto<GetAllRequestToFundRaiserDto>(
                totalCount,
                await listRequest.PageBy(input).ToListAsync()
                );
        }
        public async Task ApproveFundRaiser(long userId)
        {
            var user = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == userId);
            if(user!= null)
            {
                user.TypeUser = 3;
                user.FundRaiserDate = DateTime.Now;
                await _mstSleUserRepo.UpdateAsync(user);
                var request = await _mstRequestToFundraiserRepo.FirstOrDefaultAsync(e => e.UserId == userId);
                request.IsApprove = true;
                await _mstRequestToFundraiserRepo.UpdateAsync(request);

                SendEmailInputDto sendEmailInput = new SendEmailInputDto
                {
                    EmailReceive = user.EmailAddress,
                    //Body = "Xin chúc mừng bạn đã trở thành người gây quỹ trên hệ thống của chúng tôi, bạn có thể bắt đầu ngay vào việc gây quỹ.",
                    Body = "<p style='font-weight:bold;font-size:18px'>Hệ thống gây quỹ trực tuyến FundRaising.</p>" +
                           "<p>Xin chúc mừng bạn đã trở thành thành viên trong cộng đồng của chúng tôi. Hãy bắt tay ngay vào thực hiện những dự án" +
                           " mà bạn đang có dự định thực hiện thông qua việc:</p>" +
                           "<p style='font-weight:bold'>1. Gây quỹ từ thiện</p>" +
                           "<p style='font-weight:bold'>2. Tạo phiên đấu giá từ thiện<p>" +
                           "<i style='color:red'>Cảm ơn bạn đã tin tưởng vào sử dụng hệ thống.Chúng tôi cam kết tạo ra một môi trường gây quỹ công bằng, hợp pháp." +
                           "Chúc cho những dự án của bạn sẽ hoàn thành tốt đẹp giúp đỡ cho những hoàn cảnh khó khăn kịp thời, nâng cao chất lượng xã hội.</i>",
                    Subject = "Thông báo trở thành người gây quỹ",
                };
                _sendEmail.SendEmail(sendEmailInput);
            }

        }
    }
}
