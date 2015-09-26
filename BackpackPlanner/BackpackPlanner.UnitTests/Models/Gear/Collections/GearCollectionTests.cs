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

using EnergonSoftware.BackpackPlanner.Models.Gear.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SQLite.Net.Async;

using TypeMock.ArrangeActAssert;

namespace EnergonSoftware.BackpackPlanner.UnitTests.Models.Gear.Collections
{
    [TestClass, Isolated]
    public class GearCollectionTests
    {
        [TestMethod]
        public void GearCollection_Id_Default()
        {
            // Arrange
            GearCollection gearCollection = new GearCollection();

            // Act

            // Assert
            Assert.IsTrue(gearCollection.GearCollectionId < 1);
        }

#region Equals
        [TestMethod]
        public void GearCollection_Equals_NegativeId_NotEqual()
        {
            // Arrange
            GearCollection gearCollection1 = new GearCollection
            {
                GearCollectionId = -1
            };

            GearCollection gearCollection2 = new GearCollection
            {
                GearCollectionId = -1
            };

            // Act
            bool areEqual = gearCollection1.Equals(gearCollection2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearCollection_Equals_ZeroId_NotEqual()
        {
            // Arrange
            GearCollection gearCollection1 = new GearCollection
            {
                GearCollectionId = 0
            };

            GearCollection gearCollection2 = new GearCollection
            {
                GearCollectionId = 0
            };

            // Act
            bool areEqual = gearCollection1.Equals(gearCollection2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearCollection_Equals_NotEqual()
        {
            // Arrange
            GearCollection gearCollection1 = new GearCollection
            {
                GearCollectionId = 1
            };

            GearCollection gearCollection2 = new GearCollection
            {
                GearCollectionId = 2
            };

            // Act
            bool areEqual = gearCollection1.Equals(gearCollection2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearCollection_Equals_Equal()
        {
            // Arrange
            GearCollection gearCollection1 = new GearCollection
            {
                GearCollectionId = 1
            };

            GearCollection gearCollection2 = new GearCollection()
            {
                GearCollectionId = 1
            };

            // Act
            bool areEqual = gearCollection1.Equals(gearCollection2);

            // Assert
            Assert.IsTrue(areEqual);
        }
#endregion
    }
}
