/*
   Copyright 2015 Shane Lillie

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System.Threading.Tasks;

using EnergonSoftware.BackpackPlanner.Models.Gear.Items;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SQLite.Net.Async;

using TypeMock.ArrangeActAssert;

namespace EnergonSoftware.BackpackPlanner.UnitTests.Models.Gear.Items
{
    [TestClass, Isolated]
    public class GearItemTests
    {
        [TestMethod]
        public void GearItem_Id_Default()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act

            // Assert
            Assert.IsTrue(gearItem.GearItemId < 1);
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
        public void GearItem_ConsumedPerDay_Negative()
        {
            // Arrange
            GearItem gearItem = new GearItem();

            // Act
            gearItem.ConsumedPerDay = -1;

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

#region Equals
        [TestMethod]
        public void GearItem_Equals_NegativeId_NotEqual()
        {
            // Arrange
            GearItem gearItem1 = new GearItem
            {
                GearItemId = -1
            };

            GearItem gearItem2 = new GearItem
            {
                GearItemId = -1
            };

            // Act
            bool areEqual = gearItem1.Equals(gearItem2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearItem_Equals_ZeroId_NotEqual()
        {
            // Arrange
            GearItem gearItem1 = new GearItem
            {
                GearItemId = 0
            };

            GearItem gearItem2 = new GearItem
            {
                GearItemId = 0
            };

            // Act
            bool areEqual = gearItem1.Equals(gearItem2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearItem_Equals_NotEqual()
        {
            // Arrange
            GearItem gearItem1 = new GearItem
            {
                GearItemId = 1
            };

            GearItem gearItem2 = new GearItem
            {
                GearItemId = 2
            };

            // Act
            bool areEqual = gearItem1.Equals(gearItem2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearItem_Equals_Equal()
        {
            // Arrange
            GearItem gearItem1 = new GearItem
            {
                GearItemId = 1
            };

            GearItem gearItem2 = new GearItem
            {
                GearItemId = 1
            };

            // Act
            bool areEqual = gearItem1.Equals(gearItem2);

            // Assert
            Assert.IsTrue(areEqual);
        }
#endregion
    }
}
