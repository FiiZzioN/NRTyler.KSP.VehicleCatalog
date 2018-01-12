// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-05-2018
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
    public class ApplicationSettingsRepo : IDataContractRepository<ApplicationSettings>, ICrudRepository<ApplicationSettings>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingsRepo"/> class.
        /// </summary>
        /// <param name="path">The path where the settings file is located.</param>
        public ApplicationSettingsRepo(string path) : this(path, new ErrorReport(true))
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettingsRepo"/> class.
        /// </summary>
        /// <param name="path">The path where the settings file is located.</param>
        /// <param name="errorDialogService">The dialog service that this will use when an error occurs.</param>
        public ApplicationSettingsRepo(string path, IErrorDialogService errorDialogService)
        {
            Path               = path;
            ErrorDialogService = errorDialogService;
            DCSerializer       = new DataContractSerializer(typeof(ApplicationSettings));
        }

        #region Fields, Properties, and Constants

        public const string FileName = "Settings";

        private string path;
        private IErrorDialogService errorDialogService;
        private DataContractSerializer dcSerializer;

        /// <summary>
        /// Gets or sets the path where the settings file will be located.
        /// </summary>
        public string Path
        {
            get { return this.path; }
            set
            {
                if (value == null) return;
                this.path = value;
            }
        }

        /// <summary>
        /// Gets or sets the service that shows the error reporting dialog boxes.
        /// </summary>
        private IErrorDialogService ErrorDialogService
        {
            get { return this.errorDialogService; }
            set
            {
                if (value == null) return;
                this.errorDialogService = value;
            }
        }

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

        #endregion

        /// <summary>
        /// Serializes an <see cref="T:System.Object" /> using the specified <see cref="T:System.IO.Stream" />.
        /// </summary>
        /// <param name="stream">The <see cref="T:System.IO.Stream" /> that the <see cref="T:System.Object" /> will be serialized to.</param>
        /// <param name="obj">The <see cref="T:System.Object" /> being serialized.</param>
        public void Serialize(Stream stream, ApplicationSettings obj)
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
        public ApplicationSettings Deserialize(Stream stream)
        {
            using (stream)
            {
                return (ApplicationSettings)DCSerializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Creates the specified <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" />.</param>
        public void Create(ApplicationSettings obj)
        {
            var message = String.Empty;

            // This methods job is to create new objects, not replace them. 
            // If the file already exists, then that's what the update method is for.
            // If you need to replace the file, delete it and then create it.
            if (File.Exists(Path))
            {
                message =
                    "A file with that name already exists. If you wish to update the file, call the Update() method. " +
                    "If you want to replace the file, call the Delete() method and then try to create the file again.";
                ErrorDialogService.Show(message);
                return;
            }

            FileStream stream = null;

            try
            {
                stream = File.Create(Path);
            }
            catch (UnauthorizedAccessException)
            {
                message = "This application's settings file couldn't be created " +
                          "because doesn't have access to the destination.";

                ErrorDialogService.Show(message);
            }
            catch (PathTooLongException)
            {
                message = "This application's settings file couldn't be created " +
                          "because the resulting file path would be too long.";

                ErrorDialogService.Show(message);
                // Throw here because, if this file can't be created, 
                // then nothing else will be able to be created either.
                throw new PathTooLongException();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDialogService.Show(e.Message);
                throw;
            }

            using (stream)
            {
                Serialize(stream, obj);
            }
        }

        /// <summary>
        /// Retrieves an <see cref="T:System.Object" /> with the specified key.
        /// </summary>
        /// <param name="key">The file's name.</param>
        public ApplicationSettings Retrieve(string key)
        {
            var message = String.Empty;
            FileStream stream = null;

            try
            {
                stream = File.OpenRead(Path);
            }
            catch (UnauthorizedAccessException)
            {
                message = "This application's settings file couldn't be created " +
                          "because doesn't have access to the destination.";

                ErrorDialogService.Show(message);
            }
            catch (FileNotFoundException)
            {
                message =
                    "This application's settings file could not be found. A new one " +
                    "containing the default information has been created in its place.";

                ErrorDialogService.Show(message);

                Create(new ApplicationSettings());
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
        /// Updates the specified <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" />.</param>
        public void Update(ApplicationSettings obj)
        {
            Delete(Path);
            Create(obj);
        }

        /// <summary>
        /// Deletes the <see cref="T:System.Object" /> with the specified key.
        /// </summary>
        /// <param name="key">The <see cref="T:System.Object" />'s key.</param>
        public void Delete(string key)
        {
            var message = String.Empty;

            try
            {
                File.Delete(Path);
            }
            catch (IOException)
            {
                message = "The specified file is in use and cannot be deleted.";
                ErrorDialogService.Show(message);
            }
            catch (UnauthorizedAccessException)
            {
                message = "You don't have the required permissions to remove this file.";
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
        /// Retrieves the standard XML writer settings that this repo uses.
        /// </summary>
        private XmlWriterSettings RetrieveXMLWriterSettings()
        {
            return new XmlWriterSettings
            {
                CloseOutput = true,
                Indent = true,
                NewLineOnAttributes = true,
                WriteEndDocumentOnClose = true
            };
        }
    }
}