using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace ImageEditor.Model
{
    /// <summary>
    /// Contains Data of a single Image Layer.
    /// </summary>
    public class ImageLayer : BindableObjectBase
    {
        #region Properties
        /// <summary>
        /// Sets/retrieves the Layer's Image.
        /// </summary>
        public BitmapSource Image
        {
            get { return this.image; }
            set
            {
                this.image = value;
                base.OnPropertyChanged("Image");
            }
        }

        /// <summary>
        /// Sets/retrieves whether the Layer is Visible on the resulting Image.
        /// </summary>
        public bool Visible
        {
            get { return this.visible; }
            set
            {
                this.visible = value;
                base.OnPropertyChanged("Visible");
            }
        }

        /// <summary>
        /// Sets/retrieves the Name of the Layer.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
                base.OnPropertyChanged("Name");
            }
        }

        #endregion

        #region Field Declaration
        private BitmapSource image = null;
        private bool visible = true;
        private string name = string.Empty;
        
        #endregion
    }
}
