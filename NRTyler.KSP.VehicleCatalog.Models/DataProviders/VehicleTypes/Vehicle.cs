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
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleTypes
{
    /// <summary>
    /// The base class for any other type of vehicle available. This can be used by itself should you need a basic
    /// vehicle, or you're creating your own. Otherwise, I highly recommend using a more specific type of vehicle class.
    /// </summary>
    /// <seealso cref="IVehicle" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
	public class Vehicle : IVehicle, INotifyPropertyChanged
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Vehicle"/> class.
		/// </summary>
		public Vehicle() : this("Not Set")
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Vehicle"/> class.
		/// </summary>
		/// <param name="name">The name of the vehicle.</param>
		public Vehicle(string name)
		{
			VehicleType    = VehicleType.Undefined;
			StageInfo      = new SortedDictionary<int, Stage>();
			VehicleNotes   = new List<VehicleNote>();
			OptionalStages = new List<Stage>();

			Name = name;
		}

		#region Backing Fields
		 
		private string name;
		private double dryMass;
		private double wetMass;
		private double deltaV;
		private decimal price;
		private VehicleType vehicleType;
		private SortedDictionary<int, Stage> stageInfo;
		private List<VehicleNote> vehicleNotes;
		private List<Stage> optionalStages;

		#endregion

		#region Implementation of IVehicle

		/// <summary>
		/// Gets or sets the name of the vehicle.
		/// </summary>
		public string Name
		{
			get { return this.name; }
			set
			{
				this.name = value.HandleNullOrWhiteSpace();
				OnPropertyChanged(nameof(Name));
			}
		}

		/// <summary>
		/// Gets or sets the total dry mass of the vehicle.
		/// </summary>
		public double DryMass
		{
			get { return this.dryMass; }
			set
			{
				if (value < 0) return;

				this.dryMass = value;
				OnPropertyChanged(nameof(DryMass));
			}
		}

		/// <summary>
		/// Gets or sets the total wet mass of the vehicle.
		/// </summary>
		public double WetMass
		{
			get { return this.wetMass; }
			set
			{
				if (value < 0) return;

				this.wetMass = value;
				OnPropertyChanged(nameof(WetMass));
			}
		}

		/// <summary>
		/// Gets or sets the total amount of Delta V available to the vehicle.
		/// </summary>
		public double DeltaV
		{
			get { return this.deltaV; }
			set
			{
				if (value < 0) return;

				this.deltaV = value;
				OnPropertyChanged(nameof(DeltaV));
			}
		}

		/// <summary>
		/// Gets or sets the total price of the vehicle.
		/// </summary>
		public decimal Price
		{
			get { return this.price; }
			set
			{
				if (value < 0) return;
				this.price = value;
				OnPropertyChanged(nameof(Price));
			}
		}

		/// <summary>
		/// Gets or sets the type of the vehicle.
		/// </summary>
		public VehicleType VehicleType
		{
			get { return this.vehicleType; }
			set
			{
				this.vehicleType = value;
				OnPropertyChanged(nameof(VehicleType));
			}
		}

		/// <summary>
		/// Gets or sets the dictionary of stage information.
		/// </summary>
		public SortedDictionary<int, Stage> StageInfo
		{
			get { return this.stageInfo; }
			set
			{
				this.stageInfo = value;
				OnPropertyChanged(nameof(StageInfo));
			}
		}

		/// <summary>
		/// Gets or sets the dictionary of vehicle notes.
		/// </summary>
		public List<VehicleNote> VehicleNotes
		{
			get { return this.vehicleNotes; }
			set
			{
				this.vehicleNotes = value;
				OnPropertyChanged(nameof(VehicleNotes));
			}
		}

		/// <summary>
		/// Gets or sets any optional stages that the vehicle may have at its' disposal.
		/// </summary>
		public List<Stage> OptionalStages
		{
			get { return this.optionalStages; }
			set
			{
				this.optionalStages = value;
				OnPropertyChanged(nameof(OptionalStages));
			}
		}

		/// <summary>
		/// Calculates this vehicle's total delta-v.
		/// </summary>
		/// <returns>This Vehicle's DeltaV.</returns>
		public virtual double CalculateDeltaV()
		{
            // Assign the value to the "DeltaV" property.
		    DeltaV = DeltaVCalculator.CalculateVehicleDeltaV(this);

		    return DeltaV;
        }

		#endregion

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			var baseString = $"Name: {Name}@DryMass: {DryMass}@WetMass: {WetMass}@Delta-V: {DeltaV}@Price: {Price}@VehicleType: {VehicleType.GetStringValue()}";

			return baseString.Replace("@", "\n");
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
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

        #endregion
    }
}