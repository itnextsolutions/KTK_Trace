using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Infrastructure;
using System.Web.Http;
using System.Security.Claims;
using System.Configuration;

[assembly: OwinStartup(typeof(KotakTraceAPI.CustomFilterRepo.Startup))]

namespace KotakTraceAPI.CustomFilterRepo
{
    public class Startup
    {
        int AuthCodeExpireMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["AuthCodeExpireMinutes"]);
        int AccessTokenExpireMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["AccessTokenExpireMinutes"]);
        public void Configuration(IAppBuilder app)
        {
            //var oauthOptions = new OAuthAuthorizationServerOptions
            //{
            //    AllowInsecureHttp = true,
            //    TokenEndpointPath = new PathString("/accesstoken"),
            //    Provider = new AuthorizationServerProvider(),
            //    AuthorizationCodeExpireTimeSpan = TimeSpan.FromMinutes(AuthCodeExpireMinutes),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(AccessTokenExpireMinutes),
            //    SystemClock = new SystemClock()

            //};
            //app.UseOAuthAuthorizationServer(oauthOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }
    }
}
