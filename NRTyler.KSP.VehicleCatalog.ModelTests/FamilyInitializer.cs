// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 12-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 12-28-2017
//
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.ModelTests
{
    public abstract class FamilyInitializer
    {
        #region Test Initialization

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="VehicleFamily"/> that various <see langword="unit tests"/> can use.
        /// </summary>
        protected virtual VehicleFamily Family { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="LauncherCollection"/> that various <see langword="unit tests"/> can use.
        /// </summary>
        protected virtual LauncherCollection Collection { get; set; }
        
        #endregion

        #region Launcher, LauncherCollection, and VehicleFamily Creation Methods

        #region Family Member Creation

        /// <summary>
        /// Creates the <see cref="VehicleFamily"/> that's used in the test initializer.
        /// </summary>
        protected virtual VehicleFamily CreateVehicleFamily()
        {
            var family = new VehicleFamily(CreateFamilyName())
            {
                Notes           = CreateFamilyNotes(),
                PreviewLocation = CreateFamilyPreviewLocation(),
            };

            // Add the collection(s) to the family and then create the summary.
            family.Add(CreateCollection());

            family.Summary = CreateFamilySummary();

            return family;
        }

        /// <summary>
        /// Creates the name for the <see cref="Models.DataProviders.VehicleFamily"/>.
        /// </summary>
        protected virtual string CreateFamilyName()
        {
            return "Angara";
        }

        /// <summary>
        /// Create the notes for the <see cref="Models.DataProviders.VehicleFamily"/>.
        /// </summary>
        protected virtual List<Note> CreateFamilyNotes()
        {
            return new List<Note>()
            {
                new Note()
                {
                    Title = "Family Strength",
                    Body  = "Has many versions for all mission types to help tailor the vehicle to the mission's needs."
                }
            };
        }

        /// <summary>
        /// Create the preview file location for the <see cref="Models.DataProviders.VehicleFamily"/>.
        /// </summary>
        protected virtual string CreateFamilyPreviewLocation()
        {
            return @"C:/Nick/TestFolder/FamilyPictures/NothingHere.png";
        }

        /// <summary>
        /// Create the summary for the <see cref="Models.DataProviders.VehicleFamily"/>.
        /// </summary>
        protected virtual Summary CreateFamilySummary()
        {
            var summary = new Summary
            {
                NumberOfVerisons = Family.GetNumberOfVersions(),
                PriceSummary     = Family.GetPriceSummary(),
                FairingSummary   = Family.GetFairingSummary(),
            };

            return summary;
        }

        #endregion

        #region Launcher Collection Member Creation

        /// <summary>
        /// Creates the <see cref="LauncherCollection"/> that's used in the test initializer.
        /// </summary>
        protected virtual LauncherCollection CreateCollection()
        {
            var collection = new LauncherCollection(CreateCollectionName())
            {
                Notes           = CreateCollectionNotes(),
                PreviewLocation = CreateCollectionPreviewLocation()
            };

            // Add the launcher(s) to the collection and then create the summary.
            collection.Add(CreateAngaraA5());
            collection.Add(CreateAngaraA5BrizM());
            collection.Add(CreateAngaraA5KVTK());

            collection.Summary = CreateCollectionSummary();

            return collection;
        }

        /// <summary>
        /// Creates the name for the <see cref="LauncherCollection"/>.
        /// </summary>
        protected virtual string CreateCollectionName()
        {
            return "A5";
        }

        /// <summary>
        /// Create the notes for the <see cref="LauncherCollection"/>.
        /// </summary>
        protected virtual List<Note> CreateCollectionNotes()
        {
            return new List<Note>()
            {
                new Note()
                {
                    Title = "Version Strength",
                    Body  = "Has many upper stages for all payload types to help tailor the vehicle to the mission's needs."
                }
            };
        }

        /// <summary>
        /// Create the preview file location for the <see cref="LauncherCollection"/>.
        /// </summary>
        protected virtual string CreateCollectionPreviewLocation()
        {
            return @"C:/Nick/TestFolder/CollectionPictures/NothingHere.png";
        }

        /// <summary>
        /// Create the summary for the <see cref="LauncherCollection"/>.
        /// </summary>
        protected virtual Summary CreateCollectionSummary()
        {
            var summary = new Summary
            {
                NumberOfVerisons = Collection.GetNumberOfVersions(),
                PriceSummary     = Collection.GetPriceSummary(),
                FairingSummary   = Collection.GetFairingSummary(),
            };

            return summary;
        }

        #endregion

        #region Launcher Creation

        /// <summary>
        /// Creates an Angara A5 launcher.
        /// </summary>
        protected Launcher CreateAngaraA5()
        {
            // Looks complicated, but all it's doing is holding the information that makes up an Angara A5 Rocket.
            var launcher = new Launcher()
            {
                Name = "Angara A5",
                Notes = new List<Note>()
                {
                    new Note()
                    {
                        Title = "Throttle Profile",
                        Body  = "Core throttles to 37% at 35 seconds after liftoff. Goes back to 100% right before booster separation."
                    }
                },
                PacificationOptions = new List<PacificationOption>()
                {
                    new PacificationOption()
                    {
                        PacificationType = PacificationType.Deorbit,
                        RequiredDeltaV   = 150
                    },
                    new PacificationOption()
                    {
                        PacificationType = PacificationType.GraveyardOrbit,
                        RequiredDeltaV   = 350
                    }
                },
                Price = 221000,
                PreviewLocation = @"C:/Nick/TestFolder/CollectionPictures/NothingHere.png",
                CraftFileLocation = @"C:/Nick/TestFolder/CraftFile/NothingHere.craft",
                Tags = new List<string>()
                {
                    "Angara",
                    "A5"
                },
                Fairings = new List<Fairing>()
                {
                    new Fairing()
                    {
                        ID = "Small",
                        Length = 10,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID = "Medium",
                        Length = 12,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID = "Large",
                        Length = 14,
                        Diameter = 3
                    }
                },
                Capabilities = new List<Capability>()
                {
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 20500,
                            Heaviest = 24500
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.LEO,
                            Apoapsis       = 250,
                            Periapsis      = 250,
                            Inclination    = 28.7,
                            RequiredDeltaV = 6100
                        }
                    },
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 4000,
                            Heaviest = 8000
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.GTO,
                            Apoapsis       = 11475,
                            Periapsis      = 350,
                            Inclination    = 2.0,
                            RequiredDeltaV = 7650
                        }
                    },
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 0,
                            Heaviest = 4000
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.GEO,
                            Apoapsis       = 11475,
                            Periapsis      = 11475,
                            Inclination    = 0.0,
                            RequiredDeltaV = 9100
                        }
                    }
                },
                MaxPayloadDimensions = new PayloadDimensions(8, 2.75)
            };

            return launcher;
        }

        /// <summary>
        /// Creates an Angara A5 Briz-M launcher.
        /// </summary>
        protected Launcher CreateAngaraA5BrizM()
        {
            // Looks complicated, but all it's doing is holding the information 
            // that makes up an Angara A5 rocket with a Briz-M upper stage.
            var launcher = new Launcher()
            {
                Name = "Angara A5 Briz-M",
                Notes = new List<Note>()
                {
                    new Note()
                    {
                        Title = "Throttle Profile",
                        Body  = "Core throttles to 37% at 45 seconds after liftoff. Goes back to 100% right before booster separation."
                    }
                },
                PacificationOptions = new List<PacificationOption>()
                {
                    new PacificationOption()
                    {
                        PacificationType = PacificationType.Deorbit,
                        RequiredDeltaV   = 100
                    },
                    new PacificationOption()
                    {
                        PacificationType = PacificationType.GraveyardOrbit,
                        RequiredDeltaV   = 300
                    }
                },
                Price = 225000,
                PreviewLocation = @"C:/Nick/TestFolder/CollectionPictures/NothingHere.png",
                CraftFileLocation = @"C:/Nick/TestFolder/CraftFile/NothingHere.craft",
                Tags = new List<string>()
                {
                    "Angara",
                    "A5",
                    "Briz-M"
                },
                Fairings = new List<Fairing>()
                {
                    new Fairing()
                    {
                        ID = "Small",
                        Length = 10,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID = "Medium",
                        Length = 12,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID = "Large",
                        Length = 14,
                        Diameter = 3
                    }
                },
                Capabilities = new List<Capability>()
                {
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 24000,
                            Heaviest = 28000
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.LEO,
                            Apoapsis       = 250,
                            Periapsis      = 250,
                            Inclination    = 28.7,
                            RequiredDeltaV = 6100
                        }
                    },
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 6500,
                            Heaviest = 10500
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.GTO,
                            Apoapsis       = 11475,
                            Periapsis      = 350,
                            Inclination    = 2.0,
                            RequiredDeltaV = 7650
                        }
                    },
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 1500,
                            Heaviest = 5500
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.GEO,
                            Apoapsis       = 11475,
                            Periapsis      = 11475,
                            Inclination    = 0.0,
                            RequiredDeltaV = 9100
                        }
                    }
                },
                MaxPayloadDimensions = new PayloadDimensions(6, 2.75)
            };

            return launcher;
        }

        /// <summary>
        /// Creates an Angara A5 KVTK launcher.
        /// </summary>
        protected Launcher CreateAngaraA5KVTK()
        {
            // Looks complicated, but all it's doing is holding the information 
            // that makes up an Angara A5 rocket with a cryogenic KVTK upper stage.
            var launcher = new Launcher()
            {
                Name = "Angara A5 KVTK",
                Notes = new List<Note>()
                {
                    new Note()
                    {
                        Title = "Throttle Profile",
                        Body  = "Core throttles to 37% at 45 seconds after liftoff. Goes back to 100% right before booster separation."
                    }
                },
                PacificationOptions = new List<PacificationOption>()
                {
                    new PacificationOption()
                    {
                        PacificationType = PacificationType.Deorbit,
                        RequiredDeltaV   = 120
                    },
                    new PacificationOption()
                    {
                        PacificationType = PacificationType.GraveyardOrbit,
                        RequiredDeltaV   = 325
                    }
                },
                Price = 235000,
                PreviewLocation = @"C:/Nick/TestFolder/CollectionPictures/NothingHere.png",
                CraftFileLocation = @"C:/Nick/TestFolder/CraftFile/NothingHere.craft",
                Tags = new List<string>()
                {
                    "Angara",
                    "A5",
                    "KVTK"
                },
                Fairings = new List<Fairing>()
                {
                    new Fairing()
                    {
                        ID = "Small",
                        Length = 14,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID = "Medium",
                        Length = 16,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID = "Large",
                        Length = 18,
                        Diameter = 3
                    }
                },
                Capabilities = new List<Capability>()
                {
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 27000,
                            Heaviest = 31000
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.LEO,
                            Apoapsis       = 250,
                            Periapsis      = 250,
                            Inclination    = 28.7,
                            RequiredDeltaV = 6100
                        }
                    },
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 10500,
                            Heaviest = 14500
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.GTO,
                            Apoapsis       = 11475,
                            Periapsis      = 350,
                            Inclination    = 2.0,
                            RequiredDeltaV = 7650
                        }
                    },
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 4500,
                            Heaviest = 8500
                        },
                        Trajectory = new Trajectory()
                        {
                            OrbitType      = OrbitType.GEO,
                            Apoapsis       = 11475,
                            Periapsis      = 11475,
                            Inclination    = 0.0,
                            RequiredDeltaV = 9100
                        }
                    }
                },
                MaxPayloadDimensions = new PayloadDimensions(6, 2.75)
            };

            return launcher;
        }

        #endregion

        #endregion

        #region Initializer

        /// <summary>
        /// The method that gets called after each test has been completed.
        /// </summary>
        [TestInitialize]
        public virtual void SetupFamily()
        {
            Family     = CreateVehicleFamily();
            Collection = CreateCollection();
        }

        #endregion

        #endregion
    }
}