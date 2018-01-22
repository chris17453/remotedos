using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Collections;
using dm;



namespace dmPrint
{
    public partial class RemotePrint : Form
    {

        string appDirectory = System.IO.Directory.GetParent(System.IO.Path.GetDirectoryName(Application.ExecutablePath)).FullName;
        
        appConfig appConf;
        string rowID = "";    
        
        public RemotePrint(string[] args){
            
            string ap="false";
            string id="0";
            string app = "";
            if (args.Length > 0)
            {
                if (args.Length > 1) app = args[0];
                if (args.Length > 0) id = args[1];
                if (args.Length > 1) ap= args[2];
            }
            getOldPrinterValues();
            InitializeComponent();
            if (ap == "N" || ap=="false") this.autoPrint = false; 
            else                          this.autoPrint = true;
            if (app == ""){
                appConf = new appConfig(appDirectory + "\\config\\defaultConfig.txt");
            } else {
                appConf = new appConfig(appDirectory + "\\config\\" + app + "Config.txt");
            }
            rowID = id;
            
        }

        public string document = "";
        public bool isHTML = false;
        public string pr = "14";
        public string k0 = "14";
        public string k2 = "19";
        public object bottom;
        public object top;
        public object left;
        public object right;
        public object header;
        public object footer;
        bool autoPrint = false;
        Hashtable row;
            

        public string cleanString(string unicodeString) {
            // Create two different encodings.
          Encoding ascii = Encoding.ASCII;
          Encoding unicode = Encoding.Unicode;

          if (null == unicodeString) return "";
          // Convert the string into a byte array. 
          byte[] unicodeBytes = unicode.GetBytes(unicodeString);
          
        
          // Perform the conversion from one encoding to the other. 
          byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicodeBytes);

          // Convert the new byte[] into a char[] and then into a string. 
          char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
          ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
          return new string(asciiChars);
        }
        
        public void RemoveIEPrintHeaderAndFooterPacific()
        {
            try
            {
                string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
                bool writable = true;
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, writable);
                registryKey.SetValue("header", (object)"");
                registryKey.SetValue("footer", (object)"");
                registryKey.SetValue("margin_left", (object)".2");
                registryKey.SetValue("margin_right", (object)".2");
                registryKey.SetValue("margin_top", (object)".5");
                registryKey.SetValue("margin_bottom", (object)".2");
            }
            catch
            {
            }
        }

        public void RemoveIEPrintHeaderAndFooterCanada()
        {
            try
            {
                string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
                bool writable = true;
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, writable);
                registryKey.SetValue("header", (object)"");
                registryKey.SetValue("footer", (object)"");
                registryKey.SetValue("margin_left", (object)".2");
                registryKey.SetValue("margin_right", (object)".2");
                registryKey.SetValue("margin_top", (object)".2");
                registryKey.SetValue("margin_bottom", (object)".0");
            }
            catch
            {
            }
        }

        private string printRaw(string document)
        {
            string keyName = @"Software\Microsoft\Internet Explorer\PageSetup";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyName, true))
            {
                if (key != null)
                {
                    key.SetValue("footer", "");
                    key.SetValue("header", "");
                    key.SetValue("margin_bottom", 15);
                    key.SetValue("margin_top", 0.15);
                    key.SetValue("margin_left", 0.15);
                    key.SetValue("margin_right", 0.15);

                }
            }

            Text = document;
            string pageTemplate = "<html><header><style type='text/css'>.footer {page-break-after: always;} .pr{ font-size:" + pr + "px; }.k0{ font-size:" + k0 + "px;} .k2{font-size:" + k2 + "px;}</style></header><body><pre class='pr'>{DATA}</pre></body></html>";
            //          Text = Text.Replace("\", "</pre><pre class='k2'>");
            Text = Text.Replace("&k2S", "</pre><pre class='k2'>");
            Text = Text.Replace("&k0S", "</pre><pre class='k0'>");
            Text = Text.Replace("", "</pre><div class='footer'></div><pre class='pr'>");//new page character
            pageTemplate = pageTemplate.Replace("{DATA}", Text);
            document = pageTemplate;

            return pageTemplate;

        }
        
        private string formatPacificCheck2(string document)
        {
            //RemoveIEPrintHeaderAndFooterPacific();
            byte[] docBytes = Encoding.ASCII.GetBytes(document);
            //int ff = 0;
            int len = document.Length;

            StringBuilder page = new StringBuilder();
            StringBuilder checkPages = new StringBuilder();
            StringBuilder memoPages = new StringBuilder();

            for (int i = 0; i < len; i++)
            {                                 //delete extra form feed
                if (docBytes[i] != 12) page.Append((char)docBytes[i]);
                if (docBytes[i] == 12)
                {

                    bool isNotePage = false;
                    if (page.ToString().IndexOf("**** VOID ****") > -1) isNotePage = true;
                    if (isNotePage)
                    {
                        page.Append("");
                        memoPages.Append(page);
                        page = new StringBuilder();
                        //ff = 0;
                        continue;
                    }
                    if (!isNotePage)
                    {                    // This is a check page append to  checks string
                        string[] lines = page.ToString().Split('\r');
                        if (lines.Length == 121)
                        {
                            page = new StringBuilder();
                            for (int a = 0; a < lines.Length; a++)
                            {
                                lines[a] = lines[a].Replace('\n', ' ');
                                lines[a] = lines[a].Replace('', ' ');           //delete FF
                                if (a < 60) page.Append(lines[a] + "\r\n");//
                            }
                            continue;
                        }
                        //ff++; if (ff == 1) continue;
                        //ff = 0;

                        page = new StringBuilder();
                        for (int a = 0; a < lines.Length; a++)
                        {
                            lines[a] = lines[a].Replace('\n', ' ');
                            lines[a] = lines[a].Replace('', ' ');           //delete FF
                            if (a != 18 && a != 19 && a != 20 && a != 64 && a != 65) page.Append(lines[a] + "\r\n");
                        }
                        page.Append("");
                        checkPages.Append(page);
                    }
                    page = new StringBuilder();
                }
            }




            if (page.ToString() != "")
            {
                bool flag1 = false;
                if (page.ToString().IndexOf("**** VOID ****") > -1) flag1 = true;
                if (flag1){
                    page.Append("");
                    memoPages.Append(page);
                }else{
                    checkPages.Append(page);
                }
            }

            string[] lines2 = checkPages.ToString().Split('');
            for (int a = lines2.Length - 1; a > -1; a--)
            {
                if (lines2[a].Trim() == "") { lines2[a] = ""; } else break;

            }
            StringBuilder cp = new StringBuilder();
            foreach (string line in lines2) { cp.Append(line); }

            string[] lines3 = memoPages.ToString().Split('');
            for (int a = lines3.Length - 1; a > -1; a--)
            {
                if (lines3[a].Trim() == "") { lines3[a] = ""; } else break;

            }
            StringBuilder mp = new StringBuilder();
            foreach (string line in lines3) { mp.Append(line); }


            String Text = cp.ToString() + mp.ToString();
            document = Text;
            //String Text = checkPages.ToString() + memoPages.ToString();

            string pageTemplate = "<html><header><style type='text/css'> body{ margin:0px; padding:0px;} .footer {page-break-after: always;} pre { line-height:16px; letter-spacing:1px; font-family: monospace; font-weight:bold; font-size: 14px; } .pr{  font-size:" + pr + "px; }.k0{ font-size:" + k0 + "px;} .k2{font-size:" + k2 + "px;}</style></header><body><pre class='pr'>{DATA}</pre></body></html>";
            //          Text = Text.Replace("\", "</pre><pre class='k2'>");
            Text = Text.Replace("&k2S", "</pre><pre class='k2'>");
            Text = Text.Replace("&k0S", "</pre><pre class='k0'>");
            Text = Text.Replace("", "</pre><div class='footer'></div><pre class='pr'>");//new page character
            pageTemplate = pageTemplate.Replace("{DATA}", Text);
            return pageTemplate;
        }

        private string formatPacificCheck(string document)
    {
      this.RemoveIEPrintHeaderAndFooterPacific();
      byte[] bytes = Encoding.ASCII.GetBytes(document);
      int num = 0;
      int length = document.Length;
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      StringBuilder stringBuilder3 = new StringBuilder();
      for (int index1 = 0; index1 < length; ++index1)
      {
        if ((int) bytes[index1] != 12)
          stringBuilder1.Append((char) bytes[index1]);
        if ((int) bytes[index1] == 12)
        {
          bool flag = false;
          if (stringBuilder1.ToString().IndexOf("**** VOID ****") > -1)
            flag = true;
          if (flag)
          {
            stringBuilder1.Append("\f");
            stringBuilder3.Append((object) stringBuilder1);
            stringBuilder1 = new StringBuilder();
            num = 0;
          }
          else
          {
            if (!flag)
            {
              string[] strArray = stringBuilder1.ToString().Split('\r');
              if (strArray.Length == 121)
              {
                stringBuilder1 = new StringBuilder();
                for (int index2 = 0; index2 < strArray.Length; ++index2)
                {
                  strArray[index2] = strArray[index2].Replace('\n', ' ');
                  strArray[index2] = strArray[index2].Replace('\f', ' ');
                  if (index2 < 60)
                    stringBuilder1.Append(strArray[index2] + "\r\n");
                }
                continue;
              }
              ++num;
              if (num != 1)
              {
                num = 0;
                StringBuilder stringBuilder4 = new StringBuilder();
                for (int index2 = 0; index2 < strArray.Length; ++index2)
                {
                  strArray[index2] = strArray[index2].Replace('\n', ' ');
                  strArray[index2] = strArray[index2].Replace('\f', ' ');
                  if (index2 != 18 && index2 != 19 && (index2 != 20 && index2 != 64) && index2 != 65)
                    stringBuilder4.Append(strArray[index2] + "\r\n");
                }
                stringBuilder4.Append("\f");
                stringBuilder2.Append((object) stringBuilder4);
              }
              else
                continue;
            }
            stringBuilder1 = new StringBuilder();
          }
        }
      }
      if (stringBuilder1.ToString() != "")
      {
        bool flag = false;
        if (stringBuilder1.ToString().IndexOf("**** VOID ****") > -1)
          flag = true;
        if (flag)
        {
          stringBuilder1.Append("\f");
          stringBuilder3.Append((object) stringBuilder1);
        }
        else
          stringBuilder2.Append((object) stringBuilder1);
      }
      string[] strArray1 = stringBuilder2.ToString().Split('\f');
      for (int index = strArray1.Length - 1; index > -1 && strArray1[index].Trim() == ""; --index)
        strArray1[index] = "";
      StringBuilder stringBuilder5 = new StringBuilder();
      foreach (string str in strArray1)
        stringBuilder5.Append(str);
      string[] strArray2 = stringBuilder3.ToString().Split('\f');
      for (int index = strArray2.Length - 1; index > -1 && strArray2[index].Trim() == ""; --index)
        strArray2[index] = "";
      StringBuilder stringBuilder6 = new StringBuilder();
      foreach (string str in strArray2)
        stringBuilder6.Append(str);
      string str1 = stringBuilder5.ToString() + stringBuilder6.ToString();
      document = str1;
      return ("<html><header><style type='text/css'> body{ margin:0px; padding:0px;} .footer {page-break-after: always;} pre { line-height:16px; letter-spacing:1px; font-family: monospace; font-weight:bold; font-size: 14px; } .pr{  font-size:" + this.pr + "px; }.k0{ font-size:" + this.k0 + "px;} .k2{font-size:" + this.k2 + "px;}</style></header><body><pre class='pr'>{DATA}</pre></body></html>").Replace("{DATA}", str1.Replace("&k2S", "</pre><pre class='k2'>").Replace("&k0S", "</pre><pre class='k0'>").Replace("\f", "</pre><div class='footer'></div><pre class='pr'>"));
    }

        private void getOldPrinterValues()
        {
            try
            {
                string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
                bool writable = true;
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, writable);
                header = registryKey.GetValue("header");
                footer = registryKey.GetValue("footer");
                left = registryKey.GetValue("margin_left");
                right = registryKey.GetValue("margin_right");
                top = registryKey.GetValue("margin_top");
                bottom = registryKey.GetValue("margin_bottom");
            }
            catch
            {
            }
        }
        
        public void setOldPrinterValues()
        {
            try
            {
                string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
                bool writable = true;
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, writable);
                registryKey.SetValue("header", header);
                registryKey.SetValue("footer", footer);
                registryKey.SetValue("margin_left", left);
                registryKey.SetValue("margin_right", right);
                registryKey.SetValue("margin_top", top);
                registryKey.SetValue("margin_bottom", bottom);
            }
            catch
            {
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (autoPrint) this.autoPrintWeb();

        }

        private void emailB_Click(object sender,EventArgs e) {
            EmailDocument ed=new EmailDocument(document,appConf);
            ed.Show();
            ed.FormClosed+=ed_FormClosed;
        }

        void ed_FormClosed(object sender,FormClosedEventArgs e) {
            this.Close();
        }

        static byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(),0,bytes,0,bytes.Length);
            return bytes;
        }

        private void saveB_Click(object sender, EventArgs e){
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (isHTML)
            {
                saveFileDialog1.Filter = "HTML File|*.html";
            }
            else
            {
                saveFileDialog1.Filter = "Text File|*.txt";
            }
            saveFileDialog1.Title = "Save a Document";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                using (StreamWriter sw = new StreamWriter(File.Open(saveFileDialog1.FileName, FileMode.Create), Encoding.UTF8))
                {
                    sw.Write(document);
                }
                //System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog1.OpenFile();
                //byte[] docBytes = GetBytes(document);
                //fs.Write(docBytes, 0, docBytes.Length);
                //fs.Close();
                this.Close();
            }
        }
        private void printB_Click(object sender,EventArgs e) {
            //this.RemoveIEPrintHeaderAndFooterPacific();
            this.preview.ShowPrintPreviewDialog();
            
            //System.Threading.Thread.Sleep(1000);
            //this.Close();
        }

        public void autoPrintWeb() {
            this.preview.Print();
            this.WindowState = FormWindowState.Minimized;
            closeTimer.Start();
        }

        
        private void Print_FormClosing(object sender, FormClosingEventArgs e)
        {
            setOldPrinterValues();
        }

        private void RemotePrint_Load(object sender, EventArgs e) {
            if (!appConf.loaded)
            {
                this.Close();                              //if the config failed to load exit.
                return;
            }
            dm.mysqldb.connect( (string)appConf.db["server"], 
                                (string)appConf.db["user"], 
                                (string)appConf.db["password"]);
            dm.mysqldb.selectDB((string)appConf.db["database"]);

            string query = string.Format("SELECT * FROM `_print` where `id`='{0}'", rowID);//
            row = dm.mysqldb.fetch(query);
            if (null == row)
            {
                this.Close();              //if no row to print die...
                return;
            }

            
            string Text = (string)row["document"];
            string filename = "";
            string path = "";
            Text = cleanString(Text);
            if (String.IsNullOrEmpty((string)row["path"])) path = ""; else path = (string)row["path"];
            if (String.IsNullOrEmpty((string)row["filename"])) filename = ""; else filename = (string)row["filename"];

            path = path.ToUpper();
            filename = filename.ToUpper();
            if (Text.IndexOf("<body>") > -1)
            {           //this is HTML!
                preview.DocumentText = Text;
                isHTML = true;
            }
            else
            {
                if (filename.Length > 2 && filename[0] == 'A' && filename[1] == 'F')
                {
                    if (path.IndexOf("PACIFIC") > -0) {
                        this.RemoveIEPrintHeaderAndFooterPacific();
                        
                        preview.DocumentText = formatPacificCheck(Text);
                    }else {
                        if (path.IndexOf("CANADA") > -0) {
                            this.RemoveIEPrintHeaderAndFooterCanada();
                            preview.DocumentText = formatPacificCheck(Text);
                        }else {
                            preview.DocumentText = printRaw(Text);
                        }
                    }
                }
                else
                {
                    preview.DocumentText = printRaw(Text);
                }

            }
            document = Text;
        }
    }
}