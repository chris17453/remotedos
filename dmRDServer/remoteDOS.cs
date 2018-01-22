using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Management;
using System.Threading;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace DM_RD_SERVER {
    public partial class rdServer : Form {
        //int parentPid = 0;

        string keyFileName = @"C:\dmRemoteDOS\config\keyMap.txt";
        string appFileName = @"C:\dmRemoteDOS\config\appList.txt";

        public static string serverID = "test2";
        public static string dbIP = "";
        public static string dbUser = "";
        public static string dbPass = "";
        public static string application = "";
        public static string block="N";
        
        public List<Hashtable> users=new List<Hashtable>();
        public static MySqlConnection conn;
        string connString = "";
        public int pid = 0;
        public int currentPID = 0;
        public class app {
            public string uid = "0";
            public int pid = 0;
            public int parentid = 0;
            public byte[] screen = new byte[1];
            public int cursorx;
            public int cursory;
            public string printJob;
            public DateTime created;
            public app(string uid, int pid, int parentid,DateTime CREATED,string printJob) {
                this.uid = uid;
                this.printJob = printJob;
                this.pid = pid;
                this.parentid = parentid;
                this.created = CREATED;
            }
        }

        dm.keymapper keymap;
        dm.appList appList;
        List<app> apps = new List<app>();

        public rdServer(string[] args) {
            InitializeComponent();
            loadSettings();
            if (args.Length > 0) serverID= args[0];
            this.serverName.Text = serverID;
                
            reconnectDB();
            keymap  = new dm.keymapper(keyFileName);
            appList = new dm.appList(appFileName);
            reloadOrphanedSessions();
        }

        public void reloadOrphanedSessions() {
            if (null != conn && conn.State != System.Data.ConnectionState.Open) return;
            string query = "SELECT id,uid,pid,printJob FROM _applications WHERE `server`='" + rdServer.serverID + "' order by pid asc,id asc"; ;

            using (MySqlCommand cmdCommand = new MySqlCommand(query, conn)) {
                MySqlDataReader reader = cmdCommand.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        int id = reader.GetInt32(0);
                        string uid = reader.GetString(1);
                        int pid = reader.GetInt32(2);
                        string printJob = reader.GetString(3);
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ProcessID=" + pid);
                        ManagementObjectCollection moc = searcher.Get();
                        foreach (ManagementObject mo in moc) {
                            Int32 parentID = (Int32)((UInt32)mo["ParentProcessID"]);
                            apps.Add(new app(uid, pid, parentID,DateTime.Now,printJob));
                        }//end foreach
                    }//end while read
                }//end has rows
                reader.Close();
            }//end using
        }//end function

        public void reconnectDB() {
            if (conn != null) conn.Close();
            this.connString = "Server=" + dbIP + ";User ID=" + dbUser + ";Password=" + dbPass;
            dm.mysqldb.connString=this.connString;
            dm.mysqldb.connect();
            dm.mysqldb.selectDB("dm-remote-dos");
            conn = new MySqlConnection(connString);
            try {
                conn.Open();
                conn.ChangeDatabase("dm-remote-dos");
            } catch (Exception EX) {

            }
        }

        public void loadSettings() {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("DataModerated\\remoteDOS", false);
            if (rk != null) {
                serverID = (string)rk.GetValue("serverID");

                dbIP = (string)rk.GetValue("databaseIP");
                dbUser = (string)rk.GetValue("databaseUser");
                dbPass = (string)rk.GetValue("databasePass");
                application = (string)rk.GetValue("application");
                rk.Close();
            }
        }
        
        public int getParentID(int pid) {
            foreach (app a in apps) {
                if (pid == a.pid) return a.parentid;
            }
            return -1;
        }

        public void insertMessageForUser(cmdSTR command,string message){

        }

        public static string GetUniqueKey() {
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
        public void launch(cmdSTR command){
            
            dm.appList.appListItem app=appList.getApp(command.code);
            if (app == null) {
                insertMessageForUser(command,"Launch Error");
                return; //cant launch something we don't have
            }
            
            try {
                DateTime currentDate = DateTime.Now;

                string printerID = GetUniqueKey();
                Process myProcess = new Process();
                myProcess.StartInfo.UseShellExecute = false;
                string dir = Path.GetDirectoryName(app.path);
                myProcess.StartInfo.WorkingDirectory = dir + @"\";
                myProcess.StartInfo.FileName = app.path;
                myProcess.StartInfo.CreateNoWindow = true;
                myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                myProcess.StartInfo.Arguments = printerID;
                myProcess.Start();
                int pid = myProcess.Id;

            //if (type == "CAN") 
            {
                bool found = false;
                ManagementObjectSearcher searcher;
                ManagementObjectCollection moc;

                int Initduration = Environment.TickCount;
                while (found == false) {
                    if (Environment.TickCount - Initduration > 5000) found = true; //dont loop for over 5 seconds.
                    searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + myProcess.Id);
                    moc = searcher.Get();
                    pid = myProcess.Id;
                    foreach (ManagementObject mo in moc) {
                        found = true;
                        pid = (Int32)((UInt32)mo["ProcessID"]);
                    }
                    Thread.Sleep(50);
                }

            }
            string insertQuery = String.Format("INSERT INTO _applications (uid,`server`,pid,created,idle,`type`) VALUES ('{0}','{1}','{2}',{3},{4},'{5}')",
                                            command.uid, rdServer.serverID, pid, "now()", "now()", app.title);
            apps.Add(new rdServer.app(command.uid, pid, myProcess.Id,DateTime.Now,printerID));
            using (MySqlCommand insertCMD = new MySqlCommand(insertQuery, conn)) {
                insertCMD.ExecuteNonQuery();
            }
            } catch (Exception ex) {
                return;
            }

        }

        public void updateApplications() {
            if(null != conn && conn.State != System.Data.ConnectionState.Open) return;
            StringBuilder ids = new StringBuilder();

            // REMOVE DATABASE SESSION APPS THAT ARE INVALID
            string query1 = "SELECT id,uid,pid FROM _applications WHERE `server`='" + rdServer.serverID + "' order by pid asc,id asc";
            using (MySqlCommand cmdCommand = new MySqlCommand(query1, conn)) {
                using (MySqlDataReader reader = cmdCommand.ExecuteReader()) {
                    if (reader.HasRows) {
                        while (reader.Read()) {
                            int id = reader.GetInt32(0);
                            string uid = reader.GetString(1);
                            int pid = reader.GetInt32(2);

                            try {
                                Process p = Process.GetProcessById(pid);
                                p.Close();
                            } catch {
                                if (ids.Length > 0) {
                                    ids.Append("," + id.ToString());
                                } else {
                                    ids.Append(id.ToString());
                                }
                            }//end catch
                        }//end while read
                    }//end has rows
                    reader.Close();
                }//end using
            }//end using

            if (ids.Length > 0) {
                string query = String.Format("DELETE FROM _applications WHERE `server`='{0}' AND `id` IN ({1})", rdServer.serverID, ids);
                using (MySqlCommand deleteCMD = new MySqlCommand(query, conn)) {
                    deleteCMD.ExecuteNonQuery();
                }
            }
            // REMOVE LOADED SESSION APPS THAT ARE INVALID
            for (int i = apps.Count - 1; i >= 0; i--) {
                try {
                    Process p = Process.GetProcessById(apps[i].pid);
                    p.Close();
                } catch {
                    string deleteCMDQuery = String.Format("DELETE FROM _applications WHERE `uid`='{0}' AND `server`='{1}' AND `pid`='{2}'", apps[i].uid, rdServer.serverID, apps[i].pid);
                    using (MySqlCommand deleteCMD = new MySqlCommand(deleteCMDQuery, conn)) {
                        deleteCMD.ExecuteNonQuery();
                    }
                    apps.RemoveAt(i);
                    //removed = true;
                }
            }
        }

        public void killIdleApplications() {
            if (null != conn && conn.State != System.Data.ConnectionState.Open) return;

            string query = String.Format("SELECT `pid` FROM _applications WHERE `idle`<= date_sub(now(), INTERVAL 2 HOUR) AND `server`='{0}'", serverID);
            using (MySqlCommand cmdCommand = new MySqlCommand(query, conn)) {
                MySqlDataReader reader = cmdCommand.ExecuteReader();
                if (reader.HasRows) {
                    while (reader.Read()) {
                        int id = reader.GetInt32(0);
                        string uid = reader.GetString(1);
                        int pid = reader.GetInt32(2);
                        Process p = Process.GetProcessById(pid);
                        p.Kill();
                        p.Close();
                    }//end while read
                }// end has rows
                reader.Close();
            }//end using
        }//end function

        private bool ProcessExists(int id) {
            return Process.GetProcesses().Any(x => x.Id == id);
        }

        public void closePID(int pid, int id) {

            if (ProcessExists(pid)) {
                Process p = Process.GetProcessById(pid);

                if (p != null && pid > 0) {
                    p.Kill();
                    p.Close();
                }
            }
            string deleteCMDQuery = "DELETE FROM _commands where `id`='" + id.ToString() + "'";
            using (MySqlCommand deleteCMD = new MySqlCommand(deleteCMDQuery, conn)) {
                deleteCMD.ExecuteNonQuery();
            }
            
        }

        public class cmdSTR {
            public int id = 0;
            public string uid = "";
            public int pid = 0;
            public string code = "";
            public string type = "";
            public int codeValue = 0;
            public bool alt = false;
            public bool ctrl = false;
            public bool shift = false;
            public byte keyState1 = 0;
            public byte keyState2 = 0;

            public cmdSTR(int id, string uid, int pid,string type,string code, string s, string c, string a, string keyState1, string keyState2) {
                this.id = id;
                this.uid = uid;
                this.pid = pid;
                this.code = code;

                this.type = type;
                int.TryParse(code, out this.codeValue); 
                if (s == "s") this.shift = true;
                if (a == "a") this.alt = true;
                if (c == "c") this.ctrl = true;
                byte.TryParse(keyState1, out this.keyState1);
                byte.TryParse(keyState2, out this.keyState2);
            }

        }

        public void update() {
            //return;
            if (null != conn && conn.State != System.Data.ConnectionState.Open) return;

            
           // int oldPID = -1, lastpid = 0;
            List<cmdSTR> commands = new List<cmdSTR>();

            string query1 = "SELECT id,uid,pid,code,shift,alt,ctrl,"+
                            "CASE WHEN coalesce(keyState1,'-1')='-1' THEN '0' ELSE keyState1 END as keyState1,"+
                            "CASE WHEN coalesce(keyState2,'-1')='-1' THEN '0' ELSE keyState2 END as keyState2,type "+
                            "FROM _commands WHERE `server`='"+serverID+"' ORDER BY pid asc,id ASC;";
            using (MySqlCommand cmdCommand = new MySqlCommand(query1, conn)) {
                using (MySqlDataReader reader = cmdCommand.ExecuteReader()) {
                    if (reader.HasRows) {
                        while (reader.Read()) {
                            int id          = reader.GetInt32(0);
                            string uid      = reader.GetString(1);
                            int pid         = reader.GetInt32(2);
                            string code     = reader.GetString(3);
                            string s        = reader.GetString(4);
                            string c        = reader.GetString(6);
                            string a        = reader.GetString(5);
                            string keyState1= reader.GetString(7);
                            string keyState2= reader.GetString(8);
                            string type     = reader.GetString(9);

                            if(block=="Y") {
                                bool skipCommand=true;
                                foreach(Hashtable user in users) {
                                    string testUID= ("//"+user["domain"]+"/"+user["user"]);
                                   if((string)user["whiteListed"]=="Y") {
                                        if(testUID==uid) {
                                           skipCommand=false; break;
                                        }
                                    }
                                }
                                if(skipCommand) continue;
                            }

                            cmdSTR command=new cmdSTR(id, uid, pid, type, code, s, c, a,keyState1,keyState2);


                            if (command.pid == 0) {
                                if (command.code == "nc" || command.code == "np") {
                                    command.type = "application";
                                    command.code = "new|" + command.code;
                                }                                                                   //launch new app (LEGACY Conversion)
                            }

                            if (command.code.Length>4 && command.code.Substring(0, 4) == "new|") command.type = "application";
                            
                            if (command.code == "close") {
                                command.type="close";
                            }

                            
                            string[] tokens=command.code.Split('|');
                            if(tokens.Count()>1) {
                                command.type=tokens[0].Trim().ToLower();
                                command.code=tokens[1].Trim().ToLower();

                            }
                            if(command.code.Trim()=="") continue;                                   //remove blanks...
                            commands.Add(command);

                        }
                    }
                    reader.Close();
                }
            }//end cmd
            foreach (cmdSTR command in commands) {
                removeCommand(command.id);                                              //remove it from the db
                
                switch(command.type) {
                    case "application": break;
                    case "close"      : closePID(command.pid, command.id);  break;          //close app
                    case "new"        : launch(command);                    break;          //launch app
                    case "keystroke"  : sendInput(command);                 break;          //seng keyboard input
                }

                string updateQuery = String.Format("UPDATE _applications SET `idle`=now() WHERE `uid`='{0}' AND `server`='{1}' AND `pid`='{2}'", command.uid, rdServer.serverID, command.pid);
                using (MySqlCommand updateCMD = new MySqlCommand(updateQuery, conn)) {
                    updateCMD.ExecuteNonQuery();
                }


            }//enf rws loop
        }//end func

        public void removeCommand(int id) {
            string deleteQuery = "DELETE FROM _commands where `id`='" + id.ToString() + "'";
            using (MySqlCommand deleteCMD = new MySqlCommand(deleteQuery, conn)) {
                deleteCMD.ExecuteNonQuery();
            }
        }

        public void updateScreensINDB() {
            foreach (app a in apps) {
                if (DateTime.Now.Subtract(a.created) < TimeSpan.FromSeconds(5)) continue;
                ReadWriteMemory.ProcessMemory m = new ReadWriteMemory.ProcessMemory();
                bool status = m.StartProcess(a.pid);
                if (!status) {
                    return;
                }
                int offset = 0xb8000;
                byte[] video = m.ReadMem(offset, 80 * 25 * 2);

                byte[] cursor = m.ReadMem(1104, 2);
                int cursorX = cursor[0];
                int cursorY = cursor[1];
                m.closeProcess();
               // char[] ss = new char[8000];
               //StringBuilder ss=new StringBuilder();
               //  bool diff = false;
               //  if (a.screen.Length == video.Length) {
               //      for (int b = 0; b < 4000; b++) if (video[b] != a.screen[b]) diff = true;
               // }
                /*
                byte bb;
                int i = 0;
                for (int b = 0; b < 4000; b++) {
                    bb = ((byte)(video[b] >> 4));
                    ss[i++] = (char)(bb > 9 ? bb - 10 + 'A' : bb + '0');
                    bb = ((byte)(video[b] & 0x0F));
                    ss[i++] = (char)(bb > 9 ? bb - 10 + 'A' : bb + '0');
                }
                a.screen = video;
                string pulled = "";

                if (diff) pulled = "`pulled`='N',";
                string insrtQuery = "INSERT INTO _sessions (`pid`,`server`,`screen`,`time`,`cursorx`,`cursory`) VALUES ('" + a.pid + "','" + serverID + "','" + new String(ss) + "',now() ,'" + cursorX.ToString() + "','" + cursorY.ToString() + "' )"
                + "ON DUPLICATE KEY UPDATE `screen`=values(`screen`), `time`=now()," + pulled + "`cursorx`='" + cursorX.ToString() + "',`cursory`='" + cursorY.ToString() + "'";
                using (MySqlCommand insrtCMD = new MySqlCommand(insrtQuery, conn)) {
                    insrtCMD.ExecuteNonQuery();
                }
                 * */
                using (MySqlCommand command = new MySqlCommand()) {
                    command.Connection = conn;
                    command.CommandText =
                        "INSERT INTO _sessions_b (`pid`,`server`,`screen`,`time`,`cursorx`,`cursory`) VALUES (@pid,@server,@screen,now(),@cursorX,@cursorY) "+
                        "ON DUPLICATE KEY UPDATE `screen`=@scr2,`time`=now(),`cursorx`=@cursorX,`cursory`=@cursory";
                        
                    MySqlParameter serverParam= new MySqlParameter("@server", MySqlDbType.VarChar, 256);
                    MySqlParameter pidParam = new MySqlParameter("@pid", MySqlDbType.Int32, 11);
                    MySqlParameter cursorXParam = new MySqlParameter("@cursorX", MySqlDbType.Int16, 11);
                    MySqlParameter cursorYParam = new MySqlParameter("@cursorY", MySqlDbType.Int16, 11);
                    MySqlParameter screenParam = new MySqlParameter("@screen", MySqlDbType.VarBinary, video.Length);
                    MySqlParameter screenParam2 = new MySqlParameter("@scr2", MySqlDbType.VarBinary, video.Length);
                    screenParam.Value = video;
                    screenParam2.Value = video;
                    serverParam.Value = serverID;
                    pidParam.Value = a.pid;
                    cursorXParam.Value = cursorX;
                    cursorYParam.Value = cursorY;
                    command.Parameters.Add(screenParam);
                    command.Parameters.Add(screenParam2);
                    command.Parameters.Add(cursorXParam);
                    command.Parameters.Add(cursorYParam);
                    command.Parameters.Add(pidParam);
                    command.Parameters.Add(serverParam);
                    
                    string s = command.CommandText;
                    command.ExecuteNonQuery();
                }

            }
        }

        public string hexDump(byte[] buffer, string name, int offset, bool dumpToConsole) {
            int lineWidth = 16;
            int lw = 0;
            int a;
            if (buffer.Length == 0) return "";							//if nothing return nothing...
            string border = " | ", end = "\r\n";
            StringBuilder ascii = new StringBuilder();
            StringBuilder output = new StringBuilder();

            output.Append(String.Format("Buffer:{0}       Size: {1,10:G} \r\n", name, buffer.Length));
            output.Append("HEX DUMP ");
            for (a = 0; a < lineWidth; a++) {
                output.Append(String.Format("{0:X2} ", a));
                ascii.Append(String.Format("{0:X1}", a % 0x10));
            }


            output.Append(border + ascii + end);

            ascii = new StringBuilder();
            int index = 0;
            foreach (byte b in buffer) {
                if (lw == 0) {
                    output.Append(String.Format("{0:X8} ", offset + index));
                }
                index++;
                output.Append(String.Format("{0:X2} ", b));

                if (b < 0x10 || b == 0x7f) {
                    ascii.Append(" ");
                } else {
                    ascii.Append((char)b);
                }

                lw++;
                if (lw == lineWidth) {		            //display as ascii now...
                    lw = 0;
                    output.Append(border + ascii + end);
                    ascii = new StringBuilder();
                }

            }

            if (lw != 0) {
                for (a = lw; a < lineWidth; a++) {
                    output.Append("   ");
                }
                output.Append(border + ascii + end);
            }
            if (dumpToConsole) Console.Write(output.ToString());
            return output.ToString();
        }

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_SHOWWINDOW = 0x0040;

        public static void ActivateProcess(int PID) {
            if (PID < 0) return;
            try {
                Process proc = Process.GetProcessById(PID);
                IntPtr mainWindow = proc.MainWindowHandle;

                IntPtr newPos = new IntPtr(0);  // 0 puts it on top of Z order.   You can do new IntPtr(-1) to force it to a topmost window, instead.
                SetWindowPos(mainWindow, new IntPtr(0), 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE | SWP_SHOWWINDOW);
            } catch (Exception e) {

            }
        }
        
        public void sendInput(cmdSTR command) {
            if (command.codeValue > 0XFF || command.codeValue == 0x10 || command.codeValue == 0x11) return;
            //ActivateProcess(pid);
            ReadWriteMemory.ProcessMemory m = new ReadWriteMemory.ProcessMemory();
            bool status = m.StartProcess(command.pid);
            if (!status) {
                m.closeProcess();
                return;
            }

            byte[] codes = keymap.getCodes(command.codeValue, command.shift, command.alt, command.ctrl,command.keyState1);
            byte[] bytes = new byte[] {  command.keyState1,command.keyState2,0x00,0x1E, 0x00, 0x20, 0x00, codes[0], codes[1] };
            m.WriteMem(0x417, bytes);
           /*17-kb flag1
            *18-kb flag2
            *19 ?
            *20 buffer address begin relative to 0x400
            *21 empty
            *22 buffer address end relative to 0x400
            *21 begin of kb buffer-key code-1
            *22 kb biffer key code-2
            */

            m.closeProcess();
        }

        private void timer1_Tick(object sender, EventArgs e) {
            if (conn != null && conn.State == System.Data.ConnectionState.Open) {
                updateApplications();
                update();
              //  updateMouseEvents();
                updateScreensINDB();
            }
        }

        public void updateMouseEvents(){
            string query1 = "SELECT id,uid,pid,x,y,`left`,`middle`,`right` " +
                  "FROM _mouse WHERE `server`='" + serverID + "' ORDER BY pid asc,id ASC";
            StringBuilder ids = new StringBuilder();
            using (MySqlCommand cmdCommand = new MySqlCommand(query1, conn)) {
                using (MySqlDataReader reader = cmdCommand.ExecuteReader()) {
                    if (reader.HasRows) {
                        while (reader.Read()) {
                            int id      = reader.GetInt32(0);
                            string uid  = reader.GetString(1);
                            int    pid  = reader.GetInt32(2);
                            ushort x    = reader.GetUInt16(3);
                            ushort y    = reader.GetUInt16(4);
                            string left = reader.GetString(5);
                            string right= reader.GetString(6);
                            string middle=reader.GetString(7);

                            if (ids.Length > 0) {
                                ids.Append("," + id.ToString());
                            } else {
                                ids.Append(id.ToString());
                            }
                            ReadWriteMemory.ProcessMemory m = new ReadWriteMemory.ProcessMemory();
                            bool status = m.StartProcess(pid);
                            if (!status) {
                                m.closeProcess();
                                continue;
                            }
                            byte button=0;
                            if(left  =="1") button=1;
                            if(middle=="1") button=3;
                            if(right =="1") button=2;
                            byte mxh= (byte) (x >> 8);
                            byte mxl= (byte) (x & 0xff);
                            byte myh= (byte) (y >> 8);
                            byte myl= (byte) (y & 0xff);
                          
                            byte[] bytes2 = new byte[] { 0x10,00,0xff,0xfe };
                            m.WriteMem(0x4950, bytes2);
                          
                            byte[] bytes = new byte[] { button,0x00,mxl,mxh,myl,myh};
                            m.WriteMem(0x496A, bytes);
                            m.closeProcess();                                                    
                            
                        }
                    }
                    reader.Close();
                }
                 string deleteCMDQuery = String.Format("DELETE FROM _mouse WHERE `id` in ('{0}')",ids.ToString());
                 using (MySqlCommand deleteCMD = new MySqlCommand(deleteCMDQuery, conn)) {
                     deleteCMD.ExecuteNonQuery();
                 }
           
            }//end cmd

        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            configure c = new configure();
            c.ShowDialog();
            loadSettings();
            reconnectDB();
        }

        private void deleteTimer_Tick(object sender, EventArgs e) {
            if (conn == null) return;
            string query = "DELETE FROM _sessions where time< now() - INTERVAL 5 MINUTE";
            MySqlCommand command;
            command = new MySqlCommand(query, conn);
            command.ExecuteNonQuery();
        }

        private void screenTimer_Tick(object sender, EventArgs e) {
            sesCount.Text = apps.Count.ToString();
            int number = Process.GetCurrentProcess().Threads.Count;
            thrd.Text = number.ToString();

        }

        private void button1_Click(object sender, EventArgs e) {
            try {
                if (currentPID != 0) {
                    Process p = Process.GetProcessById(currentPID);
                    p.Kill();
                }
            } catch {
            }
        }

        private void restartTimer_Tick(object sender, EventArgs e) {
            Application.Restart();
        }

        private void killIdleAppsTimer_Tick(object sender, EventArgs e) {
            killIdleApplications();
            
        }

        private void checkBlockingTimer_Tick(object sender, EventArgs e) {
            string query="SELECT block from _firewall LIMIT 1";
            Hashtable res=dm.mysqldb.fetch(query);
            block=(string)res["block"];
            if(block=="Y") blockingLabel.Text="Blocking UnWhitelisted Connections";
            else blockingLabel.Text="Not Blocking";
            string query2="SELECT domain,user,whiteListed from _users where lastlogon>=curdate()";
            users=dm.mysqldb.fetchAll(query2);
        }
        
            
        private void restart_Click(object sender, EventArgs e) {
            Application.Restart();
        }

        private void gcCollect_Click(object sender, EventArgs e) {
            GC.Collect();
        }

        private void gcTimer_Tick(object sender, EventArgs e) {
            GC.Collect();
        }

        private void dbConnect_Click(object sender, EventArgs e) {
            reconnectDB();
        }

        private void properties_CheckedChanged(object sender, EventArgs e) {

        }
        
    }
}
