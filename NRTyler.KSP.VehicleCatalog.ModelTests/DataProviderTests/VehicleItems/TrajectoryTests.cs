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
	public class TrajectoryTests
	{
		[TestMethod]
		public void TrajectoryRangeAssignment()
		{
			//Arrange
			var parameters = new Trajectory();

			parameters.Apoapsis = 200000;
			parameters.Periapsis = 120000;
			parameters.Inclination = -32.22;
			parameters.RequiredDeltaV = 6500;

			var parameterList = new List<double>()
			{
				parameters.Apoapsis,
				parameters.Periapsis,
				parameters.Inclination,
				parameters.RequiredDeltaV 
			};

			var expected = new List<double>()
			{
				200000,
				120000,
				-32.22,
				6500
			};

			//Act
			var actual = parameterList;

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TrajectoryHandledNegativeParameter()
		{
			//Arrange
			var parameters = new Trajectory(103500, -90000, 57.28, 7000);
			var parameterList = new List<double>()
			{
				parameters.Apoapsis,
				parameters.Periapsis,
				parameters.Inclination,
				parameters.RequiredDeltaV
			};

			var expected = new List<double>()
			{
				103500,
				0,
				57.28,
				7000
			};

			//Act
			var actual = parameterList;

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TrajectoryNameSet()
		{
			//Arrange
			var parameters = new Trajectory(11475000, 200000, 26.2, 7400, "GTO");

			var expected = "GTO";

			//Act
			var actual = parameters.Name;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TrajectoryNameCatchInvalidName()
		{
			//Arrange
			var parameters = new Trajectory(11475000, 200000, 26.2, 7400, " ");

			var expected = "Invalid Title";

			//Act
			var actual = parameters.Name;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Trajectory_ToString()
		{
			//Arrange
			var parameters = new Trajectory(320000, 318000, 22.36, 6850, "Random");
			var oldString = $"Name: {"Random"}@Apoapsis: {320000}@Periapsis: {318000}@Inclination: {22.36}@Required DeltaV: {6850}";

			var expected = oldString.Replace("@", "\n");

			//Act
			var actual = parameters.ToString();

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}