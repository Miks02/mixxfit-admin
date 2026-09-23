using System.Drawing.Drawing2D;

namespace Mixxfit.Admin.Common.Controls
{
    /// <summary>
    /// Shared look for every "card" surface: white, slate-200 border, smooth rounded corners.
    /// </summary>
    internal static class CardPainter
    {
        public static readonly Color Amber400 = Color.FromArgb(251, 191, 36);
        public static readonly Color Slate200 = Color.FromArgb(226, 232, 240);

        private const int CornerRadius = 12;

        public static void Paint(Graphics g, Size size)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            var rect = new Rectangle(0, 0, size.Width - 1, size.Height - 1);
            using var path = RoundedRect(rect, CornerRadius);
            using var fill = new SolidBrush(Color.White);
            using var border = new Pen(Slate200);
            g.FillPath(fill, path);
            g.DrawPath(border, path);
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
