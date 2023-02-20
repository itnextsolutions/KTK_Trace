using KotakTracePortal.Shared;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace KotakTraceAPI.CustomFilterRepo
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        ClientCredentials clientCred = Cls_Common.GetClientCredentials();
        public static string strLogFile = HttpContext.Current.Server.MapPath("~\\Log\\Error-" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt");
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "client Autho Start");
            try
            {
                string clientId;
                string clientSecret;
                if (context.TryGetBasicCredentials(out clientId, out clientSecret))
                {
                    string og_secretKey = clientCred.SecretKey;
                   // System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "clientCred.SecretKey - " + clientCred.SecretKey);
                    string og_sectetKey_append = Cls_Common.AppendDateTimeNRandom(og_secretKey);
                   // System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "og_sectetKey_append - " + og_sectetKey_append);
                    string client_Secret = Cls_Common.Decrypt(clientSecret);
                   // System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "client_Secret - Decrypt - " + client_Secret);
                    bool secretKeyMatch = Cls_Common.CheckAppendDateTimeMatch(client_Secret, og_sectetKey_append, 1);
                   // System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "Check Date Time Match Status - " + secretKeyMatch);
                    if (clientId == clientCred.ClientId && secretKeyMatch == true)
                    {
                       // System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "context.Validated");
                        context.Validated();
                    }
                    //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "clientId - " + clientId);
                   // System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "clientCred.ClientId - " + clientCred.ClientId);
                  //  System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "secretKeyMatch - " + secretKeyMatch);
                }
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "Exception", ex);
               // System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "Client Autho Error - " + ex.InnerException);
            }
           // System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "client Autho End");

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "GrantResourceOwnerCredentials Start");
            try
            {
                string og_password = clientCred.Password;
                string og_password_append = Cls_Common.AppendDateTimeNRandom(og_password);
                string client_password = Cls_Common.Decrypt(context.Password);
                bool passwordMatch = Cls_Common.CheckAppendDateTimeMatch(client_password, og_password_append, 1);
                //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "GrantResourceOwner UserNAme - " + context.UserName);
                //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "GrantResourceOwner.UserName - " + clientCred.UserName);
                if (context.UserName == clientCred.UserName && passwordMatch == true)
                {
                    var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                    claimsIdentity.AddClaim(new Claim("user", context.UserName));
                    context.Validated(claimsIdentity);
                    //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "GrantResourceOwner context.Validated ");
                    return;
                }
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "Exception", ex);
                //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "GrantResourceOwner Error - " + ex.InnerException);
            }


            context.Rejected();
            //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "GrantResourceOwnerCredentials End");
        }
    }
}