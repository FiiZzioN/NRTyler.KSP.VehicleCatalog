// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-20-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Holds information about a given <see cref="Fairing" /> option that a <see cref="Launcher" /> has at its disposal.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "Fairing")]
    public class Fairing : INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Fairing"/> class.
		/// </summary>
		public Fairing() : this(null, null, null)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Fairing" /> class.
        /// </summary>
        /// <param name="id">The fairings identifier. Examples include: Small, Medium, Large</param>
        /// <param name="length">The length of the fairing.</param>
        /// <param name="diameter">The diameter of the fairing.</param>
		public Fairing(string id, double? length, double? diameter)
        {
            ID       = id.HandleNullOrWhiteSpace("ID Not Specified");
		    Length   = length;
            Diameter = diameter;
        }

        private string id;
        private double? length;
		private double? diameter;

	    /// <summary>
	    /// Gets or sets the fairings identifier.
	    /// </summary>
	    [DataMember]
	    public string ID
	    {
	        get { return this.id; }
	        set
	        {
	            this.id = value.HandleNullOrWhiteSpace("Invalid ID");
	            OnPropertyChanged(nameof(ID));
	        }
	    }

	    /// <summary>
	    /// Gets or sets the length of the fairing.
	    /// </summary>
	    [DataMember]
	    public double? Length
	    {
	        get { return this.length; }
	        set
	        {
	            if (value < 0)
	            {
	                value = null;
	            }

                this.length = value;
	            OnPropertyChanged(nameof(Length));
	        }
	    }

        /// <summary>
        /// Gets or sets the diameter of the fairing.
        /// </summary>
        [DataMember]
	    public double? Diameter
	    {
	        get { return this.diameter; }
	        set
	        {
	            if (value < 0)
	            {
	                value = null;
	            }

                this.diameter = value;
	            OnPropertyChanged(nameof(Diameter));
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
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}