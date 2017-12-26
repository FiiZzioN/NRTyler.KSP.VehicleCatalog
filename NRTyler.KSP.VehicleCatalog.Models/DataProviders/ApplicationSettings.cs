// ***********************************************************************
// Assembly         : NRTyler.KSPManager.Models
//
// Author           : Nicholas Tyler
// Created          : 09-07-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 09-11-2017
//
// License          : GNU General Public License v3.0
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    [Serializable]
    public sealed class ApplicationSettings : INotifyPropertyChanged, IEnumerable<string>
    {
        public ApplicationSettings()
        {
            VehicleFileLocation = $"{CurrentDirectory}/Vehicles";
            SettingsLocation    = $"{CurrentDirectory}/Settings";
        }

        public string CurrentDirectory { get; } = Environment.CurrentDirectory;

        private string vehicleFileLocation;
        private string settingsLocation;

        public string VehicleFileLocation
        {
            get { return this.vehicleFileLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.vehicleFileLocation = value;
                OnPropertyChanged(nameof(VehicleFileLocation));

                DirectoryEx.CreateDirectoryIfNonexistent(VehicleFileLocation);
            }
        }

        public string SettingsLocation
        {
            get { return this.settingsLocation; }
            set
            {
                if (String.IsNullOrWhiteSpace(value)) return;

                this.settingsLocation = value;
                OnPropertyChanged(nameof(SettingsLocation));

                DirectoryEx.CreateDirectoryIfNonexistent(SettingsLocation);
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

        #region Implementation of IEnumerable

        private IEnumerable<string> DirectoryList()
        {
            var list = new List<string>
            {
                VehicleFileLocation,
                SettingsLocation,
            };

            return list;
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var path in DirectoryList())
            {
                yield return path;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}