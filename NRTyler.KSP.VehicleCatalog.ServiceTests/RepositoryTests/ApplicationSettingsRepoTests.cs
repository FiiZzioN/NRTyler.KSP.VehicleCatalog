// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
//
// Author           : Nicholas Tyler
// Created          : 01-05-2018
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-08-2018
//
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using System.IO;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests.RepositoryTests
{
    [TestClass]
    public class ApplicationSettingsRepoTests
    {
        #region Initialization and Clean Up

        [TestInitialize]
        public void Initialize()
        {
            Settings = new ApplicationSettings();
            Repo     = new ApplicationSettingsRepo(Settings.CurrentDirectory, new ErrorReport(false));

            var currentDirectory = Settings.CurrentDirectory;
            var fileName         = ApplicationSettings.FileName;

            PathHolder.XMLPath = $"{currentDirectory}/{fileName}.xml";
        }

        [TestCleanup]
        public void CleanUp()
        {
            var path = PathHolder.XMLPath;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ApplicationSettings"/> object.
        /// </summary>
        protected ApplicationSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ApplicationSettingsRepo"/> object.
        /// </summary>
        protected ApplicationSettingsRepo Repo { get; set; }

        #endregion

        /// <summary>
        /// We see if the ApplicationSettingsRepo.Create() method functions properly.
        /// </summary>
        [TestMethod]
        public void ApplicationSettingsRepo_Create()
        {
            Repo.Create(Settings);

            // If the settings file was created, file should exist and the test should pass.
            Assert.IsTrue(File.Exists(PathHolder.XMLPath));
        }

        /// <summary>
        /// We see if the ApplicationSettingsRepo.Create() method properly handles
        /// a situation where you try to create a file that already exists.
        /// </summary>
        [TestMethod]
        public void ApplicationSettingsRepo_CreateAlreadyExistentItem()
        {
            // We have to a file that already exists before we can make   
            // sure a new file isn't created should one already exist.
            Repo.Create(Settings);

            // We create a copy of the default settings object and then edit it 
            // This way we have something to compare against.
            var editedObject = Settings.CopyObject();
            editedObject.LauncherCollectionLocation = "Wow, this is a launcher collection location.";

            // Try to create another object even though one should already exist.
            Repo.Create(editedObject);

            // Retrieve the file that was created so we can compare against it.
            var retievedObject = Repo.Retrieve();

            // If the edited settings file wasn't created, then 
            // it shouldn't be the same as the retrieved object.
            Assert.IsFalse(retievedObject.CompareObject(editedObject));
        }

        /// <summary>
        /// We see if the ApplicationSettingsRepo.Retrieve() method functions properly.
        /// </summary>
        [TestMethod]
        public void ApplicationSettingsRepo_Retrieve()
        {
            // We create a copy of the default settings object so we can  
            // edit it, and then compare to the original object later on.
            var copiedObject = Settings.CopyObject();
            copiedObject.LauncherLocation = "This is a new launcher location.";

            // We have to create it before we can retrieve it.
            Repo.Create(copiedObject);

            // Retrieve it.
            var retievedObject = Repo.Retrieve();

            // If we retrieved the copied object, then it should be different from the original object. Alternatively,
            // if we did retrieve the copied object, then it should be exactly like the one before we serialized it.
            Assert.IsFalse(retievedObject.LauncherLocation == Settings.LauncherLocation);
            Assert.IsTrue(retievedObject.LauncherLocation == copiedObject.LauncherLocation);
        }

        /// <summary>
        /// We see if the ApplicationSettingsRepo.Update() method functions properly.
        /// </summary>
        [TestMethod]
        public void ApplicationSettingsRepo_Update()
        {
            // We create a copy of the default settings object and then edit it 
            // to make sure we received something other than the default object.
            var editedObject = Settings.CopyObject();
            editedObject.LauncherCollectionLocation = "Wow, this is a launcher collection location.";

            // Create the default settings file.
            Repo.Create(Settings);

            // Update the newly create file with the edited settings file.
            Repo.Update(editedObject);

            // Retrieve the updated settings file.
            var retievedObject = Repo.Retrieve();

            // If the default settings file was updated, then the retrieved 
            // object should be the same as the edited object.
            Assert.IsTrue(retievedObject.LauncherCollectionLocation == editedObject.LauncherCollectionLocation);
        }

        /// <summary>
        /// We see if the ApplicationSettingsRepo.Update() method properly 
        /// handles a situation where the file being updated doesn't exist.
        /// </summary>
        [TestMethod]
        public void ApplicationSettingsRepo_UpdateNonexistentItem()
        {
            Repo.Update(Settings);

            // The settings file shouldn't exist since the Update() method can't update a file that doesn't exist.
            Assert.IsFalse(File.Exists(PathHolder.XMLPath));
        }

        /// <summary>
        /// We see if the ApplicationSettingsRepo.Delete() method functions properly.
        /// </summary>
        [TestMethod]
        public void ApplicationSettingsRepo_Delete()
        {
            // Create the file so we can delete it.
            Repo.Create(Settings);
            Repo.Delete();

            // If the file was deleted, the it shouldn't exist. 
            Assert.IsFalse(File.Exists(PathHolder.XMLPath));
        }
    }
}