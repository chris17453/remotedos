namespace dm_memBuster {
    partial class Form1 {
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.start = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.hex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.offset = new System.Windows.Forms.NumericUpDown();
            this.depth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.pid = new System.Windows.Forms.TextBox();
            this.go = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.offset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.depth)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(13, 13);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(94, 12);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(75, 23);
            this.stop.TabIndex = 1;
            this.stop.Text = "Stop";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // hex
            // 
            this.hex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.hex.BackColor = System.Drawing.Color.Black;
            this.hex.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hex.ForeColor = System.Drawing.Color.White;
            this.hex.ImeMode = System.Windows.Forms.ImeMode.On;
            this.hex.Location = new System.Drawing.Point(13, 43);
            this.hex.Multiline = true;
            this.hex.Name = "hex";
            this.hex.Size = new System.Drawing.Size(695, 283);
            this.hex.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(175, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Offset";
            // 
            // offset
            // 
            this.offset.Increment = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.offset.Location = new System.Drawing.Point(216, 14);
            this.offset.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.offset.Name = "offset";
            this.offset.Size = new System.Drawing.Size(101, 20);
            this.offset.TabIndex = 4;
            this.offset.ValueChanged += new System.EventHandler(this.offset_ValueChanged);
            // 
            // depth
            // 
            this.depth.Location = new System.Drawing.Point(388, 13);
            this.depth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.depth.Name = "depth";
            this.depth.Size = new System.Drawing.Size(54, 20);
            this.depth.TabIndex = 5;
            this.depth.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.depth.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Depth";
            // 
            // pid
            // 
            this.pid.Location = new System.Drawing.Point(491, 15);
            this.pid.Name = "pid";
            this.pid.Size = new System.Drawing.Size(100, 20);
            this.pid.TabIndex = 7;
            // 
            // go
            // 
            this.go.Location = new System.Drawing.Point(608, 13);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(75, 23);
            this.go.TabIndex = 8;
            this.go.Text = "Go";
            this.go.UseVisualStyleBackColor = true;
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 338);
            this.Controls.Add(this.go);
            this.Controls.Add(this.pid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.depth);
            this.Controls.Add(this.offset);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hex);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.start);
            this.Name = "Form1";
            this.Text = "dm Mem Buster. A memory viewer.";
            ((System.ComponentModel.ISupportInitialize)(this.offset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.depth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.TextBox hex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown offset;
        private System.Windows.Forms.NumericUpDown depth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pid;
        private System.Windows.Forms.Button go;
    }
}

