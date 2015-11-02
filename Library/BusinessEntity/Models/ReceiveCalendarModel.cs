using System;
using System.Collections.Generic;

namespace BusinessEntity.Models
{
    public class ReceiveCalendarModel
    {
        public DateTime CurDate { get; set; }
        public List<BlogCalendarEntity> Calendar { get; set; }

        public ReceiveCalendarModel()
        {
            Calendar = new List<BlogCalendarEntity>();
        }
    }
}
