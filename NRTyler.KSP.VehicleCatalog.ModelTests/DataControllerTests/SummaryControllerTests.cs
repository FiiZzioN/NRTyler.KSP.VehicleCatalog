// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 12-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-20-2018
//
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataControllerTests
{
    [TestClass]
    public class SummaryControllerTests : CatalogInitializer
    {
        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer(ExtraLaunchers.AddIndividually);

            Controller = new SummaryController();
        }

        public SummaryController Controller { get; set; }

        [TestMethod]
        public void SummaryController_GetNumberOfVehiclesTest()
        {
            var numberOfVehicles = Controller.GetNumberOfVehicles(Family);

            // Since the CatalogInitializer makes 3 launchers by default and 3 more additional launchers are added to 
            // the family's launcher list so I had more variables for more thorough summary tests. I expect a total of 6.
            Assert.AreEqual(6, numberOfVehicles);

            // ---------------------------

            Family.Launchers.Clear();
            numberOfVehicles = Controller.GetNumberOfVehicles(Family);

            // Since the CatalogInitializer makes 3 launchers by default and 
            // I removed the 3 additional launchers, I expect a total of 3.
            Assert.AreEqual(3, numberOfVehicles);
        }
    }
}