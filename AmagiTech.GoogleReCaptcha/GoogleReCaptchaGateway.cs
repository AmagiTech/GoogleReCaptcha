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

        public bool SiteVerify(string response, decimal score = 0.5m)
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

            if (!result.Success)
                return false;

            decimal _score = 1;
            if (result.Score != null)
                _score = result.Score.Value;
            return result.Success && _score >= score;
        }
    }
}
