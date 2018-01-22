using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

using System.Globalization;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;


using Microsoft.Win32;
using System.Diagnostics;
using System.Management;
using System.Threading;
using dm;


namespace dmRDClient_PC {
    public partial class ui : Form {
        public string uid = "CDUB";
        public string pid = "";
        public string server = "";
        public string app = "";
        public string appText = "";
        public bool useHTTP = false;
        public bool manual = false;
        string autoPrint = "Y";
        string appDirectory = System.IO.Directory.GetParent(System.IO.Path.GetDirectoryName(Application.ExecutablePath)).FullName;
                
        bool connected = false;
        int activityIndex = 0;
        int activeInterval = 300;
        int idleInterval1 = 1 * 1000;
        int idleInterval2 = 60 * 1000;
        string conserveBW = "N";

        public class mouseStruct {
            public bool Left = false;
            public bool Middle = false;
            public bool Right = false;
            public int x = 0;
            public int y = 0;
            public mouseStruct() {
            }

        }
        public mouseStruct mouse = new mouseStruct();

        class newTarget {
            public string pid = "0";
            public string server = "test";
        }

        class targetData {
            public string s = "";
            public string p = "";
            public int up = 0;
            public int cursorX = 0;
            public int cursorY = 0;
            public string close = "";
        }

        public ui(string[] args) {
            uid = "//" + Environment.UserDomainName + "/" + Environment.UserName;
            if (args.Length > 0) {
                if (args.Length > 0) app = args[0];
                if (args.Length > 1) uid = args[1];
                if (args.Length > 2) server = args[2];
                if (args.Length > 3) pid = args[3];
            }
            
            if (pid != "") manual = true;
            InitializeComponent();
            setSmall();
            if (app == ""){
                appConf = new appConfig(appDirectory+"\\config\\defaultConfig.txt");
            }else{
                appConf = new appConfig(appDirectory+"\\config\\"+app + "Config.txt");
            }
            appText =(string)appConf.app["title"];
            dosScreen1.colorMapFG = appConf.colorMapFG;
            dosScreen1.colorMapBG = appConf.colorMapBG;
            dosScreen1.keyMap     = appConf.keyMap;
        }

        appConfig appConf;

        public static string GetUniqueKey()
        {
            int maxSize = 8;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data) { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }  

        public string LocalIPAddress() {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList) {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            connected = false;
            string closeString = "?a=1&c=close&uid=" + uid + "&pid=" + pid + "&svr=" + server;
           // sendData(closeString);
            string query = 
                String.Format(
                 "INSERT INTO _preferences (`userID`,`autoPrint`,`size`,`width`,`height`,`conserveBW`) "+
                "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}') "+
                "ON DUPLICATE KEY UPDATE `autoPrint`='{1}',`size`='{2}',`width`='{3}',`height`='{4}',`conserveBW`='{5}'", 
                userID,autoPrint,size,this.Width,this.Height,this.conserveBW);

            dm.mysqldb.nonQuery(query);

        }

        private void parseScreenReturnData(string json) {
            if (json.Length == 0) return;
            if (json[0] == 'T') this.Close();
            dosScreen1.cursorX = (byte)json[1];
            dosScreen1.cursorY = (byte)json[2];
            if (json[4] == '1') {


                byte[] data = System.Text.Encoding.GetEncoding(1252).GetBytes(json);
                byte[] sc = new byte[4000];
                for (int a = 0; a < 4000; a++) sc[a] = data[5 + a];
                dosScreen1.screenData = sc;
            }
            //if(data.p != "0") print(data.p);

        }

        private void pullTimer_Tick(object sender, EventArgs e) {
            if (connected) {
           /*     if (useHTTP) {
                    string pullString = String.Format("?a=1&c=load2&pid={0}&svr={1}&n={2}&p={3}&uid={4}", pid, server, 0, 0, uid);
                    string json = sendData(pullString);
                    parseReturnData(json);
                } else {*/
                    pullDBScreen();
                //}
            }
        }

        private void pullDBScreen() {


            string query = string.Format("SELECT `screen`,`cursorX`,`cursorY`,`pulled` FROM `_sessions_b` WHERE `pid` = '{0}' AND `server` = '{1}' LIMIT 1", pid, server);
            //row=dm.mysqldb.fetch(query);
            using (MySqlCommand cmdCommand = new MySqlCommand(query, dm.mysqldb.conn)) {
                MySqlDataReader reader = cmdCommand.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        int x = -1, y = -1;
                        x = (int)reader.GetInt16(1);
                        y = (int)reader.GetInt16(2);
                        if (x > -1) dosScreen1.cursorX = x;
                        if (y > -1) dosScreen1.cursorY = y;
                        dosScreen1.forceShow = forceShow;
                        byte[] screen=(byte[])reader[0];
                        if(null!=screen && screen.Length>0)  dosScreen1.screenData = screen;//(byte[])reader[0];
                    }
                }
                reader.Close();
            }
            //query =string.Format("UPDATE `_sessions` SET `pulled`='Y' WHERE `pid` = '{0}' AND `server` = '{1}'",pid,server);

        }

        private byte[] hashToScreen(string data) {
            byte[] screen = new byte[4000];
            int i = 0, number = 0;
            CultureInfo provider = CultureInfo.InvariantCulture;//new CultureInfo("en-US");
            for (int a = 0; a < data.Length; a += 2) {
                //      try{
                //string value =data.Substring(a,2);
                number = 0;
                switch (data[a]) {
                    case '0': number = 0; break;
                    case '1': number = 0x10; break;
                    case '2': number = 0x20; break;
                    case '3': number = 0x30; break;
                    case '4': number = 0x40; break;
                    case '5': number = 0x50; break;
                    case '6': number = 0x60; break;
                    case '7': number = 0x70; break;
                    case '8': number = 0x80; break;
                    case '9': number = 0x90; break;
                    case 'A': number = 0xA0; break;
                    case 'B': number = 0xB0; break;
                    case 'C': number = 0xC0; break;
                    case 'E': number = 0xE0; break;
                    case 'D': number = 0xD0; break;
                    case 'F': number = 0xF0; break;
                }
                switch (data[a + 1]) {
                    case '1': number += 0x1; break;
                    case '2': number += 0x2; break;
                    case '3': number += 0x3; break;
                    case '4': number += 0x4; break;
                    case '5': number += 0x5; break;
                    case '6': number += 0x6; break;
                    case '7': number += 0x7; break;
                    case '8': number += 0x8; break;
                    case '9': number += 0x9; break;
                    case 'A': number += 0xA; break;
                    case 'B': number += 0xB; break;
                    case 'C': number += 0xC; break;
                    case 'E': number += 0xE; break;
                    case 'D': number += 0xD; break;
                    case 'F': number += 0xF; break;
                }

                //bool result = Int32.TryParse(value, NumberStyles.HexNumber, provider, out number);
                //if(result) 
                screen[i] = (byte)number; //else screen[i]=0;
                i++;
                //              }
                //                catch { }
            }
            return screen;
        }

        int size = 0;
        public void setSmall() {
            size = 0;
            sizeToolStripMenuItem.Checked = true;
            mediumToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
            hugeToolStripMenuItem.Checked = false;
            
            int lh = 16, cw = 9, fs = 10;
            int PaddingWidth = this.Width - this.ClientSize.Width;
            int PaddingHeight = this.Height - this.ClientSize.Height;
            this.Width = 80 * cw + PaddingWidth;
            this.Height = 25 * lh + PaddingHeight;
            dosScreen1.fontSize = fs;
            dosScreen1.lineHeight = lh;
            dosScreen1.columnWidth = cw;
            //dosScreen1.fontName = "Moder DOS 437"; //perfect dos vga 437 win
            dosScreen1.fontName = "courier new";
        }
        public void setMedium() {
            sizeToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = true;
            largeToolStripMenuItem.Checked = false;
            hugeToolStripMenuItem.Checked = false;
            size = 1;
            int lh = 19, cw = 11;
            float fs = 13f;
            int PaddingWidth = this.Width - this.ClientSize.Width;
            int PaddingHeight = this.Height - this.ClientSize.Height;
            this.Width = 80 * cw + PaddingWidth;
            this.Height = 25 * lh + PaddingHeight;
            dosScreen1.fontSize = fs;
            dosScreen1.lineHeight = lh;
            dosScreen1.columnWidth = cw;
            dosScreen1.fontName = "Lucida Console";
        }
        public void setLarge(){
            sizeToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = true;
            hugeToolStripMenuItem.Checked = false;
            size = 2;
            int lh = 22, cw = 13, fs = 15;
            int PaddingWidth = this.Width - this.ClientSize.Width;
            int PaddingHeight = this.Height - this.ClientSize.Height;
            this.Width = 80 * cw + PaddingWidth;
            this.Height = 25 * lh + PaddingHeight;
            dosScreen1.fontSize = fs;
            dosScreen1.lineHeight = lh;
            dosScreen1.columnWidth = cw;
            dosScreen1.fontName = "Lucida Console";
        }
        public void setHuge(){
            sizeToolStripMenuItem.Checked = false;
            mediumToolStripMenuItem.Checked = false;
            largeToolStripMenuItem.Checked = false;
            hugeToolStripMenuItem.Checked = true;
            size = 3;
            int lh = 24, cw = 15;
            float fs = 16f;
            int PaddingWidth = this.Width - this.ClientSize.Width;
            int PaddingHeight = this.Height - this.ClientSize.Height;
            this.Width = 80 * cw + PaddingWidth;
            this.Height = 25 * lh + PaddingHeight;
            dosScreen1.fontSize = fs;
            dosScreen1.lineHeight = lh;
            dosScreen1.columnWidth = cw;
            dosScreen1.fontName = "Lucida Console";
        }
        private void sizeToolStripMenuItem_Click(object sender, EventArgs e) {
            setSmall();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e) {
            setMedium();
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e) {
            setLarge();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            pullTimer.Interval = idleInterval1;
        }
 
        private void idleInteval2Timer_Tick(object sender, EventArgs e) {
            pullTimer.Interval = idleInterval2;
        }

        private void dosScreen1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) {
            if (conserveBW == "Y") {
                idleInterval1Timer.Stop();
                idleInteval2Timer.Stop();
                idleInterval1Timer.Start();
                idleInteval2Timer.Start();
            }
            pullTimer.Interval = activeInterval;
            if (!connected) return;

            if (useHTTP) {
//                httpKeyPress(e);
            } else {
                mysqlKeyPress(e);
            }
        }

      /*  public void httpKeyPress(PreviewKeyDownEventArgs e) {
            string modS = ".", modA = ".", modC = ".";
            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) modS = "s";
            if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt) modA = "a";
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control) modC = "c";
            string key = e.KeyValue.ToString();
            //keyData.

            string inputString = String.Format("?a=1&c=com&pid={0}&svr={1}&key={2}&sh={3}&al={4}&ct={5}&uid={6}", pid, server, key, modS, modA, modC, uid);
            string json = sendData(inputString);
            parseReturnData(json);
        }*/

        public bool getBit(byte value,byte bit){
            return (value & (1 << bit)) != 0;

        }
        public byte setBit(byte value, byte bit, bool status) {
            byte number = value;
            byte x = 0;
            if (status) x = 1;
            ushort p1 = (ushort)(-x ^ number);
            ushort p2 = (ushort)(1 << bit);
            ushort p4 = (ushort)(p1 & p2);
            number ^= (byte)p4;
            return number;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        [Flags]
        private enum kb_flags {
            INSERT = 0x80,
            CAPS = 0x40,
            NUM = 0x20,
            SCROLL = 0x10,
            ALT = 0x8,
            CTRL = 0x4,
            LEFT_SHIFT = 0x2,
            RIGHT_SHIFT = 0x1
        } ;

        public void mysqlKeyPress(PreviewKeyDownEventArgs e) {

            
            string modS = ".", modA = ".", modC = ".";
            string key = e.KeyValue.ToString();
            bool Ctrl  = false;
            bool Alt   = false;
            bool Shift = false;
            
            bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
            bool Scroll   = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;
            bool Insert   = (((ushort)GetKeyState(0x2D)) & 0xffff) != 0;
            bool NumLock  = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
            bool LShift   = (((ushort)GetKeyState(0xA0)) & 0xffff) != 0;
            bool RShift   = (((ushort)GetKeyState(0xA1)) & 0xffff) != 0;
            
            if(e.Shift)     Shift=true;
            if(e.Control)   Ctrl =true;
            if(e.Alt)       Alt  =true;
            if(Shift) modS = "s";
            if(Alt  ) modA = "a";
            if(Ctrl ) modC = "c";
            

           // bool CapsLock = false;
           // bool Scroll   = false;
           // bool Insert   = false;
            //bool NumLock  = false;
            //bool LShift   = false;
            //bool RShift   = false;
            //if ((Control.ModifierKeys & Keys.CapsLock   ) == Keys.CapsLock  ) CapsLock  =true;
            //if ((e.Modifiers & Keys.CapsLock   )==Keys.CapsLock )  CapsLock=true;
            //if ((e.Modifiers & Keys.Scroll     ) == Keys.Scroll    ) Scroll    =true;
            //if ((e.Modifiers & Keys.Insert     ) == Keys.Insert    ) Insert    =true;
            //if ((e.Modifiers & Keys.NumLock    ) == Keys.NumLock   ) NumLock   =true;
            //if ((Control.ModifierKeys & Keys.Shift      ) == Keys.Shift     ) Shift     =true;
            //if ((Control.ModifierKeys & Keys.Alt        ) == Keys.Alt       ) Alt       =true;
            //if ((Control.ModifierKeys & Keys.Control    ) == Keys.Control   ) 
            
            
            //

            byte kbState1 = 0, kbState2 = 0;
/*
            string t="";
            if(Insert) t+="IN=0 ";
            if(CapsLock) t+="CL=0 ";
            if(NumLock) t+="NL=0 ";
            if(Scroll) t+="SC=0 ";
            if(Alt) t+="AL=0 ";
            if(Ctrl) t+="CT=0 ";
            if(Shift) t+="SH=0 ";
            
            for(byte i=0;i<8;i++) if(this.getBit((byte)Control.ModifierKeys,i)) t+="T"; else t+="F";
            this.KEY.Text=t;*/
            if (Insert)     kbState1 = this.setBit(kbState1, 7, true);//insert active
            if (CapsLock)   kbState1 = this.setBit(kbState1, 6, true);//caps loc active  X
            if (NumLock)    kbState1 = this.setBit(kbState1, 5, true);//num loc active
            if (Scroll)     kbState1 = this.setBit(kbState1, 4, true);//scroll active     X
            if (Alt)        kbState1 = this.setBit(kbState1, 3, true);//alt depressed
            if (Ctrl)       kbState1 = this.setBit(kbState1, 2, true);//ctrl depressed
            if (Shift)      kbState1 = this.setBit(kbState1, 1, true);//left shift depressed
            //if(RShift)    kbState1 = this.setBit(kbState1, 0, true);//rshift depressed

            //kbstate2 = this.setBit(kbstate2, 7, true);//insert depressed
            //kbstate2 = this.setBit(kbstate2, 6, true);//caps loc depressed
            //kbstate2 = this.setBit(kbstate2, 5, true);//num loc depressed
            //kbstate2 = this.setBit(kbstate2, 4, true);//scroll depressed
            //kbstate2 = this.setBit(kbstate2, 3, true);//syspend key toggled
            //kbstate2 = this.setBit(kbstate2, 2, true);//system key depressed and held
            //kbstate2 = this.setBit(kbstate2, 1, true);//left alt depressed
            //kbstate2 = this.setBit(kbstate2, 0, true);//left ctrl depressed

            //       if(key!="17" && key!="18")
            {
                /*
                                |7|6|5|4|3|2|1|0|  40:17  Keyboard Flags Byte 0
                         | | | | | | | `---- right shift key depressed x
                         | | | | | | `----- left shift key depressed   x
                         | | | | | `------ CTRL key depressed          x
                         | | | | `------- ALT key depressed            x
                         | | | `-------- scroll-lock is active         x
                         | | `--------- num-lock is active             x
                         | `---------- caps-lock is active             x
                         `----------- insert is active                 x

                        |7|6|5|4|3|2|1|0|  40:18  Keyboard Flags Byte 1
                         | | | | | | | `---- left CTRL key depressed   x
                         | | | | | | `----- left ALT key depressed
                         | | | | | `------ system key depressed and held
                         | | | | `------- suspend key has been toggled
                         | | | `-------- scroll lock key is depressed
                         | | `--------- num-lock key is depressed
                         | `---------- caps-lock key is depressed
                         `----------- insert key is depressed
                                */
                string query = string.Format("INSERT INTO  `dm-remote-dos`.`_commands` (`uid`,`pid`,`server`,`code`,`shift`,`ctrl`,`alt`,`keyState1`,`keyState2`) VALUES ('" +
                                            "{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", uid, pid, server, key, modS, modC, modA, kbState1, kbState2);
                dm.mysqldb.query(query);
                pullDBScreen();

            }

        }

        private void dosScreen1_Load(object sender, EventArgs e) {

        }

        public void exitScanner(){
            Hashtable row;
            if(!manual) {
                string queryM = string.Format("SELECT `id`  FROM `_applications` WHERE `pid` = '{0}' AND `server` = '{1}' LIMIT 1", pid, server);
                row = dm.mysqldb.fetch(queryM);
                if(row == null) {
                    this.Close();
                } else
                    if((string)row["close"] == "true") {
                        this.Close();
                    }
            }
        }
        private void printTimer_Tick(object sender, EventArgs e) {
            string query = string.Format("SELECT `id` FROM `_print` where `pid` = '{0}' AND `server` = '{1}' AND printed='0' LIMIT 1", pid, server);
            GC.Collect();
 
            messageQueue();
            Hashtable row = dm.mysqldb.fetch(query);
            if (row == null) {
                return;
            }
            try {
                query = String.Format("UPDATE _print set `uid`='{1}',printed=printed+1 WHERE `id`='{0}'", (string)row["id"],uid);
                dm.mysqldb.query(query);
                print((string)row["id"]);

            } catch (Exception ex) {
            //    MessageBox.Show("Print Error occured. Is your printer installed? Err:"+ex.Message);
            }
        }

        public void print(string id) {
            if(id=="0" || id=="") return;
            DateTime currentDate = DateTime.Now;
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = false;
            string dir = (string)appConf.app["folder"];
            myProcess.StartInfo.WorkingDirectory = dir + @"bin\";
            myProcess.StartInfo.FileName = (string)appConf.app["printServer"];
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.Arguments = app+" "+id + " " + autoPrint.ToString();
            myProcess.Start();
        }

        private void messageQueue(){
            string query = string.Format("SELECT `id`,`title`,`message`,`created` FROM `_messages` where `uid` = '{0}' AND `read` = 'N' LIMIT 1", uid);
            
            Hashtable row = dm.mysqldb.fetch(query);
            if (row == null) {
                return;
            }
            try {
                query = String.Format("UPDATE _messages set `read`='Y' WHERE `id`='{0}'", (string)row["id"],uid);
                dm.mysqldb.query(query);
                MessageBox.Show((string)row["title"],(string)row["message"]);
            } catch (Exception ex) {
            
            }

            
        }

        private void dosScreen1_KeyDown(object sender, KeyEventArgs e) {
            e.SuppressKeyPress = true;
            if (e.KeyData == Keys.F10) {
                // Do what you want with the F10 key

            }
        }

        private void dosScreen1_Resize(object sender, EventArgs e) {
            dosScreen1.Update();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
            int a = 0;
        }

        private void dosScreen1_MouseClick(object sender, MouseEventArgs e) {
        }

        private void dosScreen1_MouseMove(object sender, MouseEventArgs e) {
            float cw = ((float)(this.Width)) / 0x270f;
            float lh = ((float)(this.Height)) / 0xC0f;
            int x = (int)(((float)e.X) * cw);
            int y = (int)(((float)e.Y) * lh);
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x > 0x270) x = 0x270;
            if (y > 0xC0) y = 0xC0;
            if (x != mouse.x || mouse.y != y) {

            }
            mouse.x = x;
            mouse.y = y;
            updateMouseDB();
        }

        public void updateMouseDB() {
            return;
            int l = 0, m = 0, r = 0;
            if (mouse.Left) l = 1;
            if (mouse.Middle) m = 1;
            if (mouse.Right) r = 1;

            string query = String.Format("INSERT INTO _mouse (`uid`,`pid`,`server`,`left`,`middle`,`right`,`x`,`y`) " +
                                         "VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                         uid, pid, server, l, m, r, mouse.x, mouse.y);
            dm.mysqldb.nonQuery(query);

        }

        private void dosScreen1_MouseDown(object sender, MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Left: mouse.Left = true; break;
                case MouseButtons.Middle: mouse.Middle = true; break;
                case MouseButtons.Right: mouse.Right = true; break;
            }
            updateMouseDB();
        }

        private void dosScreen1_MouseUp(object sender, MouseEventArgs e) {
            switch (e.Button) {
                case MouseButtons.Left: mouse.Left = false; break;
                case MouseButtons.Middle: mouse.Middle = false; break;
                case MouseButtons.Right: mouse.Right = false; break;
                //false; break;
            }
            updateMouseDB();
        }


        /***STARTUP****************************/
        /***STARTUP****************************/
        /***STARTUP****************************/
        /***STARTUP****************************/
        /***STARTUP****************************/
        /***STARTUP****************************/


        public DateTime startTime;
        string lastAppPID = "0";
        public bool forceShow = false;
        private void startupTimer_Tick(object sender, EventArgs e) {
            startupTimer.Stop();
            mysqlConnect();
        }

        private void scanNewApps_Tick_1(object sender, EventArgs e) {
            if (DateTime.Now.Subtract(startTime) > TimeSpan.FromSeconds(30)) {
                //MessageBox.Show("Try again in a moment. Cannot find your application.");
                this.Close();
                return;
            }
            string query = string.Format("SELECT `pid`  FROM `_applications` WHERE `uid` = '{1}' AND `server`='{0}' ORDER BY `created` DESC LIMIT 1", server, uid);

            Hashtable row = dm.mysqldb.fetch(query);

            if (null != row) {
                if (lastAppPID != (string)row["pid"] && (string)row["pid"]!="0") {
                    scanNewApps.Stop();
                    pid = (string)row["pid"];
                    this.Text = String.Format("{3} - User:{2}: PID:{0}, Server:{1}", pid, server, uid, appText);
                  //  MessageBox.Show("Connected");
                    forceShow = true;
                    pullDBScreen();
                    forceShow = false;
                    pullTimer.Start();
                    printTimer.Start();
                    idleTrigger.Start();
                    connected = true;
                    exitTimer.Start();          //this checks for application exit Server side...
                    dosScreen1.Visible = true;
                }
            }//end if
        }//end function

        public string userID="-1";

        bool mysqlConnect() {
            try {
                dm.mysqldb.connect((string)appConf.db["server"], (string)appConf.db["user"], (string)appConf.db["password"]);
                dm.mysqldb.selectDB((string)appConf.db["database"]);
                string ip = LocalIPAddress();
                if (server == "") {
                    string un = string.Format(
                        "INSERT INTO _users (`user`,`domain`,`ip`,`active`,`created`,`lastlogon`,`appServer`) VALUES " +
                        "('{0}','{1}','{2}','Y',now(),now(),'{3}')" +
                        "ON DUPLICATE KEY UPDATE `lastlogon`=now(),`ip`='{2}'", Environment.UserName, Environment.UserDomainName, ip, "appserver1");
                    dm.mysqldb.nonQuery(un);
                } else {
                    string un = string.Format(
                        "INSERT INTO _users (`user`,`domain`,`ip`,`active`,`created`,`lastlogon`,`appServer`) VALUES " +
                        "('{0}','{1}','{2}','Y',now(),now(),'{3}')" +
                        "ON DUPLICATE KEY UPDATE `lastlogon`=now(),`ip`='{2}'", Environment.UserName, Environment.UserDomainName, ip, server);
                    dm.mysqldb.nonQuery(un);
                }
                string query=String.Format("SELECT id FROM _users WHERE `user`='{0}' AND `domain`='{1}'", Environment.UserName, Environment.UserDomainName);
                Hashtable row = dm.mysqldb.fetch(query);
                if (null != row) {
                    userID=(string)row["id"];
                }

                if (server == "") {
                    query = "select s.server,IFNULL(a.count , 0) as count from  _servers s  left join ( " +
                    "select `server` as 'server',count(*) as count FROM _applications  group by server " +
                    ") a on s.server=a.server order by count asc,server asc LIMIT 1";
                    row = dm.mysqldb.fetch(query);
                    if (null != row) {
                        server = (string)row["server"];
                    } else return false;
                }
               
             if (pid == "") {
                    query = string.Format("SELECT `pid`  FROM `_applications` WHERE `uid` = '{1}' AND `server`='{0}' ORDER BY `created` DESC LIMIT 1", server, uid);
                    row = dm.mysqldb.fetch(query);
                    if (null != row) {
                        lastAppPID = (string)row["pid"];
                    }
                    if (this.app != "nc" && this.app != "np") {
                        string app = dm.mysqldb.escapeString(this.app);
                        query = string.Format("INSERT INTO  `dm-remote-dos`.`_commands` (`uid`,`server`,`code`) VALUES ('{0}','{1}','new|{2}')", uid, server, app);
                    } else {
                        query = string.Format("INSERT INTO  `dm-remote-dos`.`_commands` (`uid`,`server`,`code`) VALUES ('{0}','{1}','{2}')", uid, server, this.app);
                    }

                    dm.mysqldb.query(query);
                 /*
                    query = string.Format("SELECT `pid`  FROM `_applications` WHERE `uid` = '{1}' AND `server`='{0}' ORDER BY `created` DESC LIMIT 1", server, uid);

                    row = dm.mysqldb.fetch(query);

                    if (null != row) {
                        lastAppPID = (string)row["pid"];
                    }*/

                    query=String.Format("SELECT * FROM _preferences WHERE `userID`='{0}'",userID);
                    row = dm.mysqldb.fetch(query);

                    if (null != row) {
                        int w=0,h=0;
                        int.TryParse((string)row["size"], out size);
                        int.TryParse((string)row["width"], out w);
                        int.TryParse((string)row["height"], out h);
                        if (w < 320) w = 320;
                        if (h < 240) h = 240;
                        switch (size) {
                            case 0: setSmall(); break;
                            case 1: setMedium(); break;
                            case 2: setLarge(); break;
                            case 3: setHuge(); break;
                        }
                        this.Width = w;
                        this.Height=h;
                    }
                    startTime = DateTime.Now;
                    if ((string)row["autoPrint"] == "Y") {
                        autoPrint = "Y";
                        autoPrintToolStripMenuItem.Checked = true;
                    } else {
                        autoPrint = "N";
                        autoPrintToolStripMenuItem.Checked = false;
                    }
                    if ((string)row["conserveBW"] == "Y")
                    {
                        conserveBW = "Y";
                        conserveBandwidthToolStripMenuItem.Checked = true;
                    } else {
                        conserveBW = "N";
                        pullTimer.Interval = activeInterval;
                        conserveBandwidthToolStripMenuItem.Checked = false;
                        idleInteval2Timer.Stop();
                        idleInterval1Timer.Stop();
                    }

                    scanNewApps.Start();
                    //Timer to checked now!
                    return true;
                }
            } catch (Exception ex) {
                  MessageBox.Show(ex.Message);
            }
            if (manual) {
                this.Text = String.Format("{3} - User:{2}: PID:{0}, Server:{1}", pid, server, uid, appText);
                connected = true;
                return true;
            }
            return false;
        }

        bool fullscreen = false;
        public bool showData = false;

        private void emailScreenShotToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            string fileName = appDirectory + "\\temp\\" + currentDate.Ticks.ToString() + '-' + app + "-" +pid+ "-screenShot.jpg";
            dosScreen1.screen.Save(fileName);
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = false;
            string dir = (string)appConf.app["folder"];
            myProcess.StartInfo.WorkingDirectory = dir + @"bin\";
            myProcess.StartInfo.FileName = (string)appConf.app["emailServer"];
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.Arguments = app +" "+ fileName+" delete";
            myProcess.Start();

        }

        private void saveScreenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPG files (*.jpg)|*.jpg";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dosScreen1.screen.Save(saveFileDialog1.FileName);
            }
            //dosScreen1.screen.Save(filename);
        }

        private void autoPrintToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
        }

        private void sizeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            setSmall();

        }

        private void mediumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            setMedium();
        }

        private void hugeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            setHuge();
        }

        private void largeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            setLarge();
        }

        private void fullScreenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (!fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
                fullscreen = true;
                fullScreenToolStripMenuItem.Checked = true;
            }
            else
            {
                FormBorderStyle = FormBorderStyle.Sizable;
                WindowState = FormWindowState.Normal;
                fullscreen = false;
                fullScreenToolStripMenuItem.Checked = false;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (autoPrintToolStripMenuItem.Checked) autoPrint = "Y"; else autoPrint = "N";
        }

        private void conserveBandwidthToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.conserveBandwidthToolStripMenuItem.Checked)
            {
                this.conserveBandwidthToolStripMenuItem.Checked = false;
                this.conserveBW = "N";
                idleInterval1Timer.Stop();
                idleInteval2Timer.Stop();
                pullTimer.Interval = activeInterval;
            }
            else
            {
                this.conserveBandwidthToolStripMenuItem.Checked = true;
                this.conserveBW = "Y";
                idleInterval1Timer.Start();
                idleInteval2Timer.Start();
            }
        }

        private void showDataToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (showData)
            {
                showData = false;
                showDataToolStripMenuItem.Checked = false;
            }
            else
            {
                showData = true;
                showDataToolStripMenuItem.Checked = true;
            }
            dosScreen1.showData = showData;
        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.Show();
        }

        private void rePrintDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pastDocuments p=new pastDocuments(uid);
            p.Show();
        }

        private void reprintLastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //id='135791'");//
            string query = string.Format("SELECT `id` FROM `_print` where `uid`='{0}'  ORDER BY created desc LIMIT 1", uid);//     string query = string.Format("SELECT `id`,`document`,`filename`,`path` FROM `_print` where `pid` = '{0}' AND `server` = '{1}' AND printed='0' LIMIT 1", pid, server);
         
            Hashtable row = dm.mysqldb.fetch(query);
            if (null == row) return;
            print((string)row["id"]);

        }

        private void printScreenToolStripMenuItem_Click(object sender, EventArgs e) {
            dosScreen1.Print();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (app == "") this.Close();
        }

        public bool compareScreen(byte[] a,byte[] b){
            byte c = 0,c2;
            int mPos=24 * 80 * 2 + 79 * 2;
            if(null==a  || null==b ||  a.Length<mPos || b.Length!=a.Length) return false;
            
            for (int y = 0; y < 25; y++)
            for (int x = 0; x < 80; x++)
            {
                int pos=y * 80 * 2 + x * 2;
                c = a[pos];
                if(c>96) continue;
                c2 = b[pos];
                if(c!=c2) 
                    return false;
            }
            return true;
        }
        private void idleTrigger_Tick(object sender, EventArgs e)
        {
            if(appConf.idleList.Count==0) return;
            foreach(appConfig.idle idle in appConf.idleList){
                if (idle.screen == null) continue;
                if (compareScreen(idle.screen, dosScreen1.screenData)) idle.elapsedTime++;
                else idle.elapsedTime = 0;
                if (idle.elapsedTime >= idle.idleTime)
                {
                    if (idle.action == "close") this.Close();
                }
            
            }
  
        }

        private void binaryScreenCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Binary File|*.bin";
            saveFileDialog1.Title = "Save a Document";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                BinaryWriter bw;
                try{
                    bw = new BinaryWriter(new FileStream(saveFileDialog1.FileName, FileMode.Create));
                }catch (IOException ex){
                    return;
                }

                try{
                    bw.Write(dosScreen1.screenData);
                }catch (IOException ex){
                    return;
                }
                bw.Close();
         
            }
        }

        private void exitTimer_Tick(object sender, EventArgs e) {
            exitScanner();
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e) {
            dm.mysqldb.close();
        }


    }//end class
}//end namespace