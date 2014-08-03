using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Collections.ObjectModel;
using ImageEditor.Tools;

namespace ImageEditor.ViewModel
{
    /// <summary>
    /// View Model for the Toolbox.
    /// </summary>
    public class ToolboxViewModel : BindableObjectBase
    {
        /// <summary>
        /// Initializes a new Instance of current Class.
        /// </summary>
        public ToolboxViewModel()
        {
            this.backPen.Brush = this.backBrush;
            this.forePen.Brush = this.foreBrush;
            this.Thickness = 1.0f;

            this.CurrentTool = this.Tools[0];
        }

        public void SwapColors()
        {
            Color temp = this.foreColor;
            this.ForeColor = this.BackColor;
            this.BackColor = temp;
        }

        #region Properties

        public Color BackColor
        {
            get { return this.backColor; }
            set
            {
                this.backColor = value;
                this.backBrush.Color = value;                
                base.OnPropertyChanged("BackColor");
                base.OnPropertyChanged("BackBrush");
                base.OnPropertyChanged("BackPen");
            }
        }

        public Color ForeColor
        {
            get { return this.foreColor; }
            set
            {
                this.foreColor = value;
                this.foreBrush.Color = value;
                base.OnPropertyChanged("ForeColor");
                base.OnPropertyChanged("ForeBrush");
                base.OnPropertyChanged("ForePen");
            }
        }
        
        public SolidColorBrush BackBrush
        {
            get { return this.backBrush; }
        }

        public SolidColorBrush ForeBrush
        {
            get { return this.foreBrush; }
        }

        public Pen BackPen
        {
            get { return this.backPen; }
        }

        public Pen ForePen
        {
            get { return this.forePen; }
        }

        public double Thickness
        {
            get { return this.thickness; }
            set
            {
                this.thickness = value;
                this.backPen.Thickness = value;
                this.forePen.Thickness = value;
                base.OnPropertyChanged("Thickness");
                base.OnPropertyChanged("BackPen");
                base.OnPropertyChanged("ForePen");
            }
        }

        public ObservableCollection<double> LineThicknesses
        {
            get { return this.lineThicknesses; }
            set
            {
                this.lineThicknesses = value;
                base.OnPropertyChanged("LineThicknesses");
            }
        }
        
        public ObservableCollection<ICanvasCallback> Tools
        {
            get { return this.tools; }
            set
            {
                this.tools = value;
                base.OnPropertyChanged("Tools");
            }
        }

        public ICanvasCallback CurrentTool
        {
            get { return this.currentTool; }
            set
            {
                this.currentTool = value;
                base.OnPropertyChanged("CurrentTool");
            }
        }

        #endregion

        #region Field Declaration
        private Color backColor = default(Color);
        private Color foreColor = default(Color);
        private SolidColorBrush backBrush = new SolidColorBrush();
        private SolidColorBrush foreBrush = new SolidColorBrush();
        private Pen backPen = new Pen();
        private Pen forePen = new Pen();
        private double thickness = 1.0f;

        private ObservableCollection<double> lineThicknesses = new ObservableCollection<double>()
        {
            1, 3, 5, 7
        };

        private ObservableCollection<ICanvasCallback> tools = new ObservableCollection<ICanvasCallback>()
            {
                new BrushTool(),
                new LineTool(),
                new CircleTool(),
                new RectangleTool(),
                new MoveTool()
            };

        private ICanvasCallback currentTool = null;

        #endregion
    }
}
