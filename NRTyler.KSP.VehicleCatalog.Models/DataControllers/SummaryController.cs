// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-20-2018
//
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
    /// <summary>
    /// Contains the methods that generate a <see cref="Summary"/> object for a <see cref="VehicleFamily"/> or <see cref="LauncherCollection"/>.
    /// This can also be used to generate the just data that a <see cref="Summary"/> object holds rather than returning a complete object.
    /// </summary>
    public class SummaryController
    {
        #region Summary Controllers

        /// <summary>
        /// Gets the <see cref="FairingSummaryController"/> so you can access the methods needed to get a <see cref="FairingSummary"/>.
        /// </summary>
        public FairingSummaryController FairingController { get; } = new FairingSummaryController();

        /// <summary>
        /// Gets the <see cref="PriceSummaryController"/> so you can access the methods needed to get a <see cref="PriceSummary"/>.
        /// </summary>
        public PriceSummaryController PriceController { get; } = new PriceSummaryController();

        /// <summary>
        /// Gets the <see cref="CapabilitySummaryController"/> so you can access the methods needed to get a <see cref="CapabilitySummary"/>.
        /// </summary>
        public CapabilitySummaryController CapabilityController { get; } = new CapabilitySummaryController();

        #endregion

        /// <summary>
        /// Gets a complete <see cref="Summary"/> object for the specified <see cref="VehicleFamily"/>. 
        /// </summary>
        /// <param name="vehicleFamily">The <see cref="VehicleFamily"/> to analyze.</param>
        public Summary GetCompleteSummary(VehicleFamily vehicleFamily)
        {       
            var numberOfVehicles  = GetNumberOfVehicles(vehicleFamily);
            var fairingSummary    = FairingController.GetFairingSummary(vehicleFamily);
            var priceSummary      = PriceController.GetPriceSummary(vehicleFamily);
            var capabilitySummary = CapabilityController.GetCapabilitySummary(vehicleFamily).ToList();

            return new Summary(numberOfVehicles, capabilitySummary, fairingSummary, priceSummary);
        }

        /// <summary>
        /// Gets a complete <see cref="Summary"/> object for the specified <see cref="LauncherCollection"/>.
        /// </summary>
        /// <param name="launcherCollection">The <see cref="LauncherCollection"/> to analyze.</param>
        public Summary GetCompleteSummary(LauncherCollection launcherCollection)
        {
            var numberOfVehicles  = launcherCollection.Launchers.Count;
            var fairingSummary    = FairingController.GetFairingSummary(launcherCollection.Launchers);
            var priceSummary      = PriceController.GetPriceSummary(launcherCollection.Launchers);
            var capabilitySummary = CapabilityController.GetCapabilitySummary(launcherCollection.Launchers).ToList();

            return new Summary(numberOfVehicles, capabilitySummary, fairingSummary, priceSummary);
        }

        /// <summary>
        /// Gets the number of launchers in a <see cref="VehicleFamily"/> and it's associated launcher collections.
        /// </summary>
        /// <param name="vehicleFamily">The <see cref="VehicleFamily"/> to analyze.</param>
        public int GetNumberOfVehicles(VehicleFamily vehicleFamily)
        {
            // Go through each LauncherCollection, get the number of
            // Launchers it's holding, and then add them to the total.
            var numberOfvehicles = vehicleFamily.LauncherCollections.Sum(e => e.Launchers.Count);

            // Get the number of Launchers in the family that don't belong 
            // to any specific collection, and then add them to the total.
            numberOfvehicles += vehicleFamily.Launchers.Count;

            return numberOfvehicles;
        }
    }
}