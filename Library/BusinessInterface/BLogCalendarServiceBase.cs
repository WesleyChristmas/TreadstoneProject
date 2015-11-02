﻿using BusinessEntity;
using BusinessEntity.Models;

namespace BusinessInterface
{
    public interface IBlogCalendarServiceBase
    {
        ReceiveCalendarModel GetCalendar();
        void AddBlogCalendar(BlogCalendarEntity calendar, ReceiveFileModel image);
        void EditBlogCalendar(BlogCalendarEntity calendar, ReceiveFileModel image);
        bool DeleteBlogCalendar(int idCalendar);
    }
}
