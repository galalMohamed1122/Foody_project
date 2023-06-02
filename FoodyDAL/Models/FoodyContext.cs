using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FoodyDAL.Models
{
    public partial class FoodyContext : DbContext
    {
        public FoodyContext()
        {
        }

        public FoodyContext(DbContextOptions<FoodyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderCharity> OrderCharities { get; set; }
        public virtual DbSet<OrderDelivery> OrderDeliveries { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Orginization> Orginization { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
      
        public virtual DbSet<ViewUser> ViewUsers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.ConfirmPassword).HasMaxLength(50).HasColumnName("Confirm_password");

                entity.Property(e => e.EMail).HasMaxLength(50).HasColumnName("E_mail");

                entity.Property(e => e.FullName).HasMaxLength(50).HasColumnName("Full_name");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasColumnType("numeric(11, 0)");

                entity.Property(e => e.UserType).HasColumnName("User_type");

                entity.HasOne(d => d.Branch)
                   .WithMany(p => p.Users)
                   .HasForeignKey(d => d.BranchId)
                   .HasConstraintName("FK_Users_Branch1");

            });


            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BranchId);
                entity.Property(e => e.Location).HasMaxLength(50).HasColumnName("Location");


                entity.HasOne(d => d.Orginization)
                   .WithMany(p => p.Branches)
                   .HasForeignKey(d => d.OrganizationId)
                   .HasConstraintName("FK_Branch_Orginization1");

            });

            modelBuilder.Entity<Orginization>(entity =>
            {
                entity.HasKey(e => e.OrganizationId);

                entity.Property(e => e.Org_name).HasMaxLength(50).HasColumnName("Org_name");

            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.HasKey(e => e.Id_order).HasName("Id_order");

                entity.Property(e => e.CharityName).HasMaxLength(50).HasColumnName("Charity_name");

                entity.Property(e => e.FkCharityId).HasColumnName("FK_Charity_ID");

                entity.Property(e => e.FkRestaurantId).HasColumnName("FK_Restaurant_ID");
                
                entity.Property(e => e.FkDeliveryId).HasColumnName("FK_Delivery_ID");              

                entity.Property(e => e.RestaurantLocation).HasMaxLength(50).HasColumnName("Restaurant_location");

                entity.Property(e => e.RestaurantName).HasMaxLength(50).HasColumnName("Restaurant_name");

                entity.Property(e => e.RestaurantPhone).HasColumnType("numeric(11, 0)").HasColumnName("Restaurant_phone");

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.FkCharity)
                    .WithMany(p => p.OrderFkCharities)
                    .HasForeignKey(d => d.FkCharityId)
                    .HasConstraintName("FK_Order_Users1");

                entity.HasOne(d => d.FkRestaurant)
                    .WithMany(p => p.OrderFkRestaurants)
                    .HasForeignKey(d => d.FkRestaurantId)
                    .HasConstraintName("FK_Order_Users");

                entity.HasOne(d => d.OrderDetails)
                      .WithOne(p => p.FkIdOrderNavigation)
                      .HasForeignKey<OrderDetail>(p => p.Id)
                      .HasConstraintName("Order_OrderDetails");
            });

            modelBuilder.Entity<OrderCharity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.ToTable("Order_charity");

                entity.Property(e => e.AcceptCharity).HasColumnName("Accept_charity");

                entity.Property(e => e.DeliveryName).HasMaxLength(100).HasColumnName("Delivery_name");

                entity.Property(e => e.FkCharityId).HasColumnName("FK_Charity_ID");

                entity.Property(e => e.FkOrderId).HasColumnName("FK_Order_ID");


                entity.HasOne(d => d.FkCharity)
                    .WithMany(p => p.OrderCharityHistory)
                    .HasForeignKey(d => d.FkCharityId).HasConstraintName("FK_Order_charity_Users");


                entity.HasOne(d => d.FkOrder)
                    .WithMany(p => p.OrderCharities)
                    .HasForeignKey(d => d.FkOrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Order_charity_Order");
            });

            modelBuilder.Entity<OrderDelivery>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Id");

                entity.ToTable("Order_delivery");

                entity.Property(e => e.FkIdDelivery).HasColumnName("Fk_ID_delivery");

                entity.Property(e => e.DoneDelivery).HasColumnName("Done_delivery");

                entity.Property(e => e.FkOrderId).HasColumnName("FK_Order_ID");


                entity.HasOne(d => d.FkDelivery)
                   .WithMany(p => p.OrderDeliveyHistory)
                   .HasForeignKey(d => d.FkIdDelivery)
                   .HasConstraintName("FK_Order_delivery_Users");

                entity.HasOne(d => d.FkOrder)
                    .WithMany(p => p.OrderDeliveries)
                    .HasForeignKey(d => d.FkOrderId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Order_delivery_Order");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("Order_details");

                entity. HasKey(e => e.Id).HasName("ID");

                entity.Property(e => e.LeftoversExpiry).HasColumnType("datetime").HasColumnName("Leftovers_expiry");

                entity.Property(e => e.LeftoversQty).HasColumnName("Leftovers_qty");

                entity.Property(e => e.LeftoversType).HasMaxLength(50).HasColumnName("Leftovers_type");

                entity.Property(e => e.MealExpiry).HasColumnType("datetime").HasColumnName("Meal_expiry");

                entity.Property(e => e.MealQty).HasColumnName("Meal_qty");

                entity.Property(e => e.MealType).HasMaxLength(50).HasColumnName("Meal_type");

            
            });


          

            modelBuilder.Entity<ViewUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_User");

                entity.Property(e => e.EMail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("E_mail");

                entity.Property(e => e.FkUserId).HasColumnName("FK_user_ID");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("Full_name");

                entity.Property(e => e.IdUsers).HasColumnName("ID_users");

                entity.Property(e => e.OrgName)
                    .HasMaxLength(50)
                    .HasColumnName("Org_name");

                entity.Property(e => e.OrgType)
                    .HasMaxLength(50)
                    .HasColumnName("Org_type");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasColumnType("numeric(11, 0)");

                entity.Property(e => e.UserType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
