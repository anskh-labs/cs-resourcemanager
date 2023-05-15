using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Models.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.ID);

            builder.Property(x => x.ID).ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.ModifiedDate).HasDefaultValueSql("date('now','localtime')");
            builder.Property(x => x.Title).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Author).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Publisher).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(x => x.Filename).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.Cover).HasColumnType("nvarchar(255)").IsRequired(false);
            builder.Property(x => x.Abstraction).HasColumnType("nvarchar(255)").IsRequired(false);
            builder.HasIndex(x => x.Title).IsUnique();
        }
    }
}
