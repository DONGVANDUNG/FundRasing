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

namespace esign.FundRaising
{
    public class UserFundRaisingAppService : esignAppServiceBase, IUsersFundRaisingAppService
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        //private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<FundDetails, long> _mstSleFundDetailRepo;
        private readonly IRepository<FundRaisingTopic, int> _mstSleFundTopictRepo;
        private readonly IRepository<FundPackage, int> _mstSleFundPackageRepo;
        private readonly IRepository<FundTransactions, long> _mstSleFundTransactionRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundImage, long> _mstSleFundImageRepo;
        private readonly IRepository<BankAccount, long> _mstBankRepo;
        private readonly IRepository<RequestToFundRaiser, long> _mstRequestToFundRaiserRepo;
        private readonly IRepository<FundRaiserPost, long> _mstFundRaiserPostRepo;
        private readonly IRepository<FundImage, long> _mstFundImageRepo;
        private readonly IRepository<Auction, long> _mstAuctionRepo;
        private readonly IRepository<AuctionTransactions, long> _mstAuctionTransactionRepo;
        private readonly IRepository<AuctionItems, long> _mstAuctionItemsRepo;
        private readonly IRepository<AuctionDeposit, long> _mstAuctionDepositRepo;
        private readonly IConfigurationRoot _appConfiguration;
        private readonly ISendEmail _sendEmail;


        public UserFundRaisingAppService(IRepository<Funds, long> mstSleFundRepo,
            //IRepository<FundRaiser, long>
            // mstSleFundRaiserRepo,
            IRepository<FundDetails, long> mstSleFundDetailRepo,
            IWebHostEnvironment hostingEnvironment, IWebHostEnvironment env,
            IRepository<FundRaisingTopic, int> mstSleFundTopictRepo,
            IRepository<FundPackage, int> mstSleFundPackageRepo,
            IRepository<FundTransactions, long> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo, IRepository<FundImage, long> mstSleFundImageRepo, IRepository<BankAccount, long> mstBankRepo, IRepository<FundRaiserPost, long> mstFundRaiserPostRepo, IRepository<FundImage, long> mstFundImageRepo, IRepository<RequestToFundRaiser, long> mstRequestToFundRaiserRepo, IRepository<Auction, long> mstAuctionRepo, IRepository<AuctionTransactions, long> mstAuctionTransactionRepo, IRepository<AuctionItems, long> mstAuctionItemsRepo, IRepository<AuctionDeposit, long> mstAuctionDepositRepo, ISendEmail sendEmail)
        {
            _mstSleFundRepo = mstSleFundRepo;
            /// _mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundDetailRepo = mstSleFundDetailRepo;
            _mstSleFundTopictRepo = mstSleFundTopictRepo;
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
        }
        public async Task<List<GetListFundRasingDto>> getHistoryDonationForFund()
        {
            var listFund = from transaction in _mstSleFundTransactionRepo.GetAll().Where(e=>e.UserId == AbpSession.UserId)
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
            var fundRaising = (from post in _mstFundRaiserPostRepo.GetAll().Where(e => e.IsClose == false && e.Id == Id)
                               join fund in _mstSleFundRepo.GetAll() on (long)post.FundId equals fund.Id
                               join fundDetail in _mstSleFundDetailRepo.GetAll() on fund.Id equals (long)fundDetail.FundId

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
                                   Purpose = fundDetail.Purpose,
                                   Note = fundDetail.Note,
                                   Target = fundDetail.Target,
                                   FundName = fund.FundName,
                                   OrganizationIntroduce = user.IntroduceOrganization,
                                   AddressOrgnization = user.Address,
                                   Phone = user.Phone,
                                   Email = user.EmailAddress,
                                   FundId = fund.Id,
                                   DonateAmount = fund.DonateAmount
                               }).FirstOrDefault();
            return fundRaising;
        }
        public async Task<List<GetListFundRasingDto>> GetAllFundRaising()
        {
            var listFundOutStanding = (from post in _mstFundRaiserPostRepo.GetAll().Where(e => e.IsClose == false)
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


        public async Task<List<GetListFundPackageDto>> GetListFundPackage(FundPackageInputDto input)
        {
            var listFundPackage = from funPackage in _mstSleFundPackageRepo.GetAll().Where(e => e.Status == true)
                                  select new GetListFundPackageDto
                                  {
                                      Id = funPackage.Id,
                                      //Discount = funPackage.Discount,
                                      Commission = funPackage.Commission.ToString() + "%/giao dịch",
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
                throw new UserFriendlyException("Bạn chưa đăng ký tài khoản ngân hàng");
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
            var fundPackageId = _mstSleUserRepo.FirstOrDefault(e => e.Id == userCreatedFundId.UserId).FundPackageId;
            var commission = _mstSleFundPackageRepo.FirstOrDefault(e => e.Id == fundPackageId).Commission;

            var fundRaiserId = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.FundId).UserId;
            var fundRaiserReceive = _mstSleUserRepo.FirstOrDefault(e => e.Id == fundRaiserId).UserName;
            var userSend = _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId).UserName;

            var amountOfCommission = input.AmountOfMoney * commission / 100;
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
            bankAdmin.Balance += input.AmountOfMoney * (commission / 100);
            await _mstBankRepo.UpdateAsync(accountUserRaiseFund);

            var transactionAdmin = new FundTransactions();
            transactionAdmin.FundId = input.FundId;
            transactionAdmin.AmountOfMoney = input.AmountOfMoney * (commission / 100);
            transactionAdmin.MessageToFund = "Trừ tiền phí gói quỹ";
            transactionAdmin.Commission = input.AmountOfMoney * commission / 100;
            transactionAdmin.Receiver = "Admin";
            transactionAdmin.Sender = userSend;
            transactionAdmin.SenderId = fundRaiserId;
            transactionAdmin.Balance = bankAdmin.Balance;
            transactionAdmin.ReceiverId = 1;
            transactionAdmin.IsAdmin = true;

            await _mstSleFundTransactionRepo.InsertAsync(transactionAdmin);
            var userDonateCurrent = await _mstSleUserRepo.FirstOrDefaultAsync(e => e.Id == AbpSession.UserId);
            SendEmailInputDto sendEmailInput = new SendEmailInputDto
            {
                EmailReceive = userDonateCurrent.EmailAddress,
                //Body = "Xin chúc mừng bạn đã trở thành người gây quỹ trên hệ thống của chúng tôi, bạn có thể bắt đầu ngay vào việc gây quỹ.",
                Body = "<p style='font-weight:bold;font-size:18px'>Hệ thống gây quỹ trực tuyến FundRaising.</p>" +
                          "<p>Cảm ơn bạn đã đóng góp " + input.AmountOfMoney +" VND " + "cho quỹ "+ userCreatedFundId.FundName + " vào lúc "+DateTime.Now+ "</p>"+
                          "<p>Chúng tôi cam kết sẽ sử dụng số tiền của bạn để hỗ trợ những hoàn cảnh khó khăn một cách công khai, kịp thời, hiệu quả!</p>"+
                          "<i style='color:red'>Cảm ơn bạn đã tin tưởng vào sử dụng hệ thống.Chúng tôi cam kết tạo ra một môi trường gây quỹ công bằng, hợp pháp." +
                          "Chúc cho những dự án của bạn sẽ hoàn thành tốt đẹp giúp đỡ cho những hoàn cảnh khó khăn kịp thời, nâng cao chất lượng xã hội.</i>",
                Subject = "Thông báo trở thành người gây quỹ",
            };
            _sendEmail.SendEmail(sendEmailInput);
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
            var bankAccount = await _mstBankRepo.FirstOrDefaultAsync(e=>e.UserId == AbpSession.UserId);
            if(bankAccount == null)
            {
                throw new UserFriendlyException("Bạn chưa đăng ký tài khoản ngân hàng của hệ thống!");
            }
            var user = _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId);
            user.Address = input.Address;
            user.IntroduceOrganization = input.OrgnizationIntro;
            user.Email = input.Email;
            user.Company = input.Orgnization;
            user.Phone = input.Phone;
            user.FundPackageId = 1;
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

        public async Task<List<GetFundRaisingViewForAdminDto>> getListPostOfFundRaising()
        {
            var listPost = (from post in _mstFundRaiserPostRepo.GetAll()
                            join fund in _mstSleFundRepo.GetAll().Where(e=> DateTime.Now >= e.FundRaisingDay && DateTime.Now <=e.FundEndDate)
                            on post.FundId equals fund.Id
                            join user in _mstSleUserRepo.GetAll() on fund.UserId equals user.Id
                            select new GetFundRaisingViewForAdminDto
                            {
                                Id = post.Id,
                                FundId = fund.Id,
                                OrganizationName = user.Company,
                                PostTitle = post.PostTitle,
                                AmountDonatePresent = fund.AmountDonationPresent,
                                AmountDonateTarget = fund.AmountDonationTarget,
                                PercentAchieved = fund.PercentAchieved,
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
                                       CreatedTime = transaction.CreationTime.ToString("dd/MM/yyyy"),
                                       IsPublic = transaction.IsPublic

                                   }).ToListAsync();
            return await listTransaction;
        }
        public async Task UserAuction(UserAuction input)
        {
            var auctionItem = _mstAuctionItemsRepo.FirstOrDefault(e => e.Id == input.AuctionItemId);
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
            auctionTransaction.AuctionDate = DateTime.Now;
            auctionTransaction.AuctioneerId = AbpSession.UserId;
            auctionTransaction.IsPublic = input.IsPublic;
            await _mstAuctionTransactionRepo.InsertAsync(auctionTransaction);
        }
        public async Task UserDepositAuction(float deposit,long auctionItemId)
        {
            var auctionStartingPrice = _mstAuctionItemsRepo.FirstOrDefault(e => e.Id == auctionItemId).StartingPrice;
            if(deposit < auctionStartingPrice/100 || deposit > (auctionStartingPrice*15)/100) {
                throw new UserFriendlyException("Số tiền đặt cọc không hợp lệ");           
            }
            var auctionDeposit = new AuctionDeposit();
            auctionDeposit.UserId = AbpSession.UserId;
            auctionDeposit.AuctionItemId= auctionItemId;
            auctionDeposit.DepositAmount = deposit;
            auctionDeposit.DepositDate = DateTime.Now;
            await _mstAuctionDepositRepo.InsertAsync(auctionDeposit);
        }
        public async Task<InformationAuctionDepositDto> GetInforAuctionDeposit(long auctionItemId)
        {
            var auctionItem = (from auctionItems in _mstAuctionItemsRepo.GetAll().Where(e=>e.Id == auctionItemId)
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
            var user =  _mstSleUserRepo.FirstOrDefault(e => e.Id == AbpSession.UserId);
            if(user.FundPackageId != null)
            {
                return true;
            }
            return false;
        }
    }
}
