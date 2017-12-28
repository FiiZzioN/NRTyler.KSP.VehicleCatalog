// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 10-01-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-28-2017
//
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System;

namespace NRTyler.KSP.VehicleCatalog.ModelTests.DataProviderTests
{
    [TestClass]
	public class VehicleNoteTests
	{
        
		[TestMethod]
		public void Note_TitleNotSet()
		{
			var note  = new Note();
			var title = "Title Not Set";

            // Since the title wasn't set in the constructor, the title automatically 
            // gets set to "Title Not Set" since it was never set in the first place.
            Assert.AreEqual(title, note.Title);
		}

		[TestMethod]
		public void Note_TitleSetValid()
		{
		    var note  = new Note("This Is My Title");
		    var title = "This Is My Title";

		    // Since the title was set in the constructor, and it wasn't null, it's a valid title.
		    Assert.AreEqual(title, note.Title);
        }

	    [TestMethod]
	    public void Note_TitleSetInvalid()
	    {
	        var note  = new Note(String.Empty);
	        var title = "Invalid Title";

	        // Since the title was empty in the constructor, it means it's an invalid title. A valid one is then set in its place.
	        Assert.AreEqual(title, note.Title);
	    }

	    [TestMethod]
	    public void Note_BodyNotSet()
	    {
	        var note = new Note();
	        var body = "Body not set.";

            // Since the body wasn't set in the constructor, the body automatically 
            // gets set to "Body is empty." since it was never set in the first place.
            Assert.AreEqual(body, note.Body);
	    }

	    [TestMethod]
	    public void Note_BodySetValid()
	    {
	        var note = new Note(null, "This is my body.");
	        var body = "This is my body.";

	        // Since the body was set in the constructor, and it wasn't null, it's a valid body.
	        Assert.AreEqual(body, note.Body);
	    }

	    [TestMethod]
	    public void Note_BodySetInvalid()
	    {
	        var note = new Note(null, String.Empty);
	        var body = "Body not set.";

	        // Since the body was empty in the constructor, it means it's an invalid body. A valid one is then set in its place.
	        Assert.AreEqual(body, note.Body);
	    }
    }
}