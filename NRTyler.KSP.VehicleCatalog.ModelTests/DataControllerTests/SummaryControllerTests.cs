// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 12-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-28-2017
//
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataControllerTests
{
    [TestClass]
    public class SummaryControllerTests : CatalogInitializer
    {
        /// <summary>
        /// Adds this collection of launchers to a family. Only prices and fairings have 
        /// been added to these launchers, so they are of no use for any other tests.
        /// </summary>
        public LauncherCollection AddAnotherCollection()
        {
            var collection = new LauncherCollection();

            // Create some launchers with a range of price for more thorough tests.
            var launcher1 = new Launcher()
            {
                Price = 20500
            };
            var launcher2 = new Launcher()
            {
                Price = 200000
            };
            var launcher3 = new Launcher()
            {
                Price = 5000
            };
            var launcher4 = new Launcher()
            {
                Price = 175000,
                Fairings = new List<Fairing>()
                {
                    new Fairing("Normal", 11, 3.75)
                }
                
            };
            var launcher5 = new Launcher()
            {
                Price = -12000,
                Fairings = new List<Fairing>()
                {
                    new Fairing("Normal", -15, -2)
                }
            };

            collection.Add(launcher1);
            collection.Add(launcher2);
            collection.Add(launcher3);
            collection.Add(launcher4);
            collection.Add(launcher5);

            return collection;
        }


        [TestMethod]
        public void SummaryController_Family_GetNumberOfVersions()
        {
            // Adds another collection of launchers to the family to see if the method searches throughout the entire family 
            Family.Add(AddAnotherCollection());

            // There are 8 vehicles in total when you add the number of vehicles in both collections together.
            Assert.AreEqual(8, Family.GetNumberOfVersions());
        }

        [TestMethod]
        public void SummaryController_Collection_GetNumberOfVersions()
        {
            // There are three vehicles in the inherited collection.
            Assert.AreEqual(3, Collection.GetNumberOfVersions());
        }

        [TestMethod]
        public void SummaryController_Family_GetPriceSummary()
        {
            const int lowest  = 0;
            const int highest = 235000;

            // Adds another collection of launchers to the family to see if the method searches throughout the entire family 
            Family.Add(AddAnotherCollection());

            // These are the cheapest and most expensive values when both collections are analyzed.
            Assert.AreEqual(lowest , Family.GetPriceSummary().Cheapest);
            Assert.AreEqual(highest, Family.GetPriceSummary().MostExpensive);
        }

        [TestMethod]
        public void SummaryController_Collection_GetPriceSummary()
        {
            const int lowest  = 221000;
            const int highest = 235000;

            // These are the cheapest and most expensive values in the inherited collection.
            Assert.AreEqual(lowest , Collection.GetPriceSummary().Cheapest);
            Assert.AreEqual(highest, Collection.GetPriceSummary().MostExpensive);
        }

        [TestMethod]
        public void SummaryController_Family_GetFairingSummary()
        {
            double? length   = 18;
            double? diameter = 3.75;

            // Adds another collection of launchers to the family to see if the method searches throughout the entire family 
            Family.Add(AddAnotherCollection());

            // These are the longest and widest fairing values when both collections are analyzed.
            Assert.AreEqual(length  , Family.GetFairingSummary().MaxLength);
            Assert.AreEqual(diameter, Family.GetFairingSummary().MaxDiameter);
        }

        [TestMethod]
        public void SummaryController_Collection_GetFairingSummary()
        {
            double? length   = 18;
            double? diameter = 3.2;

            // These are the longest and widest fairing values in the inherited collection.
            Assert.AreEqual(length  , Collection.GetFairingSummary().MaxLength);
            Assert.AreEqual(diameter, Collection.GetFairingSummary().MaxDiameter);
        }
    }
}