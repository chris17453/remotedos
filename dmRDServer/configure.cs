using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DM_RD_SERVER{
    public partial class configure : Form {
        public configure() {
            InitializeComponent();
        }

        private void configure_Load(object sender, EventArgs e) {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("DataModerated\\remoteDOS",false);
            if(rk!=null){
                serverID.Text = (string)rk.GetValue("serverID");
                dbIP.Text     = (string)rk.GetValue("databaseIP");
                dbUser.Text   = (string)rk.GetValue("databaseUser");
                dbPass.Text   = (string)rk.GetValue("databasePass");
                app.Text      = (string)rk.GetValue("application");
                rk.Close();
            }
        }

        private void cancel_Click(object sender, EventArgs e) {
            this.Close();
        }
      
        private void save_Click(object sender, EventArgs e) {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("DataModerated\\remoteDOS",true);
            if (rk == null) {
                rk = Registry.CurrentUser.CreateSubKey("DataModerated\\remoteDOS");
            }
            rk.SetValue("serverID"      ,serverID.Text);
            rk.SetValue("databaseIP"          ,dbIP.Text);
            rk.SetValue("databaseUser"  ,dbUser.Text);
            rk.SetValue("databasePass"  ,dbPass.Text);
            rk.SetValue("application"   ,app.Text);
            rk.Close();
            this.Close();

        }

        private void fileBrowse_Click(object sender, EventArgs e) {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                app.Text = openFileDialog1.FileName;
            }
        }


       
    }
}
