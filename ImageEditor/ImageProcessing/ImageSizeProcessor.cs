using System;
using System.Collections.Generic;
using ImageEditor.Model;
using System.Windows;

namespace ImageEditor.ImageProcessing
{
    /// <summary>
    /// Image Processor used to resize an Image.
    /// </summary>
    public class ImageSizeProcessor : ImageProcessorBase
    {
        public override void Process(EditedImage image)
        {
            SizeSettings settings = new SizeSettings();
            settings.DataContext = new Point(image.Width, image.Height);

            if (settings.ShowDialog() == true)
                image.Resize((Point)settings.DataContext);
        }

        public override string Name
        {
            get { return StringResources.ImageSizeProcessor; }
        }
    }
}
