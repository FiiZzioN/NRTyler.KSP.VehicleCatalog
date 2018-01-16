// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-16-2018
//
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
    /// <summary>
    /// Contains the methods that a <see cref="Summary"/> object uses in order to fill its fields.
    /// </summary>
    public class SummaryController
    {
        #region Summary Controllers

        /// <summary>
        /// Gets the fairing summary controller so you access the methods required to get a fairing summary.
        /// </summary>
        private FairingSummaryController FairingController { get; } = new FairingSummaryController();
        /// <summary>
        /// Gets the price summary controller so you access the methods required to get a price summary.
        /// </summary>
        private PriceSummaryController PriceController { get; } = new PriceSummaryController();
        /// <summary>
        /// Gets the capability summary controller so you access the methods required to get a capability summary.
        /// </summary>
        private CapabilitySummaryController CapabilityController { get; } = new CapabilitySummaryController(); 

        #endregion

        /// <summary>
        /// Gets a complete <see cref="Summary"/> for the specified <see cref="VehicleFamily"/>.
        /// </summary>
        /// <param name="vehicleFamily">The <see cref="VehicleFamily"/> to analyze.</param>
        public Summary GetFamilySummary(VehicleFamily vehicleFamily)
        {       
            var numberOfVersions  = GetNumberOfVersions(vehicleFamily);
            var fairingSummary    = FairingController.GetFairingSummary(vehicleFamily);
            var priceSummary      = PriceController.GetPriceSummary(vehicleFamily);
            var capabilitySummary = CapabilityController.GetCapabilitySummary(vehicleFamily).ToList();

            return new Summary(numberOfVersions, capabilitySummary, fairingSummary, priceSummary);
        }

        /// <summary>
        /// Gets a complete <see cref="Summary"/> for the specified <see cref="LauncherCollection"/>.
        /// </summary>
        /// <param name="launcherCollection">The <see cref="LauncherCollection"/> to analyze.</param>
        public Summary GetCollectionSummary(LauncherCollection launcherCollection)
        {
            var numberOfVersions  = launcherCollection.Launchers.Count;
            var fairingSummary    = FairingController.GetFairingSummary(launcherCollection.Launchers);
            var priceSummary      = PriceController.GetPriceSummary(launcherCollection.Launchers);
            var capabilitySummary = CapabilityController.GetCapabilitySummary(launcherCollection.Launchers).ToList();

            return new Summary(numberOfVersions, capabilitySummary, fairingSummary, priceSummary);
        }

        /// <summary>
        /// Gets the number of launchers in a <see cref="VehicleFamily"/> and it's associated collection of <see cref="LauncherCollection"/>.
        /// </summary>
        /// <param name="vehicleFamily">The <see cref="VehicleFamily"/> to analyze.</param>
        public int GetNumberOfVersions(VehicleFamily vehicleFamily)
        {
            var collectionCount = vehicleFamily.LauncherCollection.Count;
            var launcherCount   = vehicleFamily.Launchers.Count;

            return collectionCount + launcherCount;
        }

        #region Helpers

        /// <summary>
        /// Gets the larger value of the two items.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object to compare. Must be a <see langword="struct"/>
        /// and implement the <see cref="IComparable{T}"/> interface.
        /// </typeparam>
        /// <param name="itemOne">The first item to compare.</param>
        /// <param name="itemTwo">The second item to compare.</param>
        public static T GetLargerValue<T>(T itemOne, T itemTwo) where T : struct, IComparable<T>
        {
            // Greater Than Zero = This current instance follows the object specified by the CompareTo method argument in the sort order.
            return itemOne.CompareTo(itemTwo) > 0 ? itemOne : itemTwo; ;
        }

        /// <summary>
        /// Gets the smaller value of the two items.
        /// </summary>
        /// <typeparam name="T">
        /// The type of object to compare. Must be a <see langword="struct"/>
        /// and implement the <see cref="IComparable{T}"/> interface.
        /// </typeparam>
        /// <param name="itemOne">The first item to compare.</param>
        /// <param name="itemTwo">The second item to compare.</param>
        public static T GetSmallerValue<T>(T itemOne, T itemTwo) where T : struct, IComparable<T>
        {
            // Less Than Zero = This object precedes the object specified by the CompareTo method in the sort order.
            return itemOne.CompareTo(itemTwo) < 0 ? itemOne : itemTwo; ;
        } 

        #endregion
    }
}