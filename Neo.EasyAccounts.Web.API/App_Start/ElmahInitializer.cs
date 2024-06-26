using Neo.EasyAccounts.Web.API;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(ElmahInitializer), "Initialize")]

namespace Neo.EasyAccounts.Web.API
{
    using Elmah.SqlServer.EFInitializer;

    public static class ElmahInitializer
    {
        public static void Initialize()
        {
            using (var context = new ElmahContext())
            {
                context.Database.Initialize(true);
            }
        }
    }
}