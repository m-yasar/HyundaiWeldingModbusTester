namespace XMSeriesRoboticWMTestSoftware
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            // -- Connection bar
            this.ipAddress        = new System.Windows.Forms.TextBox();
            this.tryConnect       = new System.Windows.Forms.Button();
            this.connectionStatus = new XMSeriesRoboticWMTestSoftware.StatusLight();
            this.timer1           = new System.Windows.Forms.Timer(this.components);
            // -- Basic controls
            this.cbWeldingStart   = new System.Windows.Forms.CheckBox();
            this.cbRobotReady     = new System.Windows.Forms.CheckBox();
            // -- Title
            this.lblTitle         = new System.Windows.Forms.Label();
            // -- Ignition group
            this.gbIgnition       = new System.Windows.Forms.GroupBox();
            this.rbHF             = new System.Windows.Forms.RadioButton();
            this.rbLF             = new System.Windows.Forms.RadioButton();
            // -- Working mode group
            this.gbWorkingMode    = new System.Windows.Forms.GroupBox();
            this.rbStandard       = new System.Windows.Forms.RadioButton();
            this.rbFreqPulse      = new System.Windows.Forms.RadioButton();
            this.rbSecondPulse    = new System.Windows.Forms.RadioButton();
            // -- Parameter labels (name)
            this.lblPregas            = new System.Windows.Forms.Label();
            this.lblHotstartPercent   = new System.Windows.Forms.Label();
            this.lblHotstartTime      = new System.Windows.Forms.Label();
            this.lblStartSlope        = new System.Windows.Forms.Label();
            this.lblMainCurrent       = new System.Windows.Forms.Label();
            this.lblMainCurrentTime   = new System.Windows.Forms.Label();
            this.lblFreq              = new System.Windows.Forms.Label();
            this.lblDutyCycle         = new System.Windows.Forms.Label();
            this.lblBaseCurrentPct    = new System.Windows.Forms.Label();
            this.lblBaseCurrentTime   = new System.Windows.Forms.Label();
            this.lblEndSlope          = new System.Windows.Forms.Label();
            this.lblEndCurrentPct     = new System.Windows.Forms.Label();
            this.lblEndCurrentTime    = new System.Windows.Forms.Label();
            this.lblPostGas           = new System.Windows.Forms.Label();
            // -- Parameter textboxes
            this.txtPregas            = new System.Windows.Forms.TextBox();
            this.txtHotstartPercent   = new System.Windows.Forms.TextBox();
            this.txtHotstartTime      = new System.Windows.Forms.TextBox();
            this.txtStartSlope        = new System.Windows.Forms.TextBox();
            this.txtMainCurrent       = new System.Windows.Forms.TextBox();
            this.txtMainCurrentTime   = new System.Windows.Forms.TextBox();
            this.txtFreq              = new System.Windows.Forms.TextBox();
            this.txtDutyCycle         = new System.Windows.Forms.TextBox();
            this.txtBaseCurrentPercent= new System.Windows.Forms.TextBox();
            this.txtBaseCurrentTime   = new System.Windows.Forms.TextBox();
            this.txtEndSlope          = new System.Windows.Forms.TextBox();
            this.txtEndCurrentPercent = new System.Windows.Forms.TextBox();
            this.txtEndCurrentTime    = new System.Windows.Forms.TextBox();
            this.txtPostGas           = new System.Windows.Forms.TextBox();
            // -- Parameter value labels
            this.lblPregasVal             = new System.Windows.Forms.Label();
            this.lblHotstartPercentVal    = new System.Windows.Forms.Label();
            this.lblHotstartTimeVal       = new System.Windows.Forms.Label();
            this.lblStartSlopeVal         = new System.Windows.Forms.Label();
            this.lblMainCurrentVal        = new System.Windows.Forms.Label();
            this.lblMainCurrentTimeVal    = new System.Windows.Forms.Label();
            this.lblFreqVal               = new System.Windows.Forms.Label();
            this.lblDutyCycleVal          = new System.Windows.Forms.Label();
            this.lblBaseCurrentPercentVal = new System.Windows.Forms.Label();
            this.lblBaseCurrentTimeVal    = new System.Windows.Forms.Label();
            this.lblEndSlopeVal           = new System.Windows.Forms.Label();
            this.lblEndCurrentPercentVal  = new System.Windows.Forms.Label();
            this.lblEndCurrentTimeVal     = new System.Windows.Forms.Label();
            this.lblPostGasVal            = new System.Windows.Forms.Label();
            // -- Send & log
            this.btnSend      = new System.Windows.Forms.Button();
            this.rtblogSend   = new System.Windows.Forms.RichTextBox();
            // -- About group
            this.groupBox1    = new System.Windows.Forms.GroupBox();
            this.lblAbout     = new System.Windows.Forms.Label();
            this.lblApp       = new System.Windows.Forms.Label();
            this.lblVersion   = new System.Windows.Forms.Label();
            this.lblDev       = new System.Windows.Forms.Label();
            this.lblEmail     = new System.Windows.Forms.Label();
            this.lblCompany   = new System.Windows.Forms.Label();
            this.lblWeb       = new System.Windows.Forms.Label();

            this.gbIgnition.SuspendLayout();
            this.gbWorkingMode.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // ── Connection bar ──────────────────────────────────────────────────
            this.ipAddress.Location = new System.Drawing.Point(10, 11);
            this.ipAddress.Name     = "ipAddress";
            this.ipAddress.Size     = new System.Drawing.Size(150, 22);
            this.ipAddress.TabIndex = 0;
            this.ipAddress.Text     = "192.168.1.11";

            this.tryConnect.Location               = new System.Drawing.Point(170, 7);
            this.tryConnect.Name                   = "tryConnect";
            this.tryConnect.Size                   = new System.Drawing.Size(130, 30);
            this.tryConnect.TabIndex               = 1;
            this.tryConnect.Text                   = "Connect";
            this.tryConnect.UseVisualStyleBackColor = true;
            this.tryConnect.Click += new System.EventHandler(this.tryConnect_Click);

            this.connectionStatus.IsOn     = false;
            this.connectionStatus.Location = new System.Drawing.Point(310, 13);
            this.connectionStatus.Name     = "connectionStatus";
            this.connectionStatus.Size     = new System.Drawing.Size(20, 20);
            this.connectionStatus.TabIndex = 2;
            this.connectionStatus.Text     = "connectionStatus";

            this.timer1.Enabled  = true;
            this.timer1.Interval = 500;
            this.timer1.Tick    += new System.EventHandler(this.tick);

            // ── Basic controls ──────────────────────────────────────────────────
            this.cbWeldingStart.AutoSize  = true;
            this.cbWeldingStart.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbWeldingStart.Location  = new System.Drawing.Point(300, 45);
            this.cbWeldingStart.Name      = "cbWeldingStart";
            this.cbWeldingStart.Size      = new System.Drawing.Size(120, 22);
            this.cbWeldingStart.TabIndex  = 3;
            this.cbWeldingStart.Text      = "Welding Start";
            this.cbWeldingStart.UseVisualStyleBackColor = true;

            this.cbRobotReady.AutoSize  = true;
            this.cbRobotReady.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.cbRobotReady.Location  = new System.Drawing.Point(440, 45);
            this.cbRobotReady.Name      = "cbRobotReady";
            this.cbRobotReady.Size      = new System.Drawing.Size(115, 22);
            this.cbRobotReady.TabIndex  = 4;
            this.cbRobotReady.Text      = "Robot Ready";
            this.cbRobotReady.UseVisualStyleBackColor = true;

            // ── Title ───────────────────────────────────────────────────────────
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font     = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(297, 70);
            this.lblTitle.Name     = "lblTitle";
            this.lblTitle.Size     = new System.Drawing.Size(300, 26);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text     = "TIG Welding Parameters";

            // ── Ignition GroupBox ───────────────────────────────────────────────
            this.gbIgnition.Controls.Add(this.rbHF);
            this.gbIgnition.Controls.Add(this.rbLF);
            this.gbIgnition.Location = new System.Drawing.Point(690, 90);
            this.gbIgnition.Name     = "gbIgnition";
            this.gbIgnition.Size     = new System.Drawing.Size(195, 55);
            this.gbIgnition.TabIndex = 6;
            this.gbIgnition.TabStop  = false;
            this.gbIgnition.Text     = "Ignition";

            this.rbHF.AutoSize  = true;
            this.rbHF.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.rbHF.Location  = new System.Drawing.Point(10, 22);
            this.rbHF.Name      = "rbHF";
            this.rbHF.Size      = new System.Drawing.Size(75, 22);
            this.rbHF.TabIndex  = 0;
            this.rbHF.Text      = "HF (On)";
            this.rbHF.UseVisualStyleBackColor = true;

            this.rbLF.AutoSize  = true;
            this.rbLF.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.rbLF.Location  = new System.Drawing.Point(100, 22);
            this.rbLF.Name      = "rbLF";
            this.rbLF.Size      = new System.Drawing.Size(78, 22);
            this.rbLF.TabIndex  = 1;
            this.rbLF.Text      = "LF (Off)";
            this.rbLF.UseVisualStyleBackColor = true;

            // ── Working Mode GroupBox ───────────────────────────────────────────
            this.gbWorkingMode.Controls.Add(this.rbStandard);
            this.gbWorkingMode.Controls.Add(this.rbFreqPulse);
            this.gbWorkingMode.Controls.Add(this.rbSecondPulse);
            this.gbWorkingMode.Location = new System.Drawing.Point(690, 155);
            this.gbWorkingMode.Name     = "gbWorkingMode";
            this.gbWorkingMode.Size     = new System.Drawing.Size(195, 100);
            this.gbWorkingMode.TabIndex = 7;
            this.gbWorkingMode.TabStop  = false;
            this.gbWorkingMode.Text     = "Working Mode";

            this.rbStandard.AutoSize  = true;
            this.rbStandard.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.rbStandard.Location  = new System.Drawing.Point(10, 18);
            this.rbStandard.Name      = "rbStandard";
            this.rbStandard.Size      = new System.Drawing.Size(83, 22);
            this.rbStandard.TabIndex  = 0;
            this.rbStandard.Text      = "Standard";
            this.rbStandard.UseVisualStyleBackColor = true;
            this.rbStandard.CheckedChanged += new System.EventHandler(this.rbStandard_CheckedChanged);

            this.rbFreqPulse.AutoSize  = true;
            this.rbFreqPulse.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.rbFreqPulse.Location  = new System.Drawing.Point(10, 42);
            this.rbFreqPulse.Name      = "rbFreqPulse";
            this.rbFreqPulse.Size      = new System.Drawing.Size(95, 22);
            this.rbFreqPulse.TabIndex  = 1;
            this.rbFreqPulse.Text      = "Freq Pulse";
            this.rbFreqPulse.UseVisualStyleBackColor = true;
            this.rbFreqPulse.CheckedChanged += new System.EventHandler(this.rbFreqPulse_CheckedChanged);

            this.rbSecondPulse.AutoSize  = true;
            this.rbSecondPulse.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.rbSecondPulse.Location  = new System.Drawing.Point(10, 66);
            this.rbSecondPulse.Name      = "rbSecondPulse";
            this.rbSecondPulse.Size      = new System.Drawing.Size(110, 22);
            this.rbSecondPulse.TabIndex  = 2;
            this.rbSecondPulse.Text      = "Second Pulse";
            this.rbSecondPulse.UseVisualStyleBackColor = true;
            this.rbSecondPulse.CheckedChanged += new System.EventHandler(this.rbSecondPulse_CheckedChanged);

            // ── Parameter rows ──────────────────────────────────────────────────
            // Layout: name-label x=300 w=170 | textbox x=478 w=70 | val-label x=556 w=120
            // Row y positions (14 rows, step=26, start=102)
            int[] rowY = { 102, 128, 154, 180, 206, 232, 258, 284, 310, 336, 362, 388, 414, 440 };
            int xName = 300, xTxt = 478, xVal = 556;
            int wName = 170, wTxt = 70, wVal = 120;

            // Helper inline: name labels
            System.Action<System.Windows.Forms.Label, string, int> setupNameLabel = (lbl, text, y) => {
                lbl.AutoSize  = true;
                lbl.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
                lbl.Location  = new System.Drawing.Point(xName, y + 3);
                lbl.Size      = new System.Drawing.Size(wName, 18);
                lbl.Text      = text;
            };

            System.Action<System.Windows.Forms.TextBox, int, System.EventHandler> setupTxt = (txt, y, changed) => {
                txt.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
                txt.Location  = new System.Drawing.Point(xTxt, y);
                txt.Size      = new System.Drawing.Size(wTxt, 22);
                txt.Text      = "0";
                if (changed != null) txt.TextChanged += changed;
                txt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumericOnly_KeyPress);
            };

            System.Action<System.Windows.Forms.Label, int> setupValLabel = (lbl, y) => {
                lbl.AutoSize  = true;
                lbl.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F);
                lbl.Location  = new System.Drawing.Point(xVal, y + 3);
                lbl.Size      = new System.Drawing.Size(wVal, 18);
                lbl.Text      = "-";
            };

            // Row 0: Pre-Gas Time
            setupNameLabel(this.lblPregas, "Pre-Gas Time", rowY[0]);
            setupTxt(this.txtPregas, rowY[0], this.txtPregas_TextChanged);
            setupValLabel(this.lblPregasVal, rowY[0]);
            this.lblPregas.Name = "lblPregas"; this.txtPregas.Name = "txtPregas"; this.lblPregasVal.Name = "lblPregasVal";
            // Row 1: Hotstart Percent
            setupNameLabel(this.lblHotstartPercent, "Hotstart Percent", rowY[1]);
            setupTxt(this.txtHotstartPercent, rowY[1], this.txtHotstartPercent_TextChanged);
            setupValLabel(this.lblHotstartPercentVal, rowY[1]);
            this.lblHotstartPercent.Name = "lblHotstartPercent"; this.txtHotstartPercent.Name = "txtHotstartPercent"; this.lblHotstartPercentVal.Name = "lblHotstartPercentVal";
            // Row 2: Hotstart Time
            setupNameLabel(this.lblHotstartTime, "Hotstart Time", rowY[2]);
            setupTxt(this.txtHotstartTime, rowY[2], this.txtHotstartTime_TextChanged);
            setupValLabel(this.lblHotstartTimeVal, rowY[2]);
            this.lblHotstartTime.Name = "lblHotstartTime"; this.txtHotstartTime.Name = "txtHotstartTime"; this.lblHotstartTimeVal.Name = "lblHotstartTimeVal";
            // Row 3: Start Slope
            setupNameLabel(this.lblStartSlope, "Start Slope", rowY[3]);
            setupTxt(this.txtStartSlope, rowY[3], this.txtStartSlope_TextChanged);
            setupValLabel(this.lblStartSlopeVal, rowY[3]);
            this.lblStartSlope.Name = "lblStartSlope"; this.txtStartSlope.Name = "txtStartSlope"; this.lblStartSlopeVal.Name = "lblStartSlopeVal";
            // Row 4: Main Current
            setupNameLabel(this.lblMainCurrent, "Main Current", rowY[4]);
            setupTxt(this.txtMainCurrent, rowY[4], this.txtMainCurrent_TextChanged);
            setupValLabel(this.lblMainCurrentVal, rowY[4]);
            this.lblMainCurrent.Name = "lblMainCurrent"; this.txtMainCurrent.Name = "txtMainCurrent"; this.lblMainCurrentVal.Name = "lblMainCurrentVal";
            // Row 5: Main Current Time
            setupNameLabel(this.lblMainCurrentTime, "Main Current Time", rowY[5]);
            setupTxt(this.txtMainCurrentTime, rowY[5], this.txtMainCurrentTime_TextChanged);
            setupValLabel(this.lblMainCurrentTimeVal, rowY[5]);
            this.lblMainCurrentTime.Name = "lblMainCurrentTime"; this.txtMainCurrentTime.Name = "txtMainCurrentTime"; this.lblMainCurrentTimeVal.Name = "lblMainCurrentTimeVal";
            // Row 6: Frequency
            setupNameLabel(this.lblFreq, "Frequency", rowY[6]);
            setupTxt(this.txtFreq, rowY[6], this.txtFreq_TextChanged);
            setupValLabel(this.lblFreqVal, rowY[6]);
            this.lblFreq.Name = "lblFreq"; this.txtFreq.Name = "txtFreq"; this.lblFreqVal.Name = "lblFreqVal";
            // Row 7: Duty Cycle
            setupNameLabel(this.lblDutyCycle, "Duty Cycle", rowY[7]);
            setupTxt(this.txtDutyCycle, rowY[7], this.txtDutyCycle_TextChanged);
            setupValLabel(this.lblDutyCycleVal, rowY[7]);
            this.lblDutyCycle.Name = "lblDutyCycle"; this.txtDutyCycle.Name = "txtDutyCycle"; this.lblDutyCycleVal.Name = "lblDutyCycleVal";
            // Row 8: Base Current Percent
            setupNameLabel(this.lblBaseCurrentPct, "Base Current Percent", rowY[8]);
            setupTxt(this.txtBaseCurrentPercent, rowY[8], this.txtBaseCurrentPercent_TextChanged);
            setupValLabel(this.lblBaseCurrentPercentVal, rowY[8]);
            this.lblBaseCurrentPct.Name = "lblBaseCurrentPct"; this.txtBaseCurrentPercent.Name = "txtBaseCurrentPercent"; this.lblBaseCurrentPercentVal.Name = "lblBaseCurrentPercentVal";
            // Row 9: Base Current Time
            setupNameLabel(this.lblBaseCurrentTime, "Base Current Time", rowY[9]);
            setupTxt(this.txtBaseCurrentTime, rowY[9], this.txtBaseCurrentTime_TextChanged);
            setupValLabel(this.lblBaseCurrentTimeVal, rowY[9]);
            this.lblBaseCurrentTime.Name = "lblBaseCurrentTime"; this.txtBaseCurrentTime.Name = "txtBaseCurrentTime"; this.lblBaseCurrentTimeVal.Name = "lblBaseCurrentTimeVal";
            // Row 10: End Slope
            setupNameLabel(this.lblEndSlope, "End Slope", rowY[10]);
            setupTxt(this.txtEndSlope, rowY[10], this.txtEndSlope_TextChanged);
            setupValLabel(this.lblEndSlopeVal, rowY[10]);
            this.lblEndSlope.Name = "lblEndSlope"; this.txtEndSlope.Name = "txtEndSlope"; this.lblEndSlopeVal.Name = "lblEndSlopeVal";
            // Row 11: End Current Percent
            setupNameLabel(this.lblEndCurrentPct, "End Current Percent", rowY[11]);
            setupTxt(this.txtEndCurrentPercent, rowY[11], this.txtEndCurrentPercent_TextChanged);
            setupValLabel(this.lblEndCurrentPercentVal, rowY[11]);
            this.lblEndCurrentPct.Name = "lblEndCurrentPct"; this.txtEndCurrentPercent.Name = "txtEndCurrentPercent"; this.lblEndCurrentPercentVal.Name = "lblEndCurrentPercentVal";
            // Row 12: End Current Time
            setupNameLabel(this.lblEndCurrentTime, "End Current Time", rowY[12]);
            setupTxt(this.txtEndCurrentTime, rowY[12], this.txtEndCurrentTime_TextChanged);
            setupValLabel(this.lblEndCurrentTimeVal, rowY[12]);
            this.lblEndCurrentTime.Name = "lblEndCurrentTime"; this.txtEndCurrentTime.Name = "txtEndCurrentTime"; this.lblEndCurrentTimeVal.Name = "lblEndCurrentTimeVal";
            // Row 13: Post-Gas Time
            setupNameLabel(this.lblPostGas, "Post-Gas Time", rowY[13]);
            setupTxt(this.txtPostGas, rowY[13], this.txtPostGas_TextChanged);
            setupValLabel(this.lblPostGasVal, rowY[13]);
            this.lblPostGas.Name = "lblPostGas"; this.txtPostGas.Name = "txtPostGas"; this.lblPostGasVal.Name = "lblPostGasVal";

            // TabIndex assignment
            int ti = 10;
            this.txtPregas.TabIndex             = ti++;
            this.txtHotstartPercent.TabIndex    = ti++;
            this.txtHotstartTime.TabIndex       = ti++;
            this.txtStartSlope.TabIndex         = ti++;
            this.txtMainCurrent.TabIndex        = ti++;
            this.txtMainCurrentTime.TabIndex    = ti++;
            this.txtFreq.TabIndex               = ti++;
            this.txtDutyCycle.TabIndex          = ti++;
            this.txtBaseCurrentPercent.TabIndex = ti++;
            this.txtBaseCurrentTime.TabIndex    = ti++;
            this.txtEndSlope.TabIndex           = ti++;
            this.txtEndCurrentPercent.TabIndex  = ti++;
            this.txtEndCurrentTime.TabIndex     = ti++;
            this.txtPostGas.TabIndex            = ti++;

            // ── Send button ─────────────────────────────────────────────────────
            this.btnSend.Font     = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnSend.Location = new System.Drawing.Point(300, 474);
            this.btnSend.Name     = "btnSend";
            this.btnSend.Size     = new System.Drawing.Size(580, 48);
            this.btnSend.TabIndex = ti++;
            this.btnSend.Text     = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click   += new System.EventHandler(this.btnSend_Click);

            // ── Log box ─────────────────────────────────────────────────────────
            this.rtblogSend.Font        = new System.Drawing.Font("Courier New", 8.5F);
            this.rtblogSend.Location    = new System.Drawing.Point(300, 530);
            this.rtblogSend.Name        = "rtblogSend";
            this.rtblogSend.ReadOnly    = true;
            this.rtblogSend.ScrollBars  = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtblogSend.Size        = new System.Drawing.Size(580, 270);
            this.rtblogSend.TabIndex    = ti++;
            this.rtblogSend.Text        = "";

            // ── About GroupBox ──────────────────────────────────────────────────
            this.groupBox1.Controls.Add(this.lblAbout);
            this.groupBox1.Controls.Add(this.lblApp);
            this.groupBox1.Controls.Add(this.lblVersion);
            this.groupBox1.Controls.Add(this.lblDev);
            this.groupBox1.Controls.Add(this.lblEmail);
            this.groupBox1.Controls.Add(this.lblCompany);
            this.groupBox1.Controls.Add(this.lblWeb);
            this.groupBox1.Location = new System.Drawing.Point(690, 265);
            this.groupBox1.Name     = "groupBox1";
            this.groupBox1.Size     = new System.Drawing.Size(240, 185);
            this.groupBox1.TabIndex = ti++;
            this.groupBox1.TabStop  = false;

            this.lblAbout.AutoSize = true;
            this.lblAbout.Font     = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblAbout.Location = new System.Drawing.Point(70, 15);
            this.lblAbout.Name     = "lblAbout";
            this.lblAbout.Text     = "About";

            this.lblApp.AutoSize  = true;
            this.lblApp.Location  = new System.Drawing.Point(10, 50);
            this.lblApp.Name      = "lblApp";
            this.lblApp.Text      = "XM Series Robotic WM Test Software";

            this.lblVersion.AutoSize  = true;
            this.lblVersion.Location  = new System.Drawing.Point(10, 68);
            this.lblVersion.Name      = "lblVersion";
            this.lblVersion.Text      = "Version 2.0 (TIG)";

            this.lblDev.AutoSize  = true;
            this.lblDev.Location  = new System.Drawing.Point(10, 86);
            this.lblDev.Name      = "lblDev";
            this.lblDev.Text      = "Developer: Mustafa Yasar";

            this.lblEmail.AutoSize  = true;
            this.lblEmail.Location  = new System.Drawing.Point(10, 104);
            this.lblEmail.Name      = "lblEmail";
            this.lblEmail.Text      = "mustafayasar@kolarc.com";

            this.lblCompany.AutoSize  = true;
            this.lblCompany.Location  = new System.Drawing.Point(10, 122);
            this.lblCompany.Name      = "lblCompany";
            this.lblCompany.Text      = "Kolarc Machine";

            this.lblWeb.AutoSize  = true;
            this.lblWeb.Location  = new System.Drawing.Point(10, 140);
            this.lblWeb.Name      = "lblWeb";
            this.lblWeb.Text      = "www.kolarc.com";

            // ── Form ────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(950, 820);
            this.Text                = "XM TIG Robotic Test";

            this.Controls.Add(this.ipAddress);
            this.Controls.Add(this.tryConnect);
            this.Controls.Add(this.connectionStatus);
            this.Controls.Add(this.cbWeldingStart);
            this.Controls.Add(this.cbRobotReady);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.gbIgnition);
            this.Controls.Add(this.gbWorkingMode);
            // name labels
            this.Controls.Add(this.lblPregas);
            this.Controls.Add(this.lblHotstartPercent);
            this.Controls.Add(this.lblHotstartTime);
            this.Controls.Add(this.lblStartSlope);
            this.Controls.Add(this.lblMainCurrent);
            this.Controls.Add(this.lblMainCurrentTime);
            this.Controls.Add(this.lblFreq);
            this.Controls.Add(this.lblDutyCycle);
            this.Controls.Add(this.lblBaseCurrentPct);
            this.Controls.Add(this.lblBaseCurrentTime);
            this.Controls.Add(this.lblEndSlope);
            this.Controls.Add(this.lblEndCurrentPct);
            this.Controls.Add(this.lblEndCurrentTime);
            this.Controls.Add(this.lblPostGas);
            // textboxes
            this.Controls.Add(this.txtPregas);
            this.Controls.Add(this.txtHotstartPercent);
            this.Controls.Add(this.txtHotstartTime);
            this.Controls.Add(this.txtStartSlope);
            this.Controls.Add(this.txtMainCurrent);
            this.Controls.Add(this.txtMainCurrentTime);
            this.Controls.Add(this.txtFreq);
            this.Controls.Add(this.txtDutyCycle);
            this.Controls.Add(this.txtBaseCurrentPercent);
            this.Controls.Add(this.txtBaseCurrentTime);
            this.Controls.Add(this.txtEndSlope);
            this.Controls.Add(this.txtEndCurrentPercent);
            this.Controls.Add(this.txtEndCurrentTime);
            this.Controls.Add(this.txtPostGas);
            // value labels
            this.Controls.Add(this.lblPregasVal);
            this.Controls.Add(this.lblHotstartPercentVal);
            this.Controls.Add(this.lblHotstartTimeVal);
            this.Controls.Add(this.lblStartSlopeVal);
            this.Controls.Add(this.lblMainCurrentVal);
            this.Controls.Add(this.lblMainCurrentTimeVal);
            this.Controls.Add(this.lblFreqVal);
            this.Controls.Add(this.lblDutyCycleVal);
            this.Controls.Add(this.lblBaseCurrentPercentVal);
            this.Controls.Add(this.lblBaseCurrentTimeVal);
            this.Controls.Add(this.lblEndSlopeVal);
            this.Controls.Add(this.lblEndCurrentPercentVal);
            this.Controls.Add(this.lblEndCurrentTimeVal);
            this.Controls.Add(this.lblPostGasVal);
            // send & log
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtblogSend);
            this.Controls.Add(this.groupBox1);

            this.gbIgnition.ResumeLayout(false);
            this.gbIgnition.PerformLayout();
            this.gbWorkingMode.ResumeLayout(false);
            this.gbWorkingMode.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Connection
        private System.Windows.Forms.TextBox        ipAddress;
        private System.Windows.Forms.Button         tryConnect;
        private StatusLight                          connectionStatus;
        private System.Windows.Forms.Timer          timer1;
        // Basic controls
        private System.Windows.Forms.CheckBox       cbWeldingStart;
        private System.Windows.Forms.CheckBox       cbRobotReady;
        // Title
        private System.Windows.Forms.Label          lblTitle;
        // Ignition
        private System.Windows.Forms.GroupBox       gbIgnition;
        private System.Windows.Forms.RadioButton    rbHF;
        private System.Windows.Forms.RadioButton    rbLF;
        // Working mode
        private System.Windows.Forms.GroupBox       gbWorkingMode;
        private System.Windows.Forms.RadioButton    rbStandard;
        private System.Windows.Forms.RadioButton    rbFreqPulse;
        private System.Windows.Forms.RadioButton    rbSecondPulse;
        // Parameter name labels
        private System.Windows.Forms.Label          lblPregas;
        private System.Windows.Forms.Label          lblHotstartPercent;
        private System.Windows.Forms.Label          lblHotstartTime;
        private System.Windows.Forms.Label          lblStartSlope;
        private System.Windows.Forms.Label          lblMainCurrent;
        private System.Windows.Forms.Label          lblMainCurrentTime;
        private System.Windows.Forms.Label          lblFreq;
        private System.Windows.Forms.Label          lblDutyCycle;
        private System.Windows.Forms.Label          lblBaseCurrentPct;
        private System.Windows.Forms.Label          lblBaseCurrentTime;
        private System.Windows.Forms.Label          lblEndSlope;
        private System.Windows.Forms.Label          lblEndCurrentPct;
        private System.Windows.Forms.Label          lblEndCurrentTime;
        private System.Windows.Forms.Label          lblPostGas;
        // Parameter textboxes
        private System.Windows.Forms.TextBox        txtPregas;
        private System.Windows.Forms.TextBox        txtHotstartPercent;
        private System.Windows.Forms.TextBox        txtHotstartTime;
        private System.Windows.Forms.TextBox        txtStartSlope;
        private System.Windows.Forms.TextBox        txtMainCurrent;
        private System.Windows.Forms.TextBox        txtMainCurrentTime;
        private System.Windows.Forms.TextBox        txtFreq;
        private System.Windows.Forms.TextBox        txtDutyCycle;
        private System.Windows.Forms.TextBox        txtBaseCurrentPercent;
        private System.Windows.Forms.TextBox        txtBaseCurrentTime;
        private System.Windows.Forms.TextBox        txtEndSlope;
        private System.Windows.Forms.TextBox        txtEndCurrentPercent;
        private System.Windows.Forms.TextBox        txtEndCurrentTime;
        private System.Windows.Forms.TextBox        txtPostGas;
        // Parameter value labels
        private System.Windows.Forms.Label          lblPregasVal;
        private System.Windows.Forms.Label          lblHotstartPercentVal;
        private System.Windows.Forms.Label          lblHotstartTimeVal;
        private System.Windows.Forms.Label          lblStartSlopeVal;
        private System.Windows.Forms.Label          lblMainCurrentVal;
        private System.Windows.Forms.Label          lblMainCurrentTimeVal;
        private System.Windows.Forms.Label          lblFreqVal;
        private System.Windows.Forms.Label          lblDutyCycleVal;
        private System.Windows.Forms.Label          lblBaseCurrentPercentVal;
        private System.Windows.Forms.Label          lblBaseCurrentTimeVal;
        private System.Windows.Forms.Label          lblEndSlopeVal;
        private System.Windows.Forms.Label          lblEndCurrentPercentVal;
        private System.Windows.Forms.Label          lblEndCurrentTimeVal;
        private System.Windows.Forms.Label          lblPostGasVal;
        // Send & log
        private System.Windows.Forms.Button         btnSend;
        private System.Windows.Forms.RichTextBox    rtblogSend;
        // About
        private System.Windows.Forms.GroupBox       groupBox1;
        private System.Windows.Forms.Label          lblAbout;
        private System.Windows.Forms.Label          lblApp;
        private System.Windows.Forms.Label          lblVersion;
        private System.Windows.Forms.Label          lblDev;
        private System.Windows.Forms.Label          lblEmail;
        private System.Windows.Forms.Label          lblCompany;
        private System.Windows.Forms.Label          lblWeb;
    }
}
