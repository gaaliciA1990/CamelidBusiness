using NUnit.Framework;

using Game;
using Game.Views;
using Game.ViewModels;
using Game.Models;
using Game.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using System;
using System.Collections.Generic;

namespace UnitTests.Views
{
    [TestFixture]
    public class MonsterCreatePageTests : MonsterCreatePage
    {
        App app;
        MonsterCreatePage page;

        public MonsterCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new MonsterCreatePage(new GenericViewModel<MonsterModel>(new MonsterModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void MonsterCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void MonsterCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void MonsterCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Save_Difficulty_Default_Should_Pass()
        {
            // Arrange
            var dummy = new Button();
            page.CurrentDifficulty = new Button();

            // Act
            page.SaveDifficulty(dummy, DifficultyEnum.Impossible);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_Save_Difficulty_CurrentDifficulty_Null_Should_Pass()
        {
            // Arrange
            page.CurrentDifficulty = null;
            var dummy = new Button();

            // Act
            page.SaveDifficulty(dummy, DifficultyEnum.Impossible);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_ShowPopup_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page.ShowPopup();

            // Reset

            // Assert
            Assert.AreEqual(result, true); // Got to here, so it happened...
        }

        [Test]
        public void MonsterCreatePage_OnPopupItemSelected_Clicked_Default_Should_Pass()
        {
            // Arrange

            var data = new ItemModel();

            var selectedCharacterChangedEventArgs = new SelectedItemChangedEventArgs(data, 0);

            // Act
            page.OnPopupItemSelected(null, selectedCharacterChangedEventArgs);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
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

        /// <summary>
        /// Test the right button click successfully changes the monster image
        /// </summary>
        [Test]
        public void MonsterCreatePage_RightButton_Clicked_Valid_Should_Pass()
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
        public void MonsterCreatePage_RightButton_Clicked_At_Last_Photo_Should_Pass()
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

        [Test]
        public void MonsterCreatePage_ClosePopup_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.ClosePopup_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }
    }
}