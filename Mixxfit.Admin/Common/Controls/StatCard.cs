using System.ComponentModel;

namespace Mixxfit.Admin.Common.Controls
{
    /// <summary>
    /// A white rounded card with an amber accent, a small caption and a large value.
    /// Painted by hand for the same reason as <see cref="RoundedButton"/>: smooth corners.
    /// </summary>
    public class StatCard : UserControl
    {
        private static readonly Color Slate500 = Color.FromArgb(100, 116, 139);
        private static readonly Color Slate900 = Color.FromArgb(15, 23, 42);

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

            CardPainter.Paint(e.Graphics, Size);

            using var accent = new SolidBrush(CardPainter.Amber400);
            e.Graphics.FillRectangle(accent, new Rectangle(20, 14, 28, 4));
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
