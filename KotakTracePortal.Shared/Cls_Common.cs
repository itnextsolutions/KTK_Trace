using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace KotakTracePortal.Shared
{
    public class Cls_Common
    {
        public static string Delim { get { return " "; } }

        public static string strLogFile
        {
            get
            {
                try
                {
                    return HttpContext.Current.Server.MapPath("~\\Log\\Error-" + DateTime.Now.ToString("dd-MMM-yyyy") + ".txt");
                }
                catch (Exception)
                {
                    try
                    {
                        return $"{AppDomain.CurrentDomain.BaseDirectory}\\Log\\Error-{DateTime.Now.ToString("dd-MMM-yyyy")}.txt";
                    }
                    catch (Exception)
                    {
                        return string.Empty;
                    }
                }

            }
            set { }
        }

        public static T2 CallAPI<T1, T2>(T1 ursAPIBaseAddress, T1 requestUri, HttpMethod post, T1 empty, out object objCls_InOut)
        {
            throw new NotImplementedException();
        }

        public static T2 CallAPI<T1, T2>(object ursAPIBaseAddress, T1 requestUri, object post, T1 empty, out object objCls_InOut)
        {
            throw new NotImplementedException();
        }

        public static string Enable_LogMessage { get { return ConfigurationManager.AppSettings["Enable_LogMessage"]; } }
        public static string yyyyMMddHHmmss { get { return "yyyyMMddHHmmss"; } }

        public static string DateFormat_ddMMMyyyy { get { return "dd-MMM-yyyy"; } }

        public static DateTime firstdayofyear { get { return new DateTime(DateTime.Today.Year, 1, 1); } }
        public static DateTime lastdayofyear { get { return new DateTime(DateTime.Today.Year, 12, 31); } }
        public static DateTime firstdayofmonth { get { return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); } }
        public static DateTime lastdayofmonth { get { return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1); } }


        public static QuickDateRange GetDateRange(string QuickDateRange)
        {
            QuickDateRange objQuickDateRange = new Cls_Common.QuickDateRange();


            switch (QuickDateRange)
            {

                case "Last 30 Days":
                    objQuickDateRange.dt_FromDate = DateTime.Today.AddDays(-29);
                    objQuickDateRange.dt_ToDate = DateTime.Today.AddDays(0);
                    break;
                case "This Month":
                    objQuickDateRange.dt_FromDate = Cls_Common.firstdayofmonth;
                    objQuickDateRange.dt_ToDate = Cls_Common.lastdayofmonth;
                    break;
                default:
                    break;
            }
            return objQuickDateRange;
        }

        public class QuickDateRange
        {
            public DateTime dt_FromDate = DateTime.Today.AddDays(-29);
            public DateTime dt_ToDate = DateTime.Today;
        }

        public static string GetDataRowValue(DataRow dr, string Column)
        {
            string output = string.Empty;
            try
            {
                output = Convert.ToString(dr[Column]);
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "Exception", ex);
            }
            return output;
        }

        public static object GetDataRowObject(DataRow dr, string Column)
        {
            object output = default(object);
            try
            {
                output = dr[Column];
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "Exception", ex);
            }
            return output;
        }


        public static int? ConvertToInt32(object input)
        {
            int? output = null;
            try
            {
                output = Convert.ToInt32(input);
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "Exception", ex);
            }
            return output;
        }

        public static Int64? ConvertToInt64(object input)
        {
            Int64? output = null;
            try
            {
                output = Convert.ToInt64(input);
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "Exception", ex);
            }
            return output;
        }

        public static string ConvertToString(object input)
        {
            string output = string.Empty;
            try
            {
                output = Convert.ToString(input);
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "Exception", ex);
            }
            return output;
        }

        public static DataSet ConvertDataTableList_To_DataSet(List<DataTable> dataSetList)
        {
            DataSet dataSet = new DataSet();

            if (dataSetList != null && dataSetList.Count > 0)
            {
                for (int i = 0; i < dataSetList.Count; i++)
                {
                    dataSet.Tables.Add(dataSetList[i]);
                }
            }

            return dataSet;
        }

        public static List<DataTable> ConvertDataSet_To_DataTableList(DataSet dataSet)
        {
            List<DataTable> dataTableList = new List<DataTable>();

            if (dataSet != null && dataSet.Tables != null && dataSet.Tables.Count > 0)
            {
                for (int i = 0; i < dataSet.Tables.Count; i++)
                {
                    dataTableList.Add(dataSet.Tables[i]);
                }
            }

            return dataTableList;
        }

        public static string ConvertObjectToXMLString(object classObject)
        {
            string xmlString = null;
            XmlSerializer xmlSerializer = new XmlSerializer(classObject.GetType());
            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, classObject);
                memoryStream.Position = 0;
                xmlString = new StreamReader(memoryStream).ReadToEnd();
            }
            return xmlString;
        }

        public static string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);
            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Decrypt(string cipherString)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static APIResponse CallAPI<APIRequest, APIResponse>(string APIBaseAddress, string requestUri, HttpMethod httpMethod, APIRequest objAPIRequest, out Cls_InOut objCls_InOut)
        {
            Cls_Common.WriteToFile("CallAPI Start");

            objCls_InOut = new Cls_InOut();
            //APIResponse objAPIResponse = Activator.CreateInstance<APIResponse>();

            APIResponse objAPIResponse;

            try
            {
                objAPIResponse = Activator.CreateInstance<APIResponse>();
            }
            catch (Exception ex)
            {
                Cls_Common.LogToFile(Cls_Common.MessageType.App_Exception, "1.0", "", ex);
                objAPIResponse = default(APIResponse);
            }


            try
            {

                //string accessToken = GetOauthToken(APIBaseAddress, out objCls_InOut);  
                string accessToken = "Kiran";

                if (!string.IsNullOrEmpty(objCls_InOut.ErrorMsg))
                {
                    return objAPIResponse;
                }
                Cls_Common.WriteToFile("2 " + objCls_InOut.ErrorMsg);

                string strAPIRequest = JsonConvert.SerializeObject(objAPIRequest);
                using (var stringContent = new StringContent(strAPIRequest, System.Text.Encoding.UTF8, "application/json"))

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(APIBaseAddress);
                    HttpResponseMessage response = new HttpResponseMessage();

                    try
                    {

                        Cls_Common.WriteToFile(Convert.ToString("2.1"));
                        ServicePointManager.Expect100Continue = true;
                        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                        // System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        response = client.PostAsync(requestUri, stringContent).Result;


                        Cls_Common.WriteToFile("2.2");
                    }
                    catch (Exception ex)
                    {

                        Cls_Common.WriteToFile("2.3" + ex.Message + ex.StackTrace);

                        throw;
                    }


                    Cls_Common.WriteToFile("2.4");


                    var result = response.Content.ReadAsStringAsync().Result;
                    var resultStream = response.Content.ReadAsStreamAsync().Result;
                    string APIException = string.Empty;

                    Cls_Common.WriteToFile("Result " + Convert.ToString(response.StatusCode));
                    if (response.IsSuccessStatusCode)
                    {
                        //objAPIResponse = JsonConvert.DeserializeObject<APIResponse>(result);
                        objAPIResponse = DeserializeJsonFromStream<APIResponse>(resultStream);

                        Cls_Common.WriteToFile("success" + Convert.ToString(objCls_InOut.ErrorMsg));
                    }
                    else
                    {
                        if (response.StatusCode == HttpStatusCode.InternalServerError)
                        {
                            //dynamic dynErrorobj = JsonConvert.DeserializeObject<dynamic>(result);
                            //objCls_InOut.setAPIError(dynErrorobj);

                            //Cls_Common.WriteToFile(objCls_InOut.ErrorMsg);

                            objCls_InOut.ExceptionMsg += Cls_Common.Delim + response.ReasonPhrase;

                            APIException = $"StatusCode: {response.StatusCode}. 1.0 API Exception Occured.";

                            Cls_Common.WriteToFile($"{APIException} {objCls_InOut.ExceptionMsg}");

                            throw new Exception(APIException);
                        }
                        else if (response.StatusCode == HttpStatusCode.Forbidden)
                        {
                            //objCls_InOut.ErrorMsg += Cls_Common.Delim + JsonConvert.DeserializeObject<string>(result);
                            Cls_InOut objCls_InOut1 = JsonConvert.DeserializeObject<Cls_InOut>(result);
                            //objCls_InOut.setClsInOut(objCls_InOut1);

                            APIException = $"StatusCode: {response.StatusCode}. 1.1 API Error Occured.";

                            Cls_Common.WriteToFile($"{APIException} {objCls_InOut.GetClsInOutLogStr(objCls_InOut1)}");

                        }
                        else if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            System.Web.HttpContext.Current.Session["accessToken"] = null;
                            objCls_InOut.ErrorMsg += Cls_Common.Delim + JsonConvert.DeserializeObject<string>(result);

                            APIException = $"StatusCode: {response.StatusCode}. 1.2 API Error Occured.";

                            Cls_Common.WriteToFile($"{APIException} {objCls_InOut.ErrorMsg}");

                        }
                        else if (response.StatusCode == HttpStatusCode.NotImplemented)
                        {
                            objCls_InOut.ExceptionMsg += Cls_Common.Delim + JsonConvert.DeserializeObject<string>(result);

                            objCls_InOut.ExceptionMsg += Cls_Common.Delim + response.ReasonPhrase;

                            APIException = $"StatusCode: {response.StatusCode}. 1.3 API Exception Occured.";

                            Cls_Common.WriteToFile($"{APIException} {objCls_InOut.ExceptionMsg}");

                            throw new Exception(APIException);
                        }
                        else
                        {
                            objCls_InOut.ExceptionMsg += Cls_Common.Delim + response.ReasonPhrase;

                            Cls_Common.WriteToFile(objCls_InOut.ExceptionMsg);

                            APIException = $"StatusCode: {response.StatusCode}. 1.4 API Exception Occured.";

                            Cls_Common.WriteToFile($"{APIException} {objCls_InOut.ExceptionMsg}");

                            throw new Exception(APIException);
                        }
                    }

                    if (!string.IsNullOrEmpty(objCls_InOut.ExceptionMsg))
                    {
                        Cls_Common.WriteToFile(objCls_InOut.ExceptionMsg);

                        APIException = $"StatusCode: Exception. 1.5 API Exception Occured.";

                        Cls_Common.WriteToFile($"{APIException} {objCls_InOut.ExceptionMsg}");

                        throw new Exception(APIException);
                    }

                }
            }
            catch (Exception ex)
            {
                objCls_InOut.Exception_To_Cls_InOut(ex);

                Cls_Common.WriteToFile("CallAPI exception" + ex.Message + " " + ex.StackTrace);
                //throw;
            }
            return objAPIResponse;
        }

        public static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        public static ResponseObj GetModelFromJSONObject<RequestObj, ResponseObj>(RequestObj requestObj)
        {
            ResponseObj responseObj = default(ResponseObj);
            responseObj = JsonConvert.DeserializeObject<ResponseObj>(JsonConvert.SerializeObject(requestObj));
            return responseObj;
        }

        public static string GetOauthToken(string APIBaseAddress, out Cls_InOut objCls_InOut)
        {

            Cls_Common.WriteToFile("GetOauthToken Start");
            objCls_InOut = new Cls_InOut();
            string accessToken = string.Empty;

            try
            {


                try
                {

                    accessToken = (System.Web.HttpContext.Current == null || System.Web.HttpContext.Current.Session == null)
                              ? null
                              : Convert.ToString(System.Web.HttpContext.Current.Session["accessToken"]);
                }
                catch (Exception ex)
                {
                    Cls_Common.WriteToFile("GetOauthToken exception 1 : " + ex.Message + " " + ex.StackTrace);
                    accessToken = string.Empty;
                }


                if (string.IsNullOrEmpty(accessToken))
                {
                    using (var client = new HttpClient())
                    {
                        var clientCred = Cls_Common.GetClientCredentials();

                        string clientId = clientCred.ClientId;
                        string secretKey = Cls_Common.Encrypt(AppendDateTimeNRandom(clientCred.SecretKey));

                        var authorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + secretKey));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);

                        var form = GetClientCredentialsGrant(clientCred);
                        string password = Cls_Common.Encrypt(AppendDateTimeNRandom(clientCred.Password));
                        form["password"] = password;

                        Cls_Common.WriteToFile("4.2");

                        System.Threading.Thread.Sleep(1000);

                        Cls_Common.WriteToFile("5");
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                        // ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                        // System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        var tokenResponse = client.PostAsync(APIBaseAddress, new FormUrlEncodedContent(form)).Result;
                        //var tokenResponse = client.PostAsync(APIBaseAddress + "accesstoken", new FormUrlEncodedContent(form)).Result;
                        //Cls_Common.WriteToFile(APIBaseAddress);
                        Cls_Common.WriteToFile("5.1");

                        var result = tokenResponse.Content.ReadAsStringAsync().Result;

                        Cls_Common.WriteToFile("5.2");
                        Cls_Common.WriteToFile("Check tokenResponse SuccessStatusCode : " + tokenResponse.IsSuccessStatusCode);

                        if (tokenResponse.IsSuccessStatusCode)
                        {

                            Cls_Common.WriteToFile("6 - " + tokenResponse.IsSuccessStatusCode + "");

                            Token token = JsonConvert.DeserializeObject<Token>(result);
                            accessToken = token.AccessToken;
                            if (string.IsNullOrEmpty(accessToken))
                            {
                                objCls_InOut.ErrorMsg += Cls_Common.Delim + "Invalid Token.";

                                Cls_Common.WriteToFile("7 - " + objCls_InOut.ErrorMsg + "");


                            }
                            else
                            {
                                try
                                {
                                    System.Web.HttpContext.Current.Session["accessToken"] = accessToken;

                                    Cls_Common.WriteToFile("8 - " + accessToken + "");
                                }
                                catch (Exception ex)
                                {
                                    Cls_Common.WriteToFile("GetOauthToken exception 1.1 : " + ex.Message + " " + ex.StackTrace);
                                }

                            }
                        }
                        else
                        {
                            if (tokenResponse.StatusCode == HttpStatusCode.BadRequest)
                            {

                                Cls_Common.WriteToFile("9 - " + tokenResponse.StatusCode + "");

                                dynamic dynErrorobj = JsonConvert.DeserializeObject<dynamic>(result);
                                objCls_InOut.setOAuthError(dynErrorobj);
                                Cls_Common.WriteToFile("10");
                                Cls_Common.WriteToFile(objCls_InOut.ErrorMsg);

                            }
                            else
                            {
                                objCls_InOut.ErrorMsg += Cls_Common.Delim + tokenResponse.ReasonPhrase;

                                Cls_Common.WriteToFile("11");
                            }
                        }
                    }
                }

                Cls_Common.WriteToFile("12");

            }
            catch (Exception ex)
            {
                objCls_InOut.Exception_To_Cls_InOut(ex);
                Cls_Common.WriteToFile("GetOauthToken exception 2 : " + ex.Message + " " + ex.StackTrace);
                Cls_Common.WriteToFile("GetOauthToken exception 2.1 InnerException : " + ex.InnerException);
                Exception ex11 = ex.InnerException;
                while (ex11 != null)
                {
                    Cls_Common.WriteToFile("GetOauthToken exception 2 While Loop : " + ex.Message);
                    ex11 = ex11.InnerException;
                }
                throw;
            }
            return accessToken;
        }

        public static HttpResponseMessage GetHttpResponseMessage<APIResponse>(HttpResponseMessage response, APIResponse objAPIResponse, Cls_InOut objCls_InOut = null, [CallerMemberName] string caller = null)
        {
            Cls_Common.WriteToFile($"GetHttpResponseMessage Start {caller}");
            //string jsonStr = JsonConvert.SerializeObject(objAPIResponse);
            //response.Content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            if (objCls_InOut != null && objCls_InOut.HasError())
            {
                response = CreateExceptionResponse<APIResponse>(response, objAPIResponse, objCls_InOut);
                Cls_Common.WriteToFile($"GetHttpResponseMessage Error {caller} : {objCls_InOut.ErrorMsg}");
            }
            else
            {
                response = CreateSuccessResponse<APIResponse>(response, objAPIResponse, objCls_InOut);
            }
            Cls_Common.WriteToFile($"GetHttpResponseMessage End {caller}");
            return response;
        }

        public static HttpResponseMessage CreateExceptionResponse<APIResponse>(HttpResponseMessage response, APIResponse objAPIResponse, Cls_InOut objCls_InOut = null)
        {
            string jsonStr = JsonConvert.SerializeObject(objCls_InOut);
            response = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "application/json")
            };
            return response;
        }

        public static HttpResponseMessage CreateSuccessResponse<APIResponse>(HttpResponseMessage response, APIResponse objAPIResponse, Cls_InOut objCls_InOut = null)
        {
            string jsonStr = JsonConvert.SerializeObject(objAPIResponse);
            response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "application/json")
            };
            return response;
        }

        public static string AppendDateTimeNRandom(string inputKey)
        {
            string appendedKey = inputKey + GetDateTimeNRandom();
            return appendedKey;
        }

        public static string GetDateTimeNRandom()
        {
            string dateTimeNRandom = GetDateTimeyyyyMMddHHmmss() + GetRandomKey();
            return dateTimeNRandom;
        }

        public static string GetRandomKey()
        {
            Random randomNum = new Random();
            string randomKey = Convert.ToString(randomNum.Next(10000, 99999));
            return randomKey;
        }


        public static bool CheckAppendDateTimeMatch(string inputKey1, string inputKey2, int maxMinutesDiff)
        {
            bool status = false;
            //check first part
            string inputKey1Trunc1 = inputKey1.Substring(0, inputKey1.Length - GetDateTimeNRandom().Length);
            string inputKey2Trunc1 = inputKey2.Substring(0, inputKey2.Length - GetDateTimeNRandom().Length);

            //check second part
            string inputKey1Trunc2 = inputKey1.Substring(inputKey1Trunc1.Length, (GetDateTimeyyyyMMddHHmmss().Length));
            string inputKey2Trunc2 = inputKey2.Substring(inputKey2Trunc1.Length, (GetDateTimeyyyyMMddHHmmss().Length));

            //check third part
            string inputKey1Trunc3 = inputKey1.Substring(inputKey1.Length - GetRandomKey().Length, GetRandomKey().Length);
            string inputKey2Trunc3 = inputKey2.Substring(inputKey2.Length - GetRandomKey().Length, GetRandomKey().Length);

            DateTime dt1Trunc2 = ConvertStringToDateTime(inputKey1Trunc2, yyyyMMddHHmmss);
            DateTime dt2Trunc2 = ConvertStringToDateTime(inputKey2Trunc2, yyyyMMddHHmmss);
            TimeSpan ts = dt2Trunc2 - dt1Trunc2;
            //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "inputKey1Trunc1 - " + inputKey1Trunc1);
            //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "inputKey2Trunc1 - " + inputKey2Trunc1);
            //System.IO.File.AppendAllText(strLogFile, Environment.NewLine + "TotalMinutes - " + ts.TotalMinutes);
            //Cls_Common.WriteToFile("AppendDateTimeMatch - " + ts);
            if (inputKey1Trunc1 == inputKey2Trunc1 && (Math.Abs(ts.TotalMinutes) > 0 && Math.Abs(ts.TotalMinutes) <= maxMinutesDiff))
            {
                status = true;
            }
            //Cls_Common.WriteToFile("AppendDateTimeMatch  status - " + status);
            return status;
        }

        public static Dictionary<string, string> GetClientCredentialsGrant(ClientCredentials objClientCredentials)
        {
            var form = new Dictionary<string, string>
                                {
                                    {"grant_type", objClientCredentials.GrantType},
                                    {"username", objClientCredentials.UserName},
                                    {"password", objClientCredentials.Password},
                                };
            return form;
        }

        //public static string yyyyMMddHHmmss = "yyyyMMddHHmmss";
        public static string GetDateTimeyyyyMMddHHmmss()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public static string GetDateTimeddMMMyyyy()
        {
            return DateTime.Now.ToString("ddMMMyyyy");
        }

        public static string ConvertToddMMMyyyy(DateTime? dt)
        {
            try
            {
                return ConvertToddMMMyyyy((DateTime)dt);
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        public static string ConvertToddMMMyyyy(DateTime dt)
        {
            return (dt).ToString("dd-MMM-yyyy").ToUpper();
        }

        public static DateTime ConvertStringToDateTime(string strDateTime, string dateTimeformat)
        {
            return DateTime.ParseExact(strDateTime, dateTimeformat, null);
        }

        public static Dictionary<string, string> ReadDetailsFile()
        {


            Cls_InOut objCls_InOut = new Cls_InOut();
            Dictionary<string, string> dictCred = new Dictionary<string, string>();

            try
            {
                string filePath = ConfigurationManager.AppSettings["DetailsFilePath"];
                string fileName = ConfigurationManager.AppSettings["DetailsFileName"];
                var listText = File.ReadAllLines(filePath + fileName).ToList();

                ClientCredentials objCredentials = new ClientCredentials();
                Cls_Common.WriteToFile("Read All Lines from File");
                for (int i = 0; i < listText.Count; i++)
                {
                    string[] lineSplit = listText[i].Split(new String[] { "|$BREAK$|" }, StringSplitOptions.None);
                    string key = lineSplit[0].Trim();
                    string value = lineSplit[1].Trim();
                    value = value.Substring(1, value.Length - 2);
                    dictCred.Add(key, Cls_Common.Decrypt(value));
                }

            }
            catch (Exception ex)
            {
                objCls_InOut.Exception_To_Cls_InOut(ex);

                Cls_Common.WriteToFile("ReadDetailsFile exception" + ex.Message + " " + ex.StackTrace);

                throw;
            }
            return dictCred;
        }

        public static ClientCredentials GetClientCredentials()
        {

            Cls_Common.WriteToFile("GetClientCredentials Start");

            Cls_InOut objCls_InOut = new Cls_InOut();

            ClientCredentials objClientCredentials = new ClientCredentials();
            try
            {
                Dictionary<string, string> dictCred = ReadDetailsFile();

                objClientCredentials.ClientId = dictCred["1"];
                objClientCredentials.SecretKey = dictCred["2"];
                objClientCredentials.GrantType = dictCred["3"];
                objClientCredentials.UserName = dictCred["4"];
                objClientCredentials.Password = dictCred["5"];
            }
            catch (Exception ex)
            {
                objCls_InOut.Exception_To_Cls_InOut(ex);

                Cls_Common.WriteToFile("GetClientCredentials exception" + ex.Message + " " + ex.StackTrace);
                throw;
            }

            return objClientCredentials;
        }

        public static void WriteToFile(string message, string uniqueId = "", string fullFilePath = "")
        {
            try
            {
                //string Enable_LogMessage = ConfigurationManager.AppSettings["Enable_LogMessage"];
                if (Enable_LogMessage == "true")
                {
                    //fullFilePath = string.IsNullOrEmpty(fullFilePath) ? "" : fullFilePath;

                    fullFilePath = string.IsNullOrEmpty(fullFilePath) ? strLogFile : fullFilePath;

                    System.IO.File.AppendAllText(fullFilePath, $"{Environment.NewLine} {DateTime.Now}   |   {uniqueId}   |   {message}");
                }
            }
            catch (Exception)
            {

            }

        }


        public static void LogToFile(string messageType, string messageSequence, string message = "", Exception exception = null, string uniqueId = "", string fullFilePath = "", [CallerMemberName] string methodName = null)
        {
            try
            {
                if (exception != null)
                {
                    message = $"{message} |Exception| {exception.Message} {exception.StackTrace}";
                }

                Cls_Common.WriteToFile($"{methodName}()    :   {messageSequence} :   {messageType}   :   {message}");
            }
            catch (Exception ex)
            {
                Cls_Common.WriteToFile($"LogToFile    :   1.0 :   App_Exception   :   {ex.Message}");
            }
        }


        public static StringBuilder ConvertDataTableToHtml(DataTable dt, string HeaderHtml = "")
        {
            StringBuilder sb = new StringBuilder();

            if (dt == null || dt.Rows == null)
            {
                return sb;
            }
            string htmlCol1 = string.Empty;
            string HeaderRow = string.Empty;

            if (!string.IsNullOrEmpty(HeaderHtml))
            {
                HeaderRow = HeaderHtml;
            }
            else
            {
                try
                {
                    if (dt.Rows.Count > 0)
                    {
                        htmlCol1 = Cls_Common.GetDataRowValue(dt.Rows[0], "htmlCol1");
                        HeaderRow = htmlCol1.Substring(htmlCol1.IndexOf("HeaderRowStart") + "HeaderRowStart".Length, htmlCol1.IndexOf("HeaderRowEnd") - (htmlCol1.IndexOf("HeaderRowStart") + "HeaderRowStart".Length));
                    }
                }
                catch (Exception)
                {

                }

            }

            dt.Columns.Remove("htmlCol1");

            #region table
            sb.Append($"<table border='1' cellpadding='2' cellspacing='0' width='80%'>{Environment.NewLine}");
            #region thead
            sb.Append($"<thead style='background-color: #CC3300; border: 1em solid #CC3300; color: white' align='center'>{Environment.NewLine}");

            if (!string.IsNullOrEmpty(HeaderRow.Trim()))
            {
                sb.Append(HeaderRow);
            }
            else
            {
                sb.Append($"<tr>{Environment.NewLine}");

                foreach (DataColumn dc in dt.Columns)
                {
                    sb.Append($"<th align='center'> {dc.ColumnName} </th>{Environment.NewLine}");
                }
                sb.Append($"</tr>{Environment.NewLine}");
            }

            sb.Append($"</thead>{Environment.NewLine}");
            #endregion



            #region tbody
            sb.Append($"<tbody style='border: 1px solid #CC3300;' align='center'>{Environment.NewLine}");

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append($"<tr align='center'>{Environment.NewLine}");

                foreach (DataColumn dc in dt.Columns)
                {
                    sb.Append($"<td> {dr[dc]} </td>{Environment.NewLine}");
                }

                sb.Append($"</tr>{Environment.NewLine}");
            }

            sb.Append($"</tbody>{Environment.NewLine}");

            #endregion
            sb.Append($"</table>{Environment.NewLine}");
            #endregion

            return sb;
        }

        public static string GetEncodedUrlParameter(string URLParameters)
        {
            ///Send parameter separator i.e "&" with "$|$" only else the parameter after "&" will get truncated in GetUri() method

            string EncodedUrlParameter = URLParameters;

            EncodedUrlParameter = EncodedUrlParameter.Replace("$|$", "&");
            EncodedUrlParameter = Cls_Common.Encrypt(EncodedUrlParameter);
            EncodedUrlParameter = System.Web.HttpUtility.UrlEncode(EncodedUrlParameter);

            return EncodedUrlParameter;
        }

        public static string GetEncodedUrlParameterJson(string URLParameters)
        {
            string EncodedUrlParameter = GetEncodedUrlParameter(URLParameters);
            return JsonConvert.SerializeObject(EncodedUrlParameter);
        }



        public static class MessageType
        {
            public static string App_Validation = "App_Validation";
            public static string App_Exception = "App_Exception";
            public static string App_Message = "App_Message";
            public static string DB_Validation = "DB_Validation";
            public static string DB_Exception = "DB_Exception";
            public static string DB_Message = "DB_Message";
        }

        public static class AppEnv
        {
            public static string DEV = "DEV";
            public static string UAT = "UAT";
            public static string PROD = "PROD";
        }

        public static DataTable LoadXMLToDataTable(string xmlFilePath)
        {
            DataSet ds = new DataSet();
            ds.ReadXml(xmlFilePath);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            return dt;
        }


    }
}
