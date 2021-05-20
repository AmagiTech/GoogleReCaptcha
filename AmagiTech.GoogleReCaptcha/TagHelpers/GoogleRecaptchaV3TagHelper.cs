using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;

namespace AmagiTech.GoogleReCaptcha.TagHelpers
{
    [HtmlTargetElement("recaptchaV3Button")]
    public class GoogleRecaptchaV3TagHelper : TagHelper
    {
        private readonly GoogleReCaptchaSettings googleReCaptchaSettings;
        public GoogleRecaptchaV3TagHelper(GoogleReCaptchaSettings reCaptchaSettings)
        {
            this.googleReCaptchaSettings = reCaptchaSettings;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            if (googleReCaptchaSettings.UseCaptcha)
            {
                var classAttribute = output.Attributes.FirstOrDefault(q => q.Name == "class");
                if (classAttribute != null)
                {
                    var value = classAttribute.Value != null ? classAttribute.Value.ToString() : string.Empty;
                    if (!value.Contains("g-recaptcha"))
                    {
                        output.Attributes.SetAttribute("class", "g-recaptcha " + value);
                    }
                }
                else
                {
                    output.Attributes.Add("class", "g-recaptcha");
                }
                output.Attributes.Add("data-sitekey", googleReCaptchaSettings.ClientKey);
            }
        }
    }
}
