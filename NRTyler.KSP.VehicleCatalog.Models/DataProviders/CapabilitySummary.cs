// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-26-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-16-2018
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.Common.Enums;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Holds the weight range that a payload can be for a given orbit type. This is primarily 
    /// used when generating a summary about a given launcher collection or vehicle family.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "PayloadRangeSummary")]
    public class CapabilitySummary : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CapabilitySummary"/> class.
        /// </summary>
        public CapabilitySummary() : this(OrbitType.Undefined, new PayloadRange())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CapabilitySummary"/> class.
        /// </summary>
        /// <param name="orbitType">The orbit type this summary represents.</param>
        /// <param name="payloadRange">The weight range that a payload can be for this orbit type.</param>
        public CapabilitySummary(OrbitType orbitType, PayloadRange payloadRange)
        {
            OrbitType    = orbitType;
            PayloadRange = payloadRange;
        }

        private OrbitType orbitType;
        private PayloadRange payloadRange;

        /// <summary>
        /// Gets or sets the orbit type this summary represents.
        /// </summary>
        [DataMember]
        public OrbitType OrbitType
        {
            get { return this.orbitType; }
            set
            {
                this.orbitType = value;
                OnPropertyChanged(nameof(OrbitType));
            }
        }

        /// <summary>
        /// Gets or sets the weight range that a payload can be for this orbit type.
        /// </summary>
        [DataMember]
        public PayloadRange PayloadRange
        {
            get { return this.payloadRange; }
            set
            {
                if (value == null) return;

                this.payloadRange = value;
                OnPropertyChanged(nameof(PayloadRange));
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