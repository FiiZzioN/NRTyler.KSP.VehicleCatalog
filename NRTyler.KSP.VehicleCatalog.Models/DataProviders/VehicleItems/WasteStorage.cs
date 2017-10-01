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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems
{
	/// <summary>
	/// Life support items required for a crew member's natural waste.
	/// </summary>
	/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
	public class WasteStorage : INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="WasteStorage"/> class.
		/// </summary>
		public WasteStorage()
		{
			InitializeClass();
		}

		private double wasteUnits;
		private double wasteWaterUnits;
		private double co2Units;

		#region Properties

		/// <summary>
		/// Gets or sets the amount of waste that can be stored.
		/// </summary>
		public double WasteUnits
		{
			get { return this.wasteUnits; }
			set
			{
				if (value < 0) return;

				this.wasteUnits = value;
				OnPropertyChanged(nameof(WasteUnits));
			}
		}

		/// <summary>
		/// Gets or sets the amount of waste water that can be stored.
		/// </summary>
		public double WasteWaterUnits
		{
			get { return this.wasteWaterUnits; }
			set
			{
				if (value < 0) return;

				this.wasteWaterUnits = value;
				OnPropertyChanged(nameof(WasteWaterUnits));
			}
		}

		/// <summary>
		/// Gets or sets the amount of CO2 that can be stored.
		/// </summary>
		public double CO2Units
		{
			get { return this.co2Units; }
			set
			{
				if (value < 0) return;

				this.co2Units = value;
				OnPropertyChanged(nameof(CO2Units));
			}
		}

		#endregion

		/// <summary>
		/// Initializes the class with default values.
		/// </summary>
		private void InitializeClass()
		{
			WasteUnits = 0;
			WasteWaterUnits = 0;
			CO2Units = 0;
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