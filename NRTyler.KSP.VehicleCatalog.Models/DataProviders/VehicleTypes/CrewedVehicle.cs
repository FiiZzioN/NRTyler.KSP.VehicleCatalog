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

using System.Collections.Generic;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.Settings;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleTypes
{
	public class CrewedVehicle : Vehicle, ICrewable
	{
		public CrewedVehicle()
		{
			VehicleType  = VehicleType.Undefined;
			StageInfo    = new SortedDictionary<int, Stage>();
			VehicleNotes = new List<VehicleNote>();
		}

		public CrewedVehicle(BaseGameSettings baseSettings, LifeSupportSettings supportSettings) : this()
		{
			LifeSupportSystem   = new LifeSupportSystem(this, baseSettings, supportSettings);
		}

		#region Backing Fields

		private int numberOfCrew;
		private LifeSupportSystem lifeSupportSystem;

		#endregion

		#region Implementation of ICrewable

		/// <summary>
		/// Gets or sets the number of crew on board the vehicle.
		/// </summary>
		public int NumberOfCrew
		{
			get { return this.numberOfCrew; }
			set
			{
				if (value < 0) return;

				this.numberOfCrew = value;
				OnPropertyChanged(nameof(NumberOfCrew));
			}
		}

		/// <summary>
		/// Gets or sets information about the life support system.
		/// </summary>
		public LifeSupportSystem LifeSupportSystem
		{
			get { return this.lifeSupportSystem; }
			set
			{
				this.lifeSupportSystem = value;
				OnPropertyChanged(nameof(LifeSupportSystem));
			}
		}


		#endregion
	}
}