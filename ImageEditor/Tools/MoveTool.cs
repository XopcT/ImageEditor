using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ImageEditor.ViewModel;

namespace ImageEditor.Tools
{
    /// <summary>
    /// Tool used to move Image Layer.
    /// </summary>
    public class MoveTool : ToolBase
    {
        public override void OnMouseDown(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            this.start = position;
            this.original = sender.EditedImage.CurrentLayer.Image;
        }

        public override void OnMouseMove(CanvasViewModel sender, MouseEventArgs e, Point position)
        {
            if (this.start.HasValue)
            {
                sender.EditedImage.CurrentLayer.Image = ImageHelper.CreateRenderTarget(sender.EditedImage.Width, sender.EditedImage.Height,
                    (visual, context) =>
                    {
                        int dx = (int)(position.X - start.Value.X);
                        int dy = (int)(position.Y - start.Value.Y);
                        context.DrawImage(this.original, new Rect(dx, dy, sender.EditedImage.Width, sender.EditedImage.Height));
                    });
            }
        }

        public override void OnMouseUp(CanvasViewModel sender, MouseButtonEventArgs e, Point position)
        {
            this.start = null;
        }

        #region Properties
        public override BitmapSource Icon
        {
            get { return new BitmapImage(new Uri("/ImageEditor;component/Images/move.png", UriKind.Relative)); }
        }

        #endregion
        #region Field Declaration
        private Point? start = null;
        private BitmapSource original = null;

        #endregion
    }
}
