using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class PCGallery
    {
        public int idRecord;
        public string name;
        public string description;
        public string photoPath;
        public int idPhoto;
    }

    public class GalleryUtils
    {

        public static List<PCGallery> GetAll()
        {
            var dbContext = MainUtils.GetDBContext();
            var gallery = (from e in dbContext.Gallery
                          join p in dbContext.Photos on e.IdPhoto equals p.IdRecord
                          select new { e.IdRecord, e.Name, e.IdPhoto, e.Description, p.Path }).ToList();

            List<PCGallery> resultList = new List<PCGallery>();
            foreach (var e in gallery)
            {
                PCGallery pcGallery = new PCGallery();

                pcGallery.idRecord = e.IdRecord;
                pcGallery.name = e.Name;
                pcGallery.idPhoto = e.IdPhoto;
                pcGallery.photoPath = e.Path;
                pcGallery.description = e.Description;

                resultList.Add(pcGallery);
            }

            return resultList;
        }

        public static void InsertGallery(PCGallery gallery, string userCreate, PCPhoto image)
        {
            var newGallery = new Gallery();
            newGallery.Name = gallery.name != null ? gallery.name : String.Empty;
            newGallery.Description = gallery.description != null ? gallery.description : String.Empty; ;
            if (image != null)
            {
                newGallery.IdPhoto = PhotoUtils.InsertImage(image, userCreate);
            }
            else
            {
                newGallery.IdPhoto = 0;
            }

            newGallery.DateCreate = DateTime.Now;
            newGallery.UserCreate = userCreate;

            var dbContext = MainUtils.GetDBContext();
            dbContext.Gallery.InsertOnSubmit(newGallery);
            dbContext.SubmitChanges();
        }

        public static void EditGallery(PCGallery gallery, string userUpdate, PCPhoto image)
        {
            var dbContext = MainUtils.GetDBContext();
            var curGallery = (from e in dbContext.Gallery
                            where e.IdRecord == gallery.idRecord
                            select e).SingleOrDefault();

            curGallery.Name = gallery.name != null ? gallery.name : String.Empty;
            curGallery.Description = gallery.name != null ? gallery.name : String.Empty;

            curGallery.DateUpdate = DateTime.Now;
            curGallery.UserUpdate = String.IsNullOrWhiteSpace(userUpdate) ? "Admin" : userUpdate;

            if (image != null)
            { 
                if (curGallery.IdPhoto > 0)
                {
                    PhotoUtils.EditImage(curGallery.IdPhoto, image, userUpdate);
                }
                else
                {
                    curGallery.IdPhoto = PhotoUtils.InsertImage(image, userUpdate);
                }
            }

            dbContext.SubmitChanges();

        }

        public static void DelGallery(int idRecord)
        {
            var dbContext = MainUtils.GetDBContext();
            var curGallery = (from e in dbContext.Gallery
                            where e.IdRecord == idRecord
                            select e).SingleOrDefault();

            dbContext.Gallery.DeleteOnSubmit(curGallery);
            dbContext.SubmitChanges();

            if (curGallery.IdPhoto > 0) { PhotoUtils.DelImage(curGallery.IdPhoto); };
        }
    }
}