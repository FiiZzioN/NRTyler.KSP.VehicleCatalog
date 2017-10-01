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
	/// <summary>
	/// Indicates that a given type can execute or holds information about a maneuver.
	/// </summary>
	public interface IManeuver
	{
		double RequiredDeltaV { get; set; }
	}
}