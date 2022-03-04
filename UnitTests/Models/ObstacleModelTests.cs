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

      
    }
}