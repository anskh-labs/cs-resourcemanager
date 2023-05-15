using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using ResourceManager.Configuration;

namespace ResourceManager.Models.Configurations
{
    public class ActivityTagConfiguration : IEntityTypeConfiguration<ActivityTag>
    {
        public void Configure(EntityTypeBuilder<ActivityTag> builder)
        {
            builder.ToTable("ActivityTags");
            builder.HasKey(x => new { x.ActivityID, x.TagID });

            builder.HasOne(x => x.Activity)
                .WithMany(x => x.ActivityTags)
                .HasForeignKey(x => x.ActivityID);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.ActivityTags)
                .HasForeignKey(x => x.TagID);

        }
    }
}
