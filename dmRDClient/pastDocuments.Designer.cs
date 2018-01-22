namespace dmRDClient_PC
{
    partial class pastDocuments
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
            this.documents = new System.Windows.Forms.DataGridView();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.preview = new System.Windows.Forms.WebBrowser();
            this.print = new System.Windows.Forms.Button();
            this.prtPreview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.documents)).BeginInit();
            this.SuspendLayout();
            // 
            // documents
            // 
            this.documents.AllowUserToAddRows = false;
            this.documents.AllowUserToDeleteRows = false;
            this.documents.AllowUserToResizeColumns = false;
            this.documents.AllowUserToResizeRows = false;
            this.documents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.documents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.documents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.documents.Location = new System.Drawing.Point(12, 35);
            this.documents.MultiSelect = false;
            this.documents.Name = "documents";
            this.documents.RowHeadersVisible = false;
            this.documents.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.documents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.documents.Size = new System.Drawing.Size(295, 590);
            this.documents.TabIndex = 0;
            this.documents.SelectionChanged += new System.EventHandler(this.documents_SelectionChanged);
            // 
            // preview
            // 
            this.preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preview.Location = new System.Drawing.Point(313, 59);
            this.preview.MinimumSize = new System.Drawing.Size(20, 20);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(534, 566);
            this.preview.TabIndex = 1;
            // 
            // print
            // 
            this.print.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.print.Location = new System.Drawing.Point(772, 30);
            this.print.Name = "print";
            this.print.Size = new System.Drawing.Size(75, 23);
            this.print.TabIndex = 2;
            this.print.Text = "Print";
            this.print.UseVisualStyleBackColor = true;
            this.print.Click += new System.EventHandler(this.print_Click);
            // 
            // prtPreview
            // 
            this.prtPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.prtPreview.Location = new System.Drawing.Point(679, 30);
            this.prtPreview.Name = "prtPreview";
            this.prtPreview.Size = new System.Drawing.Size(87, 23);
            this.prtPreview.TabIndex = 3;
            this.prtPreview.Text = "Print Preview";
            this.prtPreview.UseVisualStyleBackColor = true;
            this.prtPreview.Click += new System.EventHandler(this.prtPreview_Click);
            // 
            // pastDocuments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 637);
            this.Controls.Add(this.prtPreview);
            this.Controls.Add(this.print);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.documents);
            this.Name = "pastDocuments";
            this.Text = "RePrint Documents";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.pastDocuments_FormClosing);
            this.Load += new System.EventHandler(this.pastDocuments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.documents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.WebBrowser preview;
        public System.Windows.Forms.DataGridView documents;
        private System.Windows.Forms.Button print;
        private System.Windows.Forms.Button prtPreview;

    }
}