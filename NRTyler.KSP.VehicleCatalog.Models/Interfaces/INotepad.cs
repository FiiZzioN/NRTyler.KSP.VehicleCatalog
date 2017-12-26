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
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
    /// <summary>
    /// Indicates that an <see cref="object"/> can contain multiple notes for a later date.
    /// </summary>
    public interface INotepad
    {
        /// <summary>
        /// Gets or sets the notes for this <see cref="object"/>.
        /// </summary>
        List<Note> Notes { get; set; }
    }
}