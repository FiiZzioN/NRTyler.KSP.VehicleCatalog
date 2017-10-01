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

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems
{
	/// <summary>
	/// Holds information about a given <see cref="Fairing"/> that a <see cref="INotifyPropertyChanged"/> has at its disposal.
	/// </summary>
	/// <seealso cref="System.ComponentModel" />
	[Serializable]
	public class Fairing : INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Fairing"/> class.
		/// </summary>
		public Fairing() : this(null, null, 0)
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Fairing"/> class.
		/// </summary>
		/// <param name="length">The length of the fairing.</param>
		/// <param name="diameter">The diameter of the fairing.</param>
		/// <param name="mass">The mass of the fairing.</param>
		public Fairing( double? length, double? diameter, double mass)
		{
			Length = length;
			Diameter = diameter;
			Mass = mass;
		}

		private double? length;
		private double? diameter;
		private double mass;

		/// <summary>
		/// Gets or sets the length of the fairing.
		/// </summary>
		public double? Length
		{
			get { return this.length; }
			set
			{
				if (value < 0) return;

				this.length = value;
				OnPropertyChanged(nameof(Length));
			}
		}

		/// <summary>
		/// Gets or sets the diameter of the fairing.
		/// </summary>
		public double? Diameter
		{
			get { return this.diameter; }
			set
			{
				if (value < 0) return;

				this.diameter = value;
				OnPropertyChanged(nameof(Diameter));
			}
		}

		/// <summary>
		/// Gets or sets the mass of the fairing.
		/// </summary>
		public double Mass
		{
			get { return this.mass; }
			set
			{
				if (value < 0) return;

				this.mass = value;
				OnPropertyChanged(nameof(Mass));
			}
		}

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			var newString = $"Length: {Length}@Diameter: {Diameter}@Mass: {Mass}";
			return newString.Replace("@", "\n");
		}

		#endregion

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