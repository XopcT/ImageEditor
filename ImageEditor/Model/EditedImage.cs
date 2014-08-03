using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace ImageEditor.Model
{
    /// <summary>
    /// Contains Image to be edited.
    /// </summary>
    public class EditedImage : BindableObjectBase
    {
        /// <summary>
        /// Creates a new Layer and adds it into the Image.
        /// </summary>
        public void AddLayer()
        {
            ImageLayer layer = new ImageLayer();
            // Subscribing for Layer Events:
            layer.PropertyChanged += this.layer_PropertyChanged;
            // Initializing Layer:
            layer.Image = ImageHelper.CreateRenderTarget(this.Width, this.Height);
            layer.Name = string.Format(StringResources.NewLayerNameFormat, ++this.layerIndex);
            layer.Visible = true;            
            
            // Adding Layer into Image:
            this.Layers.Insert(0, layer);
            this.CurrentLayer = layer;
            this.UpdateImage();
        }
        
        /// <summary>
        /// Removes current Layer from Image.
        /// </summary>
        public void RemoveLayer()
        {
            // Unsubscribing from Layer Events:
            this.CurrentLayer.PropertyChanged -= this.layer_PropertyChanged;
            // Removing Layer from the Image:
            this.Layers.Remove(this.CurrentLayer);
            // Selecting the first Layer:
            this.CurrentLayer = this.Layers[0];
            this.UpdateImage();
        }
        
        /// <summary>
        /// Moves current Layer to the specified Position among othe Layers.
        /// </summary>
        /// <param name="newIndex">Position to move to.</param>
        public void MoveCurrentLayer(int newIndex)
        {
            // Moving the Layer:
            ImageLayer layer = this.CurrentLayer;
            this.Layers.Remove(layer);
            this.Layers.Insert(newIndex, layer);
            // Updating current Layer:
            this.CurrentLayer = layer;
            this.UpdateImage();
        }

        /// <summary>
        /// Resizes the Image.
        /// </summary>
        /// <param name="newSize">New Image Size.</param>
        public void Resize(Point newSize)
        {
            // Updating Width and Height:
            this.Width = (int)newSize.X;
            this.Height = (int)newSize.Y;
            if (this.Width <= 0)
                this.Width = 1;
            if (this.Height <= 0)
                this.Height = 1;
            // Updating Layers:
            for (int i = 0; i < this.layers.Count; i++)
            {
                this.layers[i].Image = ImageHelper.CreateRenderTarget(this.Width, this.Height,
                    (visual, context) =>
                    {
                        Rect target = new Rect(0.0, 0.0, this.layers[i].Image.Width, this.layers[i].Image.Height);
                        context.DrawImage(this.layers[i].Image, target);
                    });
            }
        }

        /// <summary>
        /// Updates the resulting Image.
        /// </summary>
        private void UpdateImage()
        {
            Rect drawingRectangle = new Rect(0, 0, this.Width, this.Height);
            // Creating Drawing Surface:
            this.Image = ImageHelper.CreateRenderTarget(this.Width, this.Height,
                (visual, context) =>
                {
                    // Rending the Background:
                    for (int x = 0; x < drawingRectangle.Width / 10; x++)
                        for (int y = 0; y < drawingRectangle.Height / 10; y++)
                            if ((x + y) % 2 == 0)
                                context.DrawRectangle(Brushes.LightGray, null, new Rect(new Point(10 * x, 10 * y), new Point(10 * (x + 1), 10 * (y + 1))));
                    // Rendering visible Layers:
                    for (int i = this.layers.Count - 1; i >= 0; i--)
                        if (this.layers[i].Visible)
                            context.DrawImage(this.layers[i].Image, drawingRectangle);
                });
        }

        private void layer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Image" || e.PropertyName == "Visible")
                this.UpdateImage();
            base.OnPropertyChanged(e.PropertyName);
        }

        #region Properties
        /// <summary>
        /// Retrieves the resulting Image.
        /// </summary>
        public BitmapSource Image
        {
            get { return this.image; }
            private set
            {
                this.image = value;
                base.OnPropertyChanged("Image");
            }
        }
        
        /// <summary>
        /// Sets/retrieves the Layers Collection.
        /// </summary>
        public ObservableCollection<ImageLayer> Layers
        {
            get { return this.layers; }
            set
            {
                this.layers = value;
                base.OnPropertyChanged("Layers");
            }
        }

        /// <summary>
        /// Sets/retrieves currently selected Layer.
        /// </summary>
        public ImageLayer CurrentLayer
        {
            get { return this.currentLayer; }
            set
            {
                this.currentLayer = value;
                base.OnPropertyChanged("CurrentLayer");
            }
        }

        /// <summary>
        /// Sets/retrieves Image Width.
        /// </summary>
        public int Width
        {
            get { return this.width; }
            set
            {
                this.width = value;
                base.OnPropertyChanged("Width");
            }
        }

        /// <summary>
        /// Sets/retrieves Image Height.
        /// </summary>
        public int Height
        {
            get { return this.height; }
            set
            {
                this.height = value;
                base.OnPropertyChanged("Height");
            }
        }

        #endregion

        #region Field Declaration
        private BitmapSource image = null;
        
        private ObservableCollection<ImageLayer> layers = new ObservableCollection<ImageLayer>();
        private ImageLayer currentLayer = null;

        private int width = 10;
        private int height = 10;

        int layerIndex = 0;

        #endregion
    }
}
