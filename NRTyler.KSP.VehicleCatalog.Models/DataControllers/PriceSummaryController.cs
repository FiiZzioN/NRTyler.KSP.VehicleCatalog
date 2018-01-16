// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 01-16-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-16-2018
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
    /// <summary>
    /// Holds methods that aid in getting a <see cref="PriceSummary"/> from various collections or vehicle families.
    /// </summary>
    public class PriceSummaryController
    {
        /// <summary>
        /// Gets the price of the cheapest and most expensive launcher in a vehicle family, 
        /// and then returns them in the form of a <see cref="PriceSummary"/>.
        /// </summary>
        /// <param name="vehicleFamily">The vehicle family to analyze.</param>
        /// <returns>
        /// A <see cref="PriceSummary"/> containing the price of the cheapest and 
        /// most expensive launcher in a vehicle family.
        /// </returns>
        public PriceSummary GetPriceSummary(VehicleFamily vehicleFamily)
        {
            // Get the summaries.
            var launcherPriceSummary = GetPriceSummary(vehicleFamily.Launchers);
            var collectionPriceSummary = GetPriceSummary(vehicleFamily.LauncherCollection);

            // Get the cheapest and most expensive of the two summaries in the same categories.
            var cheapest      = SummaryController.GetSmallerValue(launcherPriceSummary.Cheapest, collectionPriceSummary.Cheapest);
            var mostExpensive = SummaryController.GetLargerValue(launcherPriceSummary.MostExpensive, collectionPriceSummary.MostExpensive);

            return new PriceSummary(cheapest, mostExpensive);
        }

        /// <summary>
        /// Gets the price of the cheapest and most expensive launcher in a vehicle family, 
        /// and then returns them in the form of a <see cref="PriceSummary"/>.
        /// </summary>
        /// <param name="launcherCollections">An enumerable collection of type <see cref="LauncherCollection"/>.</param>
        /// <returns>
        /// A <see cref="PriceSummary"/> containing the price of the cheapest and 
        /// most expensive launcher in a vehicle family.
        /// </returns>
        public PriceSummary GetPriceSummary(IEnumerable<LauncherCollection> launcherCollections)
        {
            // The reason for this is 'cheapest' can only go lower and 'mostExpensive' can only go higher.
            var cheapest      = decimal.MaxValue;
            var mostExpensive = decimal.MinValue;

            foreach (var launcherCollection in launcherCollections)
            {
                // Get's the price summary for the version.
                var priceSummary = GetPriceSummary(launcherCollection.Launchers);

                // Check to make sure the values returned are cheaper or more expensive. 
                // If they are, update the values, otherwise we continue checking the collection.
                cheapest      = SummaryController.GetSmallerValue(priceSummary.Cheapest, cheapest);
                mostExpensive = SummaryController.GetLargerValue(priceSummary.MostExpensive, mostExpensive);
            }

            return new PriceSummary(cheapest, mostExpensive);
        }

        /// <summary>
        /// Gets the price of the cheapest and most expensive launcher in a launcher collection, 
        /// and then returns them in the form of a <see cref="PriceSummary"/>.
        /// </summary>
        /// <param name="launcherCollection">An enumerable collection of type <see cref="Launcher"/>.</param>
        /// <returns>
        /// A <see cref="PriceSummary"/> containing the price of the cheapest and 
        /// most expensive launcher in a Launcher collection.
        /// </returns>
        public PriceSummary GetPriceSummary(IEnumerable<Launcher> launcherCollection)
        {
            // The reason for this is 'cheapest' can only go lower and 'mostExpensive' can only go higher.
            var cheapest      = decimal.MaxValue;
            var mostExpensive = decimal.MinValue;

            foreach (var launcher in launcherCollection)
            {
                cheapest      = SummaryController.GetSmallerValue(launcher.Price, cheapest);
                mostExpensive = SummaryController.GetLargerValue(launcher.Price, mostExpensive);
            }

            return new PriceSummary(cheapest, mostExpensive);
        }
    }
}