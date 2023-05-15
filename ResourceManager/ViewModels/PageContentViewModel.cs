using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResourceManager.ViewModels
{
    public class PageContentViewModel : ViewModelBase
    {
        public PageContentViewModel() 
        {
            SelectedMenuCommand = new RelayCommand(() => ((PageViewModel)mainVM.CurrentPage).SelectedItem = this);  
        }
        public ICommand SelectedMenuCommand { get; }
        public string MenuTitle { get; protected set; }
        public BitmapImage MenuIcon { get; protected set; }
        public string Title { get; protected set; }
        public Brush PageColor { get; set; }
    }
}
