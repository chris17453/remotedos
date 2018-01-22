namespace DM_ADV_Drone {
    partial class launch {
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.userID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dir = new System.Windows.Forms.Label();
            this.file = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "user ID";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(179, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 95);
            this.button1.TabIndex = 1;
            this.button1.Text = "Launch Application as user";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // userID
            // 
            this.userID.Location = new System.Drawing.Point(25, 87);
            this.userID.Name = "userID";
            this.userID.Size = new System.Drawing.Size(100, 20);
            this.userID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Launching";
            // 
            // dir
            // 
            this.dir.AutoSize = true;
            this.dir.Location = new System.Drawing.Point(13, 29);
            this.dir.Name = "dir";
            this.dir.Size = new System.Drawing.Size(35, 13);
            this.dir.TabIndex = 4;
            this.dir.Text = "label3";
            // 
            // file
            // 
            this.file.AutoSize = true;
            this.file.Location = new System.Drawing.Point(13, 46);
            this.file.Name = "file";
            this.file.Size = new System.Drawing.Size(35, 13);
            this.file.TabIndex = 5;
            this.file.Text = "label4";
            // 
            // launch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 119);
            this.Controls.Add(this.file);
            this.Controls.Add(this.dir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.userID);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "launch";
            this.Text = "launch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox userID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label dir;
        private System.Windows.Forms.Label file;
    }
}