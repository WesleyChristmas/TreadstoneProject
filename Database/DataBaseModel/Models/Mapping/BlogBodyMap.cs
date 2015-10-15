using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataBase.Models.Mapping
{
    public class BlogBodyMap : EntityTypeConfiguration<BlogBody>
    {
        public BlogBodyMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRecord);

            // Properties
            // Table & Column Mappings
            this.ToTable("BlogBody");
            this.Property(t => t.IdRecord).HasColumnName("IdRecord");
            this.Property(t => t.IdHeader).HasColumnName("IdHeader");
            this.Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            this.Property(t => t.Message).HasColumnName("Message");

            // Relationships
            this.HasRequired(t => t.BlogHeader)
                .WithMany(t => t.BlogBodies)
                .HasForeignKey(d => d.IdHeader);
            this.HasOptional(t => t.Photo)
                .WithMany(t => t.BlogBodies)
                .HasForeignKey(d => d.IdPhoto);

        }
    }
}
