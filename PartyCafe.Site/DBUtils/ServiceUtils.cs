using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class PCServicePhoto
    {
        public int idRecord;
        public string name;
        public string photoPath;
    }

    public class PCService
    {
        public int idRecord;
        public string name;
        public string photoPath;
        public string title;
        public string description;
        public List<PCServicePhoto> photos;
    }

    public static class ServiceUtils
    {
        public static List<PCService> GetAll()
        {
            var db = MainUtils.GetDBContext();
            var services = (from s in db.Services
                            join p in db.Photos on s.IdPhoto equals p.IdRecord
                            select new { s.IdRecord, s.Name, s.Text, p.Path }).ToList();

            var servicePhotos = (from sp in db.ServicePhotos
                                 join p in db.Photos on sp.IdPhoto equals p.IdRecord
                                 select new { sp.IdRecord, sp.IdService, p.Path, sp.name }).ToList();

            List<PCService> result = new List<PCService>();
            foreach (var item in services)
            {
                var newItem = new PCService();

                newItem.idRecord = item.IdRecord;
                newItem.name = item.Name;
                newItem.photoPath = item.Path;
                newItem.description = item.Text;

                newItem.photos = new List<PCServicePhoto>();
                foreach (var p in servicePhotos)
                {
                    if (p.IdService == newItem.idRecord)
                    {
                        var newPhoto = new PCServicePhoto();
                        newPhoto.idRecord = p.IdRecord;
                        newPhoto.photoPath = p.Path;
                        newPhoto.name = p.name;
                        newItem.photos.Add(newPhoto);
                    }
                }
                result.Add(newItem);
            }

            return result;
        }

        public static void InsertService(PCService partyService, string userCreate, PCPhoto image)
        {
            var newService = new Services();
            newService.Name = partyService.name != null ? partyService.name : String.Empty;
            newService.Text = partyService.description != null ? partyService.description : String.Empty;

            if (image != null)
            {
                newService.IdPhoto = PhotoUtils.InsertImage(image, userCreate);
            }
            else
            {
                newService.IdPhoto = 0;
            }

            newService.DateCreate = DateTime.Now;
            newService.UserCreate = userCreate;

            var dbContext = MainUtils.GetDBContext();
            dbContext.Services.InsertOnSubmit(newService);
            dbContext.SubmitChanges();
        }

        public static void EditService(PCService partyService, string userUpdate, PCPhoto image)
        {
            var dbContext = MainUtils.GetDBContext();
            var curService = (from e in dbContext.Services
                            where e.IdRecord == partyService.idRecord
                            select e).SingleOrDefault();

            curService.Name = partyService.name != null ? partyService.name : String.Empty;
            curService.Text = partyService.description != null ? partyService.description : String.Empty;

            curService.DateUpdate = DateTime.Now;
            curService.UserUpdate = userUpdate;

            if (image != null)
            {
                if (curService.IdPhoto > 0)
                {
                    PhotoUtils.EditImage(curService.IdPhoto, image, userUpdate);
                }
                else {
                    curService.IdPhoto = PhotoUtils.InsertImage(image, userUpdate);
                }
            }

            dbContext.SubmitChanges();
        }

        public static void DelService(int IdService)
        {
            var dbContext = MainUtils.GetDBContext();
            var curService = (from e in dbContext.Services
                            where e.IdRecord == IdService
                            select e).SingleOrDefault();

            dbContext.Services.DeleteOnSubmit(curService);
            dbContext.SubmitChanges();

            if (curService.IdPhoto > 0) { PhotoUtils.DelImage(curService.IdPhoto); };
        }

        public static void AddPhoto(int IdService, string name, PCPhoto image, string userCreate)
        {
            var db = MainUtils.GetDBContext();
            ServicePhotos sp = new ServicePhotos();
            sp.IdPhoto = PhotoUtils.InsertImage(image, userCreate);
            sp.IdService = IdService;
            sp.name = name;
            db.ServicePhotos.InsertOnSubmit(sp);
            db.SubmitChanges();
        }

        public static void DelPhoto(int IdPhoto)
        {
            var db = MainUtils.GetDBContext();
            var x = (from sp in db.ServicePhotos
                     where sp.IdPhoto == IdPhoto
                     select sp).SingleOrDefault();
            db.ServicePhotos.DeleteOnSubmit(x);
            db.SubmitChanges();
        }
    }
}