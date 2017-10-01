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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.Settings
{
    /// <summary>
    /// Holds information about how many units are used / generated of various life support systems per day. 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class LifeSupportSettings : INotifyPropertyChanged, IEnumerable<double>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="LifeSupportSettings"/> class.
		/// </summary>
		public LifeSupportSettings()
		{
			InitializeClass();
		}

		private double foodPerDay;
		private double waterPerDay;
		private double oxygenPerDay;
		private double baseElectricityPerDay;
		private double kerbalElectricityPerDay;

		private double wastePerDay;
		private double wasteWaterPerDay;
		private double co2PerDay;

		#region Properties

		/// <summary>
		/// Gets or sets the units of food consumed per day.
		/// </summary>
		public double FoodPerDay
		{
			get { return this.foodPerDay; }
			set
			{
				if (value < 0) return;

				this.foodPerDay = value;
				OnPropertyChanged(nameof(FoodPerDay));
			}
		}

		/// <summary>
		/// Gets or set the units of water consumed per day.
		/// </summary>
		public double WaterPerDay
		{
			get { return this.waterPerDay; }
			set
			{
				if (value < 0) return;

				this.waterPerDay = value;
				OnPropertyChanged(nameof(WaterPerDay));
			}
		}

		/// <summary>
		/// Gets or sets the units of oxygen consumed per day.
		/// </summary>
		public double OxygenPerDay
		{
			get { return this.oxygenPerDay; }
			set
			{
				if (value < 0) return;

				this.oxygenPerDay = value;
				OnPropertyChanged(nameof(OxygenPerDay));
			}
		}

		/// <summary>
		/// Gets or sets the baseline units of electricity consumed per day.
		/// </summary>
		public double BaseElectricityPerDay
		{
			get { return this.baseElectricityPerDay; }
			set
			{
				if (value < 0) return;

				this.baseElectricityPerDay = value;
				OnPropertyChanged(nameof(BaseElectricityPerDay));
			}
		}

		/// <summary>
		/// Gets or sets the units of electricity a kerbal consumes per day.
		/// </summary>
		public double KerbalElectricityPerDay
		{
			get { return this.kerbalElectricityPerDay; }
			set
			{
				if (value < 0) return;

				this.kerbalElectricityPerDay = value;
				OnPropertyChanged(nameof(KerbalElectricityPerDay));
			}
		}

		/// <summary>
		/// Gets or sets the units of waste produced per day.
		/// </summary>
		public double WastePerDay
		{
			get { return this.wastePerDay; }
			set
			{
				if (value < 0) return;

				this.wastePerDay = value;
				OnPropertyChanged(nameof(WaterPerDay));
			}
		}

		/// <summary>
		/// Gets or sets the units of waste water produced per day.
		/// </summary>
		public double WasteWaterPerDay
		{
			get { return this.wasteWaterPerDay; }
			set
			{
				if (value < 0) return;

				this.wasteWaterPerDay = value;
				OnPropertyChanged(nameof(WasteWaterPerDay));
			}
		}

		/// <summary>
		/// Gets or sets the units of CO2 produced per day.
		/// </summary>
		public double CO2PerDay
		{
			get { return this.co2PerDay; }
			set
			{
				if (value < 0) return;

				this.co2PerDay = value;
				OnPropertyChanged(nameof(this.co2PerDay));
			}
		}

		#endregion

		/// <summary>
		/// Initializes the class with default values.
		/// </summary>
		private void InitializeClass()
		{
			/*
			var sixHours = BaseGameSettings.DefaultHoursInKerbinDay;

			FoodPerDay              = 0.000016927083333 * sixHours;
			WaterPerDay             = 0.000011188078704 * sixHours;
			OxygenPerDay            = 0.001713537562385 * sixHours;
			BaseElectricityPerDay   = 0.02125           * sixHours;
			KerbalElectricityPerDay = 0.014166666666667 * sixHours;
			WastePerDay             = 0.0000015393519   * sixHours;
			WasteWaterPerDay        = 0.000014247685185 * sixHours;
			CO2PerDay               = 0.00148012889876  * sixHours;
			*/

			FoodPerDay              = 0.365625;
			WaterPerDay             = 0.241663;
			OxygenPerDay            = 37.01241;
			BaseElectricityPerDay   = 459;
			KerbalElectricityPerDay = 306;
			WastePerDay             = 0.03325 ;
			WasteWaterPerDay        = 0.30775 ;
			CO2PerDay               = 31.97079;			
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

		#region Implementation of IEnumerable

		/// <summary>Returns an enumerator that iterates through the collection.</summary>
		/// <returns>An enumerator that can be used to iterate through the collection.</returns>
		public IEnumerator<double> GetEnumerator()
		{
			var resourceArray = new []
			{
				FoodPerDay,
				WaterPerDay,
				OxygenPerDay,
				BaseElectricityPerDay,
				KerbalElectricityPerDay,
				WastePerDay,
				WasteWaterPerDay,
				CO2PerDay
			};

			foreach (var resource in resourceArray)
			{
				yield return resource;
			}
		}

		/// <summary>Returns an enumerator that iterates through a collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}