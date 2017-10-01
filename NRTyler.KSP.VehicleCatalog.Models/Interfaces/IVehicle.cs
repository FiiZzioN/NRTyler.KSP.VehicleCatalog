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
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
	/// <summary>
	/// Contains items that any type of vehicle should have.
	/// </summary>
	/// <seealso cref="IValuable" />
	public interface IVehicle : IHasDeltaV, IValuable
	{
		/// <summary>
		/// Gets or sets the vehicle's the name.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// Gets or sets the vehicle's dry mass.
		/// </summary>
		double DryMass { get; set; }

		/// <summary>
		/// Gets or sets vehicle's the wet mass.
		/// </summary>
		double WetMass { get; set; }

		/// <summary>
		/// Gets or sets the vehicle's stage information.
		/// </summary>
		SortedDictionary<int, Stage> StageInfo { get; set; }

		/// <summary>
		/// Gets or sets the vehicles notes.
		/// </summary>
		List<VehicleNote> VehicleNotes { get; set; }

		/// <summary>
		/// Gets or sets the type of the vehicle.
		/// </summary>
		VehicleType VehicleType { get; set; }
	}
}