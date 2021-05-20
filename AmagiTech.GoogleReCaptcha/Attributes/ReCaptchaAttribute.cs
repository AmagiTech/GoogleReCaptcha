using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmagiTech.GoogleReCaptcha.Attributes
{
    public class ReCaptchaAttribute : ActionFilterAttribute
    {
        private readonly GoogleReCaptchaSettings reCaptchaSettings;
        private readonly IGoogleReCaptchaGateway reCaptchaGateway;
        public ReCaptchaAttribute(GoogleReCaptchaSettings reCaptchaSettings,
            IGoogleReCaptchaGateway reCaptchaGateway)
        {
            this.reCaptchaSettings = reCaptchaSettings;
            this.reCaptchaGateway = reCaptchaGateway;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isCaptchaValid = true;
            if (reCaptchaSettings != null
                && reCaptchaSettings.UseCaptcha)
            {
                StringValues _captchaResponse;
                if (context.HttpContext.Request != null
                    && context.HttpContext.Request.Form != null
                    && context.HttpContext.Request.Form.TryGetValue("g-recaptcha-response", out _captchaResponse))
                {
                    var captchaResponse = _captchaResponse.ToString();
                    isCaptchaValid = reCaptchaGateway.SiteVerify(captchaResponse);
                }
                else isCaptchaValid = false;
            }
            if (!isCaptchaValid)
            {
                context.ModelState.AddModelError("Hata!", "Capthca doğrulamasını geçemediniz!");
            }
            base.OnActionExecuting(context);
        }
    }
}
