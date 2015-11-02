using System.Data.Entity.ModelConfiguration;
using Entity.Model;

namespace Entity.Mappings
{
    public class BlogCalendarMap : EntityTypeConfiguration<BlogCalendar>
    {
        public BlogCalendarMap()
        {
            // Primary Key
            HasKey(t => t.IdRecord);

            // Properties
            Property(t => t.Header)
                .IsRequired()
                .HasMaxLength(300);

            // Table & Column Mappings
            ToTable("BlogCalendar");
            Property(t => t.IdRecord).HasColumnName("IdRecord");
            Property(t => t.IdBlog).HasColumnName("IdBlog");
            Property(t => t.IdPhoto).HasColumnName("IdPhoto");
            Property(t => t.Header).HasColumnName("Header");
            Property(t => t.EventDate).HasColumnName("EventDate");

            // Relationships
            HasRequired(t => t.BlogHeader)
                .WithMany(t => t.BlogCalendars)
                .HasForeignKey(d => d.IdBlog);
            HasOptional(t => t.Photo)
                .WithMany(t => t.BlogCalendars)
                .HasForeignKey(d => d.IdPhoto);

        }
    }
}
