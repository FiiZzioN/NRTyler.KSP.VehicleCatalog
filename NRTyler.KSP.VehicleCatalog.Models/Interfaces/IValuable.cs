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
	/// Applies to <see cref="object"/>'s that have value, such as vehicles and optional stages.
	/// </summary>
	public interface IValuable
	{
		/// <summary>
		/// Gets or sets the price of an object.
		/// </summary>
		decimal Price { get; set; }
	}
}