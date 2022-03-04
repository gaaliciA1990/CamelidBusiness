using NUnit.Framework;

using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    [TestFixture]
    public class ObstacleModelTests
    {
        [TearDown]
        public void TearDown()
        {
            ItemIndexViewModel.Instance.Dataset.Clear();
        }

        [Test]
        public void ObstacleModel_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ObstacleModel();

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }

        [Test]
        public void ObstacleModel_Constructor_Default_With_PbstacleModel_Parameter_Should_Pass()
        {
            // Arrange

            // Act
            var result = new ObstacleModel(new ObstacleModel());

            // Reset

            // Assert 
            Assert.IsNotNull(result);
        }


        [Test]
        public void ObstacleModel_Update_Default_Should_Pass()
        {
            // Arrange
            var dataOriginal = new ObstacleModel();
            dataOriginal.CurrentHealth = 10;
            dataOriginal.MaxHealth = 10;

            var dataNew = new ObstacleModel();
            dataNew.CurrentHealth = 20;
            dataNew.MaxHealth = 20;

            // Act
            var result = dataOriginal.Update(dataNew);

            // Reset

            // Assert 
            Assert.AreEqual(20, dataOriginal.CurrentHealth);
            Assert.AreEqual(20, dataOriginal.MaxHealth);
        }


        [Test]
        public void ObstacleModel_Update_Null_Should_Fail()
        {
            // Arrange
            var dataOriginal = new ObstacleModel();

            // Act
            var result = dataOriginal.Update(null);

            // Reset

            // Assert 
            Assert.AreEqual(false, result);
        }

    }
}