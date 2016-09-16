namespace SlowMotionController
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.DefaultReplayDuration = new System.Windows.Forms.NumericUpDown();
            this.PlaybackName = new System.Windows.Forms.Label();
            this.PlaybackTime = new System.Windows.Forms.Label();
            this.Speed100 = new System.Windows.Forms.Button();
            this.Speed75 = new System.Windows.Forms.Button();
            this.Speed50 = new System.Windows.Forms.Button();
            this.Speed33 = new System.Windows.Forms.Button();
            this.Speed25 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SpeedBar = new System.Windows.Forms.TrackBar();
            this.PlayAllSelectedButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.PlaySingleSelectedButton = new System.Windows.Forms.Button();
            this.Plus2Button = new System.Windows.Forms.Button();
            this.Plus1Button = new System.Windows.Forms.Button();
            this.Minus1Button = new System.Windows.Forms.Button();
            this.Minus2Button = new System.Windows.Forms.Button();
            this.LiveButton = new System.Windows.Forms.Button();
            this.Camera3Button = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Camera2Button = new System.Windows.Forms.RadioButton();
            this.Camera1Button = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.OutPointLabel = new System.Windows.Forms.Label();
            this.InPointLabel = new System.Windows.Forms.Label();
            this.CueListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CurrentTime = new System.Windows.Forms.Label();
            this.MarkOutButton = new System.Windows.Forms.Button();
            this.MarkInButton = new System.Windows.Forms.Button();
            this.FileToolStrip = new System.Windows.Forms.ToolStrip();
            this.SaveToFile = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.ServerAddressComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.ConnectButton = new System.Windows.Forms.ToolStripButton();
            this.StatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SystemStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ShuttleTrackBar = new System.Windows.Forms.TrackBar();
            this.ShuttleSpeedLabel = new System.Windows.Forms.Label();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultReplayDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedBar)).BeginInit();
            this.FileToolStrip.SuspendLayout();
            this.ToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShuttleTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.StatusStrip);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1243, 568);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1243, 621);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.FileToolStrip);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.ToolStrip);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel,
            this.SystemStatusLabel});
            this.StatusStrip.Location = new System.Drawing.Point(0, 0);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1243, 25);
            this.StatusStrip.TabIndex = 0;
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(1228, 20);
            this.StatusLabel.Spring = true;
            this.StatusLabel.Text = "Jan Starzak 2016";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ShuttleSpeedLabel);
            this.splitContainer1.Panel1.Controls.Add(this.ShuttleTrackBar);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.DefaultReplayDuration);
            this.splitContainer1.Panel1.Controls.Add(this.PlaybackName);
            this.splitContainer1.Panel1.Controls.Add(this.PlaybackTime);
            this.splitContainer1.Panel1.Controls.Add(this.Speed100);
            this.splitContainer1.Panel1.Controls.Add(this.Speed75);
            this.splitContainer1.Panel1.Controls.Add(this.Speed50);
            this.splitContainer1.Panel1.Controls.Add(this.Speed33);
            this.splitContainer1.Panel1.Controls.Add(this.Speed25);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.SpeedBar);
            this.splitContainer1.Panel1.Controls.Add(this.PlayAllSelectedButton);
            this.splitContainer1.Panel1.Controls.Add(this.StopButton);
            this.splitContainer1.Panel1.Controls.Add(this.PlaySingleSelectedButton);
            this.splitContainer1.Panel1.Controls.Add(this.Plus2Button);
            this.splitContainer1.Panel1.Controls.Add(this.Plus1Button);
            this.splitContainer1.Panel1.Controls.Add(this.Minus1Button);
            this.splitContainer1.Panel1.Controls.Add(this.Minus2Button);
            this.splitContainer1.Panel1.Controls.Add(this.LiveButton);
            this.splitContainer1.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.Camera3Button);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.Camera2Button);
            this.splitContainer1.Panel2.Controls.Add(this.Camera1Button);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label7);
            this.splitContainer1.Panel2.Controls.Add(this.OutPointLabel);
            this.splitContainer1.Panel2.Controls.Add(this.InPointLabel);
            this.splitContainer1.Panel2.Controls.Add(this.CueListView);
            this.splitContainer1.Panel2.Controls.Add(this.CurrentTime);
            this.splitContainer1.Panel2.Controls.Add(this.MarkOutButton);
            this.splitContainer1.Panel2.Controls.Add(this.MarkInButton);
            this.splitContainer1.Size = new System.Drawing.Size(1243, 568);
            this.splitContainer1.SplitterDistance = 371;
            this.splitContainer1.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 19);
            this.label1.TabIndex = 56;
            this.label1.Text = "Default duration:";
            // 
            // DefaultReplayDuration
            // 
            this.DefaultReplayDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultReplayDuration.Location = new System.Drawing.Point(264, 294);
            this.DefaultReplayDuration.Name = "DefaultReplayDuration";
            this.DefaultReplayDuration.Size = new System.Drawing.Size(98, 26);
            this.DefaultReplayDuration.TabIndex = 55;
            this.DefaultReplayDuration.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.DefaultReplayDuration.ValueChanged += new System.EventHandler(this.replayDuration_ValueChanged);
            // 
            // PlaybackName
            // 
            this.PlaybackName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaybackName.Location = new System.Drawing.Point(12, 8);
            this.PlaybackName.Name = "PlaybackName";
            this.PlaybackName.Size = new System.Drawing.Size(277, 21);
            this.PlaybackName.TabIndex = 54;
            this.PlaybackName.Text = "IN1";
            // 
            // PlaybackTime
            // 
            this.PlaybackTime.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.PlaybackTime.Location = new System.Drawing.Point(11, 29);
            this.PlaybackTime.Name = "PlaybackTime";
            this.PlaybackTime.Size = new System.Drawing.Size(277, 30);
            this.PlaybackTime.TabIndex = 53;
            this.PlaybackTime.Text = "00:00:00.00";
            // 
            // Speed100
            // 
            this.Speed100.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Speed100.Location = new System.Drawing.Point(272, 238);
            this.Speed100.Name = "Speed100";
            this.Speed100.Size = new System.Drawing.Size(52, 29);
            this.Speed100.TabIndex = 17;
            this.Speed100.Text = "100%";
            this.Speed100.UseVisualStyleBackColor = true;
            this.Speed100.Click += new System.EventHandler(this.Speed100_Click);
            // 
            // Speed75
            // 
            this.Speed75.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Speed75.Location = new System.Drawing.Point(216, 238);
            this.Speed75.Name = "Speed75";
            this.Speed75.Size = new System.Drawing.Size(52, 29);
            this.Speed75.TabIndex = 16;
            this.Speed75.Text = "75%";
            this.Speed75.UseVisualStyleBackColor = true;
            this.Speed75.Click += new System.EventHandler(this.Speed75_Click);
            // 
            // Speed50
            // 
            this.Speed50.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Speed50.Location = new System.Drawing.Point(160, 238);
            this.Speed50.Name = "Speed50";
            this.Speed50.Size = new System.Drawing.Size(52, 29);
            this.Speed50.TabIndex = 15;
            this.Speed50.Text = "50%";
            this.Speed50.UseVisualStyleBackColor = true;
            this.Speed50.Click += new System.EventHandler(this.Speed50_Click);
            // 
            // Speed33
            // 
            this.Speed33.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Speed33.Location = new System.Drawing.Point(104, 238);
            this.Speed33.Name = "Speed33";
            this.Speed33.Size = new System.Drawing.Size(52, 29);
            this.Speed33.TabIndex = 14;
            this.Speed33.Text = "33%";
            this.Speed33.UseVisualStyleBackColor = true;
            this.Speed33.Click += new System.EventHandler(this.Speed33_Click);
            // 
            // Speed25
            // 
            this.Speed25.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Speed25.Location = new System.Drawing.Point(48, 238);
            this.Speed25.Name = "Speed25";
            this.Speed25.Size = new System.Drawing.Size(52, 29);
            this.Speed25.TabIndex = 13;
            this.Speed25.Text = "25%";
            this.Speed25.UseVisualStyleBackColor = true;
            this.Speed25.Click += new System.EventHandler(this.Speed25_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(179, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 19);
            this.label4.TabIndex = 47;
            this.label4.Text = "50";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(348, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 19);
            this.label3.TabIndex = 46;
            this.label3.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(11, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 19);
            this.label2.TabIndex = 45;
            this.label2.Text = "0";
            // 
            // SpeedBar
            // 
            this.SpeedBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SpeedBar.LargeChange = 25;
            this.SpeedBar.Location = new System.Drawing.Point(4, 187);
            this.SpeedBar.Maximum = 100;
            this.SpeedBar.Name = "SpeedBar";
            this.SpeedBar.Size = new System.Drawing.Size(369, 56);
            this.SpeedBar.SmallChange = 10;
            this.SpeedBar.TabIndex = 12;
            this.SpeedBar.TickFrequency = 10;
            this.SpeedBar.Value = 100;
            this.SpeedBar.Scroll += new System.EventHandler(this.SpeedBar_Scroll);
            // 
            // PlayAllSelectedButton
            // 
            this.PlayAllSelectedButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PlayAllSelectedButton.Image = ((System.Drawing.Image)(resources.GetObject("PlayAllSelectedButton.Image")));
            this.PlayAllSelectedButton.Location = new System.Drawing.Point(261, 138);
            this.PlayAllSelectedButton.Name = "PlayAllSelectedButton";
            this.PlayAllSelectedButton.Size = new System.Drawing.Size(64, 29);
            this.PlayAllSelectedButton.TabIndex = 11;
            this.PlayAllSelectedButton.UseVisualStyleBackColor = true;
            this.PlayAllSelectedButton.Click += new System.EventHandler(this.PlayAllSelectedButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.StopButton.Image = ((System.Drawing.Image)(resources.GetObject("StopButton.Image")));
            this.StopButton.Location = new System.Drawing.Point(48, 138);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(65, 29);
            this.StopButton.TabIndex = 9;
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // PlaySingleSelectedButton
            // 
            this.PlaySingleSelectedButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PlaySingleSelectedButton.Image = ((System.Drawing.Image)(resources.GetObject("PlaySingleSelectedButton.Image")));
            this.PlaySingleSelectedButton.Location = new System.Drawing.Point(119, 138);
            this.PlaySingleSelectedButton.Name = "PlaySingleSelectedButton";
            this.PlaySingleSelectedButton.Size = new System.Drawing.Size(136, 29);
            this.PlaySingleSelectedButton.TabIndex = 10;
            this.PlaySingleSelectedButton.UseVisualStyleBackColor = true;
            this.PlaySingleSelectedButton.Click += new System.EventHandler(this.PlaySingleSelectedButton_Click);
            // 
            // Plus2Button
            // 
            this.Plus2Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Plus2Button.Location = new System.Drawing.Point(261, 103);
            this.Plus2Button.Name = "Plus2Button";
            this.Plus2Button.Size = new System.Drawing.Size(65, 29);
            this.Plus2Button.TabIndex = 8;
            this.Plus2Button.Text = "+2 sec";
            this.Plus2Button.UseVisualStyleBackColor = true;
            this.Plus2Button.Click += new System.EventHandler(this.Plus2Button_Click);
            // 
            // Plus1Button
            // 
            this.Plus1Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Plus1Button.Location = new System.Drawing.Point(190, 103);
            this.Plus1Button.Name = "Plus1Button";
            this.Plus1Button.Size = new System.Drawing.Size(65, 29);
            this.Plus1Button.TabIndex = 7;
            this.Plus1Button.Text = "+1 sec";
            this.Plus1Button.UseVisualStyleBackColor = true;
            this.Plus1Button.Click += new System.EventHandler(this.Plus1Button_Click);
            // 
            // Minus1Button
            // 
            this.Minus1Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Minus1Button.Location = new System.Drawing.Point(119, 103);
            this.Minus1Button.Name = "Minus1Button";
            this.Minus1Button.Size = new System.Drawing.Size(65, 29);
            this.Minus1Button.TabIndex = 6;
            this.Minus1Button.Text = "-1 sec";
            this.Minus1Button.UseVisualStyleBackColor = true;
            this.Minus1Button.Click += new System.EventHandler(this.Minus1Button_Click);
            // 
            // Minus2Button
            // 
            this.Minus2Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Minus2Button.Location = new System.Drawing.Point(48, 103);
            this.Minus2Button.Name = "Minus2Button";
            this.Minus2Button.Size = new System.Drawing.Size(65, 29);
            this.Minus2Button.TabIndex = 5;
            this.Minus2Button.Text = "-2 sec";
            this.Minus2Button.UseVisualStyleBackColor = true;
            this.Minus2Button.Click += new System.EventHandler(this.Minus2Button_Click);
            // 
            // LiveButton
            // 
            this.LiveButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LiveButton.Location = new System.Drawing.Point(48, 68);
            this.LiveButton.Name = "LiveButton";
            this.LiveButton.Size = new System.Drawing.Size(278, 29);
            this.LiveButton.TabIndex = 4;
            this.LiveButton.Text = "LIVE";
            this.LiveButton.UseVisualStyleBackColor = true;
            this.LiveButton.Click += new System.EventHandler(this.LiveButton_Click);
            // 
            // Camera3Button
            // 
            this.Camera3Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Camera3Button.Appearance = System.Windows.Forms.Appearance.Button;
            this.Camera3Button.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Camera3Button.Location = new System.Drawing.Point(512, 11);
            this.Camera3Button.Name = "Camera3Button";
            this.Camera3Button.Size = new System.Drawing.Size(45, 40);
            this.Camera3Button.TabIndex = 46;
            this.Camera3Button.Text = "3";
            this.Camera3Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Camera3Button.UseVisualStyleBackColor = true;
            this.Camera3Button.Click += new System.EventHandler(this.Camera3Button_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(3, 531);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 29);
            this.button2.TabIndex = 45;
            this.button2.Text = "Delete";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(98, 531);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 29);
            this.button1.TabIndex = 44;
            this.button1.Text = "Clone";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Camera2Button
            // 
            this.Camera2Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Camera2Button.Appearance = System.Windows.Forms.Appearance.Button;
            this.Camera2Button.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Camera2Button.Location = new System.Drawing.Point(461, 11);
            this.Camera2Button.Name = "Camera2Button";
            this.Camera2Button.Size = new System.Drawing.Size(45, 40);
            this.Camera2Button.TabIndex = 43;
            this.Camera2Button.Text = "2";
            this.Camera2Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Camera2Button.UseVisualStyleBackColor = true;
            this.Camera2Button.Click += new System.EventHandler(this.Camera2Button_Click);
            // 
            // Camera1Button
            // 
            this.Camera1Button.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Camera1Button.Appearance = System.Windows.Forms.Appearance.Button;
            this.Camera1Button.Checked = true;
            this.Camera1Button.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Camera1Button.Location = new System.Drawing.Point(410, 11);
            this.Camera1Button.Name = "Camera1Button";
            this.Camera1Button.Size = new System.Drawing.Size(45, 40);
            this.Camera1Button.TabIndex = 42;
            this.Camera1Button.TabStop = true;
            this.Camera1Button.Text = "1";
            this.Camera1Button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Camera1Button.UseVisualStyleBackColor = true;
            this.Camera1Button.Click += new System.EventHandler(this.Camera1Button_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(677, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 19);
            this.label8.TabIndex = 41;
            this.label8.Text = "OUT:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(688, 11);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 19);
            this.label7.TabIndex = 40;
            this.label7.Text = "IN:";
            // 
            // OutPointLabel
            // 
            this.OutPointLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OutPointLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OutPointLabel.Location = new System.Drawing.Point(715, 32);
            this.OutPointLabel.Name = "OutPointLabel";
            this.OutPointLabel.Size = new System.Drawing.Size(146, 29);
            this.OutPointLabel.TabIndex = 39;
            this.OutPointLabel.Text = "00:00:00.00";
            this.OutPointLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // InPointLabel
            // 
            this.InPointLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.InPointLabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.InPointLabel.Location = new System.Drawing.Point(715, 3);
            this.InPointLabel.Name = "InPointLabel";
            this.InPointLabel.Size = new System.Drawing.Size(146, 29);
            this.InPointLabel.TabIndex = 38;
            this.InPointLabel.Text = "00:00:00.00";
            this.InPointLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CueListView
            // 
            this.CueListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CueListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.CueListView.FullRowSelect = true;
            this.CueListView.Location = new System.Drawing.Point(3, 68);
            this.CueListView.Name = "CueListView";
            this.CueListView.Size = new System.Drawing.Size(858, 457);
            this.CueListView.TabIndex = 1;
            this.CueListView.UseCompatibleStateImageBehavior = false;
            this.CueListView.View = System.Windows.Forms.View.Details;
            this.CueListView.SelectedIndexChanged += new System.EventHandler(this.CueListView_SelectedIndexChanged);
            this.CueListView.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CueListView_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "In Point";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Duration";
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Comment";
            this.columnHeader4.Width = 259;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Camera";
            this.columnHeader5.Width = 100;
            // 
            // CurrentTime
            // 
            this.CurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentTime.Font = new System.Drawing.Font("Consolas", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CurrentTime.Location = new System.Drawing.Point(-5, 11);
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.Size = new System.Drawing.Size(507, 49);
            this.CurrentTime.TabIndex = 36;
            this.CurrentTime.Text = "00:00:00.00";
            this.CurrentTime.Click += new System.EventHandler(this.CurrentTime_Click);
            // 
            // MarkOutButton
            // 
            this.MarkOutButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MarkOutButton.Location = new System.Drawing.Point(771, 531);
            this.MarkOutButton.Name = "MarkOutButton";
            this.MarkOutButton.Size = new System.Drawing.Size(90, 29);
            this.MarkOutButton.TabIndex = 3;
            this.MarkOutButton.Text = "Mark OUT";
            this.MarkOutButton.UseVisualStyleBackColor = true;
            this.MarkOutButton.Click += new System.EventHandler(this.MarkOutButton_Click);
            // 
            // MarkInButton
            // 
            this.MarkInButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.MarkInButton.Location = new System.Drawing.Point(675, 531);
            this.MarkInButton.Name = "MarkInButton";
            this.MarkInButton.Size = new System.Drawing.Size(90, 29);
            this.MarkInButton.TabIndex = 2;
            this.MarkInButton.Text = "Mark IN";
            this.MarkInButton.UseVisualStyleBackColor = true;
            this.MarkInButton.Click += new System.EventHandler(this.MarkInButton_Click);
            // 
            // FileToolStrip
            // 
            this.FileToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.FileToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveToFile});
            this.FileToolStrip.Location = new System.Drawing.Point(3, 0);
            this.FileToolStrip.Name = "FileToolStrip";
            this.FileToolStrip.Size = new System.Drawing.Size(35, 25);
            this.FileToolStrip.TabIndex = 1;
            // 
            // SaveToFile
            // 
            this.SaveToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveToFile.Image = ((System.Drawing.Image)(resources.GetObject("SaveToFile.Image")));
            this.SaveToFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToFile.Name = "SaveToFile";
            this.SaveToFile.Size = new System.Drawing.Size(23, 22);
            this.SaveToFile.Text = "Save";
            this.SaveToFile.Click += new System.EventHandler(this.SaveToFile_Click);
            // 
            // ToolStrip
            // 
            this.ToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServerAddressComboBox,
            this.ConnectButton});
            this.ToolStrip.Location = new System.Drawing.Point(38, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Size = new System.Drawing.Size(327, 28);
            this.ToolStrip.TabIndex = 0;
            // 
            // ServerAddressComboBox
            // 
            this.ServerAddressComboBox.Name = "ServerAddressComboBox";
            this.ServerAddressComboBox.Size = new System.Drawing.Size(230, 28);
            this.ServerAddressComboBox.Text = "localhost";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Image = ((System.Drawing.Image)(resources.GetObject("ConnectButton.Image")));
            this.ConnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(83, 25);
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // StatusUpdate
            // 
            this.StatusUpdate.Interval = 40;
            this.StatusUpdate.Tick += new System.EventHandler(this.StatusUpdate_Tick);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.Filter = "EDL file (*.txt)|*.txt";
            // 
            // SystemStatusLabel
            // 
            this.SystemStatusLabel.Name = "SystemStatusLabel";
            this.SystemStatusLabel.Size = new System.Drawing.Size(0, 20);
            // 
            // ShuttleTrackBar
            // 
            this.ShuttleTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShuttleTrackBar.Location = new System.Drawing.Point(4, 384);
            this.ShuttleTrackBar.Maximum = 20;
            this.ShuttleTrackBar.Minimum = -20;
            this.ShuttleTrackBar.Name = "ShuttleTrackBar";
            this.ShuttleTrackBar.Size = new System.Drawing.Size(369, 56);
            this.ShuttleTrackBar.TabIndex = 57;
            this.ShuttleTrackBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.ShuttleTrackBar.Scroll += new System.EventHandler(this.ShuttleTrackBar_Scroll);
            this.ShuttleTrackBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ShuttleTrackBar_MouseUp);
            // 
            // ShuttleSpeedLabel
            // 
            this.ShuttleSpeedLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShuttleSpeedLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ShuttleSpeedLabel.Location = new System.Drawing.Point(139, 454);
            this.ShuttleSpeedLabel.Name = "ShuttleSpeedLabel";
            this.ShuttleSpeedLabel.Size = new System.Drawing.Size(96, 39);
            this.ShuttleSpeedLabel.TabIndex = 58;
            this.ShuttleSpeedLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1243, 621);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainWindow";
            this.Text = "Slow Motion Controller";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainWindow_KeyUp);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DefaultReplayDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpeedBar)).EndInit();
            this.FileToolStrip.ResumeLayout(false);
            this.FileToolStrip.PerformLayout();
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShuttleTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripComboBox ServerAddressComboBox;
        private System.Windows.Forms.ToolStripButton ConnectButton;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Timer StatusUpdate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button Speed100;
        private System.Windows.Forms.Button Speed75;
        private System.Windows.Forms.Button Speed50;
        private System.Windows.Forms.Button Speed33;
        private System.Windows.Forms.Button Speed25;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar SpeedBar;
        private System.Windows.Forms.Button PlayAllSelectedButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button PlaySingleSelectedButton;
        private System.Windows.Forms.Button Plus2Button;
        private System.Windows.Forms.Button Plus1Button;
        private System.Windows.Forms.Button Minus1Button;
        private System.Windows.Forms.Button Minus2Button;
        private System.Windows.Forms.Button LiveButton;
        private System.Windows.Forms.RadioButton Camera2Button;
        private System.Windows.Forms.RadioButton Camera1Button;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label OutPointLabel;
        private System.Windows.Forms.Label InPointLabel;
        private System.Windows.Forms.ListView CueListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Label CurrentTime;
        private System.Windows.Forms.Button MarkOutButton;
        private System.Windows.Forms.Button MarkInButton;
        private System.Windows.Forms.Label PlaybackName;
        private System.Windows.Forms.Label PlaybackTime;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStrip FileToolStrip;
        private System.Windows.Forms.ToolStripButton SaveToFile;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.RadioButton Camera3Button;
        private System.Windows.Forms.NumericUpDown DefaultReplayDuration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel SystemStatusLabel;
        private System.Windows.Forms.TrackBar ShuttleTrackBar;
        private System.Windows.Forms.Label ShuttleSpeedLabel;

    }
}

