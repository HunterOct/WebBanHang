namespace Project_63131631.Areas.Administrator.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AdminContext_63131631 : DbContext
    {
        public AdminContext_63131631()
            : base("name=AdminContext_63131631")
        {
        }

        public virtual DbSet<Administrator_63131631> Administrators { get; set; }
        public virtual DbSet<Category_63131631> Categories { get; set; }
        public virtual DbSet<Customer_63131631> Customers { get; set; }
        public virtual DbSet<OrderDetail_63131631> OrderDetails { get; set; }
        public virtual DbSet<Order_63131631> Orders { get; set; }
        public virtual DbSet<Producer_63131631> Producers { get; set; }
        public virtual DbSet<Product_63131631> Products { get; set; }
        public virtual DbSet<ProductType_63131631> ProductTypes { get; set; }
        public virtual DbSet<Rate_63131631> Rates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator_63131631>()
                .Property(e => e.adAcc)
                .IsUnicode(false);

            modelBuilder.Entity<Administrator_63131631>()
                .Property(e => e.adPass)
                .IsUnicode(false);


            modelBuilder.Entity<Customer_63131631>()
                .Property(e => e.cusPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer_63131631>()
                .Property(e => e.cusEmail)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail_63131631>()
                .Property(e => e.orderID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail_63131631>()
                .Property(e => e.proID)
                .IsUnicode(false);

            modelBuilder.Entity<OrderDetail_63131631>()
                .Property(e => e.ordtsThanhTien)
                .IsUnicode(false);

            modelBuilder.Entity<Order_63131631>()
                .Property(e => e.orderID)
                .IsUnicode(false);

            modelBuilder.Entity<Order_63131631>()
                .Property(e => e.cusPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Order_63131631>()
                .Property(e => e.orderDateTime)
                .IsUnicode(false);

            modelBuilder.Entity<Order_63131631>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producer_63131631>()
                .Property(e => e.pdcPhone)
                .IsUnicode(false);

            modelBuilder.Entity<Producer_63131631>()
                .Property(e => e.pdcEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Product_63131631>()
                .Property(e => e.proID)
                .IsUnicode(false);

            modelBuilder.Entity<Product_63131631>()
                .Property(e => e.proGuarantee)
                .IsUnicode(false);

            modelBuilder.Entity<Product_63131631>()
                .Property(e => e.proPrice)
                .IsUnicode(false);

            modelBuilder.Entity<Product_63131631>()
                .Property(e => e.proUpdateDate)
                .IsUnicode(false);

            modelBuilder.Entity<Product_63131631>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_63131631>()
                .HasOptional(e => e.Rate)
                .WithRequired(e => e.Product);

            modelBuilder.Entity<Rate_63131631>()
                .Property(e => e.proID)
                .IsUnicode(false);
        }
    }
}
