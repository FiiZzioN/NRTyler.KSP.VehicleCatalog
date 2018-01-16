// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-26-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-12-2018
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
    /// Holds the dimensions of the largest fairing in a vehicle family.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "FairingSummary")]
    public class FairingSummary : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FairingSummary"/> class.
        /// </summary>
        public FairingSummary() : this (0, 0)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FairingSummary"/> class.
        /// </summary>
        /// <param name="maxLength">The value of the largest fairing, length-wise, in a vehicle family.</param>
        /// <param name="maxDiameter">The value of the largest fairing, diameter-wise, in a vehicle family.</param>
        public FairingSummary(double maxLength, double maxDiameter)
        {
            MaxLength   = maxLength;
            MaxDiameter = maxDiameter;
        }

        private double maxLength;
        private double maxDiameter;

        /// <summary>
        /// Gets or sets the value of the largest fairing, length-wise, in a vehicle family.
        /// </summary>
        [DataMember]
        public double MaxLength
        {
            get { return this.maxLength; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.maxLength = value; 
                OnPropertyChanged(nameof(MaxLength));
            }
        }

        /// <summary>
        /// Gets or sets the value of the largest fairing, diameter-wise, in a vehicle family.
        /// </summary>
        [DataMember]
        public double MaxDiameter
        {
            get { return this.maxDiameter; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.maxDiameter = value; 
                OnPropertyChanged(nameof(MaxDiameter));
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