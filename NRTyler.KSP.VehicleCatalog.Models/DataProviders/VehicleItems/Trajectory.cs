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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems
{
	/// <summary>
	/// Contains the parameters used to make up a specified orbit. Those include LEO, SSO, GTO and GEO.
	/// </summary>
	/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
	/// <seealso cref="IManeuver" />
	///	[Serializable]
	public class Trajectory : Orbit, INotifyPropertyChanged, IManeuver
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Trajectory"/> class.
		/// </summary>
		public Trajectory()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Trajectory"/> class.
		/// </summary>
		/// <param name="name">The name of the trajectory.</param>
		public Trajectory(string name = "Name Not Set") 
			: this(0, 0, 0, 0, name)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Trajectory" /> class.
		/// </summary>
		/// <param name="apoapsis">The trajectory's targeted apoapsis.</param>
		/// <param name="periapsis">The trajectory's targeted periapsis.</param>
		/// <param name="inclination">The trajectory's targeted inclination.</param>
		/// <param name="name">The name of the trajectory.</param>
		public Trajectory(double apoapsis, double periapsis, double inclination, string name = "Name Not Set") 
			: this(apoapsis, periapsis, inclination, 0, name)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Trajectory" /> class.
		/// </summary>
		/// <param name="apoapsis">The trajectory's targeted apoapsis.</param>
		/// <param name="periapsis">The trajectory's targeted periapsis.</param>
		/// <param name="inclination">The trajectory's targeted inclination.</param>
		/// <param name="requiredDeltaV">The required amount of delta v need to reach the targeted trajectory.</param>
		/// <param name="name">The name of the trajectory.</param>
		public Trajectory(double apoapsis, double periapsis, double inclination, double requiredDeltaV, string name = "Name Not Set")
		{
			Apoapsis       = apoapsis;
			Periapsis      = periapsis;
			Inclination    = inclination;
			RequiredDeltaV = requiredDeltaV;
			this.Name = name;
		}

		#endregion

		private double requiredDeltaV;
		private string name;

		#region Properties

		/// <summary>
		/// Gets or sets the required amount of delta v to reach the targeted trajectory.
		/// </summary>
		public double RequiredDeltaV
		{
			get { return this.requiredDeltaV; }
			set
			{
				if (value < 0) return;

				this.requiredDeltaV = value;
				OnPropertyChanged(nameof(this.RequiredDeltaV));
			}
		}

		public string Name
		{
			get { return this.name; }
			set
			{
				this.name = value.HandleNullOrWhiteSpace();
				OnPropertyChanged(nameof(this.Name));
			}
		}

		#endregion

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			var oldString = $"Name: {this.Name}@Apoapsis: {this.Apoapsis}@Periapsis: {this.Periapsis}@Inclination: {this.Inclination}@Required DeltaV: {this.RequiredDeltaV}";
			var newString = oldString.Replace("@", "\n");

			return newString;
		}

		#endregion

		#region INotifyPropertyChanged Members

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public new event PropertyChangedEventHandler PropertyChanged;

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