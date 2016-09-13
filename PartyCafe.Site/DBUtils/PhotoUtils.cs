using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace PartyCafe.Site.DBUtils
{
    public class PCPhoto
    {
        public string fileName;
        public byte[] data;
    }

    public static class PhotoUtils
    {   
        public static string GetRelativeUrl(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                string photoPath = @"/" + System.Configuration.ConfigurationManager.AppSettings["PhotoPath"].Replace(@"\", "/");
                return Path.Combine(photoPath, path);
            }
            else return null;
        }

        private static string GetPhysicalPhotoPath(string photoFilename)
        {
            string photoPath = System.Configuration.ConfigurationManager.AppSettings["PhotoPath"];
            string serverPhotoPath = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath,
                photoPath);

            return Path.Combine(serverPhotoPath, photoFilename);
        }

        private static string SavePhoto(PCPhoto image)
        {
            string photoPath = System.Configuration.ConfigurationManager.AppSettings["PhotoPath"];
            string serverPhotoPath = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, photoPath);

            if (!Directory.Exists(serverPhotoPath)) { Directory.CreateDirectory(serverPhotoPath); };
            string newFileName = Path.GetFileNameWithoutExtension(image.fileName) + "_" + Guid.NewGuid() + Path.GetExtension(image.fileName);
            string fullPath = serverPhotoPath + newFileName;
            File.WriteAllBytes(fullPath, image.data);
            return newFileName;
        }

        public static int InsertImage(PCPhoto image, string userCreate)
        {   
            var db = MainUtils.GetDBContext();
            var photo = new Photo();

            photo.FileName = image.fileName; 
            photo.Path = SavePhoto(image);
            photo.UserCreate = userCreate;
            photo.DateCreate = DateTime.Now;

            db.Photos.InsertOnSubmit(photo);
            db.SubmitChanges();

            return photo.IdRecord;
        }

        public static void EditImage(int id, PCPhoto image, string userUpdate)
        {
            var db = MainUtils.GetDBContext();
            var curPhoto = (from p in db.Photos
                            where p.IdRecord == id
                            select p).SingleOrDefault();
            try
            {
                File.Delete(GetPhysicalPhotoPath(curPhoto.Path));
            }
            catch (Exception ex) { }

            curPhoto.Path = SavePhoto(image);
            curPhoto.FileName = image.fileName;
            curPhoto.UserUpdate = userUpdate;
            curPhoto.DateUpdate = DateTime.Now;

            db.SubmitChanges();
        }

        public static void DelImage(int id)
        {
            if (id > 0)
            { 
                var db = MainUtils.GetDBContext();
                var curPhoto = (from p in db.Photos
                                where p.IdRecord == id
                                select p).SingleOrDefault();
                try
                { 
                    File.Delete(GetPhysicalPhotoPath(curPhoto.Path));
                }
                catch (Exception ex)
                {
                    // NO LOGGER IB PROJECT  AHAHAHAHHA!
                }
                db.Photos.DeleteOnSubmit(curPhoto);

                db.SubmitChanges();
            }
        }
    }
}