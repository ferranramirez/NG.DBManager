using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Infrastructure.Contracts.Models.Enums;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Contexts
{
    public static class DataUtils
    {
        public static User[] UserSeed()
        {
            return new User[]
            {
                new User()
                {
                    Id = Guid.Parse("b0f2451e-5820-4eca-a797-46a01693a3b2"),
                    Name = "Basic QA User",
                    Birthdate = DateTime.Parse("01/01/2000"),
                    PhoneNumber = "+222222222",
                    Email = "basic@test.org",
                    //Password = "10000.+2PnZrnAWQRgqlMx+l8kyA==.ALiUC3pHYJJ7cr8Xqnn1y16XROosvjHNTDmf+Em+pMM=",
                    Role = Role.Basic,
                    //EmailConfirmed = true
                },
                new User()
                {
                    Id = Guid.Parse("0ac2c4c5-ebff-445e-85d4-1db76d65ce0a"),
                    Name = "Admin QA User",
                    Birthdate = DateTime.Parse("01/01/2000"),
                    PhoneNumber = "+000000000",
                    Email = "admin@test.org",
                    //Password = "10000.r1m2AhgohtRKaAYihSdiFQ==.9jOF0O4zo3WoBYq+H1f3XTPG9An8LZfEJd1uwB66N0s=",
                    Role = Role.Admin,
                    //EmailConfirmed = true
                },
                new User()
                {
                    Id = Guid.Parse("440edb6b-342e-4d5f-a233-62aef964cbfa"),
                    Name = "Commerce QA User",
                    Birthdate = DateTime.Parse("01/01/2000"),
                    PhoneNumber = "+111111111",
                    Email = "commerce@test.org",
                    //Password = "10000.NcEE328o58z2KLy1cIiKMA==.5+Mwrqw7XVP2dE+RtcMorXI/Ri6daF4nCRZB4+xJUAY=",
                    Role = Role.Commerce,
                    //EmailConfirmed = true
                },
                new User()
                {
                    Id = Guid.Parse("73b7b257-41f7-4b22-9a10-93fb91238fd9"),
                    Name = "FullCommerce QA User",
                    Birthdate = DateTime.Parse("01/01/2000"),
                    PhoneNumber = "+0111111111",
                    Email = "fullcommerce@test.org",
                    //Password = "10000./LphyV3IUSMjgcllhGg/HA==.ZeBKs4MVq3+BKEQw9ejzr/HbAwI7/KOGr10FqkuGSmE=",
                    Role = Role.Commerce,
                    //EmailConfirmed = true
                }
            };
        }

        public static Location[] LocationSeed()
        {
            return new Location[]
            {
                new Location()
                {
                    Id = Guid.Parse("0013a98e-32f6-494d-b055-c9fb4dafc3e8"),
                    Name = "Test Location",
                    Latitude = 33.842185M,
                    Longitude = -40.707753M,
                }
            };
        }
        public static Commerce[] CommerceSeed()
        {
            return new Commerce[]
            {
                new Commerce()
                {
                    Id = Guid.Parse("a4506bf8-9cca-4413-b0d4-4247c61b1231"),
                    Name = "Test Commerce",
                    LocationId = Guid.Parse("0013a98e-32f6-494d-b055-c9fb4dafc3e8"),
                    UserId = Guid.Parse("73b7b257-41f7-4b22-9a10-93fb91238fd9"),
                }
            };
        }

        public static Tour[] TourSeed()
        {
            return new Tour[]
            {
                new Tour()
                {
                    Id = Guid.Parse("e6cfc804-a418-4683-81be-ca9c753b698a"),
                    Name = "Test Tour",
                }
            };
        }

        public static Node[] NodeSeed()
        {
            return new Node[]
            {
                new Node()
                {
                    Id = Guid.Parse("080942cb-aad4-4e94-9385-f53e6c72113c"),
                    Name = "Test Node",
                    Order = 1,
                    TourId = Guid.Parse("e6cfc804-a418-4683-81be-ca9c753b698a"),
                    LocationId = Guid.Parse("0013a98e-32f6-494d-b055-c9fb4dafc3e8")
                }
            };
        }
    }
}
