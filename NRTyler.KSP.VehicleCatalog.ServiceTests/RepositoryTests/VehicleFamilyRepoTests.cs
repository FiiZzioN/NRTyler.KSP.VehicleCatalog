// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
//
// Author           : Nicholas Tyler
// Created          : 01-08-2018
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
    public class VehicleFamilyRepoTests : CatalogInitializer
    {
        #region Initialization and Clean Up

        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer(ExtraLaunchers.AddNone);

            Settings = new ApplicationSettings();

            var path        = Settings.VehicleFamilyLocation;
            var errorReport = new ErrorReport(false);

            Repo             = new VehicleFamilyRepo(path, errorReport);
            DirectoryCreator = new DirectoryCreator(Settings, errorReport);

            DirectoryCreator.CreateVehicleFamilyStorageLocation();

            var familyName           = Family.Name;
            PathHolder.DirectoryPath = $"{path}/{familyName}";
            PathHolder.XMLPath       = $"{path}/{familyName}/{familyName}.xml";
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
        /// Gets or sets the <see cref="VehicleFamilyRepo"/> object.
        /// </summary>
        protected VehicleFamilyRepo Repo { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Services.Utilities.DirectoryCreator"/> object.
        /// </summary>
        protected DirectoryCreator DirectoryCreator { get; set; }

        #endregion

        /// <summary>
        /// We see if the VehicleFamilyRepo.Create() method functions properly.
        /// </summary>
        [TestMethod]
        public void VehicleFamilyRepo_Create()
        {
            Repo.Create(Family);

            Assert.IsTrue(Directory.Exists(PathHolder.DirectoryPath));
            Assert.IsTrue(File.Exists(PathHolder.XMLPath));
        }

        /// <summary>
        /// We see if the VehicleFamilyRepo.Create() method properly handles
        /// a situation where you try to create a file that already exists.
        /// </summary>
        [TestMethod]
        public void VehicleFamilyRepo_CreateAlreadyExistentItem()
        {
            Repo.Create(Family);

            // We create a copy of the family so we have something to compare against the retrieved family.
            // Since the serialized family doesn't save everything, we have to change a value that IS saved.
            var editedFamily = Family.CopyObject();
            editedFamily.GlobalIdentifier = Guid.NewGuid();

            // Try to create another object even though one should already exist.
            Repo.Create(editedFamily);

            var retievedObject = Repo.Retrieve(Family.Name);

            // We tried to create a family with the same name, but an edited "GlobalIdentifier". Since 
            // the Create() method doesn't allow the creation of family directories with the same name, 
            // the retrieved object's "GlobalIdentifier" shouldn't equal the edited family's identifier.
            Assert.IsTrue(retievedObject.GlobalIdentifier != editedFamily.GlobalIdentifier);
        }

        /// <summary>
        /// We see if the VehicleFamilyRepo.Retrieve() method functions properly.
        /// </summary>
        [TestMethod]
        public void VehicleFamilyRepo_Retrieve()
        {
            Repo.Create(Family);

            // We create a copy of the family so we have something to compare against the retrieved family.
            // Since the serialized family doesn't save everything, we have to change a value that IS saved.
            var editedFamily = Family.CopyObject();
            editedFamily.GlobalIdentifier = Guid.NewGuid();

            var retrievedObject = Repo.Retrieve(Family.Name);

            // We serialized the default family, so if we retrieved that, its "GlobalIdentifier" should be the same.
            // With that said, if we retrieved the default family, it shouldn't be equal the edited family.
            Assert.IsTrue(retrievedObject.GlobalIdentifier == Family.GlobalIdentifier);
            Assert.IsTrue(retrievedObject.GlobalIdentifier != editedFamily.GlobalIdentifier);
        }

        /// <summary>
        /// We see if the VehicleFamilyRepo.Update() method functions properly.
        /// </summary>
        [TestMethod]
        public void VehicleFamilyRepo_Update()
        {
            Repo.Create(Family);

            // We create a copy of the family so we have something to compare against the retrieved family.
            // Since the serialized family doesn't save everything, we have to change a value that IS saved.
            var editedFamily = Family.CopyObject();
            editedFamily.GlobalIdentifier = Guid.NewGuid();

            // Update the newly create family with the edited family.
            Repo.Update(editedFamily);

            // Retrieve the updated family.
            var retievedObject = Repo.Retrieve(Family.Name);

            // We update the family with another one that had an edit 
            // "GlobalIdentifier", so it shouldn't be equal the default family.
            Assert.IsTrue(retievedObject.GlobalIdentifier != Family.GlobalIdentifier);
        }

        /// <summary>
        /// We see if the VehicleFamilyRepo.Update() method properly 
        /// handles a situation where the file being updated doesn't exist.
        /// </summary>
        [TestMethod]
        public void VehicleFamilyRepo_UpdateNonexistentItem()
        {
            Repo.Update(Family);

            // The family file shouldn't exist since the Update() method can't update a family that doesn't exist.
            Assert.IsFalse(File.Exists(PathHolder.XMLPath));
        }

        /// <summary>
        /// We see if the VehicleFamilyRepo.Delete() method functions properly.
        /// </summary>
        [TestMethod]
        public void VehicleFamilyRepo_Delete()
        {
            // Create the file so we can delete it.
            Repo.Create(Family);
            Repo.Delete(Family.Name);

            // If the family was deleted, the family directory shouldn't exist. 
            Assert.IsTrue(!Directory.Exists(PathHolder.DirectoryPath));
        }
    }
}