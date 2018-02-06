// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
//
// Author           : Nicholas Tyler
// Created          : 02-02-2018
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 02-05-2018
//
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.ModelTests;
using NRTyler.KSP.VehicleCatalog.Services.Cache;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using NRTyler.KSP.VehicleCatalog.Services.Utilities.Comparers;
using System;
using System.IO;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests.CacheTests
{
    [TestClass]
    public class LauncherCollectionCacheTests : CatalogInitializer
    {
        #region Initialization and Clean Up

        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer(ExtraLaunchers.AddNone);

            Settings = new ApplicationSettings();

            var path        = Settings.LauncherCollectionLocation;
            var errorReport = new ErrorReport(false);

            DirectoryCreator = new DirectoryCreator(Settings, errorReport);
            DirectoryCreator.CreateLauncherCollectionStorageLocation();

            var repo = new LauncherCollectionRepo(path, errorReport);
            repo.Create(Collection);

            Cache = new LauncherCollectionCache(Settings, errorReport);
            AdditionalCollections = new[]
            {
                new LauncherCollection("Falcon"), 
                new LauncherCollection("Delta"),
                new LauncherCollection("Atlas"),
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            DeleteLauncherCollectionDirectory();
        }

        protected void DeleteLauncherCollectionDirectory()
        {
            var path = Settings.LauncherCollectionLocation;

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
        /// Gets or sets the <see cref="LauncherCollectionCache"/> object.
        /// </summary>
        protected LauncherCollectionCache Cache { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Services.Utilities.DirectoryCreator"/> object.
        /// </summary>
        protected DirectoryCreator DirectoryCreator { get; set; }

        /// <summary>
        /// Gets or sets an array containing additional collections that can be used to test these methods. 
        /// All the additional collections have initialized are their names and GlobalIdentifiers.
        /// </summary>
        protected LauncherCollection[] AdditionalCollections { get; set; }

        /// <summary>
        /// Gets the comparer that determines whether a collection is in a collection or not.
        /// </summary>
        protected LauncherCollectionEqualityComparer Comparer { get; } = new LauncherCollectionEqualityComparer();

        #endregion

        [TestMethod]
        public void LauncherCollectionCache_Populate()
        {
            Cache.AddRange(AdditionalCollections);
            Cache.Populate();

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // Since Populate() clears the cache if it isn't empty, there should be only one item even though we 
            // added the additional collections. Then, Populate() gathers all collections in the storage location. 
            // Since the A5 collection is the only one saved there, it should be the only collection in the cache.
            Assert.IsTrue(cacheObjects.Length == 1);
            Assert.IsTrue(cacheObjects.Contains(Collection, Comparer));
        }

        [TestMethod]
        public void LauncherCollectionCache_Add_Object()
        {
            Cache.Clear();
            Cache.Add(Collection);

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We cleared the cache and then added the default collection, so 
            // the retrieved item should be the same as the default collection.
            Assert.IsTrue(cacheObjects.Contains(Collection, Comparer));
        }

        [TestMethod]
        public void LauncherCollectionCache_Add_ByName()
        {
            Cache.Clear();
            Cache.Add(Collection.Name);

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We cleared the cache and then added the default collection, so
            // the retrieved item should be the same as the default collection.
            Assert.IsTrue(cacheObjects.Contains(Collection, Comparer));
        }

        [TestMethod]
        public void LauncherCollectionCache_AddRange()
        {
            Cache.Clear();
            Cache.AddRange(AdditionalCollections);

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            var falcon = cacheObjects.SingleOrDefault(e => e.Name == "Falcon");
            var delta  = cacheObjects.SingleOrDefault(e => e.Name == "Delta");
            var atlas  = cacheObjects.SingleOrDefault(e => e.Name == "Atlas");

            // We cleared the cache and then added the additional collections, so it should only contain those three collections.
            Assert.IsTrue(cacheObjects.Contains(falcon));
            Assert.IsTrue(cacheObjects.Contains(delta));
            Assert.IsTrue(cacheObjects.Contains(atlas));
            Assert.IsTrue(cacheObjects.Length == 3);
        }

        [TestMethod]
        public void LauncherCollectionCache_Remove_Object()
        {
            Cache.Remove(Collection);
            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We removed the collection so it shouldn't be in the cached objects.
            Assert.IsFalse(cacheObjects.Contains(Collection, Comparer));
        }

        [TestMethod]
        public void LauncherCollectionCache_Remove_ByName()
        {
            Cache.Remove($"{Collection.Name}");
            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We removed the collection so it shouldn't be in the cached objects.
            Assert.IsTrue(!cacheObjects.Contains(Collection, Comparer));
        }

        [TestMethod]
        public void LauncherCollectionCache_Refresh()
        {
            Cache.AddRange(AdditionalCollections);
            Cache.Refresh();

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            var falcon = cacheObjects.SingleOrDefault(e => e.Name == "Falcon");

            // We added the additional collections and then refreshed the cache. Since the only collection saved in 
            // the storage location is the A5 collection, then it should be the only one in the cached objects.
            Assert.IsTrue(cacheObjects.Contains(Collection, Comparer));
            Assert.IsTrue(!cacheObjects.Contains(falcon));
        }

        [TestMethod]
        public void LauncherCollectionCache_Refresh_Object()
        {
            var oldGlobalIdentifier     = Collection.GlobalIdentifier;
            var newGlobalIdentifier     = new Guid();
            Collection.GlobalIdentifier = newGlobalIdentifier;

            Cache.Refresh(Collection);

            var cacheObjects     = Cache.GetCachedObjects().ToArray();
            var cachedIdentifier = cacheObjects[0].GlobalIdentifier;

            // We assigned a new GlobalIdentifier to the collection, and then refreshed the object in the cache. Refreshing an 
            // abject removes it from the cache and then reloads it from storage. Since the stored collection hadn't been
            // updated, the retrieved collection should still have the old GlobalIdentifier.
            Assert.IsTrue(cachedIdentifier != newGlobalIdentifier);
            Assert.IsTrue(cachedIdentifier == oldGlobalIdentifier);
        }

        [TestMethod]
        public void LauncherCollectionCache_Refresh_ByName()
        {
            var oldGlobalIdentifier     = Collection.GlobalIdentifier;
            var newGlobalIdentifier     = new Guid();
            Collection.GlobalIdentifier = newGlobalIdentifier;

            Cache.Refresh($"{Collection.Name}");

            var cacheObjects     = Cache.GetCachedObjects().ToArray();
            var cachedIdentifier = cacheObjects[0].GlobalIdentifier;

            // We assigned a new GlobalIdentifier to the collection, and then refreshed the object in the cache. Refreshing an 
            // abject removes it from the cache and then reloads it from storage. Since the stored collection hadn't been
            // updated, the retrieved collection should still have the old GlobalIdentifier.
            Assert.IsTrue(cachedIdentifier != newGlobalIdentifier);
            Assert.IsTrue(cachedIdentifier == oldGlobalIdentifier);
        }

        [TestMethod]
        public void LauncherCollectionCache_Retrieve()
        {
            var angara = Cache.Retrieve($"{Collection.Name}");
            var delta  = Cache.Retrieve("Delta");

            // The cache holds a collection named A5, so it should retrieve that collection.
            // It doesn't contain a collection named delta, so it should return null.
            Assert.IsTrue(angara.Name == Collection.Name && angara.GlobalIdentifier == Collection.GlobalIdentifier);
            Assert.IsTrue(delta is null);
        }

        [TestMethod]
        public void LauncherCollectionCache_Clear()
        {
            Cache.Clear();
            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // Clearing the cache removes everything, so the cache should have a size of 0.
            Assert.IsTrue(cacheObjects.Length == 0);
        }
    }
}