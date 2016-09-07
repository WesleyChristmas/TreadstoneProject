using System;
using System.Collections.Generic;
using System.Linq;
using WebGrease.Css.Extensions;

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
                          select new { e.IdRecord, e.Name, e.IdPhoto, e.Description, path = PhotoUtils.GetRelativeUrl(p.Path), e.Tag }).ToList();

            List<PCGallery> resultList = new List<PCGallery>();
            foreach (var e in gallery)
            {
                PCGallery pcGallery = new PCGallery();

                pcGallery.idRecord = e.IdRecord;
                pcGallery.name = e.Name;
                pcGallery.idPhoto = e.IdPhoto;
                pcGallery.photoPath = PhotoUtils.GetRelativeUrl(e.path);
                pcGallery.description = e.Description;

                resultList.Add(pcGallery);
            }

            return resultList.OrderByDescending(x => x.idRecord).ToList();
        }

        public static List<PCGallery> GetAllByTags(List<string> tags)
        {
            var dbContext = MainUtils.GetDBContext();
            var galleries = dbContext.GalleryHashtags
                .Where(x => tags.Contains(x.Hashtag))
                .Select(x => x.Gallery).ToList();

            List<PCGallery> resultList = new List<PCGallery>();
            galleries.ForEach(x =>
            {
                PCGallery resGallery = new PCGallery()
                {
                    idRecord = x.IdRecord,
                    name = x.Name,
                    idPhoto = x.IdPhoto,
                    photoPath = PhotoUtils.GetRelativeUrl(x.Photo.Path),
                    description = x.Description,
                };

                resultList.Add(resGallery);
            });
           
            return resultList;
        }

        public static void InsertGallery(PCGallery gallery, string userCreate, PCPhoto image)
        {
            var newGallery = new Gallery();
            newGallery.Name = gallery.name ?? String.Empty;
            newGallery.Description = gallery.description ?? String.Empty;

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

            curGallery.Name = gallery.name ?? String.Empty;
            curGallery.Description = gallery.description ?? String.Empty;

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

        public static List<string> GetHashtags(int id)
        {
            var dbContext = MainUtils.GetDBContext();
            return dbContext.GalleryHashtags
                .Where(x => x.GalleryId == id)
                .Select(x => x.Hashtag)
                .ToList();
        }

        public static void SetHashtags(int id, List<string> inputHashtagsStr)
        {
            var dbContext = MainUtils.GetDBContext();
            var curHashtags = dbContext.GalleryHashtags.Where(x => x.GalleryId == id);
            var curHashtagsStr = curHashtags
                .Select(x => x.Hashtag)
                .ToList();

            // Добавляем новые
            var addHashtagsStr = inputHashtagsStr.Except(curHashtagsStr).ToList();
            addHashtagsStr.ForEach(x =>
            {
                var gh = new GalleryHashtag
                {
                    GalleryId = id,
                    Hashtag = x
                };
                dbContext.GalleryHashtags.InsertOnSubmit(gh);
            });

            // Удаляем лишние
            var delHashtagsStr = curHashtagsStr.Except(inputHashtagsStr);
            var delHashtags = curHashtags.Where(x => delHashtagsStr.Contains(x.Hashtag));
            dbContext.GalleryHashtags.DeleteAllOnSubmit(delHashtags);

            dbContext.SubmitChanges();
        }
    }
}