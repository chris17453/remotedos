using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using dm;

namespace dm {
    public class email{
        appConfig appConf;
        SmtpClient smtpServer = new SmtpClient();
        MailMessage mailMessage = new MailMessage();
        public email(string subject,string body,appConfig config){
            appConf=config;
            smtpServer.Credentials = new System.Net.NetworkCredential((string)appConf.mail["user"], (string)appConf.mail["password"]);
            smtpServer.Port = Convert.ToInt32((string)appConf.mail["port"]);
            smtpServer.Host = (string)appConf.mail["server"];
            if((string)appConf.mail["ssl"] == "true") smtpServer.EnableSsl = true;
            else smtpServer.EnableSsl = false;
            mailMessage.IsBodyHtml = true;
            ServicePointManager.ServerCertificateValidationCallback =
            delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
        }

        string subject{
            get{ return mailMessage.Subject;}
            set{ mailMessage.Subject=value; }
        }

        public void from(string displayName,string email){
            mailMessage.From=new MailAddress(displayName, email, System.Text.Encoding.UTF8); 
        }
        public void to(string email){
            mailMessage.To.Add(email);
        }
        
        public void addAttachment(string attachmentName,string attachmentPath){
            string extension = Path.GetExtension(attachmentPath);
            //bool pic = false;
            //string type=MediaTypeNames.Application.Octet;
            /*    
            switch(extension.ToLower()) {
                case ".jpg" : pic = true;  type = MediaTypeNames.Image.Jpeg; break;
                case ".gif" : pic = true;  type = MediaTypeNames.Image.Gif; break;
                case ".tif" : pic = true;  type = MediaTypeNames.Image.Tiff; break;
                case ".tiff": pic = true;  type = MediaTypeNames.Image.Tiff; break;
                case ".txt" : pic = false; type = MediaTypeNames.Text.Plain; break;
                case ".html": pic = false; type = MediaTypeNames.Text.Html; break;
                case ".htm" : pic = false; type = MediaTypeNames.Text.Html; break;
                case ".zip" : pic = false; type = MediaTypeNames.Application.Zip; break;
                case ".pdf" : pic = false; type = MediaTypeNames.Application.Pdf; break;
                default: pic = false; type=MediaTypeNames.Application.Octet; break;
            }*/
        
            //string bt ="";
            /*
            if(pic) {
                var inlineLogo = new LinkedResource(attachmentPath, type);
                inlineLogo.ContentId = Guid.NewGuid().ToString();

                bt= "<br><br>" + string.Format("<img src=\"cid:{0}\" />", inlineLogo.ContentId);
                var view = AlternateView.CreateAlternateViewFromString(bt, null, "text/html");
                view.LinkedResources.Add(inlineLogo);
                mailMessage.AlternateViews.Add(view);
            } else { */
                Attachment file=new Attachment(attachmentPath);
                mailMessage.Attachments.Add(file);
            //}
        }

        string body{
            get{ return mailMessage.Body; }
            set { mailMessage.Body = value;}
        }

        public void send(){
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //mailMessage.ReplyToList.Add(from.Text);
                smtpServer.Send(mailMessage);
        }
        
    }
}
