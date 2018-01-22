namespace dmKB_Mapper
{
    partial class km
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(km));
            this.kView = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Reset = new System.Windows.Forms.ToolStripButton();
            this.load = new System.Windows.Forms.ToolStripButton();
            this.save = new System.Windows.Forms.ToolStripButton();
            this.saveas = new System.Windows.Forms.ToolStripButton();
            this.hex = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.kView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kView
            // 
            this.kView.AllowUserToAddRows = false;
            this.kView.AllowUserToDeleteRows = false;
            this.kView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.kView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.kView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.kView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kView.Location = new System.Drawing.Point(0, 28);
            this.kView.Name = "kView";
            this.kView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.kView.Size = new System.Drawing.Size(954, 129);
            this.kView.TabIndex = 0;
            this.kView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.kView_CellContentClick);
            this.kView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.kView_CellFormatting);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Reset,
            this.load,
            this.save,
            this.saveas});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(954, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Reset
            // 
            this.Reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Reset.Image = ((System.Drawing.Image)(resources.GetObject("Reset.Image")));
            this.Reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(39, 22);
            this.Reset.Text = "Reset";
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // load
            // 
            this.load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.load.Image = ((System.Drawing.Image)(resources.GetObject("load.Image")));
            this.load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(34, 22);
            this.load.Text = "Load";
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // save
            // 
            this.save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.save.Image = ((System.Drawing.Image)(resources.GetObject("save.Image")));
            this.save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(35, 22);
            this.save.Text = "Save";
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // saveas
            // 
            this.saveas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.saveas.Image = ((System.Drawing.Image)(resources.GetObject("saveas.Image")));
            this.saveas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveas.Name = "saveas";
            this.saveas.Size = new System.Drawing.Size(50, 22);
            this.saveas.Text = "Save As";
            this.saveas.Click += new System.EventHandler(this.saveas_Click);
            // 
            // hex
            // 
            this.hex.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hex.Location = new System.Drawing.Point(12, 163);
            this.hex.Multiline = true;
            this.hex.Name = "hex";
            this.hex.Size = new System.Drawing.Size(930, 580);
            this.hex.TabIndex = 2;
            this.hex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hex_KeyDown);
            this.hex.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.hex_PreviewKeyDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // km
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 744);
            this.Controls.Add(this.hex);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.kView);
            this.Name = "km";
            this.Text = "Data Moderated Keyboard Mappe";
            this.Load += new System.EventHandler(this.km_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView kView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton load;
        private System.Windows.Forms.ToolStripButton save;
        private System.Windows.Forms.ToolStripButton saveas;
        private System.Windows.Forms.ToolStripButton Reset;
        private System.Windows.Forms.TextBox hex;
        private System.Windows.Forms.Timer timer1;
    }
}

