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
using NRTyler.KSP.VehicleCatalog.Services.Utilities.Comparers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NRTyler.KSP.VehicleCatalog.Services.Cache
{
    public sealed class LauncherCollectionCache : ICache<LauncherCollection>
    {
        public LauncherCollectionCache(ApplicationSettings applicationSettings) : this(applicationSettings, new ErrorReport(true))
        {

        }

        public LauncherCollectionCache(ApplicationSettings applicationSettings, IErrorDialogService errorDialogService)
        {
            Settings = applicationSettings;
            Repo     = new LauncherCollectionRepo(Settings.LauncherCollectionLocation, errorDialogService);
            Cache    = new HashSet<LauncherCollection>(new LauncherCollectionEqualityComparer());

            Populate();
        }

        private ApplicationSettings Settings { get; }
        private LauncherCollectionRepo Repo { get; }
        private HashSet<LauncherCollection> Cache { get; }

        /// <summary>
        /// Loads every saved collection into the cache.
        /// </summary>
        public void Populate()
        {
            if (Cache.Count != 0)
            {
                Clear();
            }

            var directoryPaths = Directory.EnumerateDirectories(Settings.LauncherCollectionLocation);

            foreach (var directoryPath in directoryPaths)
            {
                var directoryName = new DirectoryInfo(directoryPath).Name;
                var collection    = Repo.Retrieve(directoryName);

                Add(collection);
            }
        }

        /// <summary>
        /// Adds the specified collection to the cache.
        /// </summary>
        /// <param name="obj">The collection to add to the cache.</param>
        public void Add(LauncherCollection obj)
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
        /// Adds the collection with the specified name to the cache.
        /// </summary>
        /// <param name="key">The name of the collection to add to the cache.</param>
        public void Add(string key)
        {
            var collection = Repo.Retrieve(key);
            Add(collection);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the cache.
        /// </summary>
        /// <param name="collections">The collection whose elements should be added to the end of the cache.</param>
        public void AddRange(IEnumerable<LauncherCollection> collections)
        {
            foreach (var collection in collections)
            {
                Add(collection);
            }
        }

        /// <summary>
        /// Removes the specified collection from the cache.
        /// </summary>
        /// <param name="obj">The collection to remove from the cache.</param>
        public void Remove(LauncherCollection obj)
        {
            Cache.Remove(obj);
        }

        /// <summary>
        /// Removes the collection with the specified name from the cache.
        /// </summary>
        /// <param name="key">The name of the collection to remove from the cache.</param>
        public void Remove(string key)
        {
            var collection = Retrieve(key);
            Cache.Remove(collection);
        }

        /// <summary>
        /// Refreshes every collection in the cache.
        /// </summary>
        public void Refresh()
        {
            Clear();
            Populate();
        }

        /// <summary>
        /// Refreshes the specified collection in the cache.
        /// </summary>
        /// <param name="obj">The collection to refresh in the cache.</param>
        public void Refresh(LauncherCollection obj)
        {
            Remove(obj);

            var collection = Repo.Retrieve(obj.Name);
            Add(collection);
        }

        /// <summary>
        /// Refreshes the collection with the specified name in the cache.
        /// </summary>
        /// <param name="key">The name of the collection to refresh in the cache.</param>
        public void Refresh(string key)
        {
            Remove(key);

            var collection = Repo.Retrieve(key);
            Add(collection);
        }

        /// <summary>
        /// Retrieves a collection with the specified name from the cache. If the cache doesn't 
        /// contain a collection with that name, then <see langword="null"/> is returned.
        /// </summary>
        /// <param name="key">The name of the collection to retrieve from the cache.</param>
        public LauncherCollection Retrieve(string key)
        {
            bool Predicate(LauncherCollection e) => String.Equals(e.Name, key, StringComparison.CurrentCultureIgnoreCase);

            return Cache.SingleOrDefault(Predicate);
        }

        /// <summary>
        /// Removes every collection from the cache.
        /// </summary>
        public void Clear()
        {
            Cache.Clear();
        }

        /// <summary>
        /// Gets the collections that are currently stored in the cache.
        /// </summary>
        public IEnumerable<LauncherCollection> GetCachedObjects()
        {
            return Cache;
        }
    }
}