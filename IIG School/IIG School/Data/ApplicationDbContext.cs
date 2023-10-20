﻿using IIG_School.Helpers;
using IIG_School.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IIG_School.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        static readonly string AdminRoleId = Guid.NewGuid().ToString();
        static readonly string MemberRoleId = Guid.NewGuid().ToString();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);

            SeedUser(builder);
            SeedUserRole(builder);

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }

        public DbSet<Product>? Product { get; set; }
        public DbSet<Question>? Question { get; set; }
        public DbSet<Answer>? Answers { get; set; }
        public DbSet<ExcelDataModel>? ExcelDataModels { get; set; }
        public DbSet<Exam>? Exam { get; set; }
        public DbSet<AddressExam>? AddressExams { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetail>? OrderDetails { get; set; }
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole() { Id = AdminRoleId, Name = Constants.Roles.Admin, NormalizedName = Constants.Roles.Admin, ConcurrencyStamp = null },
                new IdentityRole() { Id = MemberRoleId, Name = Constants.Roles.Member, NormalizedName = Constants.Roles.Member, ConcurrencyStamp = null }
            );
        }

        private static void SeedUser(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "admin@uef.edu.vn",
                    Email = "admin@euef.du.vn",
                    NormalizedEmail = "admin@edu.vn",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAEAACcQAAAAECAsUeOByw0jsD4x7X0K9WQdxWV/RrvPBnHITnRzdbrhHKzmf35BZDPXJBcVjp5FIQ==", //Admin@123
                    SecurityStamp = "ZD5UZJQK6Q5W6N7O6RBRF6DB2Q2G2AIJ",
                    ConcurrencyStamp = "b19f1b24-5ac9-4c8d-9b7c-5e5d5f5cfb1e",
                    FullName = "Xuân Mai",
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                }
            );
        }

        private static void SeedUserRole(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = AdminRoleId // 
                }
            );
        }
        public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
        {
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
            }
        }
    }
}