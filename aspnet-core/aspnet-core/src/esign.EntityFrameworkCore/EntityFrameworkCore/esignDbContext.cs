using Abp.IdentityServer4vNext;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using esign.Authorization.Delegation;
using esign.Authorization.Roles;
using esign.Authorization.Users;
using esign.Chat;
using esign.Editions;
using esign.Friendships;
using esign.MultiTenancy;
using esign.MultiTenancy.Accounting;
using esign.MultiTenancy.Payments;
using esign.Storage;
using esign.FundRaising;
using esign.Entity;
using esign.Enitity;

namespace esign.EntityFrameworkCore
{
    public class esignDbContext : AbpZeroDbContext<Tenant, Role, User, esignDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */


        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }


        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }

        //// Fund Raising

        public virtual DbSet<FundDetails> FundDetails { get; set; }
        public virtual DbSet<FundPackage> FundPackages { get; set; }
        //public virtual DbSet<FundRaiser> FundRaisers { get; set; }
        public virtual DbSet<Funds> Funds { get; set; }
        public virtual DbSet<FundTransactions> FundTransactions { get; set; }
        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserImage> UserImages { get; set; }
        public virtual DbSet<UserWarning> UserWarning { get; set; }
        public virtual DbSet<FundDetailContent> FundDetailContents { get; set; }
        public virtual DbSet<FundRaisingTopic> FundRaisingTopics { get; set; }
        public virtual DbSet<GuestAccount> GuestAccounts { get; set; }
        public virtual DbSet<FundImage> FundImage { get; set; }
        public virtual DbSet<BankAccount> BankAccounts { get; set; }

        public esignDbContext(DbContextOptions<esignDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique()
                    .HasFilter("[IsDeleted] = 0");
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
