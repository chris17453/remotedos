namespace dmPrint
{
    partial class RemotePrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.components = new System.ComponentModel.Container();
            this.emailB = new System.Windows.Forms.Button();
            this.saveB = new System.Windows.Forms.Button();
            this.printB = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.WebBrowser();
            this.closeTimer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // emailB
            // 
            this.emailB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.emailB.Location = new System.Drawing.Point(437, 6);
            this.emailB.Name = "emailB";
            this.emailB.Size = new System.Drawing.Size(75, 23);
            this.emailB.TabIndex = 7;
            this.emailB.Text = "Email";
            this.emailB.UseVisualStyleBackColor = true;
            this.emailB.Click += new System.EventHandler(this.emailB_Click);
            // 
            // saveB
            // 
            this.saveB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveB.Location = new System.Drawing.Point(356, 6);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(75, 23);
            this.saveB.TabIndex = 6;
            this.saveB.Text = "Save";
            this.saveB.UseVisualStyleBackColor = true;
            this.saveB.Click += new System.EventHandler(this.saveB_Click);
            // 
            // printB
            // 
            this.printB.Location = new System.Drawing.Point(12, 6);
            this.printB.Name = "printB";
            this.printB.Size = new System.Drawing.Size(75, 23);
            this.printB.TabIndex = 5;
            this.printB.Text = "Print";
            this.printB.UseVisualStyleBackColor = true;
            this.printB.Click += new System.EventHandler(this.printB_Click);
            // 
            // preview
            // 
            this.preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.preview.Location = new System.Drawing.Point(12, 35);
            this.preview.MinimumSize = new System.Drawing.Size(20, 20);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(500, 386);
            this.preview.TabIndex = 4;
            this.preview.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // closeTimer
            // 
            this.closeTimer.Interval = 5000;
            // 
            // RemotePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 433);
            this.Controls.Add(this.emailB);
            this.Controls.Add(this.saveB);
            this.Controls.Add(this.printB);
            this.Controls.Add(this.preview);
            this.Name = "RemotePrint";
            this.Text = "Remote Print";
            this.Load += new System.EventHandler(this.RemotePrint_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button emailB;
        private System.Windows.Forms.Button saveB;
        private System.Windows.Forms.Button printB;
        private System.Windows.Forms.WebBrowser preview;
        private System.Windows.Forms.Timer closeTimer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

