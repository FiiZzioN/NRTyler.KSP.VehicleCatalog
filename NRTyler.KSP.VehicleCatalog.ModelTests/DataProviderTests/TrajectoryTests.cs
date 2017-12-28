// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-21-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests
{
	[TestClass]
	public class TrajectoryTests
	{
        /*
		[TestMethod]
		public void TrajectoryRangeAssignment()
		{
			//Arrange
		    var parameters = new Trajectory
		    {
		        Apoapsis       = 200,
		        Periapsis      = 120,
		        Inclination    = -32.22,
		        RequiredDeltaV = 6500
		    };

		    var parameterList = new List<double>()
			{
				parameters.Apoapsis,
				parameters.Periapsis,
				parameters.Inclination,
				parameters.RequiredDeltaV 
			};

			var expected = new List<double>()
			{
				200,
				120,
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
			var parameters = new Trajectory(103, -90, 57.28, 7000);
			var parameterList = new List<double>()
			{
				parameters.Apoapsis,
				parameters.Periapsis,
				parameters.Inclination,
				parameters.RequiredDeltaV
			};

			var expected = new List<double>()
			{
				103,
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
			var parameters = new Trajectory(11475, 200, 26.2, 7400, "GTO");
			var expected = "GTO";

			//Act
			var actual = parameters.OrbitType;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void TrajectoryNameCatchInvalidName()
		{
			//Arrange
			var parameters = new Trajectory(11475, 200, 26.2, 7400, " ");
			var expected = "Invalid Title";

			//Act
			var actual = parameters.OrbitType;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void Trajectory_GTO_ToString()
		{
			//Arrange
			var parameters = new Trajectory(11475, 350, 10, 7600, "GTO");
            var expected = "11,475km x 350km @ 10.0° Inclination";

            //Act
            var actual = parameters.ToString();

			//Assert
			Assert.AreEqual(expected, actual);
	    }

	    [TestMethod]
	    public void Trajectory_Normal_ToString()
	    {
	        //Arrange
	        var parameters = new Trajectory(250, 249, 28.7, 6100, "LEO");
	        var expected = "250km @ 28.7° Inclination";

	        //Act
	        var actual = parameters.ToString();

	        //Assert
	        Assert.AreEqual(expected, actual);
	    }
        */
    }
}