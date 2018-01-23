// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
// 
// Author           : Nicholas Tyler
// Created          : 01-21-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-21-2018
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.Services.Utilities
{
    public static class Cache
    {
        static Cache()
        {
            Families            = new HashSet<VehicleFamily>();
            LauncherCollections = new HashSet<LauncherCollection>();
            Launchers           = new HashSet<Launcher>();
        }

        public static HashSet<VehicleFamily> Families { get; }
        public static HashSet<LauncherCollection> LauncherCollections { get;}
        public static HashSet<Launcher> Launchers { get; }

        //public static void test()
        //{
        //    Families.cont   
        //}
    }
}