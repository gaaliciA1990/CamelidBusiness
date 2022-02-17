using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

namespace UnitTests.Views
{
    [TestFixture]
    public class ItemUpdatePageTests : ItemUpdatePage
    {
        App app;
        ItemUpdatePage page;

        public ItemUpdatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ItemUpdatePage(new GenericViewModel<ItemModel>(new ItemModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ItemUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ItemUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Clicked_Invalid_Invalid_Description_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = "";
            page.ViewModel.Data.Description = "";

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Save_Click_Valid_Location_And_Attribute_Should_Show_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = "test";
            page.ViewModel.Data.Description = "test";
            page.errors.Add("ERROR","ERROR");
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            //Reset

            //Assert
            Assert.IsTrue(true);
        }

        [Test]
        public void ItemUpdatePage_Save_Click_Invalid_Location_And_Attribute_Should_Show_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = "test";
            page.ViewModel.Data.Description = "test";
            page.ViewModel.Data.Location = ItemLocationEnum.Unknown;
            page.ViewModel.Data.Attribute = AttributeEnum.Unknown;
            page.ViewModel.Data.ImageURI = null;

            // Act
            page.Save_Clicked(null, null);

            //Reset

            //Assert
            Assert.IsTrue(page.errors.ContainsKey("Location") == false);
            Assert.IsTrue(page.errors.ContainsKey("Attribute") == false);
        }




        [Test]
        public void ItemUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ItemUpdatePage_Value_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var ViewModel = new GenericViewModel<ItemModel>(data);

            page = new ItemUpdatePage(ViewModel);
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
        public void ItemUpdatePage_Range_OnStepperValueChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var ViewModel = new GenericViewModel<ItemModel>(data);

            page = new ItemUpdatePage(ViewModel);
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
        public void ItemUpdatePage_Damage_OnStepperDamageChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new ItemModel();
            var ViewModel = new GenericViewModel<ItemModel>(data);

            page = new ItemUpdatePage(ViewModel);
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
        public void ItemUpdatePage_LeftButton_Clicked_0_Should_Set_Image_At_Last_Index()
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
        public void ItemUpdatePage_LeftButton_Clicked_1_Should_Set_Image_At_Index_0()
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
        public void ItemUpdatePage_RightButton_Clicked_0_Should_Set_Image_At_Index_1()
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
        public void ItemUpdatePage_RightButton_Clicked_Last_Index_Should_Set_Image_at_index_0()
        {
            // Arrange
            page.imageIndex = page.imageList.Count - 1;

            //Act
            page.RightButton_Clicked(null, null);

            //Reset

            //Assert
            Assert.IsTrue(page.ViewModel.Data.ImageURI == page.imageList[0]);
        }

        [Test]
        public void Update_AttributePicker_SelectedIndexChanged_Invalid_Location_And_Attribute_Should_Pass()
        {
            // Arrange
            page.errors.Add("LocationAttribute", "ERROR");
            page.ViewModel.Data.Attribute = AttributeEnum.Unknown;

            // Act
            page.AttributePicker_SelectedIndexChanged(null, null);

            // Reset

            // Assert
            Assert.IsTrue(page.errors.ContainsKey("LocationAttribute") == true);
        }


        [Test]
        public void ItemUpdatePage_LocationPicker_SelectedIndexChanged()
        {
            // Arrange
            var LocationPicker = (Picker)page.FindByName("LocationPicker");
            LocationPicker.SelectedIndex = 1;

            // Act
            page.LocationPicker_SelectedIndexChanged(LocationPicker, null);

            // Reset

            //Assert
            Assert.IsTrue(page.ViewModel.Data.Location == ItemLocationEnum.Head);
        }

        


    }
}