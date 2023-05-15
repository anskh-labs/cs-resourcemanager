using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ResourceManager.Models.Configurations
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.Title).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Date).HasColumnType("text").IsRequired();
            builder.Property(x => x.Duration).HasColumnType("int").IsRequired();
            builder.Property(x => x.StartTime).HasColumnType("text").IsRequired();
            builder.Property(x => x.EndTime).HasColumnType("text").IsRequired();
            builder.Property(x => x.Metric).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(x => x.Quantity).HasColumnType("int").IsRequired();
            builder.Property(x => x.Output).HasColumnType("nvarchar(255)").IsRequired(false);
            builder.Property(x => x.Note).HasColumnType("nvarchar(255)").IsRequired(false);
            builder.HasIndex(x => new { x.Date, x.Title }).IsUnique();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Activities)
                .HasForeignKey(x => x.UserID);
        }
    }
}