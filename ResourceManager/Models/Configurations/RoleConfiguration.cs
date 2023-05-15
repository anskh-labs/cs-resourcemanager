using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ResourceManager.Configuration;

namespace ResourceManager.Models.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Description).HasColumnType("nvarchar(255)").IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasData(DesignData.Instance.Roles);
        }
    }
}
