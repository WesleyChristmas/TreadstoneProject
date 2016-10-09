using System;
using System.Collections.Generic;
using System.Linq;

namespace PartyCafe.Site.DBUtils
{
    public class PCGallery
    {
        public int idRecord;
        public string name;
        public string description;
        public string photoPath;
        public string hashtag;
        public int idPhoto;
        public List<string> hashtags;
    }

    public class Hashtag
    {
        public string Tag;
        public int Count;
    }

    public class GalleryUtils
    {
        public static List<Hashtag> GetAllHashtags()
        {
            var db = MainUtils.GetDBContext();
            return db.GalleryHashtags
                .GroupBy(x => x.Hashtag)
                .Select(x => new Hashtag() {Tag = x.Key, Count = x.Count()})
                .ToList();
        }

        public static List<PCGallery> GetAll(int startPosition, int needCount)
        {
            needCount = (needCount < 1) ? 1 : needCount;

            var dbContext = MainUtils.GetDBContext();
            var gallery = (dbContext.Gallery.OrderByDescending(x => x.IdRecord)
                    .Join(dbContext.Photos, e => e.IdPhoto, p => p.IdRecord,
                        (e, p) =>
                            new
                            {
                                e.IdRecord,
                                e.Name,
                                e.IdPhoto,
                                e.Description,
                                path = PhotoUtils.GetRelativeUrl(p.Path),
                                hashtags = e.GalleryHashtags.Select(x => x.Hashtag)
                            })
            ).ToList();

            var fullCount = gallery.Count;
            if (fullCount == 0)
                return new List<PCGallery>();

            var reminder = fullCount - (startPosition);
            var countToTake = (reminder < needCount) ? reminder : needCount;
            if (countToTake <= 0)
                return new List<PCGallery>();

            gallery = gallery.GetRange(startPosition, countToTake);

            return  gallery.Select(e => new PCGallery
            {
                idRecord = e.IdRecord,
                name = e.Name,
                idPhoto = e.IdPhoto,
                photoPath = PhotoUtils.GetRelativeUrl(e.path),
                description = e.Description,
                hashtags = e.hashtags.ToList()
            }).ToList();
        }

        public static List<PCGallery> GetAllByTags(List<string> tags, int startPosition, int needCount)
        {
            var dbContext = MainUtils.GetDBContext();
            var galleries = dbContext.GalleryHashtags
                .Where(x => tags.Contains(x.Hashtag))
                .Select(x => x.Gallery).ToList();

            var fullCount = galleries.Count;
            if (fullCount == 0)
                return new List<PCGallery>();

            var reminder = fullCount - (startPosition);
            var countToTake = (reminder < needCount) ? reminder : needCount;
            if (countToTake <= 0)
                return new List<PCGallery>();
            galleries = galleries.GetRange(startPosition, countToTake);

            return galleries.Select(x => new PCGallery
            {
                idRecord = x.IdRecord,
                name = x.Name,
                idPhoto = x.IdPhoto,
                photoPath = PhotoUtils.GetRelativeUrl(x.Photo.Path),
                description = x.Description,
                hashtags = x.GalleryHashtags.Select(tag => tag.Hashtag).ToList()
            }).ToList();
        }

        public static void InsertGallery(PCGallery gallery, PCPhoto image, string userCreate)
        {
            var newGallery = new Gallery
            {
                Name = gallery.name ?? string.Empty,
                Description = gallery.description ?? string.Empty,
                IdPhoto = (image != null) ? PhotoUtils.InsertImage(image, userCreate) : 0,
                DateCreate = DateTime.Now,
                UserCreate = userCreate
            };

            var dbContext = MainUtils.GetDBContext();
            dbContext.Gallery.InsertOnSubmit(newGallery);
            dbContext.SubmitChanges();
        }

        public static void EditGallery(PCGallery gallery, PCPhoto image, string userUpdate)
        {
            var dbContext = MainUtils.GetDBContext();
            var curGallery = (from e in dbContext.Gallery
                            where e.IdRecord == gallery.idRecord
                            select e).SingleOrDefault();
            if (curGallery == null) return;

            curGallery.Name = gallery.name ?? string.Empty;
            curGallery.Description = gallery.description ?? string.Empty;

            curGallery.DateUpdate = DateTime.Now;
            curGallery.UserUpdate = string.IsNullOrWhiteSpace(userUpdate) ? "Admin" : userUpdate;

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

            if (curGallery == null) return;
            dbContext.Gallery.DeleteOnSubmit(curGallery);
            dbContext.SubmitChanges();

            if (curGallery.IdPhoto > 0) { PhotoUtils.DelImage(curGallery.IdPhoto); };
        }

        public static List<string> GetHashtagsByPhotoId(int id)
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