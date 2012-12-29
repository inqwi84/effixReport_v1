using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace EffixReportSystem.Controls
{
    /// <summary>
    /// Displays a WebBrowser control over a given placement target element in a WPF Window.
    /// The owner window can be transparent, but not this one, due mixing DirectX and GDI drawing. 
    /// WebBrowserOverlayWF uses WinForms to avoid this limitation.
    /// </summary>
    public partial class WebBrowserOverlay : UserControl
    {
        public WebBrowser WebBrowser
        {
            get; set;
        }
    };
}
