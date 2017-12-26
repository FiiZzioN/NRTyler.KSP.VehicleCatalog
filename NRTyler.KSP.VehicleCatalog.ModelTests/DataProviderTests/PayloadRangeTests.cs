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

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests
{
	[TestClass]
	public class PayloadRangeTests
	{
		[TestMethod]
		public void RangeAssignment_Succeeded()
		{
			//Arrange
			var range = new PayloadRange(500, 5000);
			var rangeList = new List<int>
			{
				range.Lightest,
				range.Heaviest
			};

			var expected = new List<int> { 500, 5000 };

			//Act
			var actual = rangeList;

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void NegativeParameter_Caught()
		{
			//Arrange
			var range = new PayloadRange(-50, 350);
			var rangeList = new List<int>
			{
				range.Lightest,
				range.Heaviest
			};

			var expected = new List<int> { 0, 350 };

			//Act
			var actual = rangeList;

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void PayloadRange_ToString()
		{
			//Arrange
			var range = new PayloadRange(2500, 5000);
			var expected = "2,500kg - 5,000kg";

			//Act
			var actual = range.ToString();

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}