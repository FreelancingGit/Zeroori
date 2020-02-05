using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ZerooriBO
{
    public class Utils
    {
        public static String GetKey()
        {

            String Key = DateTime.Now.ToString("ddMM");
            return "ZALog" + PLWM.Utils.CnvToStr(Key);
        }
        public static void GetSms(String PhoneNo, String Otp)
        {
            String Url = "";
            if (PhoneNo != "" && Otp != "")
            {
                if (!PhoneNo.StartsWith("97"))
                {
                    if (PhoneNo.StartsWith("4"))
                    {
                        PhoneNo = "97" + PhoneNo;
                    }
                    else
                        PhoneNo = "974" + PhoneNo;
                }
               else if (PhoneNo.StartsWith("4"))
                {
                    PhoneNo = "974" + PhoneNo;
                }
                 

                Url = "http://api.smscountry.com/SMSCwebservice_bulk.aspx?User=zeroori&passwd=44381000&mobilenumber=" + PhoneNo + "&message=OTP is " + Otp + " For activate your account.&sid=zeroori&mtype=N&DR=Y";
            }
            PLSMS.Send.Sms(Url);
        }

        /// <summary>
        /// Error Message Sending Method
        /// </summary>
        /// <param name = "ErrorMsg" > Error Message from the catch</param>
        /// <param name = "ClassName" > Name Of the Curtrent Class</param>
        /// <param name = "MethodName" > Name Of the current Method</param>
        public static void SendGmailEmail(String Message, string Email, String Subject , String SenderName,String SendToName)
        {
            try
            {
                MailAddress fromAddress = new MailAddress("Support@zeroori.com", SenderName);
                MailAddress toAddress = new MailAddress(Email, SendToName);


                MailMessage message = new MailMessage();
                message.From = fromAddress;
                message.To.Add(toAddress);

                message.Subject = Subject;
                message.Body = Message;
                message.Priority = System.Net.Mail.MailPriority.High;

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port =   587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("Support@zeroori.com", "ZWj6a=hEq");
                SmtpServer.EnableSsl = true;
                SmtpServer.SendCompleted += SmtpServer_SendCompleted;
                SmtpServer.SendAsync(message, SendToName);
            }
            catch
            {

            }
        }
         
        /// <summary>
        /// Error Message Sending Method
        /// </summary>
        /// <param name="ErrorMsg">Error Message from the catch</param>
        /// <param name="ClassName"> Name Of the Curtrent Class</param>
        /// <param name="MethodName">Name Of the current Method</param>
        public static void SendEmail(String Message, string Email, String Subject, String SenderName, String SendToName)
        {
            try
            {
                //System.Windows.Forms.MessageBox.Show(ErrorMsg+ClassName);

                MailAddress fromAddress = new MailAddress("support@zeroori.in", SenderName);
                MailAddress toAddress = new MailAddress(Email, SendToName);


                MailMessage message = new MailMessage();
                message.From = fromAddress;
                message.To.Add(toAddress);

                message.Subject = Subject;
                message.Body = Message;
                message.Priority = System.Net.Mail.MailPriority.High;


                SmtpClient SmtpServer = new SmtpClient("smtp.Zeroori.in");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("support@zeroori.in", "MhA!%KD7");
                    SmtpServer.EnableSsl = false;

                    SmtpServer.SendAsync(message, "Bug");
                SmtpServer.SendCompleted += SmtpServer_SendCompleted;
                
            }
            catch( Exception EX)
            {

            }
        }


        private static void SmtpServer_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           
        }
    }
}
