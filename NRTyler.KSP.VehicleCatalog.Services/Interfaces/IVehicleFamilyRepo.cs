// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Services
// 
// Author           : Nicholas Tyler
// Created          : 01-05-2018
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-05-2018
// 
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRTyler.CodeLibrary.Interfaces.Generic;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;

namespace NRTyler.KSP.VehicleCatalog.Services.Interfaces
{
    public interface IVehicleFamilyRepo : IDataContractRepository<VehicleFamily>
    {
        
    }
}