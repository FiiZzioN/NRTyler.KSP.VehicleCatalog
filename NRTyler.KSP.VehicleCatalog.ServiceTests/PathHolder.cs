// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
// 
// Author           : Nicholas Tyler
// Created          : 01-23-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-23-2018
// 
// License          : MIT License
// ***********************************************************************

namespace NRTyler.KSP.VehicleCatalog.ServiceTests
{
    /// <summary>
    /// A static class made so people making unit tests have quick 
    /// access to directory and XML file paths that are used repeatedly.
    /// </summary>
    internal static class PathHolder
    {
        /// <summary>
        /// Gets or sets the path to a directory.
        /// </summary>
        internal static string DirectoryPath { get; set; }

        /// <summary>
        /// Gets or sets the path to an XML file.
        /// </summary>
        internal static string XMLPath { get; set; }
    }
}