using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;                   //Hashtable
//using System.ComponentModel;

using Microsoft.Win32;


namespace dmRDClient_PC
{
    public partial class pastDocuments : Form
    {
        public string uid = "";
        string pr = "14";
        string k0 = "14";
        string k2 = "19";

        public pastDocuments(string uid) 
        {
            
            getOldPrinterValues();
            this.uid=uid;
            InitializeComponent();
            documents.Columns.Add("ID", "ID");
            documents.Columns.Add("File", "File");
            documents.Columns.Add("Created", "Created");
        }
        public List<Hashtable> documentList;
        private void pastDocuments_Load(object sender, EventArgs e)
        {
            string query = string.Format("SELECT `id`,`created`,`document`,`path`,`filename` FROM `_print`  WHERE `created`>'2015-09-27' AND `uid`='{0}' order by `id` DESC LIMIT 100 ", uid);  //`uid` = '{0}' AND printed='1'   `filename` like 'AF%'
            documentList = dm.mysqldb.fetchAll(query);

            foreach (Hashtable row in documentList)
            {
                documents.Rows.Add((string)(row["id"]),(string)row["filename"],(string)(row["created"]),row["document"],row["path"]);

            }



        }


        
        public void RemoveIEPrintHeaderAndFooterPacific()
        {
          try
          {
            string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
            bool writable = true;
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, writable);
            registryKey.SetValue("header", (object) "");
            registryKey.SetValue("footer", (object) "");
            registryKey.SetValue("margin_left", (object) ".2");
            registryKey.SetValue("margin_right", (object) ".2");
            registryKey.SetValue("margin_top", (object) ".5");
            registryKey.SetValue("margin_bottom", (object) ".2");
          }
          catch
          {
          }
        }

        private void getOldPrinterValues(){
            try
            {
                string name = "Software\\Microsoft\\Internet Explorer\\PageSetup";
                bool writable = true;
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(name, writable);
                header=registryKey.GetValue("header");
                footer=registryKey.GetValue("footer");
                left=registryKey.GetValue("margin_left");
                right=registryKey.GetValue("margin_right");
                top=registryKey.GetValue("margin_top");
                bottom=registryKey.GetValue("margin_bottom");
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
        public object bottom;
        public object top;
        public object left;
        public object right;
        public object header;
        public object footer;


        private string formatPacificCheck(string document)
        {
            RemoveIEPrintHeaderAndFooterPacific();
            byte[] docBytes=Encoding.ASCII.GetBytes(document);
            int ff=0;
            int len = document.Length;

            StringBuilder page = new StringBuilder();
            StringBuilder checkPages = new StringBuilder();
            StringBuilder memoPages = new StringBuilder();

            for (int i = 0; i < len; i++){                                 //delete extra form feed
                if (docBytes[i] != 12) page.Append((char)docBytes[i]);
                if (docBytes[i] == 12){
                
                    bool flag1 = false;
                    if (page.ToString().IndexOf("**** VOID ****") > -1) flag1 = true;
                    if (flag1) {
                        page.Append("");
                        memoPages.Append(page);
                        page = new StringBuilder(); 
                        ff = 0;
                        continue;
                    }
                    if (!flag1){                    // This is a check page append to  checks string
                        string[] lines = page.ToString().Split('\r');
                        if (lines.Length == 121){
                            page = new StringBuilder();
                            for (int a = 0; a < lines.Length; a++)
                            {
                                lines[a] = lines[a].Replace('\n', ' ');
                                lines[a] = lines[a].Replace('',' ');           //delete FF
                                if (a<60) page.Append(lines[a] + "\r\n");//
                            }
                            continue; 
                        }
                        ff++; if (ff == 1) continue;
                        ff = 0;
                        
                        page = new StringBuilder();
                        for (int a = 0; a < lines.Length; a++){
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
                if (flag1) {
                    page.Append("");
                    memoPages.Append(page);
                    
                } else {
                    checkPages.Append(page);
                    
                }
            }
            
            string[] lines2=checkPages.ToString().Split('');
            for (int a = lines2.Length - 1;a>-1;a--){
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
            

           String Text = cp.ToString()+mp.ToString();
          //  String Text = checkPages.ToString() + memoPages.ToString();
            string pageTemplate = "<html><header><style type='text/css'> body{ margin:0px; padding:0px;} .footer {page-break-after: always;} pre { line-height:16px; letter-spacing:1px; font-family: monospace; font-weight:bold; font-size: 14px; } .pr{  font-size:" + pr+ "px; }.k0{ font-size:" + k0 + "px;} .k2{font-size:" + k2 + "px;}</style></header><body><pre class='pr'>{DATA}</pre></body></html>";
//          Text = Text.Replace("\", "</pre><pre class='k2'>");
            Text=Text.Replace("&k2S","</pre><pre class='k2'>");
            Text=Text.Replace("&k0S","</pre><pre class='k0'>");
            Text = Text.Replace("", "</pre><div class='footer'></div><pre class='pr'>");//new page character
            pageTemplate =pageTemplate.Replace("{DATA}", Text);
            return pageTemplate;
        }
                    
        
      




        private void documents_SelectionChanged(object sender, EventArgs e)
        {
            string id = (string)documents.SelectedRows[0].Cells[0].Value;
            string a = "";
            foreach (Hashtable row in documentList)
            {
                if ((string)row["id"] == id){
                    string Text = (string)row["document"];
                    string filename ="";
                    string path="";
                    string documentString="";
                    if (String.IsNullOrEmpty((string)row["path"])) path = ""; else path = (string)row["path"];
                    if (String.IsNullOrEmpty((string)row["filename"])) filename= ""; else filename = (string)row["filename"];
                    
                    path = path.ToUpper();
                    filename = filename.ToUpper();
                    if (Text.IndexOf("<body>") > -1)
                    {                     //this is HTML!
                        documentString = Text;
                    }else{
                        if(filename.Length>2 && filename[0]=='A' && filename [1]=='F') {
                            if(path.IndexOf("PACIFIC")>-0)  documentString = formatPacificCheck(Text); else 
                            if(path.IndexOf("CANADA") > -0) documentString = formatPacificCheck(Text); else 
                            documentString = printRaw(Text); 
                        } else {
                            documentString= printRaw(Text); 
                        }
                            
                    }
                    preview.DocumentText = documentString;
                }
            }
        }//end of switch
        
        private string printRaw(string document){
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

            Text= document;
            string pageTemplate = "<html><header><style type='text/css'>.footer {page-break-after: always;} .pr{ font-size:"+pr+"px; }.k0{ font-size:"+k0+"px;} .k2{font-size:"+k2+"px;}</style></header><body><pre class='pr'>{DATA}</pre></body></html>";
//          Text = Text.Replace("\", "</pre><pre class='k2'>");
            Text=Text.Replace("&k2S","</pre><pre class='k2'>");
            Text=Text.Replace("&k0S","</pre><pre class='k0'>");
            Text = Text.Replace("", "</pre><div class='footer'></div><pre class='pr'>");//new page character
            pageTemplate =pageTemplate.Replace("{DATA}", Text);
            return pageTemplate;
                    
        }
        
        public byte[] GetBytes(string str)
        {
            
            return 	Encoding.ASCII.GetBytes(str);

        }

        private void print_Click(object sender, EventArgs e)
        {
            preview.Print();
        }

        private void prtPreview_Click(object sender, EventArgs e)
        {
            preview.ShowPrintPreviewDialog();
        }


        private void pastDocuments_FormClosing(object sender, FormClosingEventArgs e)
        {
            setOldPrinterValues();
        }
    }
}
