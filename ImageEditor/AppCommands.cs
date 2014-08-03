using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ImageEditor
{
    /// <summary>
    /// Defines Commands for the main Application Window.
    /// </summary>
    public static class AppCommands
    {
        public static RoutedUICommand ProcessImageCommand = new RoutedUICommand("Process Image", "ProcessImageCommand", typeof(MainWindow));

        public static RoutedUICommand LayersPaletteCommand = new RoutedUICommand("Show Layers Palette", "LayersPaletteCommand", typeof(MainWindow));
    }
}
