using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace AmagiTech.GoogleReCaptcha
{
    public class GoogleReCaptchaManager : IGoogleReCaptchaManager
    {
        private readonly GoogleReCaptchaSettings reCaptchaSettings;
        private readonly IGoogleReCaptchaGateway reCaptchaGateway;
        public GoogleReCaptchaManager(GoogleReCaptchaSettings reCaptchaSettings,
            IGoogleReCaptchaGateway reCaptchaGateway)
        {
            this.reCaptchaSettings = reCaptchaSettings;
            this.reCaptchaGateway = reCaptchaGateway;
        }

        public bool IsValid(HttpContext context)
        {
            var isCaptchaValid = true;
            if (reCaptchaSettings != null
                && reCaptchaSettings.UseCaptcha)
            {
                StringValues _captchaResponse;
                if (context.Request != null
                    && context.Request.Form != null
                    && context.Request.Form.TryGetValue("g-recaptcha-response", out _captchaResponse))
                {
                    var captchaResponse = _captchaResponse.ToString();
                    isCaptchaValid = reCaptchaGateway.SiteVerify(captchaResponse);
                }
                else isCaptchaValid = false;
            }
            return isCaptchaValid;
        }
    }
}
