namespace Mixxfit.Admin.Common.Controls
{
    /// <summary>A <see cref="Panel"/> that paints itself as a rounded white card.</summary>
    public class CardPanel : Panel
    {
        public CardPanel()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Padding = new Padding(16);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            CardPainter.Paint(e.Graphics, Size);
        }
    }
}
