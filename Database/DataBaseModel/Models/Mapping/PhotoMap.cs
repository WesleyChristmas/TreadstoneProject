using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataBase.Models.Mapping
{
    public class PhotoMap : EntityTypeConfiguration<Photo>
    {
        public PhotoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRecord);

            // Properties
            this.Property(t => t.Link)
                .IsRequired();

            this.Property(t => t.UserCreate)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserUpdate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Photo");
            this.Property(t => t.IdRecord).HasColumnName("IdRecord");
            this.Property(t => t.IdType).HasColumnName("IdType");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.DateCreate).HasColumnName("DateCreate");
            this.Property(t => t.UserCreate).HasColumnName("UserCreate");
            this.Property(t => t.DateUpdate).HasColumnName("DateUpdate");
            this.Property(t => t.UserUpdate).HasColumnName("UserUpdate");
        }
    }
}
