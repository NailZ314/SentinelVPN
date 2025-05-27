using System;
using System.Drawing;
using System.Windows.Forms;

public class ModernRadioButton : RadioButton
{
    public Color AccentColor { get; set; } = Color.FromArgb(0, 179, 135);
    public Color InactiveColor { get; set; } = Color.FromArgb(93, 97, 105);
    public Color TextColor { get; set; } = Color.FromArgb(0, 179, 135);
    public int DotSize { get; set; } = 8;
    public int OuterSize { get; set; } = 18;

    public ModernRadioButton()
    {
        this.ForeColor = TextColor;
        this.BackColor = Color.Transparent;
        this.Font = new Font("Verdana", 14.25F, FontStyle.Regular);
        this.AutoSize = true;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer, true);
        UpdateStyles();
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        pevent.Graphics.Clear(this.Parent?.BackColor ?? Color.FromArgb(35, 39, 43));
        pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        int radioX = 0;
        int radioY = (this.Height - OuterSize) / 2;

        using (Pen pen = new Pen(this.Checked ? AccentColor : InactiveColor, 2))
        {
            pevent.Graphics.DrawEllipse(pen, radioX, radioY, OuterSize, OuterSize);
        }

        if (this.Checked)
        {
            using (SolidBrush brush = new SolidBrush(AccentColor))
            {
                int dotX = radioX + (OuterSize - DotSize) / 2;
                int dotY = radioY + (OuterSize - DotSize) / 2;
                pevent.Graphics.FillEllipse(brush, dotX, dotY, DotSize, DotSize);
            }
        }

        using (SolidBrush textBrush = new SolidBrush(TextColor))
        {
            pevent.Graphics.DrawString(
                this.Text,
                this.Font,
                textBrush,
                OuterSize + 8,
                (this.Height - this.Font.Height) / 2 + 1
            );
        }
    }

    public override Size GetPreferredSize(Size proposedSize)
    {
        Size textSize = TextRenderer.MeasureText(this.Text, this.Font);
        int width = OuterSize + 3 + textSize.Width + 1;
        int height = Math.Max(OuterSize, textSize.Height) + 4;
        return new Size(width, height);
    }
}
