using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Input;
using ImageEditor.Model;
using System.IO;

namespace ImageEditor.ViewModel
{
    /// <summary>
    /// View Model for a Canvas.
    /// </summary>
    public class CanvasViewModel : BindableObjectBase
    {
        /// <summary>
        /// Initializes a new Instance of current Class.
        /// </summary>
        /// <param name="editedImage">Image the Canvas is created for.</param>
        public CanvasViewModel(EditedImage editedImage)
        {
            this.EditedImage = editedImage;
            // Subscribing for Image Events:
            editedImage.PropertyChanged += this.editedImage_PropertyChanged;
        }

        /// <summary>
        /// Handles Image Changes.
        /// </summary>
        private void editedImage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Image" ||
                e.PropertyName == "Visible" ||
                e.PropertyName == "Name" ||
                e.PropertyName == "Width" ||
                e.PropertyName == "Height")
                this.IsChanged = true;
        }
        
        /// <summary>
        /// Moves current Image's Layer one step up.
        /// </summary>
        public void MoveLayerUp()
        {
            int currentLayerIndex = this.EditedImage.Layers.IndexOf(this.EditedImage.CurrentLayer);
            this.EditedImage.MoveCurrentLayer(--currentLayerIndex);
        }

        /// <summary>
        /// Moves current Image's Layer one step down.
        /// </summary>
        public void MoveLayerDown()
        {
            int currentLayerIndex = this.EditedImage.Layers.IndexOf(this.EditedImage.CurrentLayer);
            this.EditedImage.MoveCurrentLayer(++currentLayerIndex);
        }

        /// <summary>
        /// Retrieves whether current Layer can be moved up.
        /// </summary>
        /// <returns>True if Layer can be moved, False otherwise.</returns>
        public bool CanMoveLayerUp()
        {
            return this.EditedImage.Layers.IndexOf(this.EditedImage.CurrentLayer) > 0;
        }
        
        /// <summary>
        /// Retrieves whether current Layer can be moved down.
        /// </summary>
        /// <returns>True if Layer can be moved, False otherwise.</returns>
        public bool CanMoveLayerDown()
        {
            return this.EditedImage.Layers.IndexOf(this.EditedImage.CurrentLayer) < this.editedImage.Layers.Count - 1;
        }

        /// <summary>
        /// Retieves whether current Layer can be removed from the Image. Layer can be removed if its not the last one in the Image.
        /// </summary>
        /// <returns>True if Layer can be removed, False otherwise.</returns>
        public bool CanRemoveLayer()
        {
            return this.EditedImage.Layers.Count > 1;
        }
        
        public void OnMouseDown(object sender, MouseButtonEventArgs e, Point position)
        {
            if (this.toolbox != null && this.toolbox.CurrentTool != null)
                this.toolbox.CurrentTool.OnMouseDown(this, e, position);
        }

        public void OnMouseUp(object sender, MouseButtonEventArgs e, Point position)
        {
            if (this.toolbox != null && this.toolbox.CurrentTool != null)
                this.toolbox.CurrentTool.OnMouseUp(this, e, position);
        }

        public void OnMouseMove(object sender, MouseEventArgs e, Point position)
        {
            if (this.toolbox != null && this.toolbox.CurrentTool != null)
                this.toolbox.CurrentTool.OnMouseMove(this, e, position);
        }

        #region Properties
        /// <summary>
        /// Sets/retrieves the Image the Canvas works with.
        /// </summary>
        public EditedImage EditedImage
        {
            get { return this.editedImage; }
            private set
            {
                this.editedImage = value;
                base.OnPropertyChanged("EditedImage");
            }
        }

        /// <summary>
        /// Sets/retrieves the Name of the Canvas. The Name can be derived from the Image File.
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

        /// <summary>
        /// Sets/retrieves the Original File of the Image.
        /// </summary>
        public FileInfo File
        {
            get { return this.file; }
            set
            {
                this.file = value;
                base.OnPropertyChanged("File");
                // Also changing Canvas Name:
                this.Name = value.Name;
            }
        }

        /// <summary>
        /// Sets/retrieves whether some Changes were made to the Image.
        /// </summary>
        public bool IsChanged
        {
            get { return this.isChanged; }
            set
            {
                this.isChanged = value;
                base.OnPropertyChanged("IsChanged");
            }
        }

        /// <summary>
        /// Sets/retrieves the related Toolbox.
        /// </summary>
        public ToolboxViewModel Toolbox
        {
            get { return this.toolbox; }
            set { this.toolbox = value; }
        }

        #endregion

        #region Field Declaration
        private EditedImage editedImage = null;

        private string name = string.Empty;
        private FileInfo file = null;
        private bool isChanged = false;
        
        private ToolboxViewModel toolbox = null;
        
        #endregion
    }
}
