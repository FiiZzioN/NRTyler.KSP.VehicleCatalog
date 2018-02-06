// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
//
// Author           : Nicholas Tyler
// Created          : 02-02-2018
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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NRTyler.KSP.VehicleCatalog.Services.Utilities.Comparers;

namespace NRTyler.KSP.VehicleCatalog.Services.Cache
{
    public sealed class LauncherCache : ICache<Launcher>
    {
        public LauncherCache(ApplicationSettings applicationSettings) : this(applicationSettings, new ErrorReport(true))
        {

        }

        public LauncherCache(ApplicationSettings applicationSettings, IErrorDialogService errorDialogService)
        {
            Settings = applicationSettings;
            Repo     = new LauncherRepo(Settings.LauncherLocation, errorDialogService);
            Cache    = new HashSet<Launcher>(new LauncherEqualityComparer());

            Populate();
        }

        private ApplicationSettings Settings { get; }
        private LauncherRepo Repo { get; }
        private HashSet<Launcher> Cache { get; }

        /// <summary>
        /// Loads every saved launcher into the cache.
        /// </summary>
        public void Populate()
        {
            if (Cache.Count != 0)
            {
                Clear();
            }

            var directoryPaths = Directory.EnumerateDirectories(Settings.LauncherLocation);

            foreach (var directoryPath in directoryPaths)
            {
                var directoryName = new DirectoryInfo(directoryPath).Name;
                var launcher      = Repo.Retrieve(directoryName);

                Add(launcher);
            }
        }

        /// <summary>
        /// Adds the specified launcher to the cache.
        /// </summary>
        /// <param name="obj">The launcher to add to the cache.</param>
        public void Add(Launcher obj)
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
        /// Adds the launcher with the specified name to the cache.
        /// </summary>
        /// <param name="key">The name of the launcher to add to the cache.</param>
        public void Add(string key)
        {
            var launcher = Repo.Retrieve(key);
            Add(launcher);
        }

        /// <summary>
        /// Adds the elements of the specified launcher to the end of the cache.
        /// </summary>
        /// <param name="launchers">The launcher whose elements should be added to the end of the cache.</param>
        public void AddRange(IEnumerable<Launcher> launchers)
        {
            foreach (var launcher in launchers)
            {
                Add(launcher);
            }
        }

        /// <summary>
        /// Removes the specified launcher from the cache.
        /// </summary>
        /// <param name="obj">The launcher to remove from the cache.</param>
        public void Remove(Launcher obj)
        {
            Cache.Remove(obj);
        }

        /// <summary>
        /// Removes the launcher with the specified name from the cache.
        /// </summary>
        /// <param name="key">The name of the launcher to remove from the cache.</param>
        public void Remove(string key)
        {
            var launcher = Retrieve(key);
            Cache.Remove(launcher);
        }

        /// <summary>
        /// Refreshes every launcher in the cache.
        /// </summary>
        public void Refresh()
        {
            Clear();
            Populate();
        }

        /// <summary>
        /// Refreshes the specified launcher in the cache.
        /// </summary>
        /// <param name="obj">The launcher to refresh in the cache.</param>
        public void Refresh(Launcher obj)
        {
            Remove(obj);

            var launcher = Repo.Retrieve(obj.Name);
            Add(launcher);
        }

        /// <summary>
        /// Refreshes the launcher with the specified name in the cache.
        /// </summary>
        /// <param name="key">The name of the launcher to refresh in the cache.</param>
        public void Refresh(string key)
        {
            Remove(key);

            var launcher = Repo.Retrieve(key);
            Add(launcher);
        }

        /// <summary>
        /// Retrieves a launcher with the specified name from the cache. If the cache doesn't 
        /// contain a launcher with that name, then <see langword="null"/> is returned.
        /// </summary>
        /// <param name="key">The name of the launcher to retrieve from the cache.</param>
        public Launcher Retrieve(string key)
        {
            bool Predicate(Launcher e) => String.Equals(e.Name, key, StringComparison.CurrentCultureIgnoreCase);

            return Cache.SingleOrDefault(Predicate);
        }

        /// <summary>
        /// Removes every launcher from the cache.
        /// </summary>
        public void Clear()
        {
            Cache.Clear();
        }

        /// <summary>
        /// Gets the launchers that are currently stored in the cache.
        /// </summary>
        public IEnumerable<Launcher> GetCachedObjects()
        {
            return Cache;
        }
    }
}