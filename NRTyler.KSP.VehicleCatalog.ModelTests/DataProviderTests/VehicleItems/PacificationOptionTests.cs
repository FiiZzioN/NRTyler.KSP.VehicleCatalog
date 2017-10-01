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
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests.VehicleItems
{
	[TestClass]
	public class PacificationOptionTests
	{
		[TestMethod]
		public void PacificationAssignment_Succeeded()
		{
			//Arrange
			var pacificationOption = new PacificationOption(PacificationType.GraveyardOrbit, 800);
			var expected = PacificationType.GraveyardOrbit;

			//Act
			var actual = pacificationOption.PacificationType;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void DeltaVAssignment_Succeeded()
		{
			//Arrange
			var pacificationOption = new PacificationOption(PacificationType.Deorbit, 422);
			var expected = 422;

			//Act
			var actual = pacificationOption.RequiredDeltaV;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void CatchNegativeDeltaVAssignment()
		{
			//Arrange
			var pacificationOption = new PacificationOption(PacificationType.Deorbit, -357);
			var expected = 0;

			//Act
			var actual = pacificationOption.RequiredDeltaV;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void PacificationOption_ToString()
		{
			//Arrange
			var pacificationOption = new PacificationOption(PacificationType.GraveyardOrbit, 501);

			var oldString = $"Pacification Type: {StringLabel.GetLabel(PacificationType.GraveyardOrbit)}@Required DeltaV: {501}";
			var expected = oldString.Replace("@", "\n");

			//Act
			var actual = pacificationOption.ToString();

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}