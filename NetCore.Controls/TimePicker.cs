using System;
using System.Windows;
using System.Windows.Controls;

namespace NetCore.Controls
{
    public class TimePicker : UserControl
    {
        private TextBox? _currentTextbox;

        static TimePicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
        }

        public TimePicker()
        {
            SetCurrentValue(TimeProperty, DateTime.Now);
        }

        public override void OnApplyTemplate()
        {
            var btn_up = Extensions.FindChild<Button>(this, "Up_Button");
            btn_up.Click += Up_Click;
            var btn_down = Extensions.FindChild<Button>(this, "Down_Button");
            btn_down.Click += Down_Click;
            var txt_hour = Extensions.FindChild<TextBox>(this, "Txt_Hour");
            txt_hour.LostFocus += Txt_LostFocus;
            var txt_minute = Extensions.FindChild<TextBox>(this, "Txt_Minute");
            txt_minute.LostFocus += Txt_LostFocus; ;
            base.OnApplyTemplate();
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            _currentTextbox = e.Source as TextBox;
        }

        public DateTime Time
        {
            get { return (DateTime)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public static readonly DependencyProperty TimeProperty =
            DependencyProperty.Register("Time", typeof(DateTime), typeof(TimePicker), new FrameworkPropertyMetadata(DateTime.Now, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTimeChanged));

        private static void OnTimeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue != null)
            {
                //TimePicker tp = (TimePicker)sender;
            }
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if(_currentTextbox != null)
            {
                if (_currentTextbox.Name.Equals("Txt_Hour"))
                {
                    Time = Time.AddHours(1);
                }
                else if (_currentTextbox.Name.Equals("Txt_Minute"))
                {
                    Time = Time.AddMinutes(1);
                }
            }
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (_currentTextbox != null)
            {
                if (_currentTextbox.Name.Equals("Txt_Hour"))
                {
                    Time = Time.AddHours(-1);
                }
                else if (_currentTextbox.Name.Equals("Txt_Minute"))
                {
                    Time = Time.AddMinutes(-1);
                }
            }
        }
    }
}
