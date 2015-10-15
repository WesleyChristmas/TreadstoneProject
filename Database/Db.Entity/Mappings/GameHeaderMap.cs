using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class GameHeaderMap : EntityTypeConfiguration<GameHeader>
    {
        public GameHeaderMap()
        {
            // Primary Key
            HasKey(t => t.IdRecord);

            // Properties
            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(300);

            Property(t => t.UserCreate)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.UserUpdate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("GameHeader");
            Property(t => t.IdRecord).HasColumnName("IdRecord");
            Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.DateCreate).HasColumnName("DateCreate");
            Property(t => t.UserCreate).HasColumnName("UserCreate");
            Property(t => t.DateUpdate).HasColumnName("DateUpdate");
            Property(t => t.UserUpdate).HasColumnName("UserUpdate");

            // Relationships
            HasOptional(t => t.Photo)
                .WithMany(t => t.GameHeaders)
                .HasForeignKey(d => d.IdPhoto);

        }
    }
}
