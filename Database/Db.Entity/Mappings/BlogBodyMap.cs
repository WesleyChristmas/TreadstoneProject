using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class BlogBodyMap : EntityTypeConfiguration<BlogBody>
    {
        public BlogBodyMap()
        {
            // Primary Key
            HasKey(t => t.IdRecord);

            // Properties
            // Table & Column Mappings
            ToTable("BlogBody");
            Property(t => t.IdRecord).HasColumnName("IdRecord");
            Property(t => t.IdHeader).HasColumnName("IdHeader");
            Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            Property(t => t.Message).HasColumnName("Message");

            // Relationships
            HasRequired(t => t.BlogHeader)
                .WithMany(t => t.BlogBodies)
                .HasForeignKey(d => d.IdHeader);
            HasOptional(t => t.Photo)
                .WithMany(t => t.BlogBodies)
                .HasForeignKey(d => d.IdPhoto);

        }
    }
}
