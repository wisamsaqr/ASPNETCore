using System.Web.Mvc;

namespace MVCIntro.Areas.WithCookie
{
    public class WithCookieAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WithCookie";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WithCookie_default",
                "WithCookie/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}