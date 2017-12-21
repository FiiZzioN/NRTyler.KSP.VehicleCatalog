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
using NRTyler.CodeLibrary.Extensions;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders.VehicleItems
{
	[Serializable]
	public class VehicleNote : INotifyPropertyChanged
	{
		public VehicleNote()
		{
			this.Title = null;
			this.Content = String.Empty;
		}

		public VehicleNote(string title)
		{
			this.Title = title;
		}

		private string title;
		private string content;

		public string Title
		{
			get { return this.title; }
			set
			{
				this.title = value.HandleNullOrWhiteSpace();
				OnPropertyChanged(nameof(this.Title));
			}
		}


		public string Content
		{
			get { return this.content; }
			set
			{
				this.content = value;
				OnPropertyChanged(nameof(this.Content));
			}
		}

		#region Overrides of Object

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString()
		{
			var oldString = $"Title: {this.Title}@Content: {this.Content}";
			var newString = oldString.Replace("@", "\n");
			return newString;
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

        /*
	    #region Implementation of IXmlSerializable

	    public XmlSchema GetSchema()
	    {
	        return null;
	    }

	    public void ReadXml(XmlReader reader)
	    {
	        reader.MoveToContent();

	        if (!reader.IsEmptyElement)
	            Title = reader.GetAttribute(nameof(Title));

	        if (!reader.IsEmptyElement)
	            Content = reader.GetAttribute(nameof(Content));

            reader.ReadEndElement();
	    }

	    public void WriteXml(XmlWriter writer)
	    {
	        writer.WriteAttributeString(nameof(Title), Title);
            writer.WriteAttributeString(nameof(Content), Content);
	    }

	    #endregion
        */
	}
}