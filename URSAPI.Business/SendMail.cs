using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace KotakTraceAPI.Business
{
    public class SendMail
    {
        CommonControlsBL objCommonBAL = new CommonControlsBL();
        public bool success = false;
        public string SMTPServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
        public string SMTPUserName = ConfigurationManager.AppSettings["SMTPUserName"].ToString();
        public string SMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"].ToString();
        public int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"]);
        public string MailFromTo = ConfigurationManager.AppSettings["MailFromTo"].ToString();
        public string MailFrom = ConfigurationManager.AppSettings["MailFrom"].ToString();
        public string MailBcc1 = ConfigurationManager.AppSettings["MailBcc1"].ToString();
        public string MailBcc2 = ConfigurationManager.AppSettings["MailBcc2"].ToString();
        public string Mailcc1 = ConfigurationManager.AppSettings["Mailcc1"].ToString();
        public string Mailcc2 = ConfigurationManager.AppSettings["Mailcc2"].ToString();
        public string Mailcc3 = ConfigurationManager.AppSettings["Mailcc3"].ToString();
        public string Mailcc4 = ConfigurationManager.AppSettings["Mailcc4"].ToString();
        public string Mailcc5 = ConfigurationManager.AppSettings["Mailcc5"].ToString();
        public string Mailcc6 = ConfigurationManager.AppSettings["Mailcc5"].ToString();
        public string Mailcc7 = ConfigurationManager.AppSettings["Mailcc7"].ToString();
        public string Mailcc8 = ConfigurationManager.AppSettings["Mailcc8"].ToString();
        public string Mailcc9 = ConfigurationManager.AppSettings["Mailcc9"].ToString();
        public string Mailcc10 = ConfigurationManager.AppSettings["Mailcc10"].ToString();
        public bool SendEmailURS(DataTable dt,byte[] FileBytes, string FileName)
        {
            string[] to = new string[1];
            string[] cc = null;
            string subject = string.Empty;
            string EmailBody = string.Empty;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(item["TOEMAILID"])))
                    {
                        to[0] = Convert.ToString(item["TOEMAILID"]);
                    }
                    else
                    {
                        to[0] = MailFromTo;
                    }
                    subject = Convert.ToString(item["SUBJECT"]);

                    if (subject.Contains("Vendor Self Register"))
                    {
                        cc = new string[8];
                        cc[0] = Mailcc5;
                        cc[1] = Mailcc6;
                        cc[2] = Mailcc3;
                        cc[3] = Mailcc4;
                        cc[4] = Mailcc7;
                        cc[5] = Mailcc8;
                        cc[6] = Mailcc9;
                        cc[7] = Mailcc10;
                    }
                   // else if (subject.Contains("Forgot Password"))
                   // {
                   //  cc = new string[2];
                   //   cc[0] = Mailcc1;
                   //  cc[1] = Mailcc2;
                   //}

                    EmailBody = Convert.ToString(item["MAILBODY"]);
                    MailFrom = Convert.ToString(item["FROMEMAILID"]);
                    success = SendEmail(subject, EmailBody, to, cc, MailFrom);
                }

            }

            return success;
        }


        public bool SendEmail(string Subject, string Emailbody, string[] to, string[] cc, string FromMailId)
        {
            try
            {
                //Attachment at = new Attachment("");
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient(SMTPServer, SMTPPort);

                mail.IsBodyHtml = true;
                mail.From = new MailAddress(FromMailId);
                mail.Subject = Subject;
                mail.Body = Emailbody;

                if (to != null && to.Length > 0)
                {
                    foreach (var ToEmail in to)
                    {
                        mail.To.Add(ToEmail);
                    }
                }

                if (cc != null && cc.Length > 0)
                {
                    foreach (var ccEmail in cc)
                    {
                        mail.CC.Add(ccEmail);
                    }
                }

                //if (at != null)
                //{
                //    mail.Attachments.Add(at);
                //}

               // mail.Bcc.Add(MailBcc1);
               // mail.Bcc.Add(MailBcc2);
                mail.Bcc.Add(MailFromTo);

                smtp.EnableSsl = false;
                smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(mail);
                success = true;
                return success;
            }
            catch (Exception ex)
            {
                success = false;
                ErrorlogBL.InsertLog(ex.Message, "", "", "", "ISUPPLIER", "SendMail");
                return success;
            }
        }

        #region Old Email Format
        public void SendMailToUser(String mailTo, String subject, String body)
        {
            try
            {
                if (mailTo != "")
                {

                    //Parameters objParameter = new Parameters();
                    //DataSet dsSMTP = objParameter.Get(DBNull.Value,"1","1", "'SMTPServer','SMTPUserName','SMTPPassword','SMTPEmail','SMTPPort'");
                    //if (dsSMTP == null)
                    //    return;

                    string MailFrom = "eZeePay@hdfcergo.com";

                    //string SMTPServer = "10.49.11.41";
                    string SMTPServer = "antispam-gw";
                    string SMTPUserName = "eZeePay@hdfcergo.com";
                    string SMTPPassword = "Password";
                    int SMTPPort = 25;


                    //foreach (DataRow drSMTP in dsSMTP.Tables[0].Rows)
                    //{
                    //    switch (drSMTP["KEYNAME"].ToString().ToUpper())
                    //    {
                    //        case "SMTPEMAIL":
                    //            MailFrom = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPSERVER":
                    //            SMTPServer = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPUSERNAME":
                    //            SMTPUserName = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPPASSWORD":
                    //            SMTPPassword = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPPORT":
                    //            SMTPPort = int.Parse(drSMTP["KEYValue"].ToString());
                    //            break;
                    //    }
                    //}
                    SmtpClient cn = new System.Net.Mail.SmtpClient(SMTPServer, SMTPPort);
                    MailMessage cm = new System.Net.Mail.MailMessage(MailFrom, mailTo, subject, body);
                    cm.IsBodyHtml = true;
                    cn.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);
                    cn.Host = SMTPServer;
                    cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                    cn.UseDefaultCredentials = false;

                    cm.Bcc.Add("priya.bhadane@hdfcergo.com");
                    //cm.Bcc.Add("earth.support@hdfcergo.com"); -------------to be uncommented for production

                    //MailMessage msgMail = new MailMessage();

                    //msgMail.To = mailTo;
                    //msgMail.From = MailFrom;
                    //msgMail.Subject = subject;
                    //msgMail.BodyFormat = MailFormat.Html;
                    //msgMail.Body = body;

                    //SmtpMail.Send(msgMail);

                    try
                    {
                        cn.Send(cm);
                    }
                    catch (Exception)
                    {
                        //ErrorLog.InsertLog(ex.Message, "mailto-" + mailTo, subject, "Send", "SendMailToUser");

                    }
                    cn = null;
                    cm = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SendMailToUserForCCMail(String MailTo, String mailSubjetc, String Body, string CC)
        {
            try
            {
                if (MailTo != "")
                {
                    //string MailFrom = ConfigurationManager.AppSettings["FromMailId"].ToString();
                    //string SMTPServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
                    //int SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());

                    string MailFrom = "eZeePay@hdfcergo.com";
                    //string SMTPServer = "10.49.11.41";
                    string SMTPServer = "antispam-gw";
                    //string SMTPUserName = "eZeePay@hdfcergo.com";
                    //string SMTPPassword = "Password";
                    int SMTPPort = 25;

                    SmtpClient cn = new System.Net.Mail.SmtpClient(SMTPServer, SMTPPort);
                    MailMessage cm = new System.Net.Mail.MailMessage(MailFrom, MailTo, mailSubjetc, Body);
                    if (CC != "")
                        cm.CC.Add(CC);

                    cm.IsBodyHtml = true;
                    cn.Host = SMTPServer;
                    cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    cn.UseDefaultCredentials = false;
                    cn.Send(cm);
                    cn = null;

                    cm.Dispose();
                    GC.Collect();
                }
            }
            catch (Exception EX)
            {
                throw (EX);
            }
        }
        public void SendMailToUserwithCC(String mailTo, String subject, String body, string cc)
        {
            try
            {
                if (mailTo != "")
                {


                    DataSet dsSMTP = new DataSet(); /*(DataSet)objParameter.Get(DBNull.Value, "1", "1", "'SMTPServer','SMTPUserName','SMTPPassword','SMTPEmail','SMTPPort'");*/
                    if (dsSMTP == null)
                        return;

                    //string MailFrom = "";
                    //string SMTPServer = "";
                    //string SMTPUserName = "";
                    //string SMTPPassword = "";
                    //int SMTPPort = 0;
                    string MailFrom = "eZeePay@hdfcergo.com";
                    string SMTPServer = "antispam-gw";
                    string SMTPUserName = "eZeePay@hdfcergo.com";
                    string SMTPPassword = "Password";
                    int SMTPPort = 25;


                    //foreach (DataRow drSMTP in dsSMTP.Tables[0].Rows)
                    //{
                    //    switch (drSMTP["KEYNAME"].ToString().ToUpper())
                    //    {
                    //        case "SMTPEMAIL":
                    //            MailFrom = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPSERVER":
                    //            SMTPServer = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPUSERNAME":
                    //            SMTPUserName = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPPASSWORD":
                    //            SMTPPassword = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPPORT":
                    //            SMTPPort = int.Parse(drSMTP["KEYValue"].ToString());
                    //            break;
                    //    }
                    //}
                    SmtpClient cn = new System.Net.Mail.SmtpClient(SMTPServer, SMTPPort);
                    MailMessage cm = new System.Net.Mail.MailMessage(MailFrom, mailTo, subject, body);
                    cm.CC.Add(cc);
                    cm.IsBodyHtml = true;
                    cn.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);
                    cn.Host = SMTPServer;
                    cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                    cn.UseDefaultCredentials = false;

                    //MailMessage msgMail = new MailMessage();

                    //msgMail.To = mailTo;
                    //msgMail.From = MailFrom;
                    //msgMail.Subject = subject;
                    //msgMail.BodyFormat = MailFormat.Html;
                    //msgMail.Body = body;

                    //SmtpMail.Send(msgMail);
                    cn.Send(cm);
                    cn = null;
                    cm = null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SendMailToUserwithCCAndAttacchment(String mailTo, String subject, String body, string cc, Attachment at)
        {
            try
            {
                if (mailTo != "")
                {


                    DataSet dsSMTP = new DataSet(); /*(DataSet)objParameter.Get(DBNull.Value, "1", "1", "'SMTPServer','SMTPUserName','SMTPPassword','SMTPEmail','SMTPPort'");*/
                    if (dsSMTP == null)
                        return;

                    string MailFrom = "";
                    string SMTPServer = "";
                    string SMTPUserName = "";
                    string SMTPPassword = "";
                    int SMTPPort = 0;


                    foreach (DataRow drSMTP in dsSMTP.Tables[0].Rows)
                    {
                        switch (drSMTP["KEYNAME"].ToString().ToUpper())
                        {
                            case "SMTPEMAIL":
                                MailFrom = drSMTP["KEYValue"].ToString();
                                break;
                            case "SMTPSERVER":
                                SMTPServer = drSMTP["KEYValue"].ToString();
                                break;
                            case "SMTPUSERNAME":
                                SMTPUserName = drSMTP["KEYValue"].ToString();
                                break;
                            case "SMTPPASSWORD":
                                SMTPPassword = drSMTP["KEYValue"].ToString();
                                break;
                            case "SMTPPORT":
                                SMTPPort = int.Parse(drSMTP["KEYValue"].ToString());
                                break;
                        }
                    }
                    SmtpClient cn = new System.Net.Mail.SmtpClient(SMTPServer, SMTPPort);
                    MailMessage cm = new System.Net.Mail.MailMessage(MailFrom, mailTo, subject, body);
                    if (cc != "")
                        cm.CC.Add(cc);
                    cm.Attachments.Add(at);
                    cm.IsBodyHtml = true;
                    cn.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);
                    cn.Host = SMTPServer;
                    cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                    cn.UseDefaultCredentials = false;

                    //MailMessage msgMail = new MailMessage();

                    //msgMail.To = mailTo;
                    //msgMail.From = MailFrom;
                    //msgMail.Subject = subject;
                    //msgMail.BodyFormat = MailFormat.Html;
                    //msgMail.Body = body;

                    //SmtpMail.Send(msgMail);
                    cn.Send(cm);
                    cn = null;

                    cm.Dispose();
                    //cm = null;
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SendMailToUserwithAttacchment(String mailTo, String subject, String body, Attachment at)
        {
            try
            {
                if (mailTo != "")
                {


                    //DataSet dsSMTP = objParameter.Get(DBNull.Value, "1", "1", "'SMTPServer','SMTPUserName','SMTPPassword','SMTPEmail','SMTPPort'");
                    //if (dsSMTP == null)
                    //    return;

                    //string MailFrom = "";
                    //string SMTPServer = "";
                    //string SMTPUserName = "";
                    //string SMTPPassword = "";
                    //int SMTPPort = 0;


                    string MailFrom = "eZeePay@hdfcergo.com";
                    string SMTPServer = "antispam-gw";
                    string SMTPUserName = "eZeePay@hdfcergo.com";
                    string SMTPPassword = "Password";
                    int SMTPPort = 25;


                    //foreach (DataRow drSMTP in dsSMTP.Tables[0].Rows)
                    //{
                    //    switch (drSMTP["KEYNAME"].ToString().ToUpper())
                    //    {
                    //        case "SMTPEMAIL":
                    //            MailFrom = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPSERVER":
                    //            SMTPServer = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPUSERNAME":
                    //            SMTPUserName = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPPASSWORD":
                    //            SMTPPassword = drSMTP["KEYValue"].ToString();
                    //            break;
                    //        case "SMTPPORT":
                    //            SMTPPort = int.Parse(drSMTP["KEYValue"].ToString());
                    //            break;
                    //    }
                    //}
                    SmtpClient cn = new System.Net.Mail.SmtpClient(SMTPServer, SMTPPort);
                    MailMessage cm = new System.Net.Mail.MailMessage(MailFrom, mailTo, subject, body);
                    //if (cc != "")
                    //    cm.CC.Add(cc);
                    cm.Attachments.Add(at);
                    cm.IsBodyHtml = true;
                    cn.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);
                    cn.Host = SMTPServer;
                    cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis;
                    cn.UseDefaultCredentials = false;

                    //MailMessage msgMail = new MailMessage();

                    //msgMail.To = mailTo;
                    //msgMail.From = MailFrom;
                    //msgMail.Subject = subject;
                    //msgMail.BodyFormat = MailFormat.Html;
                    //msgMail.Body = body;

                    //SmtpMail.Send(msgMail);
                    cn.Send(cm);
                    cn = null;

                    cm.Dispose();
                    //cm = null;
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void SendMailToUserwithMultipleAttacchment(String mailTo, String subject, String body, Attachment at, Attachment at1, Attachment at2, string[] ccRecp, string ccCreator, string[] ToRecp)
        {
            try
            {
                if (mailTo != "")
                {



                    string MailFrom = "eZeePay@hdfcergo.com";
                    string SMTPServer = "antispam-gw";
                    string SMTPUserName = "eZeePay@hdfcergo.com";
                    string SMTPPassword = "Password";
                    int SMTPPort = 25;

                    SmtpClient cn = new System.Net.Mail.SmtpClient(SMTPServer, SMTPPort);

                    MailMessage cm = new System.Net.Mail.MailMessage(MailFrom, mailTo, subject, body);
                    if (ccRecp != null && ccRecp.Length > 0)
                    {
                        foreach (string i in ccRecp)
                        {
                            if (i != null && i != "")
                            {
                                cm.CC.Add(i);
                            }

                        }
                    }

                    if (ToRecp != null && ToRecp.Length > 0)
                    {
                        foreach (string i in ToRecp)
                        {
                            if (i != null && i != "")
                            {
                                cm.To.Add(i);
                            }

                        }
                    }
                    if (!string.IsNullOrEmpty(ccCreator))
                    {
                        cm.CC.Add(ccCreator);
                    }
                    if (at != null)
                    {
                        cm.Attachments.Add(at);
                    }
                    if (at1 != null)
                    {
                        cm.Attachments.Add(at1);
                    }
                    if (at2 != null)
                    {
                        cm.Attachments.Add(at2);
                    }
                    cm.IsBodyHtml = true;
                    cn.Credentials = new System.Net.NetworkCredential(SMTPUserName, SMTPPassword);
                    cn.Host = SMTPServer;
                    cn.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                    cn.UseDefaultCredentials = false;
                    cm.Bcc.Add("priya.bhadane@hdfcergo.com");

                    //////////////////////cm.Bcc.Add("Ashvini.Bavage@hdfcergo.com");///////////////////////Ajay.Tandon@hdfcergo.com

                    try
                    {
                        cn.Send(cm);
                        // ErrorLog.InsertLog("success", "mailto-" + mailTo, "success", "Send", "SendMailToUserwithMultipleAttacchment");
                        //ErrorLog.InsertLog("success", "mailto-" + mailTo, "2757", "SendMailToUserwithMultipleAttacchment", "SendMailToUserwithMultipleAttacchment", "SendMail");
                    }
                    catch (Exception)
                    {
                        //ErrorLog.InsertLog("Fail", "mailto-" + mailTo, "2757", "SendMailToUserwithMultipleAttacchment", "SendMailToUserwithMultipleAttacchment", "SendMail");
                    }

                    cn = null;

                    cm.Dispose();
                    GC.Collect();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
