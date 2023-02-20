using KotakTracePortal.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KotakTracePortal.Controllers
{
    public class CommonController : Controller
    {
        [HttpGet]
        public string GetEncodedUrlParameter(string URLParameters)
        {
            ///Send parameter separator i.e "&" with "$|$" only else the parameter after "&" will get truncated in GetUri() method

            string EncodedUrlParameter = URLParameters;

            EncodedUrlParameter = EncodedUrlParameter.Replace("$|$", "&");
            EncodedUrlParameter = Cls_Common.Encrypt(EncodedUrlParameter);
            EncodedUrlParameter = HttpUtility.UrlEncode(EncodedUrlParameter);

            return JsonConvert.SerializeObject(EncodedUrlParameter);
        }


        public string GetUriParameterValue(Uri CompleteUri, string ParameterName)
        {
            string ParameterValue = HttpUtility.ParseQueryString(CompleteUri.Query).Get(ParameterName);

            return ParameterValue;
        }


        public Uri GetUri(string RedirectURLEncoded, string URLEncodedInParameterName)
        {
            string redirectParameters = string.Empty;
            string redirectURL = string.Empty;

            //string baseUrl = new Uri(Request.Url, Url.Content("~")).AbsoluteUri;
            string baseUrl = "http://localhost/";
            Uri CompleteUri = new Uri(baseUrl);

            if (!string.IsNullOrEmpty(RedirectURLEncoded))
            {
                redirectParameters = RedirectURLEncoded;

                if (redirectParameters.StartsWith("?"))
                {
                    redirectParameters = redirectParameters.Substring(1, redirectParameters.Length - 1);
                }
                Uri CompleteUri1 = new Uri($"{baseUrl}?{redirectParameters}");
                CompleteUri = CompleteUri1;

                string URLParameters = HttpUtility.ParseQueryString(CompleteUri1.Query).Get(URLEncodedInParameterName);

                if (!string.IsNullOrEmpty(URLParameters))
                {
                    //URLParameters = HttpUtility.UrlDecode(URLParameters);
                    URLParameters = Cls_Common.Decrypt(URLParameters);

                    Uri CompleteUri2 = new Uri($"{baseUrl}?{URLParameters}");
                    CompleteUri = CompleteUri2;
                }

            }

            return CompleteUri;
        }
    }
}