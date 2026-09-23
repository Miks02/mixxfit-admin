namespace Mixxfit.Admin.Common.Controls
{
    /// <summary>
    /// A <see cref="GroupBox"/> painted as a rounded white card with an amber accent and the
    /// group text as its title. Children dock below the title, inside <see cref="Control.Padding"/>.
    /// </summary>
    public class CardGroupBox : GroupBox
    {
        private const int TitleHeight = 56;

        public CardGroupBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Padding = new Padding(20, 0, 20, 20);
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                var r = ClientRectangle;
                return new Rectangle(
                    r.X + Padding.Left,
                    r.Y + TitleHeight,
                    Math.Max(r.Width - Padding.Horizontal, 0),
                    Math.Max(r.Height - TitleHeight - Padding.Bottom, 0));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            CardPainter.Paint(e.Graphics, Size);

            using var accent = new SolidBrush(CardPainter.Amber400);
            e.Graphics.FillRectangle(accent, new Rectangle(20, 14, 28, 4));

            TextRenderer.DrawText(e.Graphics, Text, Font, new Point(17, 22), ForeColor,
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
        }
    }
}
