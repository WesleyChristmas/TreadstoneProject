using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartyCafe.Site.DBUtils
{
    public class AboutData
    {
        public string Name;
        public string Description;
        public string PhotoPath;
        public int IdPhoto;
    }

    public static class AboutUtils
    {
        public static void UpdateAbout(AboutData data)
        {
            var db = MainUtils.GetDBContext();
            var curData = db.Abouts.SingleOrDefault();
            curData.Description = data.Description;
            curData.Name = data.Name;
            db.SubmitChanges();
        }

        public static void UpdateMainPhoto(PCPhoto photo)
        {
            var db = MainUtils.GetDBContext();
            var curData = db.Abouts.SingleOrDefault();

            if (curData.IdPhoto > 0)
                PhotoUtils.EditImage(curData.IdPhoto, photo, "Admin");
            else
                curData.IdPhoto = PhotoUtils.InsertImage(photo, "Admin");

            db.SubmitChanges();
        }

        public static AboutData GetAbout()
        {
            var db = MainUtils.GetDBContext();
            return db.Abouts.Join(db.Photos, x => x.IdPhoto, x => x.IdRecord, (a, p) => new AboutData()
                {
                    Description = a.Description,
                    Name = a.Name,
                    IdPhoto =  a.IdPhoto,
                    PhotoPath = PhotoUtils.GetRelativeUrl(p.Path)
                }).SingleOrDefault();
        }
    }
}