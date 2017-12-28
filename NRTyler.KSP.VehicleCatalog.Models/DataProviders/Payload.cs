// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-26-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// The item that a launch vehicle inserts into an orbit.
    /// </summary>
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.IVehicle" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
	[DataContract(Name = "Payload")]
    public class Payload : IVehicle, INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Payload"/> class.
		/// </summary>
		public Payload() : this("Name Not Specified")
        {
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Payload"/> class.
        /// </summary>
        /// <param name="name">The name of the payload.</param>
        public Payload(string name)
	    {
	        Name = name;
	        VehicleType = VehicleType.Undefined;
	    }

        #region Fields and Properties

	    private int mass;
	    private double deltav;
	    private VehicleType vehicleType;
	    private PayloadDimensions dimensions;

        /// <summary>
        /// Gets or sets the wet mass of the payload.
        /// </summary>
        [DataMember]
        public int Mass
	    {
	        get { return this.mass; }
            set
            {
                this.mass = value; 
                OnPropertyChanged(nameof(Mass));
            }
	    }

        /// <summary>
        /// Gets or sets the payload's delta-v.
        /// </summary>
        [DataMember]
        public double DeltaV
	    {
	        get { return this.deltav; }
            set
            {
                this.deltav = value; 
                OnPropertyChanged(nameof(DeltaV));
            }
	    }

        /// <summary>
        /// Gets or sets the type of the vehicle.
        /// </summary>
        [DataMember]
        public VehicleType VehicleType
	    {
	        get { return this.vehicleType; }
            set
            {
                this.vehicleType = value; 
                OnPropertyChanged(nameof(VehicleType));
            }
	    }

        /// <summary>
        /// Gets or sets this payload's dimensions.
        /// </summary>
        [DataMember]
        public PayloadDimensions Dimensions
        {
            get { return this.dimensions ?? (this.dimensions = new PayloadDimensions()); }
            set
            {
                if (value == null) return;

                this.dimensions = value;
                OnPropertyChanged(nameof(Dimensions));
            }
        }

        #region IVehicle Members

        private string name;
        private List<Note> notes;
        private List<PacificationOption> pacificationOptions;
        private decimal price;
        private string previewLocation;
        private string craftFileLocation;
        private List<string> tags;

        /// <summary>
        /// Gets or sets the payload's name.
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
        /// Gets or sets the notes for this payload.
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
        /// Gets or sets the pacification options available for this payload.
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
        /// Gets or sets the price of this payload.
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
        /// Gets or sets where the preview picture of this payload is located.
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
        /// Gets or sets where the craft file of this payload is located.
        /// </summary>
        [DataMember]
        public string CraftFileLocation
        {
            get { return this.craftFileLocation; }
            set
            {
                this.craftFileLocation = value;
                OnPropertyChanged(nameof(CraftFileLocation));
            }
        }

        /// <summary>
        /// Gets or sets the tags that this payload contains.
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