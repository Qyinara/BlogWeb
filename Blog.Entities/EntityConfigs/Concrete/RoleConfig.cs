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
    public class RoleConfig : BaseConfig<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.RoleName).HasMaxLength(50);
            builder.HasIndex(p => p.RoleName).IsUnique();

            builder.HasData(

                new Role { Id = 1 , RoleName = "Admin"},
                new Role { Id = 2 , RoleName = "User"}

                );

        }
    }
}
