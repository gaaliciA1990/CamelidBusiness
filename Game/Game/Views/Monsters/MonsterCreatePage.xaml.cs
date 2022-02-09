using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using Game.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.GameRules;
using Game.Models;
using Game.ViewModels;
using Game.Helpers;
using Game.GameRules;

namespace Game.Views
{
    /// <summary>
    /// Create Monster
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterCreatePage : ContentPage
    {
        //placeholder texts
        private readonly string nameHolder = "Give it a name";
        private readonly string descriptionHolder = "Describe your monster";

        //Local storage for images
        private List<String> imageList = GameImagesHelper.GetMonsterImage();

        //index tracer for local storage
        private int imageIndex = 0;

        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        //Hold the current difficulty selected
        public Button CurrentDifficulty;

        // Empty Constructor for UTs
        public MonsterCreatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterCreatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            data.Data = new MonsterModel();
            this.ViewModel = data;
            this.ViewModel.Title = "Create";

            //Default difficulty is selected when a new monster is first created
            ViewModel.Data.Difficulty = DifficultyEnum.Easy;
            AddDifficultySelections();

            _ = UpdatePageBindingContext();

            //Erase default info
            // TODO: update this when we get connected to the database
            NameEntry.Placeholder = nameHolder;
            DescriptionEntry.Placeholder = descriptionHolder;
            DescriptionEntry.Text = "";
            NameEntry.Text = "";
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the difficulty
            var difficulty = this.ViewModel.Data.Difficulty;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ViewModel.Data.Difficulty = difficulty;

            AddItemsToDisplay();

            return true;
        }


        #region SaveCancelButtons
        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            //Check if the required input fields are filled
            bool isValid = Entry_Validator();
            if (isValid == false)
            {
                return;
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new MonsterModel().ImageURI;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }
        #endregion SaveCancelButtons

        #region Popup
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

            //Add the items to the stack for Unique Items         
            ItemBox.Children.Add(GetItemToDisplay());
        }

        /// <summary>
        /// Show the Popup for Selecting Items
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup()
        {
            PopupItemSelector.IsVisible = true;

            PopupLocationLabel.Text = "Items for Unique Drop:";
            //PopupLocationValue.Text = location.ToMessage();

            // Make a fake item for None
            var NoneItem = new ItemModel
            {
                Id = null, // will use null to clear the item
                Guid = "None", // how to find this item amoung all of them
                ImageURI = "icon_cancel.png",
                Name = "None",
                Description = "None"
            };

            List<ItemModel> itemList = new List<ItemModel>
            {
                NoneItem
            };

            // Add the rest of the items to the list
            itemList.AddRange(ItemIndexViewModel.Instance.UniqueItems);

            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;

            return true;
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
                Source = data.ImageURI
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup();

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
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageLabelBox"],
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            //Save the id of the selected item to the monster's unique item field
            ViewModel.Data.UniqueItem = data.Id;

            UpdatePageBindingContext();

            AddItemsToDisplay();

            ClosePopup();
        }

        /// <summary>
        /// When the user clicks the close in the Popup
        /// hide the view
        /// show the scroll view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ClosePopup_Clicked(object sender, EventArgs e)
        {
            ClosePopup();
        }

        /// <summary>
        /// Close the popup
        /// </summary>
        public void ClosePopup()
        {
            PopupItemSelector.IsVisible = false;
        }

        #endregion Popup

        #region AttributeEventHandlers
        /// <summary>
        /// Catch the change to Difficulty level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveDifficulty(object sender, DifficultyEnum difficulty)
        {
            //Initate the current selection
            if (CurrentDifficulty == null)
            {
                CurrentDifficulty = (Button)sender;
            }

            //Release the current selection first
            CurrentDifficulty.IsEnabled = true;

            //Subsequent assignments
            CurrentDifficulty = (Button)sender;
            CurrentDifficulty.IsEnabled = false;

            //Save the difficulty selected
            ViewModel.Data.Difficulty = difficulty;
        }

        /// <summary>
        /// Create selections for Difficulty levels
        /// </summary>
        public void AddDifficultySelections()
        {
            StackLayout stackOne = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.CenterAndExpand },
                        stackTwo = new StackLayout() { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.CenterAndExpand };
            int buttonCounts = 0;
           
            //Add selections to difficulty stack
            foreach (var level in Enum.GetValues(typeof(DifficultyEnum)))
            {
                //Skip Unknown
                if ((DifficultyEnum)level == DifficultyEnum.Unknown)
                {
                    continue;
                }
                var button = CreateDifficultyButton((DifficultyEnum)level);

                if (buttonCounts < 3)
                {
                    stackOne.Children.Add(button);
                    buttonCounts++;
                }
                if (buttonCounts == 3)
                {
                    stackTwo.Children.Add(button);
                }

                //Save current selection as well
                if ((DifficultyEnum)level == ViewModel.Data.Difficulty)
                {
                    CurrentDifficulty = button;
                    CurrentDifficulty.IsEnabled = false;
                }
            }

            DifficultyStack.Children.Add(stackOne);
            DifficultyStack.Children.Add(stackTwo);
        }

        /// <summary>
        /// Function to create a button for each difficulty level
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        public Button CreateDifficultyButton(DifficultyEnum difficulty)
        {
            string label = difficulty == DifficultyEnum.Difficult ? "Difficult" : difficulty.ToMessage();

            //Add the basic stuff first
            Button toReturn = new Button
            {
                Text = label,
                BackgroundColor = Xamarin.Forms.Color.Beige,
                BorderRadius = 10,
                BorderWidth = 1,
                BorderColor = Xamarin.Forms.Color.Black,
                Padding = new Xamarin.Forms.Thickness(5.0)
            };

            //Add the event handler
            toReturn.Clicked += (sender, args) => SaveDifficulty(sender, difficulty);


            return toReturn;
        }

        /// <summary>
        /// Catch the change to the Slider for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = string.Format("{0}", Math.Round(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the Slider for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = string.Format("{0}", Math.Round(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the Slider for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = string.Format("{0}", Math.Round(e.NewValue));
        }
        #endregion AttributeEventHandlers

        #region Randomize
        /// <summary>
        /// Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RollDice_Clicked(object sender, EventArgs e)
        {
            _ = DiceAnimationHandeler();

            _ = RandomizeMonster();

            return;
        }

        /// <summary>
        /// 
        /// Randomize the Monster
        /// Keep the Level the Same
        /// 
        /// </summary>
        /// <returns></returns>
        public bool RandomizeMonster()
        {
            // Randomize Name
            ViewModel.Data.Name = RandomPlayerHelper.GetMonsterName();
            ViewModel.Data.Description = RandomPlayerHelper.GetMonsterDescription();

            // Randomize the Attributes
            ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();

            ViewModel.Data.Difficulty = RandomPlayerHelper.GetMonsterDifficultyValue();

            ViewModel.Data.ImageURI = RandomPlayerHelper.GetMonsterImage();

            ViewModel.Data.UniqueItem = RandomPlayerHelper.GetRandomUniqueItem();

            _ = UpdatePageBindingContext();

            return true;
        }

        /// <summary>
        /// Setup the Dice Animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool DiceAnimationHandeler()
        {
            // Animate the Rolling of the Dice
            var image = RollDice;
            uint duration = 1000;

            var parentAnimation = new Animation();

            // Grow the image Size
            var scaleUpAnimation = new Animation(v => image.Scale = v, 1, 2, Easing.SpringIn);

            // Spin the Image
            var rotateAnimation = new Animation(v => image.Rotation = v, 0, 360);

            // Shrink the Image
            var scaleDownAnimation = new Animation(v => image.Scale = v, 2, 1, Easing.SpringOut);

            parentAnimation.Add(0, 0.5, scaleUpAnimation);
            parentAnimation.Add(0, 1, rotateAnimation);
            parentAnimation.Add(0.5, 1, scaleDownAnimation);

            parentAnimation.Commit(this, "ChildAnimations", 16, duration, null, null);

            return true;
        }
        #endregion Randomize

        #region LeftRightButtons
        /// <summary>
        /// When the right button is clicked, the image will change to the next index or the beginning of the
        /// index if at the last index. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightButton_Clicked(object sender, EventArgs e)
        {
            int imageCount = imageList.Count;

            // check if we are at the last photo and move to first photo when clicked
            if (imageIndex == imageCount - 1)
            {
                imageIndex = 0;
            }

            // Move to the next photo in the list
            if (imageIndex < imageCount - 1)
            {
                imageIndex++;
            }

            // Update the image
            this.ViewModel.Data.ImageURI = imageList[imageIndex];
            ImageLabel.Source = ViewModel.Data.ImageURI;
        }

        /// <summary>
        /// When the left button is clicked, the image will change to the previous index or the end of the
        /// index if at 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftButton_Clicked(object sender, EventArgs e)
        {
            int imageCount = imageList.Count;

            // check if we are at the first photo and move to last photo when clicked
            if (imageIndex == 0)
            {
                imageIndex = imageCount - 1;
            }

            // Move to the previous photo in the list
            if (imageIndex > 0)
            {
                imageIndex--;
            }

            // Update the image
            this.ViewModel.Data.ImageURI = imageList[imageIndex];
            ImageLabel.Source = ViewModel.Data.ImageURI;
        }
        #endregion LeftRightButtons

        /// <summary>
        /// Helper function to help validate required input fields
        /// </summary>
        /// <returns></returns>
        private bool Entry_Validator()
        {
            bool isValid = true;

            // validate the Name has something entered
            if (string.IsNullOrWhiteSpace(this.ViewModel.Data.Name))
            {
                NameEntry.PlaceholderColor = Xamarin.Forms.Color.Red;
                isValid = false;
            }

            // validate the Description has something entered
            if (string.IsNullOrWhiteSpace(this.ViewModel.Data.Description))
            {
                DescriptionEntry.PlaceholderColor = Xamarin.Forms.Color.Red;
                isValid = false;
            }

            return isValid;
        }

        /// <summary>
        /// Validate the Entry fields for Name and Descriptions
        /// are filled with valid text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry_Validator();
        }
    }
}