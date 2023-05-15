using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ResourceManager.ViewModels
{
    public class PageViewModel: ViewModelBase
    {
        public PageViewModel()
        {
            SelectedPageCommand = new RelayCommand(() => mainVM.CurrentPage = this);
            GoHomeCommand = new RelayCommand(mainVM.GoHome);
        }

        public string PageTitle
        {
            get => GetProperty<string>();
            set => SetProperty(value);
        }
        public Brush PageColor { get; protected set; }

        public BitmapImage PageIcon { get; protected set; }
        public virtual bool HasPermission { get; protected set; }

        public ICommand SelectedPageCommand { get; }
        public ICommand GoHomeCommand { get; }

        public ObservableCollection<PageContentViewModel> MenuItems { get; protected set; }
        public PageContentViewModel? SelectedItem
        {
            get => GetProperty<PageContentViewModel>();
            set => SetProperty(value);
        }
    }
}
