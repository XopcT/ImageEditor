using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageEditor.Tools
{
    /// <summary>
    /// Tool used to draw Line Primitives.
    /// </summary>
    public class LineTool : PrimitiveTool
    {
        protected override void DrawMethod(DrawingContext context, Point start, Point end, Brush brush, Pen pen)
        {
            context.DrawLine(pen, start, end);
        }

        public override BitmapSource Icon
        {
            get { return new BitmapImage(new Uri("/ImageEditor;component/Images/line.png", UriKind.Relative)); }
        }
    }
}
