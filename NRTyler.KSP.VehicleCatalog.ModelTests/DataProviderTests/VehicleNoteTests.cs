// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-23-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests
{
	[TestClass]
	public class VehicleNoteTests
	{
		[TestMethod]
		public void VehicleNoteTitleNotNull()
		{
			//Arrange
			var vehicleNote = new Note();

			var expected = "Invalid Title";

			//Act
			var actual = vehicleNote.Title;
			Console.WriteLine(vehicleNote.ToString());

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleNoteTitleValid()
		{
			//Arrange
			var vehicleNote = new Note("Best Note");

			var expected = "Best Note";

			//Act
			var actual = vehicleNote.Title;

			//Assert
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void VehicleNoteContentSet()
		{
			//Arrange
			var vehicleNote = new Note();
			vehicleNote.Body = "This is a note. Maybe note a great one, but it's mine.";

			var expected = "This is a note. Maybe note a great one, but it's mine.";

			//Act
			var actual = vehicleNote.Body;

			//Assert
			Assert.AreEqual(expected, actual);
		}
	}
}