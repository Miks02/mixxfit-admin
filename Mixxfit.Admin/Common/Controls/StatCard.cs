using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Mixxfit.Admin.Common.Controls
{
    /// <summary>
    /// A white rounded card with an amber accent, a small caption and a large value.
    /// Painted by hand for the same reason as <see cref="RoundedButton"/>: smooth corners.
    /// </summary>
    public class StatCard : UserControl
    {
        private static readonly Color Amber400 = Color.FromArgb(251, 191, 36);
        private static readonly Color Slate200 = Color.FromArgb(226, 232, 240);
        private static readonly Color Slate500 = Color.FromArgb(100, 116, 139);
        private static readonly Color Slate900 = Color.FromArgb(15, 23, 42);

        private const int CornerRadius = 12;

        private readonly Label _title = new();
        private readonly Label _value = new();

        [Category("Appearance"), DefaultValue("")]
        public string Title
        {
            get => _title.Text;
            set => _title.Text = value;
        }

        [Category("Appearance"), DefaultValue("—")]
        public string Value
        {
            get => _value.Text;
            set => _value.Text = value;
        }

        public StatCard()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Color.Transparent;
            Padding = new Padding(20, 28, 16, 12);
            MinimumSize = new Size(120, 90);

            _value.Dock = DockStyle.Fill;
            _value.BackColor = Color.Transparent;
            _value.Font = new Font("Segoe UI Semibold", 22F);
            _value.ForeColor = Slate900;
            _value.TextAlign = ContentAlignment.MiddleLeft;
            _value.AutoEllipsis = true;
            _value.Text = "—";

            _title.Dock = DockStyle.Top;
            _title.Height = 24;
            _title.BackColor = Color.Transparent;
            _title.Font = new Font("Segoe UI", 10F);
            _title.ForeColor = Slate500;
            _title.TextAlign = ContentAlignment.MiddleLeft;

            // Fill first, Top second: docking is resolved from the highest z-index down.
            Controls.Add(_value);
            Controls.Add(_title);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, Width - 1, Height - 1);
            using var path = RoundedRect(rect, CornerRadius);
            using var fill = new SolidBrush(Color.White);
            using var border = new Pen(Slate200);
            g.FillPath(fill, path);
            g.DrawPath(border, path);

            using var accent = new SolidBrush(Amber400);
            g.FillRectangle(accent, new Rectangle(20, 14, 28, 4));
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _title.Font?.Dispose();
                _value.Font?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
