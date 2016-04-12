using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class PCEvent
    {
        public int idRecord;
        public string name;
        public int IdPhoto;
        public string PhotoPath;
        public DateTime DateEvent;
        public TimeSpan TimeEvent;
    }

    public static class EventUtils
    {
        public static List<PCEvent> GetAll()
        {
            var dbContext = MainUtils.GetDBContext();
            var events = (from e in dbContext.Events
                          join p in dbContext.Photos on e.IdPhoto equals p.IdRecord
                          select new {e.IdRecord, e.Name, e.IdPhoto, e.EventDate, p.Path}).ToList();

            List <PCEvent> resultList = new List<PCEvent>();
            foreach (var e in events)
            {
                PCEvent pcEvent = new PCEvent();

                pcEvent.idRecord = e.IdRecord;
                pcEvent.name = e.Name;
                pcEvent.IdPhoto = e.IdPhoto;
                pcEvent.PhotoPath = e.Path;
                pcEvent.DateEvent = e.EventDate.Date;
                pcEvent.TimeEvent = e.EventDate.TimeOfDay;
          
                resultList.Add(pcEvent);
            }

            return resultList;
        }

        public static List<PCEvent> GetNearEvents()
        {
            const int DayInterval = 15;

            var dbContext = MainUtils.GetDBContext();
            var events = (from e in dbContext.Events
                          join p in dbContext.Photos on e.IdPhoto equals p.IdRecord
                          where e.EventDate <= DateTime.Now.AddDays(DayInterval) && e.EventDate >= DateTime.Now.AddDays(-DayInterval)
                          select new {e.IdRecord, e.Name, e.IdPhoto, e.EventDate, p.Path}).ToList();

            List<PCEvent> resultList = new List<PCEvent>();
            foreach (var e in events)
            {
                PCEvent pcEvent = new PCEvent();

                pcEvent.idRecord = e.IdRecord;
                pcEvent.name = e.Name;
                pcEvent.IdPhoto = e.IdPhoto;
                pcEvent.PhotoPath = e.Path;
                pcEvent.DateEvent = e.EventDate.Date;
                pcEvent.TimeEvent = e.EventDate.TimeOfDay;

                resultList.Add(pcEvent);
            }
            return resultList;
        }

        public static void DelEvent(int idRecord)
        {
            var dbContext = MainUtils.GetDBContext();
            var curEvent = (from e in dbContext.Events
                          where e.IdRecord == idRecord
                            select e).SingleOrDefault();

            dbContext.Events.DeleteOnSubmit(curEvent);
            dbContext.SubmitChanges();

            if (curEvent.IdPhoto > 0) { PhotoUtils.DelImage(curEvent.IdPhoto); };
        }

        public static void EditEvent(PCEvent partyEvent, string userUpdate, PCPhoto image)
        {
            var dbContext = MainUtils.GetDBContext();
            var curEvent = (from e in dbContext.Events
                            where e.IdRecord == partyEvent.idRecord
                            select e).SingleOrDefault();

            curEvent.Name = partyEvent.name != null ? partyEvent.name : "";
            
            if (partyEvent.DateEvent != null)
            {
                DateTime newDate = partyEvent.DateEvent;
                if (partyEvent.TimeEvent != null)
                {
                    newDate.AddSeconds(partyEvent.TimeEvent.Seconds);
                }
                curEvent.EventDate = newDate;
            }
            else
            {
                curEvent.EventDate = DateTime.Now;
            }

            curEvent.DateUpdate = DateTime.Now;
            curEvent.UserUpdate = userUpdate;

            if (image != null)
            { 
                if (curEvent.IdPhoto > 0)
                {
                    PhotoUtils.EditImage(curEvent.IdPhoto, image, userUpdate);
                } else {
                    curEvent.IdPhoto = PhotoUtils.InsertImage(image, userUpdate);
                }
            }

            dbContext.SubmitChanges();

        }

        public static void InsertEvent(PCEvent partyEvent, string userCreate, PCPhoto image)
        {
            var newEvent = new Events();
            newEvent.Name = partyEvent.name != null ? partyEvent.name : "";

            if (partyEvent.DateEvent != null)
            {
                DateTime newDate = partyEvent.DateEvent;
                if (partyEvent.TimeEvent != null)
                {
                    newDate.AddSeconds(partyEvent.TimeEvent.Seconds);
                }
                newEvent.EventDate = newDate;
            }
            else
            {
                newEvent.EventDate = DateTime.Now;
            }

            if (image != null)
            {
                newEvent.IdPhoto = PhotoUtils.InsertImage(image, userCreate);
            }
            else
            {
                newEvent.IdPhoto = 0;
            }

            newEvent.DateCreate = DateTime.Now;
            newEvent.UserCreate = userCreate;

            var dbContext = MainUtils.GetDBContext();
            dbContext.Events.InsertOnSubmit(newEvent);
            dbContext.SubmitChanges();
        }
    }
}