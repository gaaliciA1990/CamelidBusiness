using NUnit.Framework;

using Game;
using Game.Views;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

using Game.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests.Views
{
    [TestFixture]
    public class ItemCreatePageTests : ItemCreatePage
    {
        App app;
        ItemCreatePage page;

        public ItemCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemCreatePage();
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Save_Clicked_Null_Image_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldValue = 0.0;
            var newValue = 1.0;

            var args = new ValueChangedEventArgs(oldValue, newValue);

            // Act
            page.Value_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange

            page = new ItemCreatePage();
            var oldRange = 0.0;
            var newRange = 1.0;

            var args = new ValueChangedEventArgs(oldRange, newRange);

            // Act
            page.Range_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        {
            // Arrange
            page = new ItemCreatePage();
            var oldDamage = 0.0;
            var newDamage = 1.0;

            var args = new ValueChangedEventArgs(oldDamage, newDamage);

            // Act
            page.Damage_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemCreatePage_Valid_Pickers_Speed_and_Feet_Should_Pass()
        {
            // Arrange
            page.errors = new Dictionary<string, string> { { "Location", "resetted" }, { "Attribute", "resetted" } };
            var locationPicker = (Picker)page.FindByName("LocationPicker");
            var AttributePicker = (Picker)page.FindByName("AttributePicker");

            // Act
            locationPicker.SelectedIndex = 0;
            AttributePicker.SelectedIndex = 0;
            page.Picker_Validator();

            // Reset

            // Assert
            Assert.IsTrue(page.errors.ContainsKey("Location") == false);
            Assert.IsTrue(page.errors.ContainsKey("Attribute") == false);
        }

        [Test]
        public void ItemCreatePage_PrimaryHand_Location_Unknown_Attribute_Should_Not_Show_Error()
        {
            // Arrange
            page.errors = new Dictionary<string, string> { { "Location", "test" }, { "Attribute", "test" } };
            var locationPicker = (Picker)page.FindByName("LocationPicker");

            // Act
            locationPicker.SelectedIndex = 0;
            page.Picker_Validator();

            // Reset

            // Assert
            Assert.IsTrue(page.errors.ContainsKey("Location") == false);
            Assert.IsTrue(page.errors.ContainsKey("Attribute") == true);
        }

        [Test]
        public void ItemCreatePage_Unknown_Location_Unknown_Attribute_Should_Not_Show_Error()
        {
            // Arrange
            page.errors = new Dictionary<string, string> { { "Location", "test" }, { "Attribute", "test" } };

            // Act
            page.Picker_Validator();

            // Reset

            // Assert
            Assert.IsTrue(page.errors.ContainsKey("Location") == true);
            Assert.IsTrue(page.errors.ContainsKey("Attribute") == true);
        }


        [Test]
        public void ItemCreatePage_ShowDamage_Show_true_Should_Show()
        {
            // Arrange
            var DamageFrame = (Frame)page.FindByName("DamageFrame");
            var DisplayDamageLabel = (Label)page.FindByName("DisplayDamageLabel");

            // Act
            page.ShowDamageOption(true);

            // Reset

            // Assert
            Assert.IsTrue(DamageFrame.IsVisible == true);
            Assert.IsTrue(DisplayDamageLabel.IsVisible == true);
        }

        [Test]
        public void ItemCreatePage_ShowDamage_Show_false_Should_not_Show()
        {
            // Arrange
            var DamageFrame = (Frame)page.FindByName("DamageFrame");
            var DisplayDamageLabel = (Label)page.FindByName("DisplayDamageLabel");

            // Act
            page.ShowDamageOption(false);

            // Reset

            // Assert
            Assert.IsTrue(DamageFrame.IsVisible == false);
            Assert.IsTrue(DisplayDamageLabel.IsVisible == false);
        }

        [Test]
        public void ItemCreatePage_Save_Click_Inalid_Location_And_Attribute_Should_Show_Errors()
        {
            // Arrange
            page.ViewModel.Data.Name = "test";
            page.ViewModel.Data.Description = "test";
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            //Reset

            //Assert
            Assert.IsTrue(page.errors.ContainsKey("Location") == true);
            Assert.IsTrue(page.errors.ContainsKey("Attribute") == false);

        }

        [Test]
        public void ItemCreatePage_Save_Click_Valid_Location_And_Attribute_Should_Show_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = "test";
            page.ViewModel.Data.Description = "test";
            page.ViewModel.Data.Location = ItemLocationEnum.Head;
            page.ViewModel.Data.Attribute = AttributeEnum.Defense;
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            //Reset

            //Assert
            Assert.IsTrue(page.errors.ContainsKey("Location") == false);
            Assert.IsTrue(page.errors.ContainsKey("Attribute") == false);
        }


        [Test]
        public void ItemCreatePage_Save_Click_Inalid_Attribute_Should_Show_Errors()
        {
            // Arrange
            page.ViewModel.Data.Name = "test";
            page.ViewModel.Data.Description = "test";
            page.ViewModel.Data.Location = ItemLocationEnum.Head;
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            //Assert
            Assert.IsTrue(page.errors.ContainsKey("Attribute") == true);
        }

        [Test]
        public void ItemCreatePage_Save_Click_Valid_Attribute_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = "test";
            page.ViewModel.Data.Description = "test";
            page.ViewModel.Data.Location = ItemLocationEnum.Finger;
            page.ViewModel.Data.Attribute = AttributeEnum.MaxHealth;
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            // Reset

            //Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void ItemCreatePage_AttributePicker_SelectedIndexChanged_Valid_Location_And_Attribute_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = "test";
            page.ViewModel.Data.Description = "test";
            page.ViewModel.Data.Location = ItemLocationEnum.Finger;
            page.ViewModel.Data.Attribute = AttributeEnum.MaxHealth;

            // Act
            page.AttributePicker_SelectedIndexChanged(null, null);

            // Reset

            // Assert
            Assert.IsTrue(page.errors.ContainsKey("Location") == false);
            Assert.IsTrue(page.errors.ContainsKey("Attribute") == false);
        }

        [Test]
        public void ItemCreatePage_LocationPicker_SelectedIndexChanged()
        {
            // Arrange
            var LocationPicker = (Picker)page.FindByName("LocationPicker");
            LocationPicker.SelectedIndex = 1; 

            // Act
            page.LocationPicker_SelectedIndexChanged(LocationPicker, null);

            // Reset

            //Assert
            Assert.IsTrue(page.ViewModel.Data.Location == ItemLocationEnum.Finger);
        }

        [Test]
        public void ItemCreatePage_LocationPicker_SelectedIndexChangedss()
        {
            // Arrange
            var LocationPicker = (Picker)page.FindByName("LocationPicker");
            LocationPicker.SelectedIndex = 0;

            //Act
            page.LocationPicker_SelectedIndexChanged(LocationPicker, null);

            // Reset

            //Assert
            Assert.IsTrue(page.ViewModel.Data.Location == ItemLocationEnumHelper.ConvertStringToEnum(myLocationItems[LocationPicker.SelectedIndex]));
        }


        [Test]
        public void ItemCreatePage_LeftButton_Clicked_0_Should_Set_Image_At_Last_Index()
        {
            // Arrange
            page.imageIndex = 0;

            //Act
            page.LeftButton_Clicked(null, null);

            //Reset


            //Assert
            Assert.IsTrue(page.ViewModel.Data.ImageURI == page.imageList[page.imageList.Count - 1]);
        }


        [Test]
        public void ItemCreatePage_LeftButton_Clicked_1_Should_Set_Image_At_Index_0()
        {
            // Arrange
            page.imageIndex = 1;

            //Act
            page.LeftButton_Clicked(null, null);

            //Reset

            // Assert
            Assert.IsTrue(page.ViewModel.Data.ImageURI == page.imageList[0]);
        }


        [Test]
        public void ItemCreatePage_RightButton_Clicked_0_Should_Set_Image_At_Index_1()
        {
            // Arrange
            page.imageIndex = 0;

            // Act
            page.RightButton_Clicked(null, null);

            // Reset

            //Assert
            Assert.IsTrue(page.ViewModel.Data.ImageURI == page.imageList[1]);
        }


        [Test]
        public void ItemCreatePage_RightButton_Clicked_Last_Index_Should_Set_Image_at_index_0()
        {
            // Arrange
            page.imageIndex = page.imageList.Count-1;

            //Act
            page.RightButton_Clicked(null, null);

            //Reset

            //Assert
            Assert.IsTrue(page.ViewModel.Data.ImageURI == page.imageList[0]);
        }

    }
}