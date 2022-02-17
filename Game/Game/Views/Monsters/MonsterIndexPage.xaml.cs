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
    public partial class MonsterIndexPage : ContentPage
    {
        // The view model, used for data binding
        public readonly MonsterIndexViewModel ViewModel = MonsterIndexViewModel.Instance;

        // Empty Constructor for UTs
        public MonsterIndexPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the MonsterIndexView Model
        /// </summary>
        public MonsterIndexPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
        }

        /// <summary>
        /// The monster selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void FlexMonster_Clicked(object sender, EventArgs args)
        {
            var button = sender as ImageButton;
            if (button == null)
                return;
            var id = button.CommandParameter as string;
            var data = ViewModel.Dataset.FirstOrDefault(m => m.Id.Equals(id));

            await Navigation.PushAsync(new MonsterReadPage(new GenericViewModel<MonsterModel>(data)));
        }



        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CreateMonster_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterCreatePage(new GenericViewModel<MonsterModel>())));
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
        /// Call to go back a page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NavigationPage(new VillagePage()));
        }
    }
}