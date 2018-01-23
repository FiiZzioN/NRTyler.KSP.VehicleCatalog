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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.Services.Controllers
{
    public class LoadVehicleFamilyController
    {
        public LoadVehicleFamilyController(ApplicationSettings settings)
        {
            Settings = settings;
        }

        private ApplicationSettings Settings { get; }

        

    }
}