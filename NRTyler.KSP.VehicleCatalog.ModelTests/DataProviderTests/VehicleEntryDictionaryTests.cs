// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
// 
// Author           : Nicholas Tyler
// Created          : 10-05-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-05-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleTypes;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests
{
    [TestClass]
    public class VehicleEntryDictionaryTests
    {

        private VehicleEntryDictionary vehicleEntryDictionary = new VehicleEntryDictionary("Delta");

        // The test vehicle we'll be working with.
        private static Vehicle testVehicle = new Vehicle
        {
            Name = "Delta II",
            VehicleType = VehicleType.LaunchVehicle,
            Price = 120500
        };

        private VehicleEntry testEntry = new VehicleEntry(testVehicle);


        [TestMethod]
        public void TestNameLogic()
        {
            // Test default constructor.
            var dictionaryOne = new VehicleEntryDictionary();

            Assert.AreEqual("Family Name Not Set", dictionaryOne.FamilyName);

            // ----------------------------------------------------------

            // Test valid second constructor.
            var dictionaryTwo = new VehicleEntryDictionary("Correct Name");

            Assert.AreEqual("Correct Name", dictionaryTwo.FamilyName);

            // ----------------------------------------------------------

            // Test invalid second constructor.
            var dictionaryThree = new VehicleEntryDictionary(String.Empty);

            Assert.AreEqual("Invalid Family Name", dictionaryThree.FamilyName);

            // ----------------------------------------------------------

            // Test manual valid name set. 
            var dictionaryFour = new VehicleEntryDictionary {FamilyName = "Delta IV"};

            Assert.AreEqual("Delta IV", dictionaryFour.FamilyName);

            // ----------------------------------------------------------

            // Test manual invalid name set.
            var dictionaryFive = new VehicleEntryDictionary { FamilyName = null };

            Assert.AreEqual("Invalid Family Name", dictionaryFive.FamilyName);
        }

        [TestMethod]
        public void AddEntry()
        {
            // Add the entry.
            vehicleEntryDictionary.AddEntry(this.testEntry);

            // Make the variables that'll hold the expected values we want.
            var expectedKey   = "Delta II";
            var expectedValue = this.testEntry;

            // Make the variables that'll hold the actual values.
            var actualKey   = vehicleEntryDictionary.Keys.ElementAt(0);
            var actualValue = vehicleEntryDictionary.Values.ElementAt(0);

            // Compare these things!
            Assert.AreEqual(expectedKey, actualKey);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void BlockItemWithSameKey()
        {
            // Ensure the test vehicle will be in the dictionary.
            vehicleEntryDictionary.AddEntry(this.testEntry);

            // The test vehicle we'll be working with.
            var vehicle = new Vehicle
            {
                Name = "Delta II",
                VehicleType = VehicleType.Probe,
                Price = 30500
            };

            // Make the entry and add the entry.
            var vehicleEntry = new VehicleEntry(vehicle);
            vehicleEntryDictionary.AddEntry(vehicleEntry);

            // The dictionary should only have one entry since this entry has the same name.
            Assert.AreEqual(1, this.vehicleEntryDictionary.Count);
        }

        [TestMethod]
        public void AddCompletelyNewItem()
        {
            // Ensure the test vehicle will be in the dictionary.
            vehicleEntryDictionary.AddEntry(this.testEntry);

            // The test vehicle we'll be working with.
            var vehicle = new Vehicle
            {
                Name = "Delta II 7325",
                VehicleType = VehicleType.LaunchVehicle,
                Price = 112540
            };

            // Make the entry and add the entry.
            var vehicleEntry = new VehicleEntry(vehicle);
            vehicleEntryDictionary.AddEntry(vehicleEntry);

            // Make the variables that'll hold the expected values we want.
            var expectedKey = "Delta II 7325";
            var expectedValue = vehicleEntry;

            // Make the variables that'll hold the actual values.
            var actualKey = vehicleEntryDictionary.Keys.ElementAt(1);
            var actualValue = vehicleEntryDictionary.Values.ElementAt(1);

            // The dictionary should only have two entries since this entry is different than the first.
            Assert.AreEqual(2, this.vehicleEntryDictionary.Count);

            // Compare the new items values to be sure it really was added to the dictionary
            Assert.AreEqual(expectedKey, actualKey);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}