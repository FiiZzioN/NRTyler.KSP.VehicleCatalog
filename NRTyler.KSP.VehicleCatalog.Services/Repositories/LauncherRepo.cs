// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
// 
// Author           : Nicholas Tyler
// Created          : 01-08-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-08-2018
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
    public class LauncherRepo : IDataContractRepository<Launcher>, ICrudRepository<Launcher>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LauncherRepo"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public LauncherRepo(string path) : this(path, new ErrorReport(true))
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LauncherRepo"/> class.
        /// </summary>
        /// <param name="path">The directory where the launcher is located.</param>
        /// <param name="errorDialogService">The dialog service that this will use when an error occurs.</param>
        public LauncherRepo(string path, IErrorDialogService errorDialogService)
        {
            Path               = path;
            ErrorDialogService = errorDialogService;
            DCSerializer       = new DataContractSerializer(typeof(Launcher));
        }

        #region Fields and Properties

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
        public void Serialize(Stream stream, Launcher obj)
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
        public Launcher Deserialize(Stream stream)
        {
            using (stream)
            {
                return (Launcher)DCSerializer.ReadObject(stream);
            }
        }

        /// <summary>
        /// Creates the specified <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" />.</param>
        public void Create(Launcher obj)
        {
            var message = String.Empty;





        }

        /// <summary>
        /// Retrieves an <see cref="T:System.Object" /> with the specified key.
        /// </summary>
        /// <param name="key">The <see cref="T:System.Object" />'s key.</param>
        /// <returns>T.</returns>
        public Launcher Retrieve(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the specified <see cref="T:System.Object" />.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" />.</param>
        public void Update(Launcher obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the <see cref="T:System.Object" /> with the specified key.
        /// </summary>
        /// <param name="key">The <see cref="T:System.Object" />'s key.</param>
        public void Delete(string key)
        {
            throw new NotImplementedException();
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