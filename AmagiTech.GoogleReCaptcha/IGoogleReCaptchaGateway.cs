namespace AmagiTech.GoogleReCaptcha
{
    public interface IGoogleReCaptchaGateway
    {
        public bool SiteVerify(string response, decimal score = 0.5m);
        public string ClientKey { get; }
    }
}
