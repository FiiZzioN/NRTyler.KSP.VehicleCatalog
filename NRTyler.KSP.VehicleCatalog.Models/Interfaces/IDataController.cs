// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 12-23-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-23-2017
// 
// License          : MIT License
// ***********************************************************************

namespace NRTyler.KSP.VehicleCatalog.Models.Interfaces
{
    /// <summary>
    /// Indicates that a data controller implements CRUD operations.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataController<T>
    {
        void Create(T obj);
        T Retrieve(string key);
        void Update(T obj);
        void Delete(string key);
    }
}