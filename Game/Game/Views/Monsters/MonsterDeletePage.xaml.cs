using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterDeletePage : ContentPage
    {
        // View Model for Monster
        readonly GenericViewModel<MonsterModel> viewModel;

        // Empty Constructor for UTs
        public MonsterDeletePage(bool UnitTest) { }

        // Constructor for Delete takes a view model of what to delete
        public MonsterDeletePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.viewModel = data;

            this.viewModel.Title = "Delete " + data.Title;
        }

        /// <summary>
        /// Delete calls to Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "Delete", viewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Trap the Back Button on the Phone
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            // Add your code here...
            return true;
        }

        #region DifficultyLabel

        /// <summary>
        /// Function to create a button for each difficulty level
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public Button CreateDifficultyButton()
        {
            string label = viewModel.Data.Difficulty.ToMessage();

            //Add the basic stuff first
            Button toReturn = new Button
            {
                Text = label,
                BorderRadius = 10,
                BorderWidth = 1,
                BorderColor = Xamarin.Forms.Color.Black,
                Padding = new Xamarin.Forms.Thickness(5.0),
                IsEnabled = false,
                MinimumWidthRequest = 70,
                MinimumHeightRequest = 50
            };

            return toReturn;
        }

        #endregion DifficultyLabel
    }
}