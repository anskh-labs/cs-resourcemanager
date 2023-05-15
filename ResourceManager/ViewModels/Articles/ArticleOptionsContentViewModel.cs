using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetCore.Mvvm.Abstractions;
using ResourceManager.Configuration;
using ResourceManager.Helpers;
using ResourceManager.Settings;
using System;

namespace ResourceManager.ViewModels
{
    public class ArticleOptionsContentViewModel : PageContentViewModel
    {
        private AppSettings appSettings => App.ServiceProvider.GetRequiredService<IOptionsMonitor<AppSettings>>().CurrentValue;
        public ArticleOptionsContentViewModel()
        {
            MenuTitle = "Options";
            MenuIcon = AssetManager.Instance.GetImage("Options.png");
            Title = "Article Options";
            PageColor = Constants.ArticlePageColor;

            FolderPath = appSettings.ArticleSettings.FolderPath;
            SupportExtensions = appSettings.ArticleSettings.SupportExtensions;
            DeleteSourceFile =  appSettings.ArticleSettings.DeleteSourceFile.ToString();
        }
        public string FolderPath { get; }
        public string SupportExtensions { get; }
        public string DeleteSourceFile { get; }
    }
}