// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 10-05-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-05-2017
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// A <see cref="VehicleEntryDictionary"/>, also known as a Vehicle Family, holds a collection of 
    /// <see cref="VehicleEntry"/> objects that can be retrieved using the respective <see cref="VehicleEntry"/>'s name.
    /// </summary>
    public class VehicleEntryDictionary : Dictionary<string, VehicleEntry>, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleEntryDictionary"/> class.
        /// </summary>
        public VehicleEntryDictionary() : this("Family Name Not Set")
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleEntryDictionary"/> class.
        /// </summary>
        /// <param name="familyName">The name that you want this specific collection of vehicles to go by.</param>
        public VehicleEntryDictionary(string familyName)
        {
            FamilyName = familyName;
        }

        private string familyName;

        /// <summary>
        /// Gets or sets the name that this collection of vehicles will go by.
        /// </summary>
        public string FamilyName
        {
            get { return this.familyName; }
            set
            {
                if (this.familyName == value) return;
                
                this.familyName = value.HandleNullOrWhiteSpace("Invalid Family Name"); 
                OnPropertyChanged(nameof(FamilyName));
            }
        }

        /// <summary>
        /// Adds the specified <see cref="VehicleEntry"/> to the dictionary.
        /// </summary>
        /// <param name="entry">The entry to add to the dictionary.</param>
        /// <remarks>
        /// This takes the <see cref="VehicleEntry"/>'s name and uses 
        /// that as the key, and it uses the same entry for the value.
        /// </remarks>
        public virtual void AddEntry(VehicleEntry entry)
        {
            if (!ContainsKey(entry.Name) || !ContainsValue(entry))
            {
                try
                {
                    Add(entry.Name, entry);
                }
                catch (ArgumentException e)
                {
                    // No need to throw when you're just trying to add something with the same key.
                    Console.WriteLine(e);
                }
            }
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}