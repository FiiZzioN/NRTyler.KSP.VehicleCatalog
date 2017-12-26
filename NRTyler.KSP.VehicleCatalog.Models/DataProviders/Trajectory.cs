// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-25-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.Common.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Contains the parameters used to make up a specified orbit. Those include LEO, SSO, GTO and GEO.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
	public class Trajectory : IOrbit, INotifyPropertyChanged
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Trajectory"/> class.
		/// </summary>
		public Trajectory() 
            : this("Name Not Provided", 0, 0, 0, 0)
		{

		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Trajectory" /> class.
        /// </summary>
        /// <param name="name">The name that this trajectory will go by.</param>
        /// <param name="apoapsis">The orbit's apoapsis.</param>
        /// <param name="periapsis">The orbit's periapsis.</param>
        /// <param name="inclination">The orbit's inclination.</param>
        /// <param name="requiredDeltaV">Gets or sets the required amount of delta-v to reach the specified orbit.</param>
        public Trajectory(string name, double apoapsis, double periapsis, double inclination, double requiredDeltaV)
        {
            Name = name.HandleNullOrWhiteSpace("Name Not Provided");
		    SetOrbitParameters(apoapsis, periapsis, inclination);
		    RequiredDeltaV = requiredDeltaV;
        }

        #endregion

	    private string name;
        private double apoapsis;
	    private double periapsis;
	    private double inclination;
        private double requiredDeltaV;

        #region Properties

        /// <summary>
        /// Gets or sets the name of this trajectory.
        /// </summary>
        public string Name
	    {
	        get { return this.name; }
	        set
	        {
	            this.name = value.HandleNullOrWhiteSpace("Invalid Name");
	            OnPropertyChanged(nameof(Name));
	        }
	    }

        /// <summary>
        /// Gets or sets the targeted apoapsis.
        /// </summary>
        public double Apoapsis
	    {
	        get { return this.apoapsis; }
	        set
	        {
	            if (value < 0) return;

	            this.apoapsis = value;
	            OnPropertyChanged(nameof(Apoapsis));
	        }
	    }

	    /// <summary>
	    /// Gets or sets the targeted periapsis.
	    /// </summary>
	    public double Periapsis
	    {
	        get { return this.periapsis; }
	        set
	        {
	            if (value < 0) return;

	            this.periapsis = value;
	            OnPropertyChanged(nameof(Periapsis));
	        }
	    }

	    /// <summary>
	    /// Gets or sets the targeted inclination.
	    /// </summary>
	    public double Inclination
	    {
	        get { return this.inclination; }
	        set
	        {
	            if (value > 180.0) return;
	            if (value < -180.0) return;

	            this.inclination = value;
	            OnPropertyChanged(nameof(Inclination));
	        }
	    }

        /// <summary>
        /// Gets or sets the required amount of delta-v to reach the targeted orbit.
        /// </summary>
        public double RequiredDeltaV
		{
			get { return this.requiredDeltaV; }
			set
			{
				if (value < 0) return;

				this.requiredDeltaV = value;
				OnPropertyChanged(nameof(RequiredDeltaV));
			}
		}

		#endregion

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
		    var nameUpper    = Name.ToUpper();
            var gtoString    = $"{Apoapsis:n0}km x {Periapsis:n0}km @ {Inclination:n1}° Inclination";
            var normalString = $"{Apoapsis:n0}km @ {Inclination}° Inclination";

            return nameUpper == "GTO" ? gtoString : normalString;
        }

        #endregion

	    /// <summary>
	    /// Allows for quick specification of the orbit's properties;
	    /// </summary>
	    /// <param name="apoapsis">The orbit's apoapsis.</param>
	    /// <param name="periapsis">The orbit's periapsis.</param>
	    /// <param name="inclination">The orbit's inclination.</param>
	    [SuppressMessage("ReSharper", "ParameterHidesMember")]
	    public void SetOrbitParameters(double apoapsis, double periapsis, double inclination)
	    {
	        Apoapsis    = apoapsis;
	        Periapsis   = periapsis;
	        Inclination = inclination;
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