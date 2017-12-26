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

        public Launcher(string name)
        {
            Name = name;
        }

        #region Fields and Properties

        private string name;
        private List<Note> notes;
        private PacificationOption pacificationOptions;
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
                this.notes = value;
                OnPropertyChanged(nameof(Notes));
            }
        }

        /// <summary>
        /// Gets or sets the pacification options available for this vehicle.
        /// </summary>
        [DataMember]
        public PacificationOption PacificationOptions
        {
            get { return this.pacificationOptions; }
            set
            {
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
                this.tags = value;
                OnPropertyChanged(nameof(Tags));
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