// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-23-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-26-2017
//
// License          : MIT License
// ***********************************************************************

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
    /// <summary>
    /// Indicates that an <see cref="object"/> has the ability to show a preview picture.
    /// </summary>
    public interface IPreview
    {
        /// <summary>
        /// Gets or sets where the preview picture of this <see cref="object"/> is located.
        /// </summary>
        string PreviewLocation { get; set; }
    }
}