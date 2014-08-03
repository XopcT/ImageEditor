using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageEditor.Tools
{
    /// <summary>
    /// Tool used to draw Circle Primitives.
    /// </summary>
    public class CircleTool : PrimitiveTool
    {
        protected override void DrawMethod(DrawingContext context, Point start, Point end, Brush brush, Pen pen)
        {
            context.DrawEllipse(
                brush, pen, start,
                Math.Abs(start.X - end.X),
                Math.Abs(start.Y - end.Y));
        }

        public override BitmapSource Icon
        {
            get { return new BitmapImage(new Uri("/ImageEditor;component/Images/circle.png", UriKind.Relative)); }
        }
    }
}
