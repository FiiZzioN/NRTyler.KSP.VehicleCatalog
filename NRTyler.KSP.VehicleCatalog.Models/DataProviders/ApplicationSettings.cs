// ***********************************************************************
// Assembly         : NRTyler.KSPManager.Models
//
// Author           : Nicholas Tyler
// Created          : 09-07-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-29-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Holds information regarding where files are saved. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "ApplicationSettings")]
    public sealed class ApplicationSettings : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSettings" /> class.
        /// </summary>
        public ApplicationSettings()
        {
            CurrentDirectory      = Environment.CurrentDirectory;
            VehicleFamilyLocation = $"{CurrentDirectory}/VehicleFamilies";
            PayloadLocation       = $"{CurrentDirectory}/Payloads";
        }

        private string vehicleFamilyLocation;
        private string payloadLocation;

        /// <summary>
        /// Gets the current directory that the application is residing in.
        /// </summary>
        //[DataMember]
        public string CurrentDirectory { get; }

        /// <summary>
        /// Gets or sets the location where <see cref="VehicleFamily"/> objects are saved.
        /// </summary>
        [DataMember]
        public string VehicleFamilyLocation
        {
            get { return this.vehicleFamilyLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.vehicleFamilyLocation = value;
                OnPropertyChanged(nameof(VehicleFamilyLocation));

                DirectoryEx.CreateDirectoryIfNonexistent(VehicleFamilyLocation);
            }
        }

        /// <summary>
        /// Gets or sets the location where <see cref="Payload"/> objects are saved.
        /// </summary>
        [DataMember]
        public string PayloadLocation
        {
            get { return this.payloadLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.payloadLocation = value;
                OnPropertyChanged(nameof(PayloadLocation));

                DirectoryEx.CreateDirectoryIfNonexistent(PayloadLocation);
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}