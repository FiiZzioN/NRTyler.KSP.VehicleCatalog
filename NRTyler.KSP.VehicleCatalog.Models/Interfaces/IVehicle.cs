// ***********************************************************************
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
    /// Contains items that any vehicle in the catalog should have.
    /// </summary>
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.IValuable" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.IPreview" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.INotepad" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.IPacifiable" />
    /// <seealso cref="NRTyler.KSP.VehicleCatalog.Models.Interfaces.ITaggable" />
    /// <seealso cref="IValuable" />
    public interface IVehicle : IValuable, IPreview, INotepad, IPacifiable, ITaggable
	{
		/// <summary>
		/// Gets or sets the vehicle's name.
		/// </summary>
		string Name { get; set; }

	    /// <summary>
	    /// Gets or sets where the save file of this <see cref="object"/> is located.
	    /// </summary>
        string SaveFileLocation { get; set; }

    }
}