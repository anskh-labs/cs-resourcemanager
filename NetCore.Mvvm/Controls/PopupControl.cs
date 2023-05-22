using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetCore.Mvvm.Controls
{
    public class PopupControl : ContentControl
    {
        static PopupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupControl), new FrameworkPropertyMetadata(typeof(PopupControl)));
        }
    }
}
