using Microsoft.AspNetCore.Http;

namespace AmagiTech.GoogleReCaptcha
{
    public interface IGoogleReCaptchaManager
    {
        bool IsValid(HttpContext context);
    }
}
