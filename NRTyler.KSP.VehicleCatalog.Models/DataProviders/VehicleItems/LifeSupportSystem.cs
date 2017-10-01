// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Models
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 10-01-2017
//
// License          : MIT License
// ***********************************************************************

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders.Settings;
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems
{
    /// <summary>
    /// Holds information about how many days of provision and waste storage are available. Also has access to 
    /// the life support and base game settings should the user want to customize or add their preferred values.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    public class LifeSupportSystem : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LifeSupportSystem" /> class.
        /// </summary>
        /// <param name="crewedVehicle">The crewed vehicle.</param>
        /// <param name="baseGameSettings">The base game settings.</param>
        /// <param name="lifeSupportSettings">The life support settings.</param>
        public LifeSupportSystem(ICrewable crewedVehicle, BaseGameSettings baseGameSettings, LifeSupportSettings lifeSupportSettings)
        {
            Vehicle               = crewedVehicle;
            LifeSupportSettings   = lifeSupportSettings;
            BaseGameSettings      = baseGameSettings;
            ProvisionsStorage     = new ProvisionsStorage();
            WasteStorage          = new WasteStorage();
            LifeSupportCalculator = new LifeSupportCalculator(crewedVehicle);
        }

        private double daysOfFood;
		private double daysOfWater;
		private double daysOfOxygen;
		private double daysOfElectricity;
		private double daysOfWasteStorage;
		private double daysOfWasteWaterStorage;
		private double daysOfCO2Storage;

        /// <summary>
        /// Gets the crewable vehicle.
        /// </summary>
        public ICrewable Vehicle { get; }

        /// <summary>
        /// Gets the life support system.
        /// </summary>
        public ProvisionsStorage ProvisionsStorage { get; }

        /// <summary>
        /// Gets the waste storage system.
        /// </summary>
        public WasteStorage WasteStorage { get; }

        /// <summary>
        /// Gets the base game settings.
        /// </summary>
        public BaseGameSettings BaseGameSettings { get; }

        /// <summary>
        /// Gets the life support settings.
        /// </summary>
        public LifeSupportSettings LifeSupportSettings { get; }

        /// <summary>
        /// Gets the life support calculator.
        /// </summary>
        public LifeSupportCalculator LifeSupportCalculator { get; }

        #region Provision Members

        /// <summary>
        /// Gets or sets how many days worth of food is available.
        /// </summary>
        public double DaysOfFood
        {
            get
            {
                var unitsPerDay = LifeSupportSettings.FoodPerDay;
                var storage = ProvisionsStorage.TotalFoodStored;

                DaysOfFood = LifeSupportCalculator.CalculateLifeSupportResources(unitsPerDay, storage);

                return this.daysOfFood;
            }
            set
            {
                if (value < 0) return;

                this.daysOfFood = value;
                OnPropertyChanged(nameof(DaysOfFood));
            }
        }

        /// <summary>
        /// Gets or sets how many days worth of water is available.
        /// </summary>
        public double DaysOfWater
        {
            get
            {
                var unitsPerDay = LifeSupportSettings.WaterPerDay;
                var storage = ProvisionsStorage.TotalWaterStored;

                DaysOfWater = LifeSupportCalculator.CalculateLifeSupportResources(unitsPerDay, storage);

                return this.daysOfWater;
            }
            set
            {
                if (value < 0) return;

                this.daysOfWater = value;
                OnPropertyChanged(nameof(DaysOfWater));
            }
        }

        /// <summary>
        /// Gets or sets how many days worth of oxygen is available.
        /// </summary>
        public double DaysOfOxygen
        {
            get
            {
                var unitsPerDay = LifeSupportSettings.OxygenPerDay;
                var storage = ProvisionsStorage.TotalOxygenStored;

                DaysOfOxygen = LifeSupportCalculator.CalculateLifeSupportResources(unitsPerDay, storage);

                return this.daysOfOxygen;
            }
            set
            {
                if (value < 0) return;

                this.daysOfOxygen = value;
                OnPropertyChanged(nameof(DaysOfOxygen));
            }
        }


        /// <summary>
        /// Gets or sets how many days worth of electricity is available.
        /// </summary>
        public double DaysOfElectricity
        {
            get
            {
                DaysOfElectricity = LifeSupportCalculator.CalculateElectricity();

                return this.daysOfElectricity;
            }
            set
            {
                if (value < 0) return;

                this.daysOfElectricity = value;
                OnPropertyChanged(nameof(DaysOfElectricity));
            }
        }

        #endregion

        #region Waste Members

        /// <summary>
        /// Gets or sets how many days worth of waste storage is available.
        /// </summary>
        public double DaysOfWasteStorage
        {
            get
            {
                var unitsPerDay = LifeSupportSettings.WastePerDay;
                var storage     = WasteStorage.WasteUnits;

                DaysOfWasteStorage = LifeSupportCalculator.CalculateLifeSupportResources(unitsPerDay, storage);

                return this.daysOfWasteStorage;
            }
            set
            {
                if (value < 0) return;

                this.daysOfWasteStorage = value;
                OnPropertyChanged(nameof(DaysOfWasteStorage));                
            }
        }

        /// <summary>
        /// Gets or sets how many days worth of waste water storage is available.
        /// </summary>
        public double DaysOfWasteWaterStorage
        {
            get
            {
                var unitsPerDay = LifeSupportSettings.WasteWaterPerDay;
                var storage     = WasteStorage.WasteWaterUnits;

                DaysOfWasteWaterStorage = LifeSupportCalculator.CalculateLifeSupportResources(unitsPerDay, storage);

                return this.daysOfWasteWaterStorage;
            }
            set
            {
                if (value < 0) return;

                this.daysOfWasteWaterStorage = value;
                OnPropertyChanged(nameof(DaysOfWasteWaterStorage));
            }
        }

        /// <summary>
        /// Gets or sets how many days worth of CO2 storage is available.
        /// </summary>
        public double DaysOfCO2Storage
        {
            get
            {
                var unitsPerDay = LifeSupportSettings.CO2PerDay;
                var storage     = WasteStorage.CO2Units;

                DaysOfCO2Storage = LifeSupportCalculator.CalculateLifeSupportResources(unitsPerDay, storage);

                return this.daysOfCO2Storage;
            }
            set
            {
                if (value < 0) return;

                this.daysOfCO2Storage = value;
                OnPropertyChanged(nameof(DaysOfCO2Storage));
            }
        }

        #endregion

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
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}