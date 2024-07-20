using Blog.Entities.EntityConfigs.Abstract;
using Blog.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.EntityConfigs.Concrete
{
    public class CategoryConfig : BaseConfig<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.CategoryName).HasMaxLength(50);
            builder.HasIndex(x => x.CategoryName).IsUnique();

            builder.HasData(

                new Category { Id = 1, CategoryName = "Yazılım" },
                new Category { Id = 2, CategoryName = "Donanım"}


                );
        }
    }
}
