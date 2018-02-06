// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
//
// Author           : Nicholas Tyler
// Created          : 01-21-2018
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 02-02-2018
//
// License          : MIT License
// ***********************************************************************

using System;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;
using NRTyler.KSP.VehicleCatalog.Services.Repositories;

namespace NRTyler.KSP.VehicleCatalog.Services.Cache
{
    public static class Cache
    {
        static Cache()
        {
            var appSettingsRepo = new ApplicationSettingsRepo($"{Environment.CurrentDirectory}");
            ApplicationSettings = appSettingsRepo.Retrieve($"{ApplicationSettings.FileName}");

            FamilyCacheController = new VehicleFamilyCache(ApplicationSettings);
        }

        public static ApplicationSettings ApplicationSettings { get; }
        public static VehicleFamilyCache FamilyCacheController { get; }

    }
}