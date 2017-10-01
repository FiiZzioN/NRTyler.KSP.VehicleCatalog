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
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems
{
	/// <summary>
	/// Contains pacification information such as the type and the amount of delta v required for such a maneuver.
	/// </summary>
	/// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
	/// <seealso cref="IManeuver" />
	[Serializable]
	public class PacificationOption : INotifyPropertyChanged, IManeuver
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="PacificationOption"/> class.
		/// </summary>
		public PacificationOption()
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PacificationOption"/> class.
		/// </summary>
		/// <param name="pacificationType">The pacification type.</param>
		public PacificationOption(PacificationType pacificationType) : this(pacificationType, 0)
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PacificationOption"/> class.
		/// </summary>
		/// <param name="pacificationType">The pacification type.</param>
		/// <param name="requiredDeltaV">The required delta v.</param>
		public PacificationOption(PacificationType pacificationType, double requiredDeltaV)
		{
			this.PacificationType = pacificationType;
			this.RequiredDeltaV = requiredDeltaV;
		}

		#endregion

		private PacificationType pacificationType;
		private double requiredDeltaV;

		#region Properties

		/// <summary>
		/// Gets or sets the pacification type.
		/// </summary>
		/// <value>The type of the pacification.</value>
		public PacificationType PacificationType
		{
			get { return this.pacificationType; }
			set
			{
				this.pacificationType = value;
				OnPropertyChanged(nameof(this.PacificationType));
			}
		}

		/// <summary>
		/// Gets or sets the required amount of delta v to accomplish the pacification type.
		/// </summary>
		/// <value>The required delta v.</value>
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

		#endregion

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			var oldString = $"Pacification Type: {StringLabel.GetLabel(this.PacificationType)}@Required DeltaV: {this.RequiredDeltaV}";
			var newString = oldString.Replace("@", "\n");

			return newString;
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