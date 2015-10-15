using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class FoodMenuMap : EntityTypeConfiguration<FoodMenu>
    {
        public FoodMenuMap()
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
            ToTable("FoodMenu");
            Property(t => t.IdRecord).HasColumnName("IdRecord");
            Property(t => t.IdType).HasColumnName("IdType");
            Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.Price).HasColumnName("Price");
            Property(t => t.DateCreate).HasColumnName("DateCreate");
            Property(t => t.UserCreate).HasColumnName("UserCreate");
            Property(t => t.DateUpdate).HasColumnName("DateUpdate");
            Property(t => t.UserUpdate).HasColumnName("UserUpdate");

            // Relationships
            HasRequired(t => t.FoodMenuType)
                .WithMany(t => t.FoodMenus)
                .HasForeignKey(d => d.IdType);
            HasOptional(t => t.Photo)
                .WithMany(t => t.FoodMenus)
                .HasForeignKey(d => d.IdPhoto);

        }
    }
}
