// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 12-24-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-24-2017
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
    /// <summary>
    /// Indicates that an <see cref="object"/> has various pacification options 
    /// once it's final stage has done its job, or it has completed its lifespan.
    /// </summary>
    public interface IPacifiable
    {
        /// <summary>
        /// Gets or sets the pacification options available for this <see cref="object"/>.
        /// </summary>
        PacificationOption PacificationOptions { get; set; }
    }
}