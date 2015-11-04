using System.Web;
using BusinessEntity.Models;

namespace PartyCafe.Site.Areas.Admin.Core
{
    public static class ImageSaver
    {
        public static ReceiveFileModel GetSingleImage(HttpRequestBase request,int section)
        {
            ReceiveFileModel image = null;
            if (request.Files.Count == 1)
            {
                var file = request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var content = new byte[file.ContentLength];
                    file.InputStream.Read(content, 0, file.ContentLength);

                    image = new ReceiveFileModel
                    {
                        Data = content,
                        FileName = file.FileName,
                        IdSection = section
                    };
                }
            }

            return image;
        }
    }
}