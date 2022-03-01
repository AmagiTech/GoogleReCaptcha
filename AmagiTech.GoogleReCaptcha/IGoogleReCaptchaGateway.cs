namespace AmagiTech.GoogleReCaptcha
{
    public interface IGoogleReCaptchaGateway
    {
        bool SiteVerify(string response, decimal score = 0.5m);
        string ClientKey { get; }
    }
}
