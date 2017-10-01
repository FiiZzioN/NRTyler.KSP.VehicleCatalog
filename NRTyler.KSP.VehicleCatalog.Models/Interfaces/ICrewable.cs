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

using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
	/// <summary>
	/// Indicates that an <see cref="IVehicle"/> is capable of holding crew.
	/// </summary>
	public interface ICrewable : IVehicle
	{
        /// <summary>
        /// Gets or sets the number of crew aboard the vehicle.
        /// </summary>
        int NumberOfCrew { get; set; }

        /// <summary>
        /// Gets or sets the life support system the vehicle is using.
        /// </summary>
        LifeSupportSystem LifeSupportSystem { get; set; }
	}
}