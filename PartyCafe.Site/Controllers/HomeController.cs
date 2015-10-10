using System.Web.Mvc;
using Db.Service;
using Entity;
using Entity.Model;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.UnitOfWork;

namespace PartyCafe.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using(IDataContextAsync context = new PartyCafeDbContext())
            using(IUnitOfWorkAsync uof = new UnitOfWork(context))
            {
                IFoodMenuService test = new FoodMenuService(new Repository<FoodMenu>(context,uof));
                var please = test.GetAllMenu();
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}