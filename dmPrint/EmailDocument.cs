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

namespace dmPrint
{
    public partial class EmailDocument : Form
    {
        string document = "";
        bool isHTML = false;

        public string cleanString(string unicodeString)
        {
            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            Encoding unicode = Encoding.Unicode;

            // Convert the string into a byte array. 
            byte[] unicodeBytes = unicode.GetBytes(unicodeString);

            // Perform the conversion from one encoding to the other. 
            byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

            // Convert the new byte[] into a char[] and then into a string. 
            char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            return new string(asciiChars);
        }
        public appConfig appConf;

        public EmailDocument(string document,appConfig config)
        {
            document = cleanString(document);
            if (document.ToLower().IndexOf("</body>") > 0) isHTML = true;
            InitializeComponent();
            this.appConf=config;
            this.document = document;
        }


        static byte[] GetBytes(string unicodeString)
        {
            Encoding ascii = Encoding.ASCII;
            return ascii.GetBytes(unicodeString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SmtpClient smtpServer = new SmtpClient();
                smtpServer.Credentials = new System.Net.NetworkCredential((string)appConf.mail["user"], (string)appConf.mail["password"]);
                smtpServer.Port = Convert.ToInt32((string)appConf.mail["port"]);
                smtpServer.Host = (string)appConf.mail["server"];
                if((string)appConf.mail["ssl"]=="true")  smtpServer.EnableSsl = true;
                else smtpServer.EnableSsl = false;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress((string)appConf.mail["user"], (string)appConf.mail["userFrom"], System.Text.Encoding.UTF8);
                mailMessage.To.Add(to.Text);
                mailMessage.Subject = subject.Text;
                mailMessage.Body = body.Text;

                byte[] bytes = GetBytes(document);
                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

                Attachment attach;

                using (var msw = new MemoryStream(bytes))
                {
                    if (isHTML)
                    {
                        attach = new Attachment(msw, "document.html");
                    }
                    else
                    {
                        attach = new Attachment(msw, "document.txt"); 
                    }
                    attach.TransferEncoding = TransferEncoding.SevenBit;
                    mailMessage.Attachments.Add(attach);

                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    mailMessage.ReplyTo = new MailAddress(from.Text);
                    smtpServer.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Email Failed. Something went wrong. Error: " + ex.Message);
            }
            this.Close();
        }

        private void EmailDocument_Load(object sender, EventArgs e)
        {

        }
    }
}
