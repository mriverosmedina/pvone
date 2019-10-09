using pvone.Common.Models;
using System.Web;

namespace pvone.Backend.Models
{
    public class ProductView : Product
    {
        public HttpPostedFileBase ImageFile { get; set; }

    }
}