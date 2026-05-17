using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace XMSeriesRoboticWMTestSoftware
{
    [ToolboxItem(true)]
    public class StatusLight : Control
    {
        private bool _isOn = false;

        public bool IsOn
        {
            get => _isOn;
            set
            {
                _isOn = value;
                Invalidate();
            }
        }

        public StatusLight()
        {
            Size = new Size(20, 20);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var color = _isOn ? Color.LimeGreen : Color.Red;
            var brush = new SolidBrush(color);

            g.FillEllipse(brush, 0, 0, Width - 1, Height - 1);
            g.DrawEllipse(Pens.DimGray, 0, 0, Width - 1, Height - 1);

            brush.Dispose();
        }
    }
}