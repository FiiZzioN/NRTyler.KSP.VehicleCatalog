// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 12-27-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-27-2017
// 
// License          : MIT License
// ***********************************************************************

using System.Collections;
using System.Collections.Generic;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
    public static class SummaryController
    {
        /// <summary>
        /// Gets the number of versions of vehicles in the vehicle family.
        /// </summary>
        private static int GetNumberOfVersions(this ICollection collection)
        {
            return collection.Count;
        }

        public static PriceSummary GetPriceSummary(this IEnumerable<LauncherCollection> launcherCollection)
        {
            // The reason for this is 'cheapest' can only go lower and 'mostExpensive' can only go higher.
            var cheapest      = decimal.MaxValue;
            var mostExpensive = decimal.MinValue;

            foreach (var launcher in launcherCollection)
            {
                // Get's the price summary for the version.
                var priceSummary = launcher.GetPriceSummary();

                // Simply relays the values to this method's scope.
                cheapest      = priceSummary.Cheapest;
                mostExpensive = priceSummary.MostExpensive;
            }

            return new PriceSummary(cheapest, mostExpensive);
        }

        public static PriceSummary GetPriceSummary(this IEnumerable<Launcher> launchers)
        {
            // The reason for this is 'cheapest' can only go lower and 'mostExpensive' can only go higher.
            var cheapest      = decimal.MaxValue;
            var mostExpensive = decimal.MinValue;

            foreach (var launcher in launchers)
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
    }
}