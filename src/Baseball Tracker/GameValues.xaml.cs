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

namespace Baseball_Tracker
{

    public sealed partial class GameValues : Page
    {
        string hometeamname;
        string awayteamname;
        int hometeamscore;
        int awayteamscore;
        int outs2;
        int inning2;
        string topbottom;        
        
        public GameValues(string homeTeamName, string awayTeamName, int homeTeamScore, int awayTeamScore, int outs, int inning, string topBottom)
        {
           this.InitializeComponent();

           this.homeTeamBox.Text = homeTeamName;
           this.awayTeamBox.Text = awayTeamName;
           this.homeScoreBox.Text = homeTeamScore.ToString();
           this.awayScoreBox.Text = awayTeamScore.ToString();
           this.outsBox.Text = outs.ToString();
           this.inningBox.Text = inning.ToString();

           //Off = Bottom, On = Top
           if (topBottom == "Top")
           {
               this.topBottomSwitch.IsOn = true;
           }
           
           if (topBottom == "Bottom")
           {
               this.topBottomSwitch.IsOn = false;
           }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void SaveValues_Click(object sender, RoutedEventArgs e)
        {
            if (topBottomSwitch.IsOn == true)
            {
                topbottom = "Top";
            }
            else
            {
                topbottom = "Bottom";
            }

            hometeamname = this.homeTeamBox.Text;
            awayteamname = this.awayTeamBox.Text;
            hometeamscore = Convert.ToInt32(this.homeScoreBox.Text);
            awayteamscore = Convert.ToInt32(this.awayScoreBox.Text);
            outs2 = Convert.ToInt32(this.outsBox.Text);
            inning2 = Convert.ToInt32(this.inningBox.Text);

            MainPage page = new MainPage();
            page.returnFromSettings(hometeamname, awayteamname, hometeamscore, awayteamscore, outs2, inning2, topbottom);
        }
    }
}
