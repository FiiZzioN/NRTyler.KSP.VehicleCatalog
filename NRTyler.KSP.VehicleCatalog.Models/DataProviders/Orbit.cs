// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-01-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
	/// <summary>
	/// Holds the basic parameters that make up an orbit: Apoapsis, Periapsis, Inclination and Semi-Major Axis 
	/// </summary>
	/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
	[Serializable]
	public class Orbit : INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Orbit"/> class.
		/// </summary>
		public Orbit()
		{
			
		}

		private double apoapsis;
		private double periapsis;
		private double inclination;
		private double? semiMajorAxis;

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

		/// <summary>
		/// Gets or sets the targeted semi-major axis.
		/// </summary>
		public double? SemiMajorAxis
		{
			get { return this.semiMajorAxis; }
			set
			{
				//if (value < 0) return;

				this.semiMajorAxis = value;
				OnPropertyChanged(nameof(SemiMajorAxis));
			}
		}

		#region INotifyPropertyChanged Members

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			var noSMA   = $"Apoapsis: {Apoapsis}@Periapsis: {Periapsis}@Inclination: {Inclination}";
			var withSMA = $"Apoapsis: {Apoapsis}@Periapsis: {Periapsis}@Inclination: {Inclination}@Semi-Major Axis: {SemiMajorAxis}";

			return this.SemiMajorAxis == null ? noSMA.Replace("@", "\n") : withSMA.Replace("@", "\n");
		}

		#endregion

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