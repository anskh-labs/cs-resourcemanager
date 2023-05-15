using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCore.Mvvm.Abstractions;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Settings;
using System;

namespace ResourceManager.ViewModels
{
    public class RepositoryOptionsContentViewModel : PageContentViewModel
    {
        private AppSettings appSettings => App.ServiceProvider.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue;
        public RepositoryOptionsContentViewModel()
        {
            MenuTitle = "Options";
            MenuIcon = AssetManager.Instance.GetImage("Options.png");
            Title = "Repository Options";
            PageColor = Constants.RepositoryPageColor;

            FolderPath = appSettings.RepositorySettings.FolderPath;
            SupportExtensions = appSettings.RepositorySettings.SupportExtensions;
            DeleteSourceFile =  appSettings.RepositorySettings.DeleteSourceFile.ToString();
        }
        public string FolderPath { get; }
        public string SupportExtensions { get; }
        public string DeleteSourceFile { get; }
    }
}