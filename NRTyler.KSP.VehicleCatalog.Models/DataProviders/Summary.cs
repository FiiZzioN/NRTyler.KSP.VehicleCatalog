﻿// ***********************************************************************
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Holds various summaries about a given vehicle family.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "Summary")]
    public class Summary : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Summary"/> class.
        /// </summary>
        public Summary() : this(0, new List<CapabilitySummary>(), new FairingSummary(), new PriceSummary())
        {
        }

        public Summary(int numberOfVersions, List<CapabilitySummary> payloadRangeSummary, FairingSummary fairingSummary, PriceSummary priceSummary)
        {
            NumberOfVerisons    = numberOfVersions;
            PayloadRangeSummary = payloadRangeSummary;
            FairingSummary      = fairingSummary;
            PriceSummary        = priceSummary;
        }

        private int numberOfVersions;
        private List<CapabilitySummary> payloadRangeSummary;
        private FairingSummary fairingSummary;
        private PriceSummary priceSummary;

        /// <summary>
        /// Gets or sets the number of vehicle versions in the vehicle family.
        /// </summary>
        [DataMember]
        public int NumberOfVerisons
        {
            get { return this.numberOfVersions; }
            set
            {
                this.numberOfVersions = value;
                OnPropertyChanged(nameof(NumberOfVerisons));
            }
        }

        /// <summary>
        /// Gets or sets the payload range summary for the vehicle family.
        /// </summary>
        [DataMember]
        public List<CapabilitySummary> PayloadRangeSummary
        {
            get { return this.payloadRangeSummary; }
            set
            {
                if (value == null) return;

                this.payloadRangeSummary = value;
                OnPropertyChanged(nameof(PayloadRangeSummary));
            }
        }

        /// <summary>
        /// Gets or sets the fairing summary for the vehicle family.
        /// </summary>
        [DataMember]
        public FairingSummary FairingSummary
        {
            get { return this.fairingSummary; }
            set
            {
                if (value == null) return;

                this.fairingSummary = value;
                OnPropertyChanged(nameof(FairingSummary));
            }
        }

        /// <summary>
        /// Gets or sets the price summary for the vehicle family.
        /// </summary>
        [DataMember]
        public PriceSummary PriceSummary
        {
            get { return this.priceSummary; }
            set
            {
                if (value == null) return;

                this.priceSummary = value;
                OnPropertyChanged(nameof(PriceSummary));
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