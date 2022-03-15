using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AutoBattlePage : ContentPage
    {
        // Hold the Engine, so it can be swapped out for unit testing
        public IAutoBattleInterface AutoBattle = BattleEngineViewModel.Instance.AutoBattleEngine;

        // waiting time before starting the game
        public int WaitTime = 150;


        /// <summary>
        /// Constructor
        /// </summary>
        public AutoBattlePage()
        {
            InitializeComponent();
        }

        public async void AutobattleButton_Clicked(object sender, EventArgs e)
        {
            // Hide the button and begin battle message
            StartBattleButton.IsVisible = false;
            BeginBattleLabel.IsVisible = false;

            // Show battle message 
            BattleMessageValue.IsVisible = true;

            await Task.Delay(WaitTime);

            //Measure start time
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            _ = await AutoBattle.RunAutoBattle();

            //measure elapsed time
            stopwatch.Stop();
            var elapsed_time = (float)stopwatch.ElapsedMilliseconds/1000;

            await Navigation.PushModalAsync(new ScorePage(elapsed_time));

            await Navigation.PopAsync();
        }
    }
}