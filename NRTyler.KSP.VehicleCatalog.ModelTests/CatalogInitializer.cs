// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 12-27-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-20-2018
//
// License          : MIT License
// ***********************************************************************

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.ModelTests
{
    /// <summary>
    /// Meant to be an aid for the <see cref="VehicleFamily"/>, <see cref="LauncherCollection"/>, 
    /// and <see cref="Launcher"/> <see langword="unit tests"/>.
    /// </summary>
    public abstract class CatalogInitializer
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

        /// <summary>
        /// Gets or sets an Angara A5 <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// </summary>
        protected virtual Launcher AngaraA5 { get; set; }

        /// <summary>
        /// Gets or sets an Angara A5 Briz-M <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// </summary>
        protected virtual Launcher AngaraA5BrizM { get; set; }

        /// <summary>
        /// Gets or sets an Angara A5 KVTK <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// </summary>
        protected virtual Launcher AngaraA5KVTK { get; set; }

        /// <summary>
        /// Gets or sets the extra <see cref="LauncherCollection"/> that various <see langword="unit tests"/> can use.
        /// During Setup, if "addExtraLaunchers" is set to "AddNone", this property is set to <see langword="null"/>.
        /// </summary>
        protected virtual LauncherCollection ExtraCollection { get; set; }

        /// <summary>
        /// Gets or sets an Angara A3 <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// During Setup, if "addExtraLaunchers" is set to "AddNone", this property is set to <see langword="null"/>.
        /// </summary>
        protected virtual Launcher AngaraA3 { get; set; }

        /// <summary>
        /// Gets or sets an Angara A3 Briz-M <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// During Setup, if "addExtraLaunchers" is set to "AddNone", this property is set to <see langword="null"/>.
        /// </summary>
        protected virtual Launcher AngaraA3BrizM { get; set; }

        /// <summary>
        /// Gets or sets an Angara A3 KVTK <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// During Setup, if "addExtraLaunchers" is set to "AddNone", this property is set to <see langword="null"/>.
        /// </summary>
        protected virtual Launcher AngaraA3KVTK { get; set; }

        #endregion

        #region VehicleFamily, LauncherCollection, and Launcher Creation Methods

        #region Family Member Creation

        /// <summary>
        /// Creates the <see cref="VehicleFamily"/> that's used in the test initializer.
        /// </summary>
        protected virtual void CreateVehicleFamily()
        {
            Family = new VehicleFamily(CreateFamilyName())
            {
                Notes           = CreateFamilyNotes(),
                PreviewLocation = CreateFamilyPreviewLocation(),
            };

            // Add the collection(s) to the family and then create the summary.
            Family.LauncherCollections.Add(Collection);

            Family.Summary = CreateFamilySummary();
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
            var controller = new SummaryController();
            var summary    = controller.GetCompleteSummary(Family);

            return summary;
        }

        #endregion

        #region Launcher Collection Member Creation

        /// <summary>
        /// Creates the <see cref="LauncherCollection"/> that's used in the test initializer.
        /// </summary>
        protected virtual void CreateCollection()
        {
            Collection = new LauncherCollection(CreateCollectionName())
            {
                Notes           = CreateCollectionNotes(),
                PreviewLocation = CreateCollectionPreviewLocation()
            };

            // Add the launcher(s) to the collection and then create the summary.
            Collection.Launchers.Add(AngaraA5);
            Collection.Launchers.Add(AngaraA5BrizM);
            Collection.Launchers.Add(AngaraA5KVTK);

            Collection.Summary = CreateCollectionSummary();
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
            var controller = new SummaryController();
            var summary    = controller.GetCompleteSummary(Collection);

            return summary;
        }

        #endregion

        #region Launcher Creation

        /// <summary>
        /// Creates an Angara A5 launcher.
        /// </summary>
        protected virtual void CreateAngaraA5()
        {
            // Looks complicated, but all it's doing is holding the information that makes up an Angara A5 Rocket.
            AngaraA5 = new Launcher()
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
                        ID       = "Small",
                        Length   = 10,
                        Diameter = 2.8
                    },
                    new Fairing()
                    {
                        ID       = "Medium",
                        Length   = 12,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID       = "Large",
                        Length   = 14,
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
        }

        /// <summary>
        /// Creates an Angara A5 Briz-M launcher.
        /// </summary>
        protected virtual void CreateAngaraA5BrizM()
        {
            // Looks complicated, but all it's doing is holding the information 
            // that makes up an Angara A5 rocket with a Briz-M upper stage.
            AngaraA5BrizM = new Launcher()
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
                        ID       = "Small",
                        Length   = 10,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID       = "Medium",
                        Length   = 12,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID       = "Large",
                        Length   = 14,
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
        }

        /// <summary>
        /// Creates an Angara A5 KVTK launcher.
        /// </summary>
        protected virtual void CreateAngaraA5KVTK()
        {
            // Looks complicated, but all it's doing is holding the information 
            // that makes up an Angara A5 rocket with a cryogenic KVTK upper stage.
            AngaraA5KVTK = new Launcher()
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
                        ID       = "Small",
                        Length   = 14,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID       = "Medium",
                        Length   = 16,
                        Diameter = 3
                    },
                    new Fairing()
                    {
                        ID       = "Large",
                        Length   = 18,
                        Diameter = 3.2
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
        }

        #endregion

        #endregion

        #region Extra LauncherCollection Creation

        protected virtual void CreateExtraLauncherCollection()
        {
            ExtraCollection = new LauncherCollection("A3")
            {
                PreviewLocation = @"C:/Nick/TestFolder/CollectionPictures/NothingHere.png",
                Notes = new List<Note>()
                {
                    new Note()
                    {
                        Title = "Version Strength",
                        Body  = "Has many upper stages for all payload types to help tailor the vehicle to the mission's needs."
                    }
                }
            };

            // Add the launcher(s) to the collection and then create the summary.
            ExtraCollection.Launchers.Add(AngaraA3);
            ExtraCollection.Launchers.Add(AngaraA3BrizM);
            ExtraCollection.Launchers.Add(AngaraA3KVTK);

            var controller = new SummaryController();

            Collection.Summary = controller.GetCompleteSummary(ExtraCollection);
        }

        #region Extra Launcher Creation

        /// <summary>
        /// Creates an Angara A3 launcher.
        /// </summary>
        protected virtual void CreateAngaraA3()
        {
            // Looks complicated, but all it's doing is holding the information that makes up an Angara A3 Rocket.
            AngaraA3 = new Launcher()
            {
                Name = "Angara A3",
                Notes = new List<Note>()
                {
                    new Note()
                    {
                        Title = "Throttle Profile",
                        Body  = "Core throttles to 37% at 50 seconds after liftoff. Goes back to 100% right before booster separation."
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
                Price = 146000,
                PreviewLocation = @"C:/Nick/TestFolder/CollectionPictures/NothingHere.png",
                CraftFileLocation = @"C:/Nick/TestFolder/CraftFile/NothingHere.craft",
                Tags = new List<string>()
                {
                    "Angara",
                    "A3"
                },
                Fairings = new List<Fairing>()
                {
                    new Fairing()
                    {
                        ID       = "Small",
                        Length   = 8,
                        Diameter = 2.5
                    },
                    new Fairing()
                    {
                        ID       = "Medium",
                        Length   = 10,
                        Diameter = 2.7
                    },
                    new Fairing()
                    {
                        ID       = "Large",
                        Length   = 12,
                        Diameter = 2.9
                    }
                },
                Capabilities = new List<Capability>()
                {
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 11500,
                            Heaviest = 15500
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
                            Lightest = 3000,
                            Heaviest = 7000
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
                },
                MaxPayloadDimensions = new PayloadDimensions(6, 2.4)
            };
        }

        /// <summary>
        /// Creates an Angara A3 Briz-M launcher.
        /// </summary>
        protected virtual void CreateAngaraA3BrizM()
        {
            // Looks complicated, but all it's doing is holding the information 
            // that makes up an Angara A3 rocket with a Briz-M upper stage.
            AngaraA3BrizM = new Launcher()
            {
                Name = "Angara A3 Briz-M",
                Notes = new List<Note>()
                {
                    new Note()
                    {
                        Title = "Throttle Profile",
                        Body  = "Core throttles to 37% at 60 seconds after liftoff. Goes back to 100% right before booster separation."
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
                Price = 154500,
                PreviewLocation = @"C:/Nick/TestFolder/CollectionPictures/NothingHere.png",
                CraftFileLocation = @"C:/Nick/TestFolder/CraftFile/NothingHere.craft",
                Tags = new List<string>()
                {
                    "Angara",
                    "A3",
                    "Briz-M"
                },
                Fairings = new List<Fairing>()
                {
                    new Fairing()
                    {
                        ID       = "Small",
                        Length   = 8,
                        Diameter = 2.5
                    },
                    new Fairing()
                    {
                        ID       = "Medium",
                        Length   = 10,
                        Diameter = 2.7
                    },
                    new Fairing()
                    {
                        ID       = "Large",
                        Length   = 12,
                        Diameter = 2.9
                    }
                },
                Capabilities = new List<Capability>()
                {
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 12000,
                            Heaviest = 16000
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
                            Heaviest = 500
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
                MaxPayloadDimensions = new PayloadDimensions(5, 2.4)
            };
        }

        /// <summary>
        /// Creates an Angara A3 KVTK launcher.
        /// </summary>
        protected virtual void CreateAngaraA3KVTK()
        {
            // Looks complicated, but all it's doing is holding the information 
            // that makes up an Angara A3 rocket with a cryogenic KVTK upper stage.
            AngaraA3KVTK = new Launcher()
            {
                Name = "Angara A3 KVTK",
                Notes = new List<Note>()
                {
                    new Note()
                    {
                        Title = "Throttle Profile",
                        Body  = "Core throttles to 37% at 60 seconds after liftoff. Goes back to 100% right before booster separation."
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
                Price = 160000,
                PreviewLocation = @"C:/Nick/TestFolder/CollectionPictures/NothingHere.png",
                CraftFileLocation = @"C:/Nick/TestFolder/CraftFile/NothingHere.craft",
                Tags = new List<string>()
                {
                    "Angara",
                    "A3",
                    "KVTK"
                },
                Fairings = new List<Fairing>()
                {
                    new Fairing()
                    {
                        ID       = "Small",
                        Length   = 12,
                        Diameter = 2.9
                    },
                    new Fairing()
                    {
                        ID       = "Medium",
                        Length   = 14,
                        Diameter = 2.9
                    },
                    new Fairing()
                    {
                        ID       = "Large",
                        Length   = 16,
                        Diameter = 2.9
                    }
                },
                Capabilities = new List<Capability>()
                {
                    new Capability()
                    {
                        PayloadRange = new PayloadRange()
                        {
                            Lightest = 14000,
                            Heaviest = 18000
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
                            Lightest = 5100,
                            Heaviest = 9100
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
                            Lightest = 1000,
                            Heaviest = 5000
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
                MaxPayloadDimensions = new PayloadDimensions(7, 2.9)
            };
        }

        #endregion

        #endregion

        #region Initializer

        /// <summary>
        /// The method that gets called after each test has been completed.
        /// </summary>
        [TestInitialize]
        public virtual void Initialize()
        {
            SetupCatalogInitializer();
        }

        /// <summary>
        /// This method is used to setup the <see cref="CatalogInitializer" /> class. Call this in
        /// any class that overrides the Initialize() method so everything gets set up correctly.
        /// </summary>
        /// <param name="addExtraLaunchers">
        /// Choose whether or not you want to add an additional launcher 
        /// collection and the way you wish to add it to the family.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Will be thrown if you specify a value that doesn't exist in the <see cref="ExtraLaunchers"/> enumeration.
        /// </exception>
        protected void SetupCatalogInitializer(ExtraLaunchers addExtraLaunchers = ExtraLaunchers.AddNone)
        {
            switch ((int)addExtraLaunchers)
            {
                case 0:
                    AddNone();
                    break;
                case 1:
                    AddAsCollection();
                    break;
                case 2:
                    AddIndividually();
                    break;
                default:
                    throw new
                        ArgumentOutOfRangeException($"{nameof(ExtraLaunchers)} doesn't support the specified value.");
            }
        }

        #region SetupCatalogInitializer Methods

        /// <summary>
        /// The setup process to use when "addExtraLaunchers" is set to "AddNone".
        /// </summary>
        private void AddNone()
        {
            // Vehicles created first so they can be added to the collection.
            CreateAngaraA5();
            CreateAngaraA5BrizM();
            CreateAngaraA5KVTK();

            // Since we now have vehicles, we can create the collection. From 
            // there, we have a collection, so we can then create a family.
            CreateCollection();
            CreateVehicleFamily();
        }

        /// <summary>
        /// Goes through process of creating the Extra LauncherCollection.
        /// </summary>
        private void ExtraCollectionSetup()
        {
            // Vehicles created first so they can be added to the collection.
            CreateAngaraA3();
            CreateAngaraA3BrizM();
            CreateAngaraA3KVTK();

            // Since the vehicles have been created, add them to the extra collection.
            CreateExtraLauncherCollection();
        }

        /// <summary>
        /// The setup process to use when "addExtraLaunchers" is set to "AddAsCollection".
        /// </summary>
        private void AddAsCollection()
        {
            // The default setup needed so the base vehicle family and extra collection can be created.
            AddNone();
            ExtraCollectionSetup();

            // Add the vehicles to the family as new collection.
            Family.LauncherCollections.Add(ExtraCollection);
        }

        /// <summary>
        /// The setup process to use when "addExtraLaunchers" is set to "AddIndividually".
        /// </summary>
        private void AddIndividually()
        {
            // The default setup needed so the base vehicle family and extra collection can be created.
            AddNone();
            ExtraCollectionSetup();

            // Add the vehicles to the family as individual launchers.
            Family.Launchers.AddRange(ExtraCollection.Launchers);
        }


        #endregion

        #endregion

        #endregion
    }

    public enum ExtraLaunchers
    {
        AddNone = 0,
        AddAsCollection = 1,
        AddIndividually = 2
    }
}