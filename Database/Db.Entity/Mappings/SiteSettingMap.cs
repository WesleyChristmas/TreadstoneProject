using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class SiteSettingMap : EntityTypeConfiguration<SiteSetting>
    {
        public SiteSettingMap()
        {
            // Primary Key
            HasKey(t => new { t.IdRecord, t.ImageDataWeb, t.ImageDataDrive });

            // Properties
            Property(t => t.IdRecord)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ImageDataWeb)
                .IsRequired();

            Property(t => t.ImageDataDrive)
                .IsRequired();

            // Table & Column Mappings
            ToTable("SiteSettings");
            Property(t => t.IdRecord).HasColumnName("IdRecord");
            Property(t => t.ImageDataWeb).HasColumnName("ImageDataWeb");
            Property(t => t.ImageDataDrive).HasColumnName("ImageDataDrive");
        }
    }
}
