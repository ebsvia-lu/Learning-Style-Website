using System.Web;
using System.Web.Mvc;

namespace UOS.LearningStyle.FinalYear.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
