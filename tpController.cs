using Microsoft.AspNetCore.Mvc;

namespace job_api.Controllers
{
    public class tpController : Controller
    {
        public IActionResult job()
        {
            return View();
        }
    }
}
