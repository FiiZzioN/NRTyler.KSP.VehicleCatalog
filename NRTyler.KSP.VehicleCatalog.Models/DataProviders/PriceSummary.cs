// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-26-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-28-2017
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
    /// Holds the cheapest and most expensive price that's found in a vehicle family.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "PriceSummary")]
    public class PriceSummary : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriceSummary"/> class.
        /// </summary>
        public PriceSummary() : this (0, 0)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriceSummary" /> class.
        /// </summary>
        /// <param name="cheapest">The cheapest price.</param>
        /// <param name="mostExpensive">The most expensive price.</param>
        public PriceSummary(decimal cheapest, decimal mostExpensive)
        {
            Cheapest      = cheapest;
            MostExpensive = mostExpensive;
        }

        private decimal cheapest;
        private decimal mostExpensive;

        /// <summary>
        /// Gets or sets the cheapest price found in the family.
        /// </summary>
        [DataMember]
        public decimal Cheapest
        {
            get { return this.cheapest; }
            set
            {
                if (value < 0) return;

                this.cheapest = value;
                OnPropertyChanged(nameof(Cheapest));
            }
        }

        /// <summary>
        /// Gets or sets the most expensive price found in the family.
        /// </summary>
        [DataMember]
        public decimal MostExpensive
        {
            get { return this.mostExpensive; }
            set
            {
                if (value < 0) return;

                this.mostExpensive = value;
                OnPropertyChanged(nameof(MostExpensive));
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