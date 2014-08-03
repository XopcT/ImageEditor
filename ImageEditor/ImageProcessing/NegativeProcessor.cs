using System;
using System.Collections.Generic;
using System.Windows.Media.Effects;
using ImageEditor.Model;
using ImageEditor.Effects;
using System.Windows;

namespace ImageEditor.ImageProcessing
{
    /// <summary>
    /// Image Processor used to make an Image Negative.
    /// </summary>
    public class NegativeProcessor : ImageProcessorBase
    {
        public override void Process(EditedImage image)
        {
            image.CurrentLayer.Image = ImageHelper.CreateRenderTarget(
                (int)image.CurrentLayer.Image.Width, (int)image.CurrentLayer.Image.Height,
                (visual, context) =>
                {
                    visual.Effect = this.effect.Value;
                    context.DrawImage(image.CurrentLayer.Image, new Rect(0.0, 0.0, image.CurrentLayer.Image.Width, image.CurrentLayer.Image.Height));
                });
        }

        #region Properties

        public override string Name
        {
            get { return StringResources.NegativeProcessor; }
        }

        #endregion

        #region Field Declaration
        private Lazy<Effect> effect = new Lazy<Effect>(() => new NegativeEffect());

        #endregion
    }
}
