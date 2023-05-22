using NetCore.Mvvm.Abstractions;
using NetCore.Mvvm.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace NetCore.Mvvm.ViewModels
{
    /// <summary>
    /// Implementation in View
    /// <Grid>
    ///     <!--View Content-->
    ///     <Grid>
    ///         <!--Content Sample-->
    ///         <TextBlock Text="Hello World" />
    ///     </Grid>
    ///     <!--End View Content-->
    ///     <!--Popup Content-->
    ///     <Grid x:Name="Popup_Container" HorizontalAlignment="Stretch" VerticalAlignment="Stretch" Visibility="{Binding IsPopupVisible, Converter={StaticResource VisibilityConverter}}" >
    ///        <Border HorizontalAlignment = "Stretch" VerticalAlignment="Stretch" Background="Black" Opacity="0.6" />
    ///        <DockPanel HorizontalAlignment = "Center" VerticalAlignment="Center" Background="Transparent">
    ///            <local:ViewModelPresenter ViewModel = "{Binding CurrentPopup}" />
    ///        </ DockPanel >
    ///    </Grid>
    ///    <!--End Popup Content-->
    /// </Grid>
    /// </summary>
    public abstract class HasPopupViewModel : PropertyChangedBase, IPopupable
    {
        public PopupViewModel? CurrentPopup 
        { 
            get => GetProperty<PopupViewModel>();
            set => SetProperty(value);
        }
        public bool IsPopupVisible 
        { 
            get => GetProperty<bool>();
            set => SetProperty(value);
        }
        public void HidePopup()
        {
            IsPopupVisible = false;
            if (CurrentPopup != null)
            {
                CurrentPopup.CloseHandler -= PopupViewModel_CloseHandler;
                CurrentPopup = null;
            }
        }

        public PopupViewModel? ShowPoup(PopupViewModel popupViewModel)
        {
            popupViewModel.CloseHandler += PopupViewModel_CloseHandler;
            CurrentPopup = popupViewModel;
            IsPopupVisible = true;

            while (IsPopupVisible)
            {
                if (Dispatcher.CurrentDispatcher.HasShutdownStarted || Dispatcher.CurrentDispatcher.HasShutdownFinished) break;
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
                Thread.Sleep(20);
            }

            return popupViewModel;
        }
        private void PopupViewModel_CloseHandler(object? sender, EventArgs e)
        {
            HidePopup();
        }
    }
}
