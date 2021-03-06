﻿// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-26-2017
//
// License          : MIT License
// ***********************************************************************

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
	/// <summary>
	/// Indicates that an <see cref="object"/> is worth a various amount of currency.
	/// </summary>
	public interface IValuable
	{
		/// <summary>
		/// Gets or sets the price of this <see cref="object"/>.
		/// </summary>
		decimal Price { get; set; }
	}
}