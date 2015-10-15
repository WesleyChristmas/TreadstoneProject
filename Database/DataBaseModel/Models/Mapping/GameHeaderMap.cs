using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataBase.Models.Mapping
{
    public class GameHeaderMap : EntityTypeConfiguration<GameHeader>
    {
        public GameHeaderMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRecord);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(300);

            this.Property(t => t.UserCreate)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UserUpdate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GameHeader");
            this.Property(t => t.IdRecord).HasColumnName("IdRecord");
            this.Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DateCreate).HasColumnName("DateCreate");
            this.Property(t => t.UserCreate).HasColumnName("UserCreate");
            this.Property(t => t.DateUpdate).HasColumnName("DateUpdate");
            this.Property(t => t.UserUpdate).HasColumnName("UserUpdate");

            // Relationships
            this.HasOptional(t => t.Photo)
                .WithMany(t => t.GameHeaders)
                .HasForeignKey(d => d.IdPhoto);

        }
    }
}
