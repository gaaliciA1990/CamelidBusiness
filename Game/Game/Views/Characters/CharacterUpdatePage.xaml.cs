using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using Game.GameRules;
using Game.Helpers;

namespace Game.Views
{
    /// <summary>
    /// Character Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CharacterUpdatePage : ContentPage
    {
        //Local storage for images
        private Dictionary<CharacterClanEnum, List<string>> imageList = GameImagesHelper.GetCharacterImage();

        //index tracer for local storage
        private int imageIndex = 0;

        //backup data
        private readonly CharacterModel BackupData;

        //Flag to control whether Max Health should be updated 
        //This is a workaround for when binding cause the value on the Level slider to change
        //thus invoke ManageHealth, which is not our intention.
        private bool updateMH = false;

        // The Character to create
        public GenericViewModel<CharacterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        // Empty Constructor for UTs
        public CharacterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public CharacterUpdatePage(GenericViewModel<CharacterModel> data)
        {
            InitializeComponent();

            this.ViewModel = data;

            //Create a backup
            BackupData = new CharacterModel(data.Data);

            //_ = UpdatePageBindingContext();
            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            NameEntry.Placeholder = "Give your character a name";
            DescriptionEntry.Placeholder = "Describe your character";

            updateMH = true;

            AddItemsToDisplay();

            AdjustSliderValues();

            ClanPicker.SelectedItem = ViewModel.Data.Clan.ToString();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            //Don't update MH when re-binding
            updateMH = false;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ClanPicker.SelectedItem = ViewModel.Data.Clan.ToString();

            //Set flag to okay after binding
            updateMH = true;

            ManageHealth();

            AddItemsToDisplay();

            return true;
        }

        /// <summary>
        /// adjust attribute values to include item bonuses 
        /// </summary>
        public void AdjustValuesWithBonuses(ItemModel item)
        {
            switch (item.Attribute)
            {
                case AttributeEnum.Speed:
                    ViewModel.Data.Speed += Math.Min(0, 50 - ViewModel.Data.GetSpeedTotal);
                    SpeedValue.Text = ViewModel.Data.GetSpeedTotal.ToString();
                    SpeedSlider.Maximum = 50 - ViewModel.Data.GetSpeedTotal + ViewModel.Data.Speed;
                    break;
                case AttributeEnum.Defense:
                    ViewModel.Data.Defense += Math.Min(0, 50 - ViewModel.Data.GetDefenseTotal);
                    DefenseValue.Text = ViewModel.Data.GetDefenseTotal.ToString();
                    break;
                case AttributeEnum.Attack:
                    ViewModel.Data.Attack += Math.Min(0, 50 - ViewModel.Data.GetAttackTotal);
                    AttackValue.Text = ViewModel.Data.GetAttackTotal.ToString();
                    break;
                case AttributeEnum.MaxHealth:
                    MaxHealthValue.Text = ViewModel.Data.GetMaxHealthTotal.ToString();
                    break;
                case AttributeEnum.CurrentHealth:
                    break;
                case AttributeEnum.Unknown:
                    AdjustSliderValues();
                    break;
            }
        }

        /// <summary>
        /// Adjust slider values
        /// </summary>
        /// <param name="s"></param>
        public void AdjustSliderValues()
        {
            SpeedValue.Text = ViewModel.Data.GetSpeedTotal.ToString();
            SpeedSlider.Maximum = 50 - ViewModel.Data.GetSpeedTotal + ViewModel.Data.Speed;

            DefenseValue.Text = ViewModel.Data.GetDefenseTotal.ToString();
            DefenseSlider.Maximum = 50 - ViewModel.Data.GetDefenseTotal + ViewModel.Data.Defense;

            AttackValue.Text = ViewModel.Data.GetAttackTotal.ToString();
            AttackSlider.Maximum = 50 - ViewModel.Data.GetAttackTotal + ViewModel.Data.Attack;
        }


        /// <summary>
        /// The Level selected from the list
        /// Need to recalculate Max Health
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void Level_Changed(object sender, EventArgs args)
        {
            ManageHealth();
        }

        /// <summary>
        /// Change the Level Picker
        /// </summary>
        public void ManageHealth()
        {
            //No need to update MH when don't have to
            if(updateMH == false)
            {
                return;
            }

            // Roll for new HP
            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            // Show the Result
            MaxHealthValue.Text = ViewModel.Data.MaxHealth.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = new CharacterModel().ImageURI;
            }

            bool isValid = Entry_Validator();
            if(isValid == false)
            {
                return;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            //Tadaaa, revert the change
            ViewModel.Data.Update(BackupData);

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Slider for Level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Level_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            LevelValue.Text = string.Format("{0}", Math.Round(e.NewValue));

            AdjustSliderValues();

            //TODO: may need a condition to change on whole value
            Level_Changed(null, null);
        }

        /// <summary>
        /// Catch the change to the slider for Attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Attack_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            AttackValue.Text = string.Format("{0}", this.ViewModel.Data.GetAttackTotal);
        }

        /// <summary>
        /// Catch the change to the slider for Defense
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Defense_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            DefenseValue.Text = string.Format("{0}", this.ViewModel.Data.GetDefenseTotal);
        }

        /// <summary>
        /// Catch the change to the slider for Speed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Speed_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            SpeedValue.Text = string.Format("{0}", this.ViewModel.Data.GetSpeedTotal);
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPopupItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemModel data = args.SelectedItem as ItemModel;
            if (data == null)
            {
                return;
            }

            _ = ViewModel.Data.AddItem(PopupLocationEnum, data.Id);
            
            AddItemsToDisplay();

            AdjustValuesWithBonuses(data);

            UpdatePageBindingContext();

            AdjustSliderValues();

            ClosePopup();
        }

        /// <summary>
        /// Show the Popup for Selecting Items
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool ShowPopup(ItemLocationEnum location)
        {
            PopupItemSelector.IsVisible = true;

            PopupLocationLabel.Text = "Items for :";
            PopupLocationValue.Text = location.ToMessage();

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
            itemList.AddRange(ItemIndexViewModel.Instance.GetLocationItems(location));

            // Populate the list with the items
            PopupLocationItemListView.ItemsSource = itemList;

            // Remember the location for this popup
            PopupLocationEnum = location;

            return true;
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

            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Head));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Necklass));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.PrimaryHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.OffHand));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.RightFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.LeftFinger));
            ItemBox.Children.Add(GetItemToDisplay(ItemLocationEnum.Feet));
        }

        /// <summary>
        /// Look up the Item to Display
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public StackLayout GetItemToDisplay(ItemLocationEnum location)
        {
            // Get the Item, if it exist show the info
            // If it does not exist, show a Plus Icon for the location

            // Defualt Image is the Plus
            var ImageSource = "icon_add.png";

            var data = ViewModel.Data.GetItemByLocation(location);
            if (data == null)
            {
                data = new ItemModel { Location = location, ImageURI = ImageSource };
            }

            // Hookup the Image Button to show the Item picture
            var ItemButton = new ImageButton
            {
                Style = (Style)Application.Current.Resources["ImageMediumStyle"],
                Source = data.ImageURI,
                HeightRequest = 40,
                WidthRequest = 40
            };

            // Add a event to the user can click the item and see more
            ItemButton.Clicked += (sender, args) => ShowPopup(location);

            // Add the Display Text for the item
            var ItemLabel = new Label
            {
                Text = location.ToMessage(),
                Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Put the Image Button and Text inside a layout
            var ItemStack = new StackLayout
            {
                Padding = 3,
                Style = (Style)Application.Current.Resources["ItemImageLabelBox"],
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    ItemButton,
                    ItemLabel
                },
            };

            return ItemStack;
        }

        /// <summary>
        /// Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RollDice_Clicked(object sender, EventArgs e)
        {
            _ = DiceAnimationHandeler();

            _ = RandomizeCharacter();

            return;
        }

        /// <summary>
        /// Randomize the Character, keep the level the same
        /// </summary>
        /// <returns></returns>
        public bool RandomizeCharacter()
        {
            // Randomize Name
            ViewModel.Data.Name = RandomPlayerHelper.GetCharacterName();
            ViewModel.Data.Description = RandomPlayerHelper.GetCharacterDescription();

            // Randomize the Attributes
            ViewModel.Data.Attack = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Speed = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Defense = RandomPlayerHelper.GetAbilityValue();
            ViewModel.Data.Level = RandomPlayerHelper.GetAbilityValue();

            // Randomize an Item for Location
            ViewModel.Data.Head = RandomPlayerHelper.GetItem(ItemLocationEnum.Head);
            ViewModel.Data.Necklass = RandomPlayerHelper.GetItem(ItemLocationEnum.Necklass);
            ViewModel.Data.PrimaryHand = RandomPlayerHelper.GetItem(ItemLocationEnum.PrimaryHand);
            ViewModel.Data.OffHand = RandomPlayerHelper.GetItem(ItemLocationEnum.OffHand);
            ViewModel.Data.RightFinger = RandomPlayerHelper.GetItem(ItemLocationEnum.Finger);
            ViewModel.Data.LeftFinger = RandomPlayerHelper.GetItem(ItemLocationEnum.Finger);
            ViewModel.Data.Feet = RandomPlayerHelper.GetItem(ItemLocationEnum.Feet);

            ViewModel.Data.MaxHealth = RandomPlayerHelper.GetHealth(ViewModel.Data.Level);

            (ViewModel.Data.ImageURI, ViewModel.Data.Clan) = RandomPlayerHelper.GetCharacterImage();

            _ = UpdatePageBindingContext();

            AdjustSliderValues();

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

        /// <summary>
        /// When the right button is clicked, the image will change to the next index or the beginning of the
        /// index if at the last index. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RightButton_Clicked(object sender, EventArgs e)
        {
            int imageCount = imageList[ViewModel.Data.Clan].Count;

            //increment, if greater than imagecount loop back
            imageIndex++;
            if (imageIndex >= imageCount)
            {
                imageIndex = 0;
            }

            // Update the image
            ViewModel.Data.ImageURI = imageList[ViewModel.Data.Clan][imageIndex];
            CharacterImage.Source = ViewModel.Data.ImageURI;
        }

        /// <summary>
        /// Change to default image of changed clan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Picker_ClanChanged(object sender, EventArgs e)
        {
            imageIndex = 0;
            ViewModel.Data.ImageURI = imageList[ViewModel.Data.Clan][imageIndex];
            CharacterImage.Source = ViewModel.Data.ImageURI;
        }

        /// <summary>
        /// When the left button is clicked, the image will change to the previous index or the end of the
        /// index if at 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftButton_Clicked(object sender, EventArgs e)
        {
            int imageCount = imageList[ViewModel.Data.Clan].Count;

            //decrement, if less than imagecount loop back
            imageIndex--;
            if (imageIndex < 0)
            {
                imageIndex = imageCount - 1;
            }

            // Update the image
            ViewModel.Data.ImageURI = imageList[ViewModel.Data.Clan][imageIndex];
            CharacterImage.Source = ViewModel.Data.ImageURI;
        }

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