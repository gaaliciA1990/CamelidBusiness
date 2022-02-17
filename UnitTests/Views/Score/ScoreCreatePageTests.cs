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
    public class ScoreCreatePageTests : ScoreCreatePage
    {
        App app;
        ScoreCreatePage page;

        public ScoreCreatePageTests() : base(true) { }

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new ScoreCreatePage(new GenericViewModel<ScoreModel>(new ScoreModel()));
        }

        [TearDown]
        public void TearDown()
        {
            Application.Current = null;
        }

        [Test]
        public void ScoreCreatePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ScoreCreatePage_Cancel_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Cancel_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreCreatePage_Save_Clicked_Default_Should_Pass()
        {
            // Arrange

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreCreatePage_Save_Clicked_Valid_Name_Default_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = "Game1";

            // Act
            page.Save_Clicked(null, null);

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }

        [Test]
        public void ScoreCreatePage_Save_Clicked_Null_Image_Should_Pass()
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
        public void ScoreCreatePage_OnBackButtonPressed_Valid_Should_Pass()
        {
            // Arrange

            // Act
            _ = OnBackButtonPressed();

            // Reset

            // Assert
            Assert.IsTrue(true); // Got to here, so it happened...
        }


        [Test]
        public void ScoreCreatePageEntry_Entry_Validator_Valid_Name_Should_Pass()
        {
            // Arrange
            page.ViewModel.Data.Name = "Valid";
            // Act
            var result = page.Entry_Validator();

            // Reset

            // Assert
            Assert.IsTrue(result); 
        }

        [Test]
        public void ScoreCreatePageEntry_Entry_Validator_Invalid_Name_Should_Fail()
        {
            // Arrange
            page.ViewModel.Data.Name = null;

            // Act
            var result = page.Entry_Validator();

            // Reset

            // Assert
            Assert.IsFalse(result); 
        }

        [Test]
        public void ScoreCreatePageEntry_Entry_TextChanged_Invalid_Name_Should_Fail()
        {
            // Arrange

            // Act
            page.Entry_TextChanged(null,null);

            // Reset

            // Assert
            Assert.IsTrue(true);
        }

        
    }
}