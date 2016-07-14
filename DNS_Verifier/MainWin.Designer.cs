namespace DNS_Verifier
{
    partial class MainWin
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
            this.configGroupBox = new System.Windows.Forms.GroupBox();
            this.gapPanel = new System.Windows.Forms.Panel();
            this.controlsGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.getDnsButton = new System.Windows.Forms.Button();
            this.dnsTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.pslContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generatePopularSitesListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.dnsGroupBox = new System.Windows.Forms.GroupBox();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.customCB = new System.Windows.Forms.CheckBox();
            this.openDnsCB = new System.Windows.Forms.CheckBox();
            this.level3CB = new System.Windows.Forms.CheckBox();
            this.freeDnsCB = new System.Windows.Forms.CheckBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.resultsGroupBox = new System.Windows.Forms.GroupBox();
            this.resultTextBox = new System.Windows.Forms.TextBox();
            this.aboutButton = new System.Windows.Forms.Button();
            this.bgWorkerDefault = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerGoogle = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerCustom = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerFreeDns = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerOpenDns = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerLevel3 = new System.ComponentModel.BackgroundWorker();
            this.resultTimer = new System.Windows.Forms.Timer(this.components);
            this.otherContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generateConfigFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generatePopularSitesListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.configGroupBox.SuspendLayout();
            this.controlsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pslContextMenuStrip.SuspendLayout();
            this.dnsGroupBox.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.resultsGroupBox.SuspendLayout();
            this.otherContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // configGroupBox
            // 
            this.configGroupBox.Controls.Add(this.gapPanel);
            this.configGroupBox.Controls.Add(this.controlsGroupBox);
            this.configGroupBox.Controls.Add(this.dnsGroupBox);
            this.configGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configGroupBox.Location = new System.Drawing.Point(5, 7);
            this.configGroupBox.Name = "configGroupBox";
            this.configGroupBox.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.configGroupBox.Size = new System.Drawing.Size(500, 138);
            this.configGroupBox.TabIndex = 0;
            this.configGroupBox.TabStop = false;
            this.configGroupBox.Text = "Configuration";
            // 
            // gapPanel
            // 
            this.gapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gapPanel.Location = new System.Drawing.Point(186, 20);
            this.gapPanel.Name = "gapPanel";
            this.gapPanel.Size = new System.Drawing.Size(10, 111);
            this.gapPanel.TabIndex = 2;
            // 
            // controlsGroupBox
            // 
            this.controlsGroupBox.Controls.Add(this.groupBox1);
            this.controlsGroupBox.Controls.Add(this.groupBox2);
            this.controlsGroupBox.Controls.Add(this.loadButton);
            this.controlsGroupBox.Controls.Add(this.startButton);
            this.controlsGroupBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.controlsGroupBox.Location = new System.Drawing.Point(196, 20);
            this.controlsGroupBox.Name = "controlsGroupBox";
            this.controlsGroupBox.Size = new System.Drawing.Size(299, 111);
            this.controlsGroupBox.TabIndex = 1;
            this.controlsGroupBox.TabStop = false;
            this.controlsGroupBox.Text = "Controls";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.getDnsButton);
            this.groupBox1.Controls.Add(this.dnsTextBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(119, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 92);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current DNS Servers";
            // 
            // getDnsButton
            // 
            this.getDnsButton.Location = new System.Drawing.Point(6, 66);
            this.getDnsButton.Name = "getDnsButton";
            this.getDnsButton.Size = new System.Drawing.Size(168, 24);
            this.getDnsButton.TabIndex = 1;
            this.getDnsButton.Text = "Get My Current DNS";
            this.getDnsButton.UseVisualStyleBackColor = true;
            this.getDnsButton.Click += new System.EventHandler(this.getDnsButton_Click);
            // 
            // dnsTextBox
            // 
            this.dnsTextBox.BackColor = System.Drawing.Color.White;
            this.dnsTextBox.Location = new System.Drawing.Point(6, 19);
            this.dnsTextBox.Multiline = true;
            this.dnsTextBox.Name = "dnsTextBox";
            this.dnsTextBox.ReadOnly = true;
            this.dnsTextBox.Size = new System.Drawing.Size(168, 43);
            this.dnsTextBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.statusLabel);
            this.groupBox2.Location = new System.Drawing.Point(6, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(107, 41);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "List of Sites";
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.Color.IndianRed;
            this.statusLabel.ContextMenuStrip = this.pslContextMenuStrip;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.LightGray;
            this.statusLabel.Location = new System.Drawing.Point(3, 16);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(101, 22);
            this.statusLabel.TabIndex = 0;
            this.statusLabel.Text = "Unloaded";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pslContextMenuStrip
            // 
            this.pslContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generatePopularSitesListToolStripMenuItem});
            this.pslContextMenuStrip.Name = "otherContextMenuStrip";
            this.pslContextMenuStrip.Size = new System.Drawing.Size(242, 26);
            // 
            // generatePopularSitesListToolStripMenuItem
            // 
            this.generatePopularSitesListToolStripMenuItem.Name = "generatePopularSitesListToolStripMenuItem";
            this.generatePopularSitesListToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.generatePopularSitesListToolStripMenuItem.Text = "Generate Popular Sites List Only";
            this.generatePopularSitesListToolStripMenuItem.Click += new System.EventHandler(this.generatePopularSitesListToolStripMenuItem_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(6, 82);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(107, 24);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load Sites List";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(6, 56);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(107, 24);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Check";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // dnsGroupBox
            // 
            this.dnsGroupBox.Controls.Add(this.ipTextBox);
            this.dnsGroupBox.Controls.Add(this.customCB);
            this.dnsGroupBox.Controls.Add(this.openDnsCB);
            this.dnsGroupBox.Controls.Add(this.level3CB);
            this.dnsGroupBox.Controls.Add(this.freeDnsCB);
            this.dnsGroupBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.dnsGroupBox.Location = new System.Drawing.Point(5, 20);
            this.dnsGroupBox.Name = "dnsGroupBox";
            this.dnsGroupBox.Size = new System.Drawing.Size(181, 111);
            this.dnsGroupBox.TabIndex = 0;
            this.dnsGroupBox.TabStop = false;
            this.dnsGroupBox.Text = "DNS to verify with";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(74, 85);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(100, 20);
            this.ipTextBox.TabIndex = 6;
            this.ipTextBox.Visible = false;
            this.ipTextBox.Leave += new System.EventHandler(this.ipTextBox_Leave);
            // 
            // customCB
            // 
            this.customCB.AutoSize = true;
            this.customCB.Location = new System.Drawing.Point(9, 87);
            this.customCB.Name = "customCB";
            this.customCB.Size = new System.Drawing.Size(61, 17);
            this.customCB.TabIndex = 4;
            this.customCB.Text = "Custom";
            this.customCB.UseVisualStyleBackColor = true;
            this.customCB.CheckedChanged += new System.EventHandler(this.customCB_CheckedChanged);
            // 
            // openDnsCB
            // 
            this.openDnsCB.AutoSize = true;
            this.openDnsCB.Location = new System.Drawing.Point(9, 64);
            this.openDnsCB.Name = "openDnsCB";
            this.openDnsCB.Size = new System.Drawing.Size(114, 17);
            this.openDnsCB.TabIndex = 3;
            this.openDnsCB.Text = "OpenDNS (Global)";
            this.openDnsCB.UseVisualStyleBackColor = true;
            this.openDnsCB.CheckedChanged += new System.EventHandler(this.dnsCB_CheckedChanged);
            // 
            // level3CB
            // 
            this.level3CB.AutoSize = true;
            this.level3CB.Checked = true;
            this.level3CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.level3CB.Location = new System.Drawing.Point(9, 19);
            this.level3CB.Name = "level3CB";
            this.level3CB.Size = new System.Drawing.Size(126, 17);
            this.level3CB.TabIndex = 2;
            this.level3CB.Text = "Level 3 DNS (Global)";
            this.level3CB.UseVisualStyleBackColor = true;
            this.level3CB.CheckedChanged += new System.EventHandler(this.dnsCB_CheckedChanged);
            // 
            // freeDnsCB
            // 
            this.freeDnsCB.AutoSize = true;
            this.freeDnsCB.Location = new System.Drawing.Point(9, 42);
            this.freeDnsCB.Name = "freeDnsCB";
            this.freeDnsCB.Size = new System.Drawing.Size(109, 17);
            this.freeDnsCB.TabIndex = 1;
            this.freeDnsCB.Text = "FreeDNS (Global)";
            this.freeDnsCB.UseVisualStyleBackColor = true;
            this.freeDnsCB.CheckedChanged += new System.EventHandler(this.dnsCB_CheckedChanged);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.configGroupBox);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.topPanel.Size = new System.Drawing.Size(510, 152);
            this.topPanel.TabIndex = 3;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.resultsGroupBox);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(0, 152);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Padding = new System.Windows.Forms.Padding(5, 7, 5, 7);
            this.bottomPanel.Size = new System.Drawing.Size(510, 131);
            this.bottomPanel.TabIndex = 4;
            // 
            // resultsGroupBox
            // 
            this.resultsGroupBox.Controls.Add(this.resultTextBox);
            this.resultsGroupBox.Controls.Add(this.aboutButton);
            this.resultsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsGroupBox.Location = new System.Drawing.Point(5, 7);
            this.resultsGroupBox.Name = "resultsGroupBox";
            this.resultsGroupBox.Padding = new System.Windows.Forms.Padding(5);
            this.resultsGroupBox.Size = new System.Drawing.Size(500, 117);
            this.resultsGroupBox.TabIndex = 3;
            this.resultsGroupBox.TabStop = false;
            this.resultsGroupBox.Text = "Results";
            // 
            // resultTextBox
            // 
            this.resultTextBox.BackColor = System.Drawing.Color.White;
            this.resultTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultTextBox.Location = new System.Drawing.Point(5, 18);
            this.resultTextBox.Multiline = true;
            this.resultTextBox.Name = "resultTextBox";
            this.resultTextBox.ReadOnly = true;
            this.resultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultTextBox.Size = new System.Drawing.Size(474, 94);
            this.resultTextBox.TabIndex = 1;
            // 
            // aboutButton
            // 
            this.aboutButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.aboutButton.FlatAppearance.BorderSize = 0;
            this.aboutButton.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.aboutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aboutButton.Location = new System.Drawing.Point(479, 18);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(16, 94);
            this.aboutButton.TabIndex = 6;
            this.aboutButton.Text = "About";
            this.aboutButton.UseVisualStyleBackColor = true;
            this.aboutButton.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // bgWorkerDefault
            // 
            this.bgWorkerDefault.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerDefault_DoWork);
            this.bgWorkerDefault.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerDefault_RunWorkerCompleted);
            // 
            // bgWorkerGoogle
            // 
            this.bgWorkerGoogle.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerGoogle_DoWork);
            this.bgWorkerGoogle.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerGoogle_RunWorkerCompleted);
            // 
            // bgWorkerCustom
            // 
            this.bgWorkerCustom.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerCustom_DoWork);
            this.bgWorkerCustom.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerCustom_RunWorkerCompleted);
            // 
            // bgWorkerFreeDns
            // 
            this.bgWorkerFreeDns.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerFreeDns_DoWork);
            this.bgWorkerFreeDns.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerFreeDns_RunWorkerCompleted);
            // 
            // bgWorkerOpenDns
            // 
            this.bgWorkerOpenDns.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerOpenDns_DoWork);
            this.bgWorkerOpenDns.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerOpenDns_RunWorkerCompleted);
            // 
            // bgWorkerLevel3
            // 
            this.bgWorkerLevel3.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkerLevel3_DoWork);
            this.bgWorkerLevel3.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorkerLevel3_RunWorkerCompleted);
            // 
            // resultTimer
            // 
            this.resultTimer.Tick += new System.EventHandler(this.resultTimer_Tick);
            // 
            // otherContextMenuStrip
            // 
            this.otherContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateConfigFilesToolStripMenuItem,
            this.generatePopularSitesListToolStripMenuItem1});
            this.otherContextMenuStrip.Name = "otherContextMenuStrip";
            this.otherContextMenuStrip.Size = new System.Drawing.Size(242, 48);
            // 
            // generateConfigFilesToolStripMenuItem
            // 
            this.generateConfigFilesToolStripMenuItem.Name = "generateConfigFilesToolStripMenuItem";
            this.generateConfigFilesToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.generateConfigFilesToolStripMenuItem.Text = "Generate Config Files";
            this.generateConfigFilesToolStripMenuItem.Click += new System.EventHandler(this.generateConfigFilesToolStripMenuItem_Click);
            // 
            // generatePopularSitesListToolStripMenuItem1
            // 
            this.generatePopularSitesListToolStripMenuItem1.Name = "generatePopularSitesListToolStripMenuItem1";
            this.generatePopularSitesListToolStripMenuItem1.Size = new System.Drawing.Size(241, 22);
            this.generatePopularSitesListToolStripMenuItem1.Text = "Generate Popular Sites List Only";
            this.generatePopularSitesListToolStripMenuItem1.Click += new System.EventHandler(this.generatePopularSitesListToolStripMenuItem_Click);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 283);
            this.ContextMenuStrip = this.otherContextMenuStrip;
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.topPanel);
            this.MaximumSize = new System.Drawing.Size(600, 9999999);
            this.MinimumSize = new System.Drawing.Size(526, 322);
            this.Name = "MainWin";
            this.ShowIcon = false;
            this.Text = "DNS Verifier";
            this.Load += new System.EventHandler(this.MainWin_Load);
            this.configGroupBox.ResumeLayout(false);
            this.controlsGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.pslContextMenuStrip.ResumeLayout(false);
            this.dnsGroupBox.ResumeLayout(false);
            this.dnsGroupBox.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.resultsGroupBox.ResumeLayout(false);
            this.resultsGroupBox.PerformLayout();
            this.otherContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox configGroupBox;
        private System.Windows.Forms.GroupBox dnsGroupBox;
        private System.Windows.Forms.CheckBox customCB;
        private System.Windows.Forms.CheckBox openDnsCB;
        private System.Windows.Forms.CheckBox level3CB;
        private System.Windows.Forms.CheckBox freeDnsCB;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.GroupBox resultsGroupBox;
        private System.Windows.Forms.TextBox resultTextBox;
        private System.Windows.Forms.GroupBox controlsGroupBox;
        private System.Windows.Forms.Panel gapPanel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button getDnsButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox dnsTextBox;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label statusLabel;
        private System.ComponentModel.BackgroundWorker bgWorkerDefault;
        private System.ComponentModel.BackgroundWorker bgWorkerGoogle;
        private System.ComponentModel.BackgroundWorker bgWorkerCustom;
        private System.ComponentModel.BackgroundWorker bgWorkerFreeDns;
        private System.ComponentModel.BackgroundWorker bgWorkerOpenDns;
        private System.ComponentModel.BackgroundWorker bgWorkerLevel3;
        private System.Windows.Forms.Timer resultTimer;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.ContextMenuStrip otherContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem generateConfigFilesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip pslContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem generatePopularSitesListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generatePopularSitesListToolStripMenuItem1;
    }
}

