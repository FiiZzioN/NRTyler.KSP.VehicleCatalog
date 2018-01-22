// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 01-21-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-21-2018
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
    /// <summary>
    /// Contains methods for adding to, or removing launchers and launcher collections from a 
    /// <see cref="DataProviders.VehicleFamily"/>. When modifying a family's collections, it's 
    /// recommended that you use a <see cref="VehicleFamilyController"/> since <see cref="Guid"/>'s 
    /// also have to be modified at the time of addition or removal.
    /// </summary>
    public class VehicleFamilyController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleFamilyController"/> class.
        /// </summary>
        /// <param name="vehicleFamily">
        /// The <see cref="DataProviders.VehicleFamily"/> that this controller will work with.
        /// </param>
        public VehicleFamilyController(VehicleFamily vehicleFamily)
        {
            VehicleFamily = vehicleFamily;
        }

        /// <summary>
        /// Gets the <see cref="DataProviders.VehicleFamily"/> we're working with.
        /// </summary>
        private VehicleFamily VehicleFamily { get; }

        /// <summary>
        /// Adds the specified <see cref="Launcher" /> to the family's launcher <see cref="List{T}" />.
        /// When added, the <see cref="Launcher" /> will have its RootIdentifier set to the family's GlobalIdentifier.
        /// </summary>
        /// <param name="launcher">
        /// The <see cref="Launcher" /> you wish to add to the family's launcher <see cref="List{T}" />.
        /// </param>
        /// <exception cref="ArgumentNullException">launcher - The object specified cannot be <see langword="null"/>.</exception>
        public void Add(Launcher launcher)
        {
            if (launcher == null)
            {
                throw new ArgumentNullException($"{nameof(launcher)}", "The object specified cannot be null.");
            }

            launcher.RootIdentifier = VehicleFamily.GlobalIdentifier;
            VehicleFamily.Launchers.Add(launcher);
        }

        /// <summary>
        /// Adds the specified <see cref="LauncherCollection" /> to the family's launcher collection <see cref="List{T}" />.
        /// When added, the <see cref="LauncherCollection" /> will have its RootIdentifier set to the family's GlobalIdentifier.
        /// </summary>
        /// <param name="launcherCollection">
        /// The <see cref="LauncherCollection" /> you wish to add to the family's launcher collection <see cref="List{T}" />.
        /// </param>
        /// <exception cref="ArgumentNullException">launcherCollection - The object specified cannot be <see langword="null"/>.</exception>
        public void Add(LauncherCollection launcherCollection)
        {
            if (launcherCollection == null)
            {
                throw new ArgumentNullException($"{nameof(launcherCollection)}", "The object specified cannot be null.");
            }

            launcherCollection.RootIdentifier = VehicleFamily.GlobalIdentifier;
            VehicleFamily.LauncherCollections.Add(launcherCollection);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the family's launcher <see cref="List{T}"/>. 
        /// Every <see cref="Launcher"/> added will have its RootIdentifier set to the family's GlobalIdentifier.
        /// </summary>
        /// <param name="launchers">
        /// The collection whose elements should be added to the end of the <see cref="List{T}"/>. The collection 
        /// itself cannot be <see langword="null"/>, but it can contain elements that are <see langword="null"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">launchers - The collection specified cannot be <see langword="null"/>.</exception>
        public void AddRange(IEnumerable<Launcher> launchers)
        {
            if (launchers == null)
            {
                throw new ArgumentNullException($"{nameof(launchers)}", "The collection specified cannot be null.");
            }

            foreach (var launcher in launchers)
            {
                Add(launcher);
            }
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the family's launcher collection <see cref="List{T}"/>. 
        /// Every <see cref="LauncherCollection"/> added will have its RootIdentifier set to the family's GlobalIdentifier.
        /// </summary>
        /// <param name="launcherCollections">
        /// The collection whose elements should be added to the end of the <see cref="List{T}"/>. The collection 
        /// itself cannot be <see langword="null"/>, but it can contain elements that are <see langword="null"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">launcherCollections - The collection specified cannot be <see langword="null"/>.</exception>
        public void AddRange(IEnumerable<LauncherCollection> launcherCollections)
        {
            if (launcherCollections == null)
            {
                throw new ArgumentNullException($"{nameof(launcherCollections)}" ,"The collection specified cannot be null.");
            }

            foreach (var launcherCollection in launcherCollections)
            {
                Add(launcherCollection);
            }
        }

        /// <summary>
        /// Removes the specified <see cref="Launcher"/> from the family's launcher 
        /// <see cref="List{T}"/> and changes its RootIdentifier to an empty <see cref="Guid"/>.
        /// </summary>
        /// <param name="launcher">The <see cref="Launcher"/> you wish to remove.</param>
        public void Remove(Launcher launcher)
        {
            launcher.RootIdentifier = Guid.Empty;
            VehicleFamily.Launchers.Remove(launcher);
        }

        /// <summary>
        /// Removes the specified <see cref="LauncherCollection"/> from the family's launcher 
        /// collection <see cref="List{T}"/> and changes its RootIdentifier to an empty <see cref="Guid"/>.
        /// </summary>
        /// <param name="launcherCollection">The <see cref="LauncherCollection"/> you wish to remove.</param>
        public void Remove(LauncherCollection launcherCollection)
        {
            launcherCollection.RootIdentifier = Guid.Empty;
            VehicleFamily.LauncherCollections.Remove(launcherCollection);
        }

        /// <summary>
        /// Removes a range of elements from a collection. Any <see cref="object"/> 
        /// removed will have its RootIdentifier set to an empty <see cref="Guid"/>.
        /// </summary>
        /// <param name="collectionType">
        /// The type of the collection. Must be either <see cref="LauncherCollection"/> 
        /// or <see cref="Launcher"/> since a family can contain bot.
        /// </param>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// index - Index can't be less than zero.
        /// or
        /// count - Count can't be less than zero.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// CollectionType must be of type <see cref="LauncherCollection"/> or <see cref="Launcher"/>.
        /// </exception>
        public void RemoveRange(Type collectionType, int index, int count)
        {
            #region Argument Checks

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(index)}", "Index can't be less than zero.");
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(count)}", "Count can't be less than zero.");
            } 

            #endregion

            if (collectionType == typeof(LauncherCollection))
            {
                var collection = VehicleFamily.LauncherCollections.GetRange(index, count);

                foreach (var launcherCollection in collection)
                {
                    Remove(launcherCollection);
                }

                // We must return so we don't throw an exception.
                return;
            }

            if (collectionType == typeof(Launcher))
            {
                var collection = VehicleFamily.Launchers.GetRange(index, count);

                foreach (var launcher in collection)
                {
                    Remove(launcher);
                }

                // We must return so we don't throw an exception.
                return;
            }

            // Using an if with an or statement will throw a false-positive, so we can't include it in the "Argument Checks" region.
            throw new ArgumentException($"CollectionType must be of type {typeof(LauncherCollection)} or {typeof(Launcher)}.");
        }
    }
}