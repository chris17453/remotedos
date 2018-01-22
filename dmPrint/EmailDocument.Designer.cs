namespace dmPrint
{
    partial class EmailDocument
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
            this.label4 = new System.Windows.Forms.Label();
            this.from = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.body = new System.Windows.Forms.TextBox();
            this.subject = new System.Windows.Forms.TextBox();
            this.to = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Reply To";
            // 
            // from
            // 
            this.from.Location = new System.Drawing.Point(86, 19);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(334, 20);
            this.from.TabIndex = 16;
            this.from.Text = "outgoing@performanceradiator.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Body";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Subject";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "To";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(468, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.TabStop = false;
            this.button1.Text = "Send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // body
            // 
            this.body.Location = new System.Drawing.Point(86, 97);
            this.body.Multiline = true;
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(474, 239);
            this.body.TabIndex = 11;
            this.body.Text = "You have recieved a document from Performance Radiator.";
            // 
            // subject
            // 
            this.subject.Location = new System.Drawing.Point(86, 71);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(334, 20);
            this.subject.TabIndex = 10;
            this.subject.Text = "Performance Radiator Document. ";
            // 
            // to
            // 
            this.to.Location = new System.Drawing.Point(86, 45);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(334, 20);
            this.to.TabIndex = 9;
            // 
            // EmailDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 354);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.from);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.body);
            this.Controls.Add(this.subject);
            this.Controls.Add(this.to);
            this.Name = "EmailDocument";
            this.Text = "Email";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox from;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox body;
        private System.Windows.Forms.TextBox subject;
        private System.Windows.Forms.TextBox to;

    }
}