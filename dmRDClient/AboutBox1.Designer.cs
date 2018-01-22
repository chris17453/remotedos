namespace dmRDClient_PC
{
    partial class AboutBox1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox1));
            this.logoPictureBox = new System.Windows.Forms.PictureBox();
            this.eula = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.licenceNo = new System.Windows.Forms.Label();
            this.serverInstances = new System.Windows.Forms.Label();
            this.totalClientInstances = new System.Windows.Forms.Label();
            this.clientPerServer = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.licenceType = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.licencePeriod = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // logoPictureBox
            // 
            this.logoPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("logoPictureBox.Image")));
            this.logoPictureBox.Location = new System.Drawing.Point(-2, -2);
            this.logoPictureBox.Name = "logoPictureBox";
            this.logoPictureBox.Size = new System.Drawing.Size(176, 438);
            this.logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPictureBox.TabIndex = 13;
            this.logoPictureBox.TabStop = false;
            // 
            // eula
            // 
            this.eula.HideSelection = false;
            this.eula.Location = new System.Drawing.Point(196, 210);
            this.eula.Multiline = true;
            this.eula.Name = "eula";
            this.eula.ReadOnly = true;
            this.eula.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eula.ShortcutsEnabled = false;
            this.eula.Size = new System.Drawing.Size(385, 190);
            this.eula.TabIndex = 14;
            this.eula.TabStop = false;
            this.eula.TextChanged += new System.EventHandler(this.eula_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(506, 406);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(195, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Licence No:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(195, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Server Instances:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(195, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Total Client Instances:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(195, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Client Per Server Instances:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "RemoteDOS";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Copyright (c) 2015 DataModerated";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // licenceNo
            // 
            this.licenceNo.AutoSize = true;
            this.licenceNo.Location = new System.Drawing.Point(338, 77);
            this.licenceNo.Name = "licenceNo";
            this.licenceNo.Size = new System.Drawing.Size(35, 13);
            this.licenceNo.TabIndex = 23;
            this.licenceNo.Text = "label8";
            this.licenceNo.Click += new System.EventHandler(this.licenceNo_Click);
            // 
            // serverInstances
            // 
            this.serverInstances.AutoSize = true;
            this.serverInstances.Location = new System.Drawing.Point(338, 133);
            this.serverInstances.Name = "serverInstances";
            this.serverInstances.Size = new System.Drawing.Size(35, 13);
            this.serverInstances.TabIndex = 24;
            this.serverInstances.Text = "label8";
            this.serverInstances.Click += new System.EventHandler(this.serverInstances_Click);
            // 
            // totalClientInstances
            // 
            this.totalClientInstances.AutoSize = true;
            this.totalClientInstances.Location = new System.Drawing.Point(338, 153);
            this.totalClientInstances.Name = "totalClientInstances";
            this.totalClientInstances.Size = new System.Drawing.Size(35, 13);
            this.totalClientInstances.TabIndex = 25;
            this.totalClientInstances.Text = "label8";
            this.totalClientInstances.Click += new System.EventHandler(this.totalClientInstances_Click);
            // 
            // clientPerServer
            // 
            this.clientPerServer.AutoSize = true;
            this.clientPerServer.Location = new System.Drawing.Point(338, 174);
            this.clientPerServer.Name = "clientPerServer";
            this.clientPerServer.Size = new System.Drawing.Size(35, 13);
            this.clientPerServer.TabIndex = 26;
            this.clientPerServer.Text = "label8";
            this.clientPerServer.Click += new System.EventHandler(this.clientPerServer_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(195, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 27;
            this.label8.Text = "Licence Type:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // licenceType
            // 
            this.licenceType.AutoSize = true;
            this.licenceType.Location = new System.Drawing.Point(338, 97);
            this.licenceType.Name = "licenceType";
            this.licenceType.Size = new System.Drawing.Size(35, 13);
            this.licenceType.TabIndex = 28;
            this.licenceType.Text = "label9";
            this.licenceType.Click += new System.EventHandler(this.licenceType_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(195, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Licence Period:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // licencePeriod
            // 
            this.licencePeriod.AutoSize = true;
            this.licencePeriod.Location = new System.Drawing.Point(338, 114);
            this.licencePeriod.Name = "licencePeriod";
            this.licencePeriod.Size = new System.Drawing.Size(41, 13);
            this.licencePeriod.TabIndex = 30;
            this.licencePeriod.Text = "label10";
            this.licencePeriod.Click += new System.EventHandler(this.licencePeriod_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "Version 2.1a";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(195, 194);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "ELUA";
            // 
            // AboutBox1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 434);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.licencePeriod);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.licenceType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.clientPerServer);
            this.Controls.Add(this.totalClientInstances);
            this.Controls.Add(this.serverInstances);
            this.Controls.Add(this.licenceNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.eula);
            this.Controls.Add(this.logoPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox1";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About RemoteDOS";
            this.Load += new System.EventHandler(this.AboutBox1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.TextBox eula;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label licenceNo;
        private System.Windows.Forms.Label serverInstances;
        private System.Windows.Forms.Label totalClientInstances;
        private System.Windows.Forms.Label clientPerServer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label licenceType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label licencePeriod;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;


    }
}
