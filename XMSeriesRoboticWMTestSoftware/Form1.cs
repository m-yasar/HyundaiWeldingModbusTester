using FluentModbus;
using System;
using System.Net;
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
                connectionStatus.IsOn = false;
            }
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // TIG COM_DI struct — 23 bytes (packed) + 1 padding = 12 registers
                byte[] bytes = new byte[24];

                // Byte 0: WeldingStart
                bytes[0]  = cbWeldingStart.Checked ? (byte)1 : (byte)0;
                // Byte 1: RobotReady
                bytes[1]  = cbRobotReady.Checked   ? (byte)1 : (byte)0;
                // Byte 2: JobNumber — sabit 5
                bytes[2]  = 5;
                // Byte 3: MachineMode — sabit 0 (2T)
                bytes[3]  = 0;
                // Byte 4: TriggerState
                bytes[4]  = 0;
                // Byte 5: WorkingMode — 0=Standart, 1=FreqPulse, 2=SecondPulse
                bytes[5]  = rbFreqPulse.Checked ? (byte)1 : rbSecondPulse.Checked ? (byte)2 : (byte)0;
                // Byte 6-7: Freq (uint16_t, little-endian)
                if (ushort.TryParse(txtFreq.Text, out ushort freq))
                { bytes[6] = (byte)(freq & 0xFF); bytes[7] = (byte)(freq >> 8); }
                // Byte 8: DutyCycle
                bytes[8]  = ParseByte(txtDutyCycle.Text);
                // Byte 9: PregasTime
                bytes[9]  = ParseByte(txtPregas.Text);
                // Byte 10: HotstartPercent
                bytes[10] = ParseByte(txtHotstartPercent.Text);
                // Byte 11: HotstartTime
                bytes[11] = ParseByte(txtHotstartTime.Text);
                // Byte 12: StartSlope
                bytes[12] = ParseByte(txtStartSlope.Text);
                // Byte 13-14: MainCurrent (uint16_t, little-endian)
                if (ushort.TryParse(txtMainCurrent.Text, out ushort mc))
                { bytes[13] = (byte)(mc & 0xFF); bytes[14] = (byte)(mc >> 8); }
                // Byte 15: MainCurrentTime
                bytes[15] = ParseByte(txtMainCurrentTime.Text);
                // Byte 16: BaseCurrentPercent
                bytes[16] = ParseByte(txtBaseCurrentPercent.Text);
                // Byte 17: BaseCurrentTime
                bytes[17] = ParseByte(txtBaseCurrentTime.Text);
                // Byte 18: EndSlope
                bytes[18] = ParseByte(txtEndSlope.Text);
                // Byte 19: EndCurrentPercent
                bytes[19] = ParseByte(txtEndCurrentPercent.Text);
                // Byte 20: EndCurrentTime
                bytes[20] = ParseByte(txtEndCurrentTime.Text);
                // Byte 21: PostGasTime
                bytes[21] = ParseByte(txtPostGas.Text);
                // Byte 22: Ignition — 0=HF, 1=LF
                bytes[22] = rbLF.Checked ? (byte)1 : (byte)0;
                // Byte 23: padding (0)

                // 24 byte → 12 register (little-endian pack, Modbus big-endian swap)
                short[] registers = new short[12];
                for (int i = 0; i < 12; i++)
                {
                    ushort val = (ushort)(bytes[2 * i] | (bytes[2 * i + 1] << 8));
                    registers[i] = (short)((val >> 8) | (val << 8));
                }

                _client?.WriteMultipleRegisters(1, 0, registers);
                LogBytes(bytes, 23);
                MessageBox.Show("Sent Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LogBytes(byte[] bytes, int count = -1)
        {
            string[] names = {
                "WeldingStart", "RobotReady", "JobNumber(fixed=5)", "MachineMode(2T fixed)",
                "TriggerState", "WorkingMode(0=Std,1=Freq,2=Sec)", "Freq_L", "Freq_H",
                "DutyCycle", "PregasTime", "HotstartPercent", "HotstartTime",
                "StartSlope", "MainCurrent_L", "MainCurrent_H", "MainCurrentTime",
                "BaseCurrentPercent", "BaseCurrentTime", "EndSlope",
                "EndCurrentPercent", "EndCurrentTime", "PostGasTime",
                "Ignition(0=HF,1=LF)"
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
