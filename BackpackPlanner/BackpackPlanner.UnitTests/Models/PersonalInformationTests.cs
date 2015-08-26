using EnergonSoftware.BackpackPlanner.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BackpackPlanner.UnitTests.Models
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
            Assert.AreEqual(Sex.Undeclared, personalInformation.Sex);
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
