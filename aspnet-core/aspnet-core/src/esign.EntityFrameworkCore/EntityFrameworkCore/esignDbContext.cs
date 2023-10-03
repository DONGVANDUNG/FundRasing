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
using esign.ESign;

namespace esign.EntityFrameworkCore
{
    public class esignDbContext : AbpZeroDbContext<Tenant, Role, User, esignDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<PersistedGrantEntity> PersistedGrants { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }
        public virtual DbSet<ESiDocument> ESiDocuments { get; set; }
        public virtual DbSet<ESiDocumentFile> ESiDocumentFiles { get; set; }
        public virtual DbSet<ESiDocumentFileImage> ESiDocumentFileImages { get; set; }
        public virtual DbSet<ESiDocumentFileImageSign> ESiDocumentFileImageSigns { get; set; }
        public virtual DbSet<ESiDocumentFileImageSignLog> ESiDocumentFileImageSignLogs { get; set; }
        public virtual DbSet<ESiDocumentFileLog> ESiDocumentFileLogs { get; set; }
        public virtual DbSet<ESiDocumentForward> ESiDocumentForwards { get; set; }
        public virtual DbSet<ESiDocumentUser> ESiDocumentUsers { get; set; }
        public virtual DbSet<ESiDocumentUserLog> ESiDocumentUserLogs { get; set; }
        public virtual DbSet<ESiEmailAd> ESiEmailAds { get; set; }
        public virtual DbSet<ESiEmailTemplate> ESiEmailTemplates { get; set; }
        public virtual DbSet<ESiNotification> ESiNotifications { get; set; }
        public virtual DbSet<ESiSignature> ESiSignatures { get; set; }
        public virtual DbSet<ESiSystem> ESiSystems { get; set; }

        public esignDbContext(DbContextOptions<esignDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

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
