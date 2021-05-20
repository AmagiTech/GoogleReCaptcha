using System.Text.Json.Serialization;

namespace AmagiTech.GoogleReCaptcha
{
    public class RecaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}
