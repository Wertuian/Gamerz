using Gamerz.Data.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Data.Context
{
    public class GamerzContext : DbContext
    {
        private readonly IDataProtector _dataProtector;
        public GamerzContext(DbContextOptions<GamerzContext> options,IDataProtectionProvider dataProtectionProvider) : base(options)
        {

            _dataProtector = dataProtectionProvider.CreateProtector("security");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductModelConfiguration());

            var pwd = "123";
            pwd = _dataProtector.Protect(pwd);


            modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>() {

                new UserEntity()
                {
                    Id = 1,
                    Name = "mert",
                    LastName = "uy",
                    Email = "mert@gmail.com",
                    Password = pwd,
                    UserType = Enums.UserTypeEnum.Admin,
                    
                }
                });









        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<ProductModelEntity> ProductModels => Set<ProductModelEntity>();

    }
}
