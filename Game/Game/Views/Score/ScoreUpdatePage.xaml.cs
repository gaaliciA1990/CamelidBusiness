using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using Game.Helpers;


namespace Game.Views
{
    /// <summary>
    /// Score Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreUpdatePage : ContentPage
    {
        // View Model for Score
        public readonly GenericViewModel<ScoreModel> ViewModel;


        // Backup ViewModel for when user don't want to keep their changes
        private ScoreModel BackupData = new ScoreModel();

        // Constructor for Unit Testing
        public ScoreUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data Score
        /// </summary>
        public ScoreUpdatePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Create a backup for current data in the item
            CopyValues(data.Data, BackupData);

            // Fill in the Character, Monster and Item images
            PopulateCharacters();
            PopulateMonsters();

            NameEntry.Placeholder = "Enter a Name";


        }

        /// <summary>
        /// Populate the characters the player played with 
        /// in their game. TODO: UPDATE SO DYNAMICALLY SHOW CHARACTERS
        /// </summary>
        private void PopulateCharacters()
        {
            //just for testing layout for dead characters list
            for (int x = 0; x <= 5; x++)
            {
                Image img = new Image();
                img.HeightRequest = 50;
                img.WidthRequest = 50;
                var character = new CharacterModel();
                var player = new PlayerInfoModel(character);
                img.Source = player.ImageURI;
                CharactersDeadList.Children.Add(img);
            }
        }

        /// <summary>
        /// Populate the monsters the player killed in their game. 
        /// TODO: UPDATE SO DYNAMICALLY SHOW MONSTERS
        /// </summary>
        private void PopulateMonsters()
        {
            //just for testing layout for dead characters list
            for (int x = 0; x <= 5; x++)
            {
                Image img = new Image();
                img.HeightRequest = 50;
                img.WidthRequest = 50;
                var monster = new MonsterModel();
                var player = new PlayerInfoModel(monster);
                img.Source = player.ImageURI;
                MonstersDeadList.Children.Add(img);
            }
        }

        /// <summary>
        /// Populate the items the player dopped in their game. 
        /// TODO: UPDATE SO DYNAMICALLY SHOW ITEMS
        /// </summary>
        /*
        private void PopulateItems()
        {
            //just for testing layout for dead characters list
            for (int x = 0; x <= 5; x++)
            {
                Image img = new Image();
                img.HeightRequest = 50;
                img.WidthRequest = 50;
                var item = new CharacterModel();
                var player = new PlayerInfoModel(item);
                img.Source = player.UniqueItem;
                ItemsDroppedList.Children.Add(img);
            }
        }
        */
        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            bool isValid = Entry_Validator();
            if (isValid == false)
            {
                return;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Revert the unsaved changes and go back to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BackButton_Clicked(object sender, EventArgs e)
        {
            //Undo all unconfirmed changes user might have made
            CopyValues(BackupData, this.ViewModel.Data);

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Helper function to help validate required input fields
        /// </summary>
        /// <returns></returns>
        public bool Entry_Validator()
        {
            bool isValid = true;

            // validate the Name has something entered
            if (string.IsNullOrWhiteSpace(this.ViewModel.Data.Name))
            {
                NameEntry.PlaceholderColor = Xamarin.Forms.Color.Red;
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Validate the Entry fields for Name
        /// are filled with valid text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry_Validator();
        }

        /// <summary>
        /// Helper function to copy data from a ScoreModel object to another ScoreModel object
        /// </summary>
        /// <param name="data"></param>
        /// <param name="copyTarget"></param>
        private void CopyValues(ScoreModel data, ScoreModel copyTarget)
        {
            //Get the Properties on each ItemModel object
            var propertiesData = data.GetType().GetProperties();
            var propertiesCopyTarget = copyTarget.GetType().GetProperties();

            //Then copy over
            for (int i = 0; i < propertiesData.Length; i++)
            {
                propertiesCopyTarget[i].SetValue(copyTarget, propertiesData[i].GetValue(data));
            }
        }
    }
}