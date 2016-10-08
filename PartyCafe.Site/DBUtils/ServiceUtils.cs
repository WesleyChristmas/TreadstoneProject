using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace PartyCafe.Site.DBUtils
{
    public class PCServicePhoto
    {
        public int idRecord;
        public string name;
        public string photoPath;
    }

    public class PCServiceVideo
    {
        public int IdRecord;
        public string Name;
        public string Description;
        public string Url;
    }

    public static class ServiceType
    {
        public static int originService = 0;
        public static int aboutService = 1;
    }

    public class PCService
    {
        public int idRecord;
        public string name;
        public string photoPath;
        public string description;
        public string title;
        public int serviceType;
        public List<PCServicePhoto> photos;
        public List<PCServiceVideo> videos;
    }

    public static class ServiceUtils
    {
        public static List<PCService> GetAll(int serviceType = 0)
        {
            var db = MainUtils.GetDBContext();
            var services = (from s in db.Services
                            join p in db.Photos on s.IdPhoto equals p.IdRecord
                            where s.serviceType == serviceType
                            select new { s.IdRecord, s.Name, s.Text, s.Title, p.Path }).ToList();

            return services.Select(item => new PCService
            {
                idRecord = item.IdRecord,
                name = item.Name,
                photoPath = PhotoUtils.GetRelativeUrl(item.Path),
                description = item.Text, title = item.Title
            }).ToList();
        }

        public static PCService GetServiceFull(int id)
        {
            var db = MainUtils.GetDBContext();

            var service = db.Services
                .Where(x => x.IdRecord == id)
                .Join(db.Photos, x => x.IdPhoto, x => x.IdRecord, (s, p) => new PCService()
                {
                    description = s.Text,
                    idRecord = s.IdRecord,
                    name = s.Name,
                    photoPath = PhotoUtils.GetRelativeUrl(p.Path),
                    serviceType = s.serviceType,
                    title = s.Title
                }).SingleOrDefault();

            var servicePhotos = (from sp in db.ServicePhotos
                                 join p in db.Photos on sp.IdPhoto equals p.IdRecord
                                 where sp.IdService == id
                                 select new PCServicePhoto {
                                     idRecord = sp.IdRecord,
                                     name = sp.name,
                                     photoPath = PhotoUtils.GetRelativeUrl(p.Path)
                                 }).ToList();

            var serviceVideos = (from sv in db.ServiceVideos
                                    where sv.IdService == id
                                    select new PCServiceVideo
                                    {
                                        Url = sv.Url,
                                        Description = sv.Description,
                                        Name = sv.Name,
                                        IdRecord = sv.IdRecord
                                    }).ToList();

            service.photos = servicePhotos;
            service.videos = serviceVideos;
            return service;
        }

        public static void InsertService(PCService partyService, string userCreate, PCPhoto image)
        {
            var newService = new Service
            {
                Name = partyService.name ?? string.Empty,
                Text = partyService.description ?? string.Empty,
                serviceType = partyService.serviceType,
                Title = partyService.title,
                IdPhoto = (image != null) ? PhotoUtils.InsertImage(image, userCreate) : 0,
                DateCreate = DateTime.Now,
                UserCreate = userCreate
            };

            var dbContext = MainUtils.GetDBContext();
            dbContext.Services.InsertOnSubmit(newService);
            dbContext.SubmitChanges();
        }

        public static void EditService(PCService partyService, PCPhoto image, string userUpdate)
        {
            var dbContext = MainUtils.GetDBContext();
            var curService = (from e in dbContext.Services
                            where e.IdRecord == partyService.idRecord
                            select e).SingleOrDefault();

            if (curService != null)
            {
                curService.Name = partyService.name ?? string.Empty;
                curService.Text = partyService.description ?? string.Empty;
                curService.Title = partyService.title ?? string.Empty;

                curService.DateUpdate = DateTime.Now;
                curService.UserUpdate = userUpdate;

                if (image != null)
                {
                    if (curService.IdPhoto > 0)
                    {
                        PhotoUtils.EditImage(curService.IdPhoto, image, userUpdate);
                    }
                    else
                    {
                        curService.IdPhoto = PhotoUtils.InsertImage(image, userUpdate);
                    }
                }
            } else return;

            dbContext.SubmitChanges();
        }

        public static void DelService(int idService)
        {
            var dbContext = MainUtils.GetDBContext();
            
            var curService = (from e in dbContext.Services
                            where e.IdRecord == idService
                            select e).SingleOrDefault();

            // Delete subphotos
            var curServicePhotos = (from x in dbContext.ServicePhotos
                                    where x.IdService == idService
                                    select x);
            foreach(var item in curServicePhotos)
            {
                DelPhoto(item.IdRecord);
            }

            // Delete subvideos
            var curServiceVideos = (from x in dbContext.ServiceVideos
                                    where x.IdService == idService
                                    select x);
            foreach (var item in curServiceVideos)
            {
                DelVideo(item.IdRecord);
            }

            dbContext.Services.DeleteOnSubmit(curService);
            dbContext.SubmitChanges();

            if (curService.IdPhoto > 0) { PhotoUtils.DelImage(curService.IdPhoto); };
        }

        public static void AddPhoto(int IdService, string name, PCPhoto image, string userCreate)
        {
            var db = MainUtils.GetDBContext();
            ServicePhoto sp = new ServicePhoto
            {
                IdPhoto = PhotoUtils.InsertImage(image, userCreate),
                IdService = IdService,
                name = name
            };
            db.ServicePhotos.InsertOnSubmit(sp);
            db.SubmitChanges();
        }

        public static void EditPhoto(int idServicePhoto, string name, PCPhoto image, string userUpdate)
        {
            var db = MainUtils.GetDBContext();
            var sp = (from p in db.ServicePhotos
                      where p.IdRecord == idServicePhoto
                      select p).FirstOrDefault();

            if (image != null)
            {
                PhotoUtils.EditImage(sp.IdPhoto, image, userUpdate);
            }

            sp.name = name ?? string.Empty;
            db.SubmitChanges();
        }

        public static void DelPhoto(int idServicePhoto)
        {
            var db = MainUtils.GetDBContext();
            var x = (from sp in db.ServicePhotos
                     where sp.IdRecord == idServicePhoto
                     select sp).SingleOrDefault();

            int idPhoto = x.IdPhoto;

            db.ServicePhotos.DeleteOnSubmit(x);
            db.SubmitChanges();

            PhotoUtils.DelImage(idPhoto);
        }


        public static void AddVideo(int idService, string name, string description, string url)
        {
            var db = MainUtils.GetDBContext();
            ServiceVideos sp = new ServiceVideos
            {
                Name = name,
                IdService = idService,
                Description = description,
                Url = url
            };
            db.ServiceVideos.InsertOnSubmit(sp);
            db.SubmitChanges();
        }


        public static void DelVideo(int idServiceVideo)
        {
            var db = MainUtils.GetDBContext();
            var x = (from sp in db.ServiceVideos
                     where sp.IdRecord == idServiceVideo
                     select sp).SingleOrDefault();

            db.ServiceVideos.DeleteOnSubmit(x);
            db.SubmitChanges();
        }
    }
}