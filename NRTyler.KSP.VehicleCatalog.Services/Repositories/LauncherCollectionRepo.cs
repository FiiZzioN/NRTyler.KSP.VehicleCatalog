﻿// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
//
// Author           : Nicholas Tyler
// Created          : 01-12-2018
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-28-2018
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Interfaces.Generic;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.Services.Interfaces;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace NRTyler.KSP.VehicleCatalog.Services.Repositories
{
    /// <summary>
    /// Handles the creation, retrieval, updating, and deletion of <see cref="LauncherCollection"/> objects and their directories.
    /// </summary>
    /// <seealso cref="NRTyler.CodeLibrary.Interfaces.Generic.IDataContractRepository{NRTyler.KSP.VehicleCatalog.Models.DataProviders.LauncherCollection}" />
    /// <seealso cref="NRTyler.CodeLibrary.Interfaces.Generic.ICrudRepository{NRTyler.KSP.VehicleCatalog.Models.DataProviders.LauncherCollection}" />
    public sealed class LauncherCollectionRepo : IDataContractRepository<LauncherCollection>, ICrudRepository<LauncherCollection>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LauncherCollectionRepo"/> class.
        /// </summary>
        /// <param name="path">The directory where launcher collections are saved.</param>
        public LauncherCollectionRepo(string path) : this(path, new ErrorReport(true))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LauncherCollectionRepo"/> class.
        /// </summary>
        /// <param name="path">The directory where launchers are saved.</param>
        /// <param name="errorDialogService">The dialog service that'll be used when an error occurs.</param>
        public LauncherCollectionRepo(string path, IErrorDialogService errorDialogService)
        {
            Path               = path;
            ErrorDialogService = errorDialogService;
            DCSerializer       = new DataContractSerializer(typeof(LauncherCollection));
        }

        #region Fields and Properties

        private DataContractSerializer dcSerializer;

        /// <summary>
        /// Gets or sets the <see cref="T:System.Runtime.Serialization.DataContractSerializer" />.
        /// </summary>
        public DataContractSerializer DCSerializer
        {
            get { return this.dcSerializer; }
            set
            {
                if (value == null) return;
                this.dcSerializer = value;
            }
        }

        /// <summary>
        /// Gets the directory where launcher collections are saved.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets or sets the service that shows the error reporting dialog boxes.
        /// </summary>
        private IErrorDialogService ErrorDialogService { get; }

        #endregion

        /// <summary>
        /// Serializes an <see cref="T:System.Object" /> using the specified <see cref="T:System.IO.Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream" /> that the <see cref="T:System.Object" /> will be serialized to.</param>
        /// <param name="obj">The <see cref="T:System.Object" /> being serialized.</param>
        public void Serialize(Stream stream, LauncherCollection obj)
        {
            var xmlWriter = XmlWriter.Create(stream, RetrieveXMLWriterSettings());

            using (xmlWriter)
            {
                DCSerializer.WriteObject(xmlWriter, obj);
            }
        }

        /// <summary>
        /// Deserializes an <see cref="T:System.Object" /> using the specified <see cref="T:System.IO.Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream" /> that the <see cref="T:System.Object" /> is being deserialized from.</param>
        /// <returns>The deserialized <see cref="T:System.Object" />.</returns>
        public LauncherCollection Deserialize(Stream stream)
        {
            using (stream)
            {
                return (LauncherCollection)DCSerializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Creates the launcher collection's directory and XML file.
        /// </summary>
        /// <param name="obj">The launcher collection you want to serialize.</param>
        public void Create(LauncherCollection obj)
        {
            var message        = String.Empty;
            var collectionName = obj.Name;
            var path           = $"{Path}/{collectionName}";

            // This methods job is to create new objects, not replace them. 
            // If the folder already exists, then that's what the update method is for.
            // If you need to replace the family, delete it and then create it again.
            if (Directory.Exists(path))
            {
                message =
                    "A Launcher Collection with that name already exists. If you wish to update the launcher collection, call the Update() method. " +
                    "If you want to replace the launcher collection, call the Delete() method and then try to create the launcher collection again.";
                ErrorDialogService.Show(message);
                return;
            }

            var directory  = CreateLauncherDirectory(obj);
            var fileStream = CreateLauncherFileStream(obj);

            using (fileStream)
            {
                Serialize(fileStream, obj);
            }
        }

        /// <summary>
        /// Retrieves a launcher collection with specified name / key.
        /// </summary>
        /// <param name="key">The name of the launcher collection.</param>
        public LauncherCollection Retrieve(string key)
        {
            var message = String.Empty;
            var path    = $"{Path}/{key}/{key}.xml";

            FileStream stream = null;

            try
            {
                stream = File.OpenRead(path);
            }
            catch (DirectoryNotFoundException)
            {
                message = "The launcher collection's directory couldn't be found because the path was invalid (for example, it's on an unmapped drive).";
                ErrorDialogService.Show(message);
            }
            catch (FileNotFoundException)
            {
                message = "The launcher collection's XML file couldn't be found.";
                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "The launcher collection's XML file couldn't be retrieved as the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = $"The launcher collection's XML file couldn't be retrieved because an Input / Output error occurred while opening the file.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "The launcher collection's XML file couldn't be retrieved because this application doesn't have access to the destination.";
                ErrorDialogService.Show(message);
            }
            catch (NotSupportedException)
            {
                message = "The launcher collection's XML file couldn't be retrieved because the path was in an invalid format.";
                ErrorDialogService.Show(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDialogService.Show(e.Message);
                throw;
            }

            using (stream)
            {
                return Deserialize(stream);
            }
        }

        /// <summary>
        /// Updates a launcher collection with the specified launcher collection.
        /// </summary>
        /// <param name="obj">The launcher collection you wish to replace the old launcher collection with.</param>
        public void Update(LauncherCollection obj)
        {
            var message        = String.Empty;
            var collectionName = obj.Name;
            var path           = $"{Path}/{collectionName}";

            if (!Directory.Exists(path))
            {
                message = "The launcher collection's directory doesn't exist so there's nothing to update.";
                ErrorDialogService.Show(message);
                return;
            }

            var fileStream = CreateLauncherFileStream(obj);

            using (fileStream)
            {
                Serialize(fileStream, obj);
            }
        }

        /// <summary>
        /// Deletes the directory of the launcher collection with the specified name / key. This also removes all files inside of the directory.
        /// </summary>
        /// <param name="key">The name of the launcher collection.</param>
        public void Delete(string key)
        {
            var message = String.Empty;
            var path    = $"{Path}/{key}";

            try
            {
                Directory.Delete(path, true);
            }
            catch (DirectoryNotFoundException)
            {
                message = "The launcher collection's directory couldn't be deleted because the path was invalid (for example, it's on an unmapped drive or it couldn't be found).";
                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "The launcher collection's directory couldn't be deleted because the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = "The launcher collection's directory couldn't be deleted because it's the applications current " +
                          "working directory, being used by another process, or contains a read-only file.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "The launcher collection's directory couldn't be deleted because this application doesn't have the proper permissions.";
                ErrorDialogService.Show(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDialogService.Show(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Creates the directory where the launcher collection's files are located.
        /// Returns the <see cref="DirectoryInfo"/> of the directory being created.
        /// </summary>
        /// <param name="obj">The <see cref="LauncherCollection"/> object that this method uses to gather its information.</param>
        private DirectoryInfo CreateLauncherDirectory(LauncherCollection obj)
        {
            var message        = String.Empty;
            var collectionName = obj.Name;
            var path           = $"{Path}/{collectionName}";

            DirectoryInfo directory = null;

            try
            {
                directory = Directory.CreateDirectory(path);
            }
            catch (DirectoryNotFoundException)
            {
                message = "The launcher collection directory couldn't be created because the path was invalid (for example, it's on an unmapped drive).";
                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "The launcher collection directory couldn't be created because the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = $"The launcher collection directory couldn't be created because the specified path was a file, or the network name isn't known.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "The launcher collection directory couldn't be created because this application doesn't have access to the destination.";
                ErrorDialogService.Show(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDialogService.Show(e.Message);
                throw;
            }

            return directory;
        }

        /// <summary>
        /// Creates the XML file where the launcher collection's information is held.
        /// Returns the <see cref="FileStream"/> of the file being created.
        /// </summary>
        /// <param name="obj">The <see cref="LauncherCollection"/> object that this method uses to gather its information.</param>
        private FileStream CreateLauncherFileStream(LauncherCollection obj)
        {
            var message        = String.Empty;
            var collectionName = obj.Name;
            var path           = $"{Path}/{collectionName}/{collectionName}.xml";

            FileStream stream = null;

            try
            {
                stream = File.Create(path);
            }
            catch (DirectoryNotFoundException)
            {
                message = "The launcher collection file couldn't be created because the path was invalid (for example, it's on an unmapped drive).";
                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "The launcher collection file couldn't be created because the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = $"The launcher collection file couldn't be created because the specified path was a file, or the network name isn't known.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "The launcher collection file couldn't be created because this application doesn't have access to the destination.";
                ErrorDialogService.Show(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDialogService.Show(e.Message);
                throw;
            }

            return stream;
        }

        /// <summary>
        /// Retrieves the standard XML writer settings that this repo uses.
        /// </summary>
        private XmlWriterSettings RetrieveXMLWriterSettings()
        {
            return new XmlWriterSettings
            {
                CloseOutput             = true,
                Indent                  = true,
                NewLineOnAttributes     = true,
                WriteEndDocumentOnClose = true
            };
        }
    }
}