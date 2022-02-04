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

        // Backup ViewModel for when user don't want to keep their changes
        private ItemModel BackupData;

        // Empty Constructor for Tests
        public ItemUpdatePage(bool UnitTest) { }

        //errors
        Dictionary<string, string> errors = new Dictionary<string, string>();

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public ItemUpdatePage(GenericViewModel<ItemModel> data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;

            //Create a backup for current data in the item
            BackupData = new ItemModel(data.Data);

            this.ViewModel.Title = "Update " + data.Title;

            NameEntry.Placeholder = "Give your character a name";
            DescriptionEntry.Placeholder = "Describe your character";

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = data.Data.Location.ToString();
            AttributePicker.SelectedItem = data.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save calls to Update. Validation checks are implemented to prevent saving with 
        /// empty fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            bool isValid = Entry_Validator();
            if (isValid == false)
            {
                return;
            }

            if (errors.Count > 0)
            {
                return;
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

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
            //Undo all unconfirmed changes user might have made
            this.ViewModel.Data.Update(BackupData);

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Catch the change to the Stepper for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeLabel.Text = string.Format("{0}", Math.Round(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the stepper for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueLabel.Text = string.Format("{0}", Math.Round(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the stepper for Damage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Damage_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            DamageLabel.Text = string.Format("{0}", Math.Round(e.NewValue));
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

        

        /// <summary>
        /// Validate Attribute Dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AttributePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (errors.ContainsKey("LocationAttribute"))
            { 
                errors.Remove("LocationAttribute"); 
            }

            if (this.ViewModel.Data.Attribute == AttributeEnum.Unknown)
            {
                errors["LocationAttribute"] = "Location not selected";
            }

            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);
        }

        /// <summary>
        /// Validate Location Dropdpwn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (errors.ContainsKey("LocationAttribute")) 
            { 
                errors.Remove("LocationAttribute"); 
            };

            if (this.ViewModel.Data.Attribute == AttributeEnum.Unknown)
            {
                errors["LocationAttribute"] = "Location not selected";
            }

            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);
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
        /// Validate entry fields for Name and Description
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