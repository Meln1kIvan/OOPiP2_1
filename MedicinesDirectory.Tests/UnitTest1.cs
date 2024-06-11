using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using MyLib;

namespace MedicinesDirectory.Tests
{
    [TestClass]
    public class MainWindowTests
    {

        [TestMethod]
        public void Medicine_SetProperties_GetPropertiesReturnCorrectValues()
        {
            // Arrange
            var medicine = new Medicine();

            // Act
            medicine.Name = "Test Medicine";
            medicine.Form = "Test Form";
            medicine.Quantity = 10;
            medicine.Unit = "Test Unit";
            medicine.Dosage = "Test Dosage";
            medicine.Price = 9.99m;
            medicine.Manufacturer = "Test Manufacturer";

            // Assert
            Assert.AreEqual("Test Medicine", medicine.Name);
            Assert.AreEqual("Test Form", medicine.Form);
            Assert.AreEqual(10, medicine.Quantity);
            Assert.AreEqual("Test Unit", medicine.Unit);
            Assert.AreEqual("Test Dosage", medicine.Dosage);
            Assert.AreEqual(9.99m, medicine.Price);
            Assert.AreEqual("Test Manufacturer", medicine.Manufacturer);
        }

        [TestMethod]
        public void Medicine_DefaultConstructor_DefaultPropertyValues()
        {
            // Arrange
            var medicine = new Medicine();

            // Assert
            Assert.IsNull(medicine.Name);
            Assert.IsNull(medicine.Form);
            Assert.AreEqual(0, medicine.Quantity);
            Assert.IsNull(medicine.Unit);
            Assert.IsNull(medicine.Dosage);
            Assert.AreEqual(0m, medicine.Price);
            Assert.IsNull(medicine.Manufacturer);
        }
    }
}