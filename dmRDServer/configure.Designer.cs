namespace DM_RD_SERVER {
    partial class configure {
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
            this.serverID = new System.Windows.Forms.TextBox();
            this.dbIP = new System.Windows.Forms.TextBox();
            this.dbUser = new System.Windows.Forms.TextBox();
            this.dbPass = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cancel = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.app = new System.Windows.Forms.TextBox();
            this.fileBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverID
            // 
            this.serverID.Location = new System.Drawing.Point(123, 13);
            this.serverID.Name = "serverID";
            this.serverID.Size = new System.Drawing.Size(184, 20);
            this.serverID.TabIndex = 7;
            // 
            // dbIP
            // 
            this.dbIP.Location = new System.Drawing.Point(123, 39);
            this.dbIP.Name = "dbIP";
            this.dbIP.Size = new System.Drawing.Size(184, 20);
            this.dbIP.TabIndex = 8;
            // 
            // dbUser
            // 
            this.dbUser.Location = new System.Drawing.Point(123, 65);
            this.dbUser.Name = "dbUser";
            this.dbUser.Size = new System.Drawing.Size(184, 20);
            this.dbUser.TabIndex = 9;
            // 
            // dbPass
            // 
            this.dbPass.Location = new System.Drawing.Point(123, 91);
            this.dbPass.Name = "dbPass";
            this.dbPass.Size = new System.Drawing.Size(184, 20);
            this.dbPass.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "ServerID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Database IP";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Database User";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(102, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "Database Password";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 6;
            this.label10.Text = "Application";
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(151, 159);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(75, 23);
            this.cancel.TabIndex = 0;
            this.cancel.Text = "cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(232, 159);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 1;
            this.save.Text = "save";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // app
            // 
            this.app.Location = new System.Drawing.Point(123, 118);
            this.app.Name = "app";
            this.app.Size = new System.Drawing.Size(184, 20);
            this.app.TabIndex = 11;
            // 
            // fileBrowse
            // 
            this.fileBrowse.Location = new System.Drawing.Point(313, 114);
            this.fileBrowse.Name = "fileBrowse";
            this.fileBrowse.Size = new System.Drawing.Size(75, 23);
            this.fileBrowse.TabIndex = 12;
            this.fileBrowse.Text = "browse";
            this.fileBrowse.UseVisualStyleBackColor = true;
            this.fileBrowse.Click += new System.EventHandler(this.fileBrowse_Click);
            // 
            // configure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 194);
            this.Controls.Add(this.fileBrowse);
            this.Controls.Add(this.app);
            this.Controls.Add(this.dbPass);
            this.Controls.Add(this.dbUser);
            this.Controls.Add(this.dbIP);
            this.Controls.Add(this.serverID);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.save);
            this.Controls.Add(this.cancel);
            this.Name = "configure";
            this.Text = "Configure";
            this.Load += new System.EventHandler(this.configure_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverID;
        private System.Windows.Forms.TextBox dbIP;
        private System.Windows.Forms.TextBox dbUser;
        private System.Windows.Forms.TextBox dbPass;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox app;
        private System.Windows.Forms.Button fileBrowse;

    }
}