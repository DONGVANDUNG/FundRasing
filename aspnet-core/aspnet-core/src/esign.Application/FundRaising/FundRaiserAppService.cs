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
using NPOI.HSSF.Record;
using esign.FundRaising.UserFundRaising.Dto;

namespace esign.FundRaising
{
    public class FundRaiserAppService : esignAppServiceBase, IFundRaiser
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        private readonly IRepository<FundTransactions, long> _mstSleFundTransactionRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<FundDetails, long> _mstSleDetailsRepo;
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;
        private readonly IRepository<Auction, long> _mstAuctionRepo;
        private readonly IRepository<AuctionTransactions, long> _mstAuctionTransactionRepo;
        private readonly IRepository<AuctionImages, long> _mstAuctionImagesRepo;
        ///private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<FundImage, long> _mstSleFundImageRepo;
        private readonly IRepository<AuctionItems, long> _mstSleAuctionItemsRepo;
        private readonly IRepository<AuctionDeposit, long> _mstSleAuctionDepositRepo;
        private readonly IRepository<FundRaiserPost, long> _mstFundRaiserPostRepo;
        public FundRaiserAppService(IRepository<Funds, long> mstSleFundRepo,
            IRepository<FundTransactions, long> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo,
            IWebHostEnvironment hostingEnvironment,
            IWebHostEnvironment env,
            IRepository<FundDetails, long> mstSleDetailsRepo,
            IRepository<UserWarning, int> mstSleUserWarningRepo,
            //IRepository<FundRaiser, long> mstSleFundRaiserRepo,
            IRepository<FundImage, long> mstSleFundImageRepo,
            IRepository<Auction, long> mstAuctionRepo,
            IRepository<AuctionTransactions, long> mstAuctionTransactionRepo,
            IRepository<AuctionImages, long> mstAuctionImagesRepo,
            IRepository<AuctionItems, long> mstSleAuctionItemsRepo,
            IRepository<AuctionDeposit, long> mstSleAuctionDepositRepo,
            IRepository<FundRaiserPost, long> mstFundRaiserPostRepo)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleDetailsRepo = mstSleDetailsRepo;
            _mstSleUserWarningRepo = mstSleUserWarningRepo;
            //_mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundImageRepo = mstSleFundImageRepo;
            _mstAuctionRepo = mstAuctionRepo;
            _mstAuctionTransactionRepo = mstAuctionTransactionRepo;
            _mstAuctionImagesRepo = mstAuctionImagesRepo;
            _mstSleAuctionItemsRepo = mstSleAuctionItemsRepo;
            _mstSleAuctionDepositRepo = mstSleAuctionDepositRepo;
            _mstFundRaiserPostRepo = mstFundRaiserPostRepo;
        }
        public async Task CloseFundRaising(long fundId)
        {
            try
            {
                var fund = await _mstSleFundRepo.FirstOrDefaultAsync(s => s.Id == fundId);
                if (fund != null)
                {
                    fund.Status = 2;
                }
                await _mstSleFundRepo.UpdateAsync(fund);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Có lỗi xảy ra trong quá trình đóng quỹ");
            }
        }
        //[HttpPost]
        //[Consumes("multipart/form-data")]
        public async Task CreatePostOfFundRaising(CreateOrEditFundRaisingInputDto input)
        {
            try
            {
                FundRaiserPost post = new FundRaiserPost();
                post.FundId = input.FundId;
                post.PostTitle = input.PostTitle;
                post.UserId = AbpSession.UserId;
                post.TargetIntroduce = input.TargetIntroduce;
                post.Note = input.Note;
                post.PostTopic = input.PostTopic;
                post.IsClose = false;
                post.Purpose = input.Purpose;
                var postId = await _mstFundRaiserPostRepo.InsertAndGetIdAsync(post);

                foreach (var image in input.File)
                {
                    FundImage fundImage = new FundImage();
                    fundImage.PostId = postId;
                    fundImage.ImageUrl = Path.Combine("uploads", image.ImageUrl);
                    fundImage.Size = image.Size;
                    await _mstSleFundImageRepo.InsertAsync(fundImage);
                }
                //if (input.File.Count() > 0)
                //{
                //    foreach (var file in input.File)
                //    {
                //        if (file != null)
                //        {
                //            var fileName = Path.GetFileName(file.FileName);
                //            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                //            using (var stream = new FileStream(filePath, FileMode.Create))
                //            {
                //                await file.CopyToAsync(stream);
                //            }
                //            FundImage fundImage = new FundImage();
                //            fundImage.PostId = postId;

                //            fundImage.ImageUrl = Path.Combine("uploads", fileName);
                //            await _mstSleFundImageRepo.InsertAsync(fundImage);
                //        }
                //    }
                //}
                //else
                //{
                //    throw new UserFriendlyException("Please select a file");
                //}
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                throw new UserFriendlyException("Error uploading file");
            }
        }

        public async Task<CreateOrEditFundRaisingInputDto> getForEditPost(long? postId)
        {
            var post = await _mstFundRaiserPostRepo.FirstOrDefaultAsync(e => e.Id == postId);
            var postResultResponse = new CreateOrEditFundRaisingInputDto();
            ObjectMapper.Map(post, postResultResponse);
            postResultResponse.File = await _mstSleFundImageRepo.GetAll().Where(e => e.PostId == postId).Select(re => new GetInforFileDto
            {
                ImageUrl = re.ImageUrl,
                Size = re.Size
            }
            ).ToListAsync();
            return postResultResponse;
        }

        public void CreateOrEditFundRaising(CreateOrEditFundRaisingDto input)
        {
            if (input.Id == null || input.Id == 0)
            {
                CreateFundRaising(input);
            }
            else
                UpdateFundRaising(input);
        }
        public async Task CreateFundRaising(CreateOrEditFundRaisingDto input)
        {
            Funds fundCreate = new Funds();
            ObjectMapper.Map(input, fundCreate);
            fundCreate.UserId = AbpSession.UserId;
            fundCreate.Status = 1;
            fundCreate.DonateAmount = 0;
            fundCreate.AmountDonationPresent = 0;
            await _mstSleFundRepo.InsertAsync(fundCreate);
        }
        public void UpdateFundRaising(CreateOrEditFundRaisingDto input)
        {
            var fundRaising = _mstSleFundRepo.FirstOrDefault(e => e.Id == input.Id);
            var postFund = _mstFundRaiserPostRepo.FirstOrDefault(e => e.FundId == input.Id);
            if (postFund != null)
            {
                throw new UserFriendlyException("Không thể sửa do quỹ đang được kêu gọi");
            }
            fundRaising.FundName = input.FundName;
            fundRaising.AmountDonationTarget = input.AmountDonationTarget;
            fundRaising.FundRaisingDay = (DateTime)input.FundRaisingDay;
            fundRaising.FundEndDate = (DateTime)input.FundEndDate;
            _mstSleFundRepo.Update(fundRaising);
        }
        public async Task<CreateOrEditFundRaisingDto> GetForEditFundRaising(long? fundId)
        {
            var fundRaising = await _mstSleFundRepo.FirstOrDefaultAsync(e => e.Id == fundId);
            var resultForEdit = new CreateOrEditFundRaisingDto();
            ObjectMapper.Map(fundRaising, resultForEdit);
            return resultForEdit;
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

        public async Task<PagedResultDto<GetListPostByFundRaisingDto>> getListPostByFundRaisingId(GetListPostByFundRaisingInputDto input)
        {
            var listPost = from post in _mstFundRaiserPostRepo.GetAll().Where(e => e.FundId == input.FundId)
                           select new GetListPostByFundRaisingDto
                           {
                               Id = post.Id,
                               TargetIntroduce = post.TargetIntroduce,
                               Note = post.Note,
                               PostTitle = post.PostTitle,
                               PostTopic = post.PostTopic,
                               Purpose = post.Purpose
                           };
            var totalCount = await listPost.CountAsync();
            var result = await listPost.PageBy(input).ToListAsync();
            return new PagedResultDto<GetListPostByFundRaisingDto>(
               totalCount,
               result);
        }
        /// Auction
        public async Task CreateOrEditAuction(CreateOrEditAuction input)
        {
            if (input.Id == null || input.Id == 0)
            {
                var auction = new Auction();
                auction.TitleAuction = input.TitleAuction;
                auction.StartDate = input.StartDate;
                auction.EndDate = input.EndDate;
                auction.LimitedOfNumber = auction.LimitedOfNumber;
                auction.UserId = AbpSession.UserId;
                auction.AmountTargetOfMoney = input.AmountTargetOfMoney;
                auction.AuctionPresentAmount = 0;
                await _mstAuctionRepo.InsertAsync(auction);
            }
            else
            {
                var auction = await _mstAuctionRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                auction.TitleAuction = input.TitleAuction;
                auction.StartDate = input.StartDate;
                auction.EndDate = input.EndDate;
                auction.LimitedOfNumber = auction.LimitedOfNumber;
                auction.AmountTargetOfMoney = input.AmountTargetOfMoney;
                await _mstAuctionRepo.UpdateAsync(auction);
            }
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task CreateOrEditItemAuction([FromForm] CreateOrEditAuctionInputDto input)
        {
            try
            {
                if (input.Id == null || input.Id == 0)
                {
                    AuctionItems auctionItem = new AuctionItems();
                    auctionItem.AuctionId = input.Id;
                    auctionItem.ItemName = input.ItemName;
                    auctionItem.AmountJumpMax = input.AmountJumpMax;
                    auctionItem.AmountJumpMin = input.AmountJumpMin;
                    auctionItem.IntroduceItem = input.IntroduceItem;
                    auctionItem.StartingPrice = input.StartingPrice;
                    var auctionItemId = await _mstSleAuctionItemsRepo.InsertAndGetIdAsync(auctionItem);
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
                                auctionImage.AuctionItemId = auctionItemId;

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
                        join item in _mstSleAuctionItemsRepo.GetAll() on auction.Id equals item.AuctionId
                        select new GetAllAuctionDto
                        {
                            Id = auction.Id,
                            ItemName = item.ItemName,
                            //TitleAuction = auction.TitleAuction,
                            AmountJumpMax = item.AmountJumpMax,
                            AmountJumpMin = item.AmountJumpMin,
                            EndDate = auction.EndDate,
                            StartingPrice = item.StartingPrice,
                            AuctionPresentAmount = item.AuctionPresentAmount,
                            StartDate = (DateTime)auction.StartDate,
                            IntroduceItem = auction.IntroduceItem,
                            Status = auction.IsStatus == true ? "Đang hoạt động" : "Đã đóng"
                        };

            var totalCount = await query.CountAsync();
            var result = await query.PageBy(input).ToListAsync();

            return new PagedResultDto<GetAllAuctionDto>(
                totalCount,
                result);
        }
        public async Task DeleteAuction(long auctionId)
        {
            var auction = await _mstAuctionRepo.FirstOrDefaultAsync(e => e.Id == auctionId);
            if (auction != null)
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
        public async Task<List<GetAllAuctionDto>> getAllAuctionUser()
        {
            var listAuction = (from auction in _mstAuctionRepo.GetAll()
                                   //|| e.UserId != AbpSession.UserId)
                               join item in _mstSleAuctionItemsRepo.GetAll()
                               on auction.Id equals item.AuctionId
                               select new GetAllAuctionDto
                               {
                                   Id = auction.Id,
                                   ItemName = item.ItemName,
                                   AuctionPresentAmount = item.AuctionPresentAmount,
                                   TitleAuction = auction.TitleAuction,
                                   AmountJumpMax = item.AmountJumpMax,
                                   AmountJumpMin = item.AmountJumpMin,
                                   EndDate = auction.EndDate,
                                   StartingPrice = item.StartingPrice,
                                   StartDate = (DateTime)auction.StartDate,
                                   IntroduceItem = auction.IntroduceItem,
                                   NextMinimumBid = item.AuctionPresentAmount + item.AmountJumpMin,
                                   NextMaximumBid = item.AuctionPresentAmount + item.AmountJumpMax,
                                   ListImage = _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == auction.Id).Select(re => re.ImageUrl).ToList(),
                               }).ToListAsync();


            return await listAuction;
        }
        public GetAuctionDetailDto getAuctionById(long? auctionId)
        {
            var auctionItem = _mstSleAuctionItemsRepo.FirstOrDefault(e => e.AuctionId == auctionId);
            var auctionResult = new GetAuctionDetailDto();
            ObjectMapper.Map(auctionItem, auctionResult);
            auctionResult.ListImage = _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == auctionItem.Id).Select(re => re.ImageUrl).ToList();
            auctionResult.NextMaximumBid = auctionResult.AuctionPresentAmount + auctionItem.AmountJumpMax;
            auctionResult.NextMinimumBid = auctionResult.AuctionPresentAmount + auctionItem.AmountJumpMin;
            TimeSpan timeSpan = DateTime.Now - auctionItem.CreationTime;
            auctionResult.TimeLeft = (int?)timeSpan.TotalDays;
            auctionResult.ListImage = _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == auctionItem.Id).Select(re => re.ImageUrl).ToList();
            //auctionResult.UserName = _mstSleUserRepo.FirstOrDefault(e => e.Id == auction.UserId).UserName;
            return auctionResult;
        }
        public bool CheckUserDepositAuction()
        {
            var deposit = _mstSleAuctionDepositRepo.FirstOrDefault(e => e.UserId == AbpSession.UserId);
            return deposit != null;
        }
        public async Task<PagedResultDto<GetListFundRaisingDto>> getListFundRaising(FundRaisingInputDto input)
        {
            var listFundRaising = from funds in _mstSleFundRepo.GetAll().Where(e => e.UserId == AbpSession.UserId)
                                  select new GetListFundRaisingDto
                                  {
                                      Id = funds.Id,
                                      FundTitle = funds.FundTitle,
                                      FundName = funds.FundName,
                                      FundRaisingDay = funds.FundRaisingDay,
                                      FundEndDate = funds.FundEndDate,
                                      AmountDonationTarget = funds.AmountDonationTarget,
                                      Status = funds.Status == 1 ? "Đang hoạt động" : "Đã đóng",
                                      TotalPost = _mstFundRaiserPostRepo.GetAll().Where(e => e.FundId == funds.Id).Count(),
                                      TotalDonate = _mstSleFundTransactionRepo.GetAll().Where(e => e.FundId == funds.Id && e.IsAdmin == false || e.IsAdmin == null).Count(),
                                  };
            var totalCount = await listFundRaising.CountAsync();
            var result = await listFundRaising.PageBy(input).ToListAsync();

            return new PagedResultDto<GetListFundRaisingDto>(
                totalCount,
                result);
        }
        public async Task<PagedResultDto<GetListPostByFundRaisingDto>> getAllFundRaiserPost(GetListFundRaiserPostInputDto input)
        {
            var listPost = from post in _mstFundRaiserPostRepo.GetAll().Where(e => e.UserId == AbpSession.UserId)
                           join fund in _mstSleFundRepo.GetAll() on post.FundId equals fund.Id
                           select new GetListPostByFundRaisingDto
                           {
                               Id = post.Id,
                               FundName = fund.FundName,
                               PostTopic = post.PostTopic,
                               PostCreated = post.CreationTime,
                               PostTitle = post.PostTitle,
                               AmountOfTarget = fund.AmountDonationTarget,
                               FundEndDate = fund.FundEndDate,
                               Status = fund.Status == 1 ? "Đang hoạt động" : "Đã đóng"
                           };
            var totalCount = await listPost.CountAsync();
            var result = await listPost.PageBy(input).ToListAsync();

            return new PagedResultDto<GetListPostByFundRaisingDto>(
                totalCount,
                result);
        }
    }
}
