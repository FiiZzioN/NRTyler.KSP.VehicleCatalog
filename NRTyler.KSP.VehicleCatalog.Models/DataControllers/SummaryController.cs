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
        public static int GetNumberOfVersions(this ICollection collection)
        {
            return collection.Count;
        }

        public static PriceSummary GetPriceSummary(this IEnumerable<LauncherCollection> collection)
        {
            // The reason for this is 'cheapest' can only go lower and 'mostExpensive' can only go higher.
            var cheapest      = decimal.MaxValue;
            var mostExpensive = decimal.MinValue;

            foreach (var launcher in collection)
            {
                // Get's the price summary for the version.
                var priceSummary = launcher.GetPriceSummary();

                // Simply relays the values to this method's scope.
                cheapest      = priceSummary.Cheapest;
                mostExpensive = priceSummary.MostExpensive;
            }

            return new PriceSummary(cheapest, mostExpensive);
        }

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










        public static FairingSummary GetFairingSummary(this IEnumerable<LauncherCollection> collection)
        {
            // The reason for this is 'maxLength' and 'maxDiameter' can only go higher.
            double? maxLength   = double.MinValue;
            double? maxDiameter = double.MinValue;

            foreach (var launcher in collection)
            {
                // Get's the price summary for the version.
                var fairingSummary = launcher.GetFairingSummary();

                // Simply relays the values to this method's scope.
                maxLength   = fairingSummary.MaxLength;
                maxDiameter = fairingSummary.MaxDiameter;
            }

            return new FairingSummary(maxLength, maxDiameter);
        }

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

            // If either value is below zero, then that means that a 
            // vehicle has no fairings, so there are no values to represent.
            if (maxLength <= 0)
            {
                maxLength = null;
            }
            if (maxDiameter <= 0)
            {
                maxDiameter = null;
            }

            return new FairingSummary(maxLength, maxDiameter);
        }

    }
}