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
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.ModelTests;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using System;
using System.IO;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests.RepositoryTests
{
    [TestClass]
    public class LauncherRepoTests : CatalogInitializer
    {
        #region Initialization and Clean Up

        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer(ExtraLaunchers.AddNone);

            Settings = new ApplicationSettings();

            var path        = Settings.LauncherLocation;
            var errorReport = new ErrorReport(false);

            Repo             = new LauncherRepo(path, errorReport);
            DirectoryCreator = new DirectoryCreator(Settings, errorReport);

            DirectoryCreator.CreateLauncherStorageLocation();

            var launcherName         = AngaraA5.Name;
            PathHolder.DirectoryPath = $"{path}/{launcherName}";
            PathHolder.XMLPath       = $"{path}/{launcherName}/{launcherName}.xml";
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
        /// Gets or sets the <see cref="LauncherRepo"/> object.
        /// </summary>
        protected LauncherRepo Repo { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Services.Utilities.DirectoryCreator"/> object.
        /// </summary>
        protected DirectoryCreator DirectoryCreator { get; set; }

        #endregion

        /// <summary>
        /// We see if the LauncherRepo.Create() method functions properly.
        /// </summary>
        [TestMethod]
        public void LauncherRepo_Create()
        {
            Repo.Create(AngaraA5);

            Assert.IsTrue(Directory.Exists(PathHolder.DirectoryPath));
            Assert.IsTrue(File.Exists(PathHolder.XMLPath));
        }

        /// <summary>
        /// We see if the LauncherRepo.Create() method properly handles
        /// a situation where you try to create a file that already exists.
        /// </summary>
        [TestMethod]
        public void LauncherRepo_CreateAlreadyExistentItem()
        {
            Repo.Create(AngaraA5);

            // We create a copy of the launcher so we have something to compare against the retrieved launcher.
            // Since the serialized launcher doesn't save everything, we have to change a value that IS saved.
            var editedLauncher = AngaraA5.CopyObject();
            editedLauncher.RootIdentifier = Guid.NewGuid();

            // Try to create another object even though one should already exist.
            Repo.Create(editedLauncher);

            var retievedObject = Repo.Retrieve(AngaraA5.Name);

            // We tried to create a launcher with the same name, but an edited "RootIdentifier". Since 
            // the Create() method doesn't allow the creation of launcher directories with the same name, 
            // the retrieved object's "RootIdentifier" shouldn't equal the edited launcher's identifier.
            Assert.IsTrue(retievedObject.RootIdentifier != editedLauncher.RootIdentifier);
        }

        /// <summary>
        /// We see if the LauncherRepo.Retrieve() method functions properly.
        /// </summary>
        [TestMethod]
        public void LauncherRepo_Retrieve()
        {
            Repo.Create(AngaraA5);

            // We create a copy of the launcher so we have something to compare against the retrieved launcher.
            // Since the serialized launcher doesn't save everything, we have to change a value that IS saved.
            var editedLauncher = AngaraA5.CopyObject();
            editedLauncher.RootIdentifier = Guid.NewGuid();

            var retrievedObject = Repo.Retrieve(AngaraA5.Name);

            // We serialized the default launcher, so if we retrieved that, its "RootIdentifier" should be the same.
            // With that said, if we retrieved the default launcher, it shouldn't be equal the edited launcher.
            Assert.IsTrue(retrievedObject.RootIdentifier == AngaraA5.RootIdentifier);
            Assert.IsTrue(retrievedObject.RootIdentifier != editedLauncher.RootIdentifier);
        }

        /// <summary>
        /// We see if the LauncherRepo.Update() method functions properly.
        /// </summary>
        [TestMethod]
        public void LauncherRepo_Update()
        {
            Repo.Create(AngaraA5);

            // We create a copy of the launcher so we have something to compare against the retrieved launcher.
            // Since the serialized launcher doesn't save everything, we have to change a value that IS saved.
            var editedLauncher = AngaraA5.CopyObject();
            editedLauncher.RootIdentifier = Guid.NewGuid();

            // Update the newly create launcher with the edited launcher.
            Repo.Update(editedLauncher);

            // Retrieve the updated launcher.
            var retievedObject = Repo.Retrieve(AngaraA5.Name);

            // We update the launcher with another one that had an edit 
            // "RootIdentifier", so it shouldn't be equal the default launcher.
            Assert.IsTrue(retievedObject.RootIdentifier != AngaraA5.RootIdentifier);
        }

        /// <summary>
        /// We see if the LauncherRepo.Update() method properly 
        /// handles a situation where the file being updated doesn't exist.
        /// </summary>
        [TestMethod]
        public void LauncherRepo_UpdateNonexistentItem()
        {
            Repo.Update(AngaraA5);

            // The launcher file shouldn't exist since the Update() method can't update a launcher that doesn't exist.
            Assert.IsFalse(File.Exists(PathHolder.XMLPath));
        }

        /// <summary>
        /// We see if the LauncherRepo.Delete() method functions properly.
        /// </summary>
        [TestMethod]
        public void LauncherRepo_Delete()
        {
            // Create the file so we can delete it.
            Repo.Create(AngaraA5);
            Repo.Delete(AngaraA5.Name);

            // If the launcher was deleted, the launcher directory shouldn't exist. 
            Assert.IsTrue(!Directory.Exists(PathHolder.DirectoryPath));
        }
    }
}