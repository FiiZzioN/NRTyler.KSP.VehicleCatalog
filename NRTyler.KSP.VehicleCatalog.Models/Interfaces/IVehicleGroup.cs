// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 12-27-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-27-2017
// 
// License          : MIT License
// ***********************************************************************

using System.Collections;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
    public interface IVehicleGroup : INotepad, IPreview
    {
        /// <summary>
        /// Gets or sets the name of the vehicle group.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the summary for this vehicle group.
        /// </summary>
        Summary Summary { get; set; }

        /// <summary>
        /// Gets or sets the vehicle versions in this group.
        /// </summary>
        List<object> VehicleVersions { get; set; }
    }
}