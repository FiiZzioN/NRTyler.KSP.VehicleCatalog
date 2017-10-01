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

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests.VehicleItems
{
	[TestClass]
	public class FairingTests
	{
		[TestMethod]
		public void FairingConstructorOne()
		{
			//Arrange instantiation
			var fairing  = new Fairing();
			var expected = new List<double?> { null, null, 0 };

			//Act
			var actual = new List<double?> {  fairing.Length, fairing.Diameter, fairing.Mass };

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FairingConstructorTwo()
		{
			//Arrange instantiation
			var fairing  = new Fairing(12.2, 2.5, 2.2);
			var expected = new List<double?> { 12.2, 2.5, 2.2 };

			//Act
			var actual = new List<double?> { fairing.Length, fairing.Diameter, fairing.Mass };

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FairingInvlaidEntries()
		{
			//Arrange instantiation
			var fairing = new Fairing(-1.1, -5.1, -0.9);
			var expected = new List<double?> { null, null, 0 };

			//Act
			var actual = new List<double?> { fairing.Length, fairing.Diameter, fairing.Mass };

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}