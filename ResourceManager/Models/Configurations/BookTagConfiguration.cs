using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using ResourceManager.Configuration;

namespace ResourceManager.Models.Configurations
{
    public class BookTagConfiguration : IEntityTypeConfiguration<BookTag>
    {
        public void Configure(EntityTypeBuilder<BookTag> builder)
        {
            builder.ToTable("BookTags");
            builder.HasKey(x => new { x.BookID, x.TagID });

            builder.HasOne(x => x.Book)
                .WithMany(x => x.BookTags)
                .HasForeignKey(x => x.BookID);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.BookTags)
                .HasForeignKey(x => x.TagID);

        }
    }
}
