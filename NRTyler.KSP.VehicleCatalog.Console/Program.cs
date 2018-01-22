// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Console
//
// Author           : Nicholas Tyler
// Created          : 01-12-2018
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-12-2018
//
// License          : MIT License
// ***********************************************************************

using System;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.ModelTests;

namespace NRTyler.KSP.VehicleCatalog.Console
{
    public class Program : CatalogInitializer
    {
        private static void Main()
        {
            var family = new VehicleFamily("Muh Fam");
            var controller = new VehicleFamilyController(family);
            var seperator = "---------";

            WriteLine($"Family Guid: {family.GlobalIdentifier}");
            WriteLine(seperator);

            var launcher1 = new Launcher("Proton") { Price = 100 };
            var launcher2 = new Launcher("Soyuz")  { Price = 200 };
            var launcher3 = new Launcher("Angara") { Price = 300 };

            var collection = new LauncherCollection();
            //collection.Launchers.Add(launcher1);
            //collection.Launchers.Add(launcher2);
            //collection.Launchers.Add(launcher3);

            //foreach (var launcher in collection.Launchers)
            //{
            //    WriteLine($" Name: {launcher.Name}");
            //    WriteLine($"Price: {launcher.Price}");
            //    WriteLine(seperator);
            //}

            controller.Add(collection);

            WriteLine(collection.RootIdentifier);
            WriteLine(seperator);

            controller.RemoveRange(typeof(LauncherCollection), 0, 1);

            WriteLine(collection.RootIdentifier);
            WriteLine(seperator);
        }

        private static void WriteLine(object obj = null)
        {
            System.Console.WriteLine(obj);
        }
    }
}
