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

using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
    /// <summary>
    /// Indicates that an <see cref="object"/> can contain various strings for easy search functionality.
    /// </summary>
    public interface ITaggable
    {
        /// <summary>
        /// Gets or sets the tags that this <see cref="object"/> contains.
        /// </summary>
        List<string> Tags { get; set; }
    }
}