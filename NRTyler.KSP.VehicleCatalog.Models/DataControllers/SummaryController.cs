// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 12-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-28-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
    /// <summary>
    /// Contains the methods that a <see cref="Summary"/> object uses in order to fill its fields.
    /// </summary>
    public static class SummaryController
    {


        /// <summary>
        /// Gets the price of the cheapest and most expensive launcher in a vehicle family, 
        /// and then returns them in the form of a <see cref="PriceSummary"/>.
        /// </summary>
        /// <param name="collection">An enumerable collection of type <see cref="LauncherCollection"/>.</param>
        /// <returns>
        /// A <see cref="PriceSummary"/> containing the price of the cheapest and 
        /// most expensive launcher in a vehicle family.
        /// </returns>
        public static PriceSummary GetPriceSummary(this IEnumerable<LauncherCollection> collection)
        {
            // The reason for this is 'cheapest' can only go lower and 'mostExpensive' can only go higher.
            var cheapest      = decimal.MaxValue;
            var mostExpensive = decimal.MinValue;

            foreach (var launcher in collection)
            {
                // Get's the price summary for the version.
                var priceSummary = launcher.GetPriceSummary();

                // Check to make sure the values returned are cheaper or more expensive. 
                // If they are, update the values, otherwise we continue checking the collection.
                if (priceSummary.Cheapest < cheapest)
                {
                    cheapest = priceSummary.Cheapest;
                }
                if (priceSummary.MostExpensive > mostExpensive)
                {
                    mostExpensive = priceSummary.MostExpensive;
                }                
            }

            return new PriceSummary(cheapest, mostExpensive);
        }

        /// <summary>
        /// Gets the price of the cheapest and most expensive launcher in a launcher collection, 
        /// and then returns them in the form of a <see cref="PriceSummary"/>.
        /// </summary>
        /// <param name="collection">An enumerable collection of type <see cref="Launcher"/>.</param>
        /// <returns>
        /// A <see cref="PriceSummary"/> containing the price of the cheapest and 
        /// most expensive launcher in a Launcher collection.
        /// </returns>
        public static PriceSummary GetPriceSummary(this IEnumerable<Launcher> collection)
        {
            // The reason for this is 'cheapest' can only go lower and 'mostExpensive' can only go higher.
            var cheapest      = decimal.MaxValue;
            var mostExpensive = decimal.MinValue;

            foreach (var launcher in collection)
            {
                // If the price is less than the current 'cheapest' value, then replace it with the cheaper value.
                if (launcher.Price < cheapest)
                {
                    cheapest = launcher.Price;
                }

                // If the price is more than the current 'mostExpensive' value, then replace it with the more expensive value.
                if (launcher.Price > mostExpensive)
                {
                    mostExpensive = launcher.Price;
                }
            }

            return new PriceSummary(cheapest, mostExpensive);
        }


        /// <summary>
        /// Gets the values from the longest and the largest diameter fairing in a vehicle family
        /// and returns the values in the form of a <see cref="FairingSummary"/>;
        /// </summary>
        /// <param name="collection">An enumerable collection of type <see cref="LauncherCollection"/>.</param>
        /// <returns>
        /// A <see cref="FairingSummary"/> containing the longest 
        /// and the largest diameter fairing in a vehicle family.
        /// </returns>
        public static FairingSummary GetFairingSummary(this IEnumerable<LauncherCollection> collection)
        {
            // The reason for this is 'maxLength' and 'maxDiameter' can only go higher.
            double? maxLength   = double.MinValue;
            double? maxDiameter = double.MinValue;

            foreach (var launcher in collection)
            {
                // Get's the price summary for the version.
                var fairingSummary = launcher.GetFairingSummary();

                // Check to make sure the values returned are greater than the current values. 
                // If they are, update the values, otherwise we continue checking the collection.
                if (fairingSummary.MaxLength > maxLength)
                {
                    maxLength = fairingSummary.MaxLength;
                }
                if (fairingSummary.MaxDiameter > maxDiameter)
                {
                    maxDiameter = fairingSummary.MaxDiameter;
                }
            }

            return new FairingSummary(maxLength, maxDiameter);
        }

        /// <summary>
        /// Gets the values from the longest and the largest diameter fairing in a launcher collection
        /// and returns the values in the form of a <see cref="FairingSummary"/>;
        /// </summary>
        /// <param name="collection">An enumerable collection of type <see cref="Launcher"/>.</param>
        /// <returns>
        /// A <see cref="FairingSummary"/> containing the longest and 
        /// the largest diameter fairing in a launcher collection.
        /// </returns>
        public static FairingSummary GetFairingSummary(this IEnumerable<Launcher> collection)
        {
            // The reason for this is 'maxLength' and 'maxDiameter' can only go higher.
            double? maxLength   = double.MinValue;
            double? maxDiameter = double.MinValue;

            // Parse through each launcher...
            foreach (var launcher in collection)
            {
                // And then parse through each one of it's fairings to get their values.
                // Do this until you go through all launchers fairings.
                foreach (var fairing in launcher.Fairings)
                {
                    if (fairing.Length > maxLength)
                    {
                        maxLength = fairing.Length;
                    }
                    if (fairing.Diameter > maxDiameter)
                    {
                        maxDiameter = fairing.Diameter;
                    }
                }
            }

            return new FairingSummary(maxLength, maxDiameter);
        }

        /// <summary>
        /// Gets the number of launchers in a vehicle family.
        /// </summary>
        /// <param name="collection">A collection of type <see cref="LauncherCollection"/>..</param>
        /// <returns>The number of items in the vehicle family.</returns>
        public static int GetNumberOfVersions(this ICollection<LauncherCollection> collection)
        {
            return collection.Sum(GetNumberOfVersions);
        }

        /// <summary>
        /// Gets the number of launchers in a launcher collection
        /// </summary>
        /// <param name="collection">A collection of type <see cref="Launcher"/>..</param>
        /// <returns>The number of items in the launcher collection.</returns>
        public static int GetNumberOfVersions(this ICollection<Launcher> collection)
        {
            return collection.Count;
        }
    }
}