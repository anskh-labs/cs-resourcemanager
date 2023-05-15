using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using ResourceManager.Configuration;

namespace ResourceManager.Models.Configurations
{
    public class RepositoryTagConfiguration : IEntityTypeConfiguration<RepositoryTag>
    {
        public void Configure(EntityTypeBuilder<RepositoryTag> builder)
        {
            builder.ToTable("RepositoryTags");
            builder.HasKey(x => new { x.RepositoryID, x.TagID });

            builder.HasOne(x => x.Repository)
                .WithMany(x => x.RepositoryTags)
                .HasForeignKey(x => x.RepositoryID);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.RepositoryTags)
                .HasForeignKey(x => x.TagID);

        }
    }
}
