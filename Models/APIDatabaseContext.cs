using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi.Models
{
    public partial class APIDatabaseContext : DbContext
    {

        public APIDatabaseContext(DbContextOptions<APIDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Carts> Carts { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carts>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsOrderPlaced).HasColumnName("isOrderPlaced");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.ProductData).HasColumnName("productData");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
               entity.HasKey(e => e.Id);

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Paymenttype)
                    .IsRequired()
                    .HasColumnName("paymenttype")
                    .HasMaxLength(15);

                entity.Property(e => e.Totalprice).HasColumnName("totalprice");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50);

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasColumnName("fname")
                    .HasMaxLength(20);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Lname)
                    .HasColumnName("lname")
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
