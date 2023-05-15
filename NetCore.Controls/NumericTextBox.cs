using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NetCore.Controls
{
    public class NumericTextBox : InputTextBox
    {
        static NumericTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericTextBox), new FrameworkPropertyMetadata(typeof(NumericTextBox)));
        }

        public NumericTextBox()
        {
            SetCurrentValue(ValueProperty, 0);
        }

        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(int), typeof(NumericTextBox), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                NumericTextBox ntb = (NumericTextBox)sender;
                ntb.Text = ntb.Value.ToString();
            }
        }

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value);  }
        }
        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            SetValue(ValueProperty, Text.Length == 0 ? 0 : int.Parse(Text));
        }
    }
}
