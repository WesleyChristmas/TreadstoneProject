using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class BlogPhotoMap : EntityTypeConfiguration<BlogPhoto>
    {
        public BlogPhotoMap()
        {
            // Primary Key
            HasKey(t => t.IdRecord);

            // Properties
            // Table & Column Mappings
            ToTable("BlogPhoto");
            Property(t => t.IdRecord).HasColumnName("IdRecord");
            Property(t => t.IdHeader).HasColumnName("IdHeader");
            Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            Property(t => t.Description).HasColumnName("Message");
        }
    }
}
