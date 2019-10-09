using System.IO;
using System.Web;

namespace pvone.Backend.Helpers
{
    public class FilesHelpers
    {
        public static string UploadPhotos(HttpPostedFileBase file, string folder)
        {
            string path = string.Empty;
            string pic = string.Empty;
            if (file != null)
            {
                pic = Path.GetFileName(file.FileName);
                path = Path.Combine(HttpContext.Current.Server.MapPath(folder), pic);
                file.SaveAs(path);
            }
            return pic;

        }
    }
}