using System;
using System.Linq;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// Index Page
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemIndexPage : ContentPage
    {
        // The view model, used for data binding
        readonly ItemIndexViewModel ViewModel = ItemIndexViewModel.Instance;

        // Empty Constructor for UTs
        public ItemIndexPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the ItemIndexView Model
        /// </summary>
        public ItemIndexPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
        }

        /// <summary>
        /// Call to Create a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CreateItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ItemCreatePage()));
        }

        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then set it for needing refresh
            if (ViewModel.Dataset.Count == 0)
            {
                _ = ViewModel.SetNeedsRefresh(true);
            }

            // If the needs Refresh flag is set update it
            if (ViewModel.NeedsRefresh())
            {
                ViewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = ViewModel;
        }

        /// <summary>
        /// Call for when an Item is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void FlexItem_Clicked(object sender, EventArgs e)
        {
            Frame lblClicked = (Frame)sender;
            var item = (TapGestureRecognizer)lblClicked.GestureRecognizers[0];
            var id = item.CommandParameter as String;
            var data = ViewModel.Dataset.FirstOrDefault(m => m.Id.Equals(id));

            // Open the read page
            await Navigation.PushAsync(new ItemReadPage(new GenericViewModel<ItemModel>(data)));
        }

        /// <summary>
        /// Call to go back a page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BackButton_Clicked(object sender, EventArgs e)
        {
            //Pop itself from the stack
            await Navigation.PopAsync();
        }
    }
}