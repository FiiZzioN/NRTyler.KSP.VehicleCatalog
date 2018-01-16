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
    /// Holds methods that aid in getting a <see cref="FairingSummary"/> from various collections or vehicle families.
    /// </summary>
    public class FairingSummaryController
    {
        /// <summary>
        /// Gets the values from the longest and the largest diameter fairing in a vehicle family
        /// and returns the values in the form of a <see cref="FairingSummary"/>;
        /// </summary>
        /// <param name="vehicleFamily">The vehicle family to analyze.</param>
        /// <returns>
        /// A <see cref="FairingSummary"/> containing the longest and the largest diameter fairing in a vehicle family.
        /// </returns>
        public FairingSummary GetFairingSummary(VehicleFamily vehicleFamily)
        {
            // Get the summaries.
            var launcherFairingSummary   = GetFairingSummary(vehicleFamily.Launchers);
            var collectionFairingSummary = GetFairingSummary(vehicleFamily.LauncherCollection);

            // Get the larger of the two summaries in the same categories.
            var maxLength   = SummaryController.GetLargerValue(launcherFairingSummary.MaxLength, collectionFairingSummary.MaxLength);
            var maxDiameter = SummaryController.GetLargerValue(launcherFairingSummary.MaxDiameter, collectionFairingSummary.MaxDiameter);

            return new FairingSummary(maxLength, maxDiameter);
        }

        /// <summary>
        /// Gets the values from the longest and the largest diameter fairing in a vehicle family
        /// and returns the values in the form of a <see cref="FairingSummary"/>;
        /// </summary>
        /// <param name="launcherCollections">An enumerable collection of type <see cref="LauncherCollection"/>.</param>
        /// <returns>
        /// A <see cref="FairingSummary"/> containing the longest 
        /// and the largest diameter fairing in a vehicle family.
        /// </returns>
        public FairingSummary GetFairingSummary(IEnumerable<LauncherCollection> launcherCollections)
        {
            // The reason for this is 'maxLength' and 'maxDiameter' can only go higher.
            var maxLength   = double.MinValue;
            var maxDiameter = double.MinValue;

            foreach (var collection in launcherCollections)
            {
                // Get's the price summary for the version.
                var fairingSummary = GetFairingSummary(collection.Launchers);

                // Check to make sure the values returned are greater than the current values. 
                // If they are, update the values, otherwise we continue checking the collection.
                maxLength   = SummaryController.GetLargerValue(fairingSummary.MaxLength, maxLength);
                maxDiameter = SummaryController.GetLargerValue(fairingSummary.MaxDiameter, maxDiameter);
            }

            return new FairingSummary(maxLength, maxDiameter);
        }

        /// <summary>
        /// Gets the values from the longest and the largest diameter fairing in a launcher collection
        /// and returns the values in the form of a <see cref="FairingSummary"/>;
        /// </summary>
        /// <param name="launcherCollection">An enumerable collection of type <see cref="Launcher"/>.</param>
        /// <returns>
        /// A <see cref="FairingSummary"/> containing the longest and 
        /// the largest diameter fairing in a launcher collection.
        /// </returns>
        public FairingSummary GetFairingSummary(IEnumerable<Launcher> launcherCollection)
        {
            // The reason for this is 'maxLength' and 'maxDiameter' can only go higher.
            var maxLength   = double.MinValue;
            var maxDiameter = double.MinValue;

            foreach (var launcher in launcherCollection)
            {
                // And then parse through each one of it's fairings to get their values.
                // Do this until you go through all launchers fairings.
                foreach (var fairing in launcher.Fairings)
                {
                    maxLength   = SummaryController.GetLargerValue(fairing.Length, maxLength);
                    maxDiameter = SummaryController.GetLargerValue(fairing.Diameter, maxDiameter);
                }
            }

            return new FairingSummary(maxLength, maxDiameter);
        }
    }
}