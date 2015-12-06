using System.Web.Mvc;
using BusinessInterface;
using Db.Service;

namespace PartyCafe.Site.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: Admin/Blog
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BlogTable()
        {
            return View("BlogTable");
        }

        [HttpGet]
        public ActionResult BlogEdit()
        {
            return View("BlogEdit");
        }

        [HttpGet]
        public JsonResult GetBlogs()
        {
            return Json(_blogService.GetBlogs(),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBlogDetail(int idBlog)
        {
            return Json(_blogService.GetBlogDetails(idBlog), JsonRequestBehavior.AllowGet);
        }
    }
}