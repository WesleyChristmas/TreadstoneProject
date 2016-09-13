using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Models.Utils
{
    public static class ControllerUtils
    {
        public static PCPhoto GetPhotoEntity(HttpFileCollectionBase files)
        {
            if (files.Count == 0) return null;

            var file = files[0];
            var content = new byte[file.ContentLength];
            var filename = file.FileName;
            file.InputStream.Read(content, 0, file.ContentLength);
            return new PCPhoto()
            {
                data = content,
                fileName = filename
            };
        }
    }
}