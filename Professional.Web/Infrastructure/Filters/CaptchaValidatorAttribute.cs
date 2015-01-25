namespace Professional.Web.Infrastructure.Filters
{
    using System;
    using System.Web.Mvc;
    using Recaptcha.Web;
    using Recaptcha.Web.Mvc;

    public class CaptchaValidatorAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.Controller is Controller)
            {
                var controller = filterContext.Controller as Controller;
                RecaptchaVerificationHelper recaptchaHelper = controller.GetRecaptchaVerificationHelper();

                if (String.IsNullOrEmpty(recaptchaHelper.Response))
                {
                    controller.ModelState.AddModelError("", "Captcha answer cannot be empty.");
                    filterContext.Result = null;
                }
                else
                {
                    RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();

                    if (recaptchaResult != RecaptchaVerificationResult.Success)
                    {
                        controller.ModelState.AddModelError("", "Incorrect captcha answer.");
                        filterContext.Result = null;
                    }
                }             
            }
        }
    }
}