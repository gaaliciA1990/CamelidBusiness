using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;
using System.Diagnostics;

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

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoBattlePage()
        {
            InitializeComponent();
        }

        public async void AutobattleButton_Clicked(object sender, EventArgs e)
        {
            //Measure start time
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // Call into Auto Battle from here to do the Battle...

            // To See Level UP happening, a character needs to be close to the next level
            var Character = new CharacterModel
            {
                ExperienceTotal = 300,    // Enough for next level
                Name = "Mike Level Example",
                Speed = 100,    // Go first
            };

            var CharacterPlayer = new PlayerInfoModel(Character);

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            _ = await AutoBattle.RunAutoBattle();

            //var BattleMessage = string.Format("Done {0} Rounds", AutoBattle.Battle.EngineSettings.BattleScore.RoundCount);

            //BattleMessageValue.Text = BattleMessage;

            //AutobattleImage.Source = "troll6_d.gif";


            //measure elapsed time
            stopwatch.Stop();
            var elapsed_time = (float)stopwatch.ElapsedMilliseconds/1000;

            await Navigation.PushModalAsync(new ScorePage(elapsed_time));
        }
    }
}