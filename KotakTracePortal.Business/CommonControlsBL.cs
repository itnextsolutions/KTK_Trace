using KotakTracePortal.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KotakTracePortal.Buisness
{
    public class CommonControlsBL
    {
        //CommonControlsDL objDropDL = new CommonControlsDL();
        string UrsAPIBaseAddress = ConfigurationManager.AppSettings["UrsAPIBaseAddress"];
        string requestUri = "";
        bool success = false;
        Cls_InOut objCls_InOut = new Cls_InOut();
        DataSet dsFill = new DataSet();
        DataTable dtFill = new DataTable();
        public static CommonControlsBL _this;
        public static CommonControlsBL ThisClass
        {
            get
            {
                if (_this == null)
                    _this = new CommonControlsBL();
                return _this;
            }
            set { _this = value; }
        }
        public static CommonControlsBL Instance()
        {
            return ThisClass;
        }
        public DataSet CommonFillDrp(string Key, Int32 DeptId)
        {
            dynamic dynModel = new ExpandoObject();
            dynModel.Key = Key;
            dynModel.DeptId = DeptId;

            requestUri = "api/CommonControls/CommonFillDrp";
            dsFill = Cls_Common.CallAPI<dynamic, DataSet>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, dynModel, out objCls_InOut);
            return dsFill;
        }


        public static string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            if (!string.IsNullOrEmpty(toEncrypt))
            {

                byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(Convert.ToString(toEncrypt));

                System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
                // Get the key from config file
                string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
                //System.Windows.Forms.MessageBox.Show(key);
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
            else
            {
                return null;

            }

        }
        public static string Decrypt(string cipherString)
        {

            bool useHashing = true;
            byte[] keyArray;

            if (!string.IsNullOrEmpty(cipherString))
            {
                cipherString = cipherString.Replace(" ", "+");
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
            else
            {
                return null;

            }
        }
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    try
                    {
                        if (pro.Name.ToUpper() == column.ColumnName.ToUpper())
                            pro.SetValue(obj, Convert.ChangeType(dr[column.ColumnName], Nullable.GetUnderlyingType(pro.PropertyType) ?? pro.PropertyType), null);
                        else
                            continue;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return obj;
        }

        public IEnumerable<CommonControls.DropDownListModel> CityListDrp()
        {
            //return objDropDL.CityListDrp();

            requestUri = "api/CommonControls/CityListDrp";
            IEnumerable<CommonControls.DropDownListModel> dtList = Cls_Common.CallAPI<string, List<CommonControls.DropDownListModel>>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, string.Empty, out objCls_InOut);
            return dtList;
        }



        public DataTable autocompleteURSSearch(string URSNO)
        {
            requestUri = "api/CommonControls/autocompleteURSSearch";
            dtFill = Cls_Common.CallAPI<string, DataTable>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, URSNO, out objCls_InOut);
            return dtFill;
        }


        private string Encrypt_AES(string clearText)
        {
            string EncryptionKey = "##SAI##1990##"; //Encryption Key
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            byte[] array = Encoding.ASCII.GetBytes("##100SAINESHWAR99##"); //salt

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, array);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt_AES(string cipherText)
        {
            string EncryptionKey = "##SAI##1990##"; //Encryption Key
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            byte[] array = Encoding.ASCII.GetBytes("##100SAINESHWAR99##"); //salt

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, array);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public bool SendMailURS(DataTable dt, byte[] FileBytes, string FileName)
        {
            dynamic dynModel = new ExpandoObject();
            dynModel.dt = dt;
            dynModel.FileBytes = FileBytes;
            dynModel.FileName = FileName;
            requestUri = "api/CommonControls/SendMailURS";
            success = Cls_Common.CallAPI<dynamic, bool>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, dynModel, out objCls_InOut);
            return success;
        }


        public string SendMailToUser(string Subject, string ToMailId, string CCMailId, string MailBody)
        {
            try
            {
                Cls_InOut objCls_InOut = new Cls_InOut();
                dynamic dynmodel = new ExpandoObject();
                dynmodel.Subject = Subject;
                dynmodel.ToMailId = ToMailId;
                dynmodel.CCMailId = CCMailId;
                dynmodel.MailBody = MailBody;
                string requestUri = "api/Login/SendMailToUser";
                string dsTbl = Cls_Common.CallAPI<dynamic, string>(UrsAPIBaseAddress, requestUri, HttpMethod.Post, dynmodel, out objCls_InOut);
                return dsTbl;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
