using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataBase.Models.Mapping
{
    public class BlogCalendarMap : EntityTypeConfiguration<BlogCalendar>
    {
        public BlogCalendarMap()
        {
            // Primary Key
            this.HasKey(t => t.IdRecord);

            // Properties
            this.Property(t => t.Header)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            this.ToTable("BlogCalendar");
            this.Property(t => t.IdRecord).HasColumnName("IdRecord");
            this.Property(t => t.IdHeader).HasColumnName("IdHeader");
            this.Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            this.Property(t => t.Header).HasColumnName("Header");

            // Relationships
            this.HasRequired(t => t.BlogHeader)
                .WithMany(t => t.BlogCalendars)
                .HasForeignKey(d => d.IdHeader);

        }
    }
}
