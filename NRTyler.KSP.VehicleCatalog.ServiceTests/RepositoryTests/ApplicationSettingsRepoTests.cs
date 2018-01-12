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
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using System;
using System.IO;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests.RepositoryTests
{
    [TestClass]
    public class ApplicationSettingsRepoTests
    {
        [TestInitialize]
        public void Initialize()
        {
            Path     = $"{Environment.CurrentDirectory}/{ApplicationSettingsRepo.FileName}.xml";
            Settings = new ApplicationSettings();
            Repo     = new ApplicationSettingsRepo(Path, new ErrorReport(false));
        }

        private string Path { get; set; }
        private ApplicationSettings Settings { get; set; }
        private ApplicationSettingsRepo Repo { get; set; }

        [TestMethod]
        public void SettingsCreationTest()
        {
            // The file has to be deleted for the test to actually see it being created,
            // otherwise we could have one lurking from a previous test giving a false positive.
            File.Delete(Path);

            Repo.Create(Settings);

            // If the file exists, then return true.
            Assert.IsTrue(File.Exists(Path));
        }

        [TestMethod]
        public void SettingsRetrievalTest()
        {
            // The file has to be deleted for the test to actually see it being created,
            // otherwise we could have one lurking from a previous test giving a false positive.
            File.Delete(Path);

            // A unique settings file to test against.
            Settings = new ApplicationSettings
            {
                PayloadLocation       = $"{Environment.CurrentDirectory}/NewPayloadLocation",
                VehicleFamilyLocation = $"{Environment.CurrentDirectory}/NewFamilyLocation"
            };

            // You have to have a file to retrieve.
            Repo.Create(Settings);

            var settingsRetrieved = Repo.Retrieve(Path);

            Assert.IsTrue(settingsRetrieved.PayloadLocation       == Settings.PayloadLocation);
            Assert.IsTrue(settingsRetrieved.VehicleFamilyLocation == Settings.VehicleFamilyLocation);
        }

        [TestMethod]
        public void SettingsUpdateTest()
        {
            // Using this method to create a unique settings file so I can see if it's been updated.
            SettingsRetrievalTest();

            // Settings needs to be reverted to a default settings file since the other method changes it.
            Settings = new ApplicationSettings();

            Repo.Update(Settings);
            var settingsRetrieved = Repo.Retrieve(Path);

            Assert.IsTrue(settingsRetrieved.PayloadLocation       == Settings.PayloadLocation);
            Assert.IsTrue(settingsRetrieved.VehicleFamilyLocation == Settings.VehicleFamilyLocation);
        }

        [TestMethod]
        public void SettingsDeleteTest()
        {
            // A file has to be created to test that it gets deleted.
            Repo.Create(Settings);
            Repo.Delete(Path);

            Assert.IsFalse(File.Exists(Path));
        }
    }
}