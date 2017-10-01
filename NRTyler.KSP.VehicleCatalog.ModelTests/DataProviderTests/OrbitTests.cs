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
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests
{
	[TestClass]
	public class OrbitTests
	{
		[TestMethod]
		public void Orbit_ToStringWithAxis()
		{
			//Arrange
			var orbit = new Orbit();
			orbit.Apoapsis = 105000;
			orbit.Periapsis = 85000;
			orbit.Inclination = 26.0;
			orbit.SemiMajorAxis = 190000;

			var oldString = $"Apoapsis: {105000}@Periapsis: {85000}@Inclination: {26.0}@Semi-Major Axis: {190000}";
			var expected = oldString.Replace("@", "\n");

			//Act
			var actual = orbit.ToString();

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Orbit_ToStringWithoutAxis()
		{
			//Arrange
			var orbit = new Orbit();
			orbit.Apoapsis = 194150;
			orbit.Periapsis = 96200;
			orbit.Inclination = 24.21;

			var oldString = $"Apoapsis: {194150}@Periapsis: {96200}@Inclination: {24.21}";
			var expected = oldString.Replace("@", "\n");

			//Act
			var actual = orbit.ToString();

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OrbitCorrectedInclination()
		{
			//Arrange
			var orbit = new Orbit();
			orbit.Apoapsis = 150000;
			orbit.Periapsis = 92000;
			orbit.Inclination = 265.19;
			
			var expected = 0;

			//Act
			var actual = orbit.Inclination;

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}