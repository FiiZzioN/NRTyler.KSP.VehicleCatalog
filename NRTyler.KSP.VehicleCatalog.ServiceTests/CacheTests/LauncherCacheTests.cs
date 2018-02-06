// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
// 
// Author           : Nicholas Tyler
// Created          : 02-05-2018
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
using System.IO;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests.CacheTests
{
    [TestClass]
    public class LauncherCacheTests : CatalogInitializer
    {
        #region Initialization and Clean Up

        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer(ExtraLaunchers.AddNone);

            Settings = new ApplicationSettings();

            var path        = Settings.LauncherLocation;
            var errorReport = new ErrorReport(false);

            DirectoryCreator = new DirectoryCreator(Settings, errorReport);
            DirectoryCreator.CreateLauncherStorageLocation();

            var repo = new LauncherRepo(path, errorReport);
            repo.Create(AngaraA5);

            Cache               = new LauncherCache(Settings, errorReport);
            AdditionalLaunchers = new[]
            {
                new Launcher("Falcon 9"),
                new Launcher("Delta II"),
                new Launcher("Atlas V"),
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            DeleteLauncherDirectory();
        }

        protected void DeleteLauncherDirectory()
        {
            var path = Settings.LauncherLocation;

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
        /// Gets or sets the <see cref="LauncherCache"/> object.
        /// </summary>
        protected LauncherCache Cache { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Services.Utilities.DirectoryCreator"/> object.
        /// </summary>
        protected DirectoryCreator DirectoryCreator { get; set; }

        /// <summary>
        /// Gets or sets an array containing additional launchers that can be used to 
        /// test these methods. All the additional launchers have their name initialized.
        /// </summary>
        protected Launcher[] AdditionalLaunchers { get; set; }

        /// <summary>
        /// Gets the comparer that determines whether a launcher is in a collection or not.
        /// </summary>
        protected LauncherEqualityComparer Comparer { get; } = new LauncherEqualityComparer();

        #endregion

        [TestMethod]
        public void LauncherCache_Populate()
        {
            Cache.AddRange(AdditionalLaunchers);
            Cache.Populate();

            var cacheObjects = Cache.GetCachedObjects().ToArray();


            // Since Populate() clears the cache if it isn't empty, there should be only one item even though we 
            // added the additional launcher. Then, Populate() gathers all launchers in the launcher storage location. 
            // Since the Angara A5 is the only one saved there, it should be the only launcher in the cache.
            Assert.IsTrue(cacheObjects.Length == 1);
            Assert.IsTrue(cacheObjects.Contains(AngaraA5, Comparer));
        }

        [TestMethod]
        public void LauncherCache_Add_Object()
        {
            Cache.Clear();
            Cache.Add(AngaraA5);

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We cleared the cache and then added the default Angara A5, so the retrieved item should be the same as the default Angara A5.
            Assert.IsTrue(cacheObjects.Contains(AngaraA5, Comparer));
        }

        [TestMethod]
        public void LauncherCache_Add_ByName()
        {
            Cache.Clear();
            Cache.Add(AngaraA5.Name);

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We cleared the cache and then added the default Angara A5, so the retrieved item should be the same as the default Angara A5.
            Assert.IsTrue(cacheObjects.Contains(AngaraA5, Comparer));
        }

        [TestMethod]
        public void LauncherCache_AddRange()
        {
            Cache.Clear();
            Cache.AddRange(AdditionalLaunchers);

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            var falcon9 = cacheObjects.SingleOrDefault(e => e.Name == "Falcon 9");
            var deltaII = cacheObjects.SingleOrDefault(e => e.Name == "Delta II");
            var atlasV  = cacheObjects.SingleOrDefault(e => e.Name == "Atlas V");

            // We cleared the cache and then added the additional launchers, so it should only contain those three launchers.
            Assert.IsTrue(cacheObjects.Contains(falcon9));
            Assert.IsTrue(cacheObjects.Contains(deltaII));
            Assert.IsTrue(cacheObjects.Contains(atlasV));
            Assert.IsTrue(cacheObjects.Length == 3);
        }

        [TestMethod]
        public void LauncherCache_Remove_Object()
        {
            Cache.Remove(AngaraA5);
            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We removed the Angara A5 so it shouldn't be in the cached objects.
            Assert.IsFalse(cacheObjects.Contains(AngaraA5, Comparer));
        }

        [TestMethod]
        public void LauncherCache_Remove_ByName()
        {
            Cache.Remove($"{AngaraA5.Name}");
            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We removed the Angara A5 so it shouldn't be in the cached objects.
            Assert.IsTrue(!cacheObjects.Contains(AngaraA5, Comparer));
        }

        [TestMethod]
        public void LauncherCache_Refresh()
        {
            Cache.AddRange(AdditionalLaunchers);
            Cache.Refresh();

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            var falcon9 = cacheObjects.SingleOrDefault(e => e.Name == "Falcon 9");

            // We added the additional launchers and then refreshed the cache. Since the only launcher saved in 
            // the storage location is the Angara A5, then it should be the only one in the cached objects.
            Assert.IsTrue(cacheObjects.Contains(AngaraA5, Comparer));
            Assert.IsTrue(!cacheObjects.Contains(falcon9));
        }

        [TestMethod]
        public void LauncherCache_Refresh_Object()
        {
            var oldPrice   = AngaraA5.Price;
            var newPrice   = 10;
            AngaraA5.Price = newPrice;

            Cache.Refresh(AngaraA5);

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We assigned a new Price to the Angara A5, and then refreshed the object in the cache. Refreshing an 
            // abject removes it from the cache and then reloads it from storage. Since the stored launcher hadn't been
            // updated, the retrieved launcher should still have the old Price.
            Assert.IsTrue(cacheObjects[0].Price != newPrice);
            Assert.IsTrue(cacheObjects[0].Price == oldPrice);
        }

        [TestMethod]
        public void LauncherCache_Refresh_ByName()
        {
            var oldPrice = AngaraA5.Price;
            var newPrice = 10;
            AngaraA5.Price = newPrice;

            Cache.Refresh($"{AngaraA5.Name}");

            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // We assigned a new Price to the Angara A5, and then refreshed the object in the cache. Refreshing an 
            // abject removes it from the cache and then reloads it from storage. Since the stored launcher hadn't been
            // updated, the retrieved launcher should still have the old Price.
            Assert.IsTrue(cacheObjects[0].Price != newPrice);
            Assert.IsTrue(cacheObjects[0].Price == oldPrice);
        }

        [TestMethod]
        public void LauncherCache_Retrieve()
        {
            var angara  = Cache.Retrieve($"{AngaraA5.Name}");
            var deltaII = Cache.Retrieve("Delta II");

            // The cache holds a launcher named Angara A5, so it should retrieve that launcher.
            // It doesn't contain a launcher named Delta II, so it should return null.
            Assert.IsTrue(angara.Price == AngaraA5.Price && angara.PreviewLocation == AngaraA5.PreviewLocation);
            Assert.IsTrue(deltaII is null);
        }

        [TestMethod]
        public void LauncherCache_Clear()
        {
            Cache.Clear();
            var cacheObjects = Cache.GetCachedObjects().ToArray();

            // Clearing the cache removes everything, so the cache should have a size of 0.
            Assert.IsTrue(cacheObjects.Length == 0);
        }
    }
}