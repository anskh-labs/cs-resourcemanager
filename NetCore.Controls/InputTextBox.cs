using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NetCore.Controls
{
    public class InputTextBox : TextBox
    {
        static InputTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputTextBox), new FrameworkPropertyMetadata(typeof(InputTextBox)));
        }
    }
}
