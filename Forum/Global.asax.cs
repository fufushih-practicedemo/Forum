using Forum.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Forum
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest()
        {
            // Check if user is logged in
            if(User == null) { return; }

            // Get account
            string account = Context.User.Identity.Name;
            // Declare array of roles
            string[] roles = null;

            using(Db db = new Db()) {
                // Populate roles
                MemberDTO dto = db.Members.FirstOrDefault(x => x.Account == account);
                if(dto != null) {
                    roles = db.UserRoles.Where(x => x.UserId == dto.UID).Select(x => x.Role.Name).ToArray();
                }
            }

            // Build IPrinciple obj
            IIdentity userIdentity = new GenericIdentity(account);
            IPrincipal newUserObj = new GenericPrincipal(userIdentity, roles);

            // Update context user
            Context.User = newUserObj;
        }
    }
}
