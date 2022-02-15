using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScorePage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScorePage(float? elapsedTime = null)
        {
            InitializeComponent();
            DrawOutput();

            if (elapsedTime != null)
            {
                ElapsedTime.Text = String.Format("Battle took {0:0.00} sec", elapsedTime);
            }
        }

        /// <summary>
        /// Draw data for
        /// Character
        /// Monster
        /// Item
        /// </summary>
        public void DrawOutput()
        {
            //Number of colums to display characters and monsters
            var columns = 3;
            
            //Populate Grid with Characters
            for (var x = 0; x < EngineViewModel.Engine.EngineSettings.BattleScore.CharacterModelDeathList.Count(); x++)
            {
                var col = x % columns;
                var row = (int)Math.Floor((double)x / columns);
                if (col == 0)
                    CharacterListGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            
                var data = EngineViewModel.Engine.EngineSettings.BattleScore.CharacterModelDeathList[x];
                var cell = CreateCharacterDisplayBox(data);
            
                cell.SetValue(Grid.RowProperty, row);
                cell.SetValue(Grid.ColumnProperty, col);
                CharacterListGrid.Children.Add(cell);
            }
            
            //Get duplicate counts of Monsters
            var Monsters = from x in EngineViewModel.Engine.EngineSettings.BattleScore.MonsterModelDeathList
                           group x by x.ImageURI into g
                           orderby g.Key
                           let count = g.Count()
                           select new { Value = g.First(), Count = count };
            
            //Populate Grid with Monsters
            for (var x = 0; x < Monsters.Count(); x++)
            {
                var col = x % columns;
                var row = (int)Math.Floor((double)x / columns);
                if (col == 0)
                    MonsterListGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            
                var data = Monsters.ElementAt(x).Value;
                var count = Monsters.ElementAt(x).Count;
                var cell = CreateMonsterDisplayBox(data, count);
            
                cell.SetValue(Grid.RowProperty, row);
                cell.SetValue(Grid.ColumnProperty, col);
                MonsterListGrid.Children.Add(cell);
            }
            
            //Get duplicate counts of items
            var Items = from x in EngineViewModel.Engine.EngineSettings.BattleScore.ItemModelDropList
                        where x.IsUnique = true
                        group x by x.ImageURI into g
                        let count = g.Count()
                        select new { Value = g.First(), Count = count };
            
            foreach (var data in Items)
            {
                ItemsStackLayout.Children.Add(CreateItemDisplayBox(data.Value, data.Count));
            }

            //// Update Values in the UI
            TotalKilled.Text = "Monsters Killed: " + EngineViewModel.Engine.EngineSettings.BattleScore.MonsterModelDeathList.Count().ToString();
            TotalCollected.Text = "Items Collected: " +EngineViewModel.Engine.EngineSettings.BattleScore.ItemModelDropList.Count().ToString();
            TotalScore.Text = "Score: " + EngineViewModel.Engine.EngineSettings.BattleScore.ExperienceGainedTotal.ToString();
            TotalRounds.Text = "Rounds: " + EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount.ToString();
        }

        /// <summary>
        /// Return a stack layout for the Characters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Grid CreateCharacterDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = data.ImageURI
            };

            // Add the Level under image
            var PlayerNameLabel = new Label
            {
                Text = data.Name,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Add the Level under Name
            var PlayerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a Grid
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            PlayerImage.SetValue(Grid.RowProperty, 0);
            PlayerNameLabel.SetValue(Grid.RowProperty, 1);
            PlayerLevelLabel.SetValue(Grid.RowProperty, 2);
            grid.Children.Add(PlayerImage);
            grid.Children.Add(PlayerNameLabel);
            grid.Children.Add(PlayerLevelLabel);

            return grid;
        }

        /// <summary>
        /// Return a stack layout for the Monsters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Grid CreateMonsterDisplayBox(PlayerInfoModel data, int? count = null)
        {
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = data.ImageURI
            };

            // Add count under image
            var MonsterCountLabel = new Label
            {
                Text = "x" + count.ToString(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a Grid
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            PlayerImage.SetValue(Grid.RowProperty, 0);
            MonsterCountLabel.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(PlayerImage);
            grid.Children.Add(MonsterCountLabel);

            return grid;
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Grid CreateItemDisplayBox(ItemModel data, int? count = null)
        {
            if (data == null)
            {
                data = new ItemModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a Grid
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            PlayerImage.SetValue(Grid.RowProperty, 0);
            grid.Children.Add(PlayerImage);

            return grid;
        }

        /// <summary>
        /// Close the Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }
    }
}