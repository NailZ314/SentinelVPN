using System;
using System.Drawing;
using System.Windows.Forms;

public class ModernCheckBox : CheckBox
{
    public Color AccentColor { get; set; } = Color.FromArgb(0, 179, 135);
    public Color TextColor { get; set; } = Color.FromArgb(225, 227, 232);

    public ModernCheckBox()
    {
        this.ForeColor = TextColor;
        this.BackColor = Color.Transparent;
        this.Font = new Font("Verdana", 8.25F, FontStyle.Regular);
        this.AutoSize = true;
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        pevent.Graphics.Clear(this.Parent.BackColor);

        int boxSize = 13; // smaller box
        int marginTop = (this.Height - boxSize) / 2;

        Rectangle rect = new Rectangle(0, marginTop, boxSize, boxSize);

        pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        // Border
        using (Pen borderPen = new Pen(AccentColor, 1.8f))
            pevent.Graphics.DrawEllipse(borderPen, rect);

        // Fill if checked
        if (this.Checked)
        {
            using (SolidBrush brush = new SolidBrush(AccentColor))
                pevent.Graphics.FillEllipse(brush, rect.X + 3, rect.Y + 3, boxSize - 6, boxSize - 6);
        }

        // Draw text, slightly smaller font and lighter color
        using (SolidBrush textBrush = new SolidBrush(TextColor))
        {
            pevent.Graphics.DrawString(
                this.Text, this.Font, textBrush, boxSize + 6, (this.Height - this.Font.Height) / 2 + 1
            );
        }
    }
}
