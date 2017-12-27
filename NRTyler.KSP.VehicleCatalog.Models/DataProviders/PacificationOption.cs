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
using NRTyler.KSP.Common.Enums;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Contains pacification information for a vehicle's final stage of a payload once it's passed its lifespan.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "PacificationOption")]
	public class PacificationOption : INotifyPropertyChanged
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PacificationOption"/> class.
		/// </summary>
		public PacificationOption() : this (PacificationType.Undefined, 0)
		{

		}

        /// <summary>
        /// Initializes a new instance of the <see cref="PacificationOption" /> class.
        /// </summary>
        /// <param name="pacificationType">The pacification type.</param>
        /// <param name="requiredDeltaV">The required amount of delta-v to accomplish this pacification type.</param>
        public PacificationOption(PacificationType pacificationType, double requiredDeltaV)
		{
		    PacificationType = pacificationType;
		    RequiredDeltaV   = requiredDeltaV;
		}

		#endregion

		private PacificationType pacificationType;
	    private double requiredDeltaV;

        /// <summary>
        /// Gets or sets the pacification type.
        /// </summary>
        /// <value>The type of the pacification.</value>
        [DataMember]
		public PacificationType PacificationType
		{
			get { return this.pacificationType; }
			set
			{
				this.pacificationType = value;
				OnPropertyChanged(nameof(PacificationType));
			}
		}

        /// <summary>
        /// Gets or sets the required amount of delta-v to accomplish this pacification type.
        /// </summary>
        [DataMember]
        public double RequiredDeltaV
	    {
	        get { return this.requiredDeltaV; }
	        set
	        {
	            if (value < 0) return;

	            this.requiredDeltaV = value;
	            OnPropertyChanged(nameof(RequiredDeltaV));
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
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}