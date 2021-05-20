using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AmagiTech.GoogleReCaptcha.TagHelpers
{
    [HtmlTargetElement("recaptchaV2")]
    public class GoogleRecaptchaV2TagHelper : TagHelper
    {
        private readonly GoogleReCaptchaSettings googleReCaptchaSettings;
        public GoogleRecaptchaV2TagHelper(GoogleReCaptchaSettings reCaptchaSettings)
        {
            this.googleReCaptchaSettings = reCaptchaSettings;
        }

        [HtmlAttributeName("id")]
        public string Id { get; set; }

        [HtmlAttributeName("style")]
        public string Style { get; set; }

        [HtmlAttributeName("is-form")]
        public bool IsForm { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "";
            if (googleReCaptchaSettings.UseCaptcha)
            {
                var _id = string.IsNullOrWhiteSpace(Id) ? "" : $"id=\"{Id}\"";
                var _style = string.IsNullOrWhiteSpace(Style) ? (IsForm ? "style=\"display:inline-block;\"" : "") : $"style=\"{Style}\"";
                output.Content.SetHtmlContent($"{(IsForm ? "<div class=\"form-row\"><div class=\"col text-center\">" : "")} <div {_id} class=\"g-recaptcha\" {_style} data-sitekey=\"{googleReCaptchaSettings.ClientKey}\" data-callback=\"correctCaptcha\"></div>{(IsForm ? "</div></div>" : "")}");
            }
        }
    }
}
