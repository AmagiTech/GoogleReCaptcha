using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace AmagiTech.GoogleReCaptcha
{
    public class GoogleReCaptchaGateway : IGoogleReCaptchaGateway
    {
        private readonly GoogleReCaptchaSettings _googleReCaptchaSettings;
        public GoogleReCaptchaGateway(GoogleReCaptchaSettings googleReCaptchaSettings)
        {
            _googleReCaptchaSettings = googleReCaptchaSettings;
        }

        public string ClientKey => _googleReCaptchaSettings.UseCaptcha ? _googleReCaptchaSettings.ClientKey : string.Empty;

        public bool SiteVerify(string response)
        {
            if (!_googleReCaptchaSettings.UseCaptcha)
                return true;
            if (string.IsNullOrWhiteSpace(response))
                return false;
            var httpClient = new HttpClient();

            var httpResponse = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_googleReCaptchaSettings.SecretKey}&response={response}").Result;

            if (httpResponse.StatusCode != HttpStatusCode.OK)
                return false;

            var jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
            var result = JsonSerializer.Deserialize<RecaptchaResponse>(jsonResponse);
            return result.Success;            
        }
    }
}
