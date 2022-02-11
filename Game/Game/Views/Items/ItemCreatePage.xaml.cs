using Game.Models;
using Game.ViewModels;
using Game.Helpers;

using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemCreatePage : ContentPage
    {
        // List of Item images for the player to select
        private List<String> imageList = GameImagesHelper.GetItemImage();

        // Image index variable, to load first image on Create page to implement "scrolling"
        private int imageIndex = 0;

        // The item to create
        public GenericViewModel<ItemModel> ViewModel = new GenericViewModel<ItemModel>();

        // Empty Constructor for UTs
        public ItemCreatePage(bool UnitTest) { }

        // Dictionary of errors to return based on field being encountered
        Dictionary<string, string> errors = new Dictionary<string, string>();

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ItemCreatePage()
        {
            InitializeComponent();

            this.ViewModel.Data = new ItemModel();

            // Load the first image in the list when the Create page is opened
            this.ViewModel.Data.ImageURI = imageList[imageIndex];

            BindingContext = this.ViewModel;

            this.ViewModel.Title = "Create";

            // Set the entry placeholder text
            NameEntry.Placeholder = "Give your character a name";
            DescriptionEntry.Placeholder = "Describe your character";

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = ViewModel.Data.Location.ToString();
            AttributePicker.SelectedItem = ViewModel.Data.Attribute.ToString();
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            //Check if required entry fields are filled
            bool isValid = Entry_Validator();
            if (isValid == false)
            {
                return;
            }

            // Validate the Location picker is not empty on save
            if (this.ViewModel.Data.Location == ItemLocationEnum.Unknown)
            {
                errors["Location"] = "Location is required";

                // Display the error message generated
                BindableLayout.SetItemsSource(errorMessageList, null);
                BindableLayout.SetItemsSource(errorMessageList, errors);

                return; //Stop save
            }

            // Validate the Attribute picker is not empty on save
            if (this.ViewModel.Data.Attribute == AttributeEnum.Unknown)
            {
                errors["Attribute"] = "Attribute is required";

                // Display the error message generated
                BindableLayout.SetItemsSource(errorMessageList, null);
                BindableLayout.SetItemsSource(errorMessageList, errors);

                return; //Stop save
            }

            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
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

        /// <summary>
        /// Catch the change to the slider for Range
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Range_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            RangeLabel.Text = string.Format("{0}", Math.Round(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the slider for Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Value_OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            ValueLabel.Text = string.Format("{0}", Math.Round(e.NewValue));
        }

        /// <summary>
        /// Catch the change to the slider for Damage
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
        /// Validate picker field option has a valid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool locPrimaryHand = false;

            // Check the dictionary for the Location and Attribute key and remove to start fresh
            if (errors.ContainsKey("Location"))
            {
                errors.Remove("Location");
            }
            if (errors.ContainsKey("Attribute"))
            {
                errors.Remove("Attribute");
            }

            // Validate the Location picker has been selected
            if (this.ViewModel.Data.Location == ItemLocationEnum.Unknown)
            {
                errors["Location"] = "Location is required";
            }

            // Validate the Attribute picker has been selected
            if (this.ViewModel.Data.Attribute == AttributeEnum.Unknown)
            {
                errors["Attribute"] = "Attribute is required";
            }

            if (this.ViewModel.Data.Location == ItemLocationEnum.PrimaryHand)
            {
                locPrimaryHand = true;
            }

            IsDamageSliderVisible(locPrimaryHand);

            // Display the error message generated
            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);
        }

        /// <summary>
        /// Validate if Primary hand is selected in picker and show or hide the 
        /// damage details accordingly
        /// </summary>
        /// <param name="locPrimaryHand"></param>
        public void IsDamageSliderVisible(bool locPrimaryHand)
        {
            //Validate location is Primary hand and show damage slider
            if (locPrimaryHand == true)
            {
                DamageFrame.IsVisible = true;
                DisplayDamageLabel.IsVisible = true;
                DamageLabel.IsVisible = true;
                DamageSlider.IsVisible = true;
            }
            // If location is not primary hand, hide damage
            if (locPrimaryHand == false)
            {
                DamageFrame.IsVisible = false;
                DisplayDamageLabel.IsVisible = false;
                DamageLabel.IsVisible = false;
                DamageSlider.IsVisible = false;
            }
        }

        /// <summary>
        /// Helper function to validate required entry fields
        /// </summary>
        /// <returns></returns>
        private bool Entry_Validator()
        {
            bool isValid = true;

            // validate the Name something entered
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
    }
}