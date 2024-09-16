using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESCHOOLING.Web.Controllers
{
    public class DoctorController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChatWithPatient()
        {
            return View();
        }
    }
}
