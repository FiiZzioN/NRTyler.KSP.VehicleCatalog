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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleTypes;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests.VehicleItems
{
	[TestClass]
	public class PayloadTests
	{
		[TestMethod]
		public void ConstructorOne()
		{
			//Arrange
			var vehicle  = new Vehicle("Test Payload");
			var payload  = new Payload(vehicle);
			var expected = payload.Vehicle;

			//Act
			var actual = vehicle;

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}