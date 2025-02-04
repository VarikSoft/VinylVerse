using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VinylVerse.Behaviours
{
    internal class WindowDragHelper
    {
        public static void EnableDrag(Window window, UIElement draggableElement)
        {
            draggableElement.MouseDown += (sender, e) =>
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    window.DragMove();
                }
            };
        }
    }
}
