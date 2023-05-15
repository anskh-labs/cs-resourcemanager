using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using ResourceManager.Configuration;

namespace ResourceManager.Models.Configurations
{
    public class PasswordTagConfiguration : IEntityTypeConfiguration<PasswordTag>
    {
        public void Configure(EntityTypeBuilder<PasswordTag> builder)
        {
            builder.ToTable("PasswordTags");
            builder.HasKey(x => new { x.PasswordID, x.TagID });

            builder.HasOne(x => x.Password)
                .WithMany(x => x.PasswordTags)
                .HasForeignKey(x => x.PasswordID);

            builder.HasOne(x => x.Tag)
                .WithMany(x => x.PasswordTags)
                .HasForeignKey(x => x.TagID);

        }
    }
}
