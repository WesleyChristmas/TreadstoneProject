using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class BlogHeaderMap : EntityTypeConfiguration<BlogHeader>
    {
        public BlogHeaderMap()
        {
            // Primary Key
            HasKey(t => t.IdRecord);

            // Properties
            Property(t => t.Header)
                .IsRequired()
                .HasMaxLength(300);

            Property(t => t.UserCreate)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.UserUpdate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("BlogHeader");
            Property(t => t.IdRecord).HasColumnName("IdRecord");
            Property(t => t.Header).HasColumnName("Header");
            Property(t => t.Message).HasColumnName("Message");
            Property(t => t.DateCreate).HasColumnName("DateCreate");
            Property(t => t.UserCreate).HasColumnName("UserCreate");
            Property(t => t.DateUpdate).HasColumnName("DateUpdate");
            Property(t => t.UserUpdate).HasColumnName("UserUpdate");
        }
    }
}
