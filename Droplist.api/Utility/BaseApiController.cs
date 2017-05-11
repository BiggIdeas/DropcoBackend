using Droplist.api.Data;
using Droplist.api.Models;
using System.Linq;
using System.Web.Http;

namespace Droplist.api.Utility
{
    public class BaseApiController : ApiController
    {
        protected DroplistDataContext db = new Data.DroplistDataContext();

        public User CurrentUser
        {
            get
            {
                var userName = base.User.Identity.Name;
                var user = db.Users.FirstOrDefault(u => u.UserName == userName);

                return user;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
