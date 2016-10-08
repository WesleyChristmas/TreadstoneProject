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
        public bool IsOpen;
        public List<PCEventPhoto> photos;
    }

    public static class EventUtils
    {
        public static List<PCEvent> GetAll()
        {
            var db = MainUtils.GetDBContext();
            return (from e in db.Events
                          join p in db.Photos on e.IdPhoto equals p.IdRecord
                          select new PCEvent()
                          {
                              idRecord = e.IdRecord,
                              name = e.Name,
                              IdPhoto = e.IdPhoto,
                              PhotoPath = PhotoUtils.GetRelativeUrl(p.Path),
                              DateEvent = e.EventDate.Date,
                              TimeEvent = e.EventDate.TimeOfDay,
                              Description = e.description,
                              IsOpen = e.isOpen
                          }).ToList();
        }

        public static PCEvent GetEventPhotos(int id)
        {
            var db = MainUtils.GetDBContext();

            var curEvent = (from e in db.Events
                where e.IdRecord == id
                join p in db.Photos on e.IdPhoto equals p.IdRecord
                select new PCEvent()
                {
                    idRecord = e.IdRecord,
                    name = e.Name,
                    Description = e.description,
                    DateEvent = e.EventDate.Date,
                    TimeEvent = e.EventDate.TimeOfDay,
                    IdPhoto = e.IdPhoto,
                    PhotoPath = PhotoUtils.GetRelativeUrl(p.Path),
                    IsOpen = e.isOpen
                }).SingleOrDefault();

            curEvent.photos = db.EventPhotos
                .Where(x => x.IdEvent == id)
                .Join(db.Photos, photo => photo.IdPhoto, eventPhoto => eventPhoto.IdRecord,
                    (eventPhoto, photo) => new PCEventPhoto()
                    {
                        idRecord = photo.IdRecord,
                        photoPath = PhotoUtils.GetRelativeUrl(photo.Path),
                        name = eventPhoto.name
                    }).ToList();

            return curEvent;
        }

        public static List<PCEvent> GetNearEvents()
        {
            const int dayInterval = 15;

            var db = MainUtils.GetDBContext();
            return (from e in db.Events
                    join p in db.Photos on e.IdPhoto equals p.IdRecord
                    where e.EventDate <= DateTime.Now.AddDays(dayInterval) && e.EventDate >= DateTime.Now.AddDays(-dayInterval)
                    select new PCEvent()
                          {
                              idRecord = e.IdRecord,
                              name = e.Name,
                              IdPhoto = e.IdPhoto,
                              PhotoPath = PhotoUtils.GetRelativeUrl(p.Path),
                              DateEvent = e.EventDate.Date,
                              TimeEvent = e.EventDate.TimeOfDay,
                              Description = e.description,
                              IsOpen = e.isOpen
                          }).ToList();
        }

        public static void DelEvent(int idRecord)
        {
            var dbContext = MainUtils.GetDBContext();
            var curEvent = (from e in dbContext.Events
                          where e.IdRecord == idRecord
                            select e).SingleOrDefault();

            if (curEvent == null) return;
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

            if (curEvent == null) return;
            curEvent.Name = partyEvent.name ?? string.Empty;
            curEvent.description = partyEvent.Description ?? string.Empty;
            curEvent.isOpen = partyEvent.IsOpen;

            var newDate = partyEvent.DateEvent;
            newDate = newDate.AddSeconds(partyEvent.TimeEvent.TotalSeconds);
            curEvent.EventDate = newDate;

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

        public static void InsertEvent(PCEvent partyEvent, PCPhoto image, string userCreate)
        {

            var newDate = partyEvent.DateEvent;
            newDate = newDate.AddSeconds(partyEvent.TimeEvent.TotalSeconds);

            var newEvent = new Events
            {
                Name = partyEvent.name ?? string.Empty,
                description = partyEvent.Description ?? string.Empty,
                IdPhoto = image != null ? PhotoUtils.InsertImage(image, userCreate) : 0,
                DateCreate = DateTime.Now,
                UserCreate = userCreate,
                EventDate = newDate,
                isOpen = partyEvent.IsOpen
            };

            var dbContext = MainUtils.GetDBContext();
            dbContext.Events.InsertOnSubmit(newEvent);
            dbContext.SubmitChanges();
        }

        public static void AddPhoto(int IdEvent, string name, PCPhoto image, string userCreate)
        {
            var db = MainUtils.GetDBContext();
            var ep = new EventPhoto
            {
                IdPhoto = PhotoUtils.InsertImage(image, userCreate),
                IdEvent = IdEvent,
                name = name
            };
            db.EventPhotos.InsertOnSubmit(ep);
            db.SubmitChanges();
        }

        public static void DelPhoto(int IdEvent)
        {
            var db = MainUtils.GetDBContext();
            var x = (from sp in db.EventPhotos
                     where sp.IdPhoto == IdEvent
                     select sp).SingleOrDefault();
            if (x == null) return;
            db.EventPhotos.DeleteOnSubmit(x);
            db.SubmitChanges();
        }
    }
}