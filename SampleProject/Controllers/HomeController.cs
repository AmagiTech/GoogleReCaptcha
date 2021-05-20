using AmagiTech.GoogleReCaptcha.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        [ServiceFilter(typeof(ReCaptchaAttribute))]
        public IActionResult Index(SampleModel model)
        {
            if (ModelState.IsValid)
            {
                // Recaptcha İşlemini Geçti
                return RedirectToAction("Success");
            }    
            return View(model);
        }

        public string Success()
        {
            return "Başarılı";
        }
    }
}
