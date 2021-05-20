namespace AmagiTech.GoogleReCaptcha
{
    public interface IGoogleReCaptchaGateway
    {
        public bool SiteVerify(string response);
        public string ClientKey { get; }
    }
}
