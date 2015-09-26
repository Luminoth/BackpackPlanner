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

using EnergonSoftware.BackpackPlanner.Models.Gear.Systems;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SQLite.Net.Async;

using TypeMock.ArrangeActAssert;

namespace EnergonSoftware.BackpackPlanner.UnitTests.Models.Gear.Systems
{
    [TestClass, Isolated]
    public class GearSystemTests
    {
        [TestMethod]
        public void GearSystem_Id_Default()
        {
            // Arrange
            GearSystem gearSystem = new GearSystem();

            // Act

            // Assert
            Assert.IsTrue(gearSystem.GearSystemId < 1);
        }

#region Equals
        [TestMethod]
        public void GearSystem_Equals_NegativeId_NotEqual()
        {
            // Arrange
            GearSystem gearSystem1 = new GearSystem
            {
                GearSystemId = -1
            };

            GearSystem gearSystem2 = new GearSystem
            {
                GearSystemId = -1
            };

            // Act
            bool areEqual = gearSystem1.Equals(gearSystem2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearSystem_Equals_ZeroId_NotEqual()
        {
            // Arrange
            GearSystem gearSystem1 = new GearSystem
            {
                GearSystemId = 0
            };

            GearSystem gearSystem2 = new GearSystem
            {
                GearSystemId = 0
            };

            // Act
            bool areEqual = gearSystem1.Equals(gearSystem2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearSystem_Equals_NotEqual()
        {
            // Arrange
            GearSystem gearSystem1 = new GearSystem
            {
                GearSystemId = 1
            };

            GearSystem gearSystem2 = new GearSystem
            {
                GearSystemId = 2
            };

            // Act
            bool areEqual = gearSystem1.Equals(gearSystem2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void GearSystem_Equals_Equal()
        {
            // Arrange
            GearSystem gearSystem1 = new GearSystem
            {
                GearSystemId = 1
            };

            GearSystem gearSystem2 = new GearSystem()
            {
                GearSystemId = 1
            };

            // Act
            bool areEqual = gearSystem1.Equals(gearSystem2);

            // Assert
            Assert.IsTrue(areEqual);
        }
#endregion
    }
}
