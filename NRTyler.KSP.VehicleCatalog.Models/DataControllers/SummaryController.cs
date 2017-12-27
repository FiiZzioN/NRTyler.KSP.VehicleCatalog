// ************************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
// 
// Author           : Nicholas Tyler
// Created          : 12-27-2017
// 
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-27-2017
// 
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NRTyler.KSP.VehicleCatalog.Models.DataControllers
{
    public class SummaryController<T> : INotifyPropertyChanged
    {
        public SummaryController(VehicleFamily<T> vehicleFamily)
        {
            VehicleFamily = vehicleFamily;
        }

        private VehicleFamily<T> vehicleFamily;

        public VehicleFamily<T> VehicleFamily
        {
            get { return this.vehicleFamily; }
            set
            {
                if (value == null) return;

                this.vehicleFamily = value;
                OnPropertyChanged(nameof(VehicleFamily));
            }
        }

        /// <summary>
        /// Gets the number of versions of vehicles in the vehicle family.
        /// </summary>
        public int GetNumberOfVersions()
        {
            return VehicleFamily.VehicleVersions.Count;
        }

        public PriceSummary GetPriceSummary()
        {
            decimal cheapest;
            decimal mostExpensive;

            foreach (var vehicle in VehicleFamily.VehicleVersions)
            {
                
            }









            return summary;
        }














        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}