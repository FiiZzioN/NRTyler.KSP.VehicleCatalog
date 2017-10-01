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

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests
{
	[TestClass]
	public class VehicleCapabilityTests
	{
		[TestMethod]
		public void VehicleCapabilityConstructor()
		{
			//Arrange
			var payloadRange      = new PayloadRange(250, 800);
			var trajectory        = new Trajectory(120000, 110000, 33.2, "LEO");
			var vehicleCapability = new VehicleCapability(payloadRange, trajectory);

			var expected = new List<object> { payloadRange, trajectory};

			//Act
			var actual = new List<object> { vehicleCapability.PayloadRange, vehicleCapability.Trajectory };

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}