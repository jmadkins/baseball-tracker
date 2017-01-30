/* COPYRIGHT NOTICE
 * Dealer Manager
 * Copyright (c) Adkins Software Development 2009-2013
 */

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
using Windows.UI.Popups;

namespace Baseball_Tracker
{
    /* The code has been devided into 5 regions to make viewing all 400+ lines of code easier :)
         * Game Variables = All code here declares all the variables that are going to be used: balls, strikes, outs, scores, team names, etc.
         * Required Code = All code here are required by the application or by other functions within outher sections of code: messages, popup controls, etc.
         * AppBar Extras = All code here pertains to the buttons and their related functions that are on the AppBar
         * Balls, Strikes, Points Buttons = All code here pretains to the + and - buttons on the main page
         * Events/Actions = All code here are functions such as: gameOver, threeOuts, fourBalls, etc.
         */

    public sealed partial class MainPage : Page
    {               
        #region Game Variables

        /* Set default values when app launches
         * This will clear any previous values from the app
         * without the user having to press the "Start Game" button
         */ 

        int balls = 0;
        int strikes = 0; 
       
        int outs = 0;    
    
        int inning = 1; 
       
        int homeTeamScore = 0;
        int awayTeamScore = 0;
        
        /* This is the temporary score for the inning
         * It is added to the teams score at the end of the inning
         */
        int scoreTemp = 0;

        /* The max inning must alwasy be 1 number higher than the desired ending inning
         * If it is not, it will not allow that inning to be played
         * When it is, it allows the game to be played until the bottom of said inning
         */ 
        int maxInnings = 10; 

        //Team Names
        string home = "Home";
        string away = "Away";

        string topBottom = "Top";
        
        string pitchingTeam;
        string battingTeam;

        #endregion

        #region Required Code

        //Used to load the XAML page and define variables to start the game
        public MainPage()
        {
            this.InitializeComponent();

            //Define first pitching and batting teams
            pitchingTeam = home;
            battingTeam = away;

            //Define that it is the top of the inning
            topBottom = "Top";

            currentlyPitching.Text = pitchingTeam;
            inningNumber.Text = inning.ToString() + " (" + topBottom + ")";
            inningValue.Text = scoreTemp.ToString();
            ballsValue.Text = balls.ToString();
            strikesValue.Text = strikes.ToString();
            outsValue.Text = "Outs: " + outs;
            homeScore.Text = home + " Score: " + homeTeamScore;
            awayScore.Text = away + " Score: " + awayTeamScore;
        }

        //Used to update the labels with the Variables
        public void updateDisplay()
        {
            /* This must be called at the end of most functions
             * in order to update the display of MainPage.xaml
             * If it is not, the variables will be changed,
             * but the new calues will not be reflected to the user
             */

            currentlyPitching.Text = pitchingTeam;
            inningNumber.Text = inning.ToString() + " (" + topBottom + ")";
            inningValue.Text = scoreTemp.ToString();
            ballsValue.Text = balls.ToString();
            strikesValue.Text = strikes.ToString();
            outsValue.Text = "Outs: " + outs;
            homeScore.Text = home + " Score: " + homeTeamScore;
            awayScore.Text = away + " Score: " + awayTeamScore;
        }

        //Handles the results of AppBar Extras -> resetGame
        private void CommandInvokedHandler(IUICommand command)
        {
            //The command label ("Yes" or "No") is passed into this if statement as command.Label
            if (command.Label == "Yes")
            {
                //Resets all variables to their default values            
                balls = 0;
                strikes = 0;
                outs = 0;
                inning = 1;
                homeTeamScore = 0;
                awayTeamScore = 0;
                scoreTemp = 0;
                home = "Home";
                away = "Away";
                topBottom = "Top";
                pitchingTeam = home;
                battingTeam = away;

                updateDisplay();
            }
        }

        //Required for Popup control
        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }
        
        #endregion

        #region AppBar Extras

        public async void resetGame()
        {
            //Ask user if they would like to reset the Game
            //If they want to, we go to CommandInvokedHandler            
            var messageDialog = new MessageDialog("Are you sure that you want to reset the game?", "Reset game");
            messageDialog.Commands.Add(new UICommand("Yes", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            messageDialog.Commands.Add(new UICommand("No", new UICommandInvokedHandler(this.CommandInvokedHandler)));
            await messageDialog.ShowAsync();
        }

        private void ResetCount_Click(object sender, RoutedEventArgs e)
        {
            resetCount();
        }

        private async void batterOut_Click(object sender, RoutedEventArgs e)
        {
            PopupMenu menu = new PopupMenu();

            menu.Commands.Add(new UICommand("Reset Count", (command) =>
            {
                strikeOut();
            }));

            menu.Commands.Add(new UICommand("Keep Count", (command) =>
            {
                //Batter/Runner is out at the base

                if (outs == 2)
                {
                    //If they are 2 outs already, we want to go ahead and call StrikeOut 
                    //so that way the count gets reset in the process

                    strikeOut();
                }
                else
                {
                    outs++;
                }

                updateDisplay();
            }));

            var chosenCommand = await menu.ShowForSelectionAsync(GetElementRect((FrameworkElement)sender));
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            resetGame();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Settings page
        }

        private void GameValues_Click(object sender, RoutedEventArgs e)
        {
            //Code to change the values off stuff
            /* Home Score
             * Away Score
             * Outs
             * Top or bottom
             */
        }
        #endregion

        #region Balls, Strikes, Points Buttons

        private void ballMinus_Click(object sender, RoutedEventArgs e)
        {
            if (balls == 0)
            {
                //Nothing
            }
            else
            {
                balls--;
            }

            updateDisplay();
        }

        private void ballAdd_Click(object sender, RoutedEventArgs e)
        {
            if (balls == 3)
            {
                resetCount();
            }
            else
            {
                balls++;
            }

            updateDisplay();
        }

        private void strikesMinus_Click(object sender, RoutedEventArgs e)
        {
            if (strikes == 0)
            {
                //Nothing
            }
            else
            {
                strikes--;
            }

            updateDisplay();
        }

        private void strikesAdd_Click(object sender, RoutedEventArgs e)
        {
            if (strikes == 2)
            {
                strikeOut();
            }
            else
            {
                strikes++;
            }

            updateDisplay();
        }

        private void inningScoreMinus_Click(object sender, RoutedEventArgs e)
        {
            if (scoreTemp == 0)
            {
                //Nothing
            }
            else
            {
                scoreTemp--;
            }

            updateDisplay();
        }

        private void inningScoreAdd_Click(object sender, RoutedEventArgs e)
        {
            scoreTemp++;

            updateDisplay();
        }

        #endregion
        
        #region Events/Actions

        public void resetCount()
        {
            balls = 0;
            strikes = 0;

            updateDisplay();
        }

        public void threeOuts()
        {
            //Add Scores to team scores
            if (battingTeam == home)
            {
                homeTeamScore = homeTeamScore + scoreTemp;
            }
            else
            {
                awayTeamScore = awayTeamScore + scoreTemp;
            }

            //Resets scoreTemp and outs
            scoreTemp = 0;
            outs = 0;

            //Switch batting teams and adjust inning from top to bottom or bottom to next inning
            if (battingTeam == away)
            {
                battingTeam = home;
                pitchingTeam = away;
                topBottom = "Bottom";
            }
            else
            {
                battingTeam = away;
                pitchingTeam = home;
                inning++;
                topBottom = "Top";
            }

            updateDisplay();

            if (maxInnings == inning)
            {
                if (homeTeamScore == awayTeamScore)
                {
                    //We are now in extra innings
                }
                else
                {
                    gameOver();
                }
            }
        }

        public void strikeOut()
        {
            /* ONLY CALL WHEN BATTER STRIKES OUT
             * Simply set the balls and stikes to zero 
             * Ads one to the out count, if it's three, calls 'threeOuts' 
             */ 

            balls = 0;
            strikes = 0;

            outs++;

            if (outs == 3)
            {
                threeOuts();
            }

            updateDisplay();
        }

        public void fourBalls()
        {
            resetCount();
        }

        private async void gameOver()
        {
            if (homeTeamScore > awayTeamScore)
            {
                var messageDialog = new MessageDialog("The home team has won the game!", "Winner!");
                messageDialog.Commands.Add(new UICommand("Ok"));
                await messageDialog.ShowAsync();
            }

            if (homeTeamScore < awayTeamScore)
            {
                var messageDialog = new MessageDialog("The away team has won the game!", "Winner!");
                messageDialog.Commands.Add(new UICommand("Ok"));
                await messageDialog.ShowAsync();
            }
            else
            {
                var messageDialog = new MessageDialog("The game is currently tied. YOu are now going into extra innings.", "Game is tied");
                messageDialog.Commands.Add(new UICommand("Ok"));
                await messageDialog.ShowAsync();
            }
        }

        #endregion
    }
}