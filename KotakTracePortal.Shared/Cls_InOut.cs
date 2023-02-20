using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotakTracePortal.Shared
{
    public interface IResponse
    {
        int? StatusCode { get; set; }
        string StatusMsg { get; set; }
        string ErrorMsg { get; set; }
    }

    public class Response : IResponse
    {
        public int? StatusCode { get; set; }
        public string StatusMsg { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class Cls_InOut : IResponse
    {
        public int? StatusCode { get; set; }
        public string StatusMsg { get; set; }
        public string ErrorMsg { get; set; }
        public string DBExceptionMsg { get; set; }
        public string ExceptionMsg { get; set; }
        public Int64? ErrorId { get; set; }
        public DataTable dtProc_Status { get; set; }

        public void DataTable_To_Cls_InOut()
        {
            if (this.dtProc_Status != null && this.dtProc_Status.Rows.Count > 0)
            {
                this.StatusCode = Cls_Common.ConvertToInt32(Cls_Common.GetDataRowValue(this.dtProc_Status.Rows[0], "StatusCode"));
                this.StatusMsg = Cls_Common.GetDataRowValue(this.dtProc_Status.Rows[0], "StatusMsg");

                string ErrorMsg1 = Cls_Common.GetDataRowValue(this.dtProc_Status.Rows[0], "ErrorMsg");
                if (!string.IsNullOrEmpty(ErrorMsg1))
                {
                    this.ErrorMsg += Cls_Common.Delim + ErrorMsg1;
                }

                string DBExceptionMsg1 = Cls_Common.GetDataRowValue(this.dtProc_Status.Rows[0], "DBExceptionMsg");
                if (!string.IsNullOrEmpty(DBExceptionMsg1))
                {
                    this.DBExceptionMsg += Cls_Common.Delim + DBExceptionMsg1;
                }

                this.ErrorId = Cls_Common.ConvertToInt64(Cls_Common.GetDataRowValue(this.dtProc_Status.Rows[0], "ErrorId"));
            }
            else
            {
                this.ErrorMsg += Cls_Common.Delim + "No data found for status";
            }
            SetErrorCode();
        }

        public void DataSet_To_Cls_InOut(DataSet dsResult, int tablesCount, string errorTableName = "o_Proc_Status")
        {
            if (dsResult != null && dsResult.Tables != null && dsResult.Tables.Count >= tablesCount)
            {
                this.dtProc_Status = dsResult.Tables[errorTableName];

                this.DataTable_To_Cls_InOut();
                dsResult.Tables.Remove(errorTableName);
            }
            else
            {
                this.ErrorMsg += Cls_Common.Delim + "No data found";
            }
            SetErrorCode();
        }

        public void Exception_To_Cls_InOut(Exception ex)
        {
            this.ErrorMsg += Cls_Common.Delim + "Exception Occured.";
            this.ExceptionMsg += Cls_Common.Delim + ex.Message + Cls_Common.Delim + ex.StackTrace;
            SetErrorCode();
        }

        public Response GetCommonResponse()
        {
            Response objResponse = new Response();
            objResponse.StatusCode = this.StatusCode;
            objResponse.StatusMsg = this.StatusMsg == null ? string.Empty : this.StatusMsg;
            objResponse.ErrorMsg = this.ErrorMsg == null ? string.Empty : this.ErrorMsg;

            objResponse.StatusMsg = objResponse.StatusMsg.Replace(Cls_Common.Delim, " ").Trim();
            objResponse.ErrorMsg = objResponse.ErrorMsg.Replace(Cls_Common.Delim, " ").Trim();

            SetErrorCode();
            objResponse.StatusCode = this.StatusCode;

            return objResponse;
        }

        public void setAPIError(dynamic dynErrorobj)
        {
            this.ErrorMsg += Cls_Common.Delim + (string)dynErrorobj.Message;
            this.ExceptionMsg += Cls_Common.Delim + (string)dynErrorobj.Message
                + Cls_Common.Delim + (string)dynErrorobj.ExceptionMessage
                + Cls_Common.Delim + (string)dynErrorobj.ExceptionType
                + Cls_Common.Delim + (string)dynErrorobj.StackTrace;
            SetErrorCode();
        }

        public void setOAuthError(dynamic dynErrorobj)
        {
            this.ErrorMsg += Cls_Common.Delim + (string)dynErrorobj.error;
            SetErrorCode();
        }

        public void setClsInOut(Cls_InOut objCls_InOut)
        {
            if (objCls_InOut != null)
            {
                this.StatusMsg += objCls_InOut.StatusMsg == null ? string.Empty : Cls_Common.Delim + objCls_InOut.StatusMsg;
                this.ErrorMsg += objCls_InOut.ErrorMsg == null ? string.Empty : Cls_Common.Delim + objCls_InOut.ErrorMsg;
                this.ExceptionMsg += objCls_InOut.ExceptionMsg == null ? string.Empty : Cls_Common.Delim + objCls_InOut.ExceptionMsg;
                this.DBExceptionMsg += objCls_InOut.DBExceptionMsg == null ? string.Empty : Cls_Common.Delim + objCls_InOut.DBExceptionMsg;
            }
            SetErrorCode();
        }

        public string GetClsInOutLogStr(Cls_InOut objCls_InOut)
        {
            setClsInOut(objCls_InOut);

            return ClsInOutLogStr;
        }

        public string ClsInOutLogStr
        {
            get
            {
                return $"StatusMsg: {this.StatusMsg} ErrorMsg: {this.ErrorMsg} ExceptionMsg: {this.ExceptionMsg} DBExceptionMsg: {DBExceptionMsg}";
            }
        }

        public bool HasError()
        {
            bool hasError = false;
            SetErrorCode();
            if (this.StatusCode == 0)
            {
                hasError = true;
            }
            return hasError;
        }

        public void SetErrorCode()
        {
            this.StatusCode = 1;

            if (this != null)
            {
                if (!string.IsNullOrEmpty(this.ErrorMsg) && !string.IsNullOrEmpty(Convert.ToString(this.ErrorMsg).Trim()))
                {
                    this.StatusCode = 0;
                }
                else if (!string.IsNullOrEmpty(this.ExceptionMsg) && !string.IsNullOrEmpty(Convert.ToString(this.ExceptionMsg).Trim()))
                {
                    this.StatusCode = 0;
                }
                else if (!string.IsNullOrEmpty(this.DBExceptionMsg) && !string.IsNullOrEmpty(Convert.ToString(this.DBExceptionMsg).Trim()))
                {
                    this.StatusCode = 0;
                }
            }
        }

    }
}
