using Caliburn.Micro;
using Windows.Foundation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Baseball_Tracker
{
    sealed partial class App : Application
    {
        Popup _settingsPopup;
        double _settingsWidth = 346; // Use 646 for wide settings screen

        private appSettings appSettings;

        private void RegisterAppSettings()
        {
            SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
        }

        private void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand about = new SettingsCommand("SettingsId", "About", (x) =>
            {
                Rect windowBounds = Window.Current.Bounds;

                _settingsPopup = new Popup()
                {
                    IsLightDismissEnabled = true,
                    Width = _settingsWidth,
                    Height = windowBounds.Height
                };
                _settingsPopup.Closed += OnPopupClosed;
                _settingsPopup.SetValue(Canvas.LeftProperty, windowBounds.Width - _settingsWidth);
                _settingsPopup.SetValue(Canvas.TopProperty, 0);

                SettingsUI.AboutSettingsPane aboutSettingsPane = new SettingsUI.AboutSettingsPane()
                {
                    Width = _settingsWidth,
                    Height = windowBounds.Height,
                };

                _settingsPopup.Child = aboutSettingsPane;

                _settingsPopup.IsOpen = true;
            });

            SettingsCommand settings = new SettingsCommand("SettingsId", "Settings", (x) =>
            {
                Rect windowBounds = Window.Current.Bounds;

                _settingsPopup = new Popup()
                {
                    IsLightDismissEnabled = true,
                    Width = _settingsWidth,
                    Height = windowBounds.Height,
                };
                _settingsPopup.Closed += OnPopupClosed;
                _settingsPopup.SetValue(Canvas.LeftProperty, windowBounds.Width - _settingsWidth);
                _settingsPopup.SetValue(Canvas.TopProperty, 0);

                SettingsUI.SettingsSettingsPane preferencesSettingsPane = new SettingsUI.SettingsSettingsPane()
                {
                    Width = _settingsWidth,
                    Height = windowBounds.Height,
                };

                _settingsPopup.Child = preferencesSettingsPane;

                _settingsPopup.IsOpen = true;
            });

            args.Request.ApplicationCommands.Add(about);
            args.Request.ApplicationCommands.Add(settings);
        }

        private void OnPopupClosed(object sender, object e)
        {
            _settingsPopup.Closed -= OnPopupClosed;

            appSettings newSettings = (_settingsPopup.Child as FrameworkElement).DataContext as appSettings;
            if (newSettings != null)
            {
                this.appSettings = newSettings;

                EventAggregator.Instance.Publish(this.appSettings);
            }
        }
    }
}

