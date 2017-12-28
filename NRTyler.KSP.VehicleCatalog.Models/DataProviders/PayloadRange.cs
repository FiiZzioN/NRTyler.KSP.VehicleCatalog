// ***********************************************************************
// Assembly         : NRTyler.KSPManager.Models
//
// Author           : Nicholas Tyler
// Created          : 07-14-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-28-2017
//
// License          : MIT License
// ***********************************************************************

using NRTyler.CodeLibrary.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// <see cref="PayloadRange"/> holds information about what the 
    /// lightest and heaviest possible payload a vehicle can safely lift.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "PayloadRange")]
    public class PayloadRange : INotifyPropertyChanged
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadRange"/> class.
        /// </summary>
        public PayloadRange() : this (0, 0)
	    {
	        
	    }

        /// <summary>
        /// Initializes a new instance of the <see cref="PayloadRange"/> class.
        /// </summary>
        /// <param name="lightest">The lightest the payload can be in kilograms.</param>
        /// <param name="heaviest">The heaviest the payload can be in kilograms.</param>
        public PayloadRange(int lightest, int heaviest)
		{
			Lightest = lightest;
			Heaviest = heaviest;
		}

		private int lightest;
		private int heaviest;

		/// <summary>
		/// Gets or sets the lightest the payload can possibly be in kilograms.
		/// </summary>
		[DataMember]
		public int Lightest
		{
			get { return this.lightest; }
			set
			{
				if (value < 0) return;

				this.lightest = value;
				OnPropertyChanged(nameof(Lightest));
			}
		}

        /// <summary>
        /// Gets or sets the heaviest the payload can possibly be in kilograms.
        /// </summary>
        [DataMember]
        public int Heaviest
		{
			get { return this.heaviest; }
			set
			{
				if (value < 0) return;
		
				this.heaviest = value;
				OnPropertyChanged(nameof(Heaviest));
			}
		}

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
        {
            // Formats the weight values. If 'Heaviest' is "2500", 
            // the formated version would show up as "2,500".
            return $"{Lightest:n0}kg - {Heaviest:n0}kg";
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
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		} 

		#endregion
	}
}