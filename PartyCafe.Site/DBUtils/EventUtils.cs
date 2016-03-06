using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class EventResult
    {
        public DateTime CurDate;
        public List<PartyCafeEvent> Calendar;
    }
    public class PartyCafeEvent
    {
        public int idRecord;
        public string name;
        public int IdPhoto;
        public string PhotoPath;
        public DateTime DateEvent;
        //public DateTime DateCreate;
        //public DateTime DateUpdate;
        //public string userCreate;
        //public string userUpdate;
    }

    public static class EventUtils
    {
        public static EventResult GetAll()
        {
            var dbContext = MainUtils.GetDBContext();
            var events = (from e in dbContext.Events
                          join p in dbContext.Photos on e.IdPhoto equals p.IdRecord
                          select new {e.IdRecord, e.Name, e.IdPhoto, e.EventDate, p.Path}).ToList();

            List <PartyCafeEvent> resultList = new List<PartyCafeEvent>();
            foreach (var e in events)
            {
                PartyCafeEvent pcEvent = new PartyCafeEvent();

                pcEvent.idRecord = e.IdRecord;
                pcEvent.name = e.Name;
                pcEvent.IdPhoto = e.IdPhoto;
                pcEvent.PhotoPath = e.Path;
                pcEvent.DateEvent = e.EventDate;
          
                resultList.Add(pcEvent);
            }
            EventResult er = new EventResult();
            er.Calendar = resultList;
            er.CurDate = DateTime.Now;

            return er;
            //return resultList;
        }

        public static EventResult GetNearEvents()
        {
            var dbContext = MainUtils.GetDBContext();
            var events = (from e in dbContext.Events
                          join p in dbContext.Photos on e.IdPhoto equals p.IdRecord
                          where e.EventDate <= DateTime.Now.AddDays(15) && e.EventDate >= DateTime.Now.AddDays(-15)
                          select new { e.IdRecord, e.Name, e.IdPhoto, e.EventDate, p.Path}).ToList();

            List<PartyCafeEvent> resultList = new List<PartyCafeEvent>();
            foreach (var e in events)
            {
                PartyCafeEvent pcEvent = new PartyCafeEvent();

                pcEvent.idRecord = e.IdRecord;
                pcEvent.name = e.Name;
                pcEvent.IdPhoto = e.IdPhoto;
                pcEvent.PhotoPath = e.Path;
                pcEvent.DateEvent = e.EventDate;

                resultList.Add(pcEvent);
            }
            EventResult er = new EventResult();
            er.Calendar = resultList;
            er.CurDate = DateTime.Now;

            return er;
            //return resultList;
        }

        public static void DelEvent(int idRecord)
        {
            var dbContext = MainUtils.GetDBContext();
            var curEvent = (from e in dbContext.Events
                          where e.IdRecord == idRecord
                            select e).SingleOrDefault();
            dbContext.Events.DeleteOnSubmit(curEvent);
            dbContext.SubmitChanges();
        }

        public static void EditEvent(PartyCafeEvent partyEvent, string userUpdate)
        {
            var dbContext = MainUtils.GetDBContext();
            var curEvent = (from e in dbContext.Events
                            where e.IdRecord == partyEvent.idRecord
                            select e).SingleOrDefault();

            curEvent.Name = partyEvent.name;
            curEvent.EventDate = partyEvent.DateEvent;
            curEvent.IdPhoto = partyEvent.IdPhoto;

            curEvent.DateUpdate = DateTime.Now;
            curEvent.UserUpdate = userUpdate;

            dbContext.SubmitChanges();
        }

        public static void InsertEvent(PartyCafeEvent partyEvent, string userCreate)
        {
            var newEvent = new Events();
            newEvent.Name = partyEvent.name;
            newEvent.IdPhoto = partyEvent.IdPhoto;
            newEvent.EventDate = partyEvent.DateEvent;

            newEvent.DateCreate = DateTime.Now;
            newEvent.UserCreate = userCreate;

            var dbContext = MainUtils.GetDBContext();
            dbContext.Events.InsertOnSubmit(newEvent);
            dbContext.SubmitChanges();
        }
    }
}