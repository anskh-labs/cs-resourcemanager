using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResourceManager.Models.Configurations
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.ToTable("Articles");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.Title).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Author).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Filename).HasColumnType("nvarchar(255)").IsRequired();
            builder.HasIndex(x => x.Title).IsUnique();

        }
    }
}
