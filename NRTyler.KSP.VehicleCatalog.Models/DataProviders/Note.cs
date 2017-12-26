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
using System.Runtime.Serialization;
using NRTyler.CodeLibrary.Annotations;
using NRTyler.CodeLibrary.Extensions;

namespace NRTyler.KSP.VehicleCatalog.Models.DataProviders
{
    /// <summary>
    /// Allows the user to jot down information for a later date. Contains a title and body just like a normal note would provide.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    [Serializable]
    [DataContract(Name = "Note")]
	public class Note : INotifyPropertyChanged
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        public Note()
		{
			Title = null;
			Body  = null;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class 
        /// while specifying the note's title. The note's body will be empty.
        /// </summary>
        /// <param name="title">The title of the note.</param>
        public Note(string title) : this (title, null)
		{
			Title = title;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> 
        /// class while specifying the note's title and body.
        /// </summary>
        /// <param name="title">The note's title.</param>
        /// <param name="body">The note's body.</param>
        public Note(string title, string body)
	    {
	        Title = title;
	        Body  = body;
	    }

		private string title;
		private string body;

        /// <summary>
        /// Gets or sets the note's title.
        /// </summary>
        [DataMember]
        public string Title
		{
			get { return this.title; }
			set
			{
				this.title = value.HandleNullOrWhiteSpace("Invalid Title");
				OnPropertyChanged(nameof(Title));
			}
		}


        /// <summary>
        /// Gets or sets the content of the note's body.
        /// </summary>
        [DataMember]
        public string Body
		{
			get { return this.body; }
			set
			{
				this.body = value.HandleNullOrWhiteSpace("Body is empty.");
				OnPropertyChanged(nameof(Body));
			}
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
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}