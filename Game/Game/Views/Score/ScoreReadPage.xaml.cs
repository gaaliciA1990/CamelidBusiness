using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreReadPage : ContentPage
    {
        // View Model for Score
        public readonly GenericViewModel<ScoreModel> ViewModel;

        public ScoreReadPage(bool UnitTest) { }

        public List<string> strList = new List<string>() { "One", "Two", "Three" };

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public ScoreReadPage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();


            BindingContext = this.ViewModel = data;

            //just for testing layout stuff
            for (int x = 0; x < 5; x++)
            {
                Grid grid = new Grid();
                ImageButton img = new ImageButton();
                var chr = new CharacterModel();
                var plr = new PlayerInfoModel(chr);
                img.Source = plr.ImageURI;
                grid.Children.Add(img);
                CharactersAtDeath.Children.Add(grid);
            }
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScoreUpdatePage(ViewModel)));
            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ScoreDeletePage(ViewModel)));
            _ = await Navigation.PopAsync();
        }



        public async void BackButton_Clicked(object sender, EventArgs e)
        {
            //Pop itself off the stack
            await Navigation.PopAsync();
        }
    }
}