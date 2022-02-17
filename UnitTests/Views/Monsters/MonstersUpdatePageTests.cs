using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;
using Game.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterUpdatePageTests : MonsterUpdatePage
    {
        App app;
        MonsterUpdatePage page;

        public MonsterUpdatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MonsterUpdatePage(new GenericViewModel<MonsterModel>(new MonsterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void MonsterUpdatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterUpdatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void MonsterUpdatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Item_ShowPopup_Default_Should_Pass()
        {
            // Arrange

            var item = page.GetItemToDisplay();

            // Act
            var itemButton = item.Children.FirstOrDefault(m => m.GetType().Name.Equals("Button"));

            _ = page.ShowPopup();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_ClosePopup_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePopup();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_ClosePopup_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Defense_OnSliderDefenseChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            var ViewModel = new GenericViewModel<MonsterModel>(data);

            page = new MonsterUpdatePage(ViewModel);
            var oldDefense = 0.0;
            var newDefense = 1.0;

            var args = new ValueChangedEventArgs(oldDefense, newDefense);

            // Act
            page.Defense_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_Speed_OnSliderDefenseChanged_Default_Should_Pass()
        {
            // Arrange
            var data = new MonsterModel();
            var ViewModel = new GenericViewModel<MonsterModel>(data);

            page = new MonsterUpdatePage(ViewModel);
            var oldSpeed = 0.0;
            var newSpeed = 1.0;

            var args = new ValueChangedEventArgs(oldSpeed, newSpeed);

            // Act
            page.Speed_OnSliderValueChanged(null, args);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatedPage_RollDice_Clicked_Default_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data = new MonsterModel();

            // Act
            page.RollDice_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        /// <summary>
        /// Test the right button click successfully changes the monster image
        /// </summary>
        [Test]
        public void MonsterCreatePage_LeftButton_Clicked_Valid_Should_Pass()
        {
            // Arrange
            List<String> imageList = GameImagesHelper.GetMonsterImage();
            var expected = imageList[imageList.Count - 1];

            // Act
            page.LeftButton_Clicked(null, null);
            // Reset

            // Assert
            Assert.AreEqual(expected, page.ViewModel.Data.ImageURI);
        }

        /// <summary>
        /// Test the right button click successfully changes the monster image
        /// </summary>
        [Test]
        public void MonsterCreatePage_LeftButton_Clicked_At_First_Photo_Should_Pass()
        {
            // Arrange
            List<String> imageList = GameImagesHelper.GetMonsterImage();
            var expected = imageList[imageList.Count - 1];
            for (int i = 0; i < imageList.Count; i++)
            {
                page.LeftButton_Clicked(null, null);
            }

            // Act
            page.LeftButton_Clicked(null, null);
            // Reset

            // Assert
            Assert.AreEqual(expected, page.ViewModel.Data.ImageURI);
        }

        [Test]
        public void MonsterCreatePage_OnPopupItemSelected_Clicked_Null_Should_Fail()
        {
            // Arrange

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(null, 0);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterUpdatePage_OnPopupItemSelected_Clicked_Valid_Should_Pass()
        {
            // Arrange
            ItemModel data = new ItemModel();
            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        /// <summary>
        /// Test the right button click successfully changes the monster image
        /// </summary>
        [Test]
        public void MonsterUpdatePage_RightButton_Clicked_Valid_Should_Pass()
        {
            // Arrange
            List<String> imageList = GameImagesHelper.GetMonsterImage();
            var expected = imageList[1];

            // Act
            page.RightButton_Clicked(null, null);
            // Reset

            // Assert
            Assert.AreEqual(expected, page.ViewModel.Data.ImageURI);
        }

        /// <summary>
        /// Test the right button click successfully changes the monster image
        /// </summary>
        [Test]
        public void MonsterUpdatePage_RightButton_Clicked_At_Last_Photo_Should_Pass()
        {
            // Arrange
            List<String> imageList = GameImagesHelper.GetMonsterImage();
            var expected = imageList[1];
            for (int i = 0; i < imageList.Count; i++)
            {
                page.RightButton_Clicked(null, null);
            }

            // Act
            page.RightButton_Clicked(null, null);
            // Reset

            // Assert
            Assert.AreEqual(expected, page.ViewModel.Data.ImageURI);
        }
    }
}