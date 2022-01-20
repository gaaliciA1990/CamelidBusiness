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
        private ItemModel BackupData = new ItemModel();

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
            CopyValues(data.Data, BackupData);

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
            CopyValues(BackupData, this.ViewModel.Data);

            _ = await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Helper function to copy data from an ItemModel object to another ItemModel object
        /// </summary>
        /// <param name="data"></param>
        /// <param name="copyTarget"></param>
        private void CopyValues(ItemModel data, ItemModel copyTarget)
        {
            //Get the Properties on each ItemModel object
            var propertiesData = data.GetType().GetProperties();
            var propertiesCopyTarget = copyTarget.GetType().GetProperties();

            //Then copy over
            for (int i = 0; i < propertiesData.Length; i++)
            {
                propertiesCopyTarget[i].SetValue(copyTarget, propertiesData[i].GetValue(data));
            }
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

        /// <summary>
        /// Validate name entry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Check the dictionary for the Name and Description key and remove to start fresh
            if (errors.ContainsKey("Name"))
            {
                errors.Remove("Name");
            }
            if (errors.ContainsKey("Description"))
            {
                errors.Remove("Description");
            }

            // validate the Name has something entered
            if (String.IsNullOrWhiteSpace(this.ViewModel.Data.Name))
            {
                errors["Name"] = "Name is required";
            }

            // validate the Description has something entered
            if (String.IsNullOrWhiteSpace(this.ViewModel.Data.Description))
            {
                errors["Description"] = "Description is required";
            }

            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);
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
            if (errors.ContainsKey("LocationAttribute")) { errors.Remove("LocationAttribute"); };

            if (this.ViewModel.Data.Attribute == AttributeEnum.Unknown)
                errors["LocationAttribute"] = "Location not selected";


            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);
        }
    }
}