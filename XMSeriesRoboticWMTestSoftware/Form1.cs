using FluentModbus;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
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
            rbInternalMode.Checked = true;
        }
        
        private void Connect()// bağlan butonu için
        {
            try
            {
                _client = new ModbusTcpClient();
                _client.Connect(new IPEndPoint(IPAddress.Parse(ipAddress.Text), 502));
                MessageBox.Show("Connection Established!");
                timer1.Start();
                connectionStatus.IsOn = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                connectionStatus.IsOn = false;
            }
        }

        // register okuma
        private void RegisterOku()
        {
            var data = _client.ReadHoldingRegisters<short>(1, 0, 10);
            for (int i = 0; i < data.Length; i++)
                Console.WriteLine($"Register[{i}] = {data[i]}");
        }
        private void tryConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void tick(object sender, EventArgs e)
        {
                try
                {
                    if (_client == null || !_client.IsConnected)
                        return;

                    var data = _client.ReadInputRegisters<ushort>(1, 0, 11);

                ushort word0 = (ushort)((data[0] >> 8) | (data[0] << 8));
                ushort word1 = (ushort)((data[1] >> 8) | (data[1] << 8));
                ushort word2 = (ushort)((data[2] >> 8) | (data[2] << 8));

                // Word 0 - Byte 0 (Bit 0-7)
                HPStatusLight.IsOn = (word0 & (1 << 0)) != 0; // Heartbeat Powersource
                    PSRStatusLight.IsOn = (word0 & (1 << 1)) != 0; // Power Source Ready
                    WarningStatusLight.IsOn = (word0 & (1 << 2)) != 0; // Warning
                    ProcessStatusLight.IsOn = (word0 & (1 << 3)) != 0; // Process Active
                    CurrentFlowStatusLight.IsOn = (word0 & (1 << 4)) != 0; // Current Flow
                    ArcStableTouchSignalStatusLight.IsOn = (word0 & (1 << 5)) != 0; // Arc Stable / Touch Signal
                    MainCurrentStatusLight.IsOn = (word0 & (1 << 6)) != 0; // Main Current Signal
                    TouchSignalStatusLight.IsOn = (word0 & (1 << 7)) != 0; // Touch Signal

                    // Word 0 - Byte 1 (Bit 8-9)
                    CollisionboxStatusLight.IsOn = (word0 & (1 << 8)) != 0; // Collisionbox Active
                    RobotMotionReleaseStatusLight.IsOn = (word0 & (1 << 9)) != 0; // Robot Motion Release

                    // Word 1 - Bit 22 (Word1 Bit 6)
                    MainSupplyStatusLight.IsOn = (word1 & (1 << 6)) != 0; // Main Supply Status

                    // Word 2 - Bit 39 (Word2 Bit 7)
                    SystemNotReadyStatusLight.IsOn = (word2 & (1 << 7)) != 0; // System Not Ready
                ushort word4 = (ushort)((data[4] >> 8) | (data[4] << 8));
                ushort word5 = (ushort)((data[5] >> 8) | (data[5] << 8));
                ushort word6 = (ushort)((data[6] >> 8) | (data[6] << 8));
                ushort word8 = (ushort)((data[8] >> 8) | (data[8] << 8));

                WeldingVoltageValue.Text = $"{word4} → {(word4 / 100.0):F1} V";
                WeldingCurrentValue.Text = $"{word5} → {(word5 / 10.0):F1} A";
                WireFeedSpeedValue.Text = $"{word6} → {((short)word6 / 10.0):F1} m/min";
                ErrorNumberValue.Text = word8.ToString();
                LogData(rtbLog, "READ ←", Array.ConvertAll(data.ToArray(), x => (short)x), swapBytes: true);
                }
                catch (Exception ex)
                {
                    timer1.Stop();
                    MessageBox.Show($"Bağlantı kesildi: {ex.Message}");
                    connectionStatus.IsOn = false;
            }
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != (char)Keys.Back)
                e.Handled = true;
        }

        private void SetInternalMode()
        {
            // Process Parameters — hepsi disabled
            txtProcessNumber.Enabled = false;
            txtWireFeedSpeed.Enabled = false;
            txtMemoryNumber.Enabled = false;
            txtArcCorr.Enabled = false;
            txtPulseCorr.Enabled = false;

            // S2T — hepsi disabled
            SetS2TFields(false);
        }

        private void SetJobMode()
        {
            // Process Parameters
            txtProcessNumber.Enabled = false;
            txtWireFeedSpeed.Enabled = false;
            txtMemoryNumber.Enabled = true;  // Job Number
            txtArcCorr.Enabled = false;
            txtPulseCorr.Enabled = false;

            SetS2TFields(false);
        }

        private void Set2TMode()
        {
            // Process Parameters — hepsi aktif
            txtProcessNumber.Enabled = true;
            txtWireFeedSpeed.Enabled = true;
            txtMemoryNumber.Enabled = false;
            txtArcCorr.Enabled = true;
            txtPulseCorr.Enabled = true;

            SetS2TFields(false);
        }

        private void SetS2TMode()
        {
            // Process Parameters — hepsi aktif
            txtProcessNumber.Enabled = true;
            txtWireFeedSpeed.Enabled = true;
            txtMemoryNumber.Enabled = false;
            txtArcCorr.Enabled = true;
            txtPulseCorr.Enabled = true;

            // S2T — hepsi aktif
            SetS2TFields(true);
        }

        private void SetS2TFields(bool enabled)
        {
            txtF1.Enabled = enabled;
            txtF3.Enabled = enabled;
            txtF4.Enabled = enabled;
            txtF5.Enabled = enabled;
            txtF7.Enabled = enabled;
            txtF8.Enabled = enabled;
            txtF10.Enabled = enabled;
            txtF11.Enabled = enabled;
            txtF12.Enabled = enabled;
            txtF15.Enabled = enabled;
            txtF16.Enabled = enabled;
            txtF17.Enabled = enabled;
            txtF18.Enabled = enabled;
            txtF22.Enabled = enabled;
        }

        private void rbInternalMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInternalMode.Checked) SetInternalMode();
        }

        private void rbJobMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbJobMode.Checked) SetJobMode();
        }

        private void rb2TMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2TMode.Checked) Set2TMode();
        }

        private void rbS2TMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbS2TMode.Checked) SetS2TMode();
        }
        private void LogData(RichTextBox rtb, string title, short[] data, bool swapBytes = false)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine($"[{DateTime.Now:HH:mm:ss.fff}] {title}");
            int byteIndex = 0;
            for (int i = 0; i < data.Length; i++)
            {
                byte hi = (byte)((ushort)data[i] & 0xFF);
                byte lo = (byte)(((ushort)data[i] >> 8) & 0xFF);

                if (swapBytes)
                {
                    sb.AppendLine($"Byte {byteIndex,2}: {lo,3}  (Word {i} Low)");
                    byteIndex++;
                    sb.AppendLine($"Byte {byteIndex,2}: {hi,3}  (Word {i} High)");
                    byteIndex++;
                }
                else
                {
                    sb.AppendLine($"Byte {byteIndex,2}: {hi,3}  (Word {i} High)");
                    byteIndex++;
                    sb.AppendLine($"Byte {byteIndex,2}: {lo,3}  (Word {i} Low)");
                    byteIndex++;
                }
            }
            rtb.Clear();
            rtb.AppendText(sb.ToString());
            rtb.ScrollToCaret();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ////begin
            try
            {
                short[] registers = new short[19];

                // Word 0 — Bit bazlı sinyaller + Working Mode Bits
                ushort word0 = 0;
                if (cbWeldingStart.Checked) word0 |= (1 << 0);
                if (cbRobotReady.Checked) word0 |= (1 << 1);
                if (cbGasOn.Checked) word0 |= (1 << 8);
                if (cbWireForward.Checked) word0 |= (1 << 9);
                if (cbWireBackward.Checked) word0 |= (1 << 10);
                if (cbTouchSensing.Checked) word0 |= (1 << 12);

                if (rbS2TMode.Checked) word0 |= (1 << 2);
                else if (rbJobMode.Checked) word0 |= (1 << 3);
                else if (rb2TMode.Checked) word0 |= (1 << 5);

                registers[0] = (short)word0;

                registers[1] = 0;
                registers[2] = 0;
                registers[3] = 0;

                if (rbJobMode.Checked && txtMemoryNumber.Text != "")
                    registers[4] = short.Parse(txtMemoryNumber.Text);
                else if ((rb2TMode.Checked || rbS2TMode.Checked) && txtProcessNumber.Text != "")
                    registers[4] = short.Parse(txtProcessNumber.Text);

                if (txtWireFeedSpeed.Enabled && txtWireFeedSpeed.Text != "")
                    registers[5] = short.Parse(txtWireFeedSpeed.Text);

                if (txtArcCorr.Enabled && txtArcCorr.Text != "")
                    registers[6] = short.Parse(txtArcCorr.Text);

                if (txtPulseCorr.Enabled && txtPulseCorr.Text != "")
                    registers[7] = short.Parse(txtPulseCorr.Text);

                if (rbS2TMode.Checked)
                {
                    if (txtF1.Text != "") registers[8] = short.Parse(txtF1.Text);
                    if (txtF3.Text != "") registers[9] = short.Parse(txtF3.Text);
                    if (txtF4.Text != "") registers[10] = short.Parse(txtF4.Text);
                    if (txtF5.Text != "") registers[10] |= (short)(short.Parse(txtF5.Text) << 8);
                    if (txtF7.Text != "") registers[11] = short.Parse(txtF7.Text);
                    if (txtF8.Text != "") registers[12] = short.Parse(txtF8.Text);
                    if (txtF10.Text != "") registers[13] = short.Parse(txtF10.Text);
                    if (txtF11.Text != "") registers[14] = short.Parse(txtF11.Text);
                    if (txtF12.Text != "") registers[14] |= (short)(short.Parse(txtF12.Text) << 8);
                    if (txtF15.Text != "") registers[15] = short.Parse(txtF15.Text);
                    if (txtF16.Text != "") registers[16] = short.Parse(txtF16.Text);
                    if (txtF17.Text != "") registers[17] = short.Parse(txtF17.Text);
                    if (txtF18.Text != "") registers[17] |= (short)(short.Parse(txtF18.Text) << 8);
                    if (txtF22.Text != "") registers[18] = short.Parse(txtF22.Text);
                }

                // Tüm diziye byte swap uygula
                for (int i = 0; i < registers.Length; i++)
                {
                    ushort val = (ushort)registers[i];
                    registers[i] = (short)((val >> 8) | (val << 8));
                }

                _client.WriteMultipleRegisters(1, 0, registers);
                LogData(rtblogSend, "SEND →", registers, swapBytes: true);
                MessageBox.Show("Gönderildi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            //end
           
        }

        private void txtMemoryNumber_TextChanged(object sender, EventArgs e)
        {
            labelPMemory.Text = "P" + txtMemoryNumber.Text;
        }

        private void txtWireFeedSpeed_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtWireFeedSpeed.Text, out double val))
                labelwfs.Text = (val / 10).ToString("F1") + " m/min";
            else
                labelwfs.Text = "- m/min";
        }

        private void txtArcCorr_TextChanged(object sender, EventArgs e)
        {
                if (double.TryParse(txtArcCorr.Text, out double val))
                    labelTrim.Text = (val * 0.1).ToString("F1") + "  ->  %" + (val * 0.2).ToString("F1");
                else
                    labelTrim.Text = "%-";
            
        }

        private void txtF1_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF1.Text, out double val))
                labelf1.Text = (val / 100).ToString("F2") +"s";
            else
                labelf1.Text = "-";
        }

        private void txtF3_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF3.Text, out double val))
                labelF3.Text = (val / 100).ToString("F2") + " s";
            else
                labelF3.Text = "-";
        }

        private void txtF4_TextChanged(object sender, EventArgs e)
        {

            if (double.TryParse(txtF4.Text, out double val))
                labelF4.Text = "%" + val.ToString("F0");
            else
                labelF4.Text = "-";
        }

        private void txtF5_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF5.Text, out double val))
                labelF5.Text = (val * 0.1).ToString("F1") + "  ->  %" + (val * 0.2).ToString("F1");
            else
                labelF5.Text = "%-";

        }

        private void txtF7_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF7.Text, out double val))
                labelF7.Text = (val / 100).ToString("F2") + " s";
            else
                labelF7.Text = "-";
        }

        private void txtF8_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF8.Text, out double val))
                labelF8.Text = (val / 100).ToString("F2") + " s";
            else
                labelF8.Text = "-";
        }

        private void txtF10_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF10.Text, out double val))
                labelF10.Text = (val / 100).ToString("F2") + " s";
            else
                labelF10.Text = "-";
        }

        private void txtF11_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF11.Text, out double val))
                labelF11.Text = "%" + val.ToString("F0");
            else
                labelF11.Text = "-";
        }

        private void txtF12_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF12.Text, out double val))
                labelF12.Text = (val * 0.1).ToString("F1") + "  ->  %" + (val * 0.2).ToString("F1");
            else
                labelF12.Text = "%-";
        }

        private void txtF15_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF15.Text, out double val))
                labelF15.Text = (val / 100).ToString("F2") + " s";
            else
                labelF15.Text = "-";
        }

        private void txtF16_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF16.Text, out double val))
                labelF16.Text = (val / 100).ToString("F2") + " s";
            else
                labelF16.Text = "-";
        }

        private void txtF17_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF17.Text, out double val))
                labelF17.Text = "%" + val.ToString("F0");
            else
                labelF17.Text = "-";
        }

        private void txtF18_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF18.Text, out double val))
                labelF18.Text = (val * 0.1).ToString("F1") + "  ->  %" + (val * 0.2).ToString("F1");
            else
                labelF18.Text = "%-";
        }

        private void txtF22_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(txtF22.Text, out double val))
                labelF22.Text = (val / 100).ToString("F2") + " s";
            else
                labelF22.Text = "-";
        }
    }
}
