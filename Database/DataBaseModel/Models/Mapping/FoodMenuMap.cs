using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataBase.Models.Mapping
{
    public class FoodMenuMap : EntityTypeConfiguration<FoodMenu>
    {
        public FoodMenuMap()
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
            this.ToTable("FoodMenu");
            this.Property(t => t.IdRecord).HasColumnName("IdRecord");
            this.Property(t => t.IdType).HasColumnName("IdType");
            this.Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.DateCreate).HasColumnName("DateCreate");
            this.Property(t => t.UserCreate).HasColumnName("UserCreate");
            this.Property(t => t.DateUpdate).HasColumnName("DateUpdate");
            this.Property(t => t.UserUpdate).HasColumnName("UserUpdate");

            // Relationships
            this.HasRequired(t => t.FoodMenuType)
                .WithMany(t => t.FoodMenus)
                .HasForeignKey(d => d.IdType);

        }
    }
}
