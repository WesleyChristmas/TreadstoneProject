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
            if (path != String.Empty && path.IndexOf("http") == -1)
            { 
                string PhotoPath = @"../" + System.Configuration.ConfigurationManager.AppSettings["PhotoPath"];
                return  Path.Combine(PhotoPath, Path.GetFileName(path)).Replace('\\', '/');
            } else
            {
                return path;
            }
        }

        private static string GetRandomFileName()
        {
            const int FileNameLength = 8;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            int i = 0;
            string result = "";
            var random = new Random();

            while (i < FileNameLength)
            {
                result += chars[random.Next(chars.Length)];
                i++;
            }
            return result;
        }

        private static string SavePhoto(PCPhoto image)
        {
            string photoPath = System.Configuration.ConfigurationManager.AppSettings["PhotoPath"];
            string serverPhotoPath = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, photoPath);

            if (!Directory.Exists(serverPhotoPath)) { Directory.CreateDirectory(serverPhotoPath); };
            string newFileName = GetRandomFileName();
            string fullPath = serverPhotoPath + newFileName + Path.GetExtension(image.fileName);
            File.WriteAllBytes(fullPath, image.data);
            return fullPath;
        }

        public static int InsertImage(PCPhoto image, string userCreate)
        {   
            var db = MainUtils.GetDBContext();
            var photo = new Photos();

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
                File.Delete(curPhoto.Path);
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
                    File.Delete(curPhoto.Path);
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