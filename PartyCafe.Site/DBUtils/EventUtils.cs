using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class PCEventPhoto
    {
        public int idRecord;
        public string name;
        public string photoPath;
    }

    public class PCEvent
    {
        public int idRecord;
        public string name;
        public int IdPhoto;
        public string PhotoPath;
        public string Description;
        public DateTime DateEvent;
        public TimeSpan TimeEvent;
        public List<PCEventPhoto> photos;
    }

    public static class EventUtils
    {
        public static List<PCEvent> GetAll()
        {
            var db = MainUtils.GetDBContext();
            var events = (from e in db.Events
                          join p in db.Photos on e.IdPhoto equals p.IdRecord
                          select new {e.IdRecord, e.Name, e.IdPhoto, e.EventDate, p.Path, e.description}).ToList();

            var eventPhotos = (from ep in db.EventPhotos
                            join p in db.Photos on ep.IdPhoto equals p.IdRecord
                            select new { ep.IdRecord, ep.IdEvent, p.Path, ep.name }).ToList();


            List<PCEvent> resultList = new List<PCEvent>();
            foreach (var e in events)
            {
                PCEvent pcEvent = new PCEvent();

                pcEvent.idRecord = e.IdRecord;
                pcEvent.name = e.Name;
                pcEvent.IdPhoto = e.IdPhoto;
                pcEvent.PhotoPath = PhotoUtils.GetRelativeUrl(e.Path);
                pcEvent.DateEvent = e.EventDate.Date;
                pcEvent.TimeEvent = e.EventDate.TimeOfDay;
                pcEvent.Description = e.description;

                pcEvent.photos = new List<PCEventPhoto>();
                foreach(var item in eventPhotos)
                {
                    if (item.IdEvent == pcEvent.idRecord)
                    {
                        var newPhoto = new PCEventPhoto();
                        newPhoto.idRecord = item.IdRecord;
                        newPhoto.photoPath = PhotoUtils.GetRelativeUrl(item.Path);
                        newPhoto.name = item.name;
                        pcEvent.photos.Add(newPhoto);
                    }
                }

                resultList.Add(pcEvent);
            }

            return resultList;
        }

        public static List<PCEvent> GetNearEvents()
        {
            const int DayInterval = 15;

            var db = MainUtils.GetDBContext();
            var events = (from e in db.Events
                          join p in db.Photos on e.IdPhoto equals p.IdRecord
                          where e.EventDate <= DateTime.Now.AddDays(DayInterval) && e.EventDate >= DateTime.Now.AddDays(-DayInterval)
                          select new { e.IdRecord, e.Name, e.IdPhoto, e.EventDate, p.Path, e.description }).ToList();

            var eventPhotos = (from ep in db.EventPhotos
                               join p in db.Photos on ep.IdPhoto equals p.IdRecord
                               select new { ep.IdRecord, ep.IdEvent, p.Path, ep.name }).ToList();


            List<PCEvent> resultList = new List<PCEvent>();
            foreach (var e in events)
            {
                PCEvent pcEvent = new PCEvent();

                pcEvent.idRecord = e.IdRecord;
                pcEvent.name = e.Name;
                pcEvent.IdPhoto = e.IdPhoto;
                pcEvent.PhotoPath = PhotoUtils.GetRelativeUrl(e.Path);
                pcEvent.DateEvent = e.EventDate.Date;
                pcEvent.TimeEvent = e.EventDate.TimeOfDay;
                pcEvent.Description = e.description;

                pcEvent.photos = new List<PCEventPhoto>();
                foreach (var item in eventPhotos)
                {
                    if (item.IdEvent == pcEvent.idRecord)
                    {
                        var newPhoto = new PCEventPhoto();
                        newPhoto.idRecord = item.IdRecord;
                        newPhoto.photoPath = PhotoUtils.GetRelativeUrl(item.Path);
                        newPhoto.name = item.name;
                        pcEvent.photos.Add(newPhoto);
                    }
                }

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
            curEvent.description = partyEvent.Description != null ? partyEvent.Description : String.Empty;
            
            if (partyEvent.DateEvent != null)
            {
                DateTime newDate = partyEvent.DateEvent;
                if (partyEvent.TimeEvent != null)
                {
                    newDate = newDate.AddSeconds(partyEvent.TimeEvent.TotalSeconds);
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
            newEvent.Name = partyEvent.name != null ? partyEvent.name : String.Empty;
            newEvent.description = partyEvent.Description != null ? partyEvent.Description : String.Empty;

            if (partyEvent.DateEvent != null)
            {
                DateTime newDate = partyEvent.DateEvent;
                if (partyEvent.TimeEvent != null)
                {
                   newDate = newDate.AddSeconds(partyEvent.TimeEvent.TotalSeconds);
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

        public static void AddPhoto(int IdEvent, string name, PCPhoto image, string userCreate)
        {
            var db = MainUtils.GetDBContext();
            EventPhotos ep = new EventPhotos();
            ep.IdPhoto = PhotoUtils.InsertImage(image, userCreate);
            ep.IdEvent = IdEvent;
            ep.name = name;
            db.EventPhotos.InsertOnSubmit(ep);
            db.SubmitChanges();
        }

        public static void DelPhoto(int IdEvent)
        {
            var db = MainUtils.GetDBContext();
            var x = (from sp in db.EventPhotos
                     where sp.IdPhoto == IdEvent
                     select sp).SingleOrDefault();
            db.EventPhotos.DeleteOnSubmit(x);
            db.SubmitChanges();
        }
    }
}