using Gamerz.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserTypeEnum UserType { get; set; }

    }
    public class UserConfiguration : BaseConfiguration<UserEntity> 
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(25);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(25);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(45);

            builder.Property(x => x.Password).IsRequired();


            base.Configure(builder);
        }
    }
}
