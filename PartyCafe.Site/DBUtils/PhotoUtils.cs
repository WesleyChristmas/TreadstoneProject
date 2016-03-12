using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;


namespace PartyCafe.Site.DBUtils
{
    public class Photo
    {
        public string fileName;
        public byte[] data;
    }

    public static class PhotoUtils
    {   
        private static string GetRandomFileName()
        {
            const int FileNameLength = 8;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            int i = 0;
            string result = "";
            var random = new Random();

            while (i < FileNameLength)
            {
                result += chars[random.Next(8)]; 
            }
            return result;
        }

        private static string SavePhoto(Photo image)
        {
            const string ServerPath = "./Content/photos/";

            if (!Directory.Exists(ServerPath)) { Directory.CreateDirectory(ServerPath); };
            string fileName = GetRandomFileName();
            string fullPath = ServerPath + fileName + Path.GetExtension(image.fileName);
            File.WriteAllBytes(fullPath, image.data);
            return fullPath;
        }

        public static int InsertImage(Photo image, string userCreate)
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

        public static void EditImage(int id, Photo image, string userUpdate)
        {
            var db = MainUtils.GetDBContext();
            var curPhoto = (from p in db.Photos
                            where p.IdRecord == id
                            select p).SingleOrDefault();

            File.Delete(curPhoto.Path);

            curPhoto.Path = SavePhoto(image);
            curPhoto.FileName = image.fileName;
            curPhoto.UserUpdate = userUpdate;
            curPhoto.DateUpdate = DateTime.Now;

            db.SubmitChanges();
        }

        public static void DelImage(int Id)
        {
            throw new NotImplementedException();
        }
    }
}