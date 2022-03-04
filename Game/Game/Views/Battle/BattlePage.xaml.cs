using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Timers;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
    public partial class BattlePage : ContentPage
    {
        // HTML Formatting for message output box
        public HtmlWebViewSource htmlSource = new HtmlWebViewSource();

        // Wait time before proceeding
        public int WaitTime = 1500;

        // Hold the Map Objects, for easy access to update them
        public Dictionary<string, object> MapLocationObject = new Dictionary<string, object>();

        // Empty Constructor for UTs
        bool UnitTestSetting;
        public BattlePage(bool UnitTest) { UnitTestSetting = UnitTest; }

        //Available Empty Places To Move To
        public HashSet<MapModelLocation> AvailableLocations = new HashSet<MapModelLocation>();

        //Available Monsters To Attack
        public HashSet<MapModelLocation> AvailableMonsters = new HashSet<MapModelLocation>();

        /// <summary>
        /// Constructor
        /// </summary>
        public BattlePage()
        {
            InitializeComponent();

            // Set initial State to Starting
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;

            // Set up the UI to Defaults
            BindingContext = BattleEngineViewModel.Instance;

            // Create and Draw the Map
            _ = InitializeMapGrid();

            // Start the Battle Engine
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            //Set row and colum sizes
            for (var i = 0; i < BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapYAxiesCount; i++)
            {
                MapGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10 + i, GridUnitType.Star) });
            }
            for (var i = 0; i < BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapXAxiesCount; i++)
            {
                MapGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            // Populate the UI Map
            DrawMapGridInitialState();

            // Ask the Game engine to select who goes first
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);

            // Add Players to Display
            DrawGameAttackerDefenderBoard();

            // Set the Battle Mode
            ShowBattleMode();
        }



        /// <summary>
        /// Dray the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            var CharacterBoxList = CharacterBox.Children.ToList();
            foreach (var data in CharacterBoxList)
            {
                _ = CharacterBox.Children.Remove(data);
            }

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                CharacterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            var MonsterBoxList = MonsterBox.Children.ToList();
            foreach (var data in MonsterBoxList)
            {
                _ = MonsterBox.Children.Remove(data);
            }

            // Draw the Monsters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                MonsterBox.Children.Add(PlayerInfoDisplayBox(data));
            }

            // Add one black PlayerInfoDisplayBox to hold space in case the list is empty
            CharacterBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one black PlayerInfoDisplayBox to hold space incase the list is empty
            MonsterBox.Children.Add(PlayerInfoDisplayBox(null));

        }

        /// <summary>
        /// Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            if (data == null)
            {
                data = new PlayerInfoModel
                {
                    ImageURI = ""
                };
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["PlayerBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        #region BattleMapMode

        /// <summary>
        /// Create the Initial Map Grid
        /// 
        /// All lcoations are empty
        /// </summary>
        /// <returns></returns>
        public bool InitializeMapGrid()
        {
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.ClearMapGrid();

            return true;
        }

        /// <summary>
        /// Draw the Map Grid
        /// Add the Players to the Map
        /// 
        /// Need to have Players in the Engine first, to then put on the Map
        /// 
        /// The Map Objects are all created with the map background image first
        /// 
        /// Then the actual characters are added to the map
        /// </summary>
        public void DrawMapGridInitialState()
        {
            // Create the Map in the Game Engine
            _ = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.PopulateMapModel(BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList);

            _ = CreateMapGridObjects();

            _ = UpdateMapGrid();
        }

        /// <summary>
        /// Walk the current grid
        /// check each cell to see if it matches the engine map
        /// Update only those that need change
        /// </summary>
        /// <returns></returns>
        public bool UpdateMapGrid()
        {
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                // Use the ImageButton from the dictionary because that represents the player object
                var MapObject = GetMapGridObject(GetDictionaryImageButtonName(data));
                if (MapObject == null)
                {
                    return false;
                }

                var imageObject = (ImageButton)MapObject;

                MapObject = GetMapGridObject(GetDictionaryStackName(data));
                var gridObject = (Grid)MapObject;

                //DO SOMETHING TO SHOW WHO'S TURN IS NEXT
                //JUST CHANGING THE GRID CELL COLOR AT THE MOMENT
                if (data.Player == BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn())
                {
                    gridObject.BackgroundColor = Color.Gold;
                }
                else if (gridObject != null)
                {
                    gridObject.BackgroundColor = Color.Transparent;
                }

                // Check automation ID on the Image, That should match the Player, if not a match, the cell is now different need to update
                if (!imageObject.AutomationId.Equals(data.Player.Guid))
                {
                    // The Image is different, so need to re-create the Image Object and add it to the Stack
                    // That way the correct monster is in the box.

                    if (MapObject == null)
                    {
                        return false;
                    }

                    // Remove the ImageButton
                    gridObject.Children.RemoveAt(0);

                    var PlayerImageButton = DetermineMapImageButton(data);

                    gridObject.Children.Add(PlayerImageButton);

                    // Update the Image in the Datastructure
                    _ = MapGridObjectAddImage(PlayerImageButton, data);

                    //gridObject.BackgroundColor = DetermineMapBackgroundColor(data);
                }
            }

            return true;
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryFrameName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Frame", data.Row, data.Column);
        }

        /// <summary>
        /// Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryStackName(MapModelLocation data)
        {
            return string.Format("MapR{0}C{1}Stack", data.Row, data.Column);
        }

        /// <summary>
        /// Covert the player map location to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetDictionaryImageButtonName(MapModelLocation data)
        {
            // Look up the Frame in the Dictionary
            return string.Format("MapR{0}C{1}ImageButton", data.Row, data.Column);
        }

        /// <summary>
        /// Populate the Map
        /// 
        /// For each map position in the Engine
        /// Create a grid object to hold the Stack for that grid cell.
        /// </summary>
        /// <returns></returns>
        public bool CreateMapGridObjects()
        {
            // Make a frame for each location on the map
            // Populate it with a new Frame Object that is unique
            // Then updating will be easier

            foreach (var location in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
            {
                var data = MakeMapGridBox(location);

                // Add the Box to the UI

                MapGrid.Children.Add(data, location.Column, location.Row);
            }

            // Set the Height for the MapGrid based on the number of rows * the height of the BattleMapFrame

            var height = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapXAxiesCount * 60;

            BattleMapDisplay.MinimumHeightRequest = height;
            BattleMapDisplay.HeightRequest = height;

            return true;
        }

        /// <summary>
        /// Get the Frame from the Dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetMapGridObject(string name)
        {
            _ = MapLocationObject.TryGetValue(name, out var data);
            return data;
        }

        /// <summary>
        /// Make the Game Map Frame 
        /// Place the Character, Monster in the frame
        /// If empty, place Empty
        /// </summary>
        /// <param name="mapLocationModel"></param>
        /// <returns></returns>
        public Grid MakeMapGridBox(MapModelLocation mapLocationModel)
        {
            if (mapLocationModel.Player == null)
            {
                mapLocationModel.Player = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare;
            }

            //Format Player Image
            var PlayerImageButton = DetermineMapImageButton(mapLocationModel);
            PlayerImageButton.HorizontalOptions = LayoutOptions.FillAndExpand;
            PlayerImageButton.VerticalOptions = LayoutOptions.FillAndExpand;
            PlayerImageButton.Aspect = Aspect.AspectFit;

            //Create Player Cell
            Grid cell = new Grid();
            cell.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            cell.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            //cell.Children.Add(PlayerLabel, 0, 0);
            cell.Children.Add(PlayerImageButton, 0, 0);

            _ = MapGridObjectAddImage(PlayerImageButton, mapLocationModel);
            _ = MapGridObjectAddStack(cell, mapLocationModel);

            return cell;
        }

        /// <summary>
        /// This add the ImageButton to the stack to kep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddImage(ImageButton data, MapModelLocation MapModel)
        {
            var name = GetDictionaryImageButtonName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);

            return true;
        }

        /// <summary>
        /// This adds the Stack into the Dictionary to keep track of
        /// </summary>
        /// <param name="data"></param>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public bool MapGridObjectAddStack(Grid data, MapModelLocation MapModel)
        {
            var name = GetDictionaryStackName(MapModel);

            // First check to see if it has data, if so update rather than add
            if (MapLocationObject.ContainsKey(name))
            {
                // Update it
                MapLocationObject[name] = data;
                return true;
            }

            MapLocationObject.Add(name, data);
            return true;
        }

        /// <summary>
        /// Set the Image onto the map
        /// The Image represents the player
        /// 
        /// So a charcter is the character Image for that character
        /// 
        /// The Automation ID equals the guid for the player
        /// This makes it easier to identify when checking the map to update thigns
        /// 
        /// The button action is set per the type, so Characters events are differnt than monster events
        /// </summary>
        /// <param name="MapLocationModel"></param>
        /// <returns></returns>
        public ImageButton DetermineMapImageButton(MapModelLocation MapLocationModel)
        {
            var data = new ImageButton
            {
                Style = (Style)Application.Current.Resources["BattleMapPlayerSmallStyle"],
                Source = MapLocationModel.Player.ImageURI,

                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.AspectFit,

                // Store the guid to identify this button
                AutomationId = MapLocationModel.Player.Guid
            };

            // Show the characters, monsters, and empty cells on the grid based on playertype
            switch (MapLocationModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    data.Clicked += (sender, args) => SetSelectedCharacter(MapLocationModel);
                    break;
                case PlayerTypeEnum.Monster:
                    data.Clicked += (sender, args) => SetSelectedMonster(MapLocationModel);
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    data.Clicked += (sender, args) => SetSelectedEmpty(MapLocationModel);

                    // Use the blank cell
                    data.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare.ImageURI;
                    break;
            }

            return data;
        }

        /// <summary>
        /// Set the Background color for the tile.
        /// Monsters and Characters have different colors
        /// Empty cells are transparent
        /// </summary>
        /// <param name="MapModel"></param>
        /// <returns></returns>
        public Color DetermineMapBackgroundColor(MapModelLocation MapModel)
        {
            string BattleMapBackgroundColor;
            switch (MapModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    BattleMapBackgroundColor = "BattleMapCharacterColor";
                    break;
                case PlayerTypeEnum.Monster:
                    BattleMapBackgroundColor = "BattleMapMonsterColor";
                    break;
                case PlayerTypeEnum.Unknown:
                default:
                    BattleMapBackgroundColor = "BattleMapTransparentColor";
                    break;
            }

            var result = (Color)Application.Current.Resources[BattleMapBackgroundColor];
            return result;
        }

        #region MapEvents
        /// <summary>
        /// Event when an empty location is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedEmpty(MapModelLocation data)
        {
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction == ActionEnum.Move)
            {
                if (AvailableLocations.Contains(data))
                {
                    BattleEngineViewModel.Instance.Engine.EngineSettings.MoveMapLocation = new CordinatesModel { Row = data.Row, Column = data.Column };
                    NextAttackExample(ActionEnum.Move, data);
                    BattleEngineViewModel.Instance.Engine.EngineSettings.MoveMapLocation = null;
                    BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
                }
            }
            return true;
        }

        /// <summary>
        /// Event when a Monster is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedMonster(MapModelLocation data)
        {
            //Check if user can select monster to attack
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction == ActionEnum.Attack)
            {
                //procede with attack if character can reach monster with range
                var attacker = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();
                var inRange = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.IsTargetInRange(attacker, data.Player);
                if (inRange)
                {
                    NextAttackExample(ActionEnum.Attack, data);
                    BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
                }
                else
                {

                }
            }

            data.IsSelectedTarget = true;
            return true;
        }

        /// <summary>
        /// Event when a Character is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedCharacter(MapModelLocation data)
        {
            // TODO: Implement animation (wiggles)

            /*
             * This gets called when the characters is clicked on
             * Usefull if you want to select the character and then set state or do something
             * 
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */

            return true;
        }
        #endregion MapEvents

        #endregion BattleMapMode

        #region BasicBattleMode

        /// <summary>
        /// Draw the UI for
        ///
        /// Attacker vs Defender Mode
        /// 
        /// </summary>
        public void DrawGameAttackerDefenderBoard()
        {
            // Clear the current UI
            DrawGameBoardClear();

            // Show Characters across the Top
            DrawPlayerBoxes();

            // Draw the Map
            _ = UpdateMapGrid();

            // Show the Attacker and Defender
            DrawGameBoardAttackerDefenderSection();
        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender
        /// </summary>
        public void DrawGameBoardAttackerDefenderSection()
        {
            BattlePlayerBoxVersus.Text = "";
            
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker == null)
            {
                return;
            }
            
            AttackerImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.ImageURI;
            AttackerName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.Name;
            AttackerHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.GetMaxHealthTotal.ToString();

            // Show what action the Attacker used
            AttackerAttack.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.PreviousAction.ToImageURI();
            
            var item = ItemIndexViewModel.Instance.GetItem(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PrimaryHand);
            if (item != null && BattleEngineViewModel.Instance.Engine.EngineSettings.PreviousAction == ActionEnum.Attack)
            {
                AttackerAttack.Source = item.ImageURI;
            }

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender == null)
            {
                return;
            }

            DefenderImage.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.ImageURI;
            DefenderName.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Name;
            DefenderHealth.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetCurrentHealthTotal.ToString() + " / " + BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.GetMaxHealthTotal.ToString();

            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender.Alive == false)
            {
                _ = UpdateMapGrid();
                DefenderImage.BackgroundColor = Color.Red;
            }
            
            BattlePlayerBoxVersus.Text = "vs";

        }

        /// <summary>
        /// Draws the Game Board Attacker and Defender areas to be null
        /// </summary>
        public void DrawGameBoardClear()
        {
            AttackerImage.Source = string.Empty;
            AttackerName.Text = string.Empty;
            AttackerHealth.Text = string.Empty;

            DefenderImage.Source = string.Empty;
            DefenderName.Text = string.Empty;
            DefenderHealth.Text = string.Empty;
            DefenderImage.BackgroundColor = Color.Transparent;

            BattlePlayerBoxVersus.Text = string.Empty;
        }

        /// <summary>
        /// Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            NextAttackExample();
        }

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Setttings_Clicked(object sender, EventArgs e)
        {
            await ShowBattleSettingsPage();
        }

        /// <summary>
        /// Next Attack Example
        /// 
        /// This code example follows the rule of
        /// 
        /// Auto Select Attacker
        /// Auto Select Defender
        /// 
        /// Do the Attack and show the result
        /// 
        /// So the pattern is Click Next, Next, Next until game is over
        /// 
        /// </summary>
        public void NextAttackExample(ActionEnum action = ActionEnum.Unknown, MapModelLocation data = null)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Get the turn, set the current player and attacker to match
            SetAttackerAndDefender(action, data);

            // Hold the current state
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            //show action 
            showActionPopup(BattleEngineViewModel.Instance.Engine.EngineSettings.PreviousAction);

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            DrawGameAttackerDefenderBoard();

            if (RoundCondition == RoundEnum.NewRound)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("New Round");

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();
                return;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                _ = BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                _ = Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                return;
            }

            //If Round is not over, allow user to choose action for next player.
            //just for characters right now, idk
            PlayerTurnPickAction();
        }


        /// <summary>
        //allow user to chose action
        /// </summary>
        public void PlayerTurnPickAction()
        {
            var attacker = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();

            var attackerLocation = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetLocationForPlayer(attacker);
            if (attacker.PlayerType == PlayerTypeEnum.Character)
            {
                //AttackerName.Text = attacker.Name + "'s turn, select an action";
                //AttackerAttack.Source = "";
                //DefenderName.Text = "";

                //Check if player can move on this turn
                AvailableLocations = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetAvailableLocationsFromPlayer(attackerLocation);

                //if cant character is trapped, dont allow user to select move option
                selectLocationButton.IsEnabled = true;
                selectLocationButton.BackgroundColor = Color.Transparent;
                if (AvailableLocations.Count() == 1)
                {
                    selectLocationButton.IsEnabled = false;
                    selectLocationButton.BackgroundColor = Color.DarkGray;
                }

                //if cant attack character, dont allow user to select attack option 
                selectMonsterButton.IsEnabled = false;
                selectMonsterButton.BackgroundColor = Color.DarkGray;
                AvailableMonsters.Clear();
                foreach (var monster in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).Where(m => m.Alive == true).ToList())
                {
                    var inRange = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.IsTargetInRange(attacker, monster);
                    if (inRange)
                    {
                        selectMonsterButton.IsEnabled = true;
                        selectMonsterButton.BackgroundColor = Color.Transparent;

                        var monsterLocation = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetLocationForPlayer(monster);
                        AvailableMonsters.Add(monsterLocation);
                    }
                }

                // if character's turn, show select action popup
                SelectAction.IsVisible = true;

                AttackButton.IsVisible = false;
            }
            //need to 
            else
            {
                AttackButton.IsVisible = true;
            }
        }

        /// <summary>
        /// Allow user to click on opponent space when choosing attack action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SelectMonsterToAttack(object sender, EventArgs args)
        {
            foreach (var location in AvailableMonsters)
            {
                var MapObject = (Grid)GetMapGridObject(GetDictionaryStackName(location));
                MapObject.BackgroundColor = Color.Blue;
            }

            var attacker = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();
            SelectAction.IsVisible = false;
            AttackerName.Text = "Select Monster to attack";
            AttackerAttack.Source = "";
            DefenderName.Text = "";
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;
            visualizeAttackOptions(attacker);
        }

        /// <summary>
        /// Do something to show user attack options
        /// </summary>
        /// <param name=""></param>
        public void visualizeAttackOptions(PlayerInfoModel attacker)
        {
            foreach (var monster in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                var inRange = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.IsTargetInRange(attacker, monster);
                if (inRange)
                {
                    var monsterLocation = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetLocationForPlayer(monster);
                    var MapObject = (Grid)GetMapGridObject(GetDictionaryStackName(monsterLocation));
                    MapObject.BackgroundColor = Color.Blue;
                }
            }
        }




        /// <summary>
        /// Allow user to click on empty space when choosing move action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void SelectLocationToMoveTo(object sender, EventArgs args)
        {
            foreach (var location in AvailableLocations)
            {
                var MapObject = (Grid)GetMapGridObject(GetDictionaryStackName(location));
                MapObject.BackgroundColor = Color.Blue;
            }

            SelectAction.IsVisible = false;
            AttackerName.Text = "Select a location to move to";
            AttackerAttack.Source = "";
            DefenderName.Text = "";
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Move;
        }

        /// <summary>
        /// Skip turn, +2 health
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void skipTurn(object sender, EventArgs args)
        {
            SelectAction.IsVisible = false;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Skip;
            NextAttackExample(ActionEnum.Skip, null);
        }




        /// <summary>
        /// Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender(ActionEnum action = ActionEnum.Unknown, MapModelLocation data = null)
        {
            var attacker = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();
            var attackerLocation = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.GetLocationForPlayer(attacker);

            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn());
            _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(null);


            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    if (action == ActionEnum.Attack)
                    {
                        _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(data.Player);
                    }
                    break;

                case PlayerTypeEnum.Monster:
                default:

                    // Monsters turn, so auto pick a Character to Attack
                    _ = BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine.Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker));
                    break;
            }
        }


        public async void showActionPopup(ActionEnum action) 
        {
            if (action == ActionEnum.Attack)
            {
                if (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.Hit ||
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit) 
                { 
                    ActionPopupMessage.Source = "attackpopup.png";
                }
                else if (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.Miss ||
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalMiss)
                {
                    ActionPopupMessage.Source = "misspopup.png";
                }
            }
            else if (action == ActionEnum.Move)
            {
                ActionPopupMessage.Source = "movepopup.png";
            }
            else {
                return;
            }
            ActionPopup.IsVisible = true;
            await Task.Delay(500);
            ActionPopup.IsVisible = false;
        }


        /// <summary>
        /// Game is over
        /// 
        /// Show Buttons
        /// 
        /// Clean up the Engine
        /// 
        /// Show the Score
        /// 
        /// Clear the Board
        /// 
        /// </summary>
        public void GameOver()
        {
            // Save the Score to the Score View Model, by sending a message to it.
            var Score = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            ShowBattleMode();
        }
        #endregion BasicBattleMode

        #region MessageHandelers

        /// <summary>
        /// Builds up the output message
        /// </summary>
        /// <param name="message"></param>
        public void GameMessage()
        {
            // Output The Message that happened.
            BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage, BattleMessages.Text);

            Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage))
            {
                BattleMessages.Text = string.Format("{0} \n{1}", BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage, BattleMessages.Text);
            }

            //htmlSource.Html = BattleEngineViewModel.Instance.Engine.BattleMessagesModel.GetHTMLFormattedTurnMessage();
            //HtmlBox.Source = HtmlBox.Source = htmlSource;
        }

        /// <summary>
        ///  Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            BattleMessages.Text = "";
            htmlSource.Html = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.GetHTMLBlankMessage();
            //HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers

        #region PageHandelers

        /// <summary>
        /// Battle Over, so Exit Button
        /// Need to show this for the user to click on.
        /// The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;
            ShowBattleMode();

            //commented out because we don't want to show the new round button again
            //await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        /// Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        /// Show the Round Over page
        /// 
        /// Round Over is where characters get items
        /// 
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        /// Show Settings
        /// </summary>
        public async Task ShowBattleSettingsPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new BattleSettingsPage());
        }
        #endregion PageHandelers

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowBattleMode();
        }

        /// <summary>
        /// 
        /// Hide the differnt button states
        /// 
        /// Hide the message display box
        /// 
        /// </summary>
        public void HideUIElements()
        {
            NextRoundButton.IsVisible = false;
            StartBattleButton.IsVisible = false;
            AttackButton.IsVisible = false;
            MessageDisplayBox.IsVisible = false;
            BattlePlayerInfomationBox.IsVisible = false;
        }

        /// <summary>
        /// Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode, 
            if (UnitTestSetting)
            {
                return;
            }

            HideUIElements();

            ClearMessages();

            DrawPlayerBoxes();

            // Update the Mode
            //BattleModeValue.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum.ToMessage();

            ShowBattleModeDisplay();

            ShowBattleModeUIElements();
        }

        /// <summary>
        /// Control the UI Elements to display
        /// </summary>
        public void ShowBattleModeUIElements()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum)
            {
                case BattleStateEnum.Starting:
                    //GameUIDisplay.IsVisible = false;
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    StartBattleButton.IsVisible = true;
                    BattleMapDisplay.IsVisible = false;
                    break;

                case BattleStateEnum.NewRound:
                    _ = UpdateMapGrid();
                    AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    NextRoundButton.IsVisible = true;
                    break;

                case BattleStateEnum.GameOver:
                    // Migrate to the score page, since this is the game over state
                    Navigation.PushAsync(new ScorePage());
                    break;

                case BattleStateEnum.RoundOver:
                case BattleStateEnum.Battling:
                    Page.BackgroundImageSource = "BattleBackground.jpg";
                    GameUIDisplay.IsVisible = true;
                    BattlePlayerInfomationBox.IsVisible = true;
                    MessageDisplayBox.IsVisible = true;
                    AttackButton.IsVisible = true;
                    break;

                // Based on the State disable buttons
                case BattleStateEnum.Unknown:
                default:
                    break;
            }
        }

        /// <summary>
        /// Control the Map Mode or Simple
        /// </summary>
        public void ShowBattleModeDisplay()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum)
            {
                case BattleModeEnum.MapAbility:
                case BattleModeEnum.MapFull:
                case BattleModeEnum.MapNext:
                    GamePlayersTopDisplay.IsVisible = false;
                    BattleMapDisplay.IsVisible = true;
                    break;

                case BattleModeEnum.SimpleAbility:
                case BattleModeEnum.SimpleNext:
                case BattleModeEnum.Unknown:
                default:
                    GamePlayersTopDisplay.IsVisible = true;
                    BattleMapDisplay.IsVisible = false;
                    break;
            }
        }
    }
}