using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageEditor.Tools
{
    public class RectangleTool : PrimitiveTool
    {
        protected override void DrawMethod(DrawingContext context, Point start, Point end, Brush brush, Pen pen)
        {
            context.DrawRectangle(brush, pen, new Rect(start, end));
        }

        public override BitmapSource Icon
        {
            get { return new BitmapImage(new Uri("/ImageEditor;component/Images/rectangle.png", UriKind.Relative)); }

        }
    }
}
