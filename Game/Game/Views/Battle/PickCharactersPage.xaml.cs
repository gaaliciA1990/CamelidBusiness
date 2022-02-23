using System;
using System.ComponentModel;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// Selecting Characters for the Game
    /// 
    /// Characters can only be selected once with CollectionView. 
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickCharactersPage : ContentPage
    {

        // Empty Constructor for UTs
        public PickCharactersPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the CharacterIndexView Model
        /// </summary>
        public PickCharactersPage()
        {
            InitializeComponent();

            BindingContext = BattleEngineViewModel.Instance;
            //BindingContext = BattleEngineViewModel.Instance;

            // Clear the Database List and the Party List to start
            BattleEngineViewModel.Instance.PartyCharacterList.Clear();

            //UpdateNextButtonState();
        }

        /// <summary>
        /// The character selected from the collection view (multiple can be selected, 
        /// and they are stored in an IReadOnlyList)
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnDatabaseCharacterItemSelected(object sender, SelectionChangedEventArgs args)
        {
            // Check if the party count is equal to or greater than the party max
            if (args.CurrentSelection.Count > BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters)
            {
                // Reset the selected item to the previous selection of 6, so they won't have more than 6
                // characters selected in the right column. But the left column still selects more than 6
                CharactersListView.SelectedItems = args.PreviousSelection.ToList();
                UpdateNextButtonState();
                return;
            }

            // Check if the party count is less than our party max 
            if (args.CurrentSelection.Count <= BattleEngineViewModel.Instance.Engine.EngineSettings.MaxNumberPartyCharacters)
            {
                // Clear the party list to ensure we don't duplicate the party list when reading it
                BattleEngineViewModel.Instance.PartyCharacterList.Clear();

                // populate the list of characters in the party based args passed. 
                foreach (CharacterModel character in args.CurrentSelection)
                {
                    BattleEngineViewModel.Instance.PartyCharacterList.Add(character);
                }
            }

            UpdateNextButtonState();

        }

        ///// <summary>
        ///     COLLECTIONVIEW ALLOWS FOR DESELECT, NO NEED FOR A MANUAL DESELECTION IN THE 
        ///     PARTY VIEW
        ///     
        ///// The row selected from the list
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="args"></param>
        //public void OnPartyCharacterItemSelected(object sender, SelectionChangedEventArgs args)
        //{
        //    CharacterModel data = args.CurrentSelection as CharacterModel;
        //    if (data == null)
        //    {
        //        return;
        //    }

        //    // Manually deselect Character.
        //    PartyListView.SelectedItem = null;

        //    // Remove the character from the list
        //    _ = BattleEngineViewModel.Instance.PartyCharacterList.Remove(data);

        //    //UpdateNextButtonState();
        //}


        /// <summary>
        /// Start Game button is dependent on characters being in battele
        /// 
        /// If no selected characters, disable the button
        /// 
        /// 
        /// </summary>
        public void UpdateNextButtonState()
        {
            StartBattleButton.IsEnabled = true;

            var currentCount = BattleEngineViewModel.Instance.PartyCharacterList.Count();
            if (currentCount == 0)
            {
                StartBattleButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Jump to the Battle
        /// 
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BattleButton_Clicked(object sender, EventArgs e)
        {
            CreateEngineCharacterList();

            await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// Clear out the old list and make the new list
        /// </summary>
        public void CreateEngineCharacterList()
        {
            // Clear the currett list
            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Clear();

            // Load the Characters into the Engine
            foreach (var data in BattleEngineViewModel.Instance.PartyCharacterList)
            {
                data.CurrentHealth = data.GetMaxHealthTotal;
                BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(data));
            }
        }

        /// <summary>
        /// 
        /// Redirect back to the game page when the home button is clicked.
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void homeButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new GamePage()));
        }

    }
}