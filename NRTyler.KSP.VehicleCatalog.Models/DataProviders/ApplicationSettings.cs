// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-22-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-21-2018
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
            CurrentDirectory           = Environment.CurrentDirectory;
            VehicleFamilyLocation      = $"{CurrentDirectory}/VehicleFamilies";
            LauncherCollectionLocation = $"{CurrentDirectory}/LauncherCollections";
            LauncherLocation           = $"{CurrentDirectory}/Launchers";
            PayloadLocation            = $"{CurrentDirectory}/Payloads";
        }

        private string vehicleFamilyLocation;
        private string launcherCollectionLocation;
        private string launcherLocation;
        private string payloadLocation;

        /// <summary>
        /// The file name that this settings file, once serialized, will be saved as.
        /// The file type designation is left up to the user.
        /// </summary>
        public const string FileName = "Settings";

        /// <summary>
        /// Gets the current directory that the application is residing in.
        /// </summary>
        [DataMember(Order = 0)]
        public string CurrentDirectory { get; set; }

        /// <summary>
        /// Gets or sets the location where <see cref="VehicleFamily"/> objects are saved.
        /// </summary>
        [DataMember(Order = 1)]
        public string VehicleFamilyLocation
        {
            get { return this.vehicleFamilyLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.vehicleFamilyLocation = value;
                OnPropertyChanged(nameof(VehicleFamilyLocation));
            }
        }

        /// <summary>
        /// Gets or sets the location where <see cref="LauncherCollection"/> objects are saved.
        /// </summary>
        [DataMember(Order = 1)]
        public string LauncherCollectionLocation
        {
            get { return this.launcherCollectionLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.launcherCollectionLocation = value;
                OnPropertyChanged(nameof(LauncherCollectionLocation));
            }
        }

        /// <summary>
        /// Gets or sets the location where <see cref="Launcher"/> objects are saved.
        /// </summary>
        [DataMember(Order = 1)]
        public string LauncherLocation
        {
            get { return this.launcherLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.launcherLocation = value;
                OnPropertyChanged(nameof(LauncherLocation));
            }
        }

        /// <summary>
        /// Gets or sets the location where <see cref="Payload"/> objects are saved.
        /// </summary>
        [DataMember(Order = 1)]
        public string PayloadLocation
        {
            get { return this.payloadLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.payloadLocation = value;
                OnPropertyChanged(nameof(PayloadLocation));
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