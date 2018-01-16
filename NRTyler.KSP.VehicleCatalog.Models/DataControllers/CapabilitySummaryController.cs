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

using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
    /// <summary>
    /// Holds methods that aid in getting a <see cref="CapabilitySummary"/> collection from various LauncherCollections or VehicleFamilies.
    /// </summary>
    public class CapabilitySummaryController
    {
        /// <summary>
        /// Returns a <see cref="CapabilitySummary"/> collection containing the payload weight range extremes 
        /// for any <see cref="Trajectory"/> that a <see cref="Launcher"/> in the collection supports.
        /// </summary>
        /// <param name="launchers">The <see cref="LauncherCollection"/> collection that this will analyze.</param>
        /// <remarks>
        /// What "Payload Weight Range Extremes" means is the absolute lightest and the 
        /// absolute heaviest (the extremes) a payload can weight be for a given trajectory.
        /// </remarks>
        public IEnumerable<CapabilitySummary> GetCapabilitySummary(IEnumerable<Launcher> launchers)
        {
            var capabilitySummaries = new List<CapabilitySummary>();

            // Get a capability summary for the launchers being analyzed.
            var summaryList = GetASummaryForAllCapabilities(launchers).ToList();

            // Go through each summary retrieved and figure out which OrbitTypes we're going to have to compare.
            // This is required so we can compare summaries of a specific OrbitType against one another.
            var orbitTypesToCompare = GetOrbitTypesToCompare(summaryList).ToList();

            // Add the summaries retrieved to the capabilitySummaries list. We need this list so we can compare 
            // all summaries of the same OrbitType to one another further along in the scope.
            capabilitySummaries.AddRange(summaryList);

            return GetSummariesOfPayloadRangeExtremes(orbitTypesToCompare, capabilitySummaries);
        }

        /// <summary>
        /// Returns a <see cref="CapabilitySummary"/> collection containing the payload weight range extremes 
        /// for any <see cref="Trajectory"/> that a <see cref="Launcher"/> in the collection supports.
        /// </summary>
        /// <param name="launcherCollections">The <see cref="LauncherCollection"/> collection that this will analyze.</param>
        /// <remarks>
        /// What "Payload Weight Range Extremes" means is the absolute lightest and the 
        /// absolute heaviest (the extremes) a payload can weight be for a given trajectory.
        /// </remarks>
        public IEnumerable<CapabilitySummary> GetCapabilitySummary(IEnumerable<LauncherCollection> launcherCollections)
        {
            var capabilitySummaries = new List<CapabilitySummary>();
            var orbitTypesToCompare = new List<OrbitType>();

            foreach (var launcherCollection in launcherCollections)
            {
                // Get a capability summary for the launchers being analyzed.
                var summaryList = GetASummaryForAllCapabilities(launcherCollection.Launchers).ToList();

                // Go through each summary retrieved and figure out which OrbitTypes we're going to have to compare.
                // This is required so we can compare summaries of a specific OrbitType against one another.
                orbitTypesToCompare = GetOrbitTypesToCompare(summaryList).ToList();

                // Add the summaries retrieved to the capabilitySummaries list. We need this list so we can compare 
                // all summaries of the same OrbitType to one another further along in the scope.
                capabilitySummaries.AddRange(summaryList);
            }

            return GetSummariesOfPayloadRangeExtremes(orbitTypesToCompare, capabilitySummaries);
        }

        public IEnumerable<CapabilitySummary> GetCapabilitySummary(VehicleFamily vehicleFamily)
        {
            var capabilitySummaries = new List<CapabilitySummary>();

            var collectionSummaries = GetASummaryForAllCapabilities(vehicleFamily.LauncherCollection);
            var launcherSummaries = GetASummaryForAllCapabilities(vehicleFamily.Launchers);

            capabilitySummaries.AddRange(collectionSummaries);
            capabilitySummaries.AddRange(launcherSummaries);

            var orbitTypesToCompare = GetOrbitTypesToCompare(capabilitySummaries);

            return GetSummariesOfPayloadRangeExtremes(orbitTypesToCompare, capabilitySummaries);
        }

        /// <summary>
        /// Returns a <see cref="CapabilitySummary"/> collection containing a summary for every capability that a launcher contains.
        /// </summary>
        /// <param name="launcher">The <see cref="Launcher"/> to analyze.</param>
        public IEnumerable<CapabilitySummary> GetASummaryForAllCapabilities(Launcher launcher)
        {
            var capabilitySummaries = new List<CapabilitySummary>();

            foreach (var capability in launcher.Capabilities)
            {
                var orbitType = capability.Trajectory.OrbitType;
                var lightest = capability.PayloadRange.Lightest;
                var heaviest = capability.PayloadRange.Heaviest;

                var summary = new CapabilitySummary(orbitType, new PayloadRange(lightest, heaviest));

                capabilitySummaries.Add(summary);
            }

            return capabilitySummaries;
        }

        /// <summary>
        /// Returns a <see cref="CapabilitySummary"/> collection containing a summary for every capability that the launchers contain.
        /// </summary>
        /// <param name="launchers">The <see cref="Launcher"/> collection to analyze.</param>
        public IEnumerable<CapabilitySummary> GetASummaryForAllCapabilities(IEnumerable<Launcher> launchers)
        {
            var capabilitySummaries = new List<CapabilitySummary>();

            foreach (var launcher in launchers)
            {
                // Get the collection of summaries for the launcher being analyzed.
                var summaryList = GetASummaryForAllCapabilities(launcher).ToList();

                capabilitySummaries.AddRange(summaryList);
            }

            return capabilitySummaries;
        }

        /// <summary>
        /// Returns a <see cref="CapabilitySummary"/> collection containing a summary 
        /// for every capability that a launcher in any of the collections may contain.
        /// </summary>
        /// <param name="launcherCollections">The <see cref="LauncherCollection"/> collection to analyze.</param>
        public IEnumerable<CapabilitySummary> GetASummaryForAllCapabilities(IEnumerable<LauncherCollection> launcherCollections)
        {
            var capabilitySummaries = new List<CapabilitySummary>();

            foreach (var launcherCollection in launcherCollections)
            {
                // Get the collection of summaries for the launcher being analyzed.
                var summaryList = GetASummaryForAllCapabilities(launcherCollection.Launchers).ToList();

                capabilitySummaries.AddRange(summaryList);
            }

            return capabilitySummaries;
        }

        /// <summary>
        /// Returns an <see cref="OrbitType"/> collection that the specified collection has summaries about.
        /// </summary>
        /// <param name="capabilitySummaries">The <see cref="CapabilitySummary"/> collection to analyze.</param>
        public IEnumerable<OrbitType> GetOrbitTypesToCompare(IEnumerable<CapabilitySummary> capabilitySummaries)
        {
            var orbitTypesToCompare = new List<OrbitType>();

            foreach (var summary in capabilitySummaries)
            {
                var orbitType = summary.OrbitType;

                if (!orbitTypesToCompare.Contains(orbitType))
                {
                    orbitTypesToCompare.Add(orbitType);
                }
            }

            return orbitTypesToCompare;
        }

        /// <summary>
        /// Returns a <see cref="CapabilitySummary"/> collection containing the lightest 
        /// and heaviest a payload can be for each <see cref="OrbitType"/> specified.
        /// </summary>
        /// <param name="orbitTypesToCompare">
        /// The <see cref="OrbitType"/> collection containing the orbit types you wish to gather a <see cref="CapabilitySummary"/> about.
        /// </param>
        /// <param name="capabilitySummaries">
        /// The <see cref="CapabilitySummary"/> collection  to go through to gather the payload range extremes from.
        /// </param>
        /// <remarks>
        /// What "Payload Range Extremes" means is the absolute lightest and the absolute heaviest
        /// (the extremes) a payload can weight be for the orbit type currently being analyzed.
        /// </remarks>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public IEnumerable<CapabilitySummary> GetSummariesOfPayloadRangeExtremes(IEnumerable<OrbitType> orbitTypesToCompare, IEnumerable<CapabilitySummary> capabilitySummaries)
        {
            var list = new List<CapabilitySummary>();

            foreach (var orbitType in orbitTypesToCompare)
            {
                var filteredSummaries = FilterSummaries(capabilitySummaries.ToList(), orbitType);
                var payloadRange = GetLightestAndHeaviest(filteredSummaries);

                list.Add(new CapabilitySummary(orbitType, payloadRange));
            }

            return list;
        }

        /// <summary>
        /// Returns a <see cref="PayloadRange"/> containing the lightest and 
        /// heaviest values in the specified <see cref="CapabilitySummary"/> collection.
        /// </summary>
        /// <param name="capabilitySummaries">The <see cref="CapabilitySummary"/> collection to analyze.</param>
        public PayloadRange GetLightestAndHeaviest(IEnumerable<CapabilitySummary> capabilitySummaries)
        {
            var lightest = int.MaxValue;
            var heaviest = int.MinValue;

            foreach (var summary in capabilitySummaries)
            {
                var summaryLightest = summary.PayloadRange.Lightest;
                var summaryHeaviest = summary.PayloadRange.Heaviest;

                lightest = SummaryController.GetSmallerValue(lightest, summaryLightest);
                heaviest = SummaryController.GetLargerValue(heaviest, summaryHeaviest);
            }

            return new PayloadRange(lightest, heaviest);
        }

        /// <summary>
        /// Returns a <see cref="CapabilitySummary"/> collection containing only summaries of the specified <see cref="OrbitType"/>.
        /// </summary>
        /// <param name="capabilitySummaries">The <see cref="CapabilitySummary"/> collection to search through.</param>
        /// <param name="orbitType">The <see cref="OrbitType"/> a summary must be in order to get added to the collection being returned.</param>
        public IEnumerable<CapabilitySummary> FilterSummaries(IEnumerable<CapabilitySummary> capabilitySummaries, OrbitType orbitType)
        {
            // Gather all summaries of the same OrbitType into a list of only that OrbitType.
            return capabilitySummaries.Where(summary => summary.OrbitType == orbitType).ToList();
        }
    }
}