using EnergonSoftware.BackpackPlanner.Models.Gear;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackpackPlanner.UnitTests.Models.Gear
{
    [TestClass]
    public class GearItemTests
    {
        [TestMethod]
        public void GearItem_Id_Default()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act

            // Assert
            Assert.IsTrue(gearItem.Id < 1);
        }

        [TestMethod]
        public void GearItem_Carried_Default()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act

            // Assert
            Assert.AreEqual(GearCarried.Carried, gearItem.Carried);
        }

        [TestMethod]
        public void GearItem_IsConsumable_Default()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act

            // Assert
            Assert.IsFalse(gearItem.IsConsumable);
        }

#region ConsumedPerDay
        [TestMethod]
        public void GearItem_ConsumedPerDay_Default()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act

            // Assert
            Assert.AreEqual(1, gearItem.ConsumedPerDay);
        }

        [TestMethod]
        public void GearItem_ConsumedPerDay_Zero()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act
            gearItem.ConsumedPerDay = 0;

            // Assert
            Assert.AreEqual(1, gearItem.ConsumedPerDay);
        }

        [TestMethod]
        public void GearItem_ConsumedPerDay_Negative()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act
            gearItem.ConsumedPerDay = -1;

            // Assert
            Assert.AreEqual(1, gearItem.ConsumedPerDay);
        }
#endregion

#region CostInUSDP
        [TestMethod]
        public void GearItem_CostInUSDP_Default()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act

            // Assert
            Assert.AreEqual(0, gearItem.CostInUSDP);
        }

        [TestMethod]
        public void GearItem_CostInUSDP_Negative()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act
            gearItem.CostInUSDP = -1;

            // Assert
            Assert.AreEqual(0, gearItem.CostInUSDP);
        }
#endregion

#region WeightInGrams
        [TestMethod]
        public void GearItem_WeightInGrams_Default()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act

            // Assert
            Assert.AreEqual(0, gearItem.WeightInGrams);
        }

        [TestMethod]
        public void GearItem_WeightInGrams_Negative()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act
            gearItem.WeightInGrams = -1;

            // Assert
            Assert.AreEqual(0, gearItem.WeightInGrams);
        }
#endregion
    }
}
