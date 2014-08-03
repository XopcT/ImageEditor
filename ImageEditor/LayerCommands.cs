using System;
using System.Collections.Generic;
using System.Windows.Input;
using ImageEditor.View;

namespace ImageEditor
{
    /// <summary>
    /// Defines Commands for the Layers Palette.
    /// </summary>
    public static class LayerCommands
    {
        public static RoutedUICommand AddLayerCommand = new RoutedUICommand("Add Layer", "AddLayerCommand", typeof(LayersView));
        public static RoutedUICommand RemoveLayerCommand = new RoutedUICommand("Remove Layer", "RemoveLayerCommand", typeof(LayersView));
        public static RoutedUICommand MoveLayerUpCommand = new RoutedUICommand("Move Layer up", "MoveLayerUpCommand", typeof(LayersView));
        public static RoutedUICommand MoveLayerDownCommand = new RoutedUICommand("Move Layer down", "MoveLayerDownCommand", typeof(LayersView));
    }
}
