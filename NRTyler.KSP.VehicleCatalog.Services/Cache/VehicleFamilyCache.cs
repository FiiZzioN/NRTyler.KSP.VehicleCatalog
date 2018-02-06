// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
//
// Author           : Nicholas Tyler
// Created          : 01-28-2018
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 02-05-2018
//
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.Services.Interfaces;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using NRTyler.KSP.VehicleCatalog.Services.Utilities.Comparers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.Services.Cache
{
    public sealed class VehicleFamilyCache : ICache<VehicleFamily>
    {
        public VehicleFamilyCache(ApplicationSettings applicationSettings) : this(applicationSettings, new ErrorReport(true))
        {
            
        }

        public VehicleFamilyCache(ApplicationSettings applicationSettings, IErrorDialogService errorDialogService)
        {
            Settings = applicationSettings;
            Repo     = new VehicleFamilyRepo(Settings.VehicleFamilyLocation, errorDialogService);
            Cache    = new HashSet<VehicleFamily>(new VehicleFamilyEqualityComparer());

            Populate();
        }

        private ApplicationSettings Settings { get; }
        private VehicleFamilyRepo Repo { get; }
        private HashSet<VehicleFamily> Cache { get; }

        /// <summary>
        /// Loads every saved family into the cache.
        /// </summary>
        public void Populate()
        {
            if (Cache.Count != 0)
            {
                Clear();
            }

            var directoryPaths = Directory.EnumerateDirectories(Settings.VehicleFamilyLocation);

            foreach (var directoryPath in directoryPaths)
            {
                var directoryName = new DirectoryInfo(directoryPath).Name;
                var family        = Repo.Retrieve(directoryName);

                Add(family);
            }
        }

        /// <summary>
        /// Adds the specified family to the cache.
        /// </summary>
        /// <param name="obj">The family to add to the cache.</param>
        public void Add(VehicleFamily obj)
        {
            // If the cache already contains the item, there's nothing else to do. 
            // If it doesn't contain the item, then add it to the cache.
            if (Cache.Contains(obj))
            {
                return;
            }
            Cache.Add(obj);
        }

        /// <summary>
        /// Adds the family with the specified name to the cache.
        /// </summary>
        /// <param name="key">The name of the family to add to the cache.</param>
        public void Add(string key)
        {
            var family = Repo.Retrieve(key);
            Add(family);
        }

        /// <summary>
        /// Adds the elements of the specified collection the end of the cache.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the cache.</param>
        public void AddRange(IEnumerable<VehicleFamily> collection)
        {
            foreach (var vehicleFamily in collection)
            {
                Add(vehicleFamily);
            }
        }

        /// <summary>
        /// Removes the specified family from the cache.
        /// </summary>
        /// <param name="obj">The family to remove from the cache.</param>
        public void Remove(VehicleFamily obj)
        {
            Cache.Remove(obj);
        }

        /// <summary>
        /// Removes the family with the specified name from the cache.
        /// </summary>
        /// <param name="key">The name of the family to remove from the cache.</param>
        public void Remove(string key)
        {
            var family = Retrieve(key);
            Cache.Remove(family);
        }

        /// <summary>
        /// Refreshes every family in the cache.
        /// </summary>
        public void Refresh()
        {
            Clear();
            Populate();
        }

        /// <summary>
        /// Refreshes the specified family in the cache.
        /// </summary>
        /// <param name="obj">The family to refresh in the cache.</param>
        public void Refresh(VehicleFamily obj)
        {
            Remove(obj);

            var family = Repo.Retrieve(obj.Name);
            Add(family);
        }

        /// <summary>
        /// Refreshes the family with the specified name in the cache.
        /// </summary>
        /// <param name="key">The name of the family to refresh in the cache.</param>
        public void Refresh(string key)
        {
            Remove(key);

            var family = Repo.Retrieve(key);
            Add(family);
        }

        /// <summary>
        /// Retrieves a family with the specified name from the cache. If the cache doesn't 
        /// contain a family with that name, then <see langword="null"/> is returned.
        /// </summary>
        /// <param name="key">The name of the family to retrieve from the cache.</param>
        public VehicleFamily Retrieve(string key)
        {
            bool Predicate(VehicleFamily e) => String.Equals(e.Name, key, StringComparison.CurrentCultureIgnoreCase);

            return Cache.SingleOrDefault(Predicate);
        }

        /// <summary>
        /// Removes every family from the cache.
        /// </summary>
        public void Clear()
        {
            Cache.Clear();
        }

        /// <summary>
        /// Gets the families that are currently stored in the cache.
        /// </summary>
        public IEnumerable<VehicleFamily> GetCachedObjects()
        {
            return Cache;
        }
    }
}