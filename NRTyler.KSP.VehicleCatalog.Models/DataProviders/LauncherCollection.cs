// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 02-05-2018
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
    /// A collection that holds launchers that are of the same version but contain minor changes depending on the missions requirements.
    /// </summary>
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.INotepad" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.IPreview" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "LauncherCollection")]
    public class LauncherCollection :  INotepad, IPreview, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LauncherCollection"/> class.
        /// </summary>
        public LauncherCollection() : this("Name Not Set")
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LauncherCollection"/> class.
        /// </summary>
        /// <param name="name">The name of this vehicle version.</param>
        public LauncherCollection(string name)
        {
            Name             = name;
            GlobalIdentifier = Guid.NewGuid();
            RootIdentifier   = Guid.Empty;
            Notes            = new List<Note>();
            Summary          = new Summary();
            Launchers        = new List<Launcher>();
            PreviewLocation  = String.Empty;
        }

        #region Fields of Properties

        private string name;
        private Guid globalIdentifier;
        private Guid rootIdentifier;
        private List<Note> notes;
        private string previewLocation;
        private Summary summary;
        private List<Launcher> launchers;

        /// <summary>
        /// Gets or sets the name of this launcher collection.
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
        /// Gets or sets the <see cref="Guid"/> that various launchers will use to determine whether they belong to this collection.
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
        /// Gets or sets the <see cref="Guid"/> of the family that this launcher collection belongs to.
        /// </summary>
        [DataMember]
        public Guid RootIdentifier
        {
            get { return this.rootIdentifier; }
            set
            {
                this.rootIdentifier = value;
                OnPropertyChanged(nameof(RootIdentifier));
            }
        }

        /// <summary>
        /// Gets or sets the notes for this launcher collection.
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
        /// Gets or sets where the preview picture of this launcher collection is located.
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
        /// Gets or sets the summary for this launcher collection.
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
        /// Gets or sets the launchers included in this launcher collection.
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