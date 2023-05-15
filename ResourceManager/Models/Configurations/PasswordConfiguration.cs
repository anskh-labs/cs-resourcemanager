using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResourceManager.Models.Configurations
{
    public class PasswordConfiguration : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {
            builder.ToTable("Passwords");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.Name).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Username).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.Pass).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Url).HasColumnType("nvarchar(100)").IsRequired(false);
            builder.Property(x => x.Description).HasColumnType("nvarchar(255)").IsRequired(false);
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Passwords)
                .HasForeignKey(x => x.UserID);
                
        }
    }
}
