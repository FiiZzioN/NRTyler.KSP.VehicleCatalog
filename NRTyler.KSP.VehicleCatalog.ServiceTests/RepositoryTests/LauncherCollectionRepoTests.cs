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
    public class LauncherCollectionRepoTests : CatalogInitializer
    {
        #region Initialization and Clean Up

        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer(ExtraLaunchers.AddNone);

            Settings = new ApplicationSettings();

            var path        = Settings.LauncherCollectionLocation;
            var errorReport = new ErrorReport(false);

            Repo             = new LauncherCollectionRepo(path, errorReport);
            DirectoryCreator = new DirectoryCreator(Settings, errorReport);

            DirectoryCreator.CreateLauncherCollectionStorageLocation();

            var collectionName       = Collection.Name;
            PathHolder.DirectoryPath = $"{path}/{collectionName}";
            PathHolder.XMLPath       = $"{path}/{collectionName}/{collectionName}.xml";
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
        /// Gets or sets the <see cref="LauncherCollectionRepo"/> object.
        /// </summary>
        protected LauncherCollectionRepo Repo { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Services.Utilities.DirectoryCreator"/> object.
        /// </summary>
        protected DirectoryCreator DirectoryCreator { get; set; }

        #endregion

        /// <summary>
        /// We see if the LauncherCollectionRepo.Create() method functions properly.
        /// </summary>
        [TestMethod]
        public void LauncherCollectionRepo_Create()
        {
            Repo.Create(Collection);

            Assert.IsTrue(Directory.Exists(PathHolder.DirectoryPath));
            Assert.IsTrue(File.Exists(PathHolder.XMLPath));
        }

        /// <summary>
        /// We see if the LauncherCollectionRepo.Create() method properly handles
        /// a situation where you try to create a file that already exists.
        /// </summary>
        [TestMethod]
        public void LauncherCollectionRepo_CreateAlreadyExistentItem()
        {
            Repo.Create(Collection);

            // We create a copy of the collection so we have something to compare against the retrieved collection.
            // Since the serialized collection doesn't save everything, we have to change a value that IS saved.
            var editedCollection = Collection.CopyObject();
            editedCollection.GlobalIdentifier = Guid.NewGuid();

            // Try to create another object even though one should already exist.
            Repo.Create(editedCollection);

            var retievedObject = Repo.Retrieve(Collection.Name);

            // We tried to create a collection with the same name, but an edited "GlobalIdentifier". Since 
            // the Create() method doesn't allow the creation of collection directories with the same name, 
            // the retrieved object's "GlobalIdentifier" shouldn't equal the edited collection's identifier.
            Assert.IsTrue(retievedObject.GlobalIdentifier != editedCollection.GlobalIdentifier);
        }

        /// <summary>
        /// We see if the LauncherCollectionRepo.Retrieve() method functions properly.
        /// </summary>
        [TestMethod]
        public void LauncherCollectionRepo_Retrieve()
        {
            Repo.Create(Collection);

            // We create a copy of the collection so we have something to compare against the retrieved collection.
            // Since the serialized collection doesn't save everything, we have to change a value that IS saved.
            var editedCollection = Collection.CopyObject();
            editedCollection.GlobalIdentifier = Guid.NewGuid();

            var retrievedObject = Repo.Retrieve(Collection.Name);

            // We serialized the default collection, so if we retrieved that, its "GlobalIdentifier" should be the same.
            // With that said, if we retrieved the default collection, it shouldn't be equal the edited collection.
            Assert.IsTrue(retrievedObject.GlobalIdentifier == Collection.GlobalIdentifier);
            Assert.IsTrue(retrievedObject.GlobalIdentifier != editedCollection.GlobalIdentifier);
        }

        /// <summary>
        /// We see if the LauncherCollectionRepo.Update() method functions properly.
        /// </summary>
        [TestMethod]
        public void LauncherCollectionRepo_Update()
        {
            Repo.Create(Collection);

            // We create a copy of the collection so we have something to compare against the retrieved collection.
            // Since the serialized collection doesn't save everything, we have to change a value that IS saved.
            var editedCollection = Collection.CopyObject();
            editedCollection.GlobalIdentifier = Guid.NewGuid();

            // Update the newly create collection with the edited collection.
            Repo.Update(editedCollection);

            // Retrieve the updated collection.
            var retievedObject = Repo.Retrieve(Collection.Name);

            // We update the collection with another one that had an edit 
            // "GlobalIdentifier", so it shouldn't be equal the default collection.
            Assert.IsTrue(retievedObject.GlobalIdentifier != Collection.GlobalIdentifier);
        }

        /// <summary>
        /// We see if the LauncherCollectionRepo.Update() method properly 
        /// handles a situation where the file being updated doesn't exist.
        /// </summary>
        [TestMethod]
        public void LauncherCollectionRepo_UpdateNonexistentItem()
        {
            Repo.Update(Collection);

            // The collection file shouldn't exist since the Update() method can't update a collection that doesn't exist.
            Assert.IsFalse(File.Exists(PathHolder.XMLPath));
        }

        /// <summary>
        /// We see if the LauncherCollectionRepo.Delete() method functions properly.
        /// </summary>
        [TestMethod]
        public void LauncherCollectionRepo_Delete()
        {
            // Create the file so we can delete it.
            Repo.Create(Collection);
            Repo.Delete(Collection.Name);

            // If the collection was deleted, the collection directory shouldn't exist. 
            Assert.IsTrue(!Directory.Exists(PathHolder.DirectoryPath));
        }
    }
}