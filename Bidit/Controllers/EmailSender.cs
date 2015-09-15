using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Bidit.Controllers
{
    public class EmailSender
    {
        public static bool PostMail(string email, string mailSubject, string mailBody)
        {
            //var textHtmlContent = EmailTemplate.ForgetPasswordEmail;
            var textHtmlContent = EmailTemplate.BidEndedEmail;
            textHtmlContent = textHtmlContent.Replace("USERNAME", "dan");
            textHtmlContent = textHtmlContent.Replace("PASSWORD", "123123");
            textHtmlContent = textHtmlContent.Replace("BID_ID", "23");
            textHtmlContent = textHtmlContent.Replace("PRODUCT_NAME", "טלפון");
            textHtmlContent = textHtmlContent.Replace("BEST_PRICE", "100");

            SendMail(email, mailSubject, textHtmlContent);
        }

       public static bool SendMail(string email, string mailSubject, string mailBody)
       {
           // TEMP!!!
           return true;

           var retVal = true;

            //Specify senders gmail address
            string SendersAddress = "chenvardi9@gmail.com";
            //Specify The Address You want to sent Email To(can be any valid email address)
            //string ReceiversAddress = "dandan53@gmail.com";
            string ReceiversAddress = email;

            //Specify The password of gmial account u are using to sent mail(pw of sender@gmail.com)
            const string SendersPassword = "chenvardi123";
            //Write the subject of ur mail
            string subject = mailSubject;
            //Write the contents of your mail
            string body = mailBody;
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(SendersAddress, SendersPassword),
                    Timeout = 3000
                };

                //MailMessage represents a mail message
                //it is 4 parameters(From,TO,subject,body)

                MailMessage message = new MailMessage(SendersAddress, ReceiversAddress, subject, body);
                /*WE use smtp sever we specified above to send the message(MailMessage message)*/

                message.IsBodyHtml = true;

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }

       //public static bool SendForgetPasswordEmail(string email, string password)
       //{
       //    var retVal = true;

       //    //Specify senders gmail address
       //    string SendersAddress = "chenvardi9@gmail.com";
       //    //Specify The Address You want to sent Email To(can be any valid email address)
       //    //string ReceiversAddress = "dandan53@gmail.com";
       //    string ReceiversAddress = email;

       //    //Specify The password of gmial account u are using to sent mail(pw of sender@gmail.com)
       //    const string SendersPassword = "chenvardi123";
       //    //Write the subject of ur mail
       //    const string subject = "שחזור סיסמה";
       //    //Write the contents of your mail
       //    string body = "הסיסמה שלך הינה: " + password;
       //    try
       //    {
       //        SmtpClient smtp = new SmtpClient
       //        {
       //            Host = "smtp.gmail.com",
       //            Port = 587,
       //            EnableSsl = true,
       //            DeliveryMethod = SmtpDeliveryMethod.Network,
       //            Credentials = new NetworkCredential(SendersAddress, SendersPassword),
       //            Timeout = 3000
       //        };

       //        //MailMessage represents a mail message
       //        //it is 4 parameters(From,TO,subject,body)

       //        MailMessage message = new MailMessage(SendersAddress, ReceiversAddress, subject, body);
       //        /*WE use smtp sever we specified above to send the message(MailMessage message)*/

       //        smtp.Send(message);
       //    }
       //    catch (Exception ex)
       //    {
       //        retVal = false;
       //    }

       //    return retVal;
       //}
    }
}