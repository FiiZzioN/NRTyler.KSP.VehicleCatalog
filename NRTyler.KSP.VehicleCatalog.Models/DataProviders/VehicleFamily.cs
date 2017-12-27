// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 12-26-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-26-2017
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NRTyler.CodeLibrary.Extensions;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Class VehicleFamily.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.INotepad" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.IPreview" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "VehicleFamily")]
    public class VehicleFamily<T> : INotepad, IPreview, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleFamily{T}"/> class.
        /// </summary>
        public VehicleFamily() : this ("Name Not Set")
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleFamily{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the vehicle family.</param>
        public VehicleFamily(string name)
        {
            Name = name;
        }

        #region Fields of Properties

        private string name;
        private List<Note> notes;
        private string pictureLocation;
        private Summary summary;
        private List<T> vehicleVersions;

        /// <summary>
        /// Gets or sets the name of the vehicle family.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value.HandleNullOrWhiteSpace("Invalid Name");
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Gets or sets the notes for this vehicle family.
        /// </summary>
        public List<Note> Notes
        {
            get { return this.notes; }
            set
            {
                if (value == null) return;

                this.notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        /// <summary>
        /// Gets or sets where the preview picture of this vehicle family is located.
        /// </summary>
        public string PictureLocation
        {
            get { return this.pictureLocation; }
            set
            {
                this.pictureLocation = value;
                OnPropertyChanged(nameof(PictureLocation));
            }
        }

        /// <summary>
        /// Gets or sets the summary for this vehicle family.
        /// </summary>
        public Summary Summary
        {
            get { return this.summary; }
            set
            {
                if (value == null) return;

                this.summary = value;
                OnPropertyChanged(nameof(Summary));
            }
        }

        /// <summary>
        /// Gets or sets the vehicle versions in this family.
        /// </summary>
        public List<T> VehicleVersions
        {
            get { return this.vehicleVersions; }
            set
            {
                if (value == null) return;

                this.vehicleVersions = value;
                OnPropertyChanged(nameof(VehicleVersions));
            }
        }

        #endregion

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