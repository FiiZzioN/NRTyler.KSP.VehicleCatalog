// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
// 
// Author           : Nicholas Tyler
// Created          : 01-23-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-23-2018
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.Services.Interfaces;
using System;
using System.IO;

namespace NRTyler.KSP.VehicleCatalog.Services.Utilities
{
    /// <summary>
    /// Used to facilitate the creation of directories that this application uses to store its data.
    /// </summary>
    public class DirectoryCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryCreator"/> class.
        /// </summary>
        /// <param name="applicationSettings">
        /// The <see cref="ApplicationSettings"/> object that methods in this object will pull directory paths from.
        /// </param>
        public DirectoryCreator(ApplicationSettings applicationSettings) : this(applicationSettings, new ErrorReport(true))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryCreator"/> class.
        /// </summary>
        /// <param name="applicationSettings">
        /// The <see cref="ApplicationSettings"/> object that methods in this object will pull directory paths from.
        /// </param>
        /// <param name="errorDialogService">The dialog service that'll be used when an error occurs.</param>
        public DirectoryCreator(ApplicationSettings applicationSettings, IErrorDialogService errorDialogService)
        {
            Settings           = applicationSettings;
            ErrorDialogService = errorDialogService;
        }

        /// <summary>
        /// Gets the <see cref="ApplicationSettings"/> object that holds the storage location information.
        /// </summary>
        private ApplicationSettings Settings { get; }
        /// <summary>
        /// Gets or sets the service that shows the error reporting dialog boxes.
        /// </summary>
        private IErrorDialogService ErrorDialogService { get; }

        /// <summary>
        /// Creates the directory where <see cref="VehicleFamily"/> objects are stored.
        /// Returns a <see cref="DirectoryInfo"/> object for the directory created.
        /// </summary>
        public DirectoryInfo CreateVehicleFamilyStorageLocation()
        {
            var location = Settings.VehicleFamilyLocation;
            return CreateDirectoryIfNonexistent(location, "VehicleFamilies");
        }

        /// <summary>
        /// Creates the directory where <see cref="LauncherCollection"/> objects are stored.
        /// Returns a <see cref="DirectoryInfo"/> object for the directory created.
        /// </summary>
        public DirectoryInfo CreateLauncherCollectionStorageLocation()
        {
            var location = Settings.LauncherCollectionLocation;
            return CreateDirectoryIfNonexistent(location, "LauncherCollections");
        }

        /// <summary>
        /// Creates the directory where <see cref="Launcher"/> objects are stored.
        /// Returns a <see cref="DirectoryInfo"/> object for the directory created.
        /// </summary>
        public DirectoryInfo CreateLauncherStorageLocation()
        {
            var location = Settings.LauncherLocation;
            return CreateDirectoryIfNonexistent(location, "Launchers");
        }

        /// <summary>
        /// Creates the directory where <see cref="Payload"/> objects are stored.
        /// Returns a <see cref="DirectoryInfo"/> object for the directory created.
        /// </summary>
        public DirectoryInfo CreatePayloadStorageLocation()
        {
            var location = Settings.PayloadLocation;
            return CreateDirectoryIfNonexistent(location, "Payloads");
        }

        /// <summary>
        /// Creates a directory to the path specified if it's found to be nonexistent.
        /// Returns a <see cref="DirectoryInfo"/> object for the directory created.
        /// </summary>
        /// <param name="path">The path of the directory.</param>
        /// <param name="directoryName">
        /// Name of the directory. This will placed in the <see cref="IErrorDialogService"/> message showing 
        /// the user which directory couldn't be created should an <see cref="Exception"/> be thrown.
        /// </param>
        private DirectoryInfo CreateDirectoryIfNonexistent(string path, string directoryName)
        {
            var message = String.Empty;
            DirectoryInfo directoryInfo = null;

            try
            {
                directoryInfo = DirectoryEx.CreateDirectoryIfNonexistent(path);
            }
            catch (PathTooLongException)
            {
                message = $"The \"{directoryName}\" directory couldn't be created because the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = $"The \"{directoryName}\" directory couldn't be created because this application doesn't have access to the destination.";
                ErrorDialogService.Show(message);
            }
            catch (ArgumentException)
            {
                message = $"The \"{directoryName}\" directory couldn't be created because the path is prefixed with, or contains, only a colon character (:).";
                ErrorDialogService.Show(message);
            }
            catch (DirectoryNotFoundException)
            {
                message = $"The \"{directoryName}\" directory couldn't be created because the path was invalid (for example, it's on an unmapped drive).";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = $"The \"{directoryName}\" directory couldn't be created because the specified path is a file, or the network name isn't known.";
                ErrorDialogService.Show(message);
            }
            catch (NotSupportedException)
            {
                message = $"The \"{directoryName}\" directory couldn't be created because the path contains a colon character (:) that is not part of a drive label.";
                ErrorDialogService.Show(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDialogService.Show(e.Message);
                throw;
            }

            return directoryInfo;
        }
    }
}