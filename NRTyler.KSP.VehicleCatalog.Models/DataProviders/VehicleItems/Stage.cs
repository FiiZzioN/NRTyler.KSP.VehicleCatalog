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
using NRTyler.KSP.VehicleCatalog.Models.Interfaces;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems
{
	[Serializable]
	public class Stage : IHasDeltaV, IValuable, INotifyPropertyChanged
	{
		public Stage() : this(0, 0, 0)
		{
			
		}

		public Stage(double dryMass, double wetMass, double? specificImpulse)
		{
			DryMass         = dryMass;
			WetMass         = wetMass;
			SpecificImpulse = specificImpulse;
			CalculateDeltaV();
		}


		private double wetMass;
		private double dryMass;
		private double? specificImpulse;
		private double deltaV;
		private decimal price;

		/// <summary>
		/// Gets or sets the wet mass of the stage.
		/// </summary>
		public double WetMass
		{
			get { return this.wetMass; }
			set
			{
				if (value < 0) return;

				this.wetMass = value;
				OnPropertyChanged(nameof(this.WetMass));
			}
		}

		/// <summary>
		/// Gets or sets the dry mass of the stage.
		/// </summary>
		public double DryMass
		{
			get { return this.dryMass; }
			set
			{
				if (value < 0) return;

				this.dryMass = value;
				OnPropertyChanged(nameof(DryMass));
			}
		}

		/// <summary>
		/// Gets or sets the engine's specific impulse, if the stage even has an engine.
		/// </summary>
		public double? SpecificImpulse
		{
			get { return this.specificImpulse; }
			set
			{
				if (value < 0) return;

				this.specificImpulse = value;
				SetDeltaV();
				OnPropertyChanged(nameof(SpecificImpulse));
			}
		}

		/// <summary>
		/// Gets or sets the stage's delta-v.
		/// </summary>
		public double DeltaV
		{
			get { return this.deltaV; }
			set
			{
				if (value < 0) return;

				this.deltaV = value;
				OnPropertyChanged(nameof(DeltaV));
			}
		}

		/// <summary>
		/// Gets or sets the price of an object.
		/// </summary>
		public decimal Price
		{
			get { return this.price; }
			set
			{
				if (value < 0) return;

				this.price = value;
				OnPropertyChanged(nameof(Price));
			}
		}

		/// <summary>
		/// Calculates the stage's total delta-v.
		/// </summary>
		/// <returns>System.Double.</returns>
		public double CalculateDeltaV()
		{
			return DeltaVCalculator.CalulateDeltaV(this);
		}

		private void SetDeltaV()
		{
			if (SpecificImpulse != null) CalculateDeltaV();
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

        /*
	    #region Implementation of IXmlSerializable

	    public XmlSchema GetSchema()
	    {
	        return null;
	    }

	    public void ReadXml(XmlReader reader)
	    {
	        if (!reader.IsEmptyElement)
	            WetMass = Convert.ToDouble(reader.GetAttribute(nameof(WetMass)));

	        if (!reader.IsEmptyElement)
	            DryMass = Convert.ToDouble(reader.GetAttribute(nameof(DryMass)));

	        if (!reader.IsEmptyElement)
	            SpecificImpulse = Convert.ToDouble(reader.GetAttribute(nameof(SpecificImpulse)));

	        if (!reader.IsEmptyElement)
	            DeltaV = Convert.ToDouble(reader.GetAttribute(nameof(DeltaV)));

	        if (!reader.IsEmptyElement)
	            Price = Convert.ToDecimal(reader.GetAttribute(nameof(Price)));
        }

	    public void WriteXml(XmlWriter writer)
	    {
	        writer.WriteAttributeString(nameof(WetMass), WetMass.ToString());
	        writer.WriteAttributeString(nameof(DryMass), DryMass.ToString());
	        writer.WriteAttributeString(nameof(SpecificImpulse), SpecificImpulse.ToString());
	        writer.WriteAttributeString(nameof(DeltaV), DeltaV.ToString());
	        writer.WriteAttributeString(nameof(Price),   Price.ToString());
        }

        #endregion
        */
    }
}