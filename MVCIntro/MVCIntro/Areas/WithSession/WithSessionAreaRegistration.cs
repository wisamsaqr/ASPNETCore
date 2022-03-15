using System.Web.Mvc;

namespace MVCIntro.Areas.WithSession
{
    public class WithSessionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WithSession";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WithSession_default",
                "WithSession/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}