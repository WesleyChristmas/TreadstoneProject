using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataBase.Models.Mapping
{
    public class BlogHeaderMap : EntityTypeConfiguration<BlogHeader>
    {
        public BlogHeaderMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRecord);

            // Properties
            this.Property(t => t.Header)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.UserCreate)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserUpdate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BlogHeader");
            this.Property(t => t.IdRecord).HasColumnName("IdRecord");
            this.Property(t => t.Header).HasColumnName("Header");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.DateCreate).HasColumnName("DateCreate");
            this.Property(t => t.UserCreate).HasColumnName("UserCreate");
            this.Property(t => t.DateUpdate).HasColumnName("DateUpdate");
            this.Property(t => t.UserUpdate).HasColumnName("UserUpdate");
        }
    }
}
