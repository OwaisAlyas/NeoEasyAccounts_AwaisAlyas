using System.Web.Mvc;

namespace Neo.EasyAccounts.Web.UI.Areas.Vouchers
{
    public class VouchersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Vouchers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Vouchers_default",
                "Vouchers/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}