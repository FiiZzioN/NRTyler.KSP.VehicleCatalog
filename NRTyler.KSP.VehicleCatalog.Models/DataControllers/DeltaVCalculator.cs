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
using NRTyler.KSP.Common.Utilities;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
	/// <summary>
	/// Contains methods that help calculate the amount of delta-v a given object has at its' disposal.
	/// </summary>
	public class DeltaVCalculator : BasicDeltaVCalculator
    {
		public static double CalulateDeltaV(Stage stage)
		{
			if (stage == null) throw new ArgumentNullException(nameof(stage), @"The object being serialized can not be null!");
			
			return CalulateDeltaV(stage.DryMass, stage.WetMass, stage.SpecificImpulse);
		}

		/// <summary>
		/// Calculates a vehicle's total delta-v.
		/// </summary>
		/// <param name="vehicle">The vehicle whose delta-v needs to be calculated.</param>
		/// <returns>The <see cref="IVehicle"/>'s total DeltaV.</returns>
		/// <exception cref="System.ArgumentNullException">The <see cref="IVehicle"/> can't be null.</exception>
		public static double CalculateVehicleDeltaV(IVehicle vehicle)
		{
			if (vehicle == null) throw new ArgumentNullException(nameof(vehicle), @"The object being analyzed can not be null!");

			double totalDeltaV = 0;

			// Grab the vehicle's collection of stages if it has any to begin with.
			// If it doesn't, then we just return zero since we have nothing to work with.
			var stages = vehicle.StageInfo?.Values;
			if (stages == null) return totalDeltaV;

			// Clone the collection of stages into a LinkedList for easier manipulation. This
			// means we can access the stages before and after the stage we are currently focused on.
			var modifiedStages = new LinkedList<Stage>(stages);
			var node           = modifiedStages.First;
			
			// Cycle through the list until we hit the end.
			while (node != null)
			{
				var nodeValue = node.Value;
				var nextNode  = node.Next;
				
				// Gets the current stage's wet mass, and then calculate the current stage's delta-v.
				var currentWetMass = nodeValue.WetMass;
				    totalDeltaV   += nodeValue.CalculateDeltaV();

				if (nextNode != null)
				{
					// Get's the stage AFTER the one we were just focusing on.
					var nextNodeValue = nextNode.Value;

					// Adds the PREVIOUS stage's wet mass to the next stage's dry AND wet mass.
					// This is done so the proper delta-v values is calculated. It emulates all
					// of the useless mass that'd be on top of the stage that's doing the work.
					nextNodeValue.DryMass += currentWetMass;
					nextNodeValue.WetMass += currentWetMass;
				}
					
				node = node.Next;
			}

			return totalDeltaV;
		}
	}
}