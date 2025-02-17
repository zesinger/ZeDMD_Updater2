using System.Drawing;
using System.Windows.Forms;

namespace ZeDMD_Updater2
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonFFile = new System.Windows.Forms.Button();
            this.versionList = new System.Windows.Forms.ListBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.LatestVersion = new System.Windows.Forms.TextBox();
            this.buttonFlash = new System.Windows.Forms.Button();
            this.deviceView = new System.Windows.Forms.ListView();
            this.columnCOM = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnS3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLilygo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnWidth = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeight = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textSsid = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUDelay = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.radioWTcp = new System.Windows.Forms.RadioButton();
            this.radioWUdp = new System.Windows.Forms.RadioButton();
            this.radioUsb = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUPSize = new System.Windows.Forms.NumericUpDown();
            this.buttonSNParameters = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.numericROrder = new System.Windows.Forms.NumericUpDown();
            this.numericBrightness = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericPDriver = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.radio12832 = new System.Windows.Forms.RadioButton();
            this.radio12864 = new System.Windows.Forms.RadioButton();
            this.radio25664 = new System.Windows.Forms.RadioButton();
            this.numericPCPhase = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numericPLBlanking = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericPMRRate = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.numericPISpeed = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericOY = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonLTest = new System.Windows.Forms.Button();
            this.buttonRescan = new System.Windows.Forms.Button();
            this.textDescription = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUPSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericROrder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPCPhase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPLBlanking)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPMRRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPISpeed)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericOY)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonFFile
            // 
            this.buttonFFile.Enabled = false;
            this.buttonFFile.Location = new System.Drawing.Point(431, 12);
            this.buttonFFile.Name = "buttonFFile";
            this.buttonFFile.Size = new System.Drawing.Size(200, 27);
            this.buttonFFile.TabIndex = 36;
            this.buttonFFile.Text = "Flash from a file";
            this.buttonFFile.UseVisualStyleBackColor = true;
            this.buttonFFile.Click += new System.EventHandler(this.buttonFFile_Click);
            // 
            // versionList
            // 
            this.versionList.FormattingEnabled = true;
            this.versionList.Location = new System.Drawing.Point(431, 87);
            this.versionList.Name = "versionList";
            this.versionList.Size = new System.Drawing.Size(200, 173);
            this.versionList.TabIndex = 30;
            this.versionList.SelectedIndexChanged += new System.EventHandler(this.versionList_SelectedIndexChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(431, 56);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(200, 20);
            this.textBox3.TabIndex = 29;
            this.textBox3.Text = "Online available versions :";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LatestVersion
            // 
            this.LatestVersion.Location = new System.Drawing.Point(12, 12);
            this.LatestVersion.Name = "LatestVersion";
            this.LatestVersion.ReadOnly = true;
            this.LatestVersion.Size = new System.Drawing.Size(405, 20);
            this.LatestVersion.TabIndex = 26;
            this.LatestVersion.Text = "Detected devices :";
            this.LatestVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonFlash
            // 
            this.buttonFlash.Enabled = false;
            this.buttonFlash.Location = new System.Drawing.Point(431, 336);
            this.buttonFlash.Name = "buttonFlash";
            this.buttonFlash.Size = new System.Drawing.Size(200, 23);
            this.buttonFlash.TabIndex = 25;
            this.buttonFlash.Text = "Download and flash";
            this.buttonFlash.UseVisualStyleBackColor = true;
            this.buttonFlash.Click += new System.EventHandler(this.buttonFlash_Click);
            // 
            // deviceView
            // 
            this.deviceView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnCOM,
            this.columnType,
            this.columnS3,
            this.columnLilygo,
            this.columnVersion,
            this.columnWidth,
            this.columnHeight});
            this.deviceView.FullRowSelect = true;
            this.deviceView.GridLines = true;
            this.deviceView.HideSelection = false;
            this.deviceView.Location = new System.Drawing.Point(13, 43);
            this.deviceView.MultiSelect = false;
            this.deviceView.Name = "deviceView";
            this.deviceView.Size = new System.Drawing.Size(404, 271);
            this.deviceView.TabIndex = 37;
            this.deviceView.UseCompatibleStateImageBehavior = false;
            this.deviceView.View = System.Windows.Forms.View.Details;
            this.deviceView.SelectedIndexChanged += new System.EventHandler(this.deviceView_SelectedIndexChanged);
            // 
            // columnCOM
            // 
            this.columnCOM.Text = "COM";
            // 
            // columnType
            // 
            this.columnType.Text = "Type";
            this.columnType.Width = 80;
            // 
            // columnS3
            // 
            this.columnS3.Text = "S3";
            this.columnS3.Width = 40;
            // 
            // columnLilygo
            // 
            this.columnLilygo.Text = "Lilygo";
            this.columnLilygo.Width = 40;
            // 
            // columnVersion
            // 
            this.columnVersion.Text = "Version";
            this.columnVersion.Width = 80;
            // 
            // columnWidth
            // 
            this.columnWidth.Text = "Width";
            this.columnWidth.Width = 50;
            // 
            // columnHeight
            // 
            this.columnHeight.Text = "Height";
            this.columnHeight.Width = 50;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textSsid);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUDelay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radioWTcp);
            this.groupBox1.Controls.Add(this.radioWUdp);
            this.groupBox1.Location = new System.Drawing.Point(647, 43);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 175);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "WiFi";
            // 
            // textPassword
            // 
            this.textPassword.Enabled = false;
            this.textPassword.Location = new System.Drawing.Point(9, 141);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(170, 20);
            this.textPassword.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(62, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password :";
            // 
            // textSsid
            // 
            this.textSsid.Enabled = false;
            this.textSsid.Location = new System.Drawing.Point(9, 102);
            this.textSsid.Name = "textSsid";
            this.textSsid.Size = new System.Drawing.Size(170, 20);
            this.textSsid.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "SSID :";
            // 
            // numericUDelay
            // 
            this.numericUDelay.Enabled = false;
            this.numericUDelay.Location = new System.Drawing.Point(9, 52);
            this.numericUDelay.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUDelay.Name = "numericUDelay";
            this.numericUDelay.ReadOnly = true;
            this.numericUDelay.Size = new System.Drawing.Size(41, 20);
            this.numericUDelay.TabIndex = 3;
            this.numericUDelay.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericUDelay.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "UDP delay :";
            // 
            // radioWTcp
            // 
            this.radioWTcp.AutoSize = true;
            this.radioWTcp.Location = new System.Drawing.Point(134, 16);
            this.radioWTcp.Name = "radioWTcp";
            this.radioWTcp.Size = new System.Drawing.Size(46, 17);
            this.radioWTcp.TabIndex = 1;
            this.radioWTcp.TabStop = true;
            this.radioWTcp.Text = "TCP";
            this.radioWTcp.UseVisualStyleBackColor = true;
            this.radioWTcp.CheckedChanged += new System.EventHandler(this.radioWTcp_CheckedChanged);
            // 
            // radioWUdp
            // 
            this.radioWUdp.AutoSize = true;
            this.radioWUdp.Location = new System.Drawing.Point(6, 16);
            this.radioWUdp.Name = "radioWUdp";
            this.radioWUdp.Size = new System.Drawing.Size(48, 17);
            this.radioWUdp.TabIndex = 0;
            this.radioWUdp.TabStop = true;
            this.radioWUdp.Text = "UDP";
            this.radioWUdp.UseVisualStyleBackColor = true;
            this.radioWUdp.CheckedChanged += new System.EventHandler(this.radioWUdp_CheckedChanged);
            // 
            // radioUsb
            // 
            this.radioUsb.AutoSize = true;
            this.radioUsb.Checked = true;
            this.radioUsb.Location = new System.Drawing.Point(656, 224);
            this.radioUsb.Name = "radioUsb";
            this.radioUsb.Size = new System.Drawing.Size(47, 17);
            this.radioUsb.TabIndex = 40;
            this.radioUsb.TabStop = true;
            this.radioUsb.Text = "USB";
            this.radioUsb.UseVisualStyleBackColor = true;
            this.radioUsb.CheckedChanged += new System.EventHandler(this.radioUsb_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(653, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "USB package size :";
            // 
            // numericUPSize
            // 
            this.numericUPSize.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUPSize.Location = new System.Drawing.Point(751, 241);
            this.numericUPSize.Maximum = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.numericUPSize.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericUPSize.Name = "numericUPSize";
            this.numericUPSize.ReadOnly = true;
            this.numericUPSize.Size = new System.Drawing.Size(56, 20);
            this.numericUPSize.TabIndex = 8;
            this.numericUPSize.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericUPSize.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // buttonSNParameters
            // 
            this.buttonSNParameters.Enabled = false;
            this.buttonSNParameters.Location = new System.Drawing.Point(712, 336);
            this.buttonSNParameters.Name = "buttonSNParameters";
            this.buttonSNParameters.Size = new System.Drawing.Size(172, 23);
            this.buttonSNParameters.TabIndex = 41;
            this.buttonSNParameters.Text = "Set new parameters";
            this.buttonSNParameters.UseVisualStyleBackColor = true;
            this.buttonSNParameters.Click += new System.EventHandler(this.buttonSNParameters_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(653, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 42;
            this.label5.Text = "RGB order :";
            // 
            // numericROrder
            // 
            this.numericROrder.Location = new System.Drawing.Point(656, 303);
            this.numericROrder.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericROrder.Name = "numericROrder";
            this.numericROrder.ReadOnly = true;
            this.numericROrder.Size = new System.Drawing.Size(41, 20);
            this.numericROrder.TabIndex = 8;
            this.numericROrder.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // numericBrightness
            // 
            this.numericBrightness.Location = new System.Drawing.Point(770, 303);
            this.numericBrightness.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericBrightness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericBrightness.Name = "numericBrightness";
            this.numericBrightness.ReadOnly = true;
            this.numericBrightness.Size = new System.Drawing.Size(41, 20);
            this.numericBrightness.TabIndex = 43;
            this.numericBrightness.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericBrightness.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(767, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Brightness :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(647, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(299, 20);
            this.textBox1.TabIndex = 45;
            this.textBox1.Text = "Parameters :";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericPDriver
            // 
            this.numericPDriver.Location = new System.Drawing.Point(15, 41);
            this.numericPDriver.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericPDriver.Name = "numericPDriver";
            this.numericPDriver.ReadOnly = true;
            this.numericPDriver.Size = new System.Drawing.Size(41, 20);
            this.numericPDriver.TabIndex = 46;
            this.numericPDriver.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 47;
            this.label7.Text = "Driver :";
            // 
            // radio12832
            // 
            this.radio12832.AutoSize = true;
            this.radio12832.Checked = true;
            this.radio12832.Location = new System.Drawing.Point(8, 14);
            this.radio12832.Name = "radio12832";
            this.radio12832.Size = new System.Drawing.Size(60, 17);
            this.radio12832.TabIndex = 48;
            this.radio12832.TabStop = true;
            this.radio12832.Text = "128x32";
            this.radio12832.UseVisualStyleBackColor = true;
            this.radio12832.CheckedChanged += new System.EventHandler(this.radio12832_CheckedChanged);
            // 
            // radio12864
            // 
            this.radio12864.AutoSize = true;
            this.radio12864.Location = new System.Drawing.Point(74, 14);
            this.radio12864.Name = "radio12864";
            this.radio12864.Size = new System.Drawing.Size(60, 17);
            this.radio12864.TabIndex = 49;
            this.radio12864.Text = "128x64";
            this.radio12864.UseVisualStyleBackColor = true;
            this.radio12864.CheckedChanged += new System.EventHandler(this.radio12864_CheckedChanged);
            // 
            // radio25664
            // 
            this.radio25664.AutoSize = true;
            this.radio25664.Location = new System.Drawing.Point(140, 14);
            this.radio25664.Name = "radio25664";
            this.radio25664.Size = new System.Drawing.Size(60, 17);
            this.radio25664.TabIndex = 50;
            this.radio25664.Text = "256x64";
            this.radio25664.UseVisualStyleBackColor = true;
            this.radio25664.CheckedChanged += new System.EventHandler(this.radio25664_CheckedChanged);
            // 
            // numericPCPhase
            // 
            this.numericPCPhase.Location = new System.Drawing.Point(15, 89);
            this.numericPCPhase.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPCPhase.Name = "numericPCPhase";
            this.numericPCPhase.ReadOnly = true;
            this.numericPCPhase.Size = new System.Drawing.Size(41, 20);
            this.numericPCPhase.TabIndex = 51;
            this.numericPCPhase.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Clock phase :";
            // 
            // numericPLBlanking
            // 
            this.numericPLBlanking.Location = new System.Drawing.Point(15, 186);
            this.numericPLBlanking.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericPLBlanking.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPLBlanking.Name = "numericPLBlanking";
            this.numericPLBlanking.ReadOnly = true;
            this.numericPLBlanking.Size = new System.Drawing.Size(41, 20);
            this.numericPLBlanking.TabIndex = 53;
            this.numericPLBlanking.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericPLBlanking.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericPLBlanking.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Latch blanking :";
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Location = new System.Drawing.Point(647, 274);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(188, 2);
            this.label10.TabIndex = 55;
            this.label10.Text = "label10";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericPMRRate);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.numericPISpeed);
            this.groupBox2.Controls.Add(this.numericPLBlanking);
            this.groupBox2.Controls.Add(this.numericPCPhase);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.numericPDriver);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(841, 43);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(105, 270);
            this.groupBox2.TabIndex = 56;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Panel";
            // 
            // numericPMRRate
            // 
            this.numericPMRRate.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericPMRRate.Location = new System.Drawing.Point(15, 235);
            this.numericPMRRate.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numericPMRRate.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericPMRRate.Name = "numericPMRRate";
            this.numericPMRRate.ReadOnly = true;
            this.numericPMRRate.Size = new System.Drawing.Size(41, 20);
            this.numericPMRRate.TabIndex = 55;
            this.numericPMRRate.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericPMRRate.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 219);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(86, 13);
            this.label12.TabIndex = 56;
            this.label12.Text = "Min refresh rate :";
            // 
            // numericPISpeed
            // 
            this.numericPISpeed.Increment = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericPISpeed.Location = new System.Drawing.Point(15, 138);
            this.numericPISpeed.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericPISpeed.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericPISpeed.Name = "numericPISpeed";
            this.numericPISpeed.ReadOnly = true;
            this.numericPISpeed.Size = new System.Drawing.Size(41, 20);
            this.numericPISpeed.TabIndex = 53;
            this.numericPISpeed.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericPISpeed.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericPISpeed.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 54;
            this.label11.Text = "I2S speed :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericOY);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.radio25664);
            this.groupBox3.Controls.Add(this.radio12832);
            this.groupBox3.Controls.Add(this.radio12864);
            this.groupBox3.Location = new System.Drawing.Point(431, 271);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 59);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Choose a resolution";
            // 
            // numericOY
            // 
            this.numericOY.Enabled = false;
            this.numericOY.Location = new System.Drawing.Point(115, 32);
            this.numericOY.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericOY.Name = "numericOY";
            this.numericOY.ReadOnly = true;
            this.numericOY.Size = new System.Drawing.Size(37, 20);
            this.numericOY.TabIndex = 52;
            this.numericOY.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.numericOY.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(60, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 51;
            this.label13.Text = "Offset Y :";
            // 
            // buttonLTest
            // 
            this.buttonLTest.Enabled = false;
            this.buttonLTest.Location = new System.Drawing.Point(226, 336);
            this.buttonLTest.Name = "buttonLTest";
            this.buttonLTest.Size = new System.Drawing.Size(172, 23);
            this.buttonLTest.TabIndex = 34;
            this.buttonLTest.Text = "LED test";
            this.buttonLTest.UseVisualStyleBackColor = true;
            this.buttonLTest.Click += new System.EventHandler(this.buttonLTest_Click);
            // 
            // buttonRescan
            // 
            this.buttonRescan.Location = new System.Drawing.Point(27, 336);
            this.buttonRescan.Name = "buttonRescan";
            this.buttonRescan.Size = new System.Drawing.Size(172, 23);
            this.buttonRescan.TabIndex = 58;
            this.buttonRescan.Text = "Rescan devices";
            this.buttonRescan.UseVisualStyleBackColor = true;
            this.buttonRescan.Click += new System.EventHandler(this.buttonRescan_Click);
            // 
            // textDescription
            // 
            this.textDescription.Location = new System.Drawing.Point(-1, 365);
            this.textDescription.Multiline = true;
            this.textDescription.Name = "textDescription";
            this.textDescription.ReadOnly = true;
            this.textDescription.Size = new System.Drawing.Size(962, 36);
            this.textDescription.TabIndex = 59;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 399);
            this.Controls.Add(this.textDescription);
            this.Controls.Add(this.buttonRescan);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.numericBrightness);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numericROrder);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonSNParameters);
            this.Controls.Add(this.numericUPSize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.radioUsb);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.deviceView);
            this.Controls.Add(this.buttonFFile);
            this.Controls.Add(this.buttonLTest);
            this.Controls.Add(this.versionList);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.LatestVersion);
            this.Controls.Add(this.buttonFlash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ZeDMD Updater v2";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUPSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericROrder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPCPhase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPLBlanking)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPMRRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPISpeed)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericOY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton radioUsb;
        public System.Windows.Forms.RadioButton radioWTcp;
        public System.Windows.Forms.RadioButton radioWUdp;
        public System.Windows.Forms.Button buttonFFile;
        public System.Windows.Forms.ListBox versionList;
        public System.Windows.Forms.TextBox textBox3;
        public System.Windows.Forms.TextBox LatestVersion;
        public System.Windows.Forms.Button buttonFlash;
        public System.Windows.Forms.ListView deviceView;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown numericUDelay;
        public System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox textSsid;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.NumericUpDown numericUPSize;
        public System.Windows.Forms.Button buttonSNParameters;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.NumericUpDown numericROrder;
        public System.Windows.Forms.NumericUpDown numericBrightness;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.NumericUpDown numericPDriver;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.RadioButton radio12832;
        public System.Windows.Forms.RadioButton radio12864;
        public System.Windows.Forms.RadioButton radio25664;
        public System.Windows.Forms.NumericUpDown numericPCPhase;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown numericPLBlanking;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.NumericUpDown numericPISpeed;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.NumericUpDown numericPMRRate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.ColumnHeader columnCOM;
        public System.Windows.Forms.ColumnHeader columnType;
        public System.Windows.Forms.ColumnHeader columnS3;
        public System.Windows.Forms.ColumnHeader columnVersion;
        public System.Windows.Forms.ColumnHeader columnWidth;
        public System.Windows.Forms.ColumnHeader columnHeight;
        public System.Windows.Forms.ColumnHeader columnLilygo;
        public Button buttonLTest;
        public Button buttonRescan;
        private Label label13;
        private NumericUpDown numericOY;
        private TextBox textDescription;
    }
}

