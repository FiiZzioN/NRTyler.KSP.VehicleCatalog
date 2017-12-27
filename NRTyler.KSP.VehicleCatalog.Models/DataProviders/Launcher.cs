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
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    [Serializable]
    [DataContract(Name = "Launcher")]
    public class Launcher : IVehicle, INotifyPropertyChanged
    {
        public Launcher() : this("Name Not Specified")
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Launcher"/> class.
        /// </summary>
        /// <param name="name">The name of the launcher.</param>
        public Launcher(string name)
        {
            Name = name;
        }

        #region Fields and Properties

        private List<Fairing> fairings;
        private List<Capability> capabilities;
        private PayloadDimensions maxPayloadDimensions;

        /// <summary>
        /// Gets or sets this vehicle's fairing options.
        /// </summary>
        [DataMember]
        public List<Fairing> Fairings
        {
            get { return this.fairings ?? (this.fairings = new List<Fairing>()); }
            set
            {
                if (value == null) return;

                this.fairings = value;
                OnPropertyChanged(nameof(Fairings));
            }
        }

        /// <summary>
        /// Gets or sets this vehicle's launch capabilities.
        /// </summary>
        [DataMember]
        public List<Capability> Capabilities
        {
            get { return this.capabilities ?? (this.capabilities = new List<Capability>()); }
            set
            {
                if (value == null) return;

                this.capabilities = value;
                OnPropertyChanged(nameof(Capabilities));
            }
        }

        /// <summary>
        /// Gets or sets this vehicle's maximum payload dimensions.
        /// </summary>
        [DataMember]
        public PayloadDimensions MaxPayloadDimensions
        {
            get { return this.maxPayloadDimensions ?? (this.maxPayloadDimensions = new PayloadDimensions()); }
            set
            {
                if (value == null) return;

                this.maxPayloadDimensions = value;
                OnPropertyChanged(nameof(MaxPayloadDimensions));
            }
        }

        #region IVehicle Members

        private string name;
        private List<Note> notes;
        private List<PacificationOption> pacificationOptions;
        private decimal price;
        private string pictureLocation;
        private string saveFileLocation;
        private List<string> tags;

        /// <summary>
        /// Gets or sets the vehicle's name.
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
        /// Gets or sets the notes for this vehicle.
        /// </summary>
        [DataMember]
        public List<Note> Notes
        {
            get { return this.notes ?? (this.notes = new List<Note>()); }
            set
            {
                if (value == null) return;

                this.notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        /// <summary>
        /// Gets or sets the pacification options available for this vehicle.
        /// </summary>
        [DataMember]
        public List<PacificationOption> PacificationOptions
        {
            get { return this.pacificationOptions ?? (this.pacificationOptions = new List<PacificationOption>()); }
            set
            {
                if (value == null) return;

                this.pacificationOptions = value;
                OnPropertyChanged(nameof(PacificationOptions));
            }
        }

        /// <summary>
        /// Gets or sets the price of this vehicle.
        /// </summary>
        [DataMember]
        public decimal Price
        {
            get { return this.price; }
            set
            {
                this.price = value; 
                OnPropertyChanged(nameof(Price));
            }
        }

        /// <summary>
        /// Gets or sets where the preview picture of this vehicle is located.
        /// </summary>
        [DataMember]
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
        /// Gets or sets where the save file of this vehicle is located.
        /// </summary>
        [DataMember]
        public string SaveFileLocation
        {
            get { return this.saveFileLocation; }
            set
            {
                this.saveFileLocation = value; 
                OnPropertyChanged(nameof(SaveFileLocation));
            }
        }

        /// <summary>
        /// Gets or sets the tags that this vehicle contains.
        /// </summary>
        [DataMember]
        public List<string> Tags
        {
            get { return this.tags ?? (this.tags = new List<string>()); }
            set
            {
                if (value == null) return;

                this.tags = value;
                OnPropertyChanged(nameof(Tags));
            }
        }

        #endregion

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