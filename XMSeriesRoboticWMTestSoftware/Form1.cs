using FluentModbus;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMSeriesRoboticWMTestSoftware
{
    public partial class Form1 : Form
    {
        private ModbusTcpClient _client;

        public Form1()
        {
            InitializeComponent();
            rbHF.Checked       = true;
            rbStandard.Checked = true;
            UpdateWorkingMode();
        }

        private void Connect()
        {
            try
            {
                _client = new ModbusTcpClient();
                _client.Connect(new IPEndPoint(IPAddress.Parse(ipAddress.Text), 502));
                connectionStatus.IsOn = true;
                timer1.Start();
                timer2.Start();
                MessageBox.Show("Connection Established!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                connectionStatus.IsOn = false;
            }
        }

        private void tryConnect_Click(object sender, EventArgs e) => Connect();

        private void tick(object sender, EventArgs e)
        {
            if (_client == null || !_client.IsConnected)
            {
                timer1.Stop();
                timer2.Stop();
                connectionStatus.IsOn = false;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (_client == null || !_client.IsConnected) return;
            try
            {
                // COM_DO: read 6 input registers = 12 bytes (covers bytes 0-11)
                var raw = _client.ReadInputRegisters<short>(1, 0, 6);

                // Modbus big-endian → little-endian bytes
                byte[] b = new byte[12];
                for (int i = 0; i < 6; i++)
                {
                    ushort reg = (ushort)raw[i];
                    b[2 * i]     = (byte)(reg >> 8);
                    b[2 * i + 1] = (byte)(reg & 0xFF);
                }

                // Byte 0: bit3=ProcessActive, bit5=ArcStable_TouchSignal
                slArcDetect.IsOn  = ((b[0] >> 5) & 1) == 1;
                slProcActive.IsOn = ((b[0] >> 3) & 1) == 1;
                // Byte 2: bit3=LimitSignal
                slLimitSignal.IsOn = ((b[2] >> 3) & 1) == 1;
                // Byte 4: bit7=SystemNotReady
                slPsNotReady.IsOn = ((b[4] >> 7) & 1) == 1;

                // Byte 8-9: WeldingVoltage (÷10 → V)
                ushort rawV = (ushort)(b[8] | (b[9] << 8));
                lblVoutVal.Text = $"{rawV} → {rawV / 10.0:F1} V";

                // Byte 10-11: WeldingCurrent (÷10 → A)
                ushort rawI = (ushort)(b[10] | (b[11] << 8));
                lblIoutVal.Text = $"{rawI} → {rawI / 10.0:F1} A";

                LogRecvBytes(b);
            }
            catch { }
        }

        private void UpdateWorkingMode()
        {
            bool isFreq   = rbFreqPulse.Checked;
            bool isSecond = rbSecondPulse.Checked;

            txtFreq.Enabled            = isFreq;
            txtDutyCycle.Enabled       = isFreq;
            txtMainCurrentTime.Enabled = isSecond;
            txtBaseCurrentTime.Enabled = isSecond;
        }

        private void rbStandard_CheckedChanged(object sender, EventArgs e)    => UpdateWorkingMode();
        private void rbFreqPulse_CheckedChanged(object sender, EventArgs e)   => UpdateWorkingMode();
        private void rbSecondPulse_CheckedChanged(object sender, EventArgs e) => UpdateWorkingMode();

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private byte ParseByte(string s) => byte.TryParse(s, out byte v) ? v : (byte)0;

        private byte[] BuildBytes(byte weldingStart)
        {
            byte[] bytes = new byte[36];
            bytes[0] = weldingStart;
            bytes[1] = cbRobotReady.Checked ? (byte)1 : (byte)0;
            bytes[2] = 5;   // JobNumber fixed
            bytes[3] = 0;   // MachineMode 2T
            bytes[4] = 0;   // TriggerState
            bytes[5] = rbFreqPulse.Checked ? (byte)1 : rbSecondPulse.Checked ? (byte)2 : (byte)0;
            WriteU16(bytes,  6, txtFreq.Text);
            WriteU16(bytes,  8, txtDutyCycle.Text);
            WriteU16(bytes, 10, txtPregas.Text);
            WriteU16(bytes, 12, txtHotstartPercent.Text);
            WriteU16(bytes, 14, txtHotstartTime.Text);
            WriteU16(bytes, 16, txtStartSlope.Text);
            WriteU16(bytes, 18, txtMainCurrent.Text);
            WriteU16(bytes, 20, txtMainCurrentTime.Text);
            WriteU16(bytes, 22, txtBaseCurrentPercent.Text);
            WriteU16(bytes, 24, txtBaseCurrentTime.Text);
            WriteU16(bytes, 26, txtEndSlope.Text);
            WriteU16(bytes, 28, txtEndCurrentPercent.Text);
            WriteU16(bytes, 30, txtEndCurrentTime.Text);
            WriteU16(bytes, 32, txtPostGas.Text);
            WriteU16(bytes, 34, rbLF.Checked ? "1" : "0");
            return bytes;
        }

        private static void WriteU16(byte[] b, int offset, string text)
        {
            if (ushort.TryParse(text, out ushort v))
            { b[offset] = (byte)(v & 0xFF); b[offset + 1] = (byte)(v >> 8); }
        }

        private void SendBytes(byte[] bytes)
        {
            short[] registers = new short[18];
            for (int i = 0; i < 18; i++)
            {
                ushort val = (ushort)(bytes[2 * i] | (bytes[2 * i + 1] << 8));
                registers[i] = (short)((val >> 8) | (val << 8));
            }
            _client?.WriteMultipleRegisters(1, 0, registers);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = BuildBytes(cbWeldingStart.Checked ? (byte)1 : (byte)0);
                SendBytes(bytes);
                LogBytes(bytes, 36);
                MessageBox.Show("Sent Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDeneme.Text,      out int deneme)  || deneme  <= 0) { MessageBox.Show("Geçerli deneme sayısı girin."); return; }
            if (!int.TryParse(txtBekleme.Text,     out int bekleme) || bekleme <  0) { MessageBox.Show("Geçerli bekleme süresi girin."); return; }
            if (!int.TryParse(txtTestDuration.Text, out int sure)   || sure    <= 0) { MessageBox.Show("Geçerli test süresi girin."); return; }

            btnTest.Enabled = false;
            try
            {
                for (int i = 0; i < deneme; i++)
                {
                    btnTest.Text = string.Format("{0}/{1} {2}s...", i + 1, deneme, sure);

                    byte[] onBytes = BuildBytes(1);
                    SendBytes(onBytes);
                    LogBytes(onBytes, 23);

                    await Task.Delay(sure * 1000);

                    byte[] offBytes = BuildBytes(0);
                    SendBytes(offBytes);
                    LogBytes(offBytes, 23);

                    if (i < deneme - 1 && bekleme > 0)
                    {
                        btnTest.Text = string.Format("{0}/{1} Bekleme {2}s...", i + 1, deneme, bekleme);
                        await Task.Delay(bekleme * 1000);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error: {0}", ex.Message));
            }
            finally
            {
                btnTest.Enabled = true;
                btnTest.Text    = "Test";
            }
        }

        private void LogRecvBytes(byte[] b)
        {
            string[] names = {
                "Status0 (b5=ArcStbl, b3=ProcAct)", "Status1",
                "Status2 (b3=LimitSignal)",          "Status3",
                "Status4 (b7=NotReady)",             "Status5",
                "Status6",                            "Status7",
                "WeldVoltage_L",                     "WeldVoltage_H",
                "WeldCurrent_L",                     "WeldCurrent_H"
            };
            ushort rawV = (ushort)(b[8]  | (b[9]  << 8));
            ushort rawI = (ushort)(b[10] | (b[11] << 8));
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"[{DateTime.Now:HH:mm:ss.fff}] RECV ←");
            for (int i = 0; i < b.Length && i < names.Length; i++)
                sb.AppendLine($"Byte {i,2}: {b[i],3}  ({names[i]})");
            sb.AppendLine($"  → Voltage: {rawV / 10.0:F1} V");
            sb.AppendLine($"  → Current: {rawI / 10.0:F1} A");
            rtblogRecv.Clear();
            rtblogRecv.AppendText(sb.ToString());
        }

        private void LogBytes(byte[] bytes, int count = -1)
        {
            string[] names = {
                "WeldingStart",          "RobotReady",
                "JobNumber(fixed=5)",    "MachineMode(2T fixed)",
                "TriggerState",          "WorkingMode(0=Std,1=Freq,2=Sec)",
                "Freq_L",                "Freq_H",
                "DutyCycle_L",           "DutyCycle_H",
                "PregasTime_L",          "PregasTime_H",
                "HotstartPercent_L",     "HotstartPercent_H",
                "HotstartTime_L",        "HotstartTime_H",
                "StartSlope_L",          "StartSlope_H",
                "MainCurrent_L",         "MainCurrent_H",
                "MainCurrentTime_L",     "MainCurrentTime_H",
                "BaseCurrentPercent_L",  "BaseCurrentPercent_H",
                "BaseCurrentTime_L",     "BaseCurrentTime_H",
                "EndSlope_L",            "EndSlope_H",
                "EndCurrentPercent_L",   "EndCurrentPercent_H",
                "EndCurrentTime_L",      "EndCurrentTime_H",
                "PostGasTime_L",         "PostGasTime_H",
                "Ignition_L",            "Ignition_H"
            };
            int len = (count < 0) ? bytes.Length : count;
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"[{DateTime.Now:HH:mm:ss.fff}] SEND →");
            for (int i = 0; i < len; i++)
                sb.AppendLine($"Byte {i,2}: {bytes[i],3}  ({names[i]})");
            rtblogSend.Clear();
            rtblogSend.AppendText(sb.ToString());
        }

        // Value label updates
        private void txtPregas_TextChanged(object sender, EventArgs e)
        { lblPregasVal.Text = double.TryParse(txtPregas.Text, out double v) ? $"{v / 10:F1} s" : "-"; }

        private void txtHotstartPercent_TextChanged(object sender, EventArgs e)
        { lblHotstartPercentVal.Text = double.TryParse(txtHotstartPercent.Text, out double v) ? $"%{v:F0}" : "-"; }

        private void txtHotstartTime_TextChanged(object sender, EventArgs e)
        { lblHotstartTimeVal.Text = double.TryParse(txtHotstartTime.Text, out double v) ? $"{v / 10:F1} s" : "-"; }

        private void txtStartSlope_TextChanged(object sender, EventArgs e)
        { lblStartSlopeVal.Text = double.TryParse(txtStartSlope.Text, out double v) ? $"{v / 10:F1} s" : "-"; }

        private void txtMainCurrent_TextChanged(object sender, EventArgs e)
        { lblMainCurrentVal.Text = double.TryParse(txtMainCurrent.Text, out double v) ? $"{v} A" : "-"; }

        private void txtMainCurrentTime_TextChanged(object sender, EventArgs e)
        { lblMainCurrentTimeVal.Text = double.TryParse(txtMainCurrentTime.Text, out double v) ? $"{v / 10:F1} s" : "-"; }

        private void txtFreq_TextChanged(object sender, EventArgs e)
        { lblFreqVal.Text = double.TryParse(txtFreq.Text, out double v) ? $"{v} Hz" : "-"; }

        private void txtDutyCycle_TextChanged(object sender, EventArgs e)
        { lblDutyCycleVal.Text = double.TryParse(txtDutyCycle.Text, out double v) ? $"%{v:F0}" : "-"; }

        private void txtBaseCurrentPercent_TextChanged(object sender, EventArgs e)
        { lblBaseCurrentPercentVal.Text = double.TryParse(txtBaseCurrentPercent.Text, out double v) ? $"%{v:F0}" : "-"; }

        private void txtBaseCurrentTime_TextChanged(object sender, EventArgs e)
        { lblBaseCurrentTimeVal.Text = double.TryParse(txtBaseCurrentTime.Text, out double v) ? $"{v / 10:F1} s" : "-"; }

        private void txtEndSlope_TextChanged(object sender, EventArgs e)
        { lblEndSlopeVal.Text = double.TryParse(txtEndSlope.Text, out double v) ? $"{v / 10:F1} s" : "-"; }

        private void txtEndCurrentPercent_TextChanged(object sender, EventArgs e)
        { lblEndCurrentPercentVal.Text = double.TryParse(txtEndCurrentPercent.Text, out double v) ? $"%{v:F0}" : "-"; }

        private void txtEndCurrentTime_TextChanged(object sender, EventArgs e)
        { lblEndCurrentTimeVal.Text = double.TryParse(txtEndCurrentTime.Text, out double v) ? $"{v / 10:F1} s" : "-"; }

        private void txtPostGas_TextChanged(object sender, EventArgs e)
        { lblPostGasVal.Text = double.TryParse(txtPostGas.Text, out double v) ? $"{v / 10:F1} s" : "-"; }
    }
}
