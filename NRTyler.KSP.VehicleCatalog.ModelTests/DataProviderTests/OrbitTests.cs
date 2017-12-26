// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-20-2017
//
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests
{
	[TestClass]
	public class OrbitTests
	{
	    [TestMethod]
	    public void OrbitLowInclination()
	    {
	        //Arrange
	        var orbit = new Orbit
	        {
	            Apoapsis = 150000,
	            Periapsis = 92000,
	            Inclination = -195.2
	        };

	        var expected = 0;

	        //Act
	        var actual = orbit.Inclination;

	        //Assert
	        Assert.AreEqual(expected, actual);
	    }

        [TestMethod]
		public void OrbitHighInclination()
		{
			//Arrange
		    var orbit = new Orbit
		    {
		        Apoapsis    = 150000,
		        Periapsis   = 92000,
		        Inclination = 265.19
		    };

		    var expected = 0;

			//Act
			var actual = orbit.Inclination;

			//Assert
			Assert.AreEqual(expected, actual);
	    }

	    [TestMethod]
	    public void Orbit_ToString()
	    {
	        //Arrange
	        var orbit = new Orbit
	        {
	            Apoapsis = 105000,
	            Periapsis = 85000,
	            Inclination = 26.0,
	        };

	        var oldString = $"Apoapsis: {105000}@Periapsis: {85000}@Inclination: {26.0}";
	        var expected = oldString.Replace("@", "\n");

	        //Act
	        var actual = orbit.ToString();

	        //Assert
	        Assert.AreEqual(expected, actual);
	    }

    }
}