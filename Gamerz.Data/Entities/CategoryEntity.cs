using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Data.Entities
{
    public class CategoryEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }


        public List<ProductModelEntity> ProductsModel { get; set; }



    }
    public class CategoryConfiguration : BaseConfiguration<CategoryEntity> 
    {
        public override void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);

            builder.Property(x => x.Description).IsRequired(false);

            base.Configure(builder);
        }
    }
}
