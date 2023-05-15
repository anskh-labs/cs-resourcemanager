using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResourceManager.Configuration;

namespace ResourceManager.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.AccountName).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.Password).HasColumnType("nvarchar(100)").IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasIndex(x => x.AccountName).IsUnique();

            builder.HasData(DesignData.Instance.Users);
        }
    }
}