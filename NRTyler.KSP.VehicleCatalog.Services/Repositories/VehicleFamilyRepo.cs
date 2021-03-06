﻿// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
//
// Author           : Nicholas Tyler
// Created          : 01-05-2018
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
    /// Handles the creation, retrieval, updating, and deletion of <see cref="VehicleFamily"/> objects and their directories.
    /// </summary>
    /// <seealso cref="NRTyler.CodeLibrary.Interfaces.Generic.IDataContractRepository{NRTyler.KSP.VehicleCatalog.Models.DataProviders.LauncherCollection}" />
    /// <seealso cref="NRTyler.CodeLibrary.Interfaces.Generic.ICrudRepository{NRTyler.KSP.VehicleCatalog.Models.DataProviders.LauncherCollection}" />

    public sealed class VehicleFamilyRepo : IDataContractRepository<VehicleFamily>, ICrudRepository<VehicleFamily>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleFamilyRepo"/> class.
        /// </summary>
        /// <param name="path">The directory where launcher collections are saved.</param>
        public VehicleFamilyRepo(string path) : this(path, new ErrorReport(true))
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleFamilyRepo"/> class.
        /// </summary>
        /// <param name="path">The directory where launcher collections are saved.</param>
        /// <param name="errorDialogService">The dialog service that this will use when an error occurs.</param>
        public VehicleFamilyRepo(string path, IErrorDialogService errorDialogService)
        {
            Path               = path;
            ErrorDialogService = errorDialogService;
            DCSerializer       = new DataContractSerializer(typeof(VehicleFamily));
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
        /// Gets the directory where vehicle families are saved.
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
        public void Serialize(Stream stream, VehicleFamily obj)
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
        public VehicleFamily Deserialize(Stream stream)
        {
            using (stream)
            {
                return (VehicleFamily)DCSerializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Creates the vehicle family's directory and XML file.
        /// </summary>
        /// <param name="obj">The launcher collection you want to serialize.</param>
        public void Create(VehicleFamily obj)
        {
            var message    = String.Empty;
            var familyName = obj.Name;
            var path       = $"{Path}/{familyName}";

            // This methods job is to create new objects, not replace them. 
            // If the folder already exists, then that's what the update method is for.
            // If you need to replace the family, delete it and then create it again.
            if (Directory.Exists(path))
            {
                message =
                    "A Family with that name already exists. If you wish to update the family, call the Update() method. " +
                    "If you want to replace the family, call the Delete() method and then try to create the family again.";
                ErrorDialogService.Show(message);
                return;
            }

            var directory  = CreateFamilyDirectory(obj);
            var fileStream = CreateFamilyFileStream(obj);

            using (fileStream)
            {
                Serialize(fileStream, obj);
            }
        }

        /// <summary>
        /// Retrieves a vehicle family with specified name / key.
        /// </summary>
        /// <param name="key">The name of the vehicle family.</param>
        public VehicleFamily Retrieve(string key)
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
                message = "The family's directory couldn't be found because the path was invalid (for example, it's on an unmapped drive).";
                ErrorDialogService.Show(message);
            }
            catch (FileNotFoundException)
            {
                message = "The family's XML file couldn't be found.";
                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "The family's XML file couldn't be retrieved as the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = $"The family's XML file couldn't be retrieved because an Input / Output error occurred while opening the file.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "The family's XML file couldn't be retrieved because this application doesn't have access to the destination.";
                ErrorDialogService.Show(message);
            }
            catch (NotSupportedException)
            {
                message = "The family's XML file couldn't be retrieved because the path was in an invalid format.";
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
        /// Updates a vehicle family with the specified vehicle family.
        /// </summary>
        /// <param name="obj">The vehicle family you wish to replace the old vehicle family with.</param>
        public void Update(VehicleFamily obj)
        {
            var message    = String.Empty;
            var familyName = obj.Name;
            var path       = $"{Path}/{familyName}";

            if (!Directory.Exists(path))
            {
                message = "The family's directory doesn't exist so there's nothing to update.";
                ErrorDialogService.Show(message);
                return;
            }

            var familyFileStream = CreateFamilyFileStream(obj);

            using (familyFileStream)
            {
                Serialize(familyFileStream, obj);
            }
        }

        /// <summary>
        /// Deletes the directory of the vehicle family with the specified name / key. This also removes all files inside of the directory.
        /// </summary>
        /// <param name="key">The name of the vehicle family.</param>
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
                message = "The family's directory couldn't be deleted because the path was invalid (for example, it's on an unmapped drive or it couldn't be found).";
                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "The family's directory couldn't be deleted because the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = "The family's directory couldn't be deleted because it's the applications current " +
                          "working directory, being used by another process, or contains a read-only file.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "The family's directory couldn't be deleted because this application doesn't have the proper permissions.";
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
        /// Creates the directory where the vehicle family's files are located.
        /// Returns the <see cref="DirectoryInfo"/> of the directory being created.
        /// </summary>
        /// <param name="obj">The <see cref="VehicleFamily"/> object that this method uses to gather its information.</param>
        private DirectoryInfo CreateFamilyDirectory(VehicleFamily obj)
        {
            var message    = String.Empty;
            var familyName = obj.Name;
            var path       = $"{Path}/{familyName}";

            DirectoryInfo directory = null;

            try
            {
                directory = Directory.CreateDirectory(path);
            }
            catch (DirectoryNotFoundException)
            {
                message = "The family directory couldn't be created because the path was invalid (for example, it's on an unmapped drive).";
                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "The family directory couldn't be created because the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = $"The family directory couldn't be created because the specified path was a file, or the network name isn't known.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "The family directory couldn't be created because this application doesn't have access to the destination.";
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
        /// Creates the XML file where the vehicle family's information is held.
        /// Returns the <see cref="FileStream"/> of the file being created.
        /// </summary>
        /// <param name="obj">The <see cref="VehicleFamily"/> object that this method uses to gather its information.</param>
        private FileStream CreateFamilyFileStream(VehicleFamily obj)
        {
            var message    = String.Empty;
            var familyName = obj.Name;
            var path       = $"{Path}/{familyName}/{familyName}.xml";

            FileStream stream = null;

            try
            {
                stream = File.Create(path);
            }
            catch (DirectoryNotFoundException)
            {
                message = "The family file couldn't be created because the path was invalid (for example, it's on an unmapped drive).";
                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "The family file couldn't be created because the resulting path would be too long.";
                ErrorDialogService.Show(message);
            }
            catch (IOException)
            {
                message = $"The family file couldn't be created because the specified path was a file, or the network name isn't known.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "The family file couldn't be created because this application doesn't have access to the destination.";
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