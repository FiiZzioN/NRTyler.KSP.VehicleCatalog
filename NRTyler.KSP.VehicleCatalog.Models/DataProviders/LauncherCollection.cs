// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 12-27-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-27-2017
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
    /// <seealso cref="System.Collections.Generic.List{NRTyler.KSP.VehicleCatalog.Models.DataProviders.Launcher}" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.INotepad" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.IPreview" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "LauncherCollection")]
    public class LauncherCollection : List<Launcher>, INotepad, IPreview, INotifyPropertyChanged
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
            Name = name;
        }

        #region Fields of Properties

        private string name;
        private List<Note> notes;
        private string previewLocation;
        private Summary summary;

        /// <summary>
        /// Gets or sets the name of this vehicle version.
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
        /// Gets or sets the notes for this vehicle version.
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
        /// Gets or sets where the preview picture of this vehicle version is located.
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
        /// Gets or sets the summary for this vehicle version.
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