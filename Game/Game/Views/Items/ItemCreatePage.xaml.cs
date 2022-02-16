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
        //Variable to keep track of the items in location picker
        public List<string> myLocationItems = ItemLocationEnumHelper.GetListItem;

        // List of Item images for the player to select
        public List<String> imageList = GameImagesHelper.GetItemImage();

        // Image index variable, to load first image on Create page to implement "scrolling"
        public int imageIndex = 0;

        // The item to create
        public GenericViewModel<ItemModel> ViewModel = new GenericViewModel<ItemModel>();

        // Empty Constructor for UTs
        public ItemCreatePage(bool UnitTest) { }

        // Dictionary of errors to return based on field being encountered
        public Dictionary<string, string> errors = new Dictionary<string, string>();

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
            NameEntry.Placeholder = "Give it a name";
            DescriptionEntry.Placeholder = "Describe your item";

            //Need to make the SelectedItem a string, so it can select the correct item.
            LocationPicker.SelectedItem = ViewModel.Data.Location.ToString();
            AttributePicker.SelectedItem = ViewModel.Data.Attribute.ToString();
        }

        #region SaveAndCancel
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

        #endregion SaveAndCancel

        #region EventHandlers
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
        /// Validate picker field option has a valid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttributePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker_Validator();

            //Make damage slider visible if needed
            IsDamageSliderVisible();

            // Display the error message generated
            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);
        }

        /// <summary>
        /// Validate picker field option has a valid input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LocationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var obj = (Xamarin.Forms.Picker)sender;
            if (myLocationItems[obj.SelectedIndex] == "Ear")
            {
                ViewModel.Data.Location = ItemLocationEnum.Finger;
            }
            
            if (myLocationItems[obj.SelectedIndex] != "Ear")
            {
                ViewModel.Data.Location = ItemLocationEnumHelper.ConvertStringToEnum(myLocationItems[obj.SelectedIndex]);
            }
            
            
            Picker_Validator();
            
            //Make damage slider visible if needed
            IsDamageSliderVisible();
            
            //Otherwise just update the selected item
            obj.SelectedItem = ViewModel.Data.Location == ItemLocationEnum.Finger ? "Ear" : ViewModel.Data.Location.ToString();
        }

        public void Picker_Validator()
        {
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

            // Display the error message generated
            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);

        }

        /// <summary>
        /// Validate if Primary hand is selected in picker and show or show the 
        /// damage details accordingly
        /// </summary>
        /// <param name="locPrimaryHand"></param>
        public void IsDamageSliderVisible()
        {
            
            bool isLocPrimaryHand = this.ViewModel.Data.Location == ItemLocationEnum.PrimaryHand;

            // Show/Hide Range
            ShowDamageOption(isLocPrimaryHand);
        }

        /// <summary>
        /// Helper function to either show or show the Damage selection on Item Create Page
        /// </summary>
        /// <param name="hide"></param>
        public void ShowDamageOption(bool show)
        {
            DamageFrame.IsVisible = show;
            DisplayDamageLabel.IsVisible = show;
            DamageLabel.IsVisible = show;
            DamageSlider.IsVisible = show;
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

        #endregion EventHandlers

        #region ImageSelection

        /// <summary>
        /// When the left button is clicked, the image will change to the previous index or the end of the
        /// index if at 0.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LeftButton_Clicked(object sender, EventArgs e)
        {
            int imageCount = imageList.Count;

            // check if we are at the first photo and move to last photo when clicked
            if (imageIndex == 0)
            {
                imageIndex = imageCount - 1;
            }

            // Move to the previous photo in the list
            else if (imageIndex > 0)
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
        public void RightButton_Clicked(object sender, EventArgs e)
        {
            int imageCount = imageList.Count;

            // check if we are at the last photo and move to first photo when clicked
            if (imageIndex == imageCount - 1)
            {
                imageIndex = 0;
            }

            // Move to the next photo in the list
            else if (imageIndex < imageCount - 1)
            {
                imageIndex++;
            }

            // Update the image
            this.ViewModel.Data.ImageURI = imageList[imageIndex];
            ImageLabel.Source = this.ViewModel.Data.ImageURI;
        }

        #endregion ImageSelection

        #region PickerHandler
        /// <summary>
        /// Special handler for Location Earing since it needs to be converted back to our default of Finger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void ChangeSelectedItem()
        //{
            //Check if the value being changed is Ear, if yes, we need to convert before handing over to StringEnum
        
            //ViewModel.Data.Location;
        
            //{Binding Data.Location, Converter={StaticResource StringEnum}, Mode=TwoWay}"
        
            //AttributePicker.SelectedItem = 
        //}

        #endregion PickerHandler
    }
}