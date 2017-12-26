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

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.Common.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
	/// <summary>
	/// Holds the basic parameters that make up an orbit: Apoapsis, Periapsis, Inclination and Semi-Major Axis 
	/// </summary>
	/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
	[Serializable]
	public class Orbit : IOrbit, INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Orbit"/> class.
		/// </summary>
		public Orbit() : this(0, 0, 0)
		{
			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Orbit"/> class.
        /// </summary>
        /// <param name="apoapsis">The orbit's apoapsis.</param>
        /// <param name="periapsis">The orbit's periapsis.</param>
        /// <param name="inclination">The orbit's inclination.</param>
        public Orbit(double apoapsis, double periapsis, double inclination)
	    {
	        SetOrbitParameters(apoapsis, periapsis, inclination);
	    }

        #region Fields and Properties

        private double apoapsis;
        private double periapsis;
        private double inclination;

        /// <summary>
        /// Gets or sets the targeted apoapsis.
        /// </summary>
        public double Apoapsis
        {
            get { return this.apoapsis; }
            set
            {
                if (value < 0) return;
                //if (value < this.Periapsis) return;

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

        #endregion

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
	    {
	        var oldString = $"Apoapsis: {Apoapsis}@Periapsis: {Periapsis}@Inclination: {Inclination}";
	        return oldString.Replace("@", "\n");
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
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}