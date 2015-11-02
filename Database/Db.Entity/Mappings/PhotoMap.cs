using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class PhotoMap : EntityTypeConfiguration<Photo>
    {
        public PhotoMap()
        {
            // Primary Key
            HasKey(t => t.IdRecord);

            Property(t => t.Link)
                .IsRequired();

            Property(t => t.UserCreate)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.UserUpdate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Photo");
            Property(t => t.IdRecord).HasColumnName("IdRecord");
            Property(t => t.IdType).HasColumnName("IdType");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Link).HasColumnName("Link");
            Property(t => t.DateCreate).HasColumnName("DateCreate");
            Property(t => t.UserCreate).HasColumnName("UserCreate");
            Property(t => t.DateUpdate).HasColumnName("DateUpdate");
            Property(t => t.UserUpdate).HasColumnName("UserUpdate");

            // Relationships
            HasRequired(t => t.PhotoType)
                .WithMany(t => t.Photos)
                .HasForeignKey(d => d.IdType);

        }
    }
}
