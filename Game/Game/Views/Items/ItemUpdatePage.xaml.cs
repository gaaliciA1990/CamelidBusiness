using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Game.ViewModels;
using Game.Models;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemUpdatePage : ContentPage
    {
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
            BindableLayout.SetItemsSource(errorMessageList, null);
            BindableLayout.SetItemsSource(errorMessageList, errors);
        }
    }
}