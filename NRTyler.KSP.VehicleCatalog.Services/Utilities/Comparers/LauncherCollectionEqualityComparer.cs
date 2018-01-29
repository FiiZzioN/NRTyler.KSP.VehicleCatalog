// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
// 
// Author           : Nicholas Tyler
// Created          : 01-28-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-28-2018
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.Services.Utilities.Comparers
{
    public class LauncherCollectionEqualityComparer : EqualityComparer<LauncherCollection>
    {
        /// <summary>When overridden in a derived class, determines whether two objects of type <paramref name="T" /> are equal.</summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>
        /// <see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
        public override bool Equals(LauncherCollection x, LauncherCollection y)
        {
            if (x is null || y is null)
            {
                return false;
            }

            return x.Name == y.Name && x.GlobalIdentifier == y.GlobalIdentifier;
        }

        /// <summary>When overridden in a derived class, serves as a hash function for the specified object for hashing algorithms and data structures, such as a hash table.</summary>
        /// <param name="obj">The object for which to get a hash code.</param>
        /// <returns>A hash code for the specified object.</returns>
        /// <exception cref="T:System.ArgumentNullException">The type of <paramref name="obj" /> is a reference type and <paramref name="obj" /> is <see langword="null" />.</exception>
        public override int GetHashCode(LauncherCollection obj)
        {
            if (obj is null)
            {
                return 0;
            }

            var nameHashCode = obj.Name.GetHashCode();
            var guidHashCode = obj.GlobalIdentifier.GetHashCode();
            var hashCode     = nameHashCode ^ guidHashCode;

            return hashCode;
        }
    }
}