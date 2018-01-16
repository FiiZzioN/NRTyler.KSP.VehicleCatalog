// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
// 
// Author           : Nicholas Tyler
// Created          : 01-08-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-08-2018
// 
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.ModelTests;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using System.IO;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests.RepositoryTests
{
    [TestClass]
    public class VehicleFamilyRepoTests  // : CatalogInitializer
    {
        //[TestInitialize]
        //public override void Initialize()
        //{
        //    SetupMethods();

        //    Settings = new ApplicationSettings();
        //    Path     = Settings.VehicleFamilyLocation;
        //    Repo     = new VehicleFamilyRepo(Path, new ErrorReport(false));
        //}

        //private string Path { get; set; }
        //private ApplicationSettings Settings { get; set; }
        //private VehicleFamilyRepo Repo { get; set; }

        //[TestMethod]
        //public void VehicleFamilyRepoCreationTest()
        //{
        //    var familyName    = Family.Name;
        //    var directoryPath = $"{Path}/{familyName}";
        //    var xmlPath       = $"{Path}/{familyName}/{familyName} Family.xml";

        //    Directory.Delete(directoryPath, true);

        //    Repo.Create(Family);

        //    Assert.IsTrue(Directory.Exists(directoryPath));
        //    Assert.IsTrue(File.Exists(xmlPath));
        //}
    }
}