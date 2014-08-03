using System;
using System.Collections.Generic;
using System.Windows.Media;
using ImageEditor.Model;

namespace ImageEditor
{
    /// <summary>
    /// Base Class for creating Image Processors.
    /// </summary>
    public abstract class ImageProcessorBase : BindableObjectBase
    {
        /// <summary>
        /// Processes the Image in custom Way.
        /// </summary>
        /// <param name="image">Image to process.</param>
        public abstract void Process(EditedImage image);

        /// <summary>
        /// Retrieves the Name of the Processor.
        /// </summary>
        public abstract string Name
        {
            get;
        }
    }
}
