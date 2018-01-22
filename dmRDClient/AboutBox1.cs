using System;
using System.Data;

using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;


using System.Collections;
/*using System.Drawing;
using System.Text;
using System.Net;
using System.IO;

using System.Globalization;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
*/
namespace dmRDClient_PC
{
    partial class AboutBox1 : Form
    {
        public AboutBox1()
        {
            InitializeComponent();

            Hashtable row= dm.mysqldb.fetch("SELECT * from _elua order by id desc LIMIT 1");
            if (null!=row["elua"]) this.eula.Text = (string)row["elua"];
            
            if (null != row["clientInstances"]) this.totalClientInstances.Text = (string)row["clientInstances"];
            if (null != row["licence"]) this.licenceNo.Text = (string)row["licence"];
            if (null != row["clusterInstances"]) this.serverInstances.Text = (string)row["clusterInstances"];
            if (null != row["perServerLimit"]) this.clientPerServer.Text = (string)row["perServerLimit"];
            if (null != row["licenceType"]) this.licenceType.Text = (string)row["licenceType"];
            if (null != row["licenceBegin"])
            {
                string[] begin;
                begin = ((string)row["licenceBegin"]).Split(' ');
                this.licencePeriod.Text = begin[0];

            }
            if (null != row["licenceEnd"])
            {
                string[] end;
                end = ((string)row["licenceEnd"]).Split(' ');
                this.licencePeriod.Text += " - " + end[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eula_TextChanged(object sender, EventArgs e)
        {

        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {
            this.eula.DeselectAll();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void licenceType_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void clientPerServer_Click(object sender, EventArgs e)
        {

        }

        private void totalClientInstances_Click(object sender, EventArgs e)
        {

        }

        private void serverInstances_Click(object sender, EventArgs e)
        {

        }

        private void licenceNo_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void licencePeriod_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
