using System.Web.Mvc;

namespace MusicCompany.Website.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /About/
		[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            return View();
        }

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult TermsOfService()
		{
			return View();
		}

    }
}
