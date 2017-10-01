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
	/// Life support items required for a crew member to survive.
	/// </summary>
	/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
	public class ProvisionsStorage : INotifyPropertyChanged
	{
		public ProvisionsStorage()
		{
			InitializeClass();
		}

		private double totalFoodStored;
		private double totalWaterStored;
		private double totalOxygenStored;
		private double totalElectricityStored;

		#region Properties

		/// <summary>
		/// Gets or sets the amount of food that can be stored.
		/// </summary>
		public double TotalFoodStored
		{
			get { return this.totalFoodStored; }
			set
			{
				if (value < 0) return;

				this.totalFoodStored = value;
				OnPropertyChanged(nameof(TotalFoodStored));
			}
		}

		/// <summary>
		/// Gets or sets the amount of water that can be stored.
		/// </summary>
		public double TotalWaterStored
		{
			get { return this.totalWaterStored; }
			set
			{
				if (value < 0) return;

				this.totalWaterStored = value;
				OnPropertyChanged(nameof(TotalWaterStored));
			}
		}

		/// <summary>
		/// Gets or sets the amount of oxygen that can be stored.
		/// </summary>
		public double TotalOxygenStored
		{
			get { return this.totalOxygenStored; }
			set
			{
				if (value < 0) return;

				this.totalOxygenStored = value;
				OnPropertyChanged(nameof(TotalOxygenStored));
			}
		}

		/// <summary>
		/// Gets or sets the amount of electricity that can be stored.
		/// </summary>
		public double TotalElectricityStored
		{
			get { return this.totalElectricityStored; }
			set
			{
				if (value < 0) return;

				this.totalElectricityStored = value;
				OnPropertyChanged(nameof(TotalElectricityStored));
			}
		}

		#endregion

		/// <summary>
		/// Initializes the class with default values.
		/// </summary>
		private void InitializeClass()
		{
			TotalFoodStored        = 0;
			TotalWaterStored       = 0;
			TotalOxygenStored      = 0;
			TotalElectricityStored = 0;
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