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
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Holds information containing what a launch vehicle can lift and where it can place its payload.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "Capability")]
	public class Capability : INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Capability"/> class.
		/// </summary>
		/// <param name="payloadRange">The payload weight range.</param>
		/// <param name="trajectory">The trajectory parameters.</param>
		public Capability(PayloadRange payloadRange, Trajectory trajectory)
		{
			PayloadRange = payloadRange;
			Trajectory   = trajectory;
		}

		private PayloadRange payloadRange;
		private Trajectory trajectory;

		/// <summary>
		/// Gets or sets the payload weight range.
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

        /// <summary>
        /// Gets or sets the trajectory parameters.
        /// </summary>
        [DataMember]
        public Trajectory Trajectory
		{
			get { return this.trajectory; }
			set
			{
			    if (value == null) return;

                this.trajectory = value;
			    OnPropertyChanged(nameof(Trajectory));
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