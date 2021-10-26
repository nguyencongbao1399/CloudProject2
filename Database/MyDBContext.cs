using System;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TrackingVoucher_v02.Models;


#nullable disable

namespace TrackingVoucher_v02.Database
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
        {
        }

        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppliedPromotion> AppliedPromotions { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<ClaimedVoucher> ClaimedVouchers { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

          
           
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=34.92.238.241;Initial Catalog=TrackingVoucher;User ID=congbao-admin;Password=123456;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppliedPromotion>(entity =>
            {
                entity.ToTable("AppliedPromotion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PromotionId).HasColumnName("promotionId");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.AppliedPromotions)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AppliedPr__promo__17F790F9");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.AppliedPromotions)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AppliedPr__store__17036CC0");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("phone");

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("website");
            });

            modelBuilder.Entity<ClaimedVoucher>(entity =>
            {
                entity.ToTable("ClaimedVoucher");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.ClaimedDate)
                    .HasColumnType("date")
                    .HasColumnName("claimedDate");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("date")
                    .HasColumnName("expiredDate");

                entity.Property(e => e.LastUsedDate)
                    .HasColumnType("date")
                    .HasColumnName("lastUsedDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.VoucherId).HasColumnName("voucherId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ClaimedVouchers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClaimedVo__userI__208CD6FA");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.ClaimedVouchers)
                    .HasForeignKey(d => d.VoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClaimedVo__vouch__2180FB33");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("Promotion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("date")
                    .HasColumnName("beginDate");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("date")
                    .HasColumnName("expiredDate");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Promotions)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Promotion__brand__14270015");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.BrandId).HasColumnName("brandId");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("phone");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Store__brandId__114A936A");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasColumnName("createdDate");

                entity.Property(e => e.Facebook)
                    .HasMaxLength(100)
                    .HasColumnName("facebook");

                entity.Property(e => e.FacebookToken)
                    .HasMaxLength(500)
                    .HasColumnName("facebook_token");

                entity.Property(e => e.Gmail)
                    .HasMaxLength(100)
                    .HasColumnName("gmail");

                entity.Property(e => e.GmailToken)
                    .HasMaxLength(500)
                    .HasColumnName("gmail_token");

                entity.Property(e => e.Img)
                    .HasMaxLength(200)
                    .HasColumnName("img");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .HasColumnName("phone");

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("date")
                    .HasColumnName("updatedDate");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.ToTable("Voucher");

                entity.HasIndex(e => e.Code, "UQ__Voucher__357D4CF90F655608")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.BeginDate)
                    .HasColumnType("date")
                    .HasColumnName("beginDate");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("description");

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("date")
                    .HasColumnName("expiredDate");

                entity.Property(e => e.MaxDiscountAmount).HasColumnName("maxDiscountAmount");

                entity.Property(e => e.MinRequiredAmount).HasColumnName("minRequiredAmount");

                entity.Property(e => e.PercentDiscount).HasColumnName("percentDiscount");

                entity.Property(e => e.PromotionId).HasColumnName("promotionId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Vouchers)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Voucher__promoti__1BC821DD");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
