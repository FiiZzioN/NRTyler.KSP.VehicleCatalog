// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
// 
// Author           : Nicholas Tyler
// Created          : 01-28-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-28-2018
// 
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.ModelTests;
using NRTyler.KSP.VehicleCatalog.Services.Controllers;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using System;
using System.IO;
using System.Linq;
using NRTyler.KSP.VehicleCatalog.Services.Utilities.Comparers;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests.ControllerTests
{
    [TestClass]
    public class VehicleFamilyCacheControllerTests : CatalogInitializer
    {
        #region Initialization and Clean Up

        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer(ExtraLaunchers.AddNone);

            Settings = new ApplicationSettings();

            var path        = Settings.VehicleFamilyLocation;
            var errorReport = new ErrorReport(false);

            DirectoryCreator = new DirectoryCreator(Settings, errorReport);
            DirectoryCreator.CreateVehicleFamilyStorageLocation();

            var repo = new VehicleFamilyRepo(path, errorReport);
            repo.Create(Family);

            Controller         = new VehicleFamilyCacheController(Settings, errorReport);
            AdditionalFamilies = new[]
            {
                new VehicleFamily("Falcon"),
                new VehicleFamily("Delta"),
                new VehicleFamily("Atlas"),
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            DeleteVehicleFamilyDirectory();
        }

        protected void DeleteVehicleFamilyDirectory()
        {
            var path = Settings.VehicleFamilyLocation;

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ApplicationSettings"/> object.
        /// </summary>
        protected ApplicationSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="VehicleFamilyCacheController"/> object.
        /// </summary>
        protected VehicleFamilyCacheController Controller { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Services.Utilities.DirectoryCreator"/> object.
        /// </summary>
        protected DirectoryCreator DirectoryCreator { get; set; }

        /// <summary>
        /// Gets or sets an array containing additional families that can be used to test these methods. 
        /// All the additional families have initialized are their names and GlobalIdentifiers.
        /// </summary>
        protected VehicleFamily[] AdditionalFamilies { get; set; }

        /// <summary>
        /// Gets the comparer that determines whether a family is in a collection or not.
        /// </summary>
        protected VehicleFamilyEqualityComparer Comparer { get; } = new VehicleFamilyEqualityComparer();

        #endregion

        [TestMethod]
        public void VehicleFamilyCacheController_Populate()
        {
            Controller.AddRange(AdditionalFamilies);
            Controller.Populate();

            var cacheObjects = Controller.GetCachedObjects().ToArray();

            // Since Populate() clears the cache if it isn't empty, there should be only one item even though we 
            // added the additional families. The populate gathers all families in the family storage location. 
            // Since the Angara family is the only saved there, it should be the only family in the cached objects.
            Assert.IsTrue(cacheObjects.Length == 1);
            Assert.IsTrue(cacheObjects.Contains(Family, Comparer));
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Add_Object()
        {
            Controller.Clear();
            Controller.Add(Family);

            var cacheObjects = Controller.GetCachedObjects().ToArray();

            // We cleared the cache and then added the default family, so the retrieved item should be the same as the default family.
            Assert.IsTrue(cacheObjects.Contains(Family, Comparer));
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Add_Key()
        {
            Controller.Clear();
            Controller.Add(Family.Name);

            var cacheObjects = Controller.GetCachedObjects().ToArray();

            // We cleared the cache and then added the default family, so the retrieved item should be the same as the default family.
            Assert.IsTrue(cacheObjects.Contains(Family, Comparer));
        }

        [TestMethod]
        public void VehicleFamilyCacheController_AddRange()
        {
            Controller.Clear();
            Controller.AddRange(AdditionalFamilies);

            var cacheObjects = Controller.GetCachedObjects().ToArray();

            var falcon = cacheObjects.SingleOrDefault(e => e.Name == "Falcon");
            var delta  = cacheObjects.SingleOrDefault(e => e.Name == "Delta");
            var atlas  = cacheObjects.SingleOrDefault(e => e.Name == "Atlas");

            // We cleared the cache and the added the additional families, so it should only contain those three families.
            Assert.IsTrue(cacheObjects.Contains(falcon));
            Assert.IsTrue(cacheObjects.Contains(delta));
            Assert.IsTrue(cacheObjects.Contains(atlas));
            Assert.IsTrue(cacheObjects.Length == 3);
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Remove_Object()
        {
            Controller.Remove(Family);
            var cacheObjects = Controller.GetCachedObjects().ToArray();

            // We removed the family so it shouldn't be in the cached objects.
            Assert.IsFalse(cacheObjects.Contains(Family, Comparer));
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Remove_Key()
        {
            Controller.Remove($"{Family.Name}");
            var cacheObjects = Controller.GetCachedObjects().ToArray();

            // We removed the family so it shouldn't be in the cached objects.
            Assert.IsTrue(!cacheObjects.Contains(Family, Comparer));
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Refresh()
        {
            Controller.AddRange(AdditionalFamilies);
            Controller.Refresh();

            var cacheObjects = Controller.GetCachedObjects().ToArray();

            var falcon = cacheObjects.SingleOrDefault(e => e.Name == "Falcon");

            // We added the additional launchers and then refreshed the cache. Since the only family saved in 
            // the storage location is the Angara family, then it should be the only one in the cached objects.
            Assert.IsTrue(cacheObjects.Contains(Family, Comparer));
            Assert.IsTrue(!cacheObjects.Contains(falcon));
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Refresh_Object()
        {
            var oldGlobalIdentifier = Family.GlobalIdentifier;
            var newGlobalIdentifier = new Guid();
            Family.GlobalIdentifier = newGlobalIdentifier;

            Controller.Refresh(Family);

            var cacheObjects = Controller.GetCachedObjects().ToArray();
            var cachedIdentifier = cacheObjects[0].GlobalIdentifier;

            // We assigned a new GlobalIdentifier to the family, and then refreshed the object in the cache. Refreshing an 
            // abject removes it from the cache and then reloads it from storage. Since the stored family hadn't been
            // updated, the retrieved family should still have the old GlobalIdentifier.
            Assert.IsTrue(cachedIdentifier != newGlobalIdentifier);
            Assert.IsTrue(cachedIdentifier == oldGlobalIdentifier);
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Refresh_Key()
        {
            var oldGlobalIdentifier = Family.GlobalIdentifier;
            var newGlobalIdentifier = new Guid();
            Family.GlobalIdentifier = newGlobalIdentifier;

            Controller.Refresh($"{Family.Name}");

            var cacheObjects = Controller.GetCachedObjects().ToArray();
            var cachedIdentifier = cacheObjects[0].GlobalIdentifier;

            // We assigned a new GlobalIdentifier to the family, and then refreshed the object in the cache. Refreshing an 
            // abject removes it from the cache and then reloads it from storage. Since the stored family hadn't been
            // updated, the retrieved family should still have the old GlobalIdentifier.
            Assert.IsTrue(cachedIdentifier != newGlobalIdentifier);
            Assert.IsTrue(cachedIdentifier == oldGlobalIdentifier);
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Retrieve()
        {
            var angara = Controller.Retrieve($"{Family.Name}");
            var delta  = Controller.Retrieve("Delta");

            // The cache holds a family named Angara, so it should retrieve that family.
            // It doesn't contain a family named delta, so it should return null.
            Assert.IsTrue(angara.Name == Family.Name && angara.GlobalIdentifier == Family.GlobalIdentifier);
            Assert.IsTrue(delta is null);
        }

        [TestMethod]
        public void VehicleFamilyCacheController_Clear()
        {
            Controller.Clear();
            var cacheObjects = Controller.GetCachedObjects().ToArray();

            // Clearing the cache removes everything, so the cache should have a size of 0.
            Assert.IsTrue(cacheObjects.Length == 0);
        }
    }
}