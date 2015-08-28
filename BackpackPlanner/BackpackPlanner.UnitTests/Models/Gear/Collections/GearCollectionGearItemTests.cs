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

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackpackPlanner.UnitTests.Models.Gear.Collections
{
    [TestClass]
    public class GearCollectionGearItemTests
    {
        [TestMethod]
        public void GearCollectionGearItem_GearCollectionId_Default()
        {
            // Arrange
            GearCollectionGearItem gearCollectionGearItem = new GearCollectionGearItem();

            // Act

            // Assert
            Assert.IsTrue(gearCollectionGearItem.GearCollectionId < 1);
        }

        [TestMethod]
        public void GearCollectionGearItem_GearItemId_Default()
        {
            // Arrange
            GearCollectionGearItem gearCollectionGearItem = new GearCollectionGearItem();

            // Act

            // Assert
            Assert.IsTrue(gearCollectionGearItem.GearItemId < 1);
        }

#region Amount
        [TestMethod]
        public void GearCollectionGearItem_Amount_Default()
        {
            // Arrange
            GearCollectionGearItem gearCollectionGearItem = new GearCollectionGearItem();

            // Act

            // Assert
            Assert.AreEqual(1, gearCollectionGearItem.Amount);
        }

        [TestMethod]
        public void GearCollectionGearItem_Amount_Negative()
        {
            // Arrange
            GearCollectionGearItem gearCollectionGearItem = new GearCollectionGearItem();

            // Act
            gearCollectionGearItem.Amount = -1;

            // Assert
            Assert.AreEqual(1, gearCollectionGearItem.Amount);
        }

        [TestMethod]
        public void GearCollectionGearItem_Amount_Zero()
        {
            // Arrange
            GearCollectionGearItem gearCollectionGearItem = new GearCollectionGearItem();

            // Act
            gearCollectionGearItem.Amount = 0;

            // Assert
            Assert.AreEqual(1, gearCollectionGearItem.Amount);
        }
#endregion
    }
}
