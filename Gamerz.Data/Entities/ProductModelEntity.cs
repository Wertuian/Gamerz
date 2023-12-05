using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Data.Entities
{
    public class ProductModelEntity :BaseEntity
    {
        public string Name { get; set; }    
        public int CategoryId { get; set; }      
        public CategoryEntity Category { get; set; }

    }
    public class ProductModelConfiguration : BaseConfiguration<ProductModelEntity>
    {
        public override void Configure(EntityTypeBuilder<ProductModelEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);

            builder.Property(x => x.CategoryId).IsRequired();

            

            base.Configure(builder);
        }
    }
}
