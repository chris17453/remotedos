namespace dmRDClient_PC
{
    partial class ui
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ui));
            this.pullTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoPrintToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.conserveBandwidthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binaryScreenCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.screenSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hugeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.fullScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.userActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailScreenShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rePrintDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reprintLastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.idleInterval1Timer = new System.Windows.Forms.Timer(this.components);
            this.idleInteval2Timer = new System.Windows.Forms.Timer(this.components);
            this.startupTimer = new System.Windows.Forms.Timer(this.components);
            this.printTimer = new System.Windows.Forms.Timer(this.components);
            this.scanNewApps = new System.Windows.Forms.Timer(this.components);
            this.idleTrigger = new System.Windows.Forms.Timer(this.components);
            this.exitTimer = new System.Windows.Forms.Timer(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.dosScreen1 = new dm.dosScreen();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pullTimer
            // 
            this.pullTimer.Interval = 300;
            this.pullTimer.Tick += new System.EventHandler(this.pullTimer_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.toolStripSeparator1,
            this.screenSizeToolStripMenuItem,
            this.toolStripSeparator3,
            this.userActionsToolStripMenuItem,
            this.reprintLastToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 104);
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoPrintToolStripMenuItem,
            this.toolStripSeparator5,
            this.conserveBandwidthToolStripMenuItem,
            this.showDataToolStripMenuItem,
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem,
            this.binaryScreenCaptureToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // autoPrintToolStripMenuItem
            // 
            this.autoPrintToolStripMenuItem.Checked = true;
            this.autoPrintToolStripMenuItem.CheckOnClick = true;
            this.autoPrintToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoPrintToolStripMenuItem.Name = "autoPrintToolStripMenuItem";
            this.autoPrintToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.autoPrintToolStripMenuItem.Text = "AutoPrint";
            this.autoPrintToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(214, 6);
            // 
            // conserveBandwidthToolStripMenuItem
            // 
            this.conserveBandwidthToolStripMenuItem.Name = "conserveBandwidthToolStripMenuItem";
            this.conserveBandwidthToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.conserveBandwidthToolStripMenuItem.Text = "Conserve Bandwidth";
            this.conserveBandwidthToolStripMenuItem.Click += new System.EventHandler(this.conserveBandwidthToolStripMenuItem_Click_1);
            // 
            // showDataToolStripMenuItem
            // 
            this.showDataToolStripMenuItem.Name = "showDataToolStripMenuItem";
            this.showDataToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.showDataToolStripMenuItem.Text = "Show Data";
            this.showDataToolStripMenuItem.Click += new System.EventHandler(this.showDataToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(214, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click_1);
            // 
            // binaryScreenCaptureToolStripMenuItem
            // 
            this.binaryScreenCaptureToolStripMenuItem.Name = "binaryScreenCaptureToolStripMenuItem";
            this.binaryScreenCaptureToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.binaryScreenCaptureToolStripMenuItem.Text = "Binary Screen Capture";
            this.binaryScreenCaptureToolStripMenuItem.Click += new System.EventHandler(this.binaryScreenCaptureToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // screenSizeToolStripMenuItem
            // 
            this.screenSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.hugeToolStripMenuItem,
            this.largeToolStripMenuItem,
            this.toolStripSeparator7,
            this.fullScreenToolStripMenuItem});
            this.screenSizeToolStripMenuItem.Name = "screenSizeToolStripMenuItem";
            this.screenSizeToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.screenSizeToolStripMenuItem.Text = "Screen Size";
            // 
            // sizeToolStripMenuItem
            // 
            this.sizeToolStripMenuItem.Name = "sizeToolStripMenuItem";
            this.sizeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.sizeToolStripMenuItem.Text = "Small";
            this.sizeToolStripMenuItem.Click += new System.EventHandler(this.sizeToolStripMenuItem_Click_1);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.mediumToolStripMenuItem.Text = "Medium";
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click_1);
            // 
            // hugeToolStripMenuItem
            // 
            this.hugeToolStripMenuItem.Name = "hugeToolStripMenuItem";
            this.hugeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.hugeToolStripMenuItem.Text = "Huge";
            this.hugeToolStripMenuItem.Click += new System.EventHandler(this.hugeToolStripMenuItem_Click_1);
            // 
            // largeToolStripMenuItem
            // 
            this.largeToolStripMenuItem.Name = "largeToolStripMenuItem";
            this.largeToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.largeToolStripMenuItem.Text = "Large";
            this.largeToolStripMenuItem.Click += new System.EventHandler(this.largeToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(146, 6);
            // 
            // fullScreenToolStripMenuItem
            // 
            this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
            this.fullScreenToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.fullScreenToolStripMenuItem.Text = "FullScreen";
            this.fullScreenToolStripMenuItem.Click += new System.EventHandler(this.fullScreenToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // userActionsToolStripMenuItem
            // 
            this.userActionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emailScreenShotToolStripMenuItem,
            this.saveScreenToolStripMenuItem,
            this.rePrintDocumentToolStripMenuItem,
            this.printScreenToolStripMenuItem});
            this.userActionsToolStripMenuItem.Name = "userActionsToolStripMenuItem";
            this.userActionsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.userActionsToolStripMenuItem.Text = "User Actions";
            // 
            // emailScreenShotToolStripMenuItem
            // 
            this.emailScreenShotToolStripMenuItem.Name = "emailScreenShotToolStripMenuItem";
            this.emailScreenShotToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.emailScreenShotToolStripMenuItem.Text = "Email Screen Shot";
            this.emailScreenShotToolStripMenuItem.Click += new System.EventHandler(this.emailScreenShotToolStripMenuItem_Click_1);
            // 
            // saveScreenToolStripMenuItem
            // 
            this.saveScreenToolStripMenuItem.Name = "saveScreenToolStripMenuItem";
            this.saveScreenToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.saveScreenToolStripMenuItem.Text = "Save Screen";
            this.saveScreenToolStripMenuItem.Click += new System.EventHandler(this.saveScreenToolStripMenuItem_Click_1);
            // 
            // rePrintDocumentToolStripMenuItem
            // 
            this.rePrintDocumentToolStripMenuItem.Name = "rePrintDocumentToolStripMenuItem";
            this.rePrintDocumentToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.rePrintDocumentToolStripMenuItem.Text = "RePrint Document";
            this.rePrintDocumentToolStripMenuItem.Click += new System.EventHandler(this.rePrintDocumentToolStripMenuItem_Click);
            // 
            // printScreenToolStripMenuItem
            // 
            this.printScreenToolStripMenuItem.Name = "printScreenToolStripMenuItem";
            this.printScreenToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.printScreenToolStripMenuItem.Text = "Print Screen";
            this.printScreenToolStripMenuItem.Click += new System.EventHandler(this.printScreenToolStripMenuItem_Click);
            // 
            // reprintLastToolStripMenuItem
            // 
            this.reprintLastToolStripMenuItem.Name = "reprintLastToolStripMenuItem";
            this.reprintLastToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.reprintLastToolStripMenuItem.Text = "Reprint Last";
            this.reprintLastToolStripMenuItem.Click += new System.EventHandler(this.reprintLastToolStripMenuItem_Click);
            // 
            // idleInterval1Timer
            // 
            this.idleInterval1Timer.Enabled = true;
            this.idleInterval1Timer.Interval = 60000;
            this.idleInterval1Timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // idleInteval2Timer
            // 
            this.idleInteval2Timer.Enabled = true;
            this.idleInteval2Timer.Interval = 30000;
            this.idleInteval2Timer.Tick += new System.EventHandler(this.idleInteval2Timer_Tick);
            // 
            // startupTimer
            // 
            this.startupTimer.Enabled = true;
            this.startupTimer.Tick += new System.EventHandler(this.startupTimer_Tick);
            // 
            // printTimer
            // 
            this.printTimer.Interval = 3000;
            this.printTimer.Tick += new System.EventHandler(this.printTimer_Tick);
            // 
            // scanNewApps
            // 
            this.scanNewApps.Tick += new System.EventHandler(this.scanNewApps_Tick_1);
            // 
            // idleTrigger
            // 
            this.idleTrigger.Interval = 1000;
            this.idleTrigger.Tick += new System.EventHandler(this.idleTrigger_Tick);
            // 
            // exitTimer
            // 
            this.exitTimer.Interval = 1000;
            this.exitTimer.Tick += new System.EventHandler(this.exitTimer_Tick);
            // 
            // dosScreen1
            // 
            this.dosScreen1.AutoSize = true;
            this.dosScreen1.BackColor = System.Drawing.Color.Transparent;
            this.dosScreen1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.dosScreen1.colorMapBG = ((System.Collections.Hashtable)(resources.GetObject("dosScreen1.colorMapBG")));
            this.dosScreen1.colorMapFG = ((System.Collections.Hashtable)(resources.GetObject("dosScreen1.colorMapFG")));
            this.dosScreen1.columnWidth = 8.025F;
            this.dosScreen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dosScreen1.fontName = "Ludica Consle";
            this.dosScreen1.fontSize = 10F;
            this.dosScreen1.keyMap = ((System.Collections.Hashtable)(resources.GetObject("dosScreen1.keyMap")));
            this.dosScreen1.lineHeight = 15.52F;
            this.dosScreen1.Location = new System.Drawing.Point(0, 0);
            this.dosScreen1.Name = "dosScreen1";
            this.dosScreen1.selection = true;
            this.dosScreen1.Size = new System.Drawing.Size(642, 388);
            this.dosScreen1.TabIndex = 2;
            this.dosScreen1.Visible = false;
            this.dosScreen1.Load += new System.EventHandler(this.dosScreen1_Load);
            this.dosScreen1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dosScreen1_KeyDown);
            this.dosScreen1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dosScreen1_MouseClick);
            this.dosScreen1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dosScreen1_MouseDown);
            this.dosScreen1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dosScreen1_MouseMove);
            this.dosScreen1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dosScreen1_MouseUp);
            this.dosScreen1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.dosScreen1_PreviewKeyDown);
            this.dosScreen1.Resize += new System.EventHandler(this.dosScreen1_Resize);
            // 
            // ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(642, 388);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dosScreen1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ui";
            this.Text = "Remote Dos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer pullTimer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer idleInterval1Timer;
        private System.Windows.Forms.Timer idleInteval2Timer;
        private System.Windows.Forms.Timer startupTimer;
        private dm.dosScreen dosScreen1;
        private System.Windows.Forms.Timer printTimer;
        private System.Windows.Forms.Timer scanNewApps;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem systemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoPrintToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem conserveBandwidthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screenSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hugeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem largeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem fullScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailScreenShotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rePrintDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reprintLastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printScreenToolStripMenuItem;
        private System.Windows.Forms.Timer idleTrigger;
        private System.Windows.Forms.ToolStripMenuItem binaryScreenCaptureToolStripMenuItem;
        private System.Windows.Forms.Timer exitTimer;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.HelpProvider helpProvider1;



    }
}

