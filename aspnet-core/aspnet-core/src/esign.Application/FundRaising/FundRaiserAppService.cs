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
using System.IO;
using esign.FundRaising.Admin.Dto;
using esign.Enitity;
using Microsoft.AspNetCore.Hosting;
using esign.FundRaising.UserFundRaising.Dto.Auction;
using Abp.Application.Services.Dto;
using Abp.Linq.Extensions;
using esign.FundRaising.UserFundRaising.Dto;
using esign.FundRaising.SendEmail.Dto;
using esign.FundRaising.SendEmail;

namespace esign.FundRaising
{
    public class FundRaiserAppService : esignAppServiceBase, IFundRaiser
    {
        private readonly IRepository<Funds, long> _mstSleFundRepo;
        private readonly IRepository<FundTransactions, long> _mstSleFundTransactionRepo;
        private readonly IRepository<User, long> _mstSleUserRepo;
        private readonly IRepository<UserWarning, int> _mstSleUserWarningRepo;
        private readonly IRepository<Auction, long> _mstAuctionRepo;
        private readonly IRepository<AuctionHistory, long> _mstAuctionTransactionRepo;
        private readonly IRepository<AuctionImages, long> _mstAuctionImagesRepo;
        ///private readonly IRepository<FundRaiser, long> _mstSleFundRaiserRepo;
        private readonly IRepository<PostImage, long> _mstSleFundImageRepo;
        private readonly IRepository<AuctionItems, long> _mstSleAuctionItemsRepo;
        private readonly IRepository<AuctionDeposit, long> _mstSleAuctionDepositRepo;
        private readonly IRepository<FundRaiserPost, long> _mstFundRaiserPostRepo;
        private readonly IRepository<BankAccount, long> _mstBankRepo;
        private readonly IRepository<AuctionTransactionDeposit, long> _mstAuctionTransactionDeposit;
        private readonly IRepository<UserFundPackage, long> _mstUserFundPackage;
        private readonly ISendEmail _sendEmail;


        public FundRaiserAppService(IRepository<Funds, long> mstSleFundRepo,
            IRepository<FundTransactions, long> mstSleFundTransactionRepo,
            IRepository<User, long> mstSleUserRepo,
            IWebHostEnvironment hostingEnvironment,
            IWebHostEnvironment env,
            IRepository<UserWarning, int> mstSleUserWarningRepo,
            //IRepository<FundRaiser, long> mstSleFundRaiserRepo,
            IRepository<PostImage, long> mstSleFundImageRepo,
            IRepository<Auction, long> mstAuctionRepo,
            IRepository<AuctionHistory, long> mstAuctionTransactionRepo,
            IRepository<AuctionImages, long> mstAuctionImagesRepo,
            IRepository<AuctionItems, long> mstSleAuctionItemsRepo,
            IRepository<AuctionDeposit, long> mstSleAuctionDepositRepo,
            IRepository<FundRaiserPost, long> mstFundRaiserPostRepo,
            IRepository<BankAccount, long> mstBankRepo,
            IRepository<AuctionTransactionDeposit, long> mstAuctionTransactionDeposit,
            ISendEmail sendEmail,
            IRepository<UserFundPackage, long> mstUserFundPackage)
        {
            _mstSleFundRepo = mstSleFundRepo;
            _mstSleFundTransactionRepo = mstSleFundTransactionRepo;
            _mstSleUserRepo = mstSleUserRepo;
            _mstSleUserWarningRepo = mstSleUserWarningRepo;
            //_mstSleFundRaiserRepo = mstSleFundRaiserRepo;
            _mstSleFundImageRepo = mstSleFundImageRepo;
            _mstAuctionRepo = mstAuctionRepo;
            _mstAuctionTransactionRepo = mstAuctionTransactionRepo;
            _mstAuctionImagesRepo = mstAuctionImagesRepo;
            _mstSleAuctionItemsRepo = mstSleAuctionItemsRepo;
            _mstSleAuctionDepositRepo = mstSleAuctionDepositRepo;
            _mstFundRaiserPostRepo = mstFundRaiserPostRepo;
            _mstBankRepo = mstBankRepo;
            _mstAuctionTransactionDeposit = mstAuctionTransactionDeposit;
            _sendEmail = sendEmail;
            _mstUserFundPackage = mstUserFundPackage;
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
                post.FundId = (long)input.FundId;
                post.PostTitle = input.PostTitle;
                post.UserId = AbpSession.UserId;
                post.TargetIntroduce = input.TargetIntroduce;
                post.PostTopic = input.PostTopic;
                post.Status = 1;
                post.Purpose = input.Purpose;
                var postId = await _mstFundRaiserPostRepo.InsertAndGetIdAsync(post);

                foreach (var image in input.File)
                {
                    PostImage fundImage = new PostImage();
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

        public async Task UpdatePostOfFundRaising(CreateOrEditFundRaisingInputDto input)
        {
            var post = await _mstFundRaiserPostRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
            if (post != null)
            {
                post.PostTitle = input.PostTitle;
                post.TargetIntroduce = input.TargetIntroduce;
                post.PostTopic = input.PostTopic;
                post.Purpose = input.Purpose;
                await _mstFundRaiserPostRepo.UpdateAsync(post);
            }
            var listImagePost = await _mstSleFundImageRepo.GetAll().Where(e => e.PostId == input.Id).Select(re => re.Id).ToListAsync();
            foreach (var imagePost in input.File)
            {
                if (imagePost.Id == null)
                {
                    PostImage fundImage = new PostImage();
                    fundImage.PostId = input.Id;
                    fundImage.ImageUrl = Path.Combine("uploads", imagePost.ImageUrl);
                    fundImage.Size = imagePost.Size;
                    await _mstSleFundImageRepo.InsertAsync(fundImage);
                    // input.File?.Remove(imagePost);
                }
            }
            foreach (var imagePost in listImagePost)
            {
                bool check = false;
                foreach (var imageInput in input?.File)
                {
                    if (imageInput.Id == imagePost)
                    {
                        check = true;
                        break;
                    };
                }
                if (check == false)
                {
                    await _mstSleFundImageRepo.DeleteAsync(imagePost);
                }
            }
        }

        public async Task<CreateOrEditFundRaisingInputDto> getForEditPost(long? postId)
        {
            var post = await _mstFundRaiserPostRepo.FirstOrDefaultAsync(e => e.Id == postId);
            var postResultResponse = new CreateOrEditFundRaisingInputDto();
            ObjectMapper.Map(post, postResultResponse);
            postResultResponse.File = await _mstSleFundImageRepo.GetAll().Where(e => e.PostId == postId).Select(re => new GetInforFileDto
            {
                Id = re.Id,
                ImageUrl = re.ImageUrl,
                Size = re.Size

            }).ToListAsync();
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
            var postFund = _mstFundRaiserPostRepo.GetAll().Where(e => e.FundId == input.Id);
            if (postFund.Count() > 0)
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
        public async Task CreateOrEditItemAuction(CreateOrEditAuctionInputDto input)
        {
                if (input.Id == null || input.Id == 0)
                {
                    AuctionItems auctionItem = new AuctionItems();
                    auctionItem.TitleAuction = input.TitleAuction;
                    auctionItem.StartDate = input.StartDate;
                    auctionItem.EndDate = input.EndDate;
                    auctionItem.UserId = AbpSession.UserId;
                    auctionItem.Status = 1;
                    auctionItem.LimitedPersionJoin = input.LimitedPersionJoin;
                    auctionItem.NumberOfParticipants = 0;
                    auctionItem.TargetAmountOfMoney = input.TargetAmountOfMoney;
                    auctionItem.ItemName = input.ItemName;
                    auctionItem.AmountJumpMax = input.AmountJumpMax;
                    auctionItem.AmountJumpMin = input.AmountJumpMin;
                    auctionItem.IntroduceItem = input.IntroduceItem;
                    auctionItem.StartingPrice = input.StartingPrice;
                    auctionItem.AuctionPresentAmount = input.StartingPrice;
                    auctionItem.IsPublic = input.IsPublic;
                    var auctionItemId = await _mstSleAuctionItemsRepo.InsertAndGetIdAsync(auctionItem);


                    foreach (var image in input.File)
                    {
                        AuctionImages auctionImage = new AuctionImages();
                        auctionImage.AuctionItemId = auctionItemId;
                        auctionImage.ImageUrl = Path.Combine("uploads", image.ImageUrl);
                        auctionImage.Size = image.Size;
                        await _mstAuctionImagesRepo.InsertAsync(auctionImage);
                    }
                }
                //Update
                else
                {
                    var auctionItem = await _mstSleAuctionItemsRepo.FirstOrDefaultAsync(e => e.Id == input.Id);
                    if(auctionItem.Amount > input.LimitedPersionJoin)
                    {
                        throw new UserFriendlyException("Số lượng người tham gia phải lớn hơn số lượng hiện tại");
                    }
                    if (auctionItem != null)
                    {
                        auctionItem.TitleAuction = input.TitleAuction;
                        auctionItem.StartDate = input.StartDate;
                        auctionItem.EndDate = input.EndDate;
                        auctionItem.TargetAmountOfMoney = input.TargetAmountOfMoney;
                        auctionItem.ItemName = input.ItemName;
                        auctionItem.AmountJumpMax = input.AmountJumpMax;
                        auctionItem.AmountJumpMin = input.AmountJumpMin;
                        auctionItem.IntroduceItem = input.IntroduceItem;
                        auctionItem.StartingPrice = input.StartingPrice;
                        auctionItem.IsPublic = input.IsPublic;
                        await _mstSleAuctionItemsRepo.UpdateAsync(auctionItem);
                    }
                    var listImageAuction = await _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == input.Id).Select(re => re.Id).ToListAsync();
                    foreach (var imageAution in input.File)
                    {
                        if (imageAution.Id == null)
                        {
                            AuctionImages auctionImage = new AuctionImages();
                            auctionImage.AuctionItemId = input.Id;
                            auctionImage.ImageUrl = Path.Combine("uploads", imageAution.ImageUrl);
                            auctionImage.Size = imageAution.Size;
                            await _mstAuctionImagesRepo.InsertAsync(auctionImage);
                        }
                    }
                    foreach (var imageAuction in listImageAuction)
                    {
                        bool check = false;
                        foreach (var imageInput in input?.File)
                        {
                            if (imageInput.Id == imageAuction)
                            {
                                check = true;
                                break;
                            };
                        }
                        if (check == false)
                        {
                            await _mstAuctionImagesRepo.DeleteAsync(imageAuction);
                        }
                    }
                }
        }
        public async Task<CreateOrEditAuctionInputDto> getForEditAuction(long? auctionItemId)
        {
            var auctionItem = await _mstSleAuctionItemsRepo.FirstOrDefaultAsync(e => e.Id == auctionItemId);
            //if(auctionItem.StartDate >= DateTime.Now)
            //{
            //    throw new UserFriendlyException("Không thể sửa do đang diễn ra đấu giá");
            //}
            var auctionResultResponse = new CreateOrEditAuctionInputDto();
            ObjectMapper.Map(auctionItem, auctionResultResponse);
            auctionResultResponse.File = await _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == auctionItemId).Select(re => new GetInforFileDto
            {
                Id = re.Id,
                ImageUrl = re.ImageUrl,
                Size = re.Size

            }).ToListAsync();
            return auctionResultResponse;
        }

        public async Task<List<GetListTransactionForAuctionDto>> GetListTransactionAuction(long auctionId)
        {
            var listAuction = (from transactionAuction in _mstAuctionTransactionRepo.GetAll().Where(e => e.AuctionItemId == auctionId)
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
            var query = from item in _mstSleAuctionItemsRepo.GetAll().Where(e => AbpSession.TenantId == null || e.UserId == AbpSession.UserId)
                        //.Where(e=>e)
                        select new GetAllAuctionDto
                        {
                            Id = item.Id,
                            ItemName = item.ItemName,
                            TitleAuction = item.TitleAuction,
                            AmountJumpMax = item.AmountJumpMax,
                            AmountJumpMin = item.AmountJumpMin,
                            EndDate = item.EndDate.Value.ToString("dd/MM/yyyy"),
                            StartingPrice = item.StartingPrice,
                            AuctionPresentAmount = item.AuctionPresentAmount,
                            StartDate = item.StartDate.Value.ToString("dd/MM/yyyy"),
                            IntroduceItem = item.IntroduceItem,
                            Status = item.Status == 1 ? "Đã khởi tạo" : item.Status == 2 ? "Đâng hoạt động" :"Đã kết thúc",
                            LimitedPersionJoin = item.LimitedPersionJoin,
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
            var listAuction = (from item in _mstSleAuctionItemsRepo.GetAll().
                               Where(e=>e.Status == 2).
                               WhereIf(AbpSession.TenantId != null, e => e.UserId != AbpSession.UserId)
                               join user in _mstSleUserRepo.GetAll() on item.UserId equals user.Id
                                   //|| e.UserId != AbpSession.UserId)
                               select new GetAllAuctionDto
                               {
                                   Id = item.Id,
                                   ItemName = item.ItemName,
                                   AuctionPresentAmount = item.AuctionPresentAmount,
                                   TitleAuction = item.TitleAuction,
                                   AmountJumpMax = item.AmountJumpMax,
                                   AmountJumpMin = item.AmountJumpMin,
                                   EndDate = item.EndDate.Value.ToString("dd/MM/yyyy"),
                                   StartingPrice = item.StartingPrice,
                                   StartDate = item.StartDate.Value.ToString("dd/MM/yyyy"),
                                   IntroduceItem = item.IntroduceItem,
                                   NextMinimumBid = item.AuctionPresentAmount + item.AmountJumpMin,
                                   NextMaximumBid = item.AuctionPresentAmount + item.AmountJumpMax,
                                   ListImage = _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == item.Id).Select(re => re.ImageUrl).ToList(),
                                   UserCreate = item.IsPublic == true ? user.Surname + " " + user.Name : "Anomongus",
                                   //IsCloseAuction = item.IsClose
                               }).ToListAsync();


            return await listAuction;
        }
        public GetAuctionDetailDto getAuctionById(long? auctionId)
        {
            var auctionItem = _mstSleAuctionItemsRepo.FirstOrDefault(e => e.Id == auctionId);
            var auctionResult = new GetAuctionDetailDto();
            ObjectMapper.Map(auctionItem, auctionResult);
            auctionResult.ListImage = _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == auctionItem.Id).Select(re => re.ImageUrl).ToList();
            auctionResult.NextMaximumBid = auctionResult.AuctionPresentAmount + auctionItem.AmountJumpMax;
            auctionResult.NextMinimumBid = auctionResult.AuctionPresentAmount + auctionItem.AmountJumpMin;
            TimeSpan timeSpan = DateTime.Now - auctionItem.CreationTime;
            auctionResult.TimeLeft = (int?)timeSpan.TotalDays;
            auctionResult.ListImage = _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == auctionItem.Id).Select(re => re.ImageUrl).ToList();
            auctionResult.Status = auctionItem.Status;
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
                                  .Where(e=>input.Status == 0 || e.Status == input.Status)
                                  select new GetListFundRaisingDto
                                  {
                                      Id = funds.Id,
                                      FundTitle = funds.FundTitle,
                                      FundName = funds.FundName,
                                      FundRaisingDay = funds.FundRaisingDay.ToString("dd/MM/yyyy"),
                                      FundEndDate = funds.FundEndDate.ToString("dd/MM/yyyy"),
                                      AmountDonationTarget = funds.AmountDonationTarget,
                                      Status = funds.Status == 1 ? "Đang hoạt động" : "Đã đóng",
                                      TotalPost = _mstFundRaiserPostRepo.GetAll().Where(e => e.FundId == funds.Id).Count(),
                                      TotalDonate = funds.DonateAmount,
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
                           join fund in _mstSleFundRepo.GetAll().Where(e=>input.FundId == null ||e.Id == input.FundId) on post.FundId equals fund.Id
                           select new GetListPostByFundRaisingDto
                           {
                               Id = post.Id,
                               FundName = fund.FundName,
                               PostTopic = post.PostTopic,
                               PostCreated = post.CreationTime.ToString("dd/MM/yyyy"),
                               PostTitle = post.PostTitle,
                               AmountOfTarget = fund.AmountDonationTarget,
                               FundEndDate = fund.FundEndDate.ToString("dd/MM/yyyy"),
                               FundRaisingDay = fund.FundRaisingDay.ToString("dd/MM/yyyy"),
                               Status = fund.Status == 1 ? "Đang hoạt động" : "Đã đóng"
                           };
            var totalCount = await listPost.CountAsync();
            var result = await listPost.PageBy(input).ToListAsync();

            return new PagedResultDto<GetListPostByFundRaisingDto>(
                totalCount,
                result);
        }
        public List<GetListComboboxDto> getListFundName()
        {
            var listFundRaising = from fund in _mstSleFundRepo.GetAll().Where(e => e.Status == 1)
                                  .WhereIf(AbpSession.TenantId != null,e=>e.UserId == AbpSession.UserId)
                                  select new GetListComboboxDto
                                  {
                                      Id = fund.Id,
                                      Name = fund.FundName
                                  };
            return listFundRaising.ToList();
        }

        public async void ClosePostFundRaising(long postId)
        {
            var post = await _mstFundRaiserPostRepo.FirstOrDefaultAsync(e => e.Id == postId);
            post.Status = 3;
            await _mstFundRaiserPostRepo.UpdateAsync(post);
        }
        public async void CloseAuctionItem(long auctionItemId)
        {
            var auctionItem = await _mstSleAuctionItemsRepo.FirstOrDefaultAsync(e => e.Id == auctionItemId);
            auctionItem.Status = 3;
            await _mstSleAuctionItemsRepo.UpdateAsync(auctionItem);
        }
        public async void PayDepositAuction(long auctionItemId)
        {
            var auctionItem =  _mstSleAuctionItemsRepo.FirstOrDefault(e => e.Id == auctionItemId);
            var listDeposit =  _mstSleAuctionDepositRepo.GetAll().Where(e => e.AuctionItemId == auctionItem.Id && e.IsPayDeposit == false).ToList();
            if (listDeposit == null)
            {
                throw new UserFriendlyException("Đã trả cọc toàn bộ người tham gia đấu giá");
            }
            foreach (var deposit in listDeposit)
            {
                var bankAccountUserDeposit =  _mstBankRepo.FirstOrDefault(e => e.UserId == deposit.UserId);
                var bankAccountAdmin =  _mstBankRepo.FirstOrDefault(e => e.UserId == auctionItem.UserId);
                if (bankAccountUserDeposit != null)
                {
                    bankAccountUserDeposit.Balance += deposit.DepositAmount;
                     _mstBankRepo.Update(bankAccountUserDeposit);

                    bankAccountAdmin.Balance -= deposit.DepositAmount;
                     _mstBankRepo.Update(bankAccountAdmin);

                    AuctionTransactionDeposit fundTransaction = new AuctionTransactionDeposit();
                    fundTransaction.SenderId = auctionItem.UserId;
                    fundTransaction.ReceiverId = deposit.UserId;
                    fundTransaction.AuctionItemId = deposit.AuctionItemId;
                    fundTransaction.AmountOfMoney = deposit.DepositAmount;
                    fundTransaction.MessageContent = "Trả cọc đấu giá";
                    await _mstAuctionTransactionDeposit.InsertAsync(fundTransaction);
                    deposit.IsPayDeposit = true;
                     _mstSleAuctionDepositRepo.Update(deposit);
                }
            }
            var user = listDeposit.MaxBy(e => e.DepositAmount);
            var emailUserWin =  _mstSleUserRepo.FirstOrDefault(e=>e.Id == user.UserId)?.Email;
            SendEmailInputDto sendEmailInput = new SendEmailInputDto
            {
                EmailReceive = emailUserWin,
                //Body = "Xin chúc mừng bạn đã trở thành người gây quỹ trên hệ thống của chúng tôi, bạn có thể bắt đầu ngay vào việc gây quỹ.",
                Body = "<p style='font-weight:bold;font-size:18px'>Hệ thống gây quỹ trực tuyến FundRaising.</p>" +
                          "<p>Chúc mừng bạn đã chiến thắng phiên đấu giá "+ auctionItem.TitleAuction +" với mức giá đấu là "+ auctionItem.AuctionPresentAmount+"</p>" +
                          "<p>Bạn vui lòng thanh toán giá trị vật phẩm đấu giá và nhập đầy đủ thông tin nhận hàng để chúng tôi" +
                          "vận chuyển vật phẩm tới bạn một cách nhanh chóng!</p>" +
                          "<i style='color:red'>Cảm ơn bạn đã tin tưởng vào sử dụng hệ thống.Chúng tôi cam kết tạo ra một môi trường gây quỹ công bằng, hợp pháp." +
                          "Chúc cho những dự án của bạn sẽ hoàn thành tốt đẹp giúp đỡ cho những hoàn cảnh khó khăn kịp thời, nâng cao chất lượng xã hội.</i>",
                Subject = "Thông báo trở thành người gây quỹ",
            };
            _sendEmail.SendEmail(sendEmailInput);
        }
        public async Task<List<GetAllAuctionDto>> getAllHistoryAuctionUser()
        {
            var listAuction = from auctionTransaction in _mstAuctionTransactionRepo.GetAll() 
                               join item in _mstSleAuctionItemsRepo.GetAll() on auctionTransaction.AuctionItemId equals item.Id
                               join user in _mstSleUserRepo.GetAll() on item.UserId equals user.Id
                               //|| e.UserId != AbpSession.UserId)
                               select new GetAllAuctionDto
                               {
                                   Id = auctionTransaction.Id,
                                   ItemName = item.ItemName,
                                   AuctionPresentAmount = item.AuctionPresentAmount,
                                   TitleAuction = item.TitleAuction,
                                   AmountJumpMax = item.AmountJumpMax,
                                   AmountJumpMin = item.AmountJumpMin,
                                   EndDate = item.EndDate.Value.ToString("dd/MM/yyyy"),
                                   StartingPrice = item.StartingPrice,
                                   StartDate = item.StartDate.Value.ToString("dd/MM/yyyy"),
                                   IntroduceItem = item.IntroduceItem,
                                   NextMinimumBid = item.AuctionPresentAmount + item.AmountJumpMin,
                                   NextMaximumBid = item.AuctionPresentAmount + item.AmountJumpMax,
                                   ListImage = _mstAuctionImagesRepo.GetAll().Where(e => e.AuctionItemId == item.Id).Select(re => re.ImageUrl).ToList(),
                                   UserCreate = item.IsPublic == true ? user.Surname + " " + user.Name : "Anomongus",
                                   //IsCloseAuction = item.IsClose,
                                   AuctionItemsId = item.Id
                               };

            var test = await listAuction.ToListAsync();
            var result = test.DistinctBy(x => x.AuctionItemsId).ToList();
            return result;
        }
        public bool CheckIsExpireFundPackage()
        {
            var user = _mstUserFundPackage.FirstOrDefault(e => e.UserId == AbpSession.UserId);
            if(user == null) return false;
            return (bool)user?.IsExpired;
        }
        public async void ExtentionFundPackage(long fundPackageId)
        {
            var userPackage = new UserFundPackage();
            userPackage.UserId = AbpSession.UserId;
            userPackage.FundPackageId = fundPackageId;
            userPackage.IsExpired = false;
            await _mstUserFundPackage.InsertAsync(userPackage);
        }
        public void HiddenFundRaising(long fundRaisingId)
        {
            var fundRaising = _mstSleFundRepo.FirstOrDefault(e => e.Id == fundRaisingId && e.UserId == AbpSession.UserId);
            if(fundRaising != null)
            {
                fundRaising.Status = 2;
                _mstSleFundRepo.Update(fundRaising);
            }
        }
    }
}
