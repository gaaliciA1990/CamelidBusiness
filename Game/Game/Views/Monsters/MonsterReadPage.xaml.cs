using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

using Game.ViewModels;
using Game.Models;
using System.Linq;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterReadPage : ContentPage
    {
        // View Model for Monster
        public readonly GenericViewModel<MonsterModel> ViewModel;

        // Empty Constructor for UTs
        public MonsterReadPage(bool UnitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public MonsterReadPage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            AddItemsToDisplay();
            DifficultyStack.Children.Add(CreateDifficultyButton());
            AttackBar.Progress = (float)ViewModel.Data.Attack / 20;
            DefenseBar.Progress = (float)ViewModel.Data.Defense / 20;
            SpeedBar.Progress = (float)ViewModel.Data.Speed / 20;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterUpdatePage(ViewModel)));
            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterDeletePage(ViewModel)));
            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// Back button clicked to return to index page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new MonsterIndexPage()));
        }

        #region UniqueItemDisplay
        /// <summary>
        /// Show the Items the Character has
        /// </summary>
        public void AddItemsToDisplay()
        {
            var FlexList = ItemBox.Children.ToList();
            foreach (var data in FlexList)
            {
                _ = ItemBox.Children.Remove(data);
            }

            //Add a StackLayout for each of the children 
            //Placeholder, unique items will have their unique location enum
            ItemBox.Children.Add(GetItemToDisplay());
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay()
        {
            // Get the Item, if it exist show the info
            // If it does not exist, show a Plus Icon for the location

            // Defualt Image is the Plus
            var ImageSource = "icon_add.png";

            //Get the current unique item's string id
            var neededID = ViewModel.Data.UniqueItem;
            //Find the unique Item by its id
            var data = ItemIndexViewModel.Instance.UniqueItems.Where(a => a.Id.Equals(neededID)).FirstOrDefault();

            if (data == null)
            {
                data = new ItemModel {ImageURI = ImageSource};
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI,
                IsEnabled = false,
            };

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = "Unique Drop",
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        #endregion UniqueItemDisplay

        #region Difficulty

        /// <summary>
        /// Function to create a button for each difficulty level
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public Button CreateDifficultyButton()
        {
            string label = ViewModel.Data.Difficulty.ToMessage();

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

        #endregion Difficulty
    }
}