// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
// 
// Author           : Nicholas Tyler
// Created          : 01-20-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-20-2018
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataControllerTests
{
    public class PriceSummaryControllerTests : CatalogInitializer
    {
        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer();

            //Family.Launchers.AddRange(ExtraLauncherCollection().Launchers);

            Controller = new PriceSummaryController();
        }

        public PriceSummaryController Controller { get; set; }
    }
}