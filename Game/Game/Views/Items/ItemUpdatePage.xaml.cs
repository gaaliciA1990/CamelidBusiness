using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Game.ViewModels;
using Game.Models;
using Game.Helpers;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
        // List of Item images for the player to select
        private List<String> imageList = GameImagesHelper.GetItemImage();

        // Image index variable, to load first image on Create page to implement "scrolling"
        private int imageIndex = 0;

        // View Model for Item
        public readonly GenericViewModel<ItemModel> ViewModel;

        // Empty Constructor for Tests
        public ItemUpdatePage(bool UnitTest) { }

        //errors
        List<string> errors = new List<string>();

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Update " + data.Title;

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = data.Data.Location.ToString();
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            if (errors.Count > 0)
                return;

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            //Convert to int so the values stay consistent with the UI
            //ViewModel.Data.Range = (int)ViewModel.Data.Range;
            //ViewModel.Data.Value = (int)ViewModel.Data.Value;
            //ViewModel.Data.Damage = (int)ViewModel.Data.Damage;

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeLabel.Text = String.Format("{0}", Math.Round(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueLabel.Text = String.Format("{0}", Math.Round(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageLabel.Text = String.Format("{0}", Math.Round(e.NewValue));
        }


        /// <summary>
        /// Allow submission only if inputs are valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void InputsAreValidCheck(object sender, EventArgs e)
        {
            errors.Clear();

            DamageSlider.IsEnabled = false;
            RangeSlider.IsEnabled = false;
            RangeLabel.Text = "-";
            DamageLabel.Text = "-";

            if (this.ViewModel.Data.Location == ItemLocationEnum.Unknown)
                errors.Add("Location not selected");
            else if (this.ViewModel.Data.Attribute == AttributeEnum.Unknown)
                errors.Add("Attribute not selected");
            else if (this.ViewModel.Data.Location != ItemLocationEnum.PrimaryHand && this.ViewModel.Data.Attribute == AttributeEnum.Attack)
                errors.Add("Item on " + this.ViewModel.Data.Location.ToString() + " cannot be used as " + this.ViewModel.Data.Attribute.ToString());
            else if (this.ViewModel.Data.Location == ItemLocationEnum.PrimaryHand && this.ViewModel.Data.Attribute == AttributeEnum.Attack)
            {
                DamageSlider.IsEnabled = true;
                RangeSlider.IsEnabled = true;
                RangeLabel.Text = RangeSlider.Value.ToString();
                DamageLabel.Text = DamageSlider.Value.ToString();
            }

            if (this.ViewModel.Data.Name.Length <= 0)
                errors.Add("Name cannot be empty");

            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);
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
            ImageLabel.Source = this.ViewModel.Data.ImageURI;
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
            ImageLabel.Source = this.ViewModel.Data.ImageURI;
        }
    }
}