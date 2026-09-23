using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Mixxfit.Admin.Common.Controls
{
    /// <summary>
    /// WinForms has no native corner-radius property, and clipping a Button's Region to a
    /// rounded GraphicsPath gives hard-edged (jagged) corners since Region clipping isn't
    /// anti-aliased. This paints the button itself instead, so corners stay smooth.
    /// </summary>
    public class RoundedButton : Button
    {
        private bool _isHovered;
        private bool _isPressed;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int CornerRadius { get; set; } = 10;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color HoverColor { get; set; } = Color.FromArgb(255, 178, 51);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color PressedColor { get; set; } = Color.FromArgb(217, 132, 0);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DisabledBackColor { get; set; } = Color.FromArgb(217, 150, 0);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color DisabledForeColor { get; set; } = Color.FromArgb(120, 120, 120);

        public RoundedButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!Enabled) return;
            _isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            _isPressed = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (!Enabled) return;
            _isPressed = true;
            Invalidate();
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            _isPressed = false;
            Invalidate();
            base.OnMouseUp(mevent);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            Invalidate();
            base.OnEnabledChanged(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Parent?.BackColor ?? BackColor);

            Color fillColor;
            Color textColor;

            if (!Enabled)
            {
                fillColor = DisabledBackColor;
                textColor = DisabledForeColor;
            }
            else
            {
                fillColor = _isPressed ? PressedColor : _isHovered ? HoverColor : BackColor;
                textColor = ForeColor;
            }

            var rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            using var path = RoundedRect(rect, CornerRadius);
            using var brush = new SolidBrush(fillColor);
            g.FillPath(brush, path);

            TextRenderer.DrawText(g, Text, Font, ClientRectangle, textColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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
    }
}