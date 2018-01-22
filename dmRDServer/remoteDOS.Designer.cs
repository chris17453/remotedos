namespace DM_RD_SERVER {
    partial class rdServer {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rdServer));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.deleteTimer = new System.Windows.Forms.Timer(this.components);
            this.screenTimer = new System.Windows.Forms.Timer(this.components);
            this.restartTimer = new System.Windows.Forms.Timer(this.components);
            this.killIdleAppsTimer = new System.Windows.Forms.Timer(this.components);
            this.gcCollect = new System.Windows.Forms.Button();
            this.restart = new System.Windows.Forms.Button();
            this.gcTimer = new System.Windows.Forms.Timer(this.components);
            this.dbConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.thrd = new System.Windows.Forms.Label();
            this.sesCount = new System.Windows.Forms.Label();
            this.disLaunch = new System.Windows.Forms.CheckBox();
            this.serverName = new System.Windows.Forms.Label();
            this.blockingLabel = new System.Windows.Forms.Label();
            this.firewallT = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Sessions";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(303, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(67, 22);
            this.toolStripButton2.Text = "Configure";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // deleteTimer
            // 
            this.deleteTimer.Interval = 5000;
            this.deleteTimer.Tick += new System.EventHandler(this.deleteTimer_Tick);
            // 
            // screenTimer
            // 
            this.screenTimer.Enabled = true;
            this.screenTimer.Interval = 1000;
            this.screenTimer.Tick += new System.EventHandler(this.screenTimer_Tick);
            // 
            // restartTimer
            // 
            this.restartTimer.Enabled = true;
            this.restartTimer.Interval = 43200000;
            this.restartTimer.Tick += new System.EventHandler(this.restartTimer_Tick);
            // 
            // killIdleAppsTimer
            // 
            this.killIdleAppsTimer.Interval = 60000;
            this.killIdleAppsTimer.Tick += new System.EventHandler(this.killIdleAppsTimer_Tick);
            // 
            // gcCollect
            // 
            this.gcCollect.Location = new System.Drawing.Point(30, 76);
            this.gcCollect.Name = "gcCollect";
            this.gcCollect.Size = new System.Drawing.Size(75, 23);
            this.gcCollect.TabIndex = 12;
            this.gcCollect.Text = "GC Collect";
            this.gcCollect.UseVisualStyleBackColor = true;
            this.gcCollect.Click += new System.EventHandler(this.gcCollect_Click);
            // 
            // restart
            // 
            this.restart.Location = new System.Drawing.Point(30, 47);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(75, 23);
            this.restart.TabIndex = 13;
            this.restart.Text = "Restart";
            this.restart.UseVisualStyleBackColor = true;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // gcTimer
            // 
            this.gcTimer.Enabled = true;
            this.gcTimer.Interval = 10000;
            this.gcTimer.Tick += new System.EventHandler(this.gcTimer_Tick);
            // 
            // dbConnect
            // 
            this.dbConnect.Location = new System.Drawing.Point(30, 105);
            this.dbConnect.Name = "dbConnect";
            this.dbConnect.Size = new System.Drawing.Size(75, 23);
            this.dbConnect.TabIndex = 14;
            this.dbConnect.Text = "Re- DB";
            this.dbConnect.UseVisualStyleBackColor = true;
            this.dbConnect.Click += new System.EventHandler(this.dbConnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Threads";
            // 
            // thrd
            // 
            this.thrd.AutoSize = true;
            this.thrd.Location = new System.Drawing.Point(185, 75);
            this.thrd.Name = "thrd";
            this.thrd.Size = new System.Drawing.Size(13, 13);
            this.thrd.TabIndex = 17;
            this.thrd.Text = "0";
            // 
            // sesCount
            // 
            this.sesCount.AutoSize = true;
            this.sesCount.Location = new System.Drawing.Point(188, 47);
            this.sesCount.Name = "sesCount";
            this.sesCount.Size = new System.Drawing.Size(13, 13);
            this.sesCount.TabIndex = 18;
            this.sesCount.Text = "0";
            // 
            // disLaunch
            // 
            this.disLaunch.AutoSize = true;
            this.disLaunch.Location = new System.Drawing.Point(136, 105);
            this.disLaunch.Name = "disLaunch";
            this.disLaunch.Size = new System.Drawing.Size(100, 17);
            this.disLaunch.TabIndex = 19;
            this.disLaunch.Text = "Disable Launch";
            this.disLaunch.UseVisualStyleBackColor = true;
            this.disLaunch.CheckedChanged += new System.EventHandler(this.properties_CheckedChanged);
            // 
            // serverName
            // 
            this.serverName.AutoSize = true;
            this.serverName.Location = new System.Drawing.Point(201, 6);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(38, 13);
            this.serverName.TabIndex = 20;
            this.serverName.Text = "Server";
            // 
            // blockingLabel
            // 
            this.blockingLabel.AutoSize = true;
            this.blockingLabel.Location = new System.Drawing.Point(136, 139);
            this.blockingLabel.Name = "blockingLabel";
            this.blockingLabel.Size = new System.Drawing.Size(68, 13);
            this.blockingLabel.TabIndex = 21;
            this.blockingLabel.Text = "Not Blocking";
            // 
            // firewallT
            // 
            this.firewallT.Enabled = true;
            this.firewallT.Interval = 10000;
            this.firewallT.Tick += new System.EventHandler(this.checkBlockingTimer_Tick);
            // 
            // rdServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 164);
            this.Controls.Add(this.blockingLabel);
            this.Controls.Add(this.serverName);
            this.Controls.Add(this.disLaunch);
            this.Controls.Add(this.sesCount);
            this.Controls.Add(this.thrd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dbConnect);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.gcCollect);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.Name = "rdServer";
            this.Text = "DataModerated : Remote Dos";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Timer deleteTimer;
        private System.Windows.Forms.Timer screenTimer;
        private System.Windows.Forms.Timer restartTimer;
        private System.Windows.Forms.Timer killIdleAppsTimer;
        private System.Windows.Forms.Button gcCollect;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.Timer gcTimer;
        private System.Windows.Forms.Button dbConnect;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label thrd;
        private System.Windows.Forms.Label sesCount;
        private System.Windows.Forms.CheckBox disLaunch;
        private System.Windows.Forms.Label serverName;
        private System.Windows.Forms.Label blockingLabel;
        private System.Windows.Forms.Timer firewallT;
    }
}

