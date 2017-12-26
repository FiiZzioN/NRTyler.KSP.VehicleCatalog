// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
// 
// Author           : Nicholas Tyler
// Created          : 10-01-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-01-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.Services.Repositories
{
    public class ApplicationSettingsRepository : Repository<ApplicationSettings>, INotifyPropertyChanged
    {
        public ApplicationSettingsRepository() : this("ApplicationSettings.cfg")
        {
            
        }

        public ApplicationSettingsRepository(string fileName)
        {
            FileName = fileName;
        }

        private string fileName;

        public string FileName
        {
            get { return this.fileName; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    this.fileName = "ApplicationSettings.cfg";
                }

                this.fileName = value;
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
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}