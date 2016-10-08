using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PartyCafe.Site.DBUtils;

namespace PartyCafe.Site.Models.Utils
{
    public static class ControllerUtils
    {
        public static PCPhoto GetPhotoEntity(HttpFileCollectionBase files, string hashtag)
        {
            /*
            if (files.Count == 0) return null;

            var file = files[0];
            var content = new byte[file.ContentLength];
            var filename = file.FileName;
            file.InputStream.Read(content, 0, file.ContentLength);
            return new PCPhoto()
            {
                data = content,
                fileName = filename,
                hashtag = hashtag
            };
            */
            byte[] content = null;
            string filename = null;
            if (files.Count > 0)
            {
                var file = files[0];
                content = new byte[file.ContentLength];
                filename = file.FileName;
                file.InputStream.Read(content, 0, file.ContentLength);
            }

            return new PCPhoto()
            {
                data = content,
                fileName = filename,
                hashtag = hashtag
            };
        }
    }
}