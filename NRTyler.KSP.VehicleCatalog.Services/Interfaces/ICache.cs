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

using System;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.Services.Interfaces
{
    /// <summary>
    /// Interface ICache
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of object that this cache is meant to deal with.</typeparam>
    public interface ICache<T>
    {
        /// <summary>
        /// Loads every saved object into the cache.
        /// </summary>
        void Populate();

        /// <summary>
        /// Adds the specified object to the cache.
        /// </summary>
        /// <param name="obj">The object to add to the cache.</param>
        void Add(T obj);

        /// <summary>
        /// Adds the object with the specified name or key to the cache.
        /// </summary>
        /// <param name="key">The name or key of the object to add to the cache.</param>
        void Add(string key);

        /// <summary>
        /// Adds the elements of the specified collection to the end of the cache.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the end of the cache.</param>
        void AddRange(IEnumerable<T> collection);

        /// <summary>
        /// Removes the specified object from the cache.
        /// </summary>
        /// <param name="obj">The object to remove from the cache.</param>
        void Remove(T obj);

        /// <summary>
        /// Removes the object with the specified name or key from the cache.
        /// </summary>
        /// <param name="key">The name or key of the object to remove from the cache.</param>
        void Remove(string key);

        /// <summary>
        /// Refreshes every object in the cache.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Refreshes the specified object in the cache.
        /// </summary>
        /// <param name="obj">The object to refresh in the cache.</param>
        void Refresh(T obj);

        /// <summary>
        /// Refreshes the object with the specified name or key in the cache.
        /// </summary>
        /// <param name="key">The name or key of the object to refresh in the cache.</param>
        void Refresh(string key);

        /// <summary>
        /// Retrieves an object with the specified name or key from the cache.
        /// </summary>
        /// <param name="key">The name or key of the object to retrieve from the cache.</param>
        T Retrieve(string key);

        /// <summary>
        /// Removes every object in the cache.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the objects that are currently stored in the cache.
        /// </summary>
        IEnumerable<T> GetCachedObjects();
    }
}