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
    /// Contains methods for adding to, or removing launchers from a <see cref="DataProviders.LauncherCollection"/>. When modifying 
    /// a launcher collection's launcher <see cref="List{T}"/>, it's recommended that you use a <see cref="LauncherCollectionController"/> 
    /// since <see cref="Guid"/>'s also have to be modified at the time of addition or removal.
    /// </summary>
    public class LauncherCollectionController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LauncherCollectionController"/> class.
        /// </summary>
        /// <param name="launcherCollection">
        /// The <see cref="DataProviders.LauncherCollection"/> that this controller will work with.
        /// </param>
        public LauncherCollectionController(LauncherCollection launcherCollection)
        {
            LauncherCollection = launcherCollection;
        }

        /// <summary>
        /// Gets the <see cref="DataProviders.LauncherCollection"/> we're working with.
        /// </summary>
        private LauncherCollection LauncherCollection { get; }

        /// <summary>
        /// Adds the specified <see cref="Launcher" /> to the launcher collection's launcher <see cref="List{T}"/>. When
        /// added, the <see cref="Launcher" /> will have its RootIdentifier set to the launcher collection's GlobalIdentifier.
        /// </summary>
        /// <param name="launcher">
        /// The <see cref="Launcher" /> you wish to add to the launcher collection's launcher <see cref="List{T}" />.
        /// </param>
        /// <exception cref="ArgumentNullException">launcher - The object specified cannot be <see langword="null"/>.</exception>
        public void Add(Launcher launcher)
        {
            if (launcher == null)
            {
                throw new ArgumentNullException($"{nameof(launcher)}", "The object specified cannot be null.");
            }

            launcher.RootIdentifier = LauncherCollection.GlobalIdentifier;
            LauncherCollection.Launchers.Add(launcher);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the launcher collection's launcher <see cref="List{T}"/>. 
        /// Every <see cref="Launcher"/> added will have its RootIdentifier set to the launcher collection's GlobalIdentifier.
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
        /// Removes the specified <see cref="Launcher"/> from the collection's launcher 
        /// <see cref="List{T}"/> and changes its RootIdentifier to an empty <see cref="Guid"/>.
        /// </summary>
        /// <param name="launcher">The <see cref="Launcher"/> you wish to remove.</param>
        public void Remove(Launcher launcher)
        {
            launcher.RootIdentifier = Guid.Empty;
            LauncherCollection.Launchers.Remove(launcher);
        }

        /// <summary>
        /// Removes a range of elements from the launcher list. Any <see cref="object"/> 
        /// removed will have its RootIdentifier set to an empty <see cref="Guid"/>.
        /// </summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// index - Index can't be less than zero.
        /// or
        /// count - Count can't be less than zero.
        /// </exception>
        public void RemoveRange(int index, int count)
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
            
            var collection = LauncherCollection.Launchers.GetRange(index, count);

            foreach (var launcher in collection)
            {
                Remove(launcher);
            }
        }
    }
}