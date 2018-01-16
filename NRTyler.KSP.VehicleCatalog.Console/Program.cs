// ***********************************************************************
// Assembly         : NRTyler.KSP.VehicleCatalog.Console
//
// Author           : Nicholas Tyler
// Created          : 01-12-2018
//
// Last Modified By : Nicholas Tyler
// Last Modified On : 01-12-2018
//
// License          : MIT License
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NRTyler.KSP.Common.Enums;
using NRTyler.KSP.VehicleCatalog.Models.DataControllers;
using NRTyler.KSP.VehicleCatalog.Models.DataProviders;
using NRTyler.KSP.VehicleCatalog.ModelTests;

namespace NRTyler.KSP.VehicleCatalog.Console
{
    public class Program : CatalogInitializer
    {
        /// <summary>
        /// Gets or sets an Angara A5 <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// </summary>
        protected static Launcher AngaraA5 { get; set; }

        /// <summary>
        /// Gets or sets an Angara A5 Briz-M <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// </summary>
        protected static Launcher AngaraA5BrizM { get; set; }

        /// <summary>
        /// Gets or sets an Angara A5 KVTK <see cref="Launcher"/> that various <see langword="unit tests"/> can use.
        /// </summary>
        protected static Launcher AngaraA5KVTK { get; set; }

        /// <summary>
        /// Creates an Angara A5 launcher.
        /// </summary>
        protected static void CreateAngaraA5()
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
        protected static void CreateAngaraA5BrizM()
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
        protected static void CreateAngaraA5KVTK()
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
                            OrbitType      = OrbitType.SSO,
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
        private static void Main()
        {
            var family = new VehicleFamily("Angara");

            CreateAngaraA5();
            CreateAngaraA5BrizM();
            CreateAngaraA5KVTK();

            var collection = UseLauncherCollection();
            family.LauncherCollection.Add(collection);


            foreach (var launcher in UseLaunchers())
            {
                family.Launchers.Add(launcher);
            }

            var controller = new SummaryController();
            family.Summary = controller.GetFamilySummary(family);

            foreach (var payloadRangeSummary in family.Summary.PayloadRangeSummary)
            {                
                WriteLine($"OrbitType: {payloadRangeSummary.OrbitType}");
                WriteLine($" Lightest: {payloadRangeSummary.PayloadRange.Lightest}");
                WriteLine($" Heaviest: {payloadRangeSummary.PayloadRange.Heaviest}");
                WriteLine("---------------");
            }
        }

        private static void WriteLine(object obj = null)
        {
            System.Console.WriteLine(obj);
        }

        private static LauncherCollection UseLauncherCollection()
        {
            var collection = new LauncherCollection();
            collection.Launchers.Add(AngaraA5);
            collection.Launchers.Add(AngaraA5BrizM);
            //collection.Launchers.Add(AngaraA5KVTK);

            return collection;
        }

        private static List<Launcher> UseLaunchers()
        {
            var collection = new List<Launcher>
            {
               // AngaraA5,
                //AngaraA5BrizM,
                AngaraA5KVTK 
            };

            return collection;
        }
    }
}
