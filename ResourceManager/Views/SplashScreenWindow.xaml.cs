using System.Windows;

namespace ResourceManager.Views
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        public SplashScreenWindow()
        {
            InitializeComponent();
        }

        public string Message
        {
            get => message.Text;
            set => message.Text = value;
            
        }
        public double Progress
        {
            get => progressBar.Value;
            set => progressBar.Value = value;

        }
    }
}
