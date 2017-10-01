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
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleTypes;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests.VehicleTypes
{
	[TestClass]
	public class VehicleTests
	{
		[TestMethod]
		public void VehicleNameWhitespace()
		{
			//Arrange
			var vehicle  = new Vehicle();
			vehicle.Name = " ";

			var expected = "Invalid Title";

			//Act
			var actual = vehicle.Name;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleNameValid()
		{
			//Arrange
			var vehicle  = new Vehicle("My Vehicle");
			var expected = "My Vehicle";

			//Act
			var actual = vehicle.Name;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleWetMassMassValid()
		{
			//Arrange
			var vehicle     = new Vehicle();
			vehicle.WetMass = 965854;

			var expected = 965854;

			//Act
			var actual = vehicle.WetMass;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleWetMassMassInvalid()
		{
			//Arrange
			var vehicle     = new Vehicle();
			vehicle.WetMass = -3799;

			var expected = 0;

			//Act
			var actual = vehicle.WetMass;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleDryMassMassValid()
		{
			//Arrange
			var vehicle     = new Vehicle();
			vehicle.DryMass = 42500;

			var expected = 42500;

			//Act
			var actual = vehicle.DryMass;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleDryMassMasInvalid()
		{
			//Arrange
			var vehicle     = new Vehicle();
			vehicle.DryMass = -5874;

			var expected = 0;

			//Act
			var actual = vehicle.DryMass;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehiclePriceValid()
		{
			//Arrange
			var vehicle   = new Vehicle();
			vehicle.Price = 13252.3m;

			var expected = 13252.3m;

			//Act
			var actual = vehicle.Price;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehiclePriceInvalid()
		{
			//Arrange
			var vehicle   = new Vehicle();
			vehicle.Price = -352.3m;

			var expected = 0;

			//Act
			var actual = vehicle.Price;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleTypeAssigned()
		{
			//Arrange
			var vehicle         = new Vehicle();
			vehicle.VehicleType = VehicleType.Rover;

			var expected = VehicleType.Rover;

			//Act
			var actual = vehicle.VehicleType;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleStageInfoAddedAndSorted()
		{
			// Arrange
			var vehicle = new Vehicle();
			var stage   = new Stage();

			vehicle.StageInfo.Add(5, stage);
			vehicle.StageInfo.Add(2, stage);
			vehicle.StageInfo.Add(3, stage);

			var dictionary = new Dictionary<int, Stage>
			{
				{ 2, stage },
				{ 3, stage },
				{ 5, stage },
			};

			var expected = dictionary;

			// Act
			var actual = vehicle.StageInfo;

			// Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleNotesAdded()
		{
			//Arrange
			var vehicle = new Vehicle();
			var note = new VehicleNote("Just To Say Hi");
			note.Content = "These Unit Tests Are Very Dull";

			vehicle.VehicleNotes.Add(note);

			var expected = new List<string> { "Just To Say Hi", "These Unit Tests Are Very Dull" };

			//Act
			var actual = new List<string> { vehicle.VehicleNotes[0].Title , vehicle.VehicleNotes[0].Content };

			//Assert
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void CalulateVehicleDeltaV()
		{
			var vehicle = new Vehicle("DeltaV Testing");

			#region Stages

			var stageOne = new Stage()
			{
				DryMass = 1010,
				WetMass = 4010,
				SpecificImpulse = 340
			};

			var stageTwo = new Stage()
			{
				DryMass = 2050,
				WetMass = 8050,
				SpecificImpulse = 310
			};

			#endregion

			vehicle.StageInfo.Add(1, stageOne);
			vehicle.StageInfo.Add(2, stageTwo);

			var expected = 6689.53834561119;

			//Act
			var actual = vehicle.CalculateDeltaV();

			//Assert
			Assert.AreEqual(expected, actual, 0.0000000001);
		}

		[TestMethod]
		public void VehicleOptionalStages()
		{
			// Arrange
			var vehicle = new Vehicle();
			var stage   = new Stage(560, 5200, 305);

			vehicle.OptionalStages.Add(stage);

			var expected = stage;

			// Act
			var actual = vehicle.OptionalStages[0];

			// Assert
			Assert.AreEqual(expected, actual);
		}
	}
}