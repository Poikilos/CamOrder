/*
 * Created by SharpDevelop.
 * User: Poikilos
 * Date: 10/11/2012
 * Time: 1:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ExpertMultimedia
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.tbFolderOut = new System.Windows.Forms.TextBox();
			this.btnInputBrowse = new System.Windows.Forms.Button();
			this.btnOutputBrowse = new System.Windows.Forms.Button();
			this.folderbrowserdlgMain = new System.Windows.Forms.FolderBrowserDialog();
			this.tbDTFormatString = new System.Windows.Forms.TextBox();
			this.tbPrependCam = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.addCheckedFilesButton = new System.Windows.Forms.Button();
			this.tbProjectName = new System.Windows.Forms.TextBox();
			this.lblFormatHelp = new System.Windows.Forms.Label();
			this.tbStatus = new System.Windows.Forms.TextBox();
			this.tbSuffix = new System.Windows.Forms.TextBox();
			this.lblTag = new System.Windows.Forms.Label();
			this.btnSaveCamPrefix = new System.Windows.Forms.Button();
			this.formInputSettingsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.markUnusableButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.cameraNameLabel = new System.Windows.Forms.Label();
			this.lblUnusable = new System.Windows.Forms.Label();
			this.tbAppDataFolder = new System.Windows.Forms.TextBox();
			this.unusableTextBox = new System.Windows.Forms.TextBox();
			this.cbFolderIn = new System.Windows.Forms.ComboBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.refreshButton = new System.Windows.Forms.Button();
			this.checkAllButton = new System.Windows.Forms.Button();
			this.uncheckAllButton = new System.Windows.Forms.Button();
			this.selectAllInputButton = new System.Windows.Forms.Button();
			this.deselectAllButton = new System.Windows.Forms.Button();
			this.uncheckSelectedButton = new System.Windows.Forms.Button();
			this.checkSelectedButton = new System.Windows.Forms.Button();
			this.cbDeleteFromSource = new System.Windows.Forms.CheckBox();
			this.inputListView = new System.Windows.Forms.ListView();
			this.tlpCopyCommand = new System.Windows.Forms.TableLayoutPanel();
			this.cbxCopyCommand = new System.Windows.Forms.ComboBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiListFilesMarkedUnusableBut = new System.Windows.Forms.ToolStripMenuItem();
			this.markFilesAsUnusableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.importAndMarkAsUnusableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteAndMarkAsUnusableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshDestinationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteAfterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.allowMarkingUnusableWithoutReasonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveOutputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.savefiledlgMain = new System.Windows.Forms.SaveFileDialog();
			this.openfiledlgMain = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanelOutput = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanelButtonsBelowOutputList = new System.Windows.Forms.FlowLayoutPanel();
			this.runBatchButton = new System.Windows.Forms.Button();
			this.deleteOutputButton = new System.Windows.Forms.Button();
			this.copyOutputButton = new System.Windows.Forms.Button();
			this.pasteOutputButton = new System.Windows.Forms.Button();
			this.outputClearAllButton = new System.Windows.Forms.Button();
			this.flowLayoutPanelButtonsAboveOutputList = new System.Windows.Forms.FlowLayoutPanel();
			this.markDoneButton = new System.Windows.Forms.Button();
			this.outputListBox = new System.Windows.Forms.ListBox();
			this.splitContainerMainInAndOut = new System.Windows.Forms.SplitContainer();
			this.formUpperOuterTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.formUpperSplitContainer = new System.Windows.Forms.SplitContainer();
			this.inputSettingsScrollPanel = new System.Windows.Forms.Panel();
			this.formUpperLeftOuterSettingsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.prefixCheckBox = new System.Windows.Forms.CheckBox();
			this.inputListTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.flowLayoutPanelControlsLeftOfOutputBox = new System.Windows.Forms.FlowLayoutPanel();
			this.saveCamSettingsButton = new System.Windows.Forms.Button();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.initTimer = new System.Windows.Forms.Timer(this.components);
			this.formInputSettingsTableLayoutPanel.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.tlpCopyCommand.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.tableLayoutPanelOutput.SuspendLayout();
			this.flowLayoutPanelButtonsBelowOutputList.SuspendLayout();
			this.flowLayoutPanelButtonsAboveOutputList.SuspendLayout();
			this.splitContainerMainInAndOut.Panel1.SuspendLayout();
			this.splitContainerMainInAndOut.Panel2.SuspendLayout();
			this.splitContainerMainInAndOut.SuspendLayout();
			this.formUpperOuterTableLayoutPanel.SuspendLayout();
			this.formUpperSplitContainer.Panel1.SuspendLayout();
			this.formUpperSplitContainer.Panel2.SuspendLayout();
			this.formUpperSplitContainer.SuspendLayout();
			this.inputSettingsScrollPanel.SuspendLayout();
			this.formUpperLeftOuterSettingsTableLayoutPanel.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.inputListTableLayoutPanel.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.flowLayoutPanelControlsLeftOfOutputBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbFolderOut
			// 
			this.tbFolderOut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbFolderOut.Location = new System.Drawing.Point(135, 80);
			this.tbFolderOut.Margin = new System.Windows.Forms.Padding(4);
			this.tbFolderOut.Name = "tbFolderOut";
			this.tbFolderOut.Size = new System.Drawing.Size(298, 27);
			this.tbFolderOut.TabIndex = 1;
			this.tbFolderOut.Text = "\\\\FCAFILES\\Resources\\Footage\\new";
			// 
			// btnInputBrowse
			// 
			this.btnInputBrowse.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnInputBrowse.AutoSize = true;
			this.btnInputBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnInputBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnInputBrowse.Location = new System.Drawing.Point(57, 5);
			this.btnInputBrowse.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this.btnInputBrowse.Name = "btnInputBrowse";
			this.btnInputBrowse.Size = new System.Drawing.Size(68, 31);
			this.btnInputBrowse.TabIndex = 2;
			this.btnInputBrowse.Text = "Source:";
			this.btnInputBrowse.UseVisualStyleBackColor = true;
			this.btnInputBrowse.Click += new System.EventHandler(this.BtnInputBrowseClick);
			// 
			// btnOutputBrowse
			// 
			this.btnOutputBrowse.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnOutputBrowse.AutoSize = true;
			this.btnOutputBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnOutputBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOutputBrowse.Location = new System.Drawing.Point(25, 81);
			this.btnOutputBrowse.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this.btnOutputBrowse.Name = "btnOutputBrowse";
			this.btnOutputBrowse.Size = new System.Drawing.Size(100, 31);
			this.btnOutputBrowse.TabIndex = 3;
			this.btnOutputBrowse.Text = "Destination:";
			this.btnOutputBrowse.UseVisualStyleBackColor = true;
			this.btnOutputBrowse.Click += new System.EventHandler(this.BtnOutputBrowseClick);
			// 
			// tbDTFormatString
			// 
			this.tbDTFormatString.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbDTFormatString.Location = new System.Drawing.Point(135, 189);
			this.tbDTFormatString.Margin = new System.Windows.Forms.Padding(4);
			this.tbDTFormatString.Name = "tbDTFormatString";
			this.tbDTFormatString.Size = new System.Drawing.Size(298, 27);
			this.tbDTFormatString.TabIndex = 5;
			this.tbDTFormatString.Text = "yyyy-MM-dd HH_mm_ss";
			this.tbDTFormatString.MouseLeave += new System.EventHandler(this.TbDTFormatStringMouseLeave);
			this.tbDTFormatString.MouseEnter += new System.EventHandler(this.TbDTFormatStringMouseEnter);
			// 
			// tbPrependCam
			// 
			this.tbPrependCam.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbPrependCam.ForeColor = System.Drawing.Color.Red;
			this.tbPrependCam.Location = new System.Drawing.Point(135, 154);
			this.tbPrependCam.Margin = new System.Windows.Forms.Padding(4);
			this.tbPrependCam.Name = "tbPrependCam";
			this.tbPrependCam.Size = new System.Drawing.Size(298, 27);
			this.tbPrependCam.TabIndex = 6;
			this.tbPrependCam.TextChanged += new System.EventHandler(this.TbPrependCamTextChanged);
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 7);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(273, 19);
			this.label2.TabIndex = 8;
			this.label2.Text = "Copy command (quote INFILE&&OUTFILE):";
			this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			// 
			// addCheckedFilesButton
			// 
			this.addCheckedFilesButton.AutoSize = true;
			this.addCheckedFilesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.addCheckedFilesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.addCheckedFilesButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.addCheckedFilesButton.Location = new System.Drawing.Point(4, 4);
			this.addCheckedFilesButton.Margin = new System.Windows.Forms.Padding(4);
			this.addCheckedFilesButton.Name = "addCheckedFilesButton";
			this.addCheckedFilesButton.Size = new System.Drawing.Size(133, 31);
			this.addCheckedFilesButton.TabIndex = 10;
			this.addCheckedFilesButton.Text = "Add (0) to Batch";
			this.addCheckedFilesButton.UseVisualStyleBackColor = true;
			this.addCheckedFilesButton.Click += new System.EventHandler(this.AddCheckedFilesButtonClick);
			// 
			// tbProjectName
			// 
			this.tbProjectName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbProjectName.Location = new System.Drawing.Point(135, 45);
			this.tbProjectName.Margin = new System.Windows.Forms.Padding(4);
			this.tbProjectName.Name = "tbProjectName";
			this.tbProjectName.Size = new System.Drawing.Size(298, 27);
			this.tbProjectName.TabIndex = 11;
			// 
			// lblFormatHelp
			// 
			this.lblFormatHelp.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblFormatHelp.AutoSize = true;
			this.lblFormatHelp.Location = new System.Drawing.Point(41, 193);
			this.lblFormatHelp.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.lblFormatHelp.Name = "lblFormatHelp";
			this.lblFormatHelp.Size = new System.Drawing.Size(84, 19);
			this.lblFormatHelp.TabIndex = 12;
			this.lblFormatHelp.Text = "Annotation:";
			// 
			// tbStatus
			// 
			this.tbStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tbStatus.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbStatus.Location = new System.Drawing.Point(0, 693);
			this.tbStatus.Margin = new System.Windows.Forms.Padding(4);
			this.tbStatus.Name = "tbStatus";
			this.tbStatus.ReadOnly = true;
			this.tbStatus.Size = new System.Drawing.Size(1274, 33);
			this.tbStatus.TabIndex = 13;
			this.tbStatus.TextChanged += new System.EventHandler(this.TbStatusTextChanged);
			// 
			// tbSuffix
			// 
			this.tbSuffix.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbSuffix.Location = new System.Drawing.Point(135, 224);
			this.tbSuffix.Margin = new System.Windows.Forms.Padding(4);
			this.tbSuffix.Name = "tbSuffix";
			this.tbSuffix.Size = new System.Drawing.Size(298, 27);
			this.tbSuffix.TabIndex = 14;
			this.tbSuffix.Text = " 720p30";
			// 
			// lblTag
			// 
			this.lblTag.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblTag.AutoSize = true;
			this.lblTag.Location = new System.Drawing.Point(70, 49);
			this.lblTag.Name = "lblTag";
			this.lblTag.Size = new System.Drawing.Size(58, 19);
			this.lblTag.TabIndex = 16;
			this.lblTag.Text = "Project:";
			this.lblTag.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnSaveCamPrefix
			// 
			this.btnSaveCamPrefix.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnSaveCamPrefix.AutoSize = true;
			this.btnSaveCamPrefix.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.btnSaveCamPrefix.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSaveCamPrefix.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSaveCamPrefix.Location = new System.Drawing.Point(108, 337);
			this.btnSaveCamPrefix.Margin = new System.Windows.Forms.Padding(4);
			this.btnSaveCamPrefix.Name = "btnSaveCamPrefix";
			this.btnSaveCamPrefix.Size = new System.Drawing.Size(226, 12);
			this.btnSaveCamPrefix.TabIndex = 17;
			this.btnSaveCamPrefix.Text = "Save Settings to Device Above";
			this.btnSaveCamPrefix.UseVisualStyleBackColor = true;
			this.btnSaveCamPrefix.Click += new System.EventHandler(this.SaveCamSettingsButtonClick);
			// 
			// formInputSettingsTableLayoutPanel
			// 
			this.formInputSettingsTableLayoutPanel.AutoSize = true;
			this.formInputSettingsTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.formInputSettingsTableLayoutPanel.ColumnCount = 2;
			this.formInputSettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
			this.formInputSettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.lblFormatHelp, 0, 6);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.markUnusableButton, 0, 8);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.tbSuffix, 1, 7);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.label4, 0, 7);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.tbFolderOut, 1, 3);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.tbDTFormatString, 1, 6);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.tbPrependCam, 1, 5);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.btnInputBrowse, 0, 1);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.btnOutputBrowse, 0, 3);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.cameraNameLabel, 0, 5);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.lblTag, 0, 2);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.tbProjectName, 1, 2);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.lblUnusable, 0, 4);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.tbAppDataFolder, 1, 4);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.unusableTextBox, 1, 8);
			this.formInputSettingsTableLayoutPanel.Controls.Add(this.cbFolderIn, 1, 1);
			this.formInputSettingsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.formInputSettingsTableLayoutPanel.Location = new System.Drawing.Point(3, 36);
			this.formInputSettingsTableLayoutPanel.Name = "formInputSettingsTableLayoutPanel";
			this.formInputSettingsTableLayoutPanel.RowCount = 9;
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formInputSettingsTableLayoutPanel.Size = new System.Drawing.Size(437, 294);
			this.formInputSettingsTableLayoutPanel.TabIndex = 16;
			// 
			// markUnusableButton
			// 
			this.markUnusableButton.AutoSize = true;
			this.markUnusableButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.markUnusableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.markUnusableButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.markUnusableButton.Location = new System.Drawing.Point(4, 259);
			this.markUnusableButton.Margin = new System.Windows.Forms.Padding(4);
			this.markUnusableButton.Name = "markUnusableButton";
			this.markUnusableButton.Size = new System.Drawing.Size(123, 31);
			this.markUnusableButton.TabIndex = 12;
			this.markUnusableButton.Text = "Mark (0) Unusable:";
			this.markUnusableButton.UseVisualStyleBackColor = true;
			this.markUnusableButton.Click += new System.EventHandler(this.MarkUnusableButtonClick);
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(80, 228);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(48, 19);
			this.label4.TabIndex = 18;
			this.label4.Text = "Suffix:";
			// 
			// cameraNameLabel
			// 
			this.cameraNameLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cameraNameLabel.AutoSize = true;
			this.cameraNameLabel.Location = new System.Drawing.Point(23, 158);
			this.cameraNameLabel.Name = "cameraNameLabel";
			this.cameraNameLabel.Size = new System.Drawing.Size(105, 19);
			this.cameraNameLabel.TabIndex = 24;
			this.cameraNameLabel.Text = "Camera Name:";
			// 
			// lblUnusable
			// 
			this.lblUnusable.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lblUnusable.AutoSize = true;
			this.lblUnusable.Location = new System.Drawing.Point(19, 124);
			this.lblUnusable.Name = "lblUnusable";
			this.lblUnusable.Size = new System.Drawing.Size(109, 19);
			this.lblUnusable.TabIndex = 20;
			this.lblUnusable.Text = "Records Folder:";
			this.lblUnusable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tbAppDataFolder
			// 
			this.tbAppDataFolder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbAppDataFolder.Location = new System.Drawing.Point(134, 120);
			this.tbAppDataFolder.Name = "tbAppDataFolder";
			this.tbAppDataFolder.Size = new System.Drawing.Size(300, 27);
			this.tbAppDataFolder.TabIndex = 21;
			this.tbAppDataFolder.Text = "\\\\FCAFILES\\Resources\\Footage\\unusable";
			this.tbAppDataFolder.TextChanged += new System.EventHandler(this.TbAppDataFolderTextChanged);
			// 
			// unusableTextBox
			// 
			this.unusableTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.unusableTextBox.Location = new System.Drawing.Point(135, 259);
			this.unusableTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.unusableTextBox.Name = "unusableTextBox";
			this.unusableTextBox.Size = new System.Drawing.Size(298, 27);
			this.unusableTextBox.TabIndex = 11;
			// 
			// cbFolderIn
			// 
			this.cbFolderIn.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbFolderIn.FormattingEnabled = true;
			this.cbFolderIn.Location = new System.Drawing.Point(134, 3);
			this.cbFolderIn.Name = "cbFolderIn";
			this.cbFolderIn.Size = new System.Drawing.Size(300, 27);
			this.cbFolderIn.TabIndex = 25;
			this.cbFolderIn.SelectedIndexChanged += new System.EventHandler(this.CbFolderInSelectedIndexChanged);
			this.cbFolderIn.Leave += new System.EventHandler(this.CbFolderInLeave);
			this.cbFolderIn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CbFolderInMouseDown);
			this.cbFolderIn.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CbFolderInKeyDown);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.Controls.Add(this.refreshButton);
			this.flowLayoutPanel1.Controls.Add(this.checkAllButton);
			this.flowLayoutPanel1.Controls.Add(this.uncheckAllButton);
			this.flowLayoutPanel1.Controls.Add(this.selectAllInputButton);
			this.flowLayoutPanel1.Controls.Add(this.deselectAllButton);
			this.flowLayoutPanel1.Controls.Add(this.uncheckSelectedButton);
			this.flowLayoutPanel1.Controls.Add(this.checkSelectedButton);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(791, 39);
			this.flowLayoutPanel1.TabIndex = 22;
			// 
			// refreshButton
			// 
			this.refreshButton.AutoSize = true;
			this.refreshButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.refreshButton.Location = new System.Drawing.Point(3, 3);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(243, 31);
			this.refreshButton.TabIndex = 11;
			this.refreshButton.Text = "Reload Source Camera/Camcorder";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// checkAllButton
			// 
			this.checkAllButton.AutoSize = true;
			this.checkAllButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.checkAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkAllButton.Location = new System.Drawing.Point(253, 4);
			this.checkAllButton.Margin = new System.Windows.Forms.Padding(4);
			this.checkAllButton.Name = "checkAllButton";
			this.checkAllButton.Size = new System.Drawing.Size(81, 31);
			this.checkAllButton.TabIndex = 16;
			this.checkAllButton.Text = "Check All";
			this.checkAllButton.UseVisualStyleBackColor = true;
			this.checkAllButton.Click += new System.EventHandler(this.CheckAllButtonClick);
			// 
			// uncheckAllButton
			// 
			this.uncheckAllButton.AutoSize = true;
			this.uncheckAllButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.uncheckAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.uncheckAllButton.Location = new System.Drawing.Point(342, 4);
			this.uncheckAllButton.Margin = new System.Windows.Forms.Padding(4);
			this.uncheckAllButton.Name = "uncheckAllButton";
			this.uncheckAllButton.Size = new System.Drawing.Size(97, 31);
			this.uncheckAllButton.TabIndex = 15;
			this.uncheckAllButton.Text = "Uncheck All";
			this.uncheckAllButton.UseVisualStyleBackColor = true;
			this.uncheckAllButton.Click += new System.EventHandler(this.UncheckAllButtonClick);
			// 
			// selectAllInputButton
			// 
			this.selectAllInputButton.AutoSize = true;
			this.selectAllInputButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.selectAllInputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.selectAllInputButton.Location = new System.Drawing.Point(447, 4);
			this.selectAllInputButton.Margin = new System.Windows.Forms.Padding(4);
			this.selectAllInputButton.Name = "selectAllInputButton";
			this.selectAllInputButton.Size = new System.Drawing.Size(81, 31);
			this.selectAllInputButton.TabIndex = 13;
			this.selectAllInputButton.Text = "Select All";
			this.selectAllInputButton.UseVisualStyleBackColor = true;
			this.selectAllInputButton.Click += new System.EventHandler(this.SelectAllInputButtonClick);
			// 
			// deselectAllButton
			// 
			this.deselectAllButton.AutoSize = true;
			this.deselectAllButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.deselectAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deselectAllButton.Location = new System.Drawing.Point(536, 4);
			this.deselectAllButton.Margin = new System.Windows.Forms.Padding(4);
			this.deselectAllButton.Name = "deselectAllButton";
			this.deselectAllButton.Size = new System.Drawing.Size(99, 31);
			this.deselectAllButton.TabIndex = 14;
			this.deselectAllButton.Text = "Deselect All";
			this.deselectAllButton.UseVisualStyleBackColor = true;
			this.deselectAllButton.Click += new System.EventHandler(this.DeselectAllButtonClick);
			// 
			// uncheckSelectedButton
			// 
			this.uncheckSelectedButton.AutoSize = true;
			this.uncheckSelectedButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.uncheckSelectedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.uncheckSelectedButton.Location = new System.Drawing.Point(643, 4);
			this.uncheckSelectedButton.Margin = new System.Windows.Forms.Padding(4);
			this.uncheckSelectedButton.Name = "uncheckSelectedButton";
			this.uncheckSelectedButton.Size = new System.Drawing.Size(76, 31);
			this.uncheckSelectedButton.TabIndex = 11;
			this.uncheckSelectedButton.Text = "Uncheck";
			this.uncheckSelectedButton.UseVisualStyleBackColor = true;
			this.uncheckSelectedButton.Click += new System.EventHandler(this.UncheckSelectedButtonClick);
			// 
			// checkSelectedButton
			// 
			this.checkSelectedButton.AutoSize = true;
			this.checkSelectedButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.checkSelectedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.checkSelectedButton.Location = new System.Drawing.Point(727, 4);
			this.checkSelectedButton.Margin = new System.Windows.Forms.Padding(4);
			this.checkSelectedButton.Name = "checkSelectedButton";
			this.checkSelectedButton.Size = new System.Drawing.Size(60, 31);
			this.checkSelectedButton.TabIndex = 12;
			this.checkSelectedButton.Text = "Check";
			this.checkSelectedButton.UseVisualStyleBackColor = true;
			this.checkSelectedButton.Click += new System.EventHandler(this.CheckSelectedButtonClick);
			// 
			// cbDeleteFromSource
			// 
			this.cbDeleteFromSource.AutoSize = true;
			this.cbDeleteFromSource.Location = new System.Drawing.Point(3, 3);
			this.cbDeleteFromSource.Name = "cbDeleteFromSource";
			this.cbDeleteFromSource.Size = new System.Drawing.Size(233, 23);
			this.cbDeleteFromSource.TabIndex = 3;
			this.cbDeleteFromSource.Text = "Delete from source after import";
			this.cbDeleteFromSource.UseVisualStyleBackColor = true;
			this.cbDeleteFromSource.CheckedChanged += new System.EventHandler(this.CbDeleteFromSourceCheckedChanged);
			// 
			// inputListView
			// 
			this.inputListView.CheckBoxes = true;
			this.inputListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputListView.FullRowSelect = true;
			this.inputListView.Location = new System.Drawing.Point(3, 29);
			this.inputListView.Name = "inputListView";
			this.inputListView.Size = new System.Drawing.Size(807, 346);
			this.inputListView.TabIndex = 23;
			this.inputListView.UseCompatibleStateImageBehavior = false;
			this.inputListView.View = System.Windows.Forms.View.Details;
			this.inputListView.ItemActivate += new System.EventHandler(this.InputListViewItemActivate);
			this.inputListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.InputListViewItemChecked);
			this.inputListView.SelectedIndexChanged += new System.EventHandler(this.InputListViewSelectedIndexChanged);
			this.inputListView.DoubleClick += new System.EventHandler(this.InputListViewDoubleClick);
			this.inputListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.InputListViewItemCheck);
			this.inputListView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.InputListViewMouseUp);
			this.inputListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InputListViewMouseDown);
			this.inputListView.MouseLeave += new System.EventHandler(this.InputListViewMouseLeave);
			this.inputListView.Click += new System.EventHandler(this.InputListViewClick);
			// 
			// tlpCopyCommand
			// 
			this.tlpCopyCommand.AutoSize = true;
			this.tlpCopyCommand.ColumnCount = 2;
			this.tlpCopyCommand.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCopyCommand.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpCopyCommand.Controls.Add(this.cbxCopyCommand, 1, 0);
			this.tlpCopyCommand.Controls.Add(this.label2, 0, 0);
			this.tlpCopyCommand.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpCopyCommand.Location = new System.Drawing.Point(3, 3);
			this.tlpCopyCommand.Name = "tlpCopyCommand";
			this.tlpCopyCommand.RowCount = 1;
			this.tlpCopyCommand.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpCopyCommand.Size = new System.Drawing.Size(918, 33);
			this.tlpCopyCommand.TabIndex = 18;
			// 
			// cbxCopyCommand
			// 
			this.cbxCopyCommand.FormattingEnabled = true;
			this.cbxCopyCommand.Location = new System.Drawing.Point(284, 3);
			this.cbxCopyCommand.Name = "cbxCopyCommand";
			this.cbxCopyCommand.Size = new System.Drawing.Size(368, 27);
			this.cbxCopyCommand.TabIndex = 9;
			this.cbxCopyCommand.SelectedIndexChanged += new System.EventHandler(this.CbxCopyCommandSelectedIndexChanged);
			this.cbxCopyCommand.TextChanged += new System.EventHandler(this.CbxCopyCommandTextChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.editToolStripMenuItem,
									this.optionsToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1274, 29);
			this.menuStrip1.TabIndex = 21;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmiListFilesMarkedUnusableBut,
									this.markFilesAsUnusableToolStripMenuItem,
									this.importAndMarkAsUnusableToolStripMenuItem,
									this.deleteAndMarkAsUnusableToolStripMenuItem,
									this.refreshDestinationsToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// tsmiListFilesMarkedUnusableBut
			// 
			this.tsmiListFilesMarkedUnusableBut.Enabled = false;
			this.tsmiListFilesMarkedUnusableBut.Name = "tsmiListFilesMarkedUnusableBut";
			this.tsmiListFilesMarkedUnusableBut.Size = new System.Drawing.Size(686, 26);
			this.tsmiListFilesMarkedUnusableBut.Text = "DEPRECATED List Files that are Marked Unusable but that are in Primary Footage Fo" +
			"lder";
			this.tsmiListFilesMarkedUnusableBut.Click += new System.EventHandler(this.TsmiListFilesMarkedUnusableButClick);
			// 
			// markFilesAsUnusableToolStripMenuItem
			// 
			this.markFilesAsUnusableToolStripMenuItem.Enabled = false;
			this.markFilesAsUnusableToolStripMenuItem.Name = "markFilesAsUnusableToolStripMenuItem";
			this.markFilesAsUnusableToolStripMenuItem.Size = new System.Drawing.Size(686, 26);
			this.markFilesAsUnusableToolStripMenuItem.Text = "DEPRECATED Mark file(s) as unusable...";
			this.markFilesAsUnusableToolStripMenuItem.Click += new System.EventHandler(this.MarkFilesAsUnusableToolStripMenuItemClick);
			// 
			// importAndMarkAsUnusableToolStripMenuItem
			// 
			this.importAndMarkAsUnusableToolStripMenuItem.Enabled = false;
			this.importAndMarkAsUnusableToolStripMenuItem.Name = "importAndMarkAsUnusableToolStripMenuItem";
			this.importAndMarkAsUnusableToolStripMenuItem.Size = new System.Drawing.Size(686, 26);
			this.importAndMarkAsUnusableToolStripMenuItem.Text = "DEPRECATED Import File(s) and Mark as Unusable...";
			this.importAndMarkAsUnusableToolStripMenuItem.Click += new System.EventHandler(this.ImportAndMarkAsUnusableToolStripMenuItemClick);
			// 
			// deleteAndMarkAsUnusableToolStripMenuItem
			// 
			this.deleteAndMarkAsUnusableToolStripMenuItem.Enabled = false;
			this.deleteAndMarkAsUnusableToolStripMenuItem.Name = "deleteAndMarkAsUnusableToolStripMenuItem";
			this.deleteAndMarkAsUnusableToolStripMenuItem.Size = new System.Drawing.Size(686, 26);
			this.deleteAndMarkAsUnusableToolStripMenuItem.Text = "DEPRECATED Delete File(s) and Mark as Unusable...";
			this.deleteAndMarkAsUnusableToolStripMenuItem.Click += new System.EventHandler(this.DeleteAndMarkAsUnusableToolStripMenuItemClick);
			// 
			// refreshDestinationsToolStripMenuItem
			// 
			this.refreshDestinationsToolStripMenuItem.Name = "refreshDestinationsToolStripMenuItem";
			this.refreshDestinationsToolStripMenuItem.Size = new System.Drawing.Size(686, 26);
			this.refreshDestinationsToolStripMenuItem.Text = "Refresh Destinations";
			this.refreshDestinationsToolStripMenuItem.Click += new System.EventHandler(this.RefreshDestinationsToolStripMenuItemClick);
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.cutToolStripMenuItem,
									this.copyToolStripMenuItem,
									this.pasteToolStripMenuItem,
									this.pasteAfterToolStripMenuItem,
									this.deleteToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(48, 25);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
			this.cutToolStripMenuItem.Text = "C&ut";
			this.cutToolStripMenuItem.Click += new System.EventHandler(this.CutToolStripMenuItemClick);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
			this.copyToolStripMenuItem.Text = "&Copy";
			this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyToolStripMenuItemClick);
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
			this.pasteToolStripMenuItem.Text = "&Paste";
			this.pasteToolStripMenuItem.Click += new System.EventHandler(this.PasteToolStripMenuItemClick);
			// 
			// pasteAfterToolStripMenuItem
			// 
			this.pasteAfterToolStripMenuItem.Name = "pasteAfterToolStripMenuItem";
			this.pasteAfterToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
			this.pasteAfterToolStripMenuItem.Text = "Paste &After";
			this.pasteAfterToolStripMenuItem.Click += new System.EventHandler(this.PasteAfterToolStripMenuItemClick);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
			this.deleteToolStripMenuItem.Text = "&Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.allowMarkingUnusableWithoutReasonToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(77, 25);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// allowMarkingUnusableWithoutReasonToolStripMenuItem
			// 
			this.allowMarkingUnusableWithoutReasonToolStripMenuItem.CheckOnClick = true;
			this.allowMarkingUnusableWithoutReasonToolStripMenuItem.Name = "allowMarkingUnusableWithoutReasonToolStripMenuItem";
			this.allowMarkingUnusableWithoutReasonToolStripMenuItem.Size = new System.Drawing.Size(365, 26);
			this.allowMarkingUnusableWithoutReasonToolStripMenuItem.Text = "Allow Marking Unusable Without Reason";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.saveOutputToolStripMenuItem,
									this.copyStatusToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(54, 25);
			this.helpToolStripMenuItem.Text = "&Help";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItemClick);
			// 
			// saveOutputToolStripMenuItem
			// 
			this.saveOutputToolStripMenuItem.Name = "saveOutputToolStripMenuItem";
			this.saveOutputToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
			this.saveOutputToolStripMenuItem.Text = "Save &Batch";
			this.saveOutputToolStripMenuItem.Click += new System.EventHandler(this.SaveOutputToolStripMenuItemClick);
			// 
			// copyStatusToolStripMenuItem
			// 
			this.copyStatusToolStripMenuItem.Name = "copyStatusToolStripMenuItem";
			this.copyStatusToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
									| System.Windows.Forms.Keys.C)));
			this.copyStatusToolStripMenuItem.Size = new System.Drawing.Size(261, 26);
			this.copyStatusToolStripMenuItem.Text = "Copy &Status";
			this.copyStatusToolStripMenuItem.Click += new System.EventHandler(this.CopyStatusToolStripMenuItemClick);
			// 
			// tableLayoutPanelOutput
			// 
			this.tableLayoutPanelOutput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanelOutput.ColumnCount = 1;
			this.tableLayoutPanelOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelOutput.Controls.Add(this.flowLayoutPanelButtonsBelowOutputList, 0, 3);
			this.tableLayoutPanelOutput.Controls.Add(this.flowLayoutPanelButtonsAboveOutputList, 0, 1);
			this.tableLayoutPanelOutput.Controls.Add(this.tlpCopyCommand, 0, 0);
			this.tableLayoutPanelOutput.Controls.Add(this.outputListBox, 0, 2);
			this.tableLayoutPanelOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelOutput.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelOutput.Name = "tableLayoutPanelOutput";
			this.tableLayoutPanelOutput.RowCount = 4;
			this.tableLayoutPanelOutput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanelOutput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanelOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelOutput.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanelOutput.Size = new System.Drawing.Size(924, 270);
			this.tableLayoutPanelOutput.TabIndex = 22;
			// 
			// flowLayoutPanelButtonsBelowOutputList
			// 
			this.flowLayoutPanelButtonsBelowOutputList.AutoSize = true;
			this.flowLayoutPanelButtonsBelowOutputList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanelButtonsBelowOutputList.Controls.Add(this.runBatchButton);
			this.flowLayoutPanelButtonsBelowOutputList.Controls.Add(this.deleteOutputButton);
			this.flowLayoutPanelButtonsBelowOutputList.Controls.Add(this.copyOutputButton);
			this.flowLayoutPanelButtonsBelowOutputList.Controls.Add(this.pasteOutputButton);
			this.flowLayoutPanelButtonsBelowOutputList.Controls.Add(this.outputClearAllButton);
			this.flowLayoutPanelButtonsBelowOutputList.Location = new System.Drawing.Point(3, 228);
			this.flowLayoutPanelButtonsBelowOutputList.Name = "flowLayoutPanelButtonsBelowOutputList";
			this.flowLayoutPanelButtonsBelowOutputList.Size = new System.Drawing.Size(381, 39);
			this.flowLayoutPanelButtonsBelowOutputList.TabIndex = 19;
			// 
			// runBatchButton
			// 
			this.runBatchButton.AutoSize = true;
			this.runBatchButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.runBatchButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.runBatchButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.runBatchButton.Location = new System.Drawing.Point(4, 4);
			this.runBatchButton.Margin = new System.Windows.Forms.Padding(4);
			this.runBatchButton.Name = "runBatchButton";
			this.runBatchButton.Size = new System.Drawing.Size(91, 31);
			this.runBatchButton.TabIndex = 14;
			this.runBatchButton.Text = "Run Batch";
			this.runBatchButton.UseVisualStyleBackColor = true;
			this.runBatchButton.Click += new System.EventHandler(this.RunBatchButtonClick);
			// 
			// deleteOutputButton
			// 
			this.deleteOutputButton.AutoSize = true;
			this.deleteOutputButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.deleteOutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.deleteOutputButton.Location = new System.Drawing.Point(103, 4);
			this.deleteOutputButton.Margin = new System.Windows.Forms.Padding(4);
			this.deleteOutputButton.Name = "deleteOutputButton";
			this.deleteOutputButton.Size = new System.Drawing.Size(64, 31);
			this.deleteOutputButton.TabIndex = 11;
			this.deleteOutputButton.Text = "Delete";
			this.deleteOutputButton.UseVisualStyleBackColor = true;
			this.deleteOutputButton.Click += new System.EventHandler(this.DeleteOutputButtonClick);
			// 
			// copyOutputButton
			// 
			this.copyOutputButton.AutoSize = true;
			this.copyOutputButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.copyOutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.copyOutputButton.Location = new System.Drawing.Point(175, 4);
			this.copyOutputButton.Margin = new System.Windows.Forms.Padding(4);
			this.copyOutputButton.Name = "copyOutputButton";
			this.copyOutputButton.Size = new System.Drawing.Size(53, 31);
			this.copyOutputButton.TabIndex = 12;
			this.copyOutputButton.Text = "Copy";
			this.copyOutputButton.UseVisualStyleBackColor = true;
			this.copyOutputButton.Click += new System.EventHandler(this.CopyOutputButtonClick);
			// 
			// pasteOutputButton
			// 
			this.pasteOutputButton.AutoSize = true;
			this.pasteOutputButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.pasteOutputButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.pasteOutputButton.Location = new System.Drawing.Point(236, 4);
			this.pasteOutputButton.Margin = new System.Windows.Forms.Padding(4);
			this.pasteOutputButton.Name = "pasteOutputButton";
			this.pasteOutputButton.Size = new System.Drawing.Size(57, 31);
			this.pasteOutputButton.TabIndex = 13;
			this.pasteOutputButton.Text = "Paste";
			this.pasteOutputButton.UseVisualStyleBackColor = true;
			this.pasteOutputButton.Click += new System.EventHandler(this.PasteOutputButtonClick);
			// 
			// outputClearAllButton
			// 
			this.outputClearAllButton.AutoSize = true;
			this.outputClearAllButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.outputClearAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.outputClearAllButton.Location = new System.Drawing.Point(301, 4);
			this.outputClearAllButton.Margin = new System.Windows.Forms.Padding(4);
			this.outputClearAllButton.Name = "outputClearAllButton";
			this.outputClearAllButton.Size = new System.Drawing.Size(76, 31);
			this.outputClearAllButton.TabIndex = 15;
			this.outputClearAllButton.Text = "Clear All";
			this.outputClearAllButton.UseVisualStyleBackColor = true;
			this.outputClearAllButton.Click += new System.EventHandler(this.OutputClearAllButtonClick);
			// 
			// flowLayoutPanelButtonsAboveOutputList
			// 
			this.flowLayoutPanelButtonsAboveOutputList.AutoSize = true;
			this.flowLayoutPanelButtonsAboveOutputList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanelButtonsAboveOutputList.Controls.Add(this.addCheckedFilesButton);
			this.flowLayoutPanelButtonsAboveOutputList.Controls.Add(this.markDoneButton);
			this.flowLayoutPanelButtonsAboveOutputList.Location = new System.Drawing.Point(3, 42);
			this.flowLayoutPanelButtonsAboveOutputList.Name = "flowLayoutPanelButtonsAboveOutputList";
			this.flowLayoutPanelButtonsAboveOutputList.Size = new System.Drawing.Size(286, 39);
			this.flowLayoutPanelButtonsAboveOutputList.TabIndex = 10;
			// 
			// markDoneButton
			// 
			this.markDoneButton.AutoSize = true;
			this.markDoneButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.markDoneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.markDoneButton.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.markDoneButton.Location = new System.Drawing.Point(145, 4);
			this.markDoneButton.Margin = new System.Windows.Forms.Padding(4);
			this.markDoneButton.Name = "markDoneButton";
			this.markDoneButton.Size = new System.Drawing.Size(137, 31);
			this.markDoneButton.TabIndex = 11;
			this.markDoneButton.Text = "Mark (0) as Done";
			this.markDoneButton.UseVisualStyleBackColor = true;
			this.markDoneButton.Click += new System.EventHandler(this.MarkDoneButtonClick);
			// 
			// outputListBox
			// 
			this.outputListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputListBox.FormattingEnabled = true;
			this.outputListBox.ItemHeight = 19;
			this.outputListBox.Location = new System.Drawing.Point(4, 88);
			this.outputListBox.Margin = new System.Windows.Forms.Padding(4);
			this.outputListBox.Name = "outputListBox";
			this.outputListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.outputListBox.Size = new System.Drawing.Size(916, 118);
			this.outputListBox.TabIndex = 9;
			this.outputListBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.OutputListBoxMouseClick);
			// 
			// splitContainerMainInAndOut
			// 
			this.splitContainerMainInAndOut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainerMainInAndOut.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMainInAndOut.Location = new System.Drawing.Point(0, 29);
			this.splitContainerMainInAndOut.Name = "splitContainerMainInAndOut";
			this.splitContainerMainInAndOut.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerMainInAndOut.Panel1
			// 
			this.splitContainerMainInAndOut.Panel1.Controls.Add(this.formUpperOuterTableLayoutPanel);
			// 
			// splitContainerMainInAndOut.Panel2
			// 
			this.splitContainerMainInAndOut.Panel2.Controls.Add(this.splitContainer1);
			this.splitContainerMainInAndOut.Size = new System.Drawing.Size(1274, 664);
			this.splitContainerMainInAndOut.SplitterDistance = 388;
			this.splitContainerMainInAndOut.TabIndex = 23;
			this.splitContainerMainInAndOut.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainerMainInAndOutSplitterMoved);
			// 
			// formUpperOuterTableLayoutPanel
			// 
			this.formUpperOuterTableLayoutPanel.ColumnCount = 1;
			this.formUpperOuterTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.formUpperOuterTableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.formUpperOuterTableLayoutPanel.Controls.Add(this.formUpperSplitContainer, 0, 1);
			this.formUpperOuterTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.formUpperOuterTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.formUpperOuterTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.formUpperOuterTableLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
			this.formUpperOuterTableLayoutPanel.Name = "formUpperOuterTableLayoutPanel";
			this.formUpperOuterTableLayoutPanel.RowCount = 2;
			this.formUpperOuterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formUpperOuterTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formUpperOuterTableLayoutPanel.Size = new System.Drawing.Size(1272, 386);
			this.formUpperOuterTableLayoutPanel.TabIndex = 22;
			// 
			// formUpperSplitContainer
			// 
			this.formUpperSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.formUpperSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.formUpperSplitContainer.Location = new System.Drawing.Point(3, 48);
			this.formUpperSplitContainer.Name = "formUpperSplitContainer";
			// 
			// formUpperSplitContainer.Panel1
			// 
			this.formUpperSplitContainer.Panel1.Controls.Add(this.inputSettingsScrollPanel);
			// 
			// formUpperSplitContainer.Panel2
			// 
			this.formUpperSplitContainer.Panel2.Controls.Add(this.inputListTableLayoutPanel);
			this.formUpperSplitContainer.Size = new System.Drawing.Size(1266, 380);
			this.formUpperSplitContainer.SplitterDistance = 447;
			this.formUpperSplitContainer.TabIndex = 21;
			// 
			// inputSettingsScrollPanel
			// 
			this.inputSettingsScrollPanel.AutoScroll = true;
			this.inputSettingsScrollPanel.Controls.Add(this.formUpperLeftOuterSettingsTableLayoutPanel);
			this.inputSettingsScrollPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputSettingsScrollPanel.Location = new System.Drawing.Point(0, 0);
			this.inputSettingsScrollPanel.Margin = new System.Windows.Forms.Padding(2);
			this.inputSettingsScrollPanel.Name = "inputSettingsScrollPanel";
			this.inputSettingsScrollPanel.Size = new System.Drawing.Size(445, 378);
			this.inputSettingsScrollPanel.TabIndex = 17;
			// 
			// formUpperLeftOuterSettingsTableLayoutPanel
			// 
			this.formUpperLeftOuterSettingsTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.formUpperLeftOuterSettingsTableLayoutPanel.ColumnCount = 1;
			this.formUpperLeftOuterSettingsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.formUpperLeftOuterSettingsTableLayoutPanel.Controls.Add(this.btnSaveCamPrefix, 0, 2);
			this.formUpperLeftOuterSettingsTableLayoutPanel.Controls.Add(this.formInputSettingsTableLayoutPanel, 0, 1);
			this.formUpperLeftOuterSettingsTableLayoutPanel.Controls.Add(this.flowLayoutPanel2, 0, 0);
			this.formUpperLeftOuterSettingsTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.formUpperLeftOuterSettingsTableLayoutPanel.Margin = new System.Windows.Forms.Padding(2);
			this.formUpperLeftOuterSettingsTableLayoutPanel.Name = "formUpperLeftOuterSettingsTableLayoutPanel";
			this.formUpperLeftOuterSettingsTableLayoutPanel.RowCount = 3;
			this.formUpperLeftOuterSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formUpperLeftOuterSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.formUpperLeftOuterSettingsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.formUpperLeftOuterSettingsTableLayoutPanel.Size = new System.Drawing.Size(443, 330);
			this.formUpperLeftOuterSettingsTableLayoutPanel.TabIndex = 23;
			this.formUpperLeftOuterSettingsTableLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FormUpperLeftOuterSettingsTableLayoutPanelPaint);
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.AutoSize = true;
			this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel2.Controls.Add(this.cbDeleteFromSource);
			this.flowLayoutPanel2.Controls.Add(this.prefixCheckBox);
			this.flowLayoutPanel2.Location = new System.Drawing.Point(2, 2);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(430, 29);
			this.flowLayoutPanel2.TabIndex = 17;
			// 
			// prefixCheckBox
			// 
			this.prefixCheckBox.AutoSize = true;
			this.prefixCheckBox.Checked = true;
			this.prefixCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.prefixCheckBox.Location = new System.Drawing.Point(242, 3);
			this.prefixCheckBox.Name = "prefixCheckBox";
			this.prefixCheckBox.Size = new System.Drawing.Size(185, 23);
			this.prefixCheckBox.TabIndex = 4;
			this.prefixCheckBox.Text = "name with project prefix";
			this.prefixCheckBox.UseVisualStyleBackColor = true;
			// 
			// inputListTableLayoutPanel
			// 
			this.inputListTableLayoutPanel.ColumnCount = 1;
			this.inputListTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.inputListTableLayoutPanel.Controls.Add(this.label1, 0, 0);
			this.inputListTableLayoutPanel.Controls.Add(this.inputListView, 0, 1);
			this.inputListTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.inputListTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.inputListTableLayoutPanel.Name = "inputListTableLayoutPanel";
			this.inputListTableLayoutPanel.RowCount = 2;
			this.inputListTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.inputListTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.inputListTableLayoutPanel.Size = new System.Drawing.Size(813, 378);
			this.inputListTableLayoutPanel.TabIndex = 24;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(399, 26);
			this.label1.TabIndex = 24;
			this.label1.Text = "Files Detected on Source Camera/Camcorder:";
			// 
			// splitContainer1
			// 
			this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanelControlsLeftOfOutputBox);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanelOutput);
			this.splitContainer1.Size = new System.Drawing.Size(1274, 272);
			this.splitContainer1.SplitterDistance = 344;
			this.splitContainer1.TabIndex = 23;
			// 
			// flowLayoutPanelControlsLeftOfOutputBox
			// 
			this.flowLayoutPanelControlsLeftOfOutputBox.Controls.Add(this.saveCamSettingsButton);
			this.flowLayoutPanelControlsLeftOfOutputBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanelControlsLeftOfOutputBox.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelControlsLeftOfOutputBox.Name = "flowLayoutPanelControlsLeftOfOutputBox";
			this.flowLayoutPanelControlsLeftOfOutputBox.Size = new System.Drawing.Size(342, 270);
			this.flowLayoutPanelControlsLeftOfOutputBox.TabIndex = 24;
			// 
			// saveCamSettingsButton
			// 
			this.saveCamSettingsButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.saveCamSettingsButton.AutoSize = true;
			this.saveCamSettingsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.saveCamSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.saveCamSettingsButton.Location = new System.Drawing.Point(6, 5);
			this.saveCamSettingsButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
			this.saveCamSettingsButton.Name = "saveCamSettingsButton";
			this.saveCamSettingsButton.Size = new System.Drawing.Size(140, 31);
			this.saveCamSettingsButton.TabIndex = 2;
			this.saveCamSettingsButton.Text = "Save Cam Settings";
			this.saveCamSettingsButton.UseVisualStyleBackColor = true;
			this.saveCamSettingsButton.Click += new System.EventHandler(this.SaveCamSettingsButtonClick);
			// 
			// progressBar1
			// 
			this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progressBar1.Location = new System.Drawing.Point(0, 726);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(1274, 23);
			this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.progressBar1.TabIndex = 24;
			// 
			// initTimer
			// 
			this.initTimer.Interval = 1;
			this.initTimer.Tick += new System.EventHandler(this.InitTimerTick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1274, 749);
			this.Controls.Add(this.splitContainerMainInAndOut);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.tbStatus);
			this.Controls.Add(this.progressBar1);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "MainForm";
			this.Text = "CamOrder";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.Shown += new System.EventHandler(this.MainFormShown);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.formInputSettingsTableLayoutPanel.ResumeLayout(false);
			this.formInputSettingsTableLayoutPanel.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.tlpCopyCommand.ResumeLayout(false);
			this.tlpCopyCommand.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tableLayoutPanelOutput.ResumeLayout(false);
			this.tableLayoutPanelOutput.PerformLayout();
			this.flowLayoutPanelButtonsBelowOutputList.ResumeLayout(false);
			this.flowLayoutPanelButtonsBelowOutputList.PerformLayout();
			this.flowLayoutPanelButtonsAboveOutputList.ResumeLayout(false);
			this.flowLayoutPanelButtonsAboveOutputList.PerformLayout();
			this.splitContainerMainInAndOut.Panel1.ResumeLayout(false);
			this.splitContainerMainInAndOut.Panel2.ResumeLayout(false);
			this.splitContainerMainInAndOut.ResumeLayout(false);
			this.formUpperOuterTableLayoutPanel.ResumeLayout(false);
			this.formUpperOuterTableLayoutPanel.PerformLayout();
			this.formUpperSplitContainer.Panel1.ResumeLayout(false);
			this.formUpperSplitContainer.Panel2.ResumeLayout(false);
			this.formUpperSplitContainer.ResumeLayout(false);
			this.inputSettingsScrollPanel.ResumeLayout(false);
			this.formUpperLeftOuterSettingsTableLayoutPanel.ResumeLayout(false);
			this.formUpperLeftOuterSettingsTableLayoutPanel.PerformLayout();
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.inputListTableLayoutPanel.ResumeLayout(false);
			this.inputListTableLayoutPanel.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.flowLayoutPanelControlsLeftOfOutputBox.ResumeLayout(false);
			this.flowLayoutPanelControlsLeftOfOutputBox.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ComboBox cbFolderIn;
		private System.Windows.Forms.Button saveCamSettingsButton;
		private System.Windows.Forms.ToolStripMenuItem allowMarkingUnusableWithoutReasonToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.TextBox unusableTextBox;
		private System.Windows.Forms.Button markUnusableButton;
		private System.Windows.Forms.ToolStripMenuItem refreshDestinationsToolStripMenuItem;
		private System.Windows.Forms.Button markDoneButton;
		private System.Windows.Forms.Timer initTimer;
		private System.Windows.Forms.CheckBox prefixCheckBox;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button outputClearAllButton;
		private System.Windows.Forms.ToolStripMenuItem copyStatusToolStripMenuItem;
		private System.Windows.Forms.TextBox tbAppDataFolder;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.TableLayoutPanel formUpperLeftOuterSettingsTableLayoutPanel;
		private System.Windows.Forms.TableLayoutPanel formUpperOuterTableLayoutPanel;
		private System.Windows.Forms.Panel inputSettingsScrollPanel;
		private System.Windows.Forms.TableLayoutPanel formInputSettingsTableLayoutPanel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtonsAboveOutputList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtonsBelowOutputList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelControlsLeftOfOutputBox;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel inputListTableLayoutPanel;
		private System.Windows.Forms.SplitContainer formUpperSplitContainer;
		private System.Windows.Forms.Label cameraNameLabel;
		private System.Windows.Forms.Button checkAllButton;
		private System.Windows.Forms.Button uncheckAllButton;
		private System.Windows.Forms.Button deselectAllButton;
		private System.Windows.Forms.Button selectAllInputButton;
		private System.Windows.Forms.ToolStripMenuItem pasteAfterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.Button runBatchButton;
		private System.Windows.Forms.Button checkSelectedButton;
		private System.Windows.Forms.Button uncheckSelectedButton;
		private System.Windows.Forms.Button addCheckedFilesButton;
		private System.Windows.Forms.ListBox outputListBox;
		private System.Windows.Forms.Button deleteOutputButton;
		private System.Windows.Forms.Button pasteOutputButton;
		private System.Windows.Forms.Button copyOutputButton;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOutput;
		private System.Windows.Forms.ListView inputListView;
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.SplitContainer splitContainerMainInAndOut;
		private System.Windows.Forms.ComboBox cbxCopyCommand;
		private System.Windows.Forms.CheckBox cbDeleteFromSource;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label lblUnusable;
		private System.Windows.Forms.ToolStripMenuItem deleteAndMarkAsUnusableToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem importAndMarkAsUnusableToolStripMenuItem;
		private System.Windows.Forms.TextBox tbProjectName;
		private System.Windows.Forms.OpenFileDialog openfiledlgMain;
		private System.Windows.Forms.ToolStripMenuItem markFilesAsUnusableToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog savefiledlgMain;
		private System.Windows.Forms.ToolStripMenuItem tsmiListFilesMarkedUnusableBut;
		private System.Windows.Forms.ToolStripMenuItem saveOutputToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.TableLayoutPanel tlpCopyCommand;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnSaveCamPrefix;
		private System.Windows.Forms.Label lblTag;
		private System.Windows.Forms.TextBox tbSuffix;
		private System.Windows.Forms.TextBox tbStatus;
		private System.Windows.Forms.Label lblFormatHelp;
		private System.Windows.Forms.TextBox tbFolderOut;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbPrependCam;
		private System.Windows.Forms.TextBox tbDTFormatString;
		private System.Windows.Forms.Button btnInputBrowse;
		private System.Windows.Forms.Button btnOutputBrowse;
		private System.Windows.Forms.FolderBrowserDialog folderbrowserdlgMain;
	}
}
