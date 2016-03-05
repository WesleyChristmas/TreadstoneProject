using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class PartyCafeEvent
    {
        public int idRecord;
        public string name;
        public Guid userCreate;
        public Guid userUpdate;
        public DateTime DateCreate;
        public DateTime DateUpdate;
        public DateTime DateEvent;
        public int IdPhoto;
    }

    public static class EventUtils
    {
        public static List<PartyCafeEvent> GetAll()
        {
            var dbContext = MainUtils.GetDBContext();
            var events = (from e in dbContext.Events
                      select e);

            List <PartyCafeEvent> resultList = new List<PartyCafeEvent>();
            foreach (var e in events)
            {
                PartyCafeEvent pcEvent = new PartyCafeEvent();
                pcEvent.idRecord = e.IdRecord;
                pcEvent.name = e.Name;
                pcEvent.userCreate = (e.UserCreate.HasValue) ? e.UserCreate.Value : new Guid();
                pcEvent.userUpdate = (e.UserUpdate.HasValue) ? e.UserUpdate.Value : new Guid();
                pcEvent.DateCreate = e.DateCreate;
                pcEvent.DateUpdate = (e.DateUpdate.HasValue) ? e.DateUpdate.Value : new DateTime();
                pcEvent.DateEvent = e.EventDate;
                pcEvent.IdPhoto = e.IdPhoto;
                resultList.Add(pcEvent);
            }

            return resultList;
        }

        public static List<PartyCafeEvent> GetNearEvents()
        {
            var dbContext = MainUtils.GetDBContext();
            var events = (from e in dbContext.Events
                          where e.EventDate <= DateTime.Now.AddDays(15) && e.EventDate >= DateTime.Now.AddDays(-15)
                          select e);

            List<PartyCafeEvent> resultList = new List<PartyCafeEvent>();
            foreach (var e in events)
            {
                PartyCafeEvent pcEvent = new PartyCafeEvent();
                pcEvent.idRecord = e.IdRecord;
                pcEvent.name = e.Name;
                pcEvent.userCreate = (e.UserCreate.HasValue) ? e.UserCreate.Value : new Guid();
                pcEvent.userUpdate = (e.UserUpdate.HasValue) ? e.UserUpdate.Value : new Guid();
                pcEvent.DateCreate = e.DateCreate;
                pcEvent.DateUpdate = (e.DateUpdate.HasValue) ? e.DateUpdate.Value : new DateTime();
                pcEvent.DateEvent = e.EventDate;
                pcEvent.IdPhoto = e.IdPhoto;
                resultList.Add(pcEvent);
            }

            return resultList;
        }

        public static void DelEvent(PartyCafeEvent partyEvent)
        {
            var dbContext = MainUtils.GetDBContext();
            var curEvent = (from e in dbContext.Events
                          where e.IdRecord == partyEvent.idRecord
                          select e).First();
            dbContext.Events.DeleteOnSubmit(curEvent);
            dbContext.SubmitChanges();
        }

        public static void EditEvent(PartyCafeEvent partyEvent)
        {
            var dbContext = MainUtils.GetDBContext();
            var curEvent = (from e in dbContext.Events
                            where e.IdRecord == partyEvent.idRecord
                            select e).First();

            curEvent.Name = partyEvent.name;
            curEvent.EventDate = partyEvent.DateEvent;
            curEvent.UserCreate = partyEvent.userCreate;
            curEvent.UserUpdate = partyEvent.userUpdate;
            curEvent.DateUpdate = DateTime.Now;
            curEvent.IdPhoto = partyEvent.IdPhoto;

            dbContext.SubmitChanges();
        }

        public static void InsertEvent(PartyCafeEvent partyEvent)
        {
            var newEvent = new Events();
            newEvent.Name = partyEvent.name;
            newEvent.IdPhoto = partyEvent.IdPhoto;
            newEvent.EventDate = partyEvent.DateEvent;
            newEvent.DateCreate = DateTime.Now;
            newEvent.UserCreate = partyEvent.userCreate;

            var dbContext = MainUtils.GetDBContext();
            dbContext.Events.InsertOnSubmit(newEvent);
            dbContext.SubmitChanges();
        }
    }
}