namespace SolverApp
{
	partial class frmMain
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
      this.tmrStatus = new System.Windows.Forms.Timer(this.components);
      this.grpSolverControls = new System.Windows.Forms.GroupBox();
      this.BtnNextStep = new System.Windows.Forms.Button();
      this.btnWriteSeq = new System.Windows.Forms.Button();
      this.groupEngineType = new System.Windows.Forms.GroupBox();
      this.radioNonLinear = new System.Windows.Forms.RadioButton();
      this.radioLinear = new System.Windows.Forms.RadioButton();
      this.groupDataRange = new System.Windows.Forms.GroupBox();
      this.txtRange = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.groupOptimize = new System.Windows.Forms.GroupBox();
      this.opnExperimental = new System.Windows.Forms.RadioButton();
      this.opnSumSquares = new System.Windows.Forms.RadioButton();
      this.groupErrorCalc = new System.Windows.Forms.GroupBox();
      this.chkXscaler = new System.Windows.Forms.CheckBox();
      this.txtXFactor = new System.Windows.Forms.TextBox();
      this.txtYFactor = new System.Windows.Forms.TextBox();
      this.txtZFactor = new System.Windows.Forms.TextBox();
      this.chkYscaler = new System.Windows.Forms.CheckBox();
      this.chkZscaler = new System.Windows.Forms.CheckBox();
      this.btnAxAt_uncheckall = new System.Windows.Forms.Button();
      this.btnAxAt_checkall = new System.Windows.Forms.Button();
      this.btnDH_uncheckall = new System.Windows.Forms.Button();
      this.btnDH_checkall = new System.Windows.Forms.Button();
      this.chkDebugMode = new System.Windows.Forms.CheckBox();
      this.btnRunSolverFile = new System.Windows.Forms.Button();
      this.chkSolveCompTables = new System.Windows.Forms.CheckBox();
      this.btnSaveConfig = new System.Windows.Forms.Button();
      this.btnSaveData = new System.Windows.Forms.Button();
      this.grpCompTable = new System.Windows.Forms.GroupBox();
      this.btnNullCompTables = new System.Windows.Forms.Button();
      this.btnCompChangeLimits = new System.Windows.Forms.Button();
      this.chkCompUseLimits = new System.Windows.Forms.CheckBox();
      this.txtCompAxisLimits = new System.Windows.Forms.TextBox();
      this.lstCompTables = new System.Windows.Forms.CheckedListBox();
      this.btnCTuncheckall = new System.Windows.Forms.Button();
      this.btnCTcheckall = new System.Windows.Forms.Button();
      this.chkCTYaw = new System.Windows.Forms.CheckBox();
      this.chkCTPitch = new System.Windows.Forms.CheckBox();
      this.chkCTRoll = new System.Windows.Forms.CheckBox();
      this.chkCTZ = new System.Windows.Forms.CheckBox();
      this.chkCTY = new System.Windows.Forms.CheckBox();
      this.chkCTX = new System.Windows.Forms.CheckBox();
      this.btnSolveOnce = new System.Windows.Forms.Button();
      this.label6 = new System.Windows.Forms.Label();
      this.label5 = new System.Windows.Forms.Label();
      this.lstAxisAttribs = new System.Windows.Forms.CheckedListBox();
      this.lstDHAttribs = new System.Windows.Forms.CheckedListBox();
      this.btnSaveAll = new System.Windows.Forms.Button();
      this.chkSolveAxAttribs = new System.Windows.Forms.CheckBox();
      this.chkSolveDHLinks = new System.Windows.Forms.CheckBox();
      this.btnSolveIt = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.lblFileConfigNom = new System.Windows.Forms.LinkLabel();
      this.btnUnPause = new System.Windows.Forms.Button();
      this.lblRunTimeVal = new System.Windows.Forms.Label();
      this.lblRunTime = new System.Windows.Forms.Label();
      this.lblSeqStepValue = new System.Windows.Forms.Label();
      this.lblSeqStep = new System.Windows.Forms.Label();
      this.GenSolvFile = new System.Windows.Forms.Label();
      this.SolverFile = new System.Windows.Forms.Label();
      this.DataFile = new System.Windows.Forms.Label();
      this.AdditionalFile = new System.Windows.Forms.Label();
      this.ConfigFile = new System.Windows.Forms.Label();
      this.NomConfig = new System.Windows.Forms.Label();
      this.lblFileSolver = new System.Windows.Forms.LinkLabel();
      this.lblFileGenerated = new System.Windows.Forms.LinkLabel();
      this.tbMaxIterations = new System.Windows.Forms.TextBox();
      this.lblMaxIterations = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.txtTimeOut = new System.Windows.Forms.TextBox();
      this.lblFileAdditional = new System.Windows.Forms.LinkLabel();
      this.chkAutoScale = new System.Windows.Forms.CheckBox();
      this.txtStepSize = new System.Windows.Forms.TextBox();
      this.btnAbort = new System.Windows.Forms.Button();
      this.pgStatus = new System.Windows.Forms.ProgressBar();
      this.lblFileData = new System.Windows.Forms.LinkLabel();
      this.lblFileConfig = new System.Windows.Forms.LinkLabel();
      this.txtSolverStatus = new System.Windows.Forms.TextBox();
      this.lblSolverStatus = new System.Windows.Forms.Label();
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileConfigNom = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuReloadNomConfig = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileConfig = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuFileData = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuArchiveConfigFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSolverFile = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSetGenerated = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuSaveProject = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuLoadProject = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFileAddConfigFile = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
      this.utlitiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.createCLEHeaderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.createCBCcodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.lblElementsToSolve = new System.Windows.Forms.Label();
      this.grpSolverControls.SuspendLayout();
      this.groupEngineType.SuspendLayout();
      this.groupDataRange.SuspendLayout();
      this.groupOptimize.SuspendLayout();
      this.groupErrorCalc.SuspendLayout();
      this.grpCompTable.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // tmrStatus
      // 
      this.tmrStatus.Enabled = true;
      this.tmrStatus.Interval = 50;
      this.tmrStatus.Tick += new System.EventHandler(this.tmrStatus_Tick);
      // 
      // grpSolverControls
      // 
      this.grpSolverControls.Controls.Add(this.BtnNextStep);
      this.grpSolverControls.Controls.Add(this.btnWriteSeq);
      this.grpSolverControls.Controls.Add(this.groupEngineType);
      this.grpSolverControls.Controls.Add(this.groupDataRange);
      this.grpSolverControls.Controls.Add(this.groupOptimize);
      this.grpSolverControls.Controls.Add(this.groupErrorCalc);
      this.grpSolverControls.Controls.Add(this.btnAxAt_uncheckall);
      this.grpSolverControls.Controls.Add(this.btnAxAt_checkall);
      this.grpSolverControls.Controls.Add(this.btnDH_uncheckall);
      this.grpSolverControls.Controls.Add(this.btnDH_checkall);
      this.grpSolverControls.Controls.Add(this.chkDebugMode);
      this.grpSolverControls.Controls.Add(this.btnRunSolverFile);
      this.grpSolverControls.Controls.Add(this.chkSolveCompTables);
      this.grpSolverControls.Controls.Add(this.btnSaveConfig);
      this.grpSolverControls.Controls.Add(this.btnSaveData);
      this.grpSolverControls.Controls.Add(this.grpCompTable);
      this.grpSolverControls.Controls.Add(this.btnSolveOnce);
      this.grpSolverControls.Controls.Add(this.label6);
      this.grpSolverControls.Controls.Add(this.label5);
      this.grpSolverControls.Controls.Add(this.lstAxisAttribs);
      this.grpSolverControls.Controls.Add(this.lstDHAttribs);
      this.grpSolverControls.Controls.Add(this.btnSaveAll);
      this.grpSolverControls.Controls.Add(this.chkSolveAxAttribs);
      this.grpSolverControls.Controls.Add(this.chkSolveDHLinks);
      this.grpSolverControls.Controls.Add(this.btnSolveIt);
      this.grpSolverControls.Location = new System.Drawing.Point(12, 27);
      this.grpSolverControls.Name = "grpSolverControls";
      this.grpSolverControls.Size = new System.Drawing.Size(900, 581);
      this.grpSolverControls.TabIndex = 6;
      this.grpSolverControls.TabStop = false;
      this.grpSolverControls.Text = "Solver Controls";
      this.grpSolverControls.Enter += new System.EventHandler(this.grpSolverControls_Enter);
      // 
      // BtnNextStep
      // 
      this.BtnNextStep.Location = new System.Drawing.Point(16, 489);
      this.BtnNextStep.Name = "BtnNextStep";
      this.BtnNextStep.Size = new System.Drawing.Size(153, 66);
      this.BtnNextStep.TabIndex = 48;
      this.BtnNextStep.Text = "Add selections to next step of generated file";
      this.BtnNextStep.UseVisualStyleBackColor = true;
      this.BtnNextStep.Click += new System.EventHandler(this.BtnNextStep_Click);
      // 
      // btnWriteSeq
      // 
      this.btnWriteSeq.Location = new System.Drawing.Point(16, 417);
      this.btnWriteSeq.Name = "btnWriteSeq";
      this.btnWriteSeq.Size = new System.Drawing.Size(153, 66);
      this.btnWriteSeq.TabIndex = 47;
      this.btnWriteSeq.Text = "Generate New Solver File";
      this.btnWriteSeq.UseVisualStyleBackColor = true;
      this.btnWriteSeq.Click += new System.EventHandler(this.btnWriteSeq_Click);
      // 
      // groupEngineType
      // 
      this.groupEngineType.Controls.Add(this.radioNonLinear);
      this.groupEngineType.Controls.Add(this.radioLinear);
      this.groupEngineType.Location = new System.Drawing.Point(694, 317);
      this.groupEngineType.Name = "groupEngineType";
      this.groupEngineType.Size = new System.Drawing.Size(200, 72);
      this.groupEngineType.TabIndex = 46;
      this.groupEngineType.TabStop = false;
      this.groupEngineType.Text = "Solver Engine Type";
      // 
      // radioNonLinear
      // 
      this.radioNonLinear.AutoSize = true;
      this.radioNonLinear.Location = new System.Drawing.Point(9, 44);
      this.radioNonLinear.Name = "radioNonLinear";
      this.radioNonLinear.Size = new System.Drawing.Size(74, 17);
      this.radioNonLinear.TabIndex = 1;
      this.radioNonLinear.Text = "NonLinear";
      this.radioNonLinear.UseVisualStyleBackColor = true;
      // 
      // radioLinear
      // 
      this.radioLinear.AutoSize = true;
      this.radioLinear.Checked = true;
      this.radioLinear.Location = new System.Drawing.Point(9, 20);
      this.radioLinear.Name = "radioLinear";
      this.radioLinear.Size = new System.Drawing.Size(54, 17);
      this.radioLinear.TabIndex = 0;
      this.radioLinear.TabStop = true;
      this.radioLinear.Text = "Linear";
      this.radioLinear.UseVisualStyleBackColor = true;
      // 
      // groupDataRange
      // 
      this.groupDataRange.Controls.Add(this.txtRange);
      this.groupDataRange.Controls.Add(this.label3);
      this.groupDataRange.Controls.Add(this.label4);
      this.groupDataRange.Location = new System.Drawing.Point(694, 197);
      this.groupDataRange.Name = "groupDataRange";
      this.groupDataRange.Size = new System.Drawing.Size(200, 100);
      this.groupDataRange.TabIndex = 45;
      this.groupDataRange.TabStop = false;
      this.groupDataRange.Text = "Data Range To Solve";
      // 
      // txtRange
      // 
      this.txtRange.Location = new System.Drawing.Point(6, 55);
      this.txtRange.Name = "txtRange";
      this.txtRange.Size = new System.Drawing.Size(172, 20);
      this.txtRange.TabIndex = 15;
      this.txtRange.Text = "All";
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(6, 33);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(64, 13);
      this.label3.TabIndex = 42;
      this.label3.Text = "No Commas";
      this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(6, 16);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(160, 13);
      this.label4.TabIndex = 19;
      this.label4.Text = "Range (e.g. \"123-129 234-456\")";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // groupOptimize
      // 
      this.groupOptimize.Controls.Add(this.opnExperimental);
      this.groupOptimize.Controls.Add(this.opnSumSquares);
      this.groupOptimize.Location = new System.Drawing.Point(694, 109);
      this.groupOptimize.Name = "groupOptimize";
      this.groupOptimize.Size = new System.Drawing.Size(200, 70);
      this.groupOptimize.TabIndex = 44;
      this.groupOptimize.TabStop = false;
      this.groupOptimize.Text = "Optimization";
      // 
      // opnExperimental
      // 
      this.opnExperimental.AutoSize = true;
      this.opnExperimental.Location = new System.Drawing.Point(6, 42);
      this.opnExperimental.Name = "opnExperimental";
      this.opnExperimental.Size = new System.Drawing.Size(85, 17);
      this.opnExperimental.TabIndex = 37;
      this.opnExperimental.Text = "Experimental";
      this.opnExperimental.UseVisualStyleBackColor = true;
      // 
      // opnSumSquares
      // 
      this.opnSumSquares.AutoSize = true;
      this.opnSumSquares.Checked = true;
      this.opnSumSquares.Location = new System.Drawing.Point(6, 19);
      this.opnSumSquares.Name = "opnSumSquares";
      this.opnSumSquares.Size = new System.Drawing.Size(172, 17);
      this.opnSumSquares.TabIndex = 35;
      this.opnSumSquares.TabStop = true;
      this.opnSumSquares.Text = "Minimize Sum Squares (default)";
      this.opnSumSquares.UseVisualStyleBackColor = true;
      // 
      // groupErrorCalc
      // 
      this.groupErrorCalc.Controls.Add(this.chkXscaler);
      this.groupErrorCalc.Controls.Add(this.txtXFactor);
      this.groupErrorCalc.Controls.Add(this.txtYFactor);
      this.groupErrorCalc.Controls.Add(this.txtZFactor);
      this.groupErrorCalc.Controls.Add(this.chkYscaler);
      this.groupErrorCalc.Controls.Add(this.chkZscaler);
      this.groupErrorCalc.Location = new System.Drawing.Point(694, 7);
      this.groupErrorCalc.Name = "groupErrorCalc";
      this.groupErrorCalc.Size = new System.Drawing.Size(200, 85);
      this.groupErrorCalc.TabIndex = 43;
      this.groupErrorCalc.TabStop = false;
      this.groupErrorCalc.Text = "Error Calculation";
      // 
      // chkXscaler
      // 
      this.chkXscaler.AutoSize = true;
      this.chkXscaler.Checked = true;
      this.chkXscaler.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkXscaler.Location = new System.Drawing.Point(6, 21);
      this.chkXscaler.Name = "chkXscaler";
      this.chkXscaler.Size = new System.Drawing.Size(66, 17);
      this.chkXscaler.TabIndex = 26;
      this.chkXscaler.Text = "X Scaler";
      this.chkXscaler.UseVisualStyleBackColor = true;
      this.chkXscaler.CheckedChanged += new System.EventHandler(this.chkXscaler_CheckedChanged);
      // 
      // txtXFactor
      // 
      this.txtXFactor.Location = new System.Drawing.Point(78, 19);
      this.txtXFactor.Name = "txtXFactor";
      this.txtXFactor.Size = new System.Drawing.Size(100, 20);
      this.txtXFactor.TabIndex = 9;
      this.txtXFactor.Text = "1.000";
      this.txtXFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtXFactor.Leave += new System.EventHandler(this.txtXFactor_Leave);
      // 
      // txtYFactor
      // 
      this.txtYFactor.Location = new System.Drawing.Point(78, 40);
      this.txtYFactor.Name = "txtYFactor";
      this.txtYFactor.Size = new System.Drawing.Size(100, 20);
      this.txtYFactor.TabIndex = 11;
      this.txtYFactor.Text = "1.000";
      this.txtYFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtYFactor.Leave += new System.EventHandler(this.txtYFactor_Leave);
      // 
      // txtZFactor
      // 
      this.txtZFactor.Location = new System.Drawing.Point(78, 60);
      this.txtZFactor.Name = "txtZFactor";
      this.txtZFactor.Size = new System.Drawing.Size(100, 20);
      this.txtZFactor.TabIndex = 12;
      this.txtZFactor.Text = "1.000";
      this.txtZFactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtZFactor.Leave += new System.EventHandler(this.txtZFactor_Leave);
      // 
      // chkYscaler
      // 
      this.chkYscaler.AutoSize = true;
      this.chkYscaler.Checked = true;
      this.chkYscaler.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkYscaler.Location = new System.Drawing.Point(6, 42);
      this.chkYscaler.Name = "chkYscaler";
      this.chkYscaler.Size = new System.Drawing.Size(66, 17);
      this.chkYscaler.TabIndex = 27;
      this.chkYscaler.Text = "Y Scaler";
      this.chkYscaler.UseVisualStyleBackColor = true;
      this.chkYscaler.CheckedChanged += new System.EventHandler(this.chkYscaler_CheckedChanged);
      // 
      // chkZscaler
      // 
      this.chkZscaler.AutoSize = true;
      this.chkZscaler.Checked = true;
      this.chkZscaler.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkZscaler.Location = new System.Drawing.Point(6, 62);
      this.chkZscaler.Name = "chkZscaler";
      this.chkZscaler.Size = new System.Drawing.Size(66, 17);
      this.chkZscaler.TabIndex = 28;
      this.chkZscaler.Text = "Z Scaler";
      this.chkZscaler.UseVisualStyleBackColor = true;
      this.chkZscaler.CheckedChanged += new System.EventHandler(this.chkZscaler_CheckedChanged);
      // 
      // btnAxAt_uncheckall
      // 
      this.btnAxAt_uncheckall.Location = new System.Drawing.Point(356, 542);
      this.btnAxAt_uncheckall.Name = "btnAxAt_uncheckall";
      this.btnAxAt_uncheckall.Size = new System.Drawing.Size(75, 23);
      this.btnAxAt_uncheckall.TabIndex = 41;
      this.btnAxAt_uncheckall.Text = "Uncheck All";
      this.btnAxAt_uncheckall.UseVisualStyleBackColor = true;
      this.btnAxAt_uncheckall.Click += new System.EventHandler(this.btnAxAt_uncheckall_Click);
      // 
      // btnAxAt_checkall
      // 
      this.btnAxAt_checkall.Location = new System.Drawing.Point(356, 513);
      this.btnAxAt_checkall.Name = "btnAxAt_checkall";
      this.btnAxAt_checkall.Size = new System.Drawing.Size(75, 23);
      this.btnAxAt_checkall.TabIndex = 40;
      this.btnAxAt_checkall.Text = "Check All";
      this.btnAxAt_checkall.UseVisualStyleBackColor = true;
      this.btnAxAt_checkall.Click += new System.EventHandler(this.btnAxAt_checkall_Click);
      // 
      // btnDH_uncheckall
      // 
      this.btnDH_uncheckall.Location = new System.Drawing.Point(229, 543);
      this.btnDH_uncheckall.Name = "btnDH_uncheckall";
      this.btnDH_uncheckall.Size = new System.Drawing.Size(75, 23);
      this.btnDH_uncheckall.TabIndex = 39;
      this.btnDH_uncheckall.Text = "Uncheck All";
      this.btnDH_uncheckall.UseVisualStyleBackColor = true;
      this.btnDH_uncheckall.Click += new System.EventHandler(this.btnDH_uncheckall_Click);
      // 
      // btnDH_checkall
      // 
      this.btnDH_checkall.Location = new System.Drawing.Point(229, 514);
      this.btnDH_checkall.Name = "btnDH_checkall";
      this.btnDH_checkall.Size = new System.Drawing.Size(75, 23);
      this.btnDH_checkall.TabIndex = 38;
      this.btnDH_checkall.Text = "Check All";
      this.btnDH_checkall.UseVisualStyleBackColor = true;
      this.btnDH_checkall.Click += new System.EventHandler(this.btnDH_checkall_Click);
      // 
      // chkDebugMode
      // 
      this.chkDebugMode.AutoSize = true;
      this.chkDebugMode.Location = new System.Drawing.Point(16, 294);
      this.chkDebugMode.Name = "chkDebugMode";
      this.chkDebugMode.Size = new System.Drawing.Size(112, 17);
      this.chkDebugMode.TabIndex = 34;
      this.chkDebugMode.Text = "Debug Mode Only";
      this.toolTip1.SetToolTip(this.chkDebugMode, "When checked the file selected by\r\nFile->Set Solver File\r\nwill be run, but solvin" +
        "g will not occur.\r\nA 2s delay between setups allows\r\nuser to determine if the so" +
        "lution does\r\nthe intended functions.");
      this.chkDebugMode.UseVisualStyleBackColor = true;
      // 
      // btnRunSolverFile
      // 
      this.btnRunSolverFile.Location = new System.Drawing.Point(16, 317);
      this.btnRunSolverFile.Name = "btnRunSolverFile";
      this.btnRunSolverFile.Size = new System.Drawing.Size(153, 38);
      this.btnRunSolverFile.TabIndex = 33;
      this.btnRunSolverFile.Text = "Run Solver File";
      this.toolTip1.SetToolTip(this.btnRunSolverFile, "Automation for solving.\r\nRuns the file selected by\r\nFile->Set Solver File\r\nmenu o" +
        "ption.");
      this.btnRunSolverFile.UseVisualStyleBackColor = true;
      this.btnRunSolverFile.Click += new System.EventHandler(this.btnRunSolverFile_Click);
      // 
      // chkSolveCompTables
      // 
      this.chkSolveCompTables.AutoSize = true;
      this.chkSolveCompTables.Location = new System.Drawing.Point(16, 63);
      this.chkSolveCompTables.Name = "chkSolveCompTables";
      this.chkSolveCompTables.Size = new System.Drawing.Size(158, 17);
      this.chkSolveCompTables.TabIndex = 32;
      this.chkSolveCompTables.Text = "Solve Compensation Tables";
      this.chkSolveCompTables.UseVisualStyleBackColor = true;
      this.chkSolveCompTables.CheckedChanged += new System.EventHandler(this.chkSolveCompTables_CheckedChanged);
      // 
      // btnSaveConfig
      // 
      this.btnSaveConfig.Location = new System.Drawing.Point(16, 81);
      this.btnSaveConfig.Name = "btnSaveConfig";
      this.btnSaveConfig.Size = new System.Drawing.Size(153, 26);
      this.btnSaveConfig.TabIndex = 31;
      this.btnSaveConfig.Text = "Save Config";
      this.btnSaveConfig.UseVisualStyleBackColor = true;
      this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
      // 
      // btnSaveData
      // 
      this.btnSaveData.Location = new System.Drawing.Point(16, 111);
      this.btnSaveData.Name = "btnSaveData";
      this.btnSaveData.Size = new System.Drawing.Size(153, 26);
      this.btnSaveData.TabIndex = 30;
      this.btnSaveData.Text = "Save Data";
      this.btnSaveData.UseVisualStyleBackColor = true;
      this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
      // 
      // grpCompTable
      // 
      this.grpCompTable.Controls.Add(this.btnNullCompTables);
      this.grpCompTable.Controls.Add(this.btnCompChangeLimits);
      this.grpCompTable.Controls.Add(this.chkCompUseLimits);
      this.grpCompTable.Controls.Add(this.txtCompAxisLimits);
      this.grpCompTable.Controls.Add(this.lstCompTables);
      this.grpCompTable.Controls.Add(this.btnCTuncheckall);
      this.grpCompTable.Controls.Add(this.btnCTcheckall);
      this.grpCompTable.Controls.Add(this.chkCTYaw);
      this.grpCompTable.Controls.Add(this.chkCTPitch);
      this.grpCompTable.Controls.Add(this.chkCTRoll);
      this.grpCompTable.Controls.Add(this.chkCTZ);
      this.grpCompTable.Controls.Add(this.chkCTY);
      this.grpCompTable.Controls.Add(this.chkCTX);
      this.grpCompTable.Enabled = false;
      this.grpCompTable.Location = new System.Drawing.Point(482, 20);
      this.grpCompTable.Name = "grpCompTable";
      this.grpCompTable.Size = new System.Drawing.Size(200, 413);
      this.grpCompTable.TabIndex = 29;
      this.grpCompTable.TabStop = false;
      this.grpCompTable.Text = "Comptable Elements";
      // 
      // btnNullCompTables
      // 
      this.btnNullCompTables.Location = new System.Drawing.Point(6, 363);
      this.btnNullCompTables.Name = "btnNullCompTables";
      this.btnNullCompTables.Size = new System.Drawing.Size(113, 24);
      this.btnNullCompTables.TabIndex = 38;
      this.btnNullCompTables.Text = "Null Comp Tables";
      this.toolTip1.SetToolTip(this.btnNullCompTables, "Nulls comptable with no warning!");
      this.btnNullCompTables.UseVisualStyleBackColor = true;
      this.btnNullCompTables.Click += new System.EventHandler(this.btnNullCompTables_Click);
      // 
      // btnCompChangeLimits
      // 
      this.btnCompChangeLimits.Location = new System.Drawing.Point(7, 334);
      this.btnCompChangeLimits.Name = "btnCompChangeLimits";
      this.btnCompChangeLimits.Size = new System.Drawing.Size(112, 23);
      this.btnCompChangeLimits.TabIndex = 37;
      this.btnCompChangeLimits.Text = "Change Axis Limits";
      this.btnCompChangeLimits.UseVisualStyleBackColor = true;
      this.btnCompChangeLimits.Click += new System.EventHandler(this.btnCompChangeLimits_Click);
      // 
      // chkCompUseLimits
      // 
      this.chkCompUseLimits.AutoSize = true;
      this.chkCompUseLimits.Checked = true;
      this.chkCompUseLimits.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCompUseLimits.Location = new System.Drawing.Point(6, 278);
      this.chkCompUseLimits.Name = "chkCompUseLimits";
      this.chkCompUseLimits.Size = new System.Drawing.Size(96, 17);
      this.chkCompUseLimits.TabIndex = 36;
      this.chkCompUseLimits.Text = "Use Axis Limits";
      this.chkCompUseLimits.UseVisualStyleBackColor = true;
      this.chkCompUseLimits.CheckedChanged += new System.EventHandler(this.chkCompUseLimits_CheckedChanged);
      // 
      // txtCompAxisLimits
      // 
      this.txtCompAxisLimits.Location = new System.Drawing.Point(7, 307);
      this.txtCompAxisLimits.Name = "txtCompAxisLimits";
      this.txtCompAxisLimits.Size = new System.Drawing.Size(175, 20);
      this.txtCompAxisLimits.TabIndex = 35;
      this.txtCompAxisLimits.Text = "all";
      // 
      // lstCompTables
      // 
      this.lstCompTables.CheckOnClick = true;
      this.lstCompTables.FormattingEnabled = true;
      this.lstCompTables.Location = new System.Drawing.Point(6, 82);
      this.lstCompTables.Name = "lstCompTables";
      this.lstCompTables.Size = new System.Drawing.Size(176, 169);
      this.lstCompTables.TabIndex = 34;
      this.lstCompTables.SelectedIndexChanged += new System.EventHandler(this.lstCompTables_SelectedIndexChanged);
      // 
      // btnCTuncheckall
      // 
      this.btnCTuncheckall.Location = new System.Drawing.Point(119, 49);
      this.btnCTuncheckall.Name = "btnCTuncheckall";
      this.btnCTuncheckall.Size = new System.Drawing.Size(75, 23);
      this.btnCTuncheckall.TabIndex = 8;
      this.btnCTuncheckall.Text = "Uncheck All";
      this.btnCTuncheckall.UseVisualStyleBackColor = true;
      this.btnCTuncheckall.Click += new System.EventHandler(this.btnCTuncheckall_Click);
      // 
      // btnCTcheckall
      // 
      this.btnCTcheckall.Location = new System.Drawing.Point(119, 18);
      this.btnCTcheckall.Name = "btnCTcheckall";
      this.btnCTcheckall.Size = new System.Drawing.Size(75, 23);
      this.btnCTcheckall.TabIndex = 7;
      this.btnCTcheckall.Text = "Check All";
      this.btnCTcheckall.UseVisualStyleBackColor = true;
      this.btnCTcheckall.Click += new System.EventHandler(this.btnCTcheckall_Click);
      // 
      // chkCTYaw
      // 
      this.chkCTYaw.AutoSize = true;
      this.chkCTYaw.Checked = true;
      this.chkCTYaw.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCTYaw.Location = new System.Drawing.Point(45, 59);
      this.chkCTYaw.Name = "chkCTYaw";
      this.chkCTYaw.Size = new System.Drawing.Size(36, 17);
      this.chkCTYaw.TabIndex = 6;
      this.chkCTYaw.Text = "rZ";
      this.chkCTYaw.UseVisualStyleBackColor = true;
      // 
      // chkCTPitch
      // 
      this.chkCTPitch.AutoSize = true;
      this.chkCTPitch.Checked = true;
      this.chkCTPitch.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCTPitch.Location = new System.Drawing.Point(45, 38);
      this.chkCTPitch.Name = "chkCTPitch";
      this.chkCTPitch.Size = new System.Drawing.Size(36, 17);
      this.chkCTPitch.TabIndex = 5;
      this.chkCTPitch.Text = "rY";
      this.chkCTPitch.UseVisualStyleBackColor = true;
      // 
      // chkCTRoll
      // 
      this.chkCTRoll.AutoSize = true;
      this.chkCTRoll.Checked = true;
      this.chkCTRoll.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCTRoll.Location = new System.Drawing.Point(45, 17);
      this.chkCTRoll.Name = "chkCTRoll";
      this.chkCTRoll.Size = new System.Drawing.Size(36, 17);
      this.chkCTRoll.TabIndex = 4;
      this.chkCTRoll.Text = "rX";
      this.chkCTRoll.UseVisualStyleBackColor = true;
      // 
      // chkCTZ
      // 
      this.chkCTZ.AutoSize = true;
      this.chkCTZ.Checked = true;
      this.chkCTZ.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCTZ.Location = new System.Drawing.Point(6, 59);
      this.chkCTZ.Name = "chkCTZ";
      this.chkCTZ.Size = new System.Drawing.Size(33, 17);
      this.chkCTZ.TabIndex = 3;
      this.chkCTZ.Text = "Z";
      this.chkCTZ.UseVisualStyleBackColor = true;
      // 
      // chkCTY
      // 
      this.chkCTY.AutoSize = true;
      this.chkCTY.Checked = true;
      this.chkCTY.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCTY.Location = new System.Drawing.Point(6, 38);
      this.chkCTY.Name = "chkCTY";
      this.chkCTY.Size = new System.Drawing.Size(33, 17);
      this.chkCTY.TabIndex = 2;
      this.chkCTY.Text = "Y";
      this.chkCTY.UseVisualStyleBackColor = true;
      // 
      // chkCTX
      // 
      this.chkCTX.AutoSize = true;
      this.chkCTX.Checked = true;
      this.chkCTX.CheckState = System.Windows.Forms.CheckState.Checked;
      this.chkCTX.Location = new System.Drawing.Point(6, 17);
      this.chkCTX.Name = "chkCTX";
      this.chkCTX.Size = new System.Drawing.Size(33, 17);
      this.chkCTX.TabIndex = 1;
      this.chkCTX.Text = "X";
      this.chkCTX.UseVisualStyleBackColor = true;
      // 
      // btnSolveOnce
      // 
      this.btnSolveOnce.Location = new System.Drawing.Point(16, 183);
      this.btnSolveOnce.Name = "btnSolveOnce";
      this.btnSolveOnce.Size = new System.Drawing.Size(153, 38);
      this.btnSolveOnce.TabIndex = 25;
      this.btnSolveOnce.Text = "Compute Fwd Solution from Current Config";
      this.btnSolveOnce.UseVisualStyleBackColor = true;
      this.btnSolveOnce.Click += new System.EventHandler(this.btnSolveOnce_Click);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Location = new System.Drawing.Point(353, 20);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(73, 13);
      this.label6.TabIndex = 24;
      this.label6.Text = "Axis Attributes";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Location = new System.Drawing.Point(226, 20);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(70, 13);
      this.label5.TabIndex = 23;
      this.label5.Text = "DH Attributes";
      // 
      // lstAxisAttribs
      // 
      this.lstAxisAttribs.CheckOnClick = true;
      this.lstAxisAttribs.FormattingEnabled = true;
      this.lstAxisAttribs.Location = new System.Drawing.Point(356, 38);
      this.lstAxisAttribs.Name = "lstAxisAttribs";
      this.lstAxisAttribs.Size = new System.Drawing.Size(120, 454);
      this.lstAxisAttribs.TabIndex = 22;
      // 
      // lstDHAttribs
      // 
      this.lstDHAttribs.CheckOnClick = true;
      this.lstDHAttribs.FormattingEnabled = true;
      this.lstDHAttribs.Location = new System.Drawing.Point(229, 38);
      this.lstDHAttribs.Name = "lstDHAttribs";
      this.lstDHAttribs.Size = new System.Drawing.Size(120, 454);
      this.lstDHAttribs.TabIndex = 21;
      // 
      // btnSaveAll
      // 
      this.btnSaveAll.Location = new System.Drawing.Point(16, 141);
      this.btnSaveAll.Name = "btnSaveAll";
      this.btnSaveAll.Size = new System.Drawing.Size(153, 38);
      this.btnSaveAll.TabIndex = 8;
      this.btnSaveAll.Text = "Save All";
      this.btnSaveAll.UseVisualStyleBackColor = true;
      this.btnSaveAll.Click += new System.EventHandler(this.btnSaveResults_Click);
      // 
      // chkSolveAxAttribs
      // 
      this.chkSolveAxAttribs.AutoSize = true;
      this.chkSolveAxAttribs.Location = new System.Drawing.Point(16, 40);
      this.chkSolveAxAttribs.Name = "chkSolveAxAttribs";
      this.chkSolveAxAttribs.Size = new System.Drawing.Size(185, 17);
      this.chkSolveAxAttribs.TabIndex = 7;
      this.chkSolveAxAttribs.Text = "Solve Machine Axis Relationships";
      this.chkSolveAxAttribs.UseVisualStyleBackColor = true;
      this.chkSolveAxAttribs.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
      // 
      // chkSolveDHLinks
      // 
      this.chkSolveDHLinks.AutoSize = true;
      this.chkSolveDHLinks.Location = new System.Drawing.Point(16, 16);
      this.chkSolveDHLinks.Name = "chkSolveDHLinks";
      this.chkSolveDHLinks.Size = new System.Drawing.Size(115, 17);
      this.chkSolveDHLinks.TabIndex = 6;
      this.chkSolveDHLinks.Text = "Solve for DH Links";
      this.chkSolveDHLinks.UseVisualStyleBackColor = true;
      this.chkSolveDHLinks.CheckedChanged += new System.EventHandler(this.chkSolveDHLinks_CheckedChanged);
      // 
      // btnSolveIt
      // 
      this.btnSolveIt.Location = new System.Drawing.Point(16, 225);
      this.btnSolveIt.Name = "btnSolveIt";
      this.btnSolveIt.Size = new System.Drawing.Size(153, 38);
      this.btnSolveIt.TabIndex = 5;
      this.btnSolveIt.Text = "Solve Problem";
      this.btnSolveIt.UseVisualStyleBackColor = true;
      this.btnSolveIt.Click += new System.EventHandler(this.btnSolveIt_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.lblElementsToSolve);
      this.groupBox1.Controls.Add(this.lblFileConfigNom);
      this.groupBox1.Controls.Add(this.btnUnPause);
      this.groupBox1.Controls.Add(this.lblRunTimeVal);
      this.groupBox1.Controls.Add(this.lblRunTime);
      this.groupBox1.Controls.Add(this.lblSeqStepValue);
      this.groupBox1.Controls.Add(this.lblSeqStep);
      this.groupBox1.Controls.Add(this.GenSolvFile);
      this.groupBox1.Controls.Add(this.SolverFile);
      this.groupBox1.Controls.Add(this.DataFile);
      this.groupBox1.Controls.Add(this.AdditionalFile);
      this.groupBox1.Controls.Add(this.ConfigFile);
      this.groupBox1.Controls.Add(this.NomConfig);
      this.groupBox1.Controls.Add(this.lblFileSolver);
      this.groupBox1.Controls.Add(this.lblFileGenerated);
      this.groupBox1.Controls.Add(this.tbMaxIterations);
      this.groupBox1.Controls.Add(this.lblMaxIterations);
      this.groupBox1.Controls.Add(this.label2);
      this.groupBox1.Controls.Add(this.label1);
      this.groupBox1.Controls.Add(this.txtTimeOut);
      this.groupBox1.Controls.Add(this.lblFileAdditional);
      this.groupBox1.Controls.Add(this.chkAutoScale);
      this.groupBox1.Controls.Add(this.txtStepSize);
      this.groupBox1.Controls.Add(this.btnAbort);
      this.groupBox1.Controls.Add(this.pgStatus);
      this.groupBox1.Controls.Add(this.lblFileData);
      this.groupBox1.Controls.Add(this.lblFileConfig);
      this.groupBox1.Controls.Add(this.txtSolverStatus);
      this.groupBox1.Controls.Add(this.lblSolverStatus);
      this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.groupBox1.Location = new System.Drawing.Point(12, 607);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(900, 276);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Solver Status";
      // 
      // lblFileConfigNom
      // 
      this.lblFileConfigNom.AutoSize = true;
      this.lblFileConfigNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
      this.lblFileConfigNom.Location = new System.Drawing.Point(129, 23);
      this.lblFileConfigNom.Name = "lblFileConfigNom";
      this.lblFileConfigNom.Size = new System.Drawing.Size(78, 13);
      this.lblFileConfigNom.TabIndex = 34;
      this.lblFileConfigNom.TabStop = true;
      this.lblFileConfigNom.Text = "None Selected";
      this.toolTip1.SetToolTip(this.lblFileConfigNom, "Left Click to open file, Right Click to open file folder.");
      this.lblFileConfigNom.Click += new System.EventHandler(this.lblFileConfigNom_Click);
      // 
      // btnUnPause
      // 
      this.btnUnPause.Location = new System.Drawing.Point(310, 127);
      this.btnUnPause.Name = "btnUnPause";
      this.btnUnPause.Size = new System.Drawing.Size(75, 49);
      this.btnUnPause.TabIndex = 33;
      this.btnUnPause.Text = "Unpause Solver";
      this.toolTip1.SetToolTip(this.btnUnPause, "Resumes Solving After Pause Instruction");
      this.btnUnPause.UseVisualStyleBackColor = true;
      this.btnUnPause.Click += new System.EventHandler(this.btnUnPause_Click);
      // 
      // lblRunTimeVal
      // 
      this.lblRunTimeVal.AutoSize = true;
      this.lblRunTimeVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRunTimeVal.Location = new System.Drawing.Point(491, 155);
      this.lblRunTimeVal.Name = "lblRunTimeVal";
      this.lblRunTimeVal.Size = new System.Drawing.Size(13, 13);
      this.lblRunTimeVal.TabIndex = 32;
      this.lblRunTimeVal.Text = "0";
      this.lblRunTimeVal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblRunTime
      // 
      this.lblRunTime.AutoSize = true;
      this.lblRunTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblRunTime.Location = new System.Drawing.Point(391, 155);
      this.lblRunTime.Name = "lblRunTime";
      this.lblRunTime.Size = new System.Drawing.Size(56, 13);
      this.lblRunTime.TabIndex = 31;
      this.lblRunTime.Text = "Run Time:";
      this.lblRunTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblSeqStepValue
      // 
      this.lblSeqStepValue.AutoSize = true;
      this.lblSeqStepValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSeqStepValue.Location = new System.Drawing.Point(491, 132);
      this.lblSeqStepValue.Name = "lblSeqStepValue";
      this.lblSeqStepValue.Size = new System.Drawing.Size(13, 13);
      this.lblSeqStepValue.TabIndex = 30;
      this.lblSeqStepValue.Text = "1";
      this.lblSeqStepValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblSeqStep
      // 
      this.lblSeqStep.AutoSize = true;
      this.lblSeqStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSeqStep.Location = new System.Drawing.Point(391, 132);
      this.lblSeqStep.Name = "lblSeqStep";
      this.lblSeqStep.Size = new System.Drawing.Size(94, 13);
      this.lblSeqStep.TabIndex = 29;
      this.lblSeqStep.Text = "Sequence Step #:";
      this.lblSeqStep.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // GenSolvFile
      // 
      this.GenSolvFile.AutoSize = true;
      this.GenSolvFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.GenSolvFile.Location = new System.Drawing.Point(13, 87);
      this.GenSolvFile.Name = "GenSolvFile";
      this.GenSolvFile.Size = new System.Drawing.Size(112, 13);
      this.GenSolvFile.TabIndex = 28;
      this.GenSolvFile.Text = "Generated Solver File:";
      this.GenSolvFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // SolverFile
      // 
      this.SolverFile.AutoSize = true;
      this.SolverFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.SolverFile.Location = new System.Drawing.Point(13, 74);
      this.SolverFile.Name = "SolverFile";
      this.SolverFile.Size = new System.Drawing.Size(59, 13);
      this.SolverFile.TabIndex = 27;
      this.SolverFile.Text = "Solver File:";
      this.SolverFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // DataFile
      // 
      this.DataFile.AutoSize = true;
      this.DataFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.DataFile.Location = new System.Drawing.Point(13, 61);
      this.DataFile.Name = "DataFile";
      this.DataFile.Size = new System.Drawing.Size(52, 13);
      this.DataFile.TabIndex = 26;
      this.DataFile.Text = "Data File:";
      this.DataFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // AdditionalFile
      // 
      this.AdditionalFile.AutoSize = true;
      this.AdditionalFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.AdditionalFile.Location = new System.Drawing.Point(13, 48);
      this.AdditionalFile.Name = "AdditionalFile";
      this.AdditionalFile.Size = new System.Drawing.Size(75, 13);
      this.AdditionalFile.TabIndex = 25;
      this.AdditionalFile.Text = "Additional File:";
      this.AdditionalFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // ConfigFile
      // 
      this.ConfigFile.AutoSize = true;
      this.ConfigFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.ConfigFile.Location = new System.Drawing.Point(13, 35);
      this.ConfigFile.Name = "ConfigFile";
      this.ConfigFile.Size = new System.Drawing.Size(59, 13);
      this.ConfigFile.TabIndex = 24;
      this.ConfigFile.Text = "Config File:";
      this.ConfigFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // NomConfig
      // 
      this.NomConfig.AutoSize = true;
      this.NomConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.NomConfig.Location = new System.Drawing.Point(13, 23);
      this.NomConfig.Name = "NomConfig";
      this.NomConfig.Size = new System.Drawing.Size(100, 13);
      this.NomConfig.TabIndex = 23;
      this.NomConfig.Text = "Nominal Config File:";
      this.NomConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblFileSolver
      // 
      this.lblFileSolver.AutoSize = true;
      this.lblFileSolver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFileSolver.Location = new System.Drawing.Point(129, 74);
      this.lblFileSolver.Name = "lblFileSolver";
      this.lblFileSolver.Size = new System.Drawing.Size(78, 13);
      this.lblFileSolver.TabIndex = 22;
      this.lblFileSolver.TabStop = true;
      this.lblFileSolver.Text = "None Selected";
      this.lblFileSolver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolTip1.SetToolTip(this.lblFileSolver, "Left Click to open file, Right Click to open file folder.");
      this.lblFileSolver.Click += new System.EventHandler(this.lblFileSolver_Click);
      // 
      // lblFileGenerated
      // 
      this.lblFileGenerated.AutoSize = true;
      this.lblFileGenerated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFileGenerated.Location = new System.Drawing.Point(129, 87);
      this.lblFileGenerated.Name = "lblFileGenerated";
      this.lblFileGenerated.Size = new System.Drawing.Size(78, 13);
      this.lblFileGenerated.TabIndex = 21;
      this.lblFileGenerated.TabStop = true;
      this.lblFileGenerated.Text = "None Selected";
      this.lblFileGenerated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolTip1.SetToolTip(this.lblFileGenerated, "Left Click to open file, Right Click to open file folder.");
      this.lblFileGenerated.Click += new System.EventHandler(this.lblFileGenerated_Click);
      // 
      // tbMaxIterations
      // 
      this.tbMaxIterations.Location = new System.Drawing.Point(746, 213);
      this.tbMaxIterations.Name = "tbMaxIterations";
      this.tbMaxIterations.Size = new System.Drawing.Size(100, 22);
      this.tbMaxIterations.TabIndex = 19;
      this.tbMaxIterations.Text = "100";
      this.tbMaxIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblMaxIterations
      // 
      this.lblMaxIterations.AutoSize = true;
      this.lblMaxIterations.Location = new System.Drawing.Point(743, 194);
      this.lblMaxIterations.Name = "lblMaxIterations";
      this.lblMaxIterations.Size = new System.Drawing.Size(90, 16);
      this.lblMaxIterations.TabIndex = 18;
      this.lblMaxIterations.Text = "Max Iterations";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(743, 89);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(142, 16);
      this.label2.TabIndex = 17;
      this.label2.Text = "StepSize (1e-9 to 1e-3)";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(743, 141);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(77, 16);
      this.label1.TabIndex = 16;
      this.label1.Text = "TimeOut (s)";
      // 
      // txtTimeOut
      // 
      this.txtTimeOut.Location = new System.Drawing.Point(746, 160);
      this.txtTimeOut.Name = "txtTimeOut";
      this.txtTimeOut.Size = new System.Drawing.Size(100, 22);
      this.txtTimeOut.TabIndex = 15;
      this.txtTimeOut.Text = "120";
      this.txtTimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      // 
      // lblFileAdditional
      // 
      this.lblFileAdditional.AutoSize = true;
      this.lblFileAdditional.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFileAdditional.Location = new System.Drawing.Point(129, 48);
      this.lblFileAdditional.Name = "lblFileAdditional";
      this.lblFileAdditional.Size = new System.Drawing.Size(78, 13);
      this.lblFileAdditional.TabIndex = 14;
      this.lblFileAdditional.TabStop = true;
      this.lblFileAdditional.Text = "None Selected";
      this.lblFileAdditional.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolTip1.SetToolTip(this.lblFileAdditional, "Left Click to open file, Right Click to open file folder.");
      this.lblFileAdditional.Click += new System.EventHandler(this.lblFileAdditional_Click);
      // 
      // chkAutoScale
      // 
      this.chkAutoScale.AutoSize = true;
      this.chkAutoScale.Location = new System.Drawing.Point(746, 62);
      this.chkAutoScale.Name = "chkAutoScale";
      this.chkAutoScale.Size = new System.Drawing.Size(92, 20);
      this.chkAutoScale.TabIndex = 13;
      this.chkAutoScale.Text = "Auto Scale";
      this.chkAutoScale.UseVisualStyleBackColor = true;
      // 
      // txtStepSize
      // 
      this.txtStepSize.Location = new System.Drawing.Point(746, 108);
      this.txtStepSize.Name = "txtStepSize";
      this.txtStepSize.Size = new System.Drawing.Size(100, 22);
      this.txtStepSize.TabIndex = 12;
      this.txtStepSize.Text = "1e-8";
      this.txtStepSize.MouseLeave += new System.EventHandler(this.txtStepSize_MouseLeave);
      // 
      // btnAbort
      // 
      this.btnAbort.Location = new System.Drawing.Point(229, 127);
      this.btnAbort.Name = "btnAbort";
      this.btnAbort.Size = new System.Drawing.Size(75, 49);
      this.btnAbort.TabIndex = 11;
      this.btnAbort.Text = "Abort Solver";
      this.toolTip1.SetToolTip(this.btnAbort, "Aborts Solving");
      this.btnAbort.UseVisualStyleBackColor = true;
      this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
      // 
      // pgStatus
      // 
      this.pgStatus.Location = new System.Drawing.Point(230, 182);
      this.pgStatus.Name = "pgStatus";
      this.pgStatus.Size = new System.Drawing.Size(217, 23);
      this.pgStatus.TabIndex = 10;
      // 
      // lblFileData
      // 
      this.lblFileData.AutoSize = true;
      this.lblFileData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFileData.Location = new System.Drawing.Point(129, 61);
      this.lblFileData.Name = "lblFileData";
      this.lblFileData.Size = new System.Drawing.Size(78, 13);
      this.lblFileData.TabIndex = 9;
      this.lblFileData.TabStop = true;
      this.lblFileData.Text = "None Selected";
      this.lblFileData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolTip1.SetToolTip(this.lblFileData, "Left Click to open file, Right Click to open file folder.");
      this.lblFileData.Click += new System.EventHandler(this.lblFileData_Click);
      // 
      // lblFileConfig
      // 
      this.lblFileConfig.AutoSize = true;
      this.lblFileConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblFileConfig.Location = new System.Drawing.Point(129, 35);
      this.lblFileConfig.Name = "lblFileConfig";
      this.lblFileConfig.Size = new System.Drawing.Size(78, 13);
      this.lblFileConfig.TabIndex = 8;
      this.lblFileConfig.TabStop = true;
      this.lblFileConfig.Text = "None Selected";
      this.lblFileConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.toolTip1.SetToolTip(this.lblFileConfig, "Left Click to open file, Right Click to open file folder.");
      this.lblFileConfig.Click += new System.EventHandler(this.lblFileConfig_Click);
      // 
      // txtSolverStatus
      // 
      this.txtSolverStatus.Enabled = false;
      this.txtSolverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtSolverStatus.Location = new System.Drawing.Point(6, 127);
      this.txtSolverStatus.Multiline = true;
      this.txtSolverStatus.Name = "txtSolverStatus";
      this.txtSolverStatus.Size = new System.Drawing.Size(217, 137);
      this.txtSolverStatus.TabIndex = 7;
      this.txtSolverStatus.TabStop = false;
      this.txtSolverStatus.Text = "Xerr: 4.444\r\nYerr: 5.555\r\nZerr: 666\r\nTotal: 5.555\r\nStdDev: kkk...\r\nAverage: 444.4" +
    "44\r\nA3s: 023984";
      // 
      // lblSolverStatus
      // 
      this.lblSolverStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblSolverStatus.Location = new System.Drawing.Point(6, 104);
      this.lblSolverStatus.Name = "lblSolverStatus";
      this.lblSolverStatus.Size = new System.Drawing.Size(441, 20);
      this.lblSolverStatus.TabIndex = 6;
      this.lblSolverStatus.Text = "Solver Status";
      this.lblSolverStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // menuStrip1
      // 
      this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.utlitiesToolStripMenuItem});
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(923, 24);
      this.menuStrip1.TabIndex = 8;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // mnuFile
      // 
      this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileConfigNom,
            this.mnuReloadNomConfig,
            this.mnuFileConfig,
            this.mnuFileData,
            this.mnuArchiveConfigFile,
            this.mnuSolverFile,
            this.mnuSetGenerated,
            this.mnuSaveProject,
            this.mnuLoadProject,
            this.toolStripSeparator1,
            this.mnuFileAddConfigFile,
            this.toolStripSeparator2,
            this.mnuFileExit});
      this.mnuFile.Name = "mnuFile";
      this.mnuFile.Size = new System.Drawing.Size(37, 20);
      this.mnuFile.Text = "File";
      // 
      // mnuFileConfigNom
      // 
      this.mnuFileConfigNom.Name = "mnuFileConfigNom";
      this.mnuFileConfigNom.Size = new System.Drawing.Size(242, 22);
      this.mnuFileConfigNom.Text = "Set Nominal Config File";
      this.mnuFileConfigNom.Click += new System.EventHandler(this.mnuFileConfigNom_Click);
      // 
      // mnuReloadNomConfig
      // 
      this.mnuReloadNomConfig.Name = "mnuReloadNomConfig";
      this.mnuReloadNomConfig.Size = new System.Drawing.Size(242, 22);
      this.mnuReloadNomConfig.Text = "Reload Nominal Config File";
      this.mnuReloadNomConfig.Click += new System.EventHandler(this.mnuReloadNomConfig_Click);
      // 
      // mnuFileConfig
      // 
      this.mnuFileConfig.Name = "mnuFileConfig";
      this.mnuFileConfig.Size = new System.Drawing.Size(242, 22);
      this.mnuFileConfig.Text = "Set Config File";
      this.mnuFileConfig.Click += new System.EventHandler(this.mnuFileConfig_Click);
      // 
      // mnuFileData
      // 
      this.mnuFileData.Name = "mnuFileData";
      this.mnuFileData.Size = new System.Drawing.Size(242, 22);
      this.mnuFileData.Text = "Set Data File";
      this.mnuFileData.Click += new System.EventHandler(this.mnuFileData_Click);
      // 
      // mnuArchiveConfigFile
      // 
      this.mnuArchiveConfigFile.Name = "mnuArchiveConfigFile";
      this.mnuArchiveConfigFile.Size = new System.Drawing.Size(242, 22);
      this.mnuArchiveConfigFile.Text = "Archive Current Config File";
      this.mnuArchiveConfigFile.Click += new System.EventHandler(this.mnuArchiveConfig_Click);
      // 
      // mnuSolverFile
      // 
      this.mnuSolverFile.Name = "mnuSolverFile";
      this.mnuSolverFile.Size = new System.Drawing.Size(242, 22);
      this.mnuSolverFile.Text = "Set Solver File (only if you want)";
      this.mnuSolverFile.Click += new System.EventHandler(this.mnuSolverFile_Click);
      // 
      // mnuSetGenerated
      // 
      this.mnuSetGenerated.Name = "mnuSetGenerated";
      this.mnuSetGenerated.Size = new System.Drawing.Size(242, 22);
      this.mnuSetGenerated.Text = "Set Generated Solver File";
      this.mnuSetGenerated.Click += new System.EventHandler(this.mnuSetGenerated_Click);
      // 
      // mnuSaveProject
      // 
      this.mnuSaveProject.Name = "mnuSaveProject";
      this.mnuSaveProject.Size = new System.Drawing.Size(242, 22);
      this.mnuSaveProject.Text = "Save Project File As";
      this.mnuSaveProject.Click += new System.EventHandler(this.mnuSaveProject_Click);
      // 
      // mnuLoadProject
      // 
      this.mnuLoadProject.Name = "mnuLoadProject";
      this.mnuLoadProject.Size = new System.Drawing.Size(242, 22);
      this.mnuLoadProject.Text = "Load Project File";
      this.mnuLoadProject.Click += new System.EventHandler(this.mnuLoadProject_Click);
      // 
      // toolStripSeparator1
      // 
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new System.Drawing.Size(239, 6);
      // 
      // mnuFileAddConfigFile
      // 
      this.mnuFileAddConfigFile.Name = "mnuFileAddConfigFile";
      this.mnuFileAddConfigFile.Size = new System.Drawing.Size(242, 22);
      this.mnuFileAddConfigFile.Text = "Add Config File";
      this.mnuFileAddConfigFile.Click += new System.EventHandler(this.mnuFileAddConfigFile_Click);
      // 
      // toolStripSeparator2
      // 
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new System.Drawing.Size(239, 6);
      // 
      // mnuFileExit
      // 
      this.mnuFileExit.Name = "mnuFileExit";
      this.mnuFileExit.Size = new System.Drawing.Size(242, 22);
      this.mnuFileExit.Text = "Exit";
      this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
      // 
      // utlitiesToolStripMenuItem
      // 
      this.utlitiesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createCLEHeaderToolStripMenuItem,
            this.createCBCcodeToolStripMenuItem});
      this.utlitiesToolStripMenuItem.Name = "utlitiesToolStripMenuItem";
      this.utlitiesToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
      this.utlitiesToolStripMenuItem.Text = "Utlities";
      // 
      // createCLEHeaderToolStripMenuItem
      // 
      this.createCLEHeaderToolStripMenuItem.Name = "createCLEHeaderToolStripMenuItem";
      this.createCLEHeaderToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.createCLEHeaderToolStripMenuItem.Text = "Create CLE Header";
      this.createCLEHeaderToolStripMenuItem.Click += new System.EventHandler(this.createCLEHeaderToolStripMenuItem_Click);
      // 
      // createCBCcodeToolStripMenuItem
      // 
      this.createCBCcodeToolStripMenuItem.Name = "createCBCcodeToolStripMenuItem";
      this.createCBCcodeToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
      this.createCBCcodeToolStripMenuItem.Text = "Create CB Ccode";
      this.createCBCcodeToolStripMenuItem.Click += new System.EventHandler(this.createCBCcodeToolStripMenuItem_Click);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
      // 
      // lblElementsToSolve
      // 
      this.lblElementsToSolve.AutoSize = true;
      this.lblElementsToSolve.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblElementsToSolve.Location = new System.Drawing.Point(229, 222);
      this.lblElementsToSolve.Name = "lblElementsToSolve";
      this.lblElementsToSolve.Size = new System.Drawing.Size(186, 13);
      this.lblElementsToSolve.TabIndex = 35;
      this.lblElementsToSolve.Text = "Solving xxx variables of 500 available.";
      this.lblElementsToSolve.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // frmMain
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(923, 895);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.grpSolverControls);
      this.Controls.Add(this.menuStrip1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.menuStrip1;
      this.Name = "frmMain";
      this.Text = "Electroimpact Solver";
      this.Load += new System.EventHandler(this.frmMain_Load);
      this.grpSolverControls.ResumeLayout(false);
      this.grpSolverControls.PerformLayout();
      this.groupEngineType.ResumeLayout(false);
      this.groupEngineType.PerformLayout();
      this.groupDataRange.ResumeLayout(false);
      this.groupDataRange.PerformLayout();
      this.groupOptimize.ResumeLayout(false);
      this.groupOptimize.PerformLayout();
      this.groupErrorCalc.ResumeLayout(false);
      this.groupErrorCalc.PerformLayout();
      this.grpCompTable.ResumeLayout(false);
      this.grpCompTable.PerformLayout();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}


		#endregion

		private System.Windows.Forms.Timer tmrStatus;
    private System.Windows.Forms.GroupBox grpSolverControls;
		private System.Windows.Forms.Button btnSaveAll;
		private System.Windows.Forms.CheckBox chkSolveAxAttribs;
		private System.Windows.Forms.CheckBox chkSolveDHLinks;
		private System.Windows.Forms.Button btnSolveIt;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtSolverStatus;
		private System.Windows.Forms.Label lblSolverStatus;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem mnuFile;
		private System.Windows.Forms.ToolStripMenuItem mnuFileConfig;
    private System.Windows.Forms.ToolStripMenuItem mnuFileData;
    private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
		private System.Windows.Forms.ProgressBar pgStatus;
		private System.Windows.Forms.Button btnAbort;
		private System.Windows.Forms.TextBox txtXFactor;
		private System.Windows.Forms.TextBox txtZFactor;
    private System.Windows.Forms.TextBox txtYFactor;
		private System.Windows.Forms.TextBox txtRange;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.CheckedListBox lstDHAttribs;
		private System.Windows.Forms.CheckedListBox lstAxisAttribs;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnSolveOnce;
		private System.Windows.Forms.CheckBox chkZscaler;
    private System.Windows.Forms.CheckBox chkYscaler;
		private System.Windows.Forms.GroupBox grpCompTable;
		private System.Windows.Forms.Button btnCTuncheckall;
		private System.Windows.Forms.Button btnCTcheckall;
		private System.Windows.Forms.CheckBox chkCTYaw;
		private System.Windows.Forms.CheckBox chkCTPitch;
		private System.Windows.Forms.CheckBox chkCTRoll;
		private System.Windows.Forms.CheckBox chkCTZ;
		private System.Windows.Forms.CheckBox chkCTY;
		private System.Windows.Forms.CheckBox chkCTX;
		private System.Windows.Forms.Button btnSaveConfig;
    private System.Windows.Forms.Button btnSaveData;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.CheckBox chkSolveCompTables;
    private System.Windows.Forms.CheckedListBox lstCompTables;
    private System.Windows.Forms.Button btnCompChangeLimits;
    private System.Windows.Forms.CheckBox chkCompUseLimits;
    private System.Windows.Forms.TextBox txtCompAxisLimits;
    private System.Windows.Forms.ToolStripMenuItem mnuSolverFile;
    private System.Windows.Forms.Button btnRunSolverFile;
    private System.Windows.Forms.Button btnNullCompTables;
    private System.Windows.Forms.CheckBox chkXscaler;
    private System.Windows.Forms.CheckBox chkDebugMode;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.RadioButton opnExperimental;
    private System.Windows.Forms.RadioButton opnSumSquares;
    private System.Windows.Forms.TextBox txtStepSize;
    private System.Windows.Forms.CheckBox chkAutoScale;
    private System.Windows.Forms.ToolStripMenuItem mnuFileAddConfigFile;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txtTimeOut;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button btnDH_uncheckall;
    private System.Windows.Forms.Button btnDH_checkall;
    private System.Windows.Forms.Button btnAxAt_uncheckall;
    private System.Windows.Forms.Button btnAxAt_checkall;
    private System.Windows.Forms.TextBox tbMaxIterations;
    private System.Windows.Forms.Label lblMaxIterations;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ToolStripMenuItem mnuFileConfigNom;
    private System.Windows.Forms.ToolStripMenuItem mnuReloadNomConfig;
    private System.Windows.Forms.GroupBox groupErrorCalc;
    private System.Windows.Forms.GroupBox groupDataRange;
    private System.Windows.Forms.GroupBox groupOptimize;
    private System.Windows.Forms.GroupBox groupEngineType;
    private System.Windows.Forms.RadioButton radioNonLinear;
    private System.Windows.Forms.RadioButton radioLinear;
    private System.Windows.Forms.Button btnWriteSeq;
    private System.Windows.Forms.ToolStripMenuItem mnuArchiveConfigFile;
    private System.Windows.Forms.Button BtnNextStep;
    private System.Windows.Forms.ToolStripMenuItem mnuSetGenerated;
    private System.Windows.Forms.ToolStripMenuItem mnuSaveProject;
    private System.Windows.Forms.ToolStripMenuItem mnuLoadProject;
    private System.Windows.Forms.Label GenSolvFile;
    private System.Windows.Forms.Label SolverFile;
    private System.Windows.Forms.Label DataFile;
    private System.Windows.Forms.Label AdditionalFile;
    private System.Windows.Forms.Label ConfigFile;
    private System.Windows.Forms.Label NomConfig;
    private System.Windows.Forms.Label lblSeqStepValue;
    private System.Windows.Forms.Label lblSeqStep;
    private System.Windows.Forms.Label lblRunTimeVal;
    private System.Windows.Forms.Label lblRunTime;
    private System.Windows.Forms.Button btnUnPause;
    private System.Windows.Forms.ToolStripMenuItem utlitiesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem createCLEHeaderToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.LinkLabel lblFileData;
    private System.Windows.Forms.LinkLabel lblFileConfig;
    private System.Windows.Forms.LinkLabel lblFileAdditional;
    private System.Windows.Forms.LinkLabel lblFileGenerated;
    private System.Windows.Forms.LinkLabel lblFileSolver;
    private System.Windows.Forms.LinkLabel lblFileConfigNom;
    private System.Windows.Forms.ToolStripMenuItem createCBCcodeToolStripMenuItem;
    private System.Windows.Forms.Label lblElementsToSolve;
  }
}

