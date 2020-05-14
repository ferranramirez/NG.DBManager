using Microsoft.EntityFrameworkCore;
using NG.DBManager.Infrastructure.Contracts.Contexts;
using NG.DBManager.Infrastructure.Contracts.Models;
using NG.DBManager.Test.UnitTest.Fixture.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NG.DBManager.Test.UnitTest.Infrastructure.Fixture
{
    public class FullDbSetup
    {
        public ICollection<AudioImage> AudioImages;
        public ICollection<Location> Coordinates;
        public ICollection<Featured> FeaturedTours;
        public ICollection<Image> Images;
        public ICollection<Node> Nodes;
        public ICollection<Restaurant> Restaurants;
        public ICollection<Review> Reviews;
        public ICollection<Tag> Tags;
        public ICollection<Tour> Tours;
        public ICollection<TourTag> TourTags;
        public ICollection<User> Users;

        private const int usersToGenerate = 60;
        private const int toursToGenerate = 50;
        private const int imagesToGenerate = 70;
        private const int nodesToGenerate = 5;
        private const int audiosToGenerate = 10;

        public FullDbSetup()
        {
            GenerateImages();
            GenerateTags();
            GenerateTours();
            GenerateNodes();
            GenerateAudios();
            GenerateUsers();
        }
        private void GenerateImages()
        {
            Images = new List<Image>();

            for (int i = 0; i < imagesToGenerate; i++)
            {
                Image newImage = new Image
                {
                    Id = Guid.NewGuid(),
                    Name = String.Concat(RandomGenerator.RandomString(5), ".jpg"),
                };
                Images.Add(newImage);
            }
        }


        private void GenerateTags()
        {
            Tags = new List<Tag>();

            string[] tags = new string[] { "Sport", "Adventure", "Eco", "Nature" };

            var tagsCount = tags.Length;

            for (int i = 0; i < tagsCount; i++)
            {
                Tag newTag = new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = tags[i],
                };

                Tags.Add(newTag);
            }
        }

        private void GenerateTours()
        {
            Tours = new List<Tour>();
            TourTags = new List<TourTag>();

            for (int i = 0; i < toursToGenerate; i++)
            {
                Tour newTour = new Tour
                {
                    Id = Guid.NewGuid(),
                    Name = RandomGenerator.RandomString(5),
                    Description = RandomGenerator.RandomString(50),
                };

                AddTagToFirst4Tours(i, newTour);

                AllToursWithImageButLast(i, newTour);

            }

            MakeFirstTourFeatured();
        }

        private void AddTagToFirst4Tours(int i, Tour newTour)
        {
            if (i < 4)
            {
                var newTourTag = new TourTag
                {
                    Tour = newTour,
                    Tag = Tags.ElementAt(i),
                };
                TourTags.Add(newTourTag);
            }
        }

        private void AllToursWithImageButLast(int i, Tour newTour)
        {
            // To leave the last of the Tours without any Image Referenced
            if (i + 1 < toursToGenerate)
            {
                newTour.Image = Images.ElementAt(i);
            }
            Tours.Add(newTour);
        }

        private void MakeFirstTourFeatured()
        {
            var firstTour = Tours.First();
            firstTour.Featured = new Featured() { TourId = firstTour.Id };
        }

        private void GenerateNodes()
        {
            Nodes = new List<Node>();
            Restaurants = new List<Restaurant>();

            for (int i = 0; i < nodesToGenerate; i++)
            {
                Location newLocation = new Location
                {
                    Name = RandomGenerator.RandomString(10) + i.ToString(),
                    Latitude = RandomGenerator.NextDecimal(),
                    Longitude = RandomGenerator.NextDecimal(),
                };

                Node newNode = new Node
                {
                    TourId = Tours.First().Id,
                    Id = Guid.NewGuid(),
                    Name = string.Concat(RandomGenerator.RandomString(15)),
                    Location = newLocation,
                };

                newNode.Images = Images;
                Nodes.Add(newNode);
            }
            FirstCoordinatesAreRestaurant();
        }
        private void FirstCoordinatesAreRestaurant()
        {
            var restaurantId = Guid.NewGuid();
            Commerce newCommerce = new Commerce
            {
                Id = restaurantId,
                Name = "Restaurant One",
                LocationId = Nodes.First().Location.Id,
            };
            Restaurant newRestaurant = new Restaurant { CommerceId = restaurantId, Commerce = newCommerce };
            Restaurants.Add(newRestaurant);
        }

        private void GenerateAudios()
        {
            var Audios = new List<Audio>();
            AudioImages = new List<AudioImage>();

            for (int i = 0; i < audiosToGenerate; i++)
            {
                Audio newAudio = new Audio
                {
                    Id = Guid.NewGuid(),
                    Name = string.Concat(RandomGenerator.RandomString(5), ".mp3"),
                };

                FillAudioImages(newAudio);


                Audios.Add(newAudio);
            }

            Nodes.First().Audios = Audios;
        }

        private void FillAudioImages(Audio newAudio)
        {
            const int imagesPerAudio = 3;

            for (int j = 1; j < imagesPerAudio; j++)
            {
                AudioImage newAudioImage = new AudioImage
                {
                    Audio = newAudio,
                    Image = Images.ElementAt(j),
                    StartTime = new TimeSpan(RandomGenerator.NextInt32()),
                    EndTime = new TimeSpan(RandomGenerator.NextInt32() + j)
                };

                AudioImages.Add(newAudioImage);
            }
        }

        private void GenerateUsers()
        {
            Users = new List<User>();
            Reviews = new List<Review>();

            string[] emails = new string[] { "mauricio.mraz@willms.com", "amante@yahoo.com", "eli57@yahoo.com", "cielo.mann@huel.com", "rico25@hotmail.com", "okutch@brakus.bizkellen.oberbrunner@hotmail.com", "judson.collins@yahoo.com", "elmer.lang@corkery.com", "ibahringer@gmail.com", "dach.jovani@purdy.com", "xfay@streich.com", "buddy58@christiansen.com", "efrain.carroll@yahoo.com", "kstark@yahoo.com", "darrell42@rohan.com", "xdavis@parker.infoswaniawski.joan@welch.com", "fblick@gmail.com", "rodriguez.brooklyn@wisozk.com", "celestine23@wintheiser.com", "schimmel.katelynn@wiza.com", "colin09@hotmail.com", "ybrekke@gmail.com", "colby12@cruickshank.com", "rbogisich@yahoo.com", "sheldon.kreiger@yahoo.com", "breanne.ryan@yahoo.com", "larissa85@gmail.com", "maryse33@durgan.com", "strosin.luella@yahoo.com", "maryse.lakin@hotmail.com", "lchamplin@collins.com", "mayert.robb@yahoo.com", "jaclyn03@gmail.com", "alejandrin.dibbert@bahringer.com", "harber.brandyn@beier.com", "heaney.ronaldo@weissnat.com", "graham.treva@oconner.com", "willis42@vonrueden.com", "earline.gottlieb@hartmann.com", "merle.pagac@hotmail.com", "beau.kozey@yahoo.com", "amira.dickinson@rogahn.com", "hettie37@hahn.com", "abbott.cayla@yahoo.com", "johnson.buckridge@hagenes.bizdkutch@yahoo.com", "erdman.timothy@balistreri.com", "jocelyn.dare@yahoo.com", "feeney.lexie@lesch.com", "mbechtelar@yahoo.com", "natasha19@satterfield.com", "senger.alejandra@zulauf.com", "jacquelyn.oconnell@yahoo.com", "harry.spencer@torp.com", "iwindler@hotmail.com", "shanie98@johnston.com", "wvandervort@yahoo.com", "dakota.witting@lind.com", "qschneider@hotmail.com", "camden.white@gmail.com", "raegan25@frami.com", "lockman.neha@hotmail.com", "kuhic.jessica@lebsack.com", "mathew.botsford@zulauf.com", "osimonis@hirthe.com", "doris.wilkinson@ledner.com", "moore.frankie@hotmail.com", "valentina93@wuckert.bizchristiansen.colten@schultz.bizmarcia75@gmail.com", "eriberto.willms@wisozk.infodena86@yahoo.com", "gibson.zack@upton.com", "kaela19@boyle.com", "fadel.karolann@yahoo.com", "palma.rempel@kunze.com", "bmoen@gmail.com", "bonnie12@erdman.com", "littel.taylor@strosin.com", "merle.kerluke@kiehn.com", "uwisozk@yahoo.com", "bechtelar.martine@yahoo.com", "reichel.raphaelle@hotmail.com", "mitchell.johan@vonrueden.com", "payton86@renner.com", "hermiston.julia@vandervort.com", "andreanne.hegmann@gmail.com", "bessie.stiedemann@raynor.com", "bins.lillian@hills.com", "bchamplin@gmail.com", "carrie.stamm@kohler.bizfranco57@blanda.com", "victor05@bruen.com", "corwin.jodie@hettinger.com", "bruecker@hotmail.com", "danny.jones@dickinson.com", "vonrueden.kennith@yahoo.com", "abernathy.katharina@hotmail.com", "genesis02@walsh.com", "skyla.considine@gmail.com", "astrid.weissnat@hotmail.com", "myrtie.kuphal@cremin.com", "immanuel01@yahoo.com", "olga.turner@schoen.bizadonis73@hotmail.com", "utrantow@tremblay.bizheller.maynard@hotmail.com", "josephine.robel@wolf.infobernhard.wilfrid@hotmail.com", "dangelo57@hotmail.com", "leo52@yahoo.com", "minerva84@parker.com", "kiehn.eleanora@sauer.com", "xkoepp@orn.com", "jmarvin@jenkins.com", "wolff.ronny@gmail.com", "pwiza@mcdermott.com", "king.colby@bogisich.bizraquel54@cartwright.com", "kuphal.adrienne@hotmail.com", "lemuel61@hotmail.com", "marina41@gutmann.com", "randal16@wyman.com", "kiarra.paucek@gmail.com", "mosciski.tremayne@hotmail.com", "bschowalter@gmail.com", "quinten52@yahoo.com", "tiana.berge@hotmail.com", "adaniel@hotmail.com", "dhaley@hotmail.com", "ssteuber@greenholt.infokpfeffer@blick.com", "eve.prohaska@weber.com", "gutmann.brycen@ferry.com", "matteo12@yahoo.com", "imelda.yundt@bogan.com", "fermin.zieme@gmail.com", "crona.alanna@eichmann.com", "faltenwerth@hotmail.com", "jacey.goldner@bayer.com", "jerde.katlyn@lowe.bizgracie.rutherford@kulas.com", "orn.robert@gmail.com", "grady.wendell@gmail.com", "zachariah95@hessel.com", "maryjane.wyman@yahoo.com", "nathaniel93@yahoo.com", "selena.zieme@yahoo.com", "feil.alexandrea@wyman.com", "feest.jazmyne@hotmail.com", "bins.berta@nienow.infobernier.shaniya@funk.com", "jeanne73@balistreri.com", "ines.schuppe@gmail.com", "kennedi.quigley@hotmail.com", "gleichner.ayla@hotmail.com", "christop.zulauf@gmail.com", "branson95@hermiston.com", "imuller@corwin.com", "jacobson.ashley@yahoo.com", "ariane.hand@hotmail.com", "beer.christa@hotmail.com", "rhianna73@hotmail.com", "sjaskolski@torp.com", "obahringer@hotmail.com", "marion75@yahoo.com", "vmertz@keeling.com", "naomie.rowe@homenick.com", "becker.adrain@gmail.com", "marilie.ritchie@hotmail.com", "zhodkiewicz@purdy.com", "gwendolyn29@west.com", "adrianna.murazik@schmidt.com", "kameron68@yahoo.com", "sanford.gladys@lehner.com", "kip27@thiel.com", "dino53@yahoo.com", "olarson@yahoo.com", "sigmund.herzog@maggio.com", "fhane@gmail.com", "sjast@hotmail.com", "sleuschke@yahoo.com", "psmith@deckow.com", "akeem72@yahoo.com", "green.hills@waelchi.com", "myriam.mclaughlin@yahoo.com", "genevieve.mraz@wisozk.com", "lindgren.hosea@hane.bizcrooks.violette@howe.com", "lisa.johns@price.infogoldner.emelia@hotmail.com", "rschultz@yahoo.com", "wilderman.zola@purdy.biztoy.carmella@hotmail.com", "kameron22@gmail.com", "bryce.mclaughlin@muller.com", "fernando75@gmail.com", "ernestine.lebsack@wisoky.com", "fbergnaum@gmail.com", "kavon51@mraz.com", "lizzie03@yahoo.com", "jerrold.welch@hoeger.com", "ola.bernier@yahoo.com", "idell36@goldner.bizjcollins@roberts.com", "thurman.schamberger@gmail.com", "soledad06@bartell.com", "bahringer.myrtice@yahoo.com", "heathcote.tiffany@yahoo.com", "laurie.rice@wilderman.com", "khalil.lebsack@bergnaum.com", "gmclaughlin@schimmel.com", "gwisoky@gmail.com", "farrell.adalberto@maggio.infodedric37@hotmail.com", "batz.kallie@gmail.com", "emilia.koss@mcclure.com", "kaia.leffler@yahoo.com", "olin53@hotmail.com", "treutel.regan@gmail.com", "ed31@schaefer.infotrinity.prosacco@rempel.com", "bayer.olga@satterfield.com", "blanda.madeline@schiller.com", "rtorphy@fay.com", "roscoe37@hotmail.com", "tiffany25@wiegand.com", "hackett.edison@denesik.bizhilpert.adriel@jenkins.com", "lawson82@mante.bizauer.rowena@haag.com", "dortha02@hotmail.com", "jaunita.mckenzie@yahoo.com", "eriberto.weimann@gmail.com", "gerardo.hamill@yahoo.com", "will.dale@orn.infoanita.hagenes@beatty.com", "lmueller@hotmail.com", "jon69@gmail.com", "barton22@robel.com", "annamae.schmeler@stoltenberg.com", "breanne.keebler@hotmail.com", "apollich@corkery.com", "alexis65@lubowitz.com", "esta20@yahoo.com", "karley01@gmail.com", "langworth.mollie@kuvalis.com", "quitzon.jaeden@russel.com", "lpfannerstill@nicolas.com", "zanderson@gmail.com", "jamie50@gmail.com", "stanton.myriam@reichel.com", "kailyn.durgan@hotmail.com", "tklein@yahoo.com", "candido.crooks@heller.com", "eframi@vandervort.com", "diana48@schoen.com", "schaden.heather@wisoky.com", "jwisozk@labadie.com", "raphaelle.koss@mitchell.com", "huels.andreane@reynolds.com", "theresia.nikolaus@langosh.com", "torphy.dean@rempel.com", "tiana.collier@hotmail.com", "arnoldo15@gmail.com", "acarroll@hintz.com", "lucinda77@becker.com", "qmedhurst@macejkovic.com", "monroe37@vonrueden.com", "jerrold91@yahoo.com", "harry34@gmail.com", "idell.kreiger@homenick.infodexter.torphy@gmail.com", "rmccullough@howe.bizdawson95@yahoo.com", "clara51@hotmail.com", "beau.skiles@gmail.com", "jalon.gulgowski@bartoletti.infoegutkowski@yahoo.com", "xzavier.fadel@gmail.com", "sidney.runolfsson@yahoo.com", "fisher.jerry@hamill.com", "adella.metz@jacobs.com", "muhammad02@ratke.infoeoreilly@cruickshank.com", "pamela.brekke@cummings.com", "cheyenne78@robel.com", "kemmer.scarlett@smitham.com", "fahey.dee@gmail.com", "jaida59@hotmail.com", "lessie28@luettgen.com", "krystal83@gibson.bizewilliamson@keebler.com", "addie84@gmail.com", "estel23@hotmail.com", "wilfredo.wolff@gmail.com", "callie.corkery@hirthe.com", "brain.erdman@hotmail.com", "cremin.madisyn@hotmail.com", "ghoppe@towne.com", "friesen.elta@huel.com", "xzavier.wunsch@kuvalis.inforosendo50@lind.com", "karson.paucek@gmail.com", "estel.marks@hotmail.com", "ghodkiewicz@dubuque.com", "spencer.mitchel@gmail.com", "enos81@wolff.com", "eichmann.amir@grimes.com", "rogahn.rex@koss.com", "josiah.trantow@hills.biztwalsh@langosh.com", "altenwerth.tyreek@cole.com", "lyda62@terry.com", "ricardo.nikolaus@yahoo.com", "wbradtke@prohaska.infodonnelly.alberto@hotmail.com", "terrence30@schuppe.com", "melody35@bartoletti.com", "lonny.carter@casper.com", "cartwright.monte@hotmail.com", "carter.dare@hotmail.com", "joesph28@gmail.com", "will.marion@fritsch.com", "dkemmer@hotmail.com", "hilton38@gmail.com", "luciano68@mosciski.biztatyana39@halvorson.com", "faye38@gmail.com", "nadia.shields@gmail.com", "tracey.hintz@watsica.com", "fstiedemann@prohaska.infogordon.collier@wiza.com", "joel.terry@hotmail.com", "lilliana.homenick@steuber.com", "domenico.lind@littel.bizywitting@yahoo.com", "nathen.homenick@hotmail.com", "harmon61@wiza.com", "diego.homenick@dach.com", "virgie.romaguera@gmail.com", "ispinka@yahoo.com", "adams.lawson@fadel.com", "kschiller@hotmail.com", "rosendo.smith@harvey.com", "lubowitz.candida@hotmail.com", "vhahn@wisoky.com", "yhickle@buckridge.com", "zmohr@hotmail.com", "lew.emmerich@yahoo.com", "valerie31@hotmail.com", "ines.kohler@borer.com", "ydicki@schmeler.com", "west.kailey@gmail.com", "rosendo.stiedemann@hotmail.com", "harvey.kamille@yahoo.com", "eglover@rowe.infoquinn66@kuvalis.com", "hbartell@yahoo.com", "quitzon.harold@gmail.com", "hdoyle@gmail.com", "josiane.kunde@hotmail.com", "elenora.lemke@torphy.com", "donald71@hotmail.com", "dach.kari@schamberger.com", "scarlett66@jacobson.com", "moore.sandrine@hotmail.com", "esipes@cruickshank.com", "phane@yahoo.com", "ohills@yahoo.com", "lydia63@herzog.com", "cleveland65@gmail.com", "oschoen@pouros.com", "darian32@ortiz.com", "tjones@gmail.com", "jeramie28@hills.com", "cerdman@hotmail.com", "conroy.sidney@hotmail.com", "schumm.thomas@gmail.com", "pbatz@bins.bizbecker.sonny@yahoo.com", "bogan.neoma@deckow.bizdamon92@yahoo.com", "ola71@hills.com", "darian.raynor@hotmail.com", "lindsay.bartoletti@hotmail.com", "fay.santos@bahringer.com", "savanna84@leuschke.com", "hayden50@hodkiewicz.bizalek98@wolf.infoamoore@gmail.com", "jast.odessa@kub.com", "zaria03@wuckert.bizbrennon.kreiger@yost.com", "bmoen@hotmail.com", "enoch.quigley@greenholt.com", "wdavis@durgan.com", "tristin68@yahoo.com", "savion01@swaniawski.com", "aliya01@greenholt.bizeileen.kuhic@mann.com", "klein.eleanora@leffler.com", "vincent50@farrell.com", "zwyman@gmail.com", "cecil.rowe@gmail.com", "lela52@gmail.com", "lueilwitz.yvette@gmail.com", "carroll.cloyd@jaskolski.com", "cremin.hester@hayes.com", "ebert.mallory@hotmail.com", "augustus93@hotmail.com", "kaela16@gmail.com", "vhermiston@kunze.com", "pat.bashirian@gusikowski.com", "schulist.valerie@hotmail.com", "mueller.aisha@murphy.infobosco.albert@hotmail.com", "zoe.schimmel@gmail.com", "vicenta.gulgowski@yahoo.com", "kkonopelski@stroman.infokamryn94@hotmail.com", "sswaniawski@greenfelder.com", "lisa46@yahoo.com", "kaylah20@heaney.com", "lane19@gmail.com", "joey29@yahoo.com", "eudora.reichert@kuhn.infoewalter@renner.com", "ehermiston@gmail.com", "walker.carissa@thiel.com", "lockman.nedra@vandervort.com", "ledner.eleanora@williamson.com", "ignatius29@adams.com", "berniece45@macejkovic.com", "shawn31@yahoo.com", "orn.geovanny@witting.com", "liliana28@boyle.com", "evie.russel@renner.com", "vratke@halvorson.infojonathan64@gmail.com", "hirthe.liliane@hotmail.com", "franco96@hudson.com", "sanford.kiehn@yahoo.com", "harvey.joel@bergstrom.com", "harvey.dovie@thompson.com", "hermiston.bo@yahoo.com", "ethelyn64@lemke.com", "reichel.ayla@yahoo.com", "melyna82@hotmail.com", "yrohan@gmail.com", "kozey.leta@yahoo.com", "jacynthe.kerluke@schaefer.com", "narciso.ullrich@dubuque.infoherzog.alejandra@yahoo.com", "wshanahan@yahoo.com", "alexandra.lehner@hotmail.com", "mccullough.aleen@gmail.com", "napoleon87@gmail.com", "ofeeney@lockman.com", "ludwig.lakin@dicki.com", "wilford.berge@gmail.com", "bernadine.franecki@stiedemann.com", "camilla.abshire@hotmail.com", "merritt06@yahoo.com", "gtromp@yahoo.com", "bryana.nienow@ondricka.com", "nola00@yahoo.com", "jdoyle@gmail.com", "wbins@legros.com", "pbergnaum@gmail.com", "torn@pollich.bizlegros.josiane@hotmail.com", "jeremie.feeney@hansen.infoorutherford@gmail.com", "zlind@bartell.com", "jerry.friesen@gmail.com", "west.josue@gmail.com", "xtillman@hotmail.com", "mertz.dax@yahoo.com", "hannah66@williamson.com", "dicki.clovis@hettinger.com", "selmer.hettinger@cummerata.com", "daisy.tremblay@stehr.com", "jcummings@hettinger.bizdaryl83@yahoo.com", "keebler.guy@gmail.com", "anna04@yahoo.com", "ibosco@hotmail.com", "gianni44@cole.com", "kenton78@yahoo.com", "wkemmer@beier.com", "ondricka.eloy@hotmail.com", "gibson.blaise@hotmail.com", "koss.geo@yahoo.com", "beer.cody@hotmail.com", "clint74@gmail.com", "delores.kunde@gmail.com", "forrest19@yahoo.com", "claudie57@russel.com", "rahul.beer@champlin.com", "iward@zieme.com", "hill.destiny@yahoo.com", "rath.meggie@gmail.com", "tyra29@green.com", "leone.miller@hotmail.com", "danial07@willms.com", "skemmer@hotmail.com", "kertzmann.giles@hyatt.com", "xsmitham@gmail.com", "simonis.leonel@mccullough.com", "luna23@yahoo.com", "maeve53@gmail.com", "reinger.korey@wunsch.infohelene.abernathy@hotmail.com", "manuela.borer@gmail.com", "boris76@jones.com", "madelyn.towne@ritchie.com", "funk.jenifer@hotmail.com", "walker.elsa@gmail.com", "alyce66@ohara.com", "nellie43@yahoo.com", "wluettgen@gmail.com", "kyla.buckridge@gmail.com", "fcarroll@watsica.com", "lwolff@brekke.com", "patrick.kirlin@greenfelder.com", "lula.osinski@bernhard.com", "khegmann@hotmail.com", "cummings.michelle@hotmail.com", "tpouros@yost.com", "hkoelpin@hotmail.com", "lwillms@hotmail.com", "pmclaughlin@bailey.com", "bart70@leffler.com", "xhomenick@gmail.com", "kamron00@hotmail.com", "kianna.murazik@heidenreich.com", "ernie.bartoletti@mante.bizrblanda@lynch.com", "doyle.fern@lueilwitz.com", "otho.franecki@mohr.com", "cory.wiegand@stiedemann.com", "greenfelder.gianni@kris.com", "justen07@yahoo.com", "edoyle@greenholt.com", "xrowe@brekke.com", "gleason.danny@gmail.com", "zcarroll@yahoo.com", "nitzsche.kristofer@hotmail.com", "oveum@yahoo.com", "mia69@effertz.com", "oauer@gmail.com", "rippin.myrtie@koch.com", "gboehm@wuckert.com", "aparker@greenholt.com", "dkautzer@hotmail.com", "alfonzo.beatty@gmail.com", "nbreitenberg@schmitt.com", "arjun.kozey@reinger.com", "eugene99@yahoo.com", "pete79@yahoo.com", "isac.leannon@gmail.com", "irving97@berge.infoadeline80@paucek.com", "trempel@hotmail.com", "phyllis.moore@gmail.com", "nora41@hotmail.com", "wolf.presley@gmail.com", "jjacobi@gmail.com", "felton40@hotmail.com", "acarroll@yahoo.com", "abbott.franz@gmail.com", "ewaelchi@cassin.com", "kelley.mertz@goldner.com", "breanna94@hahn.com", "abner87@gmail.com", "jazmin.crona@sporer.infofadel.jeff@kertzmann.infojacobi.elisabeth@gmail.com", "stokes.luna@gmail.com", "davis.jamison@carter.com", "hans.sawayn@breitenberg.com", "tschmitt@feest.com", "isobel10@prohaska.com", "monica.daugherty@yahoo.com", "mbosco@hotmail.com", "moore.eriberto@deckow.com", "theodora85@gmail.com", "ajacobson@hotmail.com", "uhayes@schumm.com", "delfina82@gmail.com", "nella85@yahoo.com", "zackary29@anderson.inforjacobi@hotmail.com", "tlittel@nicolas.com", "jones.kole@lehner.bizfleta33@yahoo.com", "unique25@abbott.com", "wklocko@kautzer.infogchristiansen@yahoo.com", "wkunze@gmail.com", "yhirthe@yahoo.com", "hector.ziemann@hane.com", "alycia.gerhold@gmail.com", "ewehner@yahoo.com", "boconner@yahoo.com", "vhodkiewicz@cruickshank.com", "larue56@hotmail.com", "qsenger@yahoo.com", "fprice@koepp.com", "alysson94@raynor.com", "retha99@abbott.com", "jtromp@quitzon.com", "cgoyette@doyle.com", "everette49@gmail.com", "pfannerstill.ila@hotmail.com", "betty01@gmail.com", "jkuhic@yahoo.com", "ollie92@gmail.com", "grady.roy@hotmail.com", "nwaelchi@lockman.com", "durgan.archibald@yahoo.com", "jody.dickens@kunde.com", "emilie.littel@gmail.com", "kuhlman.alexandra@fahey.com", "wdoyle@gmail.com", "colin06@skiles.com", "crunolfsdottir@carroll.com", "beier.kiel@schamberger.bizmason50@gmail.com", "freddy.stamm@gmail.com", "june.morar@spencer.com", "kim15@weimann.com", "zleuschke@aufderhar.com", "vicenta.zboncak@rosenbaum.com", "kovacek.kiera@yahoo.com", "elias.prohaska@pollich.com", "wiegand.kenya@hotmail.com", "zemlak.cathrine@yahoo.com", "hermiston.elena@hotmail.com", "carolyn.will@langosh.com", "zschumm@gmail.com", "judy63@gutkowski.com", "legros.damion@hotmail.com", "rodolfo.zulauf@grant.com", "hane.gavin@gmail.com", "morissette.marcel@hotmail.com", "ereilly@yahoo.com", "delaney.schinner@raynor.com", "stroman.julio@schroeder.com", "ayden.lueilwitz@koss.com", "ndubuque@wiegand.infobeulah.hills@yahoo.com", "sedrick.emmerich@olson.com", "smitham.lamont@schultz.com", "runolfsson.wendy@walsh.com", "rahul37@kling.com", "kenton24@strosin.com", "shanelle.ondricka@cruickshank.com", "zieme.raheem@hotmail.com", "filiberto34@haley.com", "camille99@hotmail.com", "collins.eda@hotmail.com", "carter.holden@walter.com", "jaquan71@gmail.com", "unique.bins@yahoo.com", "morton.glover@gmail.com", "bhegmann@mosciski.com", "skiles.myriam@grant.infokaitlyn.boyle@yahoo.com", "adolf.maggio@yahoo.com", "cschiller@gmail.com", "hershel.runte@hotmail.com", "mclaughlin.jamal@kunze.com", "keon.gorczany@hotmail.com", "zion24@gmail.com", "marcus97@gmail.com", "kaya31@kuhlman.com", "rice.linwood@rowe.com", "koch.florencio@yahoo.com", "oren70@krajcik.com", "jakubowski.wilton@hotmail.com", "chalvorson@bosco.com", "weimann.kyra@yahoo.com", "thalia71@flatley.com", "eloy85@gmail.com", "ryley91@yahoo.com", "bbeatty@yahoo.com", "mante.brando@yahoo.com", "dvolkman@yahoo.com", "bonita.hammes@gmail.com", "devin.cassin@hotmail.com", "cormier.geovanny@gmail.com", "kreiger.sabina@hotmail.com", "mjerde@lindgren.com", "yemmerich@gmail.com", "reece24@lesch.infonicolette49@hotmail.com", "wilber.keebler@yahoo.com", "madelynn.ferry@hotmail.com", "alexander.schulist@yahoo.com", "wmraz@yost.bizwest.karen@yahoo.com", "adalberto.dibbert@gusikowski.infoqcrist@yahoo.com", "qsawayn@wilderman.com", "hammes.luigi@yahoo.com", "lueilwitz.osborne@hotmail.com", "cleffler@robel.com", "everett66@hotmail.com", "shyann88@bahringer.com", "langworth.ben@yahoo.com", "russel.adella@gmail.com", "blehner@mcglynn.com", "jrowe@hotmail.com", "jesus81@cronin.com", "frederick38@yahoo.com", "isabel42@botsford.com", "vschmitt@beier.com", "julianne34@yahoo.com", "arnaldo53@yahoo.com", "beahan.meghan@hotmail.com", "lourdes33@kunde.com", "trempel@yahoo.com", "orlando93@spinka.com", "jamey.moen@yahoo.com", "gonzalo.becker@schmidt.com", "dmann@hotmail.com", "zboncak.genevieve@gmail.com", "rebekah15@dicki.com", "americo.kris@hotmail.com", "djenkins@oberbrunner.bizbergstrom.carlotta@pfeffer.com", "franz.upton@hotmail.com", "maggio.tre@hotmail.com", "kelley25@carroll.com", "autumn.klocko@yahoo.com", "corwin.hermann@morissette.com", "ehilpert@hotmail.com", "shanon.bergnaum@gmail.com", "morissette.irving@hotmail.com", "dora22@gmail.com", "bechtelar.nichole@howell.com", "rmaggio@gmail.com", "sierra.mckenzie@torp.com", "rohan.kole@swaniawski.infosylvan.boyer@hotmail.com", "bgutmann@bogan.com", "cummings.jaqueline@gmail.com", "antonia.gaylord@gmail.com", "levi02@waelchi.com", "dmiller@windler.com", "jerde.letha@gmail.com", "sanford.kade@yahoo.com", "bridget80@vonrueden.com", "zbeier@feeney.com", "lynch.lilla@kautzer.com", "lueilwitz.marty@hotmail.com", "uabbott@gmail.com", "cronin.cory@kulas.com", "cummings.carol@okuneva.bizlue22@yahoo.com", "cleora.feil@reynolds.com", "fabian89@luettgen.com", "susan.mcglynn@gmail.com", "neil29@stoltenberg.com", "mossie68@bartoletti.com", "gonzalo.mcdermott@vonrueden.com", "noemy.pfeffer@gmail.com", "edgardo.feil@gmail.com", "bartell.thalia@gmail.com", "cremin.jonatan@gmail.com", "dayne46@yahoo.com", "luettgen.brisa@gmail.com", "everett57@nader.com", "kbeahan@koss.infombeier@stark.com", "osborne.hilpert@bernier.com", "regan87@donnelly.com", "wswaniawski@towne.com", "roberts.crawford@kling.com", "bobbie.gulgowski@lubowitz.com", "champlin.oswaldo@gmail.com", "levi.towne@yahoo.com", "jschamberger@gmail.com", "stanford.schuppe@becker.com", "vpacocha@hotmail.com", "kale14@gmail.com", "jayne.pacocha@becker.com", "hills.logan@spencer.infojanessa88@gmail.com", "izaiah.wehner@hotmail.com", "vbecker@considine.com", "mstiedemann@gmail.com", "shanahan.jamal@hotmail.com", "hamill.rodrick@dickinson.com", "shannon14@morar.com", "champlin.erna@gmail.com", "carissa.marks@feest.com", "jacobi.felix@yahoo.com", "vilma38@kessler.com", "qmoen@cormier.com", "ihettinger@gmail.com", "laury30@gmail.com", "ima17@wisoky.com", "coleman.paucek@pacocha.com", "rbrakus@cremin.com", "windler.luis@hotmail.com", "pat01@balistreri.com", "brown.hoeger@hotmail.com", "ebert.jabari@gmail.com", "rempel.amelia@krajcik.com", "dorthy.kris@gmail.com", "gaylord41@kerluke.com", "windler.kiara@hotmail.com", "uharris@yahoo.com", "olson.queen@hotmail.com", "quitzon.maybell@hotmail.com", "hlang@yahoo.com", "davin.bernier@pfannerstill.com", "mueller.chris@tremblay.com", "mhegmann@gmail.com", "icrist@dickens.infowatsica.paige@yahoo.com", "nlemke@hotmail.com", "romaine.ferry@hotmail.com", "qlegros@abernathy.com", "herman.lonzo@jaskolski.com", "hermann81@gulgowski.com", "reynolds.merl@yahoo.com", "kenyatta72@corwin.com", "kristina38@nolan.com", "kulas.blanca@hotmail.com", "frederic.gleason@ritchie.com", "donnell.bosco@gmail.com", "lesch.candelario@yahoo.com", "hermann.ariel@mills.com", "uriah18@yahoo.com", "leta.koch@hotmail.com", "ottilie.beatty@yahoo.com", "heidenreich.nicola@hotmail.com", "raphael53@yahoo.com", "ehaley@gmail.com", "makayla11@gmail.com", "ullrich.meda@lebsack.com", "hbreitenberg@stark.com", "vstreich@hotmail.com", "odubuque@schowalter.bizastrid.ryan@yahoo.com", "koch.adrien@hotmail.com", "hessel.candido@gislason.com", "alvera53@gmail.com", "qwiza@damore.com", "braden63@hotmail.com", "gerhold.abby@schumm.com", "mccullough.jadyn@hotmail.com", "aubree13@runolfsson.com", "juliet56@hotmail.com", "rosenbaum.delmer@haley.com", "ernestine.kuphal@hotmail.com", "hgleason@hotmail.com", "ymedhurst@white.infobwindler@legros.com", "patience.dibbert@friesen.com", "ignacio.lehner@gmail.com", "hulda.padberg@gmail.com", "gilberto.russel@simonis.com", "brigitte.rau@hotmail.com", "xjaskolski@hotmail.com", "harber.tina@hotmail.com", "gabriel49@gmail.com", "kschoen@hotmail.com", "marcelle.heaney@cassin.com", "antonia46@brakus.com", "kuvalis.hertha@zemlak.com", "gorczany.clementina@gmail.com", "rschneider@hotmail.com", "huel.nick@fritsch.com", "dare.nichole@ryan.com", "zaria.balistreri@waelchi.com", "peggie.wuckert@hotmail.com", "idubuque@gmail.com", "dlarson@kling.com", "willms.eloise@auer.com", "dickinson.gaylord@yahoo.com", "amelia.steuber@koss.bizparker03@balistreri.com", "wdaniel@gmail.com", "drenner@collier.com", "ashleigh.bahringer@gmail.com", "tillman.ansel@swaniawski.com", "bryana.nienow@yahoo.com", "tyrese28@hoeger.com", "xmetz@kemmer.com", "enrique42@collier.com", "kyle56@hotmail.com", "eusebio00@gmail.com", "marge79@hotmail.com", "toy59@hotmail.com", "golden.bradtke@gmail.com", "von.theo@gmail.com", "schmitt.claudine@fahey.com", "julie29@yahoo.com", "aida14@yahoo.com", "cartwright.pierre@greenholt.com", "granville62@hotmail.com", "jess.klocko@gmail.com", "stuart.kling@marks.com", "kgrady@kuvalis.biznlarkin@lowe.com", "vhalvorson@schiller.com", "darryl68@yahoo.com", "dane.hills@yahoo.com", "rebeka.koss@dare.com", "mgreen@goodwin.bizlesley.hettinger@conroy.com", "jayne.ortiz@yahoo.com", "hauck.mariano@baumbach.com", "leone71@barrows.com", "skiles.damaris@yahoo.com", "hallie.haley@hotmail.com", "berge.jamison@gmail.com", "joana.larkin@rowe.com", "champlin.jedediah@windler.com", "fay.leslie@yahoo.com", "maureen71@gmail.com", "andrew.dietrich@gmail.com", "swunsch@hotmail.com", "melyssa29@bergstrom.com", "kshlerin.orie@cruickshank.bizpconn@hotmail.com", "hildegard.stehr@torphy.com", "emmie01@hotmail.com", "irma.muller@hoeger.com", "rogahn.jana@crona.com", "jcole@runte.com", "clementine67@beahan.com", "emile12@cronin.com", "ustrosin@hotmail.com", "wunsch.roderick@yahoo.com", "kirlin.emerald@hotmail.com", "stehr.kyler@gmail.com", "alexandre.mertz@wyman.com", "hdurgan@beier.infokadin.simonis@gmail.com", "hamill.regan@hegmann.com", "neva.crona@hotmail.com", "hilma26@yahoo.com", "loy26@vonrueden.com", "lueilwitz.rhea@torphy.com", "forn@hotmail.com", "jones.filomena@yahoo.com", "lempi78@nitzsche.com", "hope94@feeney.com", "ssatterfield@yahoo.com", "smitham.joshuah@swaniawski.com", "uwillms@wisozk.com", "wgrimes@hotmail.com", "lue.hartmann@haley.com", "eglover@yahoo.com", "jessie.robel@hoeger.com", "oconnell.melyssa@gmail.com", "samir.mckenzie@fahey.com", "llynch@yahoo.com", "hamill.regan@streich.com", "rosina33@yahoo.com", "yundt.jeramie@wiegand.com", "ustiedemann@gmail.com", "raymond.miller@hotmail.com", "stiedemann.mireille@yahoo.com", "cortez.fisher@gmail.com", "alisa45@schaefer.com", "kayden.hudson@yahoo.com", "donna04@hotmail.com", "chase.terry@yahoo.com", "jody92@yahoo.com", "geovanny.shields@gmail.com", "ldickinson@gmail.com", "carol.crona@kuhn.com", "columbus.stamm@hotmail.com", "labernathy@hotmail.com", "hoppe.dan@feeney.com", "eudora.marquardt@stamm.com", "effertz.colin@leffler.bizelissa19@gmail.com", "keara.kshlerin@gmail.com", "hand.therese@mueller.com", "qblick@armstrong.com", "giuseppe42@hotmail.com", "pschultz@torphy.com", "xbosco@mosciski.com", "newell.crooks@hotmail.com", "mekhi.shields@wilkinson.com", "justus63@hotmail.com", "emilie03@kutch.com", "hamill.judson@yahoo.com", "willa87@stiedemann.com", "nrussel@tromp.com" };

            var usersCount = Math.Min(usersToGenerate, emails.Length);

            for (int i = 0; i < usersCount; i++)
            {
                User newUser = new User
                {
                    Email = emails[i],
                    Name = RandomGenerator.RandomString(5),
                    Surname = RandomGenerator.RandomString(10),
                    Birthdate = new DateTime(),
                    Password = RandomGenerator.RandomString(15),
                    Role = Role.Basic
                };

                Users.Add(newUser);
            }
            UserReviewsTours();
        }

        private void UserReviewsTours()
        {
            var toursToReview = 1;

            for (int j = 0; j < toursToReview; j++)
            {
                Review newReview = new Review
                {
                    UserId = Users.First().Id,
                    TourId = Tours.ElementAt(j).Id,
                    Score = 5,
                };

                Reviews.Add(newReview);
            }
        }

        public void FillDatabase(DBManager.Infrastructure.Contracts.Contexts.NgContext context)
        {
            context.Image.AddRange(Images);
            context.Tag.AddRange(Tags);
            context.Tour.AddRange(Tours);
            context.TourTag.AddRange(TourTags);
            context.Node.AddRange(Nodes);
            context.Restaurant.AddRange(Restaurants);
            context.User.AddRange(Users);
            context.Review.AddRange(Reviews);
            context.SaveChanges();
        }

        public DBManager.Infrastructure.Contracts.Contexts.NgContext GenerateInMemoryContext()
        {
            var builder = new DbContextOptionsBuilder<DBManager.Infrastructure.Contracts.Contexts.NgContext>();
            builder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            DBManager.Infrastructure.Contracts.Contexts.NgContext context = new DBManager.Infrastructure.Contracts.Contexts.NgContext(builder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}