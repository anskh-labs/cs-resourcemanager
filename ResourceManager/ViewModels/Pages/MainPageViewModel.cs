using Microsoft.Extensions.DependencyInjection;
using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Commands;
using NetCore.Security;
using ResourceManager.Services.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ResourceManager.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            Pages = new ObservableCollection<PageViewModel>
            {
                App.ServiceProvider.GetRequiredService<UserPageViewModel>(),
                App.ServiceProvider.GetRequiredService<PasswordPageViewModel>(),
                App.ServiceProvider.GetRequiredService<EbookPageViewModel>(),
                App.ServiceProvider.GetRequiredService<RepositoryPageViewModel>(),
                App.ServiceProvider.GetRequiredService<ArticlePageViewModel>(),
                App.ServiceProvider.GetRequiredService<ActivityPageViewModel>(),
                App.ServiceProvider.GetRequiredService<NotePageViewModel>(),
                App.ServiceProvider.GetRequiredService<ToolsPageViewModel>()
            };

            LogoutCommand = new RelayCommand(Logout);
        }

        private void Logout()
        {
            AuthManager.User.Identity = new AnonymousIdentity();
            mainVM.RefreshUI();
        }

        public string? LoginUser => string.Format(" Logout [{0}]", AuthManager.User.Identity.Name);
        public ICommand LogoutCommand { get; }
        public ObservableCollection<PageViewModel> Pages { get; }
    }
}
