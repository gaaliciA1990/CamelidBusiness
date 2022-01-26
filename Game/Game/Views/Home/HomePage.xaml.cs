using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public HomePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Redirects the player to the Game screen to start their game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void GameButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        /// <summary>
        /// Redirects the player to the About page where they can see information about the game
        /// and the creators of the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AboutButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
        }
    }
}