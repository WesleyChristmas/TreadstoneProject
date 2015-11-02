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

            // Relationships
            HasRequired(t => t.BlogHeader)
                .WithMany(t => t.BlogPhotos)
                .HasForeignKey(d => d.IdHeader);
            HasRequired(t => t.Photo)
                .WithMany(t => t.BlogPhotos)
                .HasForeignKey(d => d.IdPhoto);
        }
    }
}
