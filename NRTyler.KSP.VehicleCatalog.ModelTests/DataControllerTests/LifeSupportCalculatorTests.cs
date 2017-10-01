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
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.Settings;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleTypes;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataControllerTests
{
	[TestClass]
	public class LifeSupportCalculatorTests
	{


		public ICrewable GenerateCrewedVehicle(int numberOfKerbals, double dayLengthModifier)
		{
			var lifeSettings                 = new LifeSupportSettings();
			var baseSettings                 = new BaseGameSettings();
			var vehicle                      = new CrewedVehicle(baseSettings, lifeSettings);

		    baseSettings.DayLengthMultiplier = dayLengthModifier;
            vehicle.NumberOfCrew             = numberOfKerbals;

			return vehicle;
		}
	}
}
