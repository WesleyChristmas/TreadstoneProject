using System;
using System.Collections.Generic;

namespace BusinessEntity.Models
{
    public class SendCalendarModel
    {
        public DateTime CurDate { get; set; }
        public List<BlogCalendarEntity> Calendar { get; set; }

        public SendCalendarModel()
        {
            Calendar = new List<BlogCalendarEntity>();
        }
    }
}
