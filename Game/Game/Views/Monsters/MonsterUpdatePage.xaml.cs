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
    /// Monster Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterUpdatePage : ContentPage
    {
        //Local storage for monster images
        private List<String> imageList = GameImagesHelper.GetMonsterImage();

        // index tracker for local storage
        private int imageIndex = 0;

        // Backup data for monster
        private readonly MonsterModel BackupData;

        // The Monster to create
        public GenericViewModel<MonsterModel> ViewModel { get; set; }

        // Hold the current location selected
        public ItemLocationEnum PopupLocationEnum = ItemLocationEnum.Unknown;

        // Empty Constructor for UTs
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Create a backup
            BackupData = new MonsterModel(data.Data);

            NameEntry.Placeholder = "Give your monster a name";
            DescriptionEntry.Placeholder = "Describe your monster";

            _ = UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Difficulty
            var difficulty = this.ViewModel.Data.Difficulty;

            // Clear the Binding and reset it
            BindingContext = null;
            BindingContext = this.ViewModel;

            ViewModel.Data.Difficulty = difficulty;

            return true;
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
                ViewModel.Data.ImageURI = new MonsterModel().ImageURI;
            }

            bool isValid = Entry_Validator();
            if(isValid == false)
            {
                return;
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
            //Tadaaa, revert the change
            ViewModel.Data.Update(BackupData);

            _ = await Navigation.PopModalAsync();
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
        /// Catch the change to the Slider for Difficulty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Difficulty_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            DifficultyValue.Text = string.Format("{0}", Math.Round(e.NewValue));

            //TODO: may need a condition to change on whole value
            //Level_Changed(null, null);
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

        /// <summary>
        /// Randomize Monster Values and Unique Items
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
        /// Randomize the Monster, keep the level the same
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

            ViewModel.Data.UniqueItem = RandomPlayerHelper.GetMonsterUniqueItem();

            _ = UpdatePageBindingContext();

            return true;
        }

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
            UpdatePageBindingContext();
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
            UpdatePageBindingContext();
        }
    }
}