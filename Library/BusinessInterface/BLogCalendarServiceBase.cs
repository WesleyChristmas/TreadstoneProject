using BusinessEntity;
using BusinessEntity.Models;

namespace BusinessInterface
{
    public interface IBlogCalendarServiceBase
    {
        SendCalendarModel GetCalendar();
        void AddBlogCalendar(BlogCalendarEntity calendar, ReceiveFileModel image);
        void UpdateBlogCalendar(BlogCalendarEntity calendar, ReceiveFileModel image);
        bool DeleteBlogCalendar(int idCalendar,string serverPath);
    }
}
