// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-01-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
	[Serializable]
	public class Payload : INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Payload"/> class.
		/// </summary>
		/// <param name="vehicle">The vehicle that is the payload for this mission.</param>
		public Payload(IVehicle vehicle)
		{
			Vehicle = vehicle;
		}

		private IVehicle vehicle;

		/// <summary>
		/// Gets or sets the vehicle that is the payload for this mission.
		/// </summary>
		public IVehicle Vehicle
		{
			get { return this.vehicle; }
			set
			{
				this.vehicle = value;
				OnPropertyChanged(nameof(Vehicle));
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