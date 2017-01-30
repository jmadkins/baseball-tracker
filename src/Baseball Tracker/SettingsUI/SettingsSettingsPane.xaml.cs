using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Baseball_Tracker.SettingsUI
{
    public sealed partial class SettingsSettingsPane : UserControl
    {
        public SettingsSettingsPane()
        {
            this.InitializeComponent();

        }

        private void SettingsBack_Clicked(object sender, RoutedEventArgs e)
        {
            Popup popup = this.Parent as Popup;
            if (popup != null)
            {
                popup.IsOpen = false;
            }
        }
    }
}
