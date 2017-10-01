// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-01-2017
//
// License          : MIT License
// ***********************************************************************

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleTypes;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests.VehicleTypes
{
	[TestClass]
	public class LaunchVehicleTests
	{

		[TestMethod]
		public void VehicleFairings()
		{
			// Arrange
			var vehicle = new LaunchVehicle();
			var fairing = new Fairing(6.3, 1.5, 220);

			vehicle.Fairings.Add("M", fairing);

			var expectedName     = "M";
			var expectedLength   = 6.3;
			var expectedDiameter = 1.5;
			var expectedMass     = 220;

			//Act
			var vFairings = vehicle.Fairings;

			var actualName     = vFairings.Keys  .ElementAt(0);
			var actualLength   = vFairings.Values.ElementAt(0).Length;
			var actualDiameter = vFairings.Values.ElementAt(0).Diameter;
			var actualMass     = vFairings.Values.ElementAt(0).Mass;

			//Assert
			Assert.AreEqual(expectedName, actualName);
			Assert.AreEqual(expectedLength, actualLength);
			Assert.AreEqual(expectedDiameter, actualDiameter);
			Assert.AreEqual(expectedMass, actualMass);
		}

		[TestMethod]
		public void VehiclePacificationOption()
		{
			//Arrange
			var vehicle = new LaunchVehicle();
			var option  = new PacificationOption(PacificationType.GraveyardOrbit, 325);

			vehicle.PacificationOptions.Add("GraveYard", option);

			var expectedName = "GraveYard";
			var expectedType = PacificationType.GraveyardOrbit;
			var expectedDeltaV = 325;

			//Act
			var vOptions = vehicle.PacificationOptions;

			var actualName   = vOptions.Keys  .ElementAt(0);
			var actualType   = vOptions.Values.ElementAt(0).PacificationType;
			var actualDeltaV = vOptions.Values.ElementAt(0).RequiredDeltaV;

			//Assert
			Assert.AreEqual(expectedName, actualName);
			Assert.AreEqual(expectedType, actualType);
			Assert.AreEqual(expectedDeltaV, actualDeltaV);
		}

		[TestMethod]
		public void VehicleCapability()
		{
			//Arrange
			var range      = new PayloadRange(1250, 5000);
			var trajectory = new Trajectory(11475000, 150000, 22.0, 7400, "GTO");

			var vehicle           = new LaunchVehicle();
			var vehicleCapability = new VehicleCapability(range, trajectory);

			vehicle.Capability.Add(trajectory.Name, vehicleCapability);

			var expectedName       = "GTO";
			var expectedRange      = range;
			var expectedTrajectory = trajectory;

			//Act
			var vCapability = vehicle.Capability;

			var actualName       = vCapability.Keys  .ElementAt(0);
			var actualRange      = vCapability.Values.ElementAt(0).PayloadRange;
			var actualTrajectory = vCapability.Values.ElementAt(0).Trajectory;

			//Assert
			Assert.AreEqual(expectedName, actualName);
			Assert.AreEqual(expectedRange, actualRange);
			Assert.AreEqual(expectedTrajectory, actualTrajectory);
		}
	}
}