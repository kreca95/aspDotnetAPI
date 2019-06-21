using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(authAPI.Startup))]

namespace authAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var client_id = "public";
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider =new AuthServerProvider();
            //var refreshToken = new AuthenticationTokenProvider()
            //{
            //    OnCreate = CreateRefreshToken,
            //    OnReceive = ReceiveRefreshToken,
            //};
            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(30),
                Provider = myProvider,
                RefreshTokenProvider = new RefreshTokenProvider()

            };

            app.UseOAuthAuthorizationServer(options);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
        //private void CreateRefreshToken(AuthenticationTokenCreateContext context)
        //{
        //    context.SetToken(context.SerializeTicket());
        //}

        //private void ReceiveRefreshToken(AuthenticationTokenReceiveContext context)
        //{
        //    context.DeserializeTicket(context.Token);
        //}
    }
}
