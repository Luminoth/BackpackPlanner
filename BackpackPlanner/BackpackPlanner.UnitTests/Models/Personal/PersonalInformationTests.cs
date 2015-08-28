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

using EnergonSoftware.BackpackPlanner.Models.Personal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackpackPlanner.UnitTests.Models.Personal
{
    [TestClass]
    public class PersonalInformationTests
    {
        [TestMethod]
        public void PersonalInformation_Sex_Default()
        {
            // Arrange
            PersonalInformation personalInformation = new PersonalInformation();

            // Act

            // Assert
            Assert.AreEqual(UserSex.Undeclared, personalInformation.Sex);
        }

#region HeightInCm
        [TestMethod]
        public void PersonalInformation_HeightInCm_Default()
        {
            // Arrange
            PersonalInformation personalInformation = new PersonalInformation();

            // Act

            // Assert
            Assert.AreEqual(0, personalInformation.HeightInCm);
        }

        [TestMethod]
        public void PersonalInformation_HeightInCm_Negative()
        {
            // Arrange
            PersonalInformation personalInformation = new PersonalInformation();

            // Act
            personalInformation.HeightInCm = -1;

            // Assert
            Assert.AreEqual(0, personalInformation.HeightInCm);
        }
#endregion

#region WeightInGrams
        [TestMethod]
        public void PersonalInformation_WeightInGrams_Default()
        {
            // Arrange
            PersonalInformation personalInformation = new PersonalInformation();

            // Act

            // Assert
            Assert.AreEqual(0, personalInformation.WeightInGrams);
        }

        [TestMethod]
        public void PersonalInformation_WeightInGrams_Negative()
        {
            // Arrange
            PersonalInformation personalInformation = new PersonalInformation();

            // Act
            personalInformation.WeightInGrams = -1;

            // Assert
            Assert.AreEqual(0, personalInformation.WeightInGrams);
        }
#endregion
    }
}
