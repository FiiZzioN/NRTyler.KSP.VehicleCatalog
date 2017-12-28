// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-26-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.Common.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Contains the parameters used to make up a specified orbit. Those include LEO, SSO, GTO and GEO.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "Trajectory")]
	public class Trajectory : IOrbit, INotifyPropertyChanged
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Trajectory"/> class.
		/// </summary>
		public Trajectory() : this(OrbitType.Undefined, 0, 0, 0, 0)
		{

		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Trajectory" /> class.
        /// </summary>
        /// <param name="orbitType">The type of this trajectory.</param>
        /// <param name="apoapsis">The orbit's apoapsis in kilometers.</param>
        /// <param name="periapsis">The orbit's periapsis in kilometers.</param>
        /// <param name="inclination">The orbit's inclination.</param>
        /// <param name="requiredDeltaV">Gets or sets the required amount of delta-v in m/s to reach the specified orbit.</param>
        public Trajectory(OrbitType orbitType, double apoapsis, double periapsis, double inclination, double requiredDeltaV)
        {
            OrbitType = orbitType;
		    SetOrbitParameters(apoapsis, periapsis, inclination);
		    RequiredDeltaV = requiredDeltaV;
        }

        #endregion

	    private OrbitType orbitType;
        private double apoapsis;
	    private double periapsis;
	    private double inclination;
        private double requiredDeltaV;

        #region Properties

        /// <summary>
        /// Gets or sets the type of this trajectory.
        /// </summary>
        [DataMember]
        public OrbitType OrbitType
        {
	        get { return this.orbitType; }
	        set
	        {
	            this.orbitType = value;
	            OnPropertyChanged(nameof(OrbitType));
	        }
	    }

        /// <summary>
        /// Gets or sets the targeted apoapsis in kilometers.
        /// </summary>
        [DataMember]
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
        /// Gets or sets the targeted periapsis in kilometers.
        /// </summary>
        [DataMember]
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
        [DataMember]
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
        /// Gets or sets the required amount of delta-v in m/s to reach the targeted orbit.
        /// </summary>
        [DataMember]
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
		    var nameUpper    = OrbitType.GetLabel().ToUpper();
            var gtoString    = $"{Apoapsis:n0}km x {Periapsis:n0}km @ {Inclination:n1}° Inclination";
            var normalString = $"{Apoapsis:n0}km @ {Inclination}° Inclination";

            return nameUpper == "GTO" ? gtoString : normalString;
        }

        #endregion

        /// <summary>
        /// Allows for quick specification of the orbit's properties;
        /// </summary>
        /// <param name="apoapsis">The orbit's apoapsis in kilometers.</param>
        /// <param name="periapsis">The orbit's periapsis in kilometers.</param>
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