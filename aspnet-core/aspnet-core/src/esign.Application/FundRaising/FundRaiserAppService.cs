using Abp.Domain.Repositories;
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
using Microsoft.AspNetCore.Http;
using System.IO;
using esign.FundRaising.Admin.Dto;
using esign.Enitity;
using Microsoft.AspNetCore.Mvc;
using esign.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Dapper;
using esign.FundRaising.UserFundRaising.Dto.Auction;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using Microsoft.AspNetCore.SignalR;
using Abp.RealTime;

namespace esign.FundRaising
{
    public class FundRaiserAppService : esignAppServiceBase, IFundRaiser
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        private readonly IRepository<FundTransactions, int> _mstSleFundTransactionRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundDetailContent, long> _mstSleDetailConentRepo;
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;
        private readonly IRepository<Auction, long> _mstAuctionRepo;
        private readonly IRepository<AuctionTransactions, long> _mstAuctionTransactionRepo;
        private readonly IRepository<AuctionImages, long> _mstAuctionImagesRepo;
        ///private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<FundImage, long> _mstSleFundImageRepo;
        private readonly IConfigurationRoot _appConfiguration;
        public FundRaiserAppService(IRepository<Funds, long> mstSleFundRepo,
            IRepository<FundTransactions, int> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo,
            IWebHostEnvironment hostingEnvironment,
            IWebHostEnvironment env,
            IRepository<FundDetailContent, long> mstSleDetailConentRepo,
            IRepository<UserWarning, int> mstSleUserWarningRepo,
            //IRepository<FundRaiser, long> mstSleFundRaiserRepo,
            IRepository<FundImage, long> mstSleFundImageRepo,
            IRepository<Auction, long> mstAuctionRepo,
            IRepository<AuctionTransactions, long> mstAuctionTransactionRepo,
            IRepository<AuctionImages, long> mstAuctionImagesRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleDetailConentRepo = mstSleDetailConentRepo;
            _mstSleUserWarningRepo = mstSleUserWarningRepo;
            //_mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundImageRepo = mstSleFundImageRepo;
            _appConfiguration = env.GetAppConfiguration();
            _appConfiguration = hostingEnvironment.GetAppConfiguration();
            _mstAuctionRepo = mstAuctionRepo;
            _mstAuctionTransactionRepo = mstAuctionTransactionRepo;
            _mstAuctionImagesRepo = mstAuctionImagesRepo;
        }
        public async Task CloseFundRaising(int fundId)
        {
            try
            {
                var fund = await _mstSleFundRepo.FirstOrDefaultAsync(s => s.Id == fundId);
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
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task CreateFundRaising([FromForm] CreateOrEditFundRaisingInputDto input)
        {
            try
            {
                Funds fundInsert = new Funds();
                fundInsert.FundRaiserId = AbpSession.UserId;
                fundInsert.FundRaisingDay = input.FundStartDate;
                fundInsert.FundEndDate = input.FundEndDate;
                fundInsert.FundName = input.FundName;
                fundInsert.FundTitle = input.FundTitle;
                fundInsert.AmountOfMoney = input.AmountOfMoney;
                fundInsert.Status = 1;
                fundInsert.IsPayFee = input.IsPayFee;
                var fundId = await _mstSleFundRepo.InsertAndGetIdAsync(fundInsert);
                FundDetailContent fundDetail = new FundDetailContent();
                fundDetail.FundId = (int)fundId;
                fundDetail.Content = input.FundContent;
                fundDetail.ReasonCreatedFund = input.ReasonCreateFund;
                await _mstSleDetailConentRepo.InsertAsync(fundDetail);
                if (input.File.Count() > 0)
                {
                    foreach (var file in input.File)
                    {
                        if (file != null)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            FundImage fundImage = new FundImage();
                            fundImage.FundId = fundId;

                            fundImage.ImageUrl = Path.Combine("uploads", fileName);
                            await _mstSleFundImageRepo.InsertAsync(fundImage);
                        }
                    }
                }
                else
                {
                    throw new UserFriendlyException("Please select a file");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new UserFriendlyException("Error uploading file");
            }
        }
        //public async Task<CreateOrEditFundRaisingInputDto> getFundRaisingForEdit(long fundId)
        //{
        //    var fundRaising = _mstSleFundRepo.FirstOrDefault(e => e.Id == fundId);
        //    CreateOrEditFundRaisingInputDto fundResult = new CreateOrEditFundRaisingInputDto();
        //    ObjectMapper.Map(fundRaising, fundResult);
        //    fundResult.List
        //}

        public async Task UpdateFundRaising(CreateOrEditFundRaisingDto input)
        {

            try
            {
                var fund = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                if (fund != null)
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
                    detailContent.FundId = (int)fund.Id;
                    await _mstSleDetailConentRepo.UpdateAsync(detailContent);
                }
            }
            catch (Exception e) { }
        }

        public async Task ExtendTimeOfFundRaising(DateTime timeExtend, int fundId)
        {
            var fund = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == fundId);
            if (fund != null)
            {
                fund.FundEndDate = timeExtend;
                fund.Status = 2;
                await _mstSleFundRepo.UpdateAsync(fund);
            }
        }

        public async Task<List<TransactionOfFundForDto>> getListTransactionForFund(int fundId)
        {
            var listTransaction = (from transaction in _mstSleFundTransactionRepo.GetAll().Where(e => e.FundId == fundId &&
                                   (e.IsAdmin == false || e.IsAdmin == null))
                                   join fund in _mstSleFundRepo.GetAll() on transaction.FundId equals fund.Id
                                   join user in _mstSleUserRepo.GetAll() on fund.FundRaiserId equals user.Id
                                   select new TransactionOfFundForDto
                                   {
                                       Id = transaction.Id,
                                       Sender = user.Name,
                                       Amount = transaction.AmountOfMoney,
                                       Content = transaction.MessageToFund,
                                       FundName = fund.FundName,
                                       CreatedTime = transaction.CreationTime,
                                       IsPublic = transaction.IsPublic
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

        /// Auction

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task CreateOrEditAuction([FromForm] CreateOrEditAuctionInputDto input)
        {
            try
            {
                if (input.Id == null || input.Id == 0)
                {
                    Auction auction = new Auction();
                    ObjectMapper.Map(input, auction);
                    auction.UserId = AbpSession.UserId;
                    var auctionId = await _mstAuctionRepo.InsertAndGetIdAsync(auction);
                    if (input.File.Count() > 0)
                    {
                        foreach (var file in input.File)
                        {
                            if (file != null)
                            {
                                var fileName = Path.GetFileName(file.FileName);
                                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(stream);
                                }
                                AuctionImages auctionImage = new AuctionImages();
                                auctionImage.AuctionId = auctionId;

                                auctionImage.ImageUrl = Path.Combine("uploads", fileName);
                                await _mstAuctionImagesRepo.InsertAsync(auctionImage);
                            }
                        }
                    }
                }
                else
                {

                    //Để sau
                    var auction = await _mstAuctionRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                    ObjectMapper.Map(input, auction);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi trong quá trình tạo phiên đấu giá");
            }
        }
        public async Task UserAuction(UserAuction input)
        {
            var auction = _mstAuctionRepo.FirstOrDefault(e => e.Id == input.AuctionId);
            var auctionTransaction = new AuctionTransactions();
            auctionTransaction.OldAmount = auction.AuctionPresentAmount != null ? auction.AuctionPresentAmount : auction.StartingPrice;
            if(input.AmountAuction < auction.AuctionPresentAmount || input.AmountAuction > auction.AuctionPresentAmount + auction.AmountJumpMax)
            {
                throw new UserFriendlyException("Mức đấu thầu không hợp lệ");
            }
            if (auction != null)
            {
                auction.AuctionPresentAmount = input.AmountAuction;
            }

            await _mstAuctionRepo.UpdateAsync(auction);
            auctionTransaction.NewAmount = auction.AuctionPresentAmount;
            auctionTransaction.AuctionId = input.AuctionId;
            auctionTransaction.AuctionDate = DateTime.Now;
            auctionTransaction.AuctioneerId = AbpSession.UserId;
            auctionTransaction.IsPublic = input.IsPublic;
            await _mstAuctionTransactionRepo.InsertAsync(auctionTransaction);
        }
        public async Task<List<GetListTransactionForAuctionDto>> GetListTransactionAuction(long auctionId)
        {
            var listAuction = (from transactionAuction in _mstAuctionTransactionRepo.GetAll().Where(e => e.AuctionId == auctionId)
                               join user in _mstSleUserRepo.GetAll() on transactionAuction.AuctioneerId equals user.Id
                               select new GetListTransactionForAuctionDto
                               {
                                   Id = transactionAuction.Id,
                                   AuctionerName = user.UserName,
                                   AmountAuctionNew = transactionAuction.NewAmount,
                                   AmountAuctionOld = transactionAuction.OldAmount,
                                   AuctionDate = transactionAuction.AuctionDate,
                                   IsPublic = transactionAuction.IsPublic,
                               }).ToList();
            return listAuction;
        }
        public async Task<PagedResultDto<GetAllAuctionDto>> getAllAuctionAdmin(AuctionInputDto input)
        {
            var query = from auction in _mstAuctionRepo.GetAll().Where(e => AbpSession.TenantId == null || e.UserId == AbpSession.UserId)
                        select new GetAllAuctionDto
                        {
                            Id = auction.Id,
                            ItemName = auction.ItemName,
                            TitleAuction = auction.TitleAuction,
                            AmountJumpMax = auction.AmountJumpMax,
                            AmountJumpMin = auction.AmountJumpMin,
                            EndDate = auction.EndDate,
                            StartingPrice = auction.StartingPrice,
                            StartDate = (DateTime)auction.StartDate,
                            IntroduceItem = auction.IntroduceItem,
                        };

            var totalCount = await query.CountAsync();
            var result = await query.PageBy(input).ToListAsync();

            return new PagedResultDto<GetAllAuctionDto>(
                totalCount,
                result);
        }
        public async Task DeleteAuction(long auctionId)
        {
            var auction = await _mstAuctionRepo.FirstOrDefaultAsync(e=>e.Id == auctionId);
            if(auction != null)
            {
                if (DateTime.Now > auction.EndDate)
                {
                    await _mstAuctionRepo.DeleteAsync(auctionId);
                }
                else throw new Exception("Phiên đấu giá này chưa hết hạn");
            }
            else
            {
                throw new Exception("Không tồn tại phiên đấu giá");
            }
        }
        public List<GetAllAuctionDto> getAllAuctionUser()
        {
            var listAuction = (from auction in _mstAuctionRepo.GetAll().Where(e => AbpSession.TenantId == null
                               || e.UserId != AbpSession.UserId)
                               join image in _mstAuctionImagesRepo.GetAll()
                               on auction.Id equals image.AuctionId
                               select new GetAllAuctionDto
                               {
                                   Id = auction.Id,
                                   ItemName = auction.ItemName,
                                   TitleAuction = auction.TitleAuction,
                                   AmountJumpMax = auction.AmountJumpMax,
                                   AmountJumpMin = auction.AmountJumpMin,
                                   EndDate = auction.EndDate,
                                   StartingPrice = auction.StartingPrice,
                                   StartDate = (DateTime)auction.StartDate,
                                   IntroduceItem = auction.IntroduceItem,
                                   ListImage = _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionId == auction.Id).Select(re => re.ImageUrl).ToList()
                               }).ToList();


            return listAuction;
        }
        public GetAllAuctionDto getAuctionById(long? auctionId)
        {
            var auction =_mstAuctionRepo.FirstOrDefault(e => e.Id == auctionId);
            var auctionResult = new GetAllAuctionDto();
            ObjectMapper.Map(auction, auctionResult);
            auctionResult.ListImage = _mstAuctionImagesRepo.GetAll().Where(e=>e.AuctionId == auctionId).Select(re=>re.ImageUrl).ToList();
            auctionResult.NextMaximumBid = auctionResult.AuctionPresentAmount + auction.AmountJumpMax;
            auctionResult.NextMinimumBid = auctionResult.AuctionPresentAmount + auction.AmountJumpMin;
            TimeSpan timeSpan = DateTime.Now - auctionResult.StartDate;
            auctionResult.TimeLeft = (int?)timeSpan.TotalDays;
            auctionResult.UserName = _mstSleUserRepo.FirstOrDefault(e=>e.Id == auction.UserId).UserName;
            return auctionResult;
        }
    }
}
