// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
// 
// Author           : Nicholas Tyler
// Created          : 01-23-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-23-2018
// 
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using System.IO;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests.UtilityTests
{
    [TestClass]
    public class DirectoryCreatorTests
    {
        #region Initialization and Clean Up

        [TestInitialize]
        public void Initialize()
        {
            Settings         = new ApplicationSettings();
            DirectoryCreator = new DirectoryCreator(Settings, new ErrorReport(false));
        }

        [TestCleanup]
        public void CleanUp()
        {
            DeleteVehicleFamilyDirectory();
            DeleteLauncherCollectionDirectory();
            DeleteLauncherDirectory();
            DeletePayloadDirectory();
        }

        protected void DeleteVehicleFamilyDirectory()
        {
            var path = Settings.VehicleFamilyLocation;

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        protected void DeleteLauncherCollectionDirectory()
        {
            var path = Settings.LauncherCollectionLocation;

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        protected void DeleteLauncherDirectory()
        {
            var path = Settings.LauncherLocation;

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        protected void DeletePayloadDirectory()
        {
            var path = Settings.PayloadLocation;

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
        /// Gets or sets the <see cref="Services.Utilities.DirectoryCreator"/> object.
        /// </summary>
        protected DirectoryCreator DirectoryCreator { get; set; }

        #endregion

        /// <summary>
        /// We see if the DirectoryCreator.CreateVehicleFamilyStorageLocation() method functions properly.
        /// </summary>
        [TestMethod]
        public void DirectoryCreator_CreateVehicleFamilyLocation()
        {
            var path = Settings.VehicleFamilyLocation;

            DirectoryCreator.CreateVehicleFamilyStorageLocation();

            Assert.IsTrue(Directory.Exists(path));
        }

        /// <summary>
        /// We see if the DirectoryCreator.CreateLauncherCollectionStorageLocation() method functions properly.
        /// </summary>
        [TestMethod]
        public void DirectoryCreator_CreateLauncherCollectionLocation()
        {
            var path = Settings.LauncherCollectionLocation;

            DirectoryCreator.CreateLauncherCollectionStorageLocation();

            Assert.IsTrue(Directory.Exists(path));
        }

        /// <summary>
        /// We see if the DirectoryCreator.CreateLauncherStorageLocation() method functions properly.
        /// </summary>
        [TestMethod]
        public void DirectoryCreator_CreateLauncherLocation()
        {
            var path = Settings.LauncherLocation;

            DirectoryCreator.CreateLauncherStorageLocation();

            Assert.IsTrue(Directory.Exists(path));
        }

        /// <summary>
        /// We see if the DirectoryCreator.CreatePayloadStorageLocation() method functions properly.
        /// </summary>
        [TestMethod]
        public void DirectoryCreator_CreatePayloadLocation()
        {
            var path = Settings.PayloadLocation;

            DirectoryCreator.CreatePayloadStorageLocation();

            Assert.IsTrue(Directory.Exists(path));
        }
    }
}