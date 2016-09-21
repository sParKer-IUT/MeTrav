using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeTrav
{
    public class MeTravUtility
    {
        public static List<Location> AllLocations = null;

        public static List<List<Location>> AllRoutes = null;

        public static List<string> AllBuses = null;


        public static void FillAllBuses()
        {
            AllBuses = new List<string>();
            
            AllBuses.Add("Anabil, Salsabil, Suprovat");
            
            AllBuses.Add("Bolaka, BRTC, VIP-27");
        }

        public static void FillAllRoutes()
        {
            AllRoutes = new List<List<Location>>();

            List<Location> loc;

            #region abdullahpur-khilgaon
            loc = new List<Location>();

            loc.Add(new Location("Abdullahpur", 23.87985, 90.40132, LocationType.BusStop));
            //loc.Add(new Location("Abdullahpur Counter Bus", 23.87746, 90.40127, LocationType.BusStop));
            loc.Add(new Location("House Building", 23.87372, 90.40073, LocationType.BusStop));
            //loc.Add(new Location("HouseBuilding Counter", 23.87219, 90.40073, LocationType.BusStop));
            //loc.Add(new Location("Azampur Counter", 23.86936, 90.40047, LocationType.BusStop));
            loc.Add(new Location("Azampur Local", 23.86836, 90.40041, LocationType.BusStop));
            loc.Add(new Location("Rajlokkhi", 23.86451, 90.40016, LocationType.BusStop));
            //loc.Add(new Location("Rajlokkhi Counter", 23.86359, 90.40012, LocationType.BusStop));
            loc.Add(new Location("Jasimuddin", 23.86089, 90.40043, LocationType.BusStop));
            loc.Add(new Location("Airport", 23.85151, 90.40789, LocationType.BusStop));
            loc.Add(new Location("Kawla", 23.84623, 90.41216, LocationType.BusStop));

            loc.Add(new Location("Khilkhet", 23.82997, 90.41993, LocationType.BusStop));
            loc.Add(new Location("Kuril Chowrasta", 23.81631, 90.42116, LocationType.BusStop));
            loc.Add(new Location("Basundhara", 23.81200, 90.42131, LocationType.BusStop));
            loc.Add(new Location("Nadda", 23.80931, 90.42145, LocationType.BusStop));
            loc.Add(new Location("Natun Bajar", 23.79737, 90.42366, LocationType.BusStop));
            loc.Add(new Location("Bashtola", 23.79452, 90.42421, LocationType.BusStop));
            loc.Add(new Location("Shahjadpur", 23.79193, 90.42467, LocationType.BusStop));
            loc.Add(new Location("Uttar Badda", 23.78577, 90.42571, LocationType.BusStop));
            loc.Add(new Location("Badda", 23.78062, 90.42569, LocationType.BusStop));
            loc.Add(new Location("Madhya Badda", 23.77785, 90.42561, LocationType.BusStop));
            loc.Add(new Location("Rampura", 23.76549, 90.42179, LocationType.BusStop));
            loc.Add(new Location("Malibagh", 23.75007, 90.41306, LocationType.BusStop));
            loc.Add(new Location("Khilgaon", 23.74800, 90.42365, LocationType.BusStop));

            AllRoutes.Add(loc);
            #endregion

            #region abdullahpur-mohakhali
            loc = new List<Location>();

            loc.Add(new Location("Abdullahpur", 23.87985, 90.40132, LocationType.BusStop));
            //loc.Add(new Location("Abdullahpur Counter Bus", 23.87746, 90.40127, LocationType.BusStop));
            loc.Add(new Location("House Building", 23.87372, 90.40073, LocationType.BusStop));
            //loc.Add(new Location("HouseBuilding Counter", 23.87219, 90.40073, LocationType.BusStop));
            //loc.Add(new Location("Azampur Counter", 23.86936, 90.40047, LocationType.BusStop));
            loc.Add(new Location("Azampur Local", 23.86836, 90.40041, LocationType.BusStop));
            loc.Add(new Location("Rajlokkhi", 23.86451, 90.40016, LocationType.BusStop));
            //loc.Add(new Location("Rajlokkhi Counter", 23.86359, 90.40012, LocationType.BusStop));
            loc.Add(new Location("Jasimuddin", 23.86089, 90.40043, LocationType.BusStop));
            loc.Add(new Location("Airport", 23.85151, 90.40789, LocationType.BusStop));
            loc.Add(new Location("Kawla", 23.84623, 90.41216, LocationType.BusStop));

            loc.Add(new Location("Khilkhet", 23.82997, 90.41993, LocationType.BusStop));
            //loc.Add(new Location("Khilkhet South", 23.82822, 90.42020, LocationType.BusStop));
            loc.Add(new Location("Bishwa Road", 23.82118, 90.41865, LocationType.BusStop));
            loc.Add(new Location("Shewra", 23.81841, 90.41443, LocationType.BusStop));
            loc.Add(new Location("MES", 23.81615, 90.40532, LocationType.BusStop));
            loc.Add(new Location("Kakoli", 23.79495, 90.40120, LocationType.BusStop));
            loc.Add(new Location("Banani South", 23.79333, 90.40093, LocationType.BusStop));
            loc.Add(new Location("Shainik Club", 23.79043, 90.40043, LocationType.BusStop));
            loc.Add(new Location("Chairman Bari", 23.78805, 90.40003, LocationType.BusStop));
            loc.Add(new Location("Mohakhali", 23.78110, 90.39890, LocationType.BusStop));

            AllRoutes.Add(loc);
            #endregion

        }

        public static void FillAllLocationList()
        {
            AllLocations = new List<Location>();

            // Syntax -> AllLocations.Add(CreateLocation("", 1.1, 2.2));

            #region Bus stops
            AllLocations.Add(new Location("Abdullahpur", 23.87985, 90.40132, LocationType.BusStop));
            AllLocations.Add(new Location("House Building", 23.87372, 90.40073, LocationType.BusStop));
            AllLocations.Add(new Location("Azampur Local", 23.86836, 90.40041, LocationType.BusStop));
            AllLocations.Add(new Location("Rajlokkhi", 23.86451, 90.40016, LocationType.BusStop));
            AllLocations.Add(new Location("Jasimuddin", 23.86089, 90.40043, LocationType.BusStop));
            AllLocations.Add(new Location("Airport", 23.85151, 90.40789, LocationType.BusStop));
            AllLocations.Add(new Location("Kawla", 23.84623, 90.41216, LocationType.BusStop));
            AllLocations.Add(new Location("Khilkhet", 23.82997, 90.41993, LocationType.BusStop));

            AllLocations.Add(new Location("Bishwa Road", 23.82118, 90.41865, LocationType.BusStop));
            AllLocations.Add(new Location("Shewra", 23.81841, 90.41443, LocationType.BusStop));
            AllLocations.Add(new Location("MES", 23.81615, 90.40532, LocationType.BusStop));
            AllLocations.Add(new Location("Kakoli", 23.79495, 90.40120, LocationType.BusStop));
            AllLocations.Add(new Location("Banani South", 23.79333, 90.40093, LocationType.BusStop));
            AllLocations.Add(new Location("Shainik Club", 23.79043, 90.40043, LocationType.BusStop));
            AllLocations.Add(new Location("Chairman Bari", 23.78805, 90.40003, LocationType.BusStop));
            AllLocations.Add(new Location("Mohakhali", 23.78110, 90.39890, LocationType.BusStop));

            AllLocations.Add(new Location("Kuril Chowrasta", 23.81631, 90.42116, LocationType.BusStop));
            AllLocations.Add(new Location("Basundhara", 23.81200, 90.42131, LocationType.BusStop));
            AllLocations.Add(new Location("Nadda", 23.80931, 90.42145, LocationType.BusStop));
            AllLocations.Add(new Location("Natun Bajar", 23.79737, 90.42366, LocationType.BusStop));
            AllLocations.Add(new Location("Bashtola", 23.79452, 90.42421, LocationType.BusStop));
            AllLocations.Add(new Location("Shahjadpur", 23.79193, 90.42467, LocationType.BusStop));
            AllLocations.Add(new Location("Uttar Badda", 23.78577, 90.42571, LocationType.BusStop));
            AllLocations.Add(new Location("Badda", 23.78062, 90.42569, LocationType.BusStop));
            //Need to fix coordinate
            AllLocations.Add(new Location("Madhya Badda", 23.77785, 90.42561, LocationType.BusStop));
            AllLocations.Add(new Location("Rampura", 23.76549, 90.42179, LocationType.BusStop));
            AllLocations.Add(new Location("Malibagh", 23.75007, 90.41306, LocationType.BusStop));
            AllLocations.Add(new Location("Khilgaon", 23.74800, 90.42365, LocationType.BusStop));
            #endregion

            #region Gulshan area
            AllLocations.Add(new Location("Wireless More", 23.78066, 90.40553));
            AllLocations.Add(new Location("TB Gate", 23.78029, 90.40938));
            AllLocations.Add(new Location("Gulshan Bridge", 23.78040, 90.41290));
            AllLocations.Add(new Location("Gulshan-1", 23.78033, 90.41620));
            #endregion

            AllLocations.Add(new Location("Link Road", 23.78069, 90.42371));


            AllLocations.Add(new Location("Microsoft Bangladesh,Gulshan-1", 23.77723, 90.41619));


            AllLocations.Add(new Location("Polwell Carnation,Sector 8,Uttara", 23.87867, 90.40175));
            AllLocations.Add(new Location("Sonali Bank Staff College,Sector 8,Uttara", 23.87711, 90.40399));
            AllLocations.Add(new Location("Goaltek Mosque,Sector 8,Uttara", 23.87656, 90.40561));
            AllLocations.Add(new Location("ATM Brac Bank,Sector 8,Uttara", 23.879099, 90.401502));
            AllLocations.Add(new Location("Valentines Café,Sector 8,Uttara", 23.879242, 90.401688));



            AllLocations.Add(new Location("Uttara Town University College,Sector 9,Uttara", 23.87938, 90.40070));
            AllLocations.Add(new Location("Cambrian School and College,Sector 9,Uttara", 23.87712, 90.39754));
            AllLocations.Add(new Location("Uttara Adhunik Medical,Sector 9,Uttara", 23.87481, 90.39672));
            AllLocations.Add(new Location("Agrani Bank,Sector 9,Uttara", 23.87912, 90.40092));
            AllLocations.Add(new Location("Mercantile Bank,Sector 9,Uttara", 23.877820, 90.400955));
            AllLocations.Add(new Location("Xinxian,Sector 9,Uttara", 23.877654, 90.400909));
            AllLocations.Add(new Location("Grand Dhaka Hotel,Sector 9,Uttara", 23.877579, 90.400925));
            AllLocations.Add(new Location("Dhaka Boys College,Sector 9,Uttara", 23.879831, 90.398138));
            AllLocations.Add(new Location("Uttara City College,Sector 9,Uttara", 23.878808, 90.398511));
            AllLocations.Add(new Location("Uttara Ideal College,Sector 9,Uttara", 23.878574, 90.397250));
            AllLocations.Add(new Location("Momtaj Mohol,Sector 9,Uttara", 23.875753, 90.400432));
            AllLocations.Add(new Location("Daffodil Internation Instution of IT,Sector 9,Uttara", 23.875584, 90.400435));
            AllLocations.Add(new Location("Pubali Bank,Sector 9,Uttara", 23.875356, 90.400384));
            AllLocations.Add(new Location("CP Chicken,Sector 9,Uttara", 23.875354, 90.400148));
            AllLocations.Add(new Location("Ideal Products,Sector 9,Uttara", 23.874822, 90.400253));
            AllLocations.Add(new Location("AB Bank Limited,Sector 9,Uttara", 23.874789, 90.399923));
            AllLocations.Add(new Location("CFC,Sector 9,Uttara", 23.874915, 90.399733));
            AllLocations.Add(new Location("Boishakhi Restora,Sector 9,Uttara", 23.874660, 90.399341));
            AllLocations.Add(new Location("Womens World,Sector 9,Uttara", 23.874729, 90.398837));
            AllLocations.Add(new Location("ATM Mutual Trust Bank,Sector 9,Uttara", 23.874915, 90.398107));
            AllLocations.Add(new Location("Brac Bank Limited,Sector 9,Uttara", 23.874630, 90.398359));
            AllLocations.Add(new Location("ATM Dutch Bangla Bank Limited,Sector 9,Uttara", 23.874647, 90.397010));
            AllLocations.Add(new Location("Uttara Polytechnic Intitute,Sector 9,Uttara", 23.875049, 90.396046));
            AllLocations.Add(new Location("Artisan,Sector 9,Uttara", 23.874527, 90.395086));
            AllLocations.Add(new Location("Bata,Sector 9,Uttara", 23.874601, 90.396406));
            AllLocations.Add(new Location("Islamic University,Sector 9,Uttara", 23.876158, 90.397361));





            AllLocations.Add(new Location("Mascot Plaza,Sector 7,Uttara", 23.874307, 90.399891));
            AllLocations.Add(new Location("North Tower,Sector 7,Uttara", 23.874376, 90.400140));
            AllLocations.Add(new Location("ATM City Bank,Sector 7,Uttara", 23.874420, 90.399869));
            AllLocations.Add(new Location("Nokia Care,Sector 7,Uttara", 23.874513, 90.400145));
            AllLocations.Add(new Location("Amana Super Shop,Sector 7,Uttara", 23.874317, 90.400260));
            AllLocations.Add(new Location("Samsung Store,Sector 7,Uttara", 23.874162, 90.400276));
            AllLocations.Add(new Location("BUFT,Sector 7,Uttara", 23.873961, 90.400181));
            AllLocations.Add(new Location("Cheers,Sector 7,Uttara", 23.873846, 90.400111));
            AllLocations.Add(new Location("Jamuna Bank,Sector 7,Uttara", 23.873731, 90.400149));
            AllLocations.Add(new Location("National Bank,Sector 7,Uttara", 23.873559, 90.400208));
            AllLocations.Add(new Location("Coopers,Sector 7,Uttara", 23.874466, 90.399895));
            AllLocations.Add(new Location("ATM EBL,Sector 7,Uttara", 23.874365, 90.398535));
            AllLocations.Add(new Location("La Bamba,Sector 7,Uttara", 23.874284, 90.398530));
            AllLocations.Add(new Location("Masala Gosth,Sector 7,Uttara", 23.874367, 90.398452));
            AllLocations.Add(new Location("Yellow,Sector 7,Uttara", 23.874328, 90.398012));
            AllLocations.Add(new Location("City Restaurant,Sector 7,Uttara", 23.874296, 90.397808));
            AllLocations.Add(new Location("Curry in a Hurry,Sector 7,Uttara", 23.874340, 90.396762));
            AllLocations.Add(new Location("Bangladesh Commerce Bank Limited,Sector 7,Uttara", 23.874264, 90.396220));
            AllLocations.Add(new Location("FFC,Sector 7,Uttara", 23.874222, 90.396043));
            AllLocations.Add(new Location("Navana Furnitures,Sector 7,Uttara", 23.874271, 90.395743));
            AllLocations.Add(new Location("Grab and Co,Sector 7,Uttara", 23.874207, 90.395341));
            AllLocations.Add(new Location("Prims Hotel,Sector 7,Uttara", 23.873957, 90.395411));
            AllLocations.Add(new Location("Food Source BD,Sector 7,Uttara", 23.874232, 90.393783));
            AllLocations.Add(new Location("National Eye Hospital,Sector 7,Uttara", 23.873592, 90.396503));
            AllLocations.Add(new Location("Dhaka Womens University College,Sector 7,Uttara", 23.873545, 90.396868));
            AllLocations.Add(new Location("Bismillah Fast Food,Sector 7,Uttara", 23.873307, 90.396798));
            AllLocations.Add(new Location("The Chef Family Restaurant,Sector 7,Uttara", 23.872971, 90.398013));
            AllLocations.Add(new Location("Asian University of Bangladesh,Sector 7,Uttara", 23.872866, 90.397334));
            AllLocations.Add(new Location("Red Chicken,Sector 7,Uttara", 23.872944, 90.396798));
            AllLocations.Add(new Location("Fork Knife,Sector 7,Uttara", 23.872083, 90.396417));
            AllLocations.Add(new Location("ATM AB Bank Limited,Sector 7,Uttara", 23.871319, 90.400014));
            AllLocations.Add(new Location("Syed Grand Center,Sector 7,Uttara", 23.871314, 90.400111));
            AllLocations.Add(new Location("Pizza End,Sector 7,Uttara", 23.871194, 90.400261));
            AllLocations.Add(new Location("BNS Center,Sector 7,Uttara", 23.871113, 90.400092));
            AllLocations.Add(new Location("Asian University Of Bangladesh,Sector 7,Uttara", 23.870875, 90.399851));
            AllLocations.Add(new Location("Uttara High School and College,Sector 7,Uttara", 23.870792, 90.397357));
            AllLocations.Add(new Location("Secot 7 Park,Sector 7,Uttara", 23.870073, 90.397116));
            AllLocations.Add(new Location("RMC Hospital,Sector 7,Uttara", 23.870238, 90.399737));
            AllLocations.Add(new Location("ATM BRac Bank,Sector 7,Uttara", 23.869550, 90.393519));
            AllLocations.Add(new Location("Lake Terrace Restaurant,Sector 7,Uttara", 23.869606, 90.393377));
            AllLocations.Add(new Location("Coopers Bakery Bangladesh,Sector 7,Uttara", 23.869258, 90.393661));
            AllLocations.Add(new Location("Hi Care Hospital,Sector 7,Uttara", 23.868841, 90.393902));
            AllLocations.Add(new Location("Kabab Factory,Sector 7,Uttara", 23.867526, 90.393970));
            AllLocations.Add(new Location("Sangam Ice cream Parlour,Sector 7,Uttara", 23.867430, 90.393460));
            AllLocations.Add(new Location("Dhaka Eye Care Hospital,Sector 7,Uttara", 23.867859, 90.396072));
            AllLocations.Add(new Location("Mr Baker,Sector 7,Uttara", 23.867896, 90.397215));
            AllLocations.Add(new Location("Rajuk Commercial,Sector 7,Uttara", 23.868263, 90.399902));


            AllLocations.Add(new Location("Amir Complex,Sector 3,Uttara", 23.867576, 90.399881));
            AllLocations.Add(new Location("Teletalk Customer Care,Sector 3,Uttara", 23.867238, 90.399688));
            AllLocations.Add(new Location("Brac Bank,Sector 3,Uttara", 23.867638, 90.399334));
            AllLocations.Add(new Location("Family Needs,Sector 3,Uttara", 23.867466, 90.398741));
            AllLocations.Add(new Location("Backyard Chef,Sector 3,Uttara", 23.867498, 90.398470));
            AllLocations.Add(new Location("Uttara University,Sector 3,Uttara", 23.867473, 90.398258));
            AllLocations.Add(new Location("La Bamba,Sector 3,Uttara", 23.867179, 90.393661));
            AllLocations.Add(new Location("Americcan Burger,Sector 3,Uttara", 23.866747, 90.393806));
            AllLocations.Add(new Location("Hungry Eyes,Sector 3,Uttara", 23.866747, 90.393806));
            AllLocations.Add(new Location("Lambada Kabab,Sector 3,Uttara", 23.866761, 90.397756));
            AllLocations.Add(new Location("Bata,Sector 3,Uttara", 23.866945, 90.399847));
            AllLocations.Add(new Location("Thai Emerald,Sector 3,Uttara", 23.866945, 90.399847));
            AllLocations.Add(new Location("Walton Plaza,Sector 3,Uttara", 23.866592, 90.399957));
            AllLocations.Add(new Location("La Bamba,Sector 3,Uttara", 23.866300, 90.399793));
            AllLocations.Add(new Location("London Plaza,Sector 3,Uttara", 23.866025, 90.399680));
            AllLocations.Add(new Location("Baly Complex,Sector 3,Uttara", 23.865657, 90.399688));
            AllLocations.Add(new Location("Uttara Vaban,Sector 3,Uttara", 23.865122, 90.399656));
            AllLocations.Add(new Location("Hungry Duck,Sector 3,Uttara", 23.865159, 90.399310));
            AllLocations.Add(new Location("Islami Bank Bangladesh Limited,Sector 3,Uttara", 23.864931, 90.399661));
            AllLocations.Add(new Location("Social Islami Bank,Sector 3,Uttara", 23.864696, 90.399645));
            AllLocations.Add(new Location("Khajana,Sector 3,Uttara", 23.864642, 90.399707));
            AllLocations.Add(new Location("Computer Source,Sector 3,Uttara", 23.864549, 90.399390));
            AllLocations.Add(new Location("Daffodil International University,Sector 3,Uttara", 23.864613, 90.398395));
            AllLocations.Add(new Location("Veni Vidi Vici,Sector 3,Uttara", 23.866355, 90.397840));
            AllLocations.Add(new Location("Uttara Model Town Post Office,Sector 3,Uttara", 23.864530, 90.396563));
            AllLocations.Add(new Location("Uttara Friends Club,Sector 3,Uttara", 23.864751, 90.395839));
            AllLocations.Add(new Location("Fakhruddin Restaurant,Sector 3,Uttara", 23.865413, 90.394342));
            AllLocations.Add(new Location("International Turkish Hope School,Sector 3,Uttara", 23.864996, 90.393924));
            AllLocations.Add(new Location("Beans and Aroma Coffes,Sector 3,Uttara", 23.862263, 90.393887));
            AllLocations.Add(new Location("Sundarban Courier,Sector 3,Uttara", 23.861699, 90.394102));
            AllLocations.Add(new Location("Flexiload and Bikash Software Server,Sector 3,Uttara", 23.862504, 90.394901));
            AllLocations.Add(new Location("Rajlokkhi Complex,Sector 3,Uttara", 23.864108, 90.399584));
            AllLocations.Add(new Location("Royal Cousine,Sector 3,Uttara", 23.864162, 90.399149));
            AllLocations.Add(new Location("Tropical Alauddin Tower,Sector 3,Uttara", 23.864334, 90.398934));
            AllLocations.Add(new Location("Uttara Bank Limited,Sector 3,Uttara", 23.862877, 90.399540));
            AllLocations.Add(new Location("Mainland China,Sector 3,Uttara", 23.862553, 90.399315));
            AllLocations.Add(new Location("Prime Bank,Sector 3,Uttara", 23.862381, 90.399524));
            AllLocations.Add(new Location("United College of Aviation Science and Management,Sector 3,Uttara", 23.862759, 90.398508));
            AllLocations.Add(new Location("Aarong,Sector 3,Uttara", 23.861778, 90.399339));
            AllLocations.Add(new Location("Coffee World,Sector 3,Uttara", 23.861057, 90.398872));
            AllLocations.Add(new Location("Pizza Inn,Sector 3,Uttara", 23.860998, 90.399242));

            AllLocations.Add(new Location("Dhaka Bank,Sector 1,Uttara", 23.860263, 90.399980));
            AllLocations.Add(new Location("Southeast Bank,Sector 1,Uttara", 23.860263, 90.399980));
            AllLocations.Add(new Location("Georges Café,Sector 1,Uttara", 23.859434, 90.400436));
            AllLocations.Add(new Location("Richmond Hotel And Suites,Sector 1,Uttara", 23.859267, 90.400613));
            AllLocations.Add(new Location("Fire on Ice,Sector 1,Uttara", 23.859022, 90.401015));
            AllLocations.Add(new Location("Scholastica,Sector 1,Uttara", 23.858438, 90.401128));
            AllLocations.Add(new Location("Medical College for Women,Sector 1,Uttara", 23.858134, 90.400892));
            AllLocations.Add(new Location("Uttara Club,Sector 1,Uttara", 23.857982, 90.400795));
            AllLocations.Add(new Location("Xinxian,Sector 1,Uttara", 23.858119, 90.401723));
            AllLocations.Add(new Location("Uttara Central Hospital and Diagnostic Center,Sector 1,Uttara", 23.857702, 90.401954));
            AllLocations.Add(new Location("Fakhruddin Biriani,Sector 1,Uttara", 23.856240, 90.403252));
            AllLocations.Add(new Location("Makka Eye Hospital,Sector 1,Uttara", 23.856054, 90.402968));
            AllLocations.Add(new Location("Jahan Ara Clinic Private Limited,Sector 1,Uttara", 23.855431, 90.403858));
            AllLocations.Add(new Location("RAB HQ,Sector 1,Uttara", 23.857222, 90.399625));
            AllLocations.Add(new Location("Airport,Sector 1,Uttara", 23.846606, 90.406372));


            AllLocations.Add(new Location("Green Line Bus Counter,Sector 6,Uttara", 23.873755, 90.401115));
            AllLocations.Add(new Location("NCC Bank,Sector 6,Uttara", 23.874408, 90.400868));
            AllLocations.Add(new Location("Uttara Commerce College,Sector 6,Uttara", 23.874452, 90.401555));
            AllLocations.Add(new Location("Sylcom RestHouse Ltd,Sector 6,Uttara", 23.873166, 90.401383));
            AllLocations.Add(new Location("Agrani Bank,Sector 6,Uttara", 23.872774, 90.401249));
            AllLocations.Add(new Location("Hotel De Meridien,Sector 6,Uttara", 23.872279, 90.401464));
            AllLocations.Add(new Location("Symphony Customer Care,Sector 6,Uttara", 23.871440, 90.401196));
            AllLocations.Add(new Location("Robi Customer Care,Sector 6,Uttara", 23.873373, 90.401153));
            AllLocations.Add(new Location("Allianse Francaise de Dhaka,Sector 6,Uttara", 23.871357, 90.402285));
            AllLocations.Add(new Location("American Super Specialty Hospital Ltd,Sector 6,Uttara", 23.870430, 90.403835));
            AllLocations.Add(new Location("Uttara University,Sector 6,Uttara", 23.870092, 90.402472));
            AllLocations.Add(new Location("Rajuk Uttara Model College,Sector 6,Uttara", 23.870033, 90.401957));
            AllLocations.Add(new Location("Uttara BGB Produce Market,Sector 6,Uttara", 23.870021, 90.401075));
            AllLocations.Add(new Location("KarmaSongstan Bank,Sector 6,Uttara", 23.869356, 90.401652));
            AllLocations.Add(new Location("CP Fried Chicken,Sector 6,Uttara", 23.868784, 90.401325));
            AllLocations.Add(new Location("Falguni Restaurant ,Sector 6,Uttara", 23.868625, 90.401218));
            AllLocations.Add(new Location("DPS STS School,Sector 6,Uttara", 23.868672, 90.402103));
            AllLocations.Add(new Location("Standard Chartered Bank,Sector 6,Uttara", 23.868363, 90.401797));
            AllLocations.Add(new Location("Persona,Sector 6,Uttara", 23.868289, 90.401577));
            AllLocations.Add(new Location("Ajompur Govt Primary School,Sector 6,Uttara", 23.868431, 90.401156));
            AllLocations.Add(new Location("Azampur Kacha Bazar,Sector 6,Uttara", 23.868026, 90.401338));
            AllLocations.Add(new Location("BTCL Compain Office,Sector 6,Uttara", 23.868131, 90.402561));
            AllLocations.Add(new Location("Rahimafroz LTD,Sector 6,Uttara", 23.868185, 90.403425));
            AllLocations.Add(new Location("Shwapno,Sector 6,Uttara", 23.868148, 90.404251));
            AllLocations.Add(new Location("Uttara Community Center,Sector 6,Uttara", 23.868764, 90.404887));
            AllLocations.Add(new Location("BNCC Headquarters,Sector 6,Uttara", 23.870780, 90.405600));


            AllLocations.Add(new Location("Azampur Post Office,Sector 4,Uttara", 23.867743, 90.401072));
            AllLocations.Add(new Location("Nawab Habibullah Model School and College,Sector 4,Uttara", 23.867615, 90.401684));
            AllLocations.Add(new Location("Uttara Police Station,Sector 4,Uttara", 23.866972, 90.400708));
            AllLocations.Add(new Location("Okapia Center,Sector 4,Uttara", 23.866354, 90.400928));
            AllLocations.Add(new Location("One Bank Ltd,Sector 4,Uttara", 23.865682, 90.400767));
            AllLocations.Add(new Location("Nest Restaurant and Bar,Sector 4,Uttara", 23.865177, 90.400885));
            AllLocations.Add(new Location("Al Arafah Islami Bank Ltd,Sector 4,Uttara", 23.864824, 90.400606));
            AllLocations.Add(new Location("Chef The City,Sector 4,Uttara", 23.864535, 90.401609));
            AllLocations.Add(new Location("SeaShell,Sector 4,Uttara", 23.863986, 90.400584));
            AllLocations.Add(new Location("Sonali Bank Ltd,Sector 4,Uttara", 23.863216, 90.400520));
            AllLocations.Add(new Location("Barrista Lavazza,Sector 4,Uttara", 23.862416, 90.400713));
            AllLocations.Add(new Location("Mutual Trust Bank Ltd,Sector 4,Uttara", 23.860444, 90.401121));
            AllLocations.Add(new Location("Grameenphone Customer Care,Sector 4,Uttara", 23.859639, 90.401609));
            AllLocations.Add(new Location("Californima Fired Chicken and Pastry,Sector 4,Uttara", 23.859060, 90.401888));
            AllLocations.Add(new Location("HSBC Bangladesh,Sector 4,Uttara", 23.858972, 90.402521));
            AllLocations.Add(new Location("Shumi’s Hot Cake,Sector 4,Uttara", 23.858771, 90.402221));
            AllLocations.Add(new Location("The Aga Khan School,Sector 4,Uttara", 23.860822, 90.402747));
            AllLocations.Add(new Location("Comfort Inn,Sector 4,Uttara", 23.863491, 90.405767));
            AllLocations.Add(new Location("Hotel Northern,Sector 4,Uttara", 23.866376, 90.404549));
            AllLocations.Add(new Location("Scholastica Middle Section,Sector 4,Uttara", 23.866881, 90.403106));
            AllLocations.Add(new Location("RAB-1,Sector 4,Uttara", 23.857628, 90.403585));
            AllLocations.Add(new Location("Airport Railway Station,Sector 4,Uttara", 23.852149, 90.408404));
            AllLocations.Add(new Location("Travel Dhaka Rent a Car,Sector 4,Uttara", 23.851344, 90.408576));
        }
        
    }
}
