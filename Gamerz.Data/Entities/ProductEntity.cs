using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Data.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; } 
        public int UnitInStock { get; set; }
        public string ImagePath { get; set; }
        public int ProductModelId { get; set; }
        // Relational Prop

        
        public ProductModelEntity ProductModel { get; set; }
        
    }

    public class ProductConfiguration : BaseConfiguration<ProductEntity> 
    {
        public override void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Description).IsRequired().HasMaxLength(200);

            builder.Property(x => x.UnitPrice).IsRequired(false);

            builder.Property(x => x.ImagePath).IsRequired(false);

            builder.Property(x => x.ProductModelId).IsRequired();
            


            base.Configure(builder);
        }
    }
}
