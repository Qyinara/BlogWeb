using Blog.Entities.Models.Concrete;
using Blog.Entities.EntityConfigs.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Entities.EntityConfigs.Concrete
{
    public class PostConfig : BaseConfig<Post>
    {

        public override void Configure(EntityTypeBuilder<Post> builder)
        {
            base.Configure(builder); // BaseConfig'teki genel yapılandırmaları uygula

            // Property configurations
            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Content)
                .IsRequired();

            builder.Property(p => p.PostImageURL)
                .HasMaxLength(500);

            // Relationships
            builder.HasOne(p => p.Author)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PostLikes)
                .WithOne(pl => pl.Post)
                .HasForeignKey(pl => pl.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
