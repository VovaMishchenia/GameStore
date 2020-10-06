using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStoreDAL.Entities;

namespace GameStoreDAL.Initializer
{
    public class Gamesinitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var genres = new List<Genre>
            {
                new Genre{Name = "Action"},
                new Genre{Name = "Shooter"},
                new Genre{Name = "RPG"},
                new Genre{Name = "Strategy"},
                new Genre{Name = "Rasing"},
                new Genre{Name = "Sport Simulator"}
            };

            var addresses = new List<Address>
            {
                new Address{ Country="Ukraine", City="Rivne",Street="Dubenska",Building=1}
            };


            var developers = new List<Developer>
            {
                new Developer{Name = "RockStar"},
                new Developer{Name = "EA"},
                new Developer{Name = "Epic"},
                new Developer{Name = "Bethesda"},
                new Developer{Name = "Activision"},
                new Developer{Name = "Valve"},
                new Developer{Name = "Ghost Games"},
                new Developer{Name = "Playrix"},
                new Developer{Name = "Ubisoft"}
            };



            var games = new List<Game>
            {
                new Game
                {
                    Name = "FarCry",
                    Description = "Far cry info",
                    Image = "https://coremission.net/wp-content/uploads/2020/02/far-cry-5-logo.jpg",
                    Price = 34,
                    Year = 2018,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Shooter"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Ubisoft"),
                    Amount=10
                    
                },
                new Game
                {
                    Name = "Assasins Creed",
                    Description = "AC info",
                    Image = "https://store-images.s-microsoft.com/image/apps.24056.67608528792882784.56fd0e59-8e5c-4c63-9eca-864b4e57fa73.ad05cf15-520c-44ca-861a-04c65bb46755?mode=scale&q=90&h=1080&w=1920",
                    Price = 54,
                    Year = 2018,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Action"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Ubisoft"),
                    Amount=17
                },
                 new Game
                {
                    Name = "GTA 5",
                    Description = "GTA 5 info",
                    Image = "https://upload.wikimedia.org/wikipedia/ru/thumb/c/c8/GTAV_Official_Cover_Art.jpg/274px-GTAV_Official_Cover_Art.jpg",
                    Price = 84,
                    Year = 2019,
                    Genre = genres.FirstOrDefault(x=>x.Name == "RPG"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "RockStar"),
                    Amount=2
                },
                 new Game
                {
                    Name = "FIFA",
                    Description = "FIFA info",
                    Image = "https://media.contentapi.ea.com/content/dam/ea/easports/fifa/home/2018/world-cup-april30/top/f18wc-homepage-top-hero-bg-xs.jpg",
                    Price = 34,
                    Year = 2020,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Sport Simulator"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "EA"),
                    Amount=9
                },
                 new Game
                {
                    Name = "NFS",
                    Description = "NFS info",
                    Image = "https://xboxunion.ru/wp-content/uploads/2020/06/need-for-speed-heat-gameplay-customization-0.jpg",
                    Price = 134,
                    Year = 2009,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Rasing"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Ghost Games"),
                    Amount=5
                },
                  new Game
                {
                    Name = "SnowRunner",
                    Description = "SnowRunner description",
                    Image = "https://store.playstation.com/store/api/chihiro/00_09_000/container/UA/ru/999/EP4133-CUSA17438_00-SNOWRUNNERGAME00/1598265846000/image?w=240&h=240&bg_color=000000&opacity=100&_version=00_09_000",
                    Price = 23,
                    Year = 2020,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Rasing"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Valve"),
                    Amount=3
                },
                   new Game
                {
                    Name = "Death Stranding",
                    Description = "Death Stranding description",
                    Image = "https://psmedia.playstation.com/is/image/psmedia/death-strandig-digital-edition-pack-01-ps4-en-22jul19_1563798596409?$Icon$",
                    Price = 63,
                    Year = 2020,
                    Genre = genres.FirstOrDefault(x=>x.Name == "RPG"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Playrix"),
                    Amount=14
                },
                    new Game
                {
                    Name = "CoD:Modern Warfare",
                    Description = "CoD:Modern Warfare description",
                    Image = "https://psmedia.playstation.com/is/image/psmedia/call-of-duty-modern-warfare-pack-01-ps4-en-31may19_en?$Icon$",
                    Price = 78,
                    Year = 2020,
                    Genre = genres.FirstOrDefault(x=>x.Name == "RPG"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "Activision"),
                    Amount=12
                },
                     new Game
                {
                    Name = "F1 2019",
                    Description = "F1 2019 description",
                    Image = "https://psmedia.playstation.com/is/image/psmedia/best-racing-games-f1-2019-two-column-01-ps4-en-22-may19_1558538539190?$Icon$",
                    Price = 99,
                    Year = 2019,
                    Genre = genres.FirstOrDefault(x=>x.Name == "Rasing"),
                    Developer = developers.FirstOrDefault(x=>x.Name == "EA"),
                    Amount=7
                }


            };



            context.Genres.AddRange(genres);
            context.Developers.AddRange(developers);
            context.Games.AddRange(games);
           


            context.SaveChanges();

            base.Seed(context);
        }
    }
}
