using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemReadPage : ContentPage
    {
        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        //List<string> test = new List<string>(Enum.GetNames(typeof(ItemLocationEnum)));
        //LocationPicker.SelectedItem = ViewModel.Data.Location.ToString();
        




        // Empty Constructor for UTs
        public ItemReadPage(bool UnitTest) { }

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public ItemReadPage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
            LocationPicker.SelectedItem = data.Data.Location.ToString();
            LocationPicker.IsEnabled = false;
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();
            AttributePicker.IsEnabled = false;

            DamageSlider.IsEnabled = false;
            RangeSlider.IsEnabled = false;
            ValueSlider.IsEnabled = false;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemUpdatePage(ViewModel)));
            _ = await Navigation.PopAsync();
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemDeletePage(ViewModel)));
            _ = await Navigation.PopAsync();
        }

        public async void BackButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemIndexPage()));
        }
    }
}