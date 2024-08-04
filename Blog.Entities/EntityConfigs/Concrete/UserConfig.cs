using Blog.Entities.EntityConfigs.Abstract;
using Blog.Entities.Models.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Entities.EntityConfigs.Concrete
{
    public class UserConfig : BaseConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.UserName).HasMaxLength(100);
            builder.Property(p => p.Mail).HasMaxLength(100);
            builder.Property(p => p.Password).HasMaxLength(100);
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.LastName).HasMaxLength(50);
            builder.Property(p => p.ProfilePhotoUrl).HasMaxLength(1000);


            builder.HasIndex(p => p.Mail).IsUnique();
            builder.HasIndex(p => p.Password).IsUnique();

            builder.HasData(
                new User { Id = 1, Name = "Erol", LastName = "Aydemir", Mail = "erolaydemir27@gmail.com", Password = "admin", UserName = "Admin", ProfilePhotoUrl = " ", RoleId = 1, Role = "Admin", CreateDate = DateTime.Now }
                );


        }
    }
}
