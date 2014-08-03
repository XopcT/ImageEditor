using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ImageEditor
{
    /// <summary>
    /// Basic Class for creating Objects which can be binded to a View.
    /// </summary>
    public class BindableObjectBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Raised when some binded Property changes it's Value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged Event.
        /// </summary>
        /// <param name="propertyName">Name of the Property who's Value was changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler temp = this.PropertyChanged;
            if (temp != null)
                temp(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
