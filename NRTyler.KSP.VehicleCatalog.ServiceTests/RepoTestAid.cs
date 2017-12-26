// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ServiceTests
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-23-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.ServiceTests
{
    public static class RepoTestAid
    {
        /// <summary>
        /// Gets the default file name.
        /// </summary>
        public static string FileName { get; } = "Default.veh";

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        public static ApplicationSettings AppSettings { get; } = new ApplicationSettings();

        /// <summary>
        /// Returns the vehicle file location plus the default vehicle file name.
        /// </summary>
        /// <returns>The path.</returns>
        public static string VehiclePathDefault()
        {
            return $"{AppSettings.VehicleFileLocation}/{FileName}";
        }

        /// <summary>
        /// Returns the vehicle file location plus a customized file name.
        /// </summary>
        /// <returns>The path.</returns>
        public static string VehiclePathPlusName(string name)
        {
            return $"{AppSettings.VehicleFileLocation}/{name}.veh";
        }
    }
}