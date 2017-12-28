// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-24-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-26-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Holds the various dimensions of a <see cref="Payload"/>.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "PayloadDimensions")]
    public class PayloadDimensions : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadDimensions"/> class.
        /// </summary>
        public PayloadDimensions() : this(0, 0)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadDimensions"/> class.
        /// </summary>
        /// <param name="length">The length of the <see cref="Payload"/> in meters.</param>
        /// <param name="diameter">The diameter of the <see cref="Payload"/> in meters.</param>
        public PayloadDimensions(double length, double diameter)
        {
            Length   = length;
            Diameter = diameter;
        }
        
        private double length;
        private double diameter;

        /// <summary>
        /// Gets or sets the length of the <see cref="Payload"/> in meters.
        /// </summary>
        [DataMember]
        public double Length
        {
            get { return this.length; }
            set
            {
                if (value < 0) return;

                this.length = value; 
                OnPropertyChanged(nameof(Length));
            }
        }

        /// <summary>
        /// Gets or sets the diameter of the <see cref="Payload"/> in meters.
        /// </summary>
        [DataMember]
        public double Diameter
        {
            get { return this.diameter; }
            set
            {
                if (value < 0) return;

                this.diameter = value; 
                OnPropertyChanged(nameof(Diameter));
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