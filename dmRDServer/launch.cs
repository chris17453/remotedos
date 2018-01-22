using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace DM_ADV_Drone {
    public partial class launch : Form {
        public launch() {
            InitializeComponent();
            dir.Text = Path.GetDirectoryName(DM_ADV_Drone.advdrone.application);
            file.Text=Path.GetFileName(DM_ADV_Drone.advdrone.application);
                
        }

        private void button1_Click(object sender, EventArgs e) {
            if(String.IsNullOrEmpty(userID.Text)) {
                MessageBox.Show("Gotta put in an integer id for the user. Else I cant launch.");
                return;
            }

            Process myProcess = new Process();

            try
            {
                myProcess.StartInfo.UseShellExecute = false;
                string dir = Path.GetDirectoryName(DM_ADV_Drone.advdrone.application);
                myProcess.StartInfo.WorkingDirectory = dir+@"\";
                myProcess.StartInfo.FileName = DM_ADV_Drone.advdrone.application;
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();


                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + myProcess.Id);
                ManagementObjectCollection moc = searcher.Get();
                int pid = 0;
                foreach (ManagementObject mo in moc) {
                    pid = (Int32)((UInt32)mo["ProcessID"]);
                    DM_ADV_Drone.advdrone.apps.Add(new DM_ADV_Drone.advdrone.app(userID.Text, pid, myProcess.Id)); 
                }


         
                string query = 
                    String.Format("INSERT INTO _applications (uid,`server`,pid,created,idle) VALUES ('{0}','{1}','{2}',{3},{4})",
                    userID.Text,DM_ADV_Drone.advdrone.serverID,pid,"now()","now()");
                dm.mysqldb.query(query);
                
            }
            catch (Exception excp)
            {
                MessageBox.Show(excp.Message);
            }
            this.Close();
        }
    }
}
