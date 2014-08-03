using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageEditor.ImageProcessing;
using ImageEditor.Model;
using ImageEditor.View;
using ImageEditor.Tools;
using System.Windows;

namespace ImageEditor.ViewModel
{
    /// <summary>
    /// View Model for the Image Editor.
    /// </summary>
    public class AppViewModel : BindableObjectBase
    {
        /// <summary>
        /// Creates a new Image.
        /// </summary>
        public void New()
        {
            // Creating new Image with the specified Size:
            SizeSettings sizeSettings = new SizeSettings();
            sizeSettings.DataContext = new Point(200, 200);
            if (sizeSettings.ShowDialog() == true)
            {
                int width = (int)((Point)sizeSettings.DataContext).X;
                int height = (int)((Point)sizeSettings.DataContext).Y;
                if (width <= 0)
                    width = 1;
                if (height <= 0)
                    height = 1;
                EditedImage image = ImageHelper.New(width, height);
                // Creating Canvas for the Image:
                CanvasViewModel canvas = new CanvasViewModel(image)
                {
                    Name = string.Format(StringResources.NewImageNameFormat, ++this.canvasIndex),
                    Toolbox = this.toolbox,
                    IsChanged = false
                };
                // Adding Canvas into the Editor.
                this.Canvases.Add(canvas);
                this.CurrentCanvas = canvas;
            }
        }
        
        /// <summary>
        /// Opens an existing Image.
        /// </summary>
        public void Open()
        {
            FileInfo file = null;
            // Selecting a File to open:
            if (DialogHelper.ShowOpenFileDialog(out file))
            {
                // Opening specified Image:
                EditedImage image = ImageHelper.Open(file);
                if (image == null)
                    return;
                // Creating Canvas for the Image:
                CanvasViewModel canvas = new CanvasViewModel(image)
                {
                    File = file,
                    Toolbox = this.toolbox,
                    IsChanged = false
                };
                // Adding Canvas into the Editor:
                this.Canvases.Add(canvas);
                this.CurrentCanvas = canvas;
            }
        }

        /// <summary>
        /// Saves current Image.
        /// </summary>
        /// <returns>True if File saved successfully, False otherwise.</returns>
        public bool Save()
        {
            // Checking if Image was loaded from a File or was just created:
            FileInfo file = this.CurrentCanvas.File;
            if (file == null)
                // Image was just created, so it needs some File to be saved to:
                if (!DialogHelper.ShowSaveFileDialog(out file))
                    return false;
            // Saving Image into the File:
            ImageHelper.Save(file, this.CurrentCanvas.EditedImage);
            this.CurrentCanvas.File = file;
            this.CurrentCanvas.IsChanged = false;
            return true;
        }

        /// <summary>
        /// Saves current Image into the specified File.
        /// </summary>
        public void SaveAs()
        {
            // Selecting a File to save Image to:
            FileInfo file = null;
            if (!DialogHelper.ShowSaveFileDialog(out file))
                return;
            // Saving Image into the specified File:
            ImageHelper.Save(file, this.CurrentCanvas.EditedImage);
            this.CurrentCanvas.File = file;
            this.CurrentCanvas.IsChanged = false;
        }

        /// <summary>
        /// Tries to save Canvas if it is necessary.
        /// </summary>
        /// <param name="canvas">Canvas to save.</param>
        /// <returns>True is Changes made to Canvas were saved, False otherwise.</returns>
        public bool TrySave(CanvasViewModel canvas)
        {
            // Checking if Canvas need to be saved:
            if (canvas.IsChanged)
            {
                // Focusing Canvas with Changes:
                this.CurrentCanvas = canvas;
                MessageBoxResult result = DialogHelper.SaveRequest(string.Format(StringResources.SaveRequestFormat, canvas.Name));                
                if (result == MessageBoxResult.Cancel)
                    // Cancelling the Process:
                    return false;
                else if (result == MessageBoxResult.No)
                    // Discarding Changes:
                    return true;
                else
                    // Saving the Canvas:
                    return this.Save();
            }
            return true;
        }

        /// <summary>
        /// Closes current Canvas.
        /// </summary>
        /// <param name="canvas">Canvas to close.</param>
        public void Close(CanvasViewModel canvas)
        {
            // Checking if Canvas needs to be saved:
            if (this.TrySave(canvas))
            {
                // Closing the Canvas:
                this.Canvases.Remove(canvas);
                if (this.Canvases.Count > 0)
                    this.CurrentCanvas = this.Canvases[0];
            }
        }

        /// <summary>
        /// Checks whether the Image Editor may to be closed.
        /// </summary>
        /// <returns>True if Image Editor may be closed, and False otherwise.</returns>
        public bool BeforeClosing()
        {
            // Checking if a Canvas cancels Closing the Editor:
            foreach (CanvasViewModel canvas in this.canvases)
                if (!this.TrySave(canvas))
                    return false;
            // Closing Layers Palette:
            if (this.layersPalette != null && this.layersPalette.IsLoaded)
                this.layersPalette.Close();
            return true;
        }
        
        /// <summary>
        /// Retrieves whether any Image is currently open.
        /// </summary>
        /// <returns>True if any Image is open, False otherwise.</returns>
        public bool AnyImageExists()
        {
            return (this.CurrentCanvas != null);
        }

        /// <summary>
        /// Processes Image using specified Processor.
        /// </summary>
        /// <param name="processor">Processor to process Image with.</param>
        public void ProcessImage(ImageProcessorBase processor)
        {
            if (processor != null)
                processor.Process(this.CurrentCanvas.EditedImage);
        }

        /// <summary>
        /// Shows/hides the Layers Palette.
        /// </summary>
        public void UpdateLayersPalette()
        {
            if (this.layersPalette != null && this.layersPalette.IsLoaded)
            {
                this.layersPalette.Close();
                this.layersPalette = null;
            }
            else
            {
                this.layersPalette = new LayersView();
                this.layersPalette.DataContext = this;
                this.layersPalette.Show();
            }
        }
        
        #region Properties
        /// <summary>
        /// Sets/retrieves the View Model for the Editor Toolbox.
        /// </summary>
        public ToolboxViewModel Toolbox
        {
            get { return this.toolbox; }
            set
            {
                this.toolbox = value;
                base.OnPropertyChanged("Toolbox");
            }
        }

        /// <summary>
        /// Sets/retrieves the Collection of available Image Processors.
        /// </summary>
        public ObservableCollection<ImageProcessorBase> ImageProcessors
        {
            get { return this.imageProcessors; }
            set
            {
                this.imageProcessors = value;
                base.OnPropertyChanged("ImageProcessors");
            }
        }

        /// <summary>
        /// Sets/retrieves the Collection of Canvases.
        /// </summary>
        public ObservableCollection<CanvasViewModel> Canvases
        {
            get { return this.canvases; }
            set
            {
                this.canvases = value;
                base.OnPropertyChanged("Canvases");
            }
        }

        /// <summary>
        /// Sets/retrieves currently active Canvas.
        /// </summary>
        public CanvasViewModel CurrentCanvas
        {
            get { return this.currentCanvas; }
            set
            {
                this.currentCanvas = value;
                base.OnPropertyChanged("CurrentCanvas");
            }
        }

        #endregion
        
        #region Field Declaration
        private ObservableCollection<CanvasViewModel> canvases = new ObservableCollection<CanvasViewModel>();
        private CanvasViewModel currentCanvas = null;

        private ObservableCollection<ImageProcessorBase> imageProcessors = new ObservableCollection<ImageProcessorBase>()
        {
            new ImageSizeProcessor(),
            new RotationProcessor(),
            new NegativeProcessor(),
            new GrayscaleProcessor(),
            new BrightnessContrastProcessor(),
        };

        private ToolboxViewModel toolbox = new ToolboxViewModel()
            {
                BackColor = Colors.White,
                ForeColor = Colors.Black,
                CurrentTool = new BrushTool()
            };

        private LayersView layersPalette = null;

        private int canvasIndex = 0;

        #endregion
    }
}