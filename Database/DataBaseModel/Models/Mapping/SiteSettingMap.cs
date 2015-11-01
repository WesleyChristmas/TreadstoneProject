using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataBase.Models.Mapping
{
    public class SiteSettingMap : EntityTypeConfiguration<SiteSetting>
    {
        public SiteSettingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IdRecord, t.ImageDataWeb, t.ImageDataDrive });

            // Properties
            this.Property(t => t.IdRecord)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ImageDataWeb)
                .IsRequired();

            this.Property(t => t.ImageDataDrive)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("SiteSettings");
            this.Property(t => t.IdRecord).HasColumnName("IdRecord");
            this.Property(t => t.ImageDataWeb).HasColumnName("ImageDataWeb");
            this.Property(t => t.ImageDataDrive).HasColumnName("ImageDataDrive");
        }
    }
}
