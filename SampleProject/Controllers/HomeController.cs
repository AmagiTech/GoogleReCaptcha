using AmagiTech.GoogleReCaptcha;
using AmagiTech.GoogleReCaptcha.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleProject.Models;

namespace SampleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGoogleReCaptchaManager _reCaptchaManager;

        public HomeController(ILogger<HomeController> logger,
            IGoogleReCaptchaManager reCaptchaManager)
        {
            _logger = logger;
            _reCaptchaManager = reCaptchaManager;
        }


        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ServiceFilter(typeof(ReCaptchaAttribute))]
        public IActionResult WithAttribute(SampleModel model)
        {
            if (ModelState.IsValid)
            {
                // Recaptcha İşlemini Geçti
                return RedirectToAction("Success");
            }
            return View("Index", model);
        }

        [HttpPost]
        public IActionResult WithoutAttribute(SampleModel model)
        {
            var isValid = _reCaptchaManager.IsValid(HttpContext);
            if (!isValid)
                ModelState.AddModelError("Error", "Captcha validation failed!");
            if (ModelState.IsValid)
            {
                return RedirectToAction("Success");
            }

            return View("Index", model);
        }

        public string Success()
        {
            return "Başarılı";
        }
    }
}
