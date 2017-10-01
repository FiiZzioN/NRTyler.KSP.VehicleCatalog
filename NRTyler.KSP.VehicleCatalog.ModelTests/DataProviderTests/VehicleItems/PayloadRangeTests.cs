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
			var range = new PayloadRange(400, 1000);
			var oldString = $"Lightest: {400}@Heaviest: {1000}";

			var expected = oldString.Replace("@", "\n");

			//Act
			var actual = range.ToString();

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}