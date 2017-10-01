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

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
	public interface IHasDeltaV
	{
		/// <summary>
		/// Gets or sets the delta-v.
		/// </summary>
		double DeltaV { get; set; }

		/// <summary>
		/// Calculates the delta-v.
		/// </summary>
		/// <returns>System.Double.</returns>
		double CalculateDeltaV();
	}
}