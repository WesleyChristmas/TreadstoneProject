using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class FoodMenuTypeMap : EntityTypeConfiguration<FoodMenuType>
    {
        public FoodMenuTypeMap()
        {
            // Primary Key
            HasKey(t => t.IdRecord);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            Property(t => t.UserCreate)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.UserUpdate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("FoodMenuType");
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
                .WithMany(t => t.FoodMenuTypes)
                .HasForeignKey(d => d.IdPhoto);
        }
    }
}
