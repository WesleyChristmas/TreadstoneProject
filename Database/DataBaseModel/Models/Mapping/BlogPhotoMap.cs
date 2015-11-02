using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataBase.Models.Mapping
{
    public class BlogPhotoMap : EntityTypeConfiguration<BlogPhoto>
    {
        public BlogPhotoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRecord);

            // Properties
            // Table & Column Mappings
            this.ToTable("BlogPhoto");
            this.Property(t => t.IdRecord).HasColumnName("IdRecord");
            this.Property(t => t.IdHeader).HasColumnName("IdHeader");
            this.Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
