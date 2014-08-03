using System;
using System.Collections.Generic;
using ImageEditor.ViewModel;
using System.IO;
using System.Windows.Media.Imaging;
using ImageEditor.Model;
using System.Windows.Media;
using System.Windows;
using System.Xml;

namespace ImageEditor
{
    /// <summary>
    /// Provides Method to create, load and save Images.
    /// </summary>
    public static class ImageHelper
    {
        /// <summary>
        /// Creates a Render Target of the specified Size and draws specified Image on it..
        /// </summary>
        /// <param name="width">Width of the Render Target.</param>
        /// <param name="height">Height of the Render Target.</param>
        /// <param name="drawAction">Callback used to draw some Image on the Render Target.</param>
        /// <returns>Created Render Target Instance.</returns>
        public static BitmapSource CreateRenderTarget(int width, int height, Action<DrawingVisual, DrawingContext> drawAction = null)
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                if (drawAction != null)
                    drawAction(visual, context);
            }
            RenderTargetBitmap bitmap = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        /// <summary>
        /// Creates a new Image with the specified Size.
        /// </summary>
        /// <param name="width">Width of the new Image.</param>
        /// <param name="height">Height of the new Image.</param>
        /// <returns>New Image Instance.</returns>
        public static EditedImage New(int width, int height)
        {
            // Creating an Image:
            EditedImage image = new EditedImage()
                {
                    Width = width,
                    Height = height
                };
            // Adding default Layer:
            image.AddLayer();
            return image;
        }        

        /// <summary>
        /// Opens the specified Image File.
        /// </summary>
        /// <param name="file">File to open.</param>
        /// <returns>Loaded Image Instance.</returns>
        public static EditedImage Open(FileInfo file)
        {
            try
            {
                // Selecting a Way to load the Image depending on it's Format:
                switch (file.Extension.ToLower())
                {
                    case ".mim": return LoadMyImage(file);
                    case ".bmp":
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".gif":
                    case ".tif": return LoadBitmap(file);
                    default:
                        throw new ArgumentException(StringResources.UnsupportedFileTypeMessage);
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCriticalError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Saves the specified Image File.
        /// </summary>        
        /// <param name="file">File to save into.</param>
        /// <param name="image">Image to save.</param>
        public static void Save(FileInfo file, EditedImage image)
        {
            try
            {
                // Selecting a Way to save the Image depending on it's Format:
                switch (file.Extension.ToLower())
                {
                    case ".mim": SaveMyImage(file, image); break;
                    case ".bmp": SaveBitmap(file, image, new BmpBitmapEncoder()); break;
                    case ".png": SaveBitmap(file, image, new PngBitmapEncoder()); break;
                    case ".jpg":
                    case ".jpeg": SaveBitmap(file, image, new JpegBitmapEncoder()); break;
                    case ".gif": SaveBitmap(file, image, new GifBitmapEncoder()); break;
                    case ".tif": SaveBitmap(file, image, new TiffBitmapEncoder()); break;
                    default:
                        throw new ArgumentException(StringResources.UnsupportedFileTypeMessage);
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCriticalError(ex.Message);
            }
        }

        /// <summary>
        /// Loads Application's internal Image Format.
        /// </summary>
        /// <param name="file">File to open.</param>
        /// <returns>Loaded Image Instance.</returns>
        public static EditedImage LoadMyImage(FileInfo file)
        {
            EditedImage image = new EditedImage();

            // Loading Document:
            XmlDocument document = new XmlDocument();
            document.Load(file.FullName);
            
            // Getting Image Width and Height from Root Node:
            XmlNode root = document.GetElementsByTagName("MyImage")[0];
            image.Width = int.Parse(root.Attributes["Width"].Value);
            image.Height = int.Parse(root.Attributes["Height"].Value);
            
            // Getting Layers:
            XmlNodeList layerNodes = document.GetElementsByTagName("Layer");
            // Adding Layers in back Direction:
            for (int i = layerNodes.Count - 1; i >= 0; i--)
            {
                image.AddLayer();
                // Loading Layer Image:
                byte[] imageBytes = Convert.FromBase64String(layerNodes[i].InnerText);
                using (MemoryStream input = new MemoryStream(imageBytes))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = input;
                    bitmap.EndInit();
                    image.CurrentLayer.Image = bitmap;
                }
                image.CurrentLayer.Name = layerNodes[i].Attributes["Name"].Value;
                image.CurrentLayer.Visible = bool.Parse(layerNodes[i].Attributes["Visible"].Value);
            }
            return image;
        }

        /// <summary>
        /// Saves Application's internal Image Format.
        /// </summary>
        /// <param name="file">File to save into.</param>
        /// <param name="image">Image to save.</param>
        public static void SaveMyImage(FileInfo file, EditedImage image)
        {
            XmlDocument document = new XmlDocument();
            XmlElement root = document.CreateElement("MyImage");
            document.AppendChild(root);
            // Saving Image Width and Height into Root Node:
            root.SetAttribute("Width", image.Width.ToString());
            root.SetAttribute("Height", image.Height.ToString());

            // Adding Layers into the Document:
            for (int i = 0; i < image.Layers.Count; i++)
            {
                XmlElement layer = document.CreateElement("Layer");
                layer.SetAttribute("Name", image.Layers[i].Name);
                layer.SetAttribute("Visible", image.Layers[i].Visible.ToString());
                // Converting Image into a String:
                using (MemoryStream output = new MemoryStream())
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image.Layers[i].Image));
                    encoder.Save(output);

                    byte[] bytes = output.ToArray();
                    layer.InnerText = Convert.ToBase64String(bytes);
                }
                root.AppendChild(layer);
            }
            // Saving the whole Document:
            document.Save(file.FullName);
        }

        /// <summary>
        /// Loads Image from the specified Bitmap File.
        /// </summary>
        /// <param name="file">File to load From.</param>
        /// <returns>Loaded Image Instance.</returns>
        private static EditedImage LoadBitmap(FileInfo file)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(file.FullName));
            
            EditedImage image = new EditedImage();
            image.Width = (int)bitmap.Width;
            image.Height = (int)bitmap.Height;
            image.AddLayer();
            image.CurrentLayer.Image = bitmap;
            
            return image;
        }

        /// <summary>
        /// Saves Image to the specified Bitmap File.
        /// </summary>
        /// <param name="file">File to save into.</param>
        /// <param name="image">Image to save.</param>
        /// <param name="encoder">Encoder used to save the File.</param>
        private static void SaveBitmap(FileInfo file, EditedImage image, BitmapEncoder encoder)
        {
            encoder.Frames.Add(BitmapFrame.Create(image.Image));            
            FileStream output = File.Open(file.FullName, FileMode.OpenOrCreate, FileAccess.Write);
            encoder.Save(output);
            output.Close();            
        }
    }    
}
