using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ZerooriBO
{
    public class GetSmsEmailFormats
    {

        public static String GetEmail(String OTP)
        {
            String EMailFormat = "";
            try
            {
                EMailFormat = "Dear Sir,\n\n";
                EMailFormat += "Thanks for opting Zeroori. Please be informed that your otp to activate your account is as mentioned below.\n\n";
                EMailFormat += "Number is " + OTP + "\n\n";
                EMailFormat += "Please ignore the email if you have not actioned this.\n\n";
                EMailFormat += "Best Regards,\n\n";
                EMailFormat += "Team Zeroori\n\n";

            }
            catch
            {

            }
            return EMailFormat;
        }

        public static String GetSubscriptionEmail(String OTP, String EmailAddress)
        {
            String EMailFormat = "";
            try
            {
                EMailFormat = "Dear Sir,\n\n";
                EMailFormat += "Thanks a lot for opting Zeroori. Please note the OTP - "+ OTP + " for your account "+ EmailAddress + " to login.\n\n";
 
                EMailFormat += "We wish you have a great time with Zeroori ahead.\n\n";
                EMailFormat += "Best Regards,\n\n";
                EMailFormat += "Team Zeroori\n\n";
            }
            catch
            {

            }
            return EMailFormat;
        }

        public static String GetOtp(String OTP)
        {
            String EMailFormat = "";
            try
            {
                EMailFormat = OTP + " is your NETSECURE(OTP)code for activating your Zeroori account.Do not share with anyone.";
            }
            catch
            {

            }
            return EMailFormat;
        }

        public static String GetResetPwdEmail(String OTP)
        {
            String EMailFormat = "";
            try
            {
                EMailFormat = "Dear Sir,\n\n";
                EMailFormat += "Thanks for opting Zeroori. Please find your new password below.Please use it to login to your account.\n\n";
                EMailFormat += OTP+"\n\n";
                EMailFormat += "Please ignore the email if you have not actioned this.\n\n";
                EMailFormat += "Best Regards,\n\n";
                EMailFormat += "Team Zeroori\n\n";
            }
            catch
            {

            }
            return EMailFormat;
        }
    }
}
