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

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.ModelTests;
using NRTyler.KSP.VehicleCatalog.Services.Utilities;

namespace NRTyler.KSP.VehicleCatalog.Console
{
    public class Program : CatalogInitializer
    {
        private static void Main()
        {
            var defaultObject = default(VehicleFamily);
            var normalObject = new VehicleFamily();


            WriteLine(defaultObject.Name);          
        }

        private static void WriteLine(object obj = null)
        {
            System.Console.WriteLine(obj);
        }
    }
}
