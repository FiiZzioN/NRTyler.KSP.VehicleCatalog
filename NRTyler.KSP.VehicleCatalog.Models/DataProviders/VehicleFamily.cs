// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-26-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-21-2018
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// A vehicle family is a collection of vehicle versions.
    /// </summary>
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.INotepad" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.IPreview" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "VehicleFamily")]
    public class VehicleFamily : INotepad, IPreview, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleFamily"/> class.
        /// </summary>
        public VehicleFamily() : this ("Name Not Set")
        {
            
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleFamily"/> class.
        /// </summary>
        /// <param name="name">The name of the vehicle family.</param>
        public VehicleFamily(string name)
        {
            Name               = name;
            GlobalIdentifier   = Guid.NewGuid();
            Notes              = new List<Note>();
            Summary            = new Summary();
            Launchers          = new List<Launcher>();
            LauncherCollections = new List<LauncherCollection>();
        }

        #region Fields of Properties

        private string name;
        private Guid globalIdentifier;
        private List<Note> notes;
        private string previewLocation;
        private Summary summary;
        private List<Launcher> launchers;
        private List<LauncherCollection> launcherCollection;


        /// <summary>
        /// Gets or sets the name of this vehicle family.
        /// </summary>
        [DataMember]
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
        /// Gets or sets the <see cref="Guid"/> that various launchers and launcher
        /// collections will use to determine whether they belong to this family.
        /// </summary>
        [DataMember]
        public Guid GlobalIdentifier
        {
            get { return this.globalIdentifier; }
            set
            {
                this.globalIdentifier = value;
                OnPropertyChanged(nameof(GlobalIdentifier));
            }
        }

        /// <summary>
        /// Gets or sets the notes for this vehicle family.
        /// </summary>
        [DataMember]
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
        [DataMember]
        public string PreviewLocation
        {
            get { return this.previewLocation; }
            set
            {
                this.previewLocation = value;
                OnPropertyChanged(nameof(PreviewLocation));
            }
        }

        /// <summary>
        /// Gets or sets the summary for this vehicle family.
        /// </summary>
        [DataMember]
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
        /// Gets or sets the launchers included in the family.
        /// </summary>
        public List<Launcher> Launchers
        {
            get { return this.launchers; }
            set
            {
                if (value == null) return;

                this.launchers = value;
                OnPropertyChanged(nameof(Launchers));
            }
        }

        /// <summary>
        /// Gets or sets the launcher collections included in the family.
        /// </summary>
        public List<LauncherCollection> LauncherCollections
        {
            get { return this.launcherCollection; }
            set
            {
                if (value == null) return;

                this.launcherCollection = value;
                OnPropertyChanged(nameof(LauncherCollections));
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