using Abp.Domain.Repositories;
using Abp.UI;
using Dapper;
using esign.Authorization.Users;
using esign.Configuration;
using esign.Enitity;
using esign.Entity;
using esign.FundRaising.Admin.Dto;
using esign.FundRaising.FundRaiserService.Dto;
using esign.FundRaising.SendEmail.Dto;
using esign.FundRaising.SendEmail;
using esign.FundRaising.UserFundRaising.Dto;
using esign.FundRaising.UserFundRaising.Dto.Auction;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace esign.FundRaising
{
    public class UserFundRaisingAppService : esignAppServiceBase, IUsersFundRaisingAppService
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        //private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;
        private readonly IRepository<FundTransactions, long> _mstSleFundTransactionRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<PostImage, long> _mstSleFundImageRepo;
        private readonly IRepository<BankAccount, long> _mstBankRepo;
        private readonly IRepository<RequestToFundRaiser, long> _mstRequestToFundRaiserRepo;
        private readonly IRepository<FundRaiserPost, long> _mstFundRaiserPostRepo;
        private readonly IRepository<PostImage, long> _mstFundImageRepo;
        private readonly IRepository<Auction, long> _mstAuctionRepo;
        private readonly IRepository<AuctionHistory, long> _mstAuctionTransactionRepo;
        private readonly IRepository<AuctionTransactionDeposit, long> _mstAuctionTransactionDeposit;
        private readonly IRepository<AuctionItems, long> _mstAuctionItemsRepo;
        private readonly IRepository<AuctionDeposit, long> _mstAuctionDepositRepo;
        private readonly IRepository<UserFundPackage, long> _mstUserFundPackageRepo;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly ISendEmail _sendEmail;


        public UserFundRaisingAppService(IRepository<Funds, long> mstSleFundRepo,
            //IRepository<FundRaiser, long>
            // mstSleFundRaiserRepo,
            IWebHostEnvironment hostingEnvironment, IWebHostEnvironment env,
            IRepository<FundPackage, int> mstSleFundPackageRepo,
            IRepository<FundTransactions, long> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo, IRepository<PostImage, long> mstSleFundImageRepo,
            IRepository<BankAccount, long> mstBankRepo,
            IRepository<FundRaiserPost, long> mstFundRaiserPostRepo,
            IRepository<PostImage, long> mstFundImageRepo,
            IRepository<RequestToFundRaiser, long> mstRequestToFundRaiserRepo,
            IRepository<Auction, long> mstAuctionRepo
            , IRepository<AuctionHistory, long> mstAuctionTransactionRepo,
            IRepository<AuctionItems, long> mstAuctionItemsRepo,
            IRepository<AuctionDeposit, long> mstAuctionDepositRepo,
            ISendEmail sendEmail, IRepository<UserFundPackage, long> mstUserFundPackageRepo,
            IRepository<AuctionTransactionDeposit, long> mstAuctionTransactionDeposit)
        {
            _mstSleFundRepo = mstSleFundRepo;
            /// _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundPackageRepo = mstSleFundPackageRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _appConfiguration = env.GetAppConfiguration();
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
            _mstSleFundImageRepo = mstSleFundImageRepo;
            _mstBankRepo = mstBankRepo;
            _mstFundRaiserPostRepo = mstFundRaiserPostRepo;
            _mstFundImageRepo = mstFundImageRepo;
            _mstRequestToFundRaiserRepo = mstRequestToFundRaiserRepo;
            _mstAuctionRepo = mstAuctionRepo;
            _mstAuctionTransactionRepo = mstAuctionTransactionRepo;
            _mstAuctionItemsRepo = mstAuctionItemsRepo;
            _mstAuctionDepositRepo = mstAuctionDepositRepo;
            _sendEmail = sendEmail;
            _mstUserFundPackageRepo = mstUserFundPackageRepo;
            _mstAuctionTransactionDeposit = mstAuctionTransactionDeposit;
        }
        public async Task<List<GetListFundRasingDto>> getHistoryDonationForFund()
        {
            var listFund = from transaction in _mstSleFundTransactionRepo.GetAll().Where(e => e.SenderId == AbpSession.UserId)
                           join fund in _mstSleFundRepo.GetAll() on transaction.FundId equals fund.Id
                           join post in _mstFundRaiserPostRepo.GetAll() on fund.Id equals post.FundId
                           join postImage in _mstFundImageRepo.GetAll() on post.Id equals postImage.PostId

                           //join fundDetail in _mstSleFundDetailRepo.GetAll() on fund.Id equals fundDetail.FundId    
                           select new GetListFundRasingDto
                           {
                               Id = post.Id,
                               ListImageUrl = _mstFundImageRepo.GetAll().Where(e => e.PostId == post.Id).Select(re => re.ImageUrl).ToList(),
                               AmountDonatePresent = fund.AmountDonationPresent,
                               PercentAchieved = (fund.AmountDonationPresent / fund.AmountDonationTarget) * 100,
                               PostTitle = post.PostTitle,
                               AmountDonateTarget = fund.AmountDonationTarget,
                               PostTopic = post.PostTopic,
                               FundId = fund.Id
                               //OrganizationName = _mstSleUserRepo.FirstOrDefault(e => e.Id == fund.UserId).IntroduceOrganization,
                               //FundDetail
                           };
            var test = await listFund.ToListAsync();
            var result = test.DistinctBy(x => x.FundId).ToList();
            return result;
        }
        public GetFundsDetailByIdForUser GetInforPostById(long Id)
        {
            var fundRaising = (from post in _mstFundRaiserPostRepo.GetAll().Where(e => e.Id == Id)
                               join fund in _mstSleFundRepo.GetAll() on (long)post.FundId equals fund.Id

                               //join postImage in _mstFundImageRepo.GetAll() on post.Id equals postImage.PostId
                               join user in _mstSleUserRepo.GetAll() on fund.UserId equals user.Id
                               select new GetFundsDetailByIdForUser
                               {
                                   Id = post.Id,
                                   ListImageUrl = _mstFundImageRepo.GetAll().Where(e => e.PostId == post.Id).Select(re => re.ImageUrl).ToList(),
                                   AmountDonatePresent = fund.AmountDonationPresent,
                                   PercentAchieved = (fund.AmountDonationPresent / fund.AmountDonationTarget) * 100,
                                   PostTitle = post.PostTitle,
                                   AmountDonateTarget = fund.AmountDonationTarget,
                                   PostTopic = post.PostTopic,
                                   OrganizationName = user.Company,
                                   FundName = fund.FundName,
                                   OrganizationIntroduce = user.IntroduceOrganization,
                                   AddressOrgnization = user.Address,
                                   Phone = user.Phone,
                                   Email = user.EmailAddress,
                                   FundId = fund.Id,
                                   Introduce = post.TargetIntroduce,
                                   DonateAmount = fund.DonateAmount,
                                   IsCloseFund = fund.Status
                               }).FirstOrDefault();
            return fundRaising;
        }
        public async Task<List<GetListFundRasingDto>> GetAllFundRaising()
        {
            var listFundOutStanding = (from post in _mstFundRaiserPostRepo.GetAll().Where(e => e.Status == 2)
                                       join fund in _mstSleFundRepo.GetAll() on post.FundId equals fund.Id
                                       join postImage in _mstFundImageRepo.GetAll() on post.Id equals postImage.PostId
                                       //join fundDetail in _mstSleFundDetailRepo.GetAll() on fund.Id equals fundDetail.FundId    
                                       select new GetListFundRasingDto
                                       {
                                           Id = post.Id,
                                           ListImageUrl = _mstFundImageRepo.GetAll().Where(e => e.PostId == post.Id).Select(re => re.ImageUrl).ToList(),
                                           AmountDonatePresent = fund.AmountDonationPresent,
                                           PercentAchieved = (fund.AmountDonationPresent / fund.AmountDonationTarget) * 100,
                                           PostTitle = post.PostTitle,
                                           AmountDonateTarget = fund.AmountDonationTarget,
                                           PostTopic = post.PostTopic,
                                           OrganizationName = _mstSleUserRepo.FirstOrDefault(e => e.Id == fund.UserId).IntroduceOrganization,
                                           //FundDetail
                                       }).ToListAsync();
            return await listFundOutStanding;
        }


        public async Task<List<GetListFundPackageDto>> GetListFundPackage()
        {
            var listFundPackage = from funPackage in _mstSleFundPackageRepo.GetAll().Where(e => e.Status == true)
                                  select new GetListFundPackageDto
                                  {
                                      Id = funPackage.Id,
                                      //Discount = funPackage.Discount,
                                      Commission = funPackage.Commission,
                                      PaymentFee = funPackage.PaymentFee,
                                      Description = funPackage.Description,
                                      Duration = funPackage.Duration,
                                      CreatedTime = funPackage.CreationTime.ToString("dd/MM/yyyy")
                                  };
            return await listFundPackage.ToListAsync();
        }
        public async Task DonateForFund(DataDonateForFundInput input)
        {

            //Trừ tiền người donate
            var accountUserDonate = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
            if (accountUserDonate == null)
            {
                throw new UserFriendlyException("Bạn chưa đăng ký tài khoản ngân hàng của hệ thống");
            }
            if (accountUserDonate.Balance < input.AmountOfMoney)
            {
                throw new UserFriendlyException("Tài khoản của bạn không đủ để donate");
            }
            accountUserDonate.Balance -= input.AmountOfMoney;
            await _mstBankRepo.UpdateAsync(accountUserDonate);

            var userCreatedFundId = _mstSleFundRepo.FirstOrDefault(e => e.Id == (long)input.FundId);
            userCreatedFundId.DonateAmount += 1;
            userCreatedFundId.AmountDonationPresent += input.AmountOfMoney;
            await _mstSleFundRepo.UpdateAsync(userCreatedFundId);

            var fundPackageId = _mstUserFundPackageRepo.FirstOrDefault(e => e.UserId == userCreatedFundId.UserId).FundPackageId;
            var fundPackage = _mstSleFundPackageRepo.FirstOrDefault(e => e.Id == fundPackageId);

            var fundRaiserId = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.FundId).UserId;
            var fundRaiserReceive = _mstSleUserRepo.FirstOrDefault(e => e.Id == fundRaiserId).UserName;
            var userSend = _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId).UserName;

            var amountOfCommission = input.AmountOfMoney * fundPackage.Commission / 100;
            //Nhận tiền
            var accountUserRaiseFund = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == fundRaiserId);
            if (accountUserRaiseFund == null)
            {
                throw new UserFriendlyException("Bạn chưa đăng ký tài khoản ngân hàng");
            }
            accountUserRaiseFund.Balance += input.AmountOfMoney - amountOfCommission;
            await _mstBankRepo.UpdateAsync(accountUserRaiseFund);

            var transactionUserDonate = new FundTransactions();
            transactionUserDonate.FundId = input.FundId;
            transactionUserDonate.AmountOfMoney = input.AmountOfMoney;
            transactionUserDonate.MessageToFund = input.NoteTransaction;
            transactionUserDonate.Commission = amountOfCommission;
            transactionUserDonate.Receiver = fundRaiserReceive;
            transactionUserDonate.Sender = userSend;
            transactionUserDonate.SenderId = AbpSession.UserId;
            transactionUserDonate.ReceiverId = fundRaiserId;
            transactionUserDonate.Balance = accountUserRaiseFund.Balance;
            transactionUserDonate.IsPublic = input.IsPublic;
            await _mstSleFundTransactionRepo.InsertAsync(transactionUserDonate);


            //Lưu giao dịch


            // Chuyển tiền cho admin
            var bankAdmin = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == 1);
            bankAdmin.Balance += input.AmountOfMoney * (fundPackage.Commission / 100);
            await _mstBankRepo.UpdateAsync(accountUserRaiseFund);

            var transactionAdmin = new FundTransactions();
            transactionAdmin.FundId = input.FundId;
            transactionAdmin.AmountOfMoney = fundPackage.PaymentFee;
            transactionAdmin.MessageToFund = "Trừ tiền phí gói quỹ";
            transactionAdmin.Commission = input.AmountOfMoney * fundPackage.Commission / 100;
            transactionAdmin.Receiver = "Admin";
            transactionAdmin.Sender = userSend;
            transactionAdmin.SenderId = fundRaiserId;
            transactionAdmin.Balance = bankAdmin.Balance;
            transactionAdmin.ReceiverId = 1;
            transactionAdmin.IsAdmin = true;

            await _mstSleFundTransactionRepo.InsertAsync(transactionAdmin);
            var userDonateCurrent = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId);
            SendEmailInputDto sendEmailInputForUser = new SendEmailInputDto
            {
                EmailReceive = userDonateCurrent.EmailAddress,
                Body = "<p style='font-weight:bold;font-size:18px'>Hệ thống gây quỹ trực tuyến FundRaising.</p>" +
                          "<p>Cảm ơn bạn đã đóng góp " + input.AmountOfMoney + " VND " + "cho quỹ " + userCreatedFundId.FundName + " vào lúc " + DateTime.Now + "</p>" +
                          "<p>Chúng tôi cam kết sẽ sử dụng số tiền của bạn để hỗ trợ những hoàn cảnh khó khăn một cách công khai, kịp thời, hiệu quả!</p>" +
                          "<i style='color:red'>Cảm ơn bạn đã tin tưởng vào sử dụng hệ thống.Chúng tôi cam kết tạo ra một môi trường gây quỹ công bằng, hợp pháp." +
                          "Chúc cho những dự án của bạn sẽ hoàn thành tốt đẹp giúp đỡ cho những hoàn cảnh khó khăn kịp thời, nâng cao chất lượng xã hội.</i>",
                Subject = "Thông báo trở thành người gây quỹ",
            };

            SendEmailInputDto sendEmailInputForFundRaiser = new SendEmailInputDto
            {
                EmailReceive = userDonateCurrent.EmailAddress,
                Body = "<p style='font-weight:bold;font-size:18px'>Hệ thống gây quỹ trực tuyến FundRaising.</p>" +
                          "<p>Người dùng " + userSend + "đã ủng hộ cho quỹ " + userCreatedFundId.FundName + " số tiền là : " + input.AmountOfMoney + " VND vào lúc " + DateTime.Now + "</p>" +
                          "<p>Email này được thông báo từ hệ thống trang web gửi tới bạn để giúp bạn năm bắt chi tiết các thông tin về việc làm từ thiện trên hệ thống</p>" +
                          "<i style='color:red'>Cảm ơn bạn đã tin tưởng vào sử dụng hệ thống.Chúng tôi cam kết tạo ra một môi trường gây quỹ công bằng, hợp pháp." +
                          "Chúc cho những dự án của bạn sẽ hoàn thành tốt đẹp giúp đỡ cho những hoàn cảnh khó khăn kịp thời, nâng cao chất lượng xã hội.</i>",
                Subject = "Thông báo trở thành người gây quỹ",
            };

            _sendEmail.SendEmail(sendEmailInputForUser);
            _sendEmail.SendEmail(sendEmailInputForFundRaiser);
        }

        public float getTotalAmountDonateOfFund(int fundId)
        {
            var listTransactionOfFund = _mstSleFundTransactionRepo.GetAll().Where(e => e.FundId == fundId).ToList();
            float totalAmount = 0;
            foreach (var transaction in listTransactionOfFund)
            {
                totalAmount += transaction.AmountOfMoney;
            }
            return totalAmount;
        }

        public List<ListUserDonateForFundDto> GetListUserDonateForFund(int fundId)
        {
            var listUser = from transaction in _mstSleFundTransactionRepo.GetAll().Where(e => e.FundId == fundId)
                           join user in _mstSleUserRepo.GetAll() on transaction.UserId equals user.Id
                           select new ListUserDonateForFundDto
                           {
                               UserNameDonate = user.Surname + user.Name,
                               AmountOfMoney = transaction.AmountOfMoney,
                               CreatedDonate = transaction.CreationTime
                           };
            return listUser.ToList();
        }
        public List<ListUserDonateForFundDto> GetUserDonateForFund(int fundId, string userName)
        {
            var listUser = from transaction in _mstSleFundTransactionRepo.GetAll().Where(e => e.FundId == fundId)
                           join user in _mstSleUserRepo.GetAll().Where(e => userName == null || (e.Surname + e.Name).Contains(userName))
                           on transaction.UserId equals user.Id
                           select new ListUserDonateForFundDto
                           {
                               UserNameDonate = user.Surname + user.Name,
                               AmountOfMoney = transaction.AmountOfMoney,
                               CreatedDonate = transaction.CreationTime
                           };
            return listUser.ToList();
        }
        public void UpdatePermissionForFundRaiser(int userId)
        {
            using (SqlConnection connection = new SqlConnection(_appConfiguration.GetConnectionString("Default")))
            {

                connection.Execute(@"
                            INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name, TenantId ,RoleId,UserId) VALUES
                            (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages.FundRaising',1,4,@p_userId)
                        ", new
                {
                    p_userId = userId
                });

                connection.Execute(@"
                            INSERT INTO dbo.AbpPermissions (CreationTime, CreatorUserId, Discriminator, IsGranted, Name, TenantId,RoleId ,UserId) VALUES
                            (GetDate(),@p_userId, 'UserPermissionSetting',1,'Pages.FundRaising',1,4,@p_userId)
                        ", new
                {
                    p_userId = userId
                });

            }
        }
        public async Task<RegisterInforFundRaiserDto> GetForEditFundRaiser()
        {
            try
            {
                var request = await _mstRequestToFundRaiserRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                if (request == null)
                {
                    return null;
                }
                var fundRaiser = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId);
                if (fundRaiser?.Id != null)
                {
                    var fundRaiserEdit = new RegisterInforFundRaiserDto();
                    ObjectMapper.Map(fundRaiser, fundRaiserEdit);
                    fundRaiserEdit.OrgnizationIntro = fundRaiser.IntroduceOrganization;
                    fundRaiserEdit.Orgnization = fundRaiser.Company;
                    fundRaiserEdit.IsRequest = request.Id != null ? true : false;
                    return fundRaiserEdit;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<List<ListComboboxDto>> getListFundPackageCombobox()
        {
            var listFundPackage = (from package in _mstSleFundPackageRepo.GetAll().Where(e => e.Status == true)
                                   select new ListComboboxDto
                                   {
                                       Id = package.Id,
                                       Name = package.Commission.ToString() + "%/" + package.Duration
                                   }).ToListAsync();
            return await listFundPackage;

        }
        public async Task<InforDetailBankAcountDto> GetInforBankUser()
        {
            try
            {
                var bankInfor = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
                if (bankInfor != null)
                {
                    var bank = new InforDetailBankAcountDto();
                    ObjectMapper.Map(bankInfor, bank);
                    return bank;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        public async Task<RegisterInforFundRaiserDto> RegisterFundRaiser(RegisterInforFundRaiserDto input)
        {
            var bankAccount = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
            if (bankAccount == null)
            {
                throw new UserFriendlyException("Bạn chưa đăng ký tài khoản ngân hàng của hệ thống!");
            }
            var user = _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId);
            user.Address = input.Address;
            user.IntroduceOrganization = input.OrgnizationIntro;
            user.Email = input.Email;
            user.Company = input.Orgnization;
            user.Phone = input.Phone;
            var balanceAccount = _mstBankRepo.FirstOrDefault(e => e.UserId == AbpSession.UserId).Balance;
            var paymentFee = _mstSleFundPackageRepo.FirstOrDefault(e => e.Id == input.FundPackageId).PaymentFee;

            if (balanceAccount < paymentFee)
            {
                throw new UserFriendlyException("Tài khoản của bạn không đủ để đăng ký gây quỹ!");
            }
            //user.TypeUser = 3;
            RequestToFundRaiser request = new RequestToFundRaiser();
            request.UserId = AbpSession.UserId;
            request.RequestTime = DateTime.Now;
            request.IsApprove = false;
            request.FundPackageId = input.FundPackageId;
            await _mstRequestToFundRaiserRepo.InsertAsync(request);
            // Trừ tiền tài khoản
            //bankAccount.Balance -= paymentFee;
            //await _mstBankRepo.UpdateAsync(bankAccount);

            await _mstSleUserRepo.UpdateAsync(user);
            RegisterInforFundRaiserDto userResult = new RegisterInforFundRaiserDto();
            userResult.Phone = input.Phone;
            userResult.Email = input.Email;
            userResult.FundPackageId = input.FundPackageId;
            userResult.OrgnizationIntro = input.OrgnizationIntro;
            userResult.Address = input.Address;
            userResult.Orgnization = input.Orgnization;
            input.Id = user.Id;
            return userResult;
        }

        public async Task<BankAccount> CreateOrEditAccountBank(InforDetailBankAcountDto input)
        {
            if (input.Id == 0 || input.Id == null)
            {
                var accountExist = await _mstBankRepo.FirstOrDefaultAsync(e => e.BankNumber == input.BankNumber && e.AccountName == input.AccountName);
                if (accountExist != null)
                {
                    throw new UserFriendlyException("Tài khoản đã tồn tại trong hệ thống");
                }
                var bank = new BankAccount();
                ObjectMapper.Map(input, bank);
                bank.UserId = AbpSession.UserId;
                bank.Balance = 1000000;
                bank.Unit = "VND";
                bank.Id = await _mstBankRepo.InsertAndGetIdAsync(bank);
                return bank;
            }
            return null;
            //else
            //{
            //    var bankAccount = await _mstBankRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
            //    ObjectMapper.Map(input, bankAccount);
            //}
        }

        public async Task<List<GetFundRaisingViewForAdminDto>> GetListPostOfFundRaising()
        {
            var listPost = (from post in _mstFundRaiserPostRepo.GetAll().Where(e => e.Status == 1)
                            //.Where(e=> input.FilterText == null || e.PostTitle.Contains(input.FilterText))
                            join fund in _mstSleFundRepo.GetAll().Where(e => e.Status == 2)
                            on post.FundId equals fund.Id
                            select new GetFundRaisingViewForAdminDto
                            {
                                Id = post.Id,
                                FundId = fund.Id,
                                FundName = fund.FundName,
                                PostTitle = post.PostTitle,
                                AmountDonatePresent = fund.AmountDonationPresent,
                                AmountDonateTarget = fund.AmountDonationTarget,
                                PercentAchieved = (fund.AmountDonationPresent / fund.AmountDonationTarget) * 100,
                                PostTopic = post.PostTopic,
                                ListImageUrl = _mstFundImageRepo.GetAll().Where(e => e.PostId == post.Id).Select(re => re.ImageUrl).ToList()
                            }).ToListAsync();
            return await listPost;
        }
        public async Task<List<TransactionOfFundForDto>> getListTransactionForFund(int fundId)
        {
            var listTransaction = (from transaction in _mstSleFundTransactionRepo.GetAll().Where(e => e.FundId == fundId &&
                                   (e.IsAdmin == false || e.IsAdmin == null))
                                   join user in _mstSleUserRepo.GetAll() on transaction.SenderId equals user.Id
                                   select new TransactionOfFundForDto
                                   {
                                       //Id = transaction.Id,
                                       UserDonate = user.Surname + " " + user.Name,
                                       Amount = transaction.AmountOfMoney,
                                       //Content = transaction.MessageToFund,
                                       //FundName = fund.FundName,
                                       CreatedTime = transaction.CreationTime.ToString("dd/MM/yyyy hh:mm"),
                                       IsPublic = transaction.IsPublic

                                   }).ToListAsync();
            return await listTransaction;
        }
        public async Task<List<HistoryOfAuctionItemDto>> getListHistoryForAuctionItem(long auctionItemId)
        {
            var listTransaction = (from history in _mstAuctionTransactionRepo.GetAll().Where(e => e.AuctionItemId == auctionItemId)
                                   join user in _mstSleUserRepo.GetAll() on history.AuctioneerId equals user.Id
                                   select new HistoryOfAuctionItemDto
                                   {
                                       Id = history.Id,
                                       UserAuction = user.Surname + " " + user.Name,
                                       AuctionDate = history.AuctionDate.Value.ToString("dd/MM/yyyy hh:mm"),
                                       IsPublic = history.IsPublic,
                                       AmountOfMoney = history.NewAmount
                                   }).OrderByDescending(e=>e.AmountOfMoney).ToListAsync();
            return await listTransaction;
        }
        //public async Task UserAuction(UserAuction input)
        //{
        //    var auctionItem = _mstAuctionItemsRepo.FirstOrDefault(e => e.Id == input.AuctionItemId);
        //    var auctionTransaction = new AuctionHistory();
        //    auctionTransaction.OldAmount = auctionItem.AuctionPresentAmount != null ? auctionItem.AuctionPresentAmount : auctionItem.StartingPrice;
        //    if (input.AmountAuction < auctionItem.AuctionPresentAmount || input.AmountAuction > auctionItem.AuctionPresentAmount + auctionItem.AmountJumpMax)
        //    {
        //        throw new UserFriendlyException("Mức đấu thầu không hợp lệ");
        //    }
        //    if (auctionItem != null)
        //    {
        //        auctionItem.AuctionPresentAmount = input.AmountAuction;
        //    }

        //    await _mstAuctionItemsRepo.UpdateAsync(auctionItem);
        //    auctionTransaction.NewAmount = auctionItem.AuctionPresentAmount;
        //    auctionTransaction.AuctionDate = DateTime.Now;
        //    auctionTransaction.AuctioneerId = AbpSession.UserId;
        //    auctionTransaction.IsPublic = input.IsPublic;
        //    await _mstAuctionTransactionRepo.InsertAsync(auctionTransaction);
        //}
        public async Task UserDepositAuction(float deposit, long auctionItemId)
        {
            var bankAccount = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
            if (bankAccount == null)
            {
                throw new UserFriendlyException("Bạn chưa đăng ký tài khoản ngân hàng của hệ thống");
            }
            var auctionItem = _mstAuctionItemsRepo.FirstOrDefault(e => e.Id == auctionItemId);

            if (auctionItem.NumberOfParticipants == auctionItem.LimitedPersionJoin)
            {
                throw new UserFriendlyException("Phiên đấu giá đã vượt quá số người hợp lệ");
            }
            if (deposit < auctionItem.StartingPrice / 100 || deposit > (auctionItem.StartingPrice * 15) / 100)
            {
                throw new UserFriendlyException("Số tiền đặt cọc không hợp lệ");
            }
            auctionItem.NumberOfParticipants += 1;
            await _mstAuctionItemsRepo.UpdateAsync(auctionItem);
            var auctionDeposit = new AuctionDeposit();
            auctionDeposit.UserId = AbpSession.UserId;
            auctionDeposit.AuctionItemId = auctionItemId;
            auctionDeposit.DepositAmount = deposit;
            auctionDeposit.DepositDate = DateTime.Now;
            await _mstAuctionDepositRepo.InsertAsync(auctionDeposit);

            var bankAccountUser = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == auctionItem.UserId);
            bankAccountUser.Balance += deposit;
            await _mstBankRepo.UpdateAsync(bankAccountUser);

            var bankAccountUserAuction = await _mstBankRepo.FirstOrDefaultAsync(e => e.UserId == AbpSession.UserId);
            bankAccountUserAuction.Balance -= deposit;
            await _mstBankRepo.UpdateAsync(bankAccountUserAuction);

            AuctionTransactionDeposit fundTransaction = new AuctionTransactionDeposit();
            fundTransaction.SenderId = AbpSession.UserId;
            fundTransaction.ReceiverId = auctionItem.UserId;
            fundTransaction.AuctionItemId = auctionItemId;
            fundTransaction.AmountOfMoney = deposit;
            fundTransaction.MessageContent = "Đặt cọc đấu giá";
            await _mstAuctionTransactionDeposit.InsertAsync(fundTransaction);
        }
        public async Task<InformationAuctionDepositDto> GetInforAuctionDeposit(long auctionItemId)
        {
            var auctionItem = (from auctionItems in _mstAuctionItemsRepo.GetAll().Where(e => e.Id == auctionItemId)
                               select new InformationAuctionDepositDto
                               {
                                   AuctionTitle = auctionItems.TitleAuction,
                                   MinAmountDepost = auctionItems.StartingPrice / 100,
                                   MaxAmountDepost = auctionItems.StartingPrice * 15 / 100
                               }).FirstOrDefault();
            return auctionItem;
        }
        public bool CheckUserIsFundRaiser()
        {
            var user = _mstUserFundPackageRepo.FirstOrDefault(e => e.UserId == AbpSession.UserId);
            if (user != null && user?.IsExpired == false)
            {
                return true;
            }
            return false;
        }
        public bool CheckUserRegisterBankAccount()
        {
            var bankAccountUser = _mstBankRepo.FirstOrDefault(e => e.UserId == AbpSession.UserId);
            if (bankAccountUser == null)
            {
                throw new UserFriendlyException("Bạn chưa đăng ký tài khoản ngân hàng của hệ thống");
            };
            return true;
        }
        public async Task<InformationWebDto> getInforWeb()
        {
            var inforResult = new InformationWebDto();
            inforResult.Project = _mstSleFundRepo.GetAll().Count();
            inforResult.FundRaiser = _mstSleUserRepo.GetAll().Where(e => e.TypeUser == 3).Count();
            inforResult.AmountDonate = _mstSleFundTransactionRepo.GetAll().Where(e => e.IsAdmin == false || e.IsAdmin == null).Count();
            var listTransaction = _mstSleFundTransactionRepo.GetAll().Where(e => e.IsAdmin == false || e.IsAdmin == null).ToList();
            inforResult.AmountOfMoneyDonate = 0;
            foreach (var transaction in listTransaction)
            {
                inforResult.AmountOfMoneyDonate += transaction.AmountOfMoney;
            }
            return inforResult;
        }
        public async Task<List<GetListFundRasingDto>> GetAllFundRaisingIsClose()
        {
            var listFundOutStanding = (from post in _mstFundRaiserPostRepo.GetAll()
                                       join fund in _mstSleFundRepo.GetAll().Where(e => e.Status == 2) on post.FundId equals fund.Id
                                       join postImage in _mstFundImageRepo.GetAll() on post.Id equals postImage.PostId
                                       join user in _mstSleUserRepo.GetAll() on fund.UserId equals user.Id
                                       select new GetListFundRasingDto
                                       {
                                           Id = post.Id,
                                           ListImageUrl = _mstFundImageRepo.GetAll().Where(e => e.PostId == post.Id).Select(re => re.ImageUrl).ToList(),
                                           AmountDonatePresent = fund.AmountDonationPresent,
                                           PercentAchieved = (fund.AmountDonationPresent / fund.AmountDonationTarget) * 100,
                                           PostTitle = post.PostTitle,
                                           AmountDonateTarget = fund.AmountDonationTarget,
                                           PostTopic = post.PostTopic,
                                           OrganizationName = user.Company
                                       }).ToListAsync();
            return await listFundOutStanding;
        }
        public async Task<List<GetListFundRasingDto>> GetAllFundRaisingIsActive()
        {
            var listFundOutStanding = (from post in _mstFundRaiserPostRepo.GetAll()
                                       join fund in _mstSleFundRepo.GetAll().Where(e => e.Status == 1) on post.FundId equals fund.Id
                                       join postImage in _mstFundImageRepo.GetAll() on post.Id equals postImage.PostId
                                       join user in _mstSleUserRepo.GetAll() on fund.UserId equals user.Id
                                       select new GetListFundRasingDto
                                       {
                                           Id = post.Id,
                                           ListImageUrl = _mstFundImageRepo.GetAll().Where(e => e.PostId == post.Id).Select(re => re.ImageUrl).ToList(),
                                           AmountDonatePresent = fund.AmountDonationPresent,
                                           PercentAchieved = (fund.AmountDonationPresent / fund.AmountDonationTarget) * 100,
                                           PostTitle = post.PostTitle,
                                           AmountDonateTarget = fund.AmountDonationTarget,
                                           PostTopic = post.PostTopic,
                                           OrganizationName = user.Company,
                                       }).ToListAsync();
            return await listFundOutStanding;
        }
    }
}
