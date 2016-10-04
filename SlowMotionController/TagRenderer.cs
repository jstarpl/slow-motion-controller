using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using BrightIdeasSoftware;

namespace SlowMotionController
{
    public class TagRenderer : BaseRenderer
    {
        public float MarginRight = 3;

        public Dictionary<string, Brush> TagBrushes = new Dictionary<string,Brush>();

        public override void Render(System.Drawing.Graphics g, System.Drawing.Rectangle r)
        {
            BufferCue cue = this.RowObject as BufferCue;

            this.DrawBackground(g, r);

            PointF nextTagStart = new PointF(r.Left, r.Top);

            foreach (string Tag in cue.Tags)
            {
                Brush tagBrush = Brushes.DarkSlateGray;
                if (TagBrushes.ContainsKey(Tag))
                {
                    tagBrush = TagBrushes[Tag];
                }

                SizeF stringSize = g.MeasureString(Tag, this.ListView.Font);
                float tagWidth = Math.Min(stringSize.Width, r.Width);
                g.FillRectangle(tagBrush, nextTagStart.X, nextTagStart.Y, tagWidth, r.Height);
                g.DrawString(Tag, this.ListView.Font, Brushes.White, new RectangleF(nextTagStart.X, nextTagStart.Y, tagWidth, r.Height));

                nextTagStart.X += tagWidth + MarginRight;
            }
        }
    }
}
