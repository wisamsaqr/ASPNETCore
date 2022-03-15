using System.Web.Mvc;

namespace MVCIntro.Areas.WithoutSessionCookie
{
    public class WithoutSessionCookieAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WithoutSessionCookie";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WithoutSessionCookie_default",
                "WithoutSessionCookie/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}