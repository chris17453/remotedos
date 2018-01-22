using System;
using System.Net;
using System.Net.Security;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using dm;

namespace dmEmail {
    public partial class emailDocument : Form {

        string appDirectory = System.IO.Directory.GetParent(System.IO.Path.GetDirectoryName(Application.ExecutablePath)).FullName;
        appConfig appConf;
        string attachmentName="";
        string app = "";
        bool delete = false;
            
        public emailDocument(string[] args) {
            InitializeComponent();
            if(args.Length > 0) {
                if(args.Length >= 1) app             = args[0];
                if(args.Length >= 2) attachmentName = args[1];
                if(args.Length >= 3) if(args[2] == "delete") delete = true;
            }
            if(app == "") {
            } else {
                appConf = new appConfig(appDirectory + "\\config\\" + app + "Config.txt");
            }
        }



        static byte[] GetBytes(string unicodeString)
        {
            Encoding ascii = Encoding.ASCII;
            return ascii.GetBytes(unicodeString);
        }

        private void EmailDocument_Load(object sender, EventArgs e)
        {
            if(app == "") this.Close();
        }

        private void outbound_TextChanged(object sender, EventArgs e) {

        }

        private void sendB_Click(object sender, EventArgs e) {
            try{
                SmtpClient smtpServer = new SmtpClient();
                smtpServer.Credentials = new System.Net.NetworkCredential((string)appConf.mail["user"], (string)appConf.mail["password"]);
                smtpServer.Port = Convert.ToInt32((string)appConf.mail["port"]);
                smtpServer.Host = (string)appConf.mail["server"];
                if((string)appConf.mail["ssl"] == "true") smtpServer.EnableSsl = true;
                else smtpServer.EnableSsl = false;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress((string)appConf.mail["user"], (string)appConf.mail["userFrom"], System.Text.Encoding.UTF8);
                mailMessage.Subject = subject.Text;
                mailMessage.To.Add(outbound.Text);
                mailMessage.IsBodyHtml = true;


                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                string extension = Path.GetExtension(attachmentName);
                bool pic = false;
                string type=MediaTypeNames.Application.Octet;
                
                switch(extension.ToLower()) {
                    case ".jpg" : pic = true; type = MediaTypeNames.Image.Jpeg; break;
                    case ".gif" : pic = true; type = MediaTypeNames.Image.Gif; break;
                    case ".tif" : pic = true; type = MediaTypeNames.Image.Tiff; break;
                    case ".tiff": pic = true
                        
                        
                        ; type = MediaTypeNames.Image.Tiff; break;
                    case ".txt": pic = false; type = MediaTypeNames.Text.Plain; break;
                    case ".html": pic = false; type = MediaTypeNames.Text.Html; break;
                    case ".htm": pic = false; type = MediaTypeNames.Text.Html; break;
                    case ".zip": pic = false; type = MediaTypeNames.Application.Zip; break;
                    case ".pdf": pic = false; type = MediaTypeNames.Application.Pdf; break;
                    default: pic = false; type=MediaTypeNames.Application.Octet; break;
                }
                string bt = body.Text;

                if(pic) {
                    var inlineLogo = new LinkedResource(attachmentName, type);
                    inlineLogo.ContentId = Guid.NewGuid().ToString();

                    bt += "<br><br>" + string.Format("<img src=\"cid:{0}\" />", inlineLogo.ContentId);
                    var view = AlternateView.CreateAlternateViewFromString(bt, null, "text/html");
                    view.LinkedResources.Add(inlineLogo);
                    mailMessage.AlternateViews.Add(view);
                } else { 
                    Attachment file=new Attachment(attachmentName);
                    mailMessage.Attachments.Add(file);
                }
                mailMessage.Body = bt;
                    
                mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mailMessage.ReplyToList.Add(from.Text);
                smtpServer.Send(mailMessage);
                
                //if(delete) File.Delete(attachmentName);
                this.Close();

            } catch(Exception ex) {
                MessageBox.Show("Email Failed. Something went wrong. Error: " + ex.Message);
            }
            
        }

    }
}
