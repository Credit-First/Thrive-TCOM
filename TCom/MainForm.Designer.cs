namespace TCom
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lineGridView = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.logGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.filterList = new System.Windows.Forms.CheckedListBox();
            this.btnSaveLineFilter = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.numCountUpdateRate = new System.Windows.Forms.NumericUpDown();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numTimeoutTransaction = new System.Windows.Forms.NumericUpDown();
            this.button5 = new System.Windows.Forms.Button();
            this.numTimeoutConnect = new System.Windows.Forms.NumericUpDown();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numReadRate = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numPullSkuRate = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLoginMsg = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.eventGrid = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUpdateLocalEvents = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.numDTId = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numEventId = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.cboLines = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.btnUpdateLocalCounters = new System.Windows.Forms.Button();
            this.gridLocalCounters = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.btnUpdatePieceCounts = new System.Windows.Forms.Button();
            this.gridPieceCount = new System.Windows.Forms.DataGridView();
            this.tbDiagnosis = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDiagnosisResult = new System.Windows.Forms.Panel();
            this.btnRunOverlappingDTEvtChk = new System.Windows.Forms.Button();
            this.lblOverlappingDTEventsChkResult = new System.Windows.Forms.Label();
            this.cbxDiagnosisLine = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDiagnosisControls = new System.Windows.Forms.Panel();
            this.btnRunAccuracyChk = new System.Windows.Forms.Button();
            this.cbxAccuracyElement = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtAccuracyToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtAccuracyFromDate = new System.Windows.Forms.DateTimePicker();
            this.grdCountAccuracyData = new System.Windows.Forms.DataGridView();
            this.btnRunOrphanDTEvtChk = new System.Windows.Forms.Button();
            this.lblOrphanDTEventsChkResult = new System.Windows.Forms.Label();
            this.pnlDiagnosis = new System.Windows.Forms.Panel();
            this.lblDiagnosisLoginMsg = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiagnosisPassword = new System.Windows.Forms.TextBox();
            this.btnViewDiagnosis = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testAlarmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SQLiteCleanup = new System.Windows.Forms.Timer(this.components);
            this.statusCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.ledBulb1 = new TCom.LedBulb();
            ((System.ComponentModel.ISupportInitialize)(this.lineGridView)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCountUpdateRate)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeoutTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeoutConnect)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadRate)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPullSkuRate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventGrid)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDTId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEventId)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLocalCounters)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPieceCount)).BeginInit();
            this.tbDiagnosis.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.pnlDiagnosisResult.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.pnlDiagnosisControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCountAccuracyData)).BeginInit();
            this.pnlDiagnosis.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lineGridView
            // 
            this.lineGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.lineGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.lineGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lineGridView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineGridView.Location = new System.Drawing.Point(3, 3);
            this.lineGridView.Name = "lineGridView";
            this.lineGridView.Size = new System.Drawing.Size(772, 256);
            this.lineGridView.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.AccessibleName = "";
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tbDiagnosis);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(786, 395);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitter1);
            this.tabPage1.Controls.Add(this.logGridView);
            this.tabPage1.Controls.Add(this.lineGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(778, 369);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logger";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(3, 259);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(772, 10);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // logGridView
            // 
            this.logGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.logGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.logGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logGridView.Location = new System.Drawing.Point(3, 259);
            this.logGridView.Name = "logGridView";
            this.logGridView.ReadOnly = true;
            this.logGridView.Size = new System.Drawing.Size(772, 107);
            this.logGridView.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox8);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox6);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(778, 369);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.filterList);
            this.groupBox3.Controls.Add(this.btnSaveLineFilter);
            this.groupBox3.Location = new System.Drawing.Point(260, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(510, 357);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Filter Lines";
            // 
            // filterList
            // 
            this.filterList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterList.FormattingEnabled = true;
            this.filterList.Location = new System.Drawing.Point(6, 13);
            this.filterList.Name = "filterList";
            this.filterList.Size = new System.Drawing.Size(498, 244);
            this.filterList.TabIndex = 16;
            // 
            // btnSaveLineFilter
            // 
            this.btnSaveLineFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveLineFilter.Location = new System.Drawing.Point(6, 319);
            this.btnSaveLineFilter.Name = "btnSaveLineFilter";
            this.btnSaveLineFilter.Size = new System.Drawing.Size(498, 32);
            this.btnSaveLineFilter.TabIndex = 15;
            this.btnSaveLineFilter.Text = "Save";
            this.btnSaveLineFilter.UseVisualStyleBackColor = true;
            this.btnSaveLineFilter.Click += new System.EventHandler(this.btnSaveLineFilter_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.numCountUpdateRate);
            this.groupBox8.Controls.Add(this.button6);
            this.groupBox8.Location = new System.Drawing.Point(6, 310);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(248, 55);
            this.groupBox8.TabIndex = 13;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Counter Update Rate (s)";
            // 
            // numCountUpdateRate
            // 
            this.numCountUpdateRate.Location = new System.Drawing.Point(6, 19);
            this.numCountUpdateRate.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numCountUpdateRate.Name = "numCountUpdateRate";
            this.numCountUpdateRate.Size = new System.Drawing.Size(125, 20);
            this.numCountUpdateRate.TabIndex = 6;
            this.numCountUpdateRate.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(167, 19);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 20);
            this.button6.TabIndex = 5;
            this.button6.Text = "Save";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.numTimeoutTransaction);
            this.groupBox7.Controls.Add(this.button5);
            this.groupBox7.Controls.Add(this.numTimeoutConnect);
            this.groupBox7.Controls.Add(this.button4);
            this.groupBox7.Location = new System.Drawing.Point(6, 226);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(248, 78);
            this.groupBox7.TabIndex = 13;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Device Timeouts (ms)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Transaction:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Connection:";
            // 
            // numTimeoutTransaction
            // 
            this.numTimeoutTransaction.Location = new System.Drawing.Point(76, 52);
            this.numTimeoutTransaction.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numTimeoutTransaction.Name = "numTimeoutTransaction";
            this.numTimeoutTransaction.Size = new System.Drawing.Size(64, 20);
            this.numTimeoutTransaction.TabIndex = 8;
            this.numTimeoutTransaction.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(167, 51);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 19);
            this.button5.TabIndex = 7;
            this.button5.Text = "Save";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // numTimeoutConnect
            // 
            this.numTimeoutConnect.Location = new System.Drawing.Point(76, 25);
            this.numTimeoutConnect.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numTimeoutConnect.Name = "numTimeoutConnect";
            this.numTimeoutConnect.Size = new System.Drawing.Size(64, 20);
            this.numTimeoutConnect.TabIndex = 6;
            this.numTimeoutConnect.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(167, 25);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 20);
            this.button4.TabIndex = 5;
            this.button4.Text = "Save";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numReadRate);
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Location = new System.Drawing.Point(6, 166);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(248, 54);
            this.groupBox6.TabIndex = 12;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Read Rate (ms)";
            // 
            // numReadRate
            // 
            this.numReadRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numReadRate.Location = new System.Drawing.Point(76, 19);
            this.numReadRate.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numReadRate.Name = "numReadRate";
            this.numReadRate.Size = new System.Drawing.Size(64, 20);
            this.numReadRate.TabIndex = 6;
            this.numReadRate.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(167, 17);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 20);
            this.button3.TabIndex = 5;
            this.button3.Text = "Save";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numPullSkuRate);
            this.groupBox5.Location = new System.Drawing.Point(6, 112);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(248, 48);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "AscommStatus Ping Rate (min)";
            // 
            // numPullSkuRate
            // 
            this.numPullSkuRate.Location = new System.Drawing.Point(167, 19);
            this.numPullSkuRate.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numPullSkuRate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPullSkuRate.Name = "numPullSkuRate";
            this.numPullSkuRate.Size = new System.Drawing.Size(75, 20);
            this.numPullSkuRate.TabIndex = 12;
            this.numPullSkuRate.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numPullSkuRate.ValueChanged += new System.EventHandler(this.numPullSkuRate_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLoginMsg);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account";
            // 
            // lblLoginMsg
            // 
            this.lblLoginMsg.AutoSize = true;
            this.lblLoginMsg.Location = new System.Drawing.Point(8, 72);
            this.lblLoginMsg.Name = "lblLoginMsg";
            this.lblLoginMsg.Size = new System.Drawing.Size(0, 13);
            this.lblLoginMsg.TabIndex = 9;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(70, 67);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(172, 23);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(70, 40);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(172, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Username:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(70, 13);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(172, 20);
            this.txtUsername.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.AccessibleName = "localEvents";
            this.tabPage3.Controls.Add(this.tableLayoutPanel2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(778, 369);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Local Events";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.eventGrid, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(772, 363);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // eventGrid
            // 
            this.eventGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.eventGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.eventGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eventGrid.Location = new System.Drawing.Point(3, 39);
            this.eventGrid.Name = "eventGrid";
            this.eventGrid.ReadOnly = true;
            this.eventGrid.Size = new System.Drawing.Size(766, 321);
            this.eventGrid.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnUpdateLocalEvents);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.numDTId);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.numEventId);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cboLines);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 30);
            this.panel1.TabIndex = 4;
            // 
            // btnUpdateLocalEvents
            // 
            this.btnUpdateLocalEvents.Location = new System.Drawing.Point(688, 4);
            this.btnUpdateLocalEvents.Name = "btnUpdateLocalEvents";
            this.btnUpdateLocalEvents.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateLocalEvents.TabIndex = 1;
            this.btnUpdateLocalEvents.Text = "Refresh Grid";
            this.btnUpdateLocalEvents.UseVisualStyleBackColor = true;
            this.btnUpdateLocalEvents.Click += new System.EventHandler(this.btnUpdateLocalEvents_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Sync";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numDTId
            // 
            this.numDTId.Location = new System.Drawing.Point(337, 6);
            this.numDTId.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numDTId.Name = "numDTId";
            this.numDTId.Size = new System.Drawing.Size(120, 20);
            this.numDTId.TabIndex = 4;
            this.numDTId.ValueChanged += new System.EventHandler(this.numDTId_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(294, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "DT Id:";
            // 
            // numEventId
            // 
            this.numEventId.Location = new System.Drawing.Point(168, 7);
            this.numEventId.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numEventId.Name = "numEventId";
            this.numEventId.Size = new System.Drawing.Size(120, 20);
            this.numEventId.TabIndex = 2;
            this.numEventId.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Id:";
            // 
            // cboLines
            // 
            this.cboLines.FormattingEnabled = true;
            this.cboLines.Location = new System.Drawing.Point(4, 6);
            this.cboLines.Name = "cboLines";
            this.cboLines.Size = new System.Drawing.Size(121, 21);
            this.cboLines.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.AccessibleName = "localCounters";
            this.tabPage4.Controls.Add(this.tableLayoutPanel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(778, 369);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Finished Goods";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gridLocalCounters, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.641873F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.35812F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(772, 363);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.btnUpdateLocalCounters);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(766, 28);
            this.panel2.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(607, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Sync";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnUpdateLocalCounters
            // 
            this.btnUpdateLocalCounters.Location = new System.Drawing.Point(688, 3);
            this.btnUpdateLocalCounters.Name = "btnUpdateLocalCounters";
            this.btnUpdateLocalCounters.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateLocalCounters.TabIndex = 6;
            this.btnUpdateLocalCounters.Text = "Refresh Grid";
            this.btnUpdateLocalCounters.UseVisualStyleBackColor = true;
            this.btnUpdateLocalCounters.Click += new System.EventHandler(this.btnUpdateLocalCounters_Click);
            // 
            // gridLocalCounters
            // 
            this.gridLocalCounters.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLocalCounters.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridLocalCounters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridLocalCounters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLocalCounters.Location = new System.Drawing.Point(3, 38);
            this.gridLocalCounters.Name = "gridLocalCounters";
            this.gridLocalCounters.ReadOnly = true;
            this.gridLocalCounters.Size = new System.Drawing.Size(766, 322);
            this.gridLocalCounters.TabIndex = 4;
            // 
            // tabPage6
            // 
            this.tabPage6.AccessibleName = "pieceCounts";
            this.tabPage6.Controls.Add(this.tableLayoutPanel4);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(778, 369);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Piece Counts";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.gridPieceCount, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.641873F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.35812F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(772, 363);
            this.tableLayoutPanel4.TabIndex = 9;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button7);
            this.panel4.Controls.Add(this.btnUpdatePieceCounts);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(766, 28);
            this.panel4.TabIndex = 7;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(607, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "Sync";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // btnUpdatePieceCounts
            // 
            this.btnUpdatePieceCounts.Location = new System.Drawing.Point(688, 3);
            this.btnUpdatePieceCounts.Name = "btnUpdatePieceCounts";
            this.btnUpdatePieceCounts.Size = new System.Drawing.Size(75, 23);
            this.btnUpdatePieceCounts.TabIndex = 6;
            this.btnUpdatePieceCounts.Text = "Refresh Grid";
            this.btnUpdatePieceCounts.UseVisualStyleBackColor = true;
            this.btnUpdatePieceCounts.Click += new System.EventHandler(this.btnUpdatePieceCounts_Click);
            // 
            // gridPieceCount
            // 
            this.gridPieceCount.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridPieceCount.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.gridPieceCount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPieceCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPieceCount.Location = new System.Drawing.Point(3, 38);
            this.gridPieceCount.Name = "gridPieceCount";
            this.gridPieceCount.ReadOnly = true;
            this.gridPieceCount.Size = new System.Drawing.Size(766, 322);
            this.gridPieceCount.TabIndex = 4;
            // 
            // tbDiagnosis
            // 
            this.tbDiagnosis.AccessibleName = "diagnosis";
            this.tbDiagnosis.Controls.Add(this.tableLayoutPanel7);
            this.tbDiagnosis.Location = new System.Drawing.Point(4, 22);
            this.tbDiagnosis.Name = "tbDiagnosis";
            this.tbDiagnosis.Padding = new System.Windows.Forms.Padding(3);
            this.tbDiagnosis.Size = new System.Drawing.Size(778, 369);
            this.tbDiagnosis.TabIndex = 6;
            this.tbDiagnosis.Text = "Diagnosis";
            this.tbDiagnosis.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.pnlDiagnosisResult, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.pnlDiagnosis, 0, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.641873F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.35812F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(772, 363);
            this.tableLayoutPanel7.TabIndex = 10;
            // 
            // pnlDiagnosisResult
            // 
            this.pnlDiagnosisResult.Controls.Add(this.btnRunOverlappingDTEvtChk);
            this.pnlDiagnosisResult.Controls.Add(this.lblOverlappingDTEventsChkResult);
            this.pnlDiagnosisResult.Controls.Add(this.cbxDiagnosisLine);
            this.pnlDiagnosisResult.Controls.Add(this.tableLayoutPanel5);
            this.pnlDiagnosisResult.Controls.Add(this.btnRunOrphanDTEvtChk);
            this.pnlDiagnosisResult.Controls.Add(this.lblOrphanDTEventsChkResult);
            this.pnlDiagnosisResult.Location = new System.Drawing.Point(3, 38);
            this.pnlDiagnosisResult.Name = "pnlDiagnosisResult";
            this.pnlDiagnosisResult.Size = new System.Drawing.Size(766, 322);
            this.pnlDiagnosisResult.TabIndex = 8;
            // 
            // btnRunOverlappingDTEvtChk
            // 
            this.btnRunOverlappingDTEvtChk.Location = new System.Drawing.Point(475, 4);
            this.btnRunOverlappingDTEvtChk.Name = "btnRunOverlappingDTEvtChk";
            this.btnRunOverlappingDTEvtChk.Size = new System.Drawing.Size(225, 23);
            this.btnRunOverlappingDTEvtChk.TabIndex = 2;
            this.btnRunOverlappingDTEvtChk.Text = "Run Overlapping DT Events Check";
            this.btnRunOverlappingDTEvtChk.UseVisualStyleBackColor = true;
            this.btnRunOverlappingDTEvtChk.Click += new System.EventHandler(this.btnRunOverlappingDTEvtChk_Click);
            // 
            // lblOverlappingDTEventsChkResult
            // 
            this.lblOverlappingDTEventsChkResult.AutoSize = true;
            this.lblOverlappingDTEventsChkResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverlappingDTEventsChkResult.Location = new System.Drawing.Point(706, 7);
            this.lblOverlappingDTEventsChkResult.Name = "lblOverlappingDTEventsChkResult";
            this.lblOverlappingDTEventsChkResult.Size = new System.Drawing.Size(53, 15);
            this.lblOverlappingDTEventsChkResult.TabIndex = 3;
            this.lblOverlappingDTEventsChkResult.Text = "Pending";
            // 
            // cbxDiagnosisLine
            // 
            this.cbxDiagnosisLine.FormattingEnabled = true;
            this.cbxDiagnosisLine.Location = new System.Drawing.Point(3, 3);
            this.cbxDiagnosisLine.Name = "cbxDiagnosisLine";
            this.cbxDiagnosisLine.Size = new System.Drawing.Size(121, 21);
            this.cbxDiagnosisLine.TabIndex = 2;
            this.cbxDiagnosisLine.SelectedIndexChanged += new System.EventHandler(this.cbxDiagnosisLine_SelectedIndexChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.pnlDiagnosisControls, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.grdCountAccuracyData, 0, 1);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 30);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0346F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.9654F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(760, 289);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // pnlDiagnosisControls
            // 
            this.pnlDiagnosisControls.Controls.Add(this.btnRunAccuracyChk);
            this.pnlDiagnosisControls.Controls.Add(this.cbxAccuracyElement);
            this.pnlDiagnosisControls.Controls.Add(this.label5);
            this.pnlDiagnosisControls.Controls.Add(this.dtAccuracyToDate);
            this.pnlDiagnosisControls.Controls.Add(this.label4);
            this.pnlDiagnosisControls.Controls.Add(this.dtAccuracyFromDate);
            this.pnlDiagnosisControls.Location = new System.Drawing.Point(0, 0);
            this.pnlDiagnosisControls.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDiagnosisControls.Name = "pnlDiagnosisControls";
            this.pnlDiagnosisControls.Size = new System.Drawing.Size(760, 28);
            this.pnlDiagnosisControls.TabIndex = 3;
            // 
            // btnRunAccuracyChk
            // 
            this.btnRunAccuracyChk.Location = new System.Drawing.Point(473, 3);
            this.btnRunAccuracyChk.Name = "btnRunAccuracyChk";
            this.btnRunAccuracyChk.Size = new System.Drawing.Size(75, 23);
            this.btnRunAccuracyChk.TabIndex = 8;
            this.btnRunAccuracyChk.Text = "Run";
            this.btnRunAccuracyChk.UseVisualStyleBackColor = true;
            this.btnRunAccuracyChk.Click += new System.EventHandler(this.btnRunAccuracyChk_Click);
            // 
            // cbxAccuracyElement
            // 
            this.cbxAccuracyElement.FormattingEnabled = true;
            this.cbxAccuracyElement.Location = new System.Drawing.Point(0, 3);
            this.cbxAccuracyElement.Name = "cbxAccuracyElement";
            this.cbxAccuracyElement.Size = new System.Drawing.Size(121, 21);
            this.cbxAccuracyElement.TabIndex = 5;
            this.cbxAccuracyElement.SelectedIndexChanged += new System.EventHandler(this.cbxAccuracyElement_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(278, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "To";
            // 
            // dtAccuracyToDate
            // 
            this.dtAccuracyToDate.CustomFormat = "MM/dd/yyyy";
            this.dtAccuracyToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAccuracyToDate.Location = new System.Drawing.Point(320, 3);
            this.dtAccuracyToDate.Name = "dtAccuracyToDate";
            this.dtAccuracyToDate.Size = new System.Drawing.Size(89, 20);
            this.dtAccuracyToDate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(131, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "From";
            // 
            // dtAccuracyFromDate
            // 
            this.dtAccuracyFromDate.CustomFormat = "MM/dd/yyyy";
            this.dtAccuracyFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtAccuracyFromDate.Location = new System.Drawing.Point(173, 3);
            this.dtAccuracyFromDate.Name = "dtAccuracyFromDate";
            this.dtAccuracyFromDate.Size = new System.Drawing.Size(89, 20);
            this.dtAccuracyFromDate.TabIndex = 0;
            // 
            // grdCountAccuracyData
            // 
            this.grdCountAccuracyData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdCountAccuracyData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.grdCountAccuracyData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdCountAccuracyData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCountAccuracyData.Location = new System.Drawing.Point(0, 28);
            this.grdCountAccuracyData.Margin = new System.Windows.Forms.Padding(0);
            this.grdCountAccuracyData.Name = "grdCountAccuracyData";
            this.grdCountAccuracyData.ReadOnly = true;
            this.grdCountAccuracyData.Size = new System.Drawing.Size(760, 261);
            this.grdCountAccuracyData.TabIndex = 4;
            // 
            // btnRunOrphanDTEvtChk
            // 
            this.btnRunOrphanDTEvtChk.Location = new System.Drawing.Point(135, 3);
            this.btnRunOrphanDTEvtChk.Name = "btnRunOrphanDTEvtChk";
            this.btnRunOrphanDTEvtChk.Size = new System.Drawing.Size(225, 23);
            this.btnRunOrphanDTEvtChk.TabIndex = 0;
            this.btnRunOrphanDTEvtChk.Text = "Run Orphan DT Events Check";
            this.btnRunOrphanDTEvtChk.UseVisualStyleBackColor = true;
            this.btnRunOrphanDTEvtChk.Click += new System.EventHandler(this.btnRunOrphanDTEvtChk_Click);
            // 
            // lblOrphanDTEventsChkResult
            // 
            this.lblOrphanDTEventsChkResult.AutoSize = true;
            this.lblOrphanDTEventsChkResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrphanDTEventsChkResult.Location = new System.Drawing.Point(366, 6);
            this.lblOrphanDTEventsChkResult.Name = "lblOrphanDTEventsChkResult";
            this.lblOrphanDTEventsChkResult.Size = new System.Drawing.Size(53, 15);
            this.lblOrphanDTEventsChkResult.TabIndex = 1;
            this.lblOrphanDTEventsChkResult.Text = "Pending";
            // 
            // pnlDiagnosis
            // 
            this.pnlDiagnosis.Controls.Add(this.lblDiagnosisLoginMsg);
            this.pnlDiagnosis.Controls.Add(this.label6);
            this.pnlDiagnosis.Controls.Add(this.txtDiagnosisPassword);
            this.pnlDiagnosis.Controls.Add(this.btnViewDiagnosis);
            this.pnlDiagnosis.Location = new System.Drawing.Point(3, 3);
            this.pnlDiagnosis.Name = "pnlDiagnosis";
            this.pnlDiagnosis.Size = new System.Drawing.Size(766, 28);
            this.pnlDiagnosis.TabIndex = 7;
            // 
            // lblDiagnosisLoginMsg
            // 
            this.lblDiagnosisLoginMsg.AutoSize = true;
            this.lblDiagnosisLoginMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosisLoginMsg.Location = new System.Drawing.Point(257, 7);
            this.lblDiagnosisLoginMsg.Name = "lblDiagnosisLoginMsg";
            this.lblDiagnosisLoginMsg.Size = new System.Drawing.Size(0, 15);
            this.lblDiagnosisLoginMsg.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "Password:";
            // 
            // txtDiagnosisPassword
            // 
            this.txtDiagnosisPassword.Location = new System.Drawing.Point(70, 4);
            this.txtDiagnosisPassword.Name = "txtDiagnosisPassword";
            this.txtDiagnosisPassword.PasswordChar = '*';
            this.txtDiagnosisPassword.Size = new System.Drawing.Size(100, 20);
            this.txtDiagnosisPassword.TabIndex = 8;
            // 
            // btnViewDiagnosis
            // 
            this.btnViewDiagnosis.Location = new System.Drawing.Point(178, 3);
            this.btnViewDiagnosis.Name = "btnViewDiagnosis";
            this.btnViewDiagnosis.Size = new System.Drawing.Size(75, 23);
            this.btnViewDiagnosis.TabIndex = 9;
            this.btnViewDiagnosis.Text = "Show";
            this.btnViewDiagnosis.UseVisualStyleBackColor = true;
            this.btnViewDiagnosis.Click += new System.EventHandler(this.btnViewDiagnosis_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(786, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.resyncToolStripMenuItem,
            this.readToolStripMenuItem,
            this.testDBToolStripMenuItem,
            this.cleanDBToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.testAlarmToolStripMenuItem,
            this.toolStripSeparator1,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.startToolStripMenuItem.Text = "Stop";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // resyncToolStripMenuItem
            // 
            this.resyncToolStripMenuItem.Name = "resyncToolStripMenuItem";
            this.resyncToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.resyncToolStripMenuItem.Text = "Resync";
            this.resyncToolStripMenuItem.Click += new System.EventHandler(this.resyncToolStripMenuItem_Click);
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.asyncToolStripMenuItem});
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.readToolStripMenuItem.Text = "Read";
            // 
            // asyncToolStripMenuItem
            // 
            this.asyncToolStripMenuItem.Name = "asyncToolStripMenuItem";
            this.asyncToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.asyncToolStripMenuItem.Text = "Async";
            this.asyncToolStripMenuItem.Click += new System.EventHandler(this.asyncToolStripMenuItem_Click);
            // 
            // testDBToolStripMenuItem
            // 
            this.testDBToolStripMenuItem.Name = "testDBToolStripMenuItem";
            this.testDBToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            // 
            // cleanDBToolStripMenuItem
            // 
            this.cleanDBToolStripMenuItem.Name = "cleanDBToolStripMenuItem";
            this.cleanDBToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.cleanDBToolStripMenuItem.Text = "Clean DB";
            this.cleanDBToolStripMenuItem.Click += new System.EventHandler(this.cleanDBToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // testAlarmToolStripMenuItem
            // 
            this.testAlarmToolStripMenuItem.Name = "testAlarmToolStripMenuItem";
            this.testAlarmToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.testAlarmToolStripMenuItem.Text = "Test Nodes";
            this.testAlarmToolStripMenuItem.Click += new System.EventHandler(this.testAlarmToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Thrive Logger is running...";
            this.notifyIcon1.BalloonTipTitle = "Thrive Logger";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Thrive Com";
            this.notifyIcon1.Visible = true;
            // 
            // SQLiteCleanup
            // 
            this.SQLiteCleanup.Interval = 86400000;
            this.SQLiteCleanup.Tick += new System.EventHandler(this.SQLiteCleanup_Tick);
            // 
            // statusCheckTimer
            // 
            this.statusCheckTimer.Enabled = true;
            this.statusCheckTimer.Interval = 60000;
            this.statusCheckTimer.Tick += new System.EventHandler(this.statusCheckTimer_Tick);
            // 
            // ledBulb1
            // 
            this.ledBulb1.Location = new System.Drawing.Point(761, 1);
            this.ledBulb1.Name = "ledBulb1";
            this.ledBulb1.On = false;
            this.ledBulb1.Size = new System.Drawing.Size(24, 23);
            this.ledBulb1.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 422);
            this.Controls.Add(this.ledBulb1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.Shown += new System.EventHandler(this.mainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lineGridView)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numCountUpdateRate)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeoutTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeoutConnect)).EndInit();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numReadRate)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numPullSkuRate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDTId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEventId)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLocalCounters)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridPieceCount)).EndInit();
            this.tbDiagnosis.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.pnlDiagnosisResult.ResumeLayout(false);
            this.pnlDiagnosisResult.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.pnlDiagnosisControls.ResumeLayout(false);
            this.pnlDiagnosisControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCountAccuracyData)).EndInit();
            this.pnlDiagnosis.ResumeLayout(false);
            this.pnlDiagnosis.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView lineGridView;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resyncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label lblLoginMsg;
        private System.Windows.Forms.DataGridView logGridView;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asyncToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView eventGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboLines;
        private System.Windows.Forms.NumericUpDown numEventId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numDTId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView gridLocalCounters;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.NumericUpDown numReadRate;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numTimeoutTransaction;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown numTimeoutConnect;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.NumericUpDown numCountUpdateRate;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.NumericUpDown numPullSkuRate;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSaveLineFilter;
        private System.Windows.Forms.CheckedListBox filterList;
        private System.Windows.Forms.Timer SQLiteCleanup;
        private System.Windows.Forms.ToolStripMenuItem cleanDBToolStripMenuItem;
        private System.Windows.Forms.Timer statusCheckTimer;
        private System.Windows.Forms.ToolStripMenuItem testAlarmToolStripMenuItem;
        private System.Windows.Forms.Button btnUpdateLocalCounters;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnUpdateLocalEvents;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnUpdatePieceCounts;
        private System.Windows.Forms.DataGridView gridPieceCount;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TabPage tbDiagnosis;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Panel pnlDiagnosis;
        private System.Windows.Forms.Button btnViewDiagnosis;
        private System.Windows.Forms.Panel pnlDiagnosisResult;
        private System.Windows.Forms.Label lblOrphanDTEventsChkResult;
        private System.Windows.Forms.Button btnRunOrphanDTEvtChk;
        private System.Windows.Forms.ComboBox cbxDiagnosisLine;
        private System.Windows.Forms.Panel pnlDiagnosisControls;
        private System.Windows.Forms.Button btnRunOverlappingDTEvtChk;
        private System.Windows.Forms.Label lblOverlappingDTEventsChkResult;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.DateTimePicker dtAccuracyFromDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtAccuracyToDate;
        private System.Windows.Forms.ComboBox cbxAccuracyElement;
        private System.Windows.Forms.Button btnRunAccuracyChk;
        private System.Windows.Forms.DataGridView grdCountAccuracyData;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDiagnosisPassword;
        private System.Windows.Forms.Label lblDiagnosisLoginMsg;
        private LedBulb ledBulb1;
    }
}

