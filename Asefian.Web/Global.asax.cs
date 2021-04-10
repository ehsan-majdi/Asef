using Asefian.Common.Security;
using Asefian.Model.Context;
using Asefian.Model.Metadata;
using Asefian.Web.App_Start;
using Newtonsoft.Json;
using Asefian.Model.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Asefian.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (Request.Headers["Authorization"] != null)
            {
                try
                {
                    var token = Request.Headers["Authorization"];
                    var userId = Auth.CheckToken(token);
                    if (userId != null)
                    {
                        using (var db = new AsefianContext())
                        {
                            var tokenEntity = db.Token.Where(x => x.AuthoritarianToken == token && x.ExpiredDateTime >= DateTime.Now).Single();
                            HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(AsefianMetadata.Site, "Forms"), Permission.GetPermissionList(tokenEntity.User.Permission));
                            tokenEntity.ExpiredDateTime = DateTime.Now.AddMinutes(180);
                        }
                    }
                    else
                    {
                        throw new AuthenticationException();
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        //let us take out the username now                
                        HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(FormsAuthentication.FormsCookieName);
                        cookie.Expires = DateTime.Now.AddMinutes(90);
                        HttpContext.Current.Request.Cookies.Set(cookie);
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
                        var user = JsonConvert.DeserializeObject<UserPrincipal>(ticket.Name);
                        //let us extract the roles from our own custom cookie
                        var permissionList = new List<string>();
                        permissionList.AddRange(Permission.GetPermissionList(int.Parse(PasswordUtility.Decrypt(user.token))));
                        using (var db = new AsefianContext())
                        {

                            var userGroup = db.Group.Where(x => x.UserGroupList.Any(y => y.UserId == user.id)).ToList();

                            foreach (var x in userGroup)
                            {
                                permissionList.AddRange(Permission.GetPermissionList(x.Permission));
                            }
                        }

                        permissionList = permissionList.Distinct().ToList();
                        //Let us set the Pricipal with our user specific details
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(AsefianMetadata.Site, "Forms"), permissionList.ToArray());
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }

        //string arg filled with the value of "varyByCustom" in your web.config
        public override string GetVaryByCustomString(HttpContext context, string arg)
        {
            if (arg == "User")
            {
                // depends on your authentication mechanism
                return "User=" + context.User.Identity.Name;
                //?return "User=" + context.Session.SessionID;
            }

            return base.GetVaryByCustomString(context, arg);
        }
    }
}
