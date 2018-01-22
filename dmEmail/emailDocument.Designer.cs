namespace dmEmail {
    partial class emailDocument {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
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
            this.label4 = new System.Windows.Forms.Label();
            this.from = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.body = new System.Windows.Forms.TextBox();
            this.subject = new System.Windows.Forms.TextBox();
            this.outbound = new System.Windows.Forms.TextBox();
            this.sendB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Reply To";
            // 
            // from
            // 
            this.from.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.from.Location = new System.Drawing.Point(85, 18);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(334, 20);
            this.from.TabIndex = 25;
            this.from.Text = "outgoing@performanceradiator.com";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Body";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Subject";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "To";
            // 
            // body
            // 
            this.body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.body.Location = new System.Drawing.Point(85, 99);
            this.body.Multiline = true;
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(478, 251);
            this.body.TabIndex = 3;
            this.body.Text = "You have recieved a document from Performance Radiator.";
            // 
            // subject
            // 
            this.subject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.subject.Location = new System.Drawing.Point(85, 70);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(334, 20);
            this.subject.TabIndex = 2;
            this.subject.Text = "Performance Radiator Document. ";
            // 
            // outbound
            // 
            this.outbound.Location = new System.Drawing.Point(85, 45);
            this.outbound.Name = "outbound";
            this.outbound.Size = new System.Drawing.Size(334, 20);
            this.outbound.TabIndex = 1;
            this.outbound.Text = "l";
            this.outbound.TextChanged += new System.EventHandler(this.outbound_TextChanged);
            // 
            // sendB
            // 
            this.sendB.Location = new System.Drawing.Point(467, 33);
            this.sendB.Name = "sendB";
            this.sendB.Size = new System.Drawing.Size(75, 23);
            this.sendB.TabIndex = 27;
            this.sendB.Text = "Send";
            this.sendB.UseVisualStyleBackColor = true;
            this.sendB.Click += new System.EventHandler(this.sendB_Click);
            // 
            // emailDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 362);
            this.Controls.Add(this.sendB);
            this.Controls.Add(this.outbound);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.from);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.body);
            this.Controls.Add(this.subject);
            this.Name = "emailDocument";
            this.Text = "Email Document";
            this.Load += new System.EventHandler(this.EmailDocument_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox from;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox body;
        private System.Windows.Forms.TextBox subject;
        private System.Windows.Forms.TextBox outbound;
        private System.Windows.Forms.Button sendB;
    }
}

