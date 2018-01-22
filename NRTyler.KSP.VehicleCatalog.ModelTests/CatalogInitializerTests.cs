// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.ModelTests
//
// Author           : Nicholas Tyler
// Created          : 12-28-2017
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-20-2018
//
// License          : MIT License
// ***********************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRTyler.CodeLibrary.Utilities;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using System.Collections.Generic;

namespace NRTyler.KSP.VehicleCatalog.ModelTests
{
    [TestClass]
    public class CatalogInitializerTests : CatalogInitializer
    {
        [TestInitialize]
        public override void Initialize()
        {
            SetupCatalogInitializer(ExtraLaunchers.AddAsCollection);
        }

        /// <summary>
        /// Ensures that the Angara A5 launcher that's created for standard tests retain the same values.
        /// </summary>
        [TestMethod]
        public void CatalogInitializer_AngaraA5Creation()
        {
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

            Assert.IsTrue(AngaraA5.CompareObject(launcher));
        }

        /// <summary>
        /// Ensures that the Angara A5 Briz-M launcher that's created for standard tests retain the same values.
        /// </summary>
        [TestMethod]
        public void CatalogInitializer_AngaraA5BrizMCreation()
        {
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

            Assert.IsTrue(AngaraA5BrizM.CompareObject(launcher));
        }

        /// <summary>
        /// Ensures that the Angara A5 KVTK launcher that's created for standard tests retain the same values.
        /// </summary>
        [TestMethod]
        public void CatalogInitializer_AngaraA5KVTKCreation()
        {
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

            Assert.IsTrue(AngaraA5KVTK.CompareObject(launcher));
        }

        /// <summary>
        /// Ensures that the Angara A3 launcher that's created for standard tests retain the same values.
        /// </summary>
        [TestMethod]
        public void CatalogInitializer_AngaraA3Creation()
        {
            var launcher = new Launcher()
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

            Assert.IsTrue(AngaraA3.CompareObject(launcher));
        }

        /// <summary>
        /// Ensures that the Angara A3 Briz-M launcher that's created for standard tests retain the same values.
        /// </summary>
        [TestMethod]
        public void CatalogInitializer_AngaraA3BrizMCreation()
        {
            var launcher = new Launcher()
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

            Assert.IsTrue(AngaraA3BrizM.CompareObject(launcher));
        }

        /// <summary>
        /// Ensures that the Angara A3 KVTK launcher that's created for standard tests retain the same values.
        /// </summary>
        [TestMethod]
        public void CatalogInitializer_AngaraA3KVTKCreation()
        {
            var launcher = new Launcher()
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

            Assert.IsTrue(AngaraA3KVTK.CompareObject(launcher));
        }
    }
}