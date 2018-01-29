// ***********************************************************************
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
using NRTyler.KSP.VehicleCatalog.Services.Interfaces;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;
using NRTyler.KSP.VehicleCatalog.Services.Utilities.Comparers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.Services.Controllers
{
    public sealed class VehicleFamilyCacheController : ICacheController<VehicleFamily>
    {
        public VehicleFamilyCacheController(ApplicationSettings applicationSettings) : this(applicationSettings, new ErrorReport(true))
        {
            
        }

        public VehicleFamilyCacheController(ApplicationSettings applicationSettings, IErrorDialogService errorDialogService)
        {
            Settings    = applicationSettings;
            FamilyRepo  = new VehicleFamilyRepo(Settings.VehicleFamilyLocation, errorDialogService);
            FamilyCache = new HashSet<VehicleFamily>(new VehicleFamilyEqualityComparer());

            Populate();
        }

        private ApplicationSettings Settings { get; }
        private VehicleFamilyRepo FamilyRepo { get; }
        private HashSet<VehicleFamily> FamilyCache { get; }

        /// <summary>
        /// Loads every saved family into the cache.
        /// </summary>
        public void Populate()
        {
            if (FamilyCache.Count != 0)
            {
                Clear();
            }

            var directoryPaths = Directory.EnumerateDirectories(Settings.VehicleFamilyLocation);

            foreach (var directoryPath in directoryPaths)
            {
                var directoryName = new DirectoryInfo(directoryPath).Name;

                var family = FamilyRepo.Retrieve(directoryName);
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
            if (FamilyCache.Contains(obj))
            {
                return;
            }
            FamilyCache.Add(obj);
        }

        /// <summary>
        /// Adds the family with the specified name to the cache.
        /// </summary>
        /// <param name="key">The name of the family to add to the cache.</param>
        public void Add(string key)
        {
            var family = FamilyRepo.Retrieve(key);
            Add(family);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the cache.
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
            FamilyCache.Remove(obj);
        }

        /// <summary>
        /// Removes the family with the specified name from the cache.
        /// </summary>
        /// <param name="key">The name of the family to remove from the cache.</param>
        public void Remove(string key)
        {
            var family = Retrieve(key);
            FamilyCache.Remove(family);
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

            var family = FamilyRepo.Retrieve(obj.Name);
            Add(family);
        }

        /// <summary>
        /// Refreshes the family with the specified name in the cache.
        /// </summary>
        /// <param name="key">The name of the family to refresh in the cache.</param>
        public void Refresh(string key)
        {
            Remove(key);

            var family = FamilyRepo.Retrieve(key);
            Add(family);
        }

        /// <summary>
        /// Retrieves a family with the specified name from the cache. If the cache doesn't 
        /// contain a family with that name, then <see langword="null"/> is returned.
        /// </summary>
        /// <param name="key">The name of the family to retrieve from the cache.</param>
        public VehicleFamily Retrieve(string key)
        {
            var family = FamilyCache.SingleOrDefault(e => String.Equals(e.Name, key, StringComparison.CurrentCultureIgnoreCase));

            return family == default(VehicleFamily) ? null : family;
        }

        /// <summary>
        /// Removes every family from the cache.
        /// </summary>
        public void Clear()
        {
            FamilyCache.Clear();
        }

        /// <summary>
        /// Gets the families that are currently stored in the cache.
        /// </summary>
        public IEnumerable<VehicleFamily> GetCachedObjects()
        {
            return FamilyCache;
        }
    }
}