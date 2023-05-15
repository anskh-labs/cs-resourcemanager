using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResourceManager.Models.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("date('now', 'localtime')");
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("date('now', 'localtime')");
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
