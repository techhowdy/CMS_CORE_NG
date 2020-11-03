using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelService;
using Serilog;

namespace CountryService
{
    public class CountrySvc : ICountrySvc
    {

        public async Task<List<CountryModel>> GetCountriesAsync()
        {
            var countryModels = new List<CountryModel>();
            try
            {
                countryModels = await CreateListAsync();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return countryModels;
        }

        private Task<List<CountryModel>> CreateListAsync()
        {
            return Task.Run(() =>
            {
                var countryModels = new List<CountryModel>
                 {
                        new CountryModel
                        {
                            CountryId = 1,   Flag =  "🇦🇫", TwoDigitCode = "AF", Name = "Afghanistan", PhoneCode = "93",
                            StatesList = new[] {"Badakhshan", "Badghis", "Baghlan", "Balkh", "Bamian", "Daykondi", "Farah", "Faryab", "Ghazni", "Ghowr", "Helmand", "Herat", "Jowzjan", "Kabul", "Kandahar", "Kapisa", "Khost", "Konar", "Kondoz", "Laghman", "Lowgar", "Nangarhar", "Nimruz", "Nurestan", "Oruzgan", "Paktia", "Paktika", "Panjshir", "Parvan", "Samangan", "Sar-e Pol", "Takhar", "Vardak", "Zabol"},
                            States = ""
                        },
                        new CountryModel
                        {
                            CountryId = 2,   Flag =  "🇦🇱", TwoDigitCode = "AL", Name = "Albania", PhoneCode = "355",
                            StatesList = new[] {"Berat", "Dibres", "Durres", "Elbasan", "Fier", "Gjirokastre", "Korce", "Kukes", "Lezhe", "Shkoder", "Tirane", "Vlore"},
                            States = ""
                        },
                        new CountryModel
                        {
                            CountryId = 3,   Flag =  "🇩🇿", TwoDigitCode = "DZ", Name = "Algeria", PhoneCode = "213",
                            StatesList = new[] {"Adrar", "Ain Defla", "Ain Temouchent", "Alger", "Annaba", "Batna", "Bechar", "Bejaia", "Biskra", "Blida", "Bordj Bou Arreridj", "Bouira", "Boumerdes", "Chlef", "Constantine", "Djelfa", "El Bayadh", "El Oued", "El Tarf", "Ghardaia", "Guelma", "Illizi", "Jijel", "Khenchela", "Laghouat", "Muaskar", "Medea", "Mila", "Mostaganem", "M'Sila", "Naama", "Oran", "Ouargla", "Oum el Bouaghi", "Relizane", "Saida", "Setif", "Sidi Bel Abbes", "Skikda", "Souk Ahras", "Tamanghasset", "Tebessa", "Tiaret", "Tindouf", "Tipaza", "Tissemsilt", "Tizi Ouzou", "Tlemcen"},
                            States = ""
                        },
                        new CountryModel
                        {
                            CountryId = 4,   Flag =  "🇦🇸", TwoDigitCode = "AS", Name = "American Samoa", PhoneCode = "1684",
                            StatesList = new[] {""},
                            States = ""
                        },
                        new CountryModel
                        {
                            CountryId = 5,   Flag =  "🇦🇩", TwoDigitCode = "AD", Name = "Andorra", PhoneCode = "376",
                            StatesList = new[] {"Andorra la Vella", "Canillo", "Encamp", "Escaldes-Engordany", "La Massana", "Ordino", "Sant Julia de Loria"},
                            States = ""
                        },
                        new CountryModel
                        {
                            CountryId = 6,   Flag =  "🇦🇴", TwoDigitCode = "AO", Name = "Angola", PhoneCode = "244",
                            StatesList = new[] {"Bengo", "Benguela", "Bie", "Cabinda", "Cuando Cubango", "Cuanza Norte", "Cuanza Sul", "Cunene", "Huambo", "Huila", "Luanda", "Lunda Norte", "Lunda Sul", "Malanje", "Moxico", "Namibe", "Uige", "Zaire"},
                            States = ""
                        },
                        new CountryModel
                        {
                            CountryId = 7,   Flag =  "🇦🇮", TwoDigitCode = "AI", Name = "Anguilla", PhoneCode = "1264",
                            StatesList = new[] {""},
                            States = ""
                        },
                        new CountryModel
                        {
                            CountryId = 8,   Flag =  "🇦🇶", TwoDigitCode = "AQ", Name = "Antarctica", PhoneCode = "0",
                            StatesList = new[] {""},
                            States = ""
                        },
                        new CountryModel {
                        CountryId = 9,   Flag =  "🇦🇬", TwoDigitCode = "AG", Name = "Antigua And Barbuda", PhoneCode = "1268",
                        StatesList = new[] {"Barbuda", "Redonda", "Saint George", "Saint John", "Saint Mary", "Saint Paul", "Saint Peter", "Saint Philip"},
                        States = ""},
                        new CountryModel {
                        CountryId = 10,  Flag =  "🇦🇷", TwoDigitCode = "AR", Name = "Argentina", PhoneCode = "54",
                        StatesList = new[] {"Buenos Aires", "Buenos Aires Capital", "Catamarca", "Chaco", "Chubut", "Cordoba", "Corrientes", "Entre Rios", "Formosa", "Jujuy", "La Pampa", "La Rioja", "Mendoza", "Misiones", "Neuquen", "Rio Negro", "Salta", "San Juan", "San Luis", "Santa Cruz", "Santa Fe", "Santiago del Estero", "Tierra del Fuego", "Tucuman"},
                        States = ""},
                        new CountryModel {
                        CountryId = 11,  Flag =  "🇦🇲", TwoDigitCode = "AM", Name = "Armenia", PhoneCode = "374",
                        StatesList = new[] {"Aragatsotn", "Ararat", "Armavir", "Geghark'unik'", "Kotayk'", "Lorri", "Shirak", "Syunik'", "Tavush", "Vayots' Dzor", "Yerevan"},
                        States = ""},
                        new CountryModel {
                        CountryId = 12,  Flag =  "🇦🇼", TwoDigitCode = "AW", Name = "Aruba", PhoneCode = "297",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 13,  Flag =  "🇦🇺", TwoDigitCode = "AU", Name = "Australia", PhoneCode = "61",
                        StatesList = new[] {"Australian Capital Territory", "New South Wales", "Northern Territory", "Queensland", "South Australia", "Tasmania", "Victoria", "Western Australia"},
                        States = ""},
                        new CountryModel {
                        CountryId = 14,  Flag =  "🇦🇹", TwoDigitCode = "AT", Name = "Austria", PhoneCode = "43",
                        StatesList = new[] {"Burgenland", "Kaernten", "Niederoesterreich", "Oberoesterreich", "Salzburg", "Steiermark", "Tirol", "Vorarlberg", "Wien"},
                        States = ""},
                        new CountryModel {
                        CountryId = 15,  Flag =  "🇦🇿", TwoDigitCode = "AZ", Name = "Azerbaijan", PhoneCode = "994",
                        StatesList = new[] {"Abseron Rayonu", "Agcabadi Rayonu", "Agdam Rayonu", "Agdas Rayonu", "Agstafa Rayonu", "Agsu Rayonu", "Astara Rayonu", "Balakan Rayonu", "Barda Rayonu", "Beylaqan Rayonu", "Bilasuvar Rayonu", "Cabrayil Rayonu", "Calilabad Rayonu", "Daskasan Rayonu", "Davaci Rayonu", "Fuzuli Rayonu", "Gadabay Rayonu", "Goranboy Rayonu", "Goycay Rayonu", "Haciqabul Rayonu", "Imisli Rayonu", "Ismayilli Rayonu", "Kalbacar Rayonu", "Kurdamir Rayonu", "Lacin Rayonu", "Lankaran Rayonu", "Lerik Rayonu", "Masalli Rayonu", "Neftcala Rayonu", "Oguz Rayonu", "Qabala Rayonu", "Qax Rayonu", "Qazax Rayonu", "Qobustan Rayonu", "Quba Rayonu", "Qubadli Rayonu", "Qusar Rayonu", "Saatli Rayonu", "Sabirabad Rayonu", "Saki Rayonu", "Salyan Rayonu", "Samaxi Rayonu", "Samkir Rayonu", "Samux Rayonu", "Siyazan Rayonu", "Susa Rayonu", "Tartar Rayonu", "Tovuz Rayonu", "Ucar Rayonu", "Xacmaz Rayonu", "Xanlar Rayonu", "Xizi Rayonu", "Xocali Rayonu", "Xocavand Rayonu", "Yardimli Rayonu", "Yevlax Rayonu", "Zangilan Rayonu", "Zaqatala Rayonu", "Zardab Rayonu", "Ali Bayramli Sahari", "Baki Sahari", "Ganca Sahari", "Lankaran Sahari", "Mingacevir Sahari", "Naftalan Sahari", "Saki Sahari", "Sumqayit Sahari", "Susa Sahari", "Xankandi Sahari", "Yevlax Sahari", "Naxcivan Muxtar"},
                        States = ""},
                        new CountryModel {
                        CountryId = 16,  Flag =  "🇧🇸", TwoDigitCode = "BS", Name = "Bahamas", PhoneCode = "1242",
                        StatesList = new[] {"Acklins and Crooked Islands", "Bimini", "Cat Island", "Exuma", "Freeport", "Fresh Creek", "Governor's Harbour", "Green Turtle Cay", "Harbour Island", "High Rock", "Inagua", "Kemps Bay", "Long Island", "Marsh Harbour", "Mayaguana", "New Providence", "Nichollstown and Berry Islands", "Ragged Island", "Rock Sound", "Sandy Point", "San Salvador and Rum Cay"},
                        States = ""},
                        new CountryModel {
                        CountryId = 17,  Flag =  "🇧🇭", TwoDigitCode = "BH", Name = "Bahrain", PhoneCode = "973",
                        StatesList = new[] {"Al Hadd", "Al Manamah", "Al Mintaqah al Gharbiyah", "Al Mintaqah al Wusta", "Al Mintaqah ash Shamaliyah", "Al Muharraq", "Ar Rifa' wa al Mintaqah al Janubiyah", "Jidd Hafs", "Madinat Hamad", "Madinat 'Isa", "Juzur Hawar", "Sitrah"},
                        States = ""},
                        new CountryModel {
                        CountryId = 18,  Flag =  "🇧🇩", TwoDigitCode = "BD", Name = "Bangladesh", PhoneCode = "880",
                        StatesList = new[] {"Barisal", "Chittagong", "Dhaka", "Khulna", "Rajshahi", "Sylhet"},
                        States = ""},
                        new CountryModel {
                        CountryId = 19,  Flag =  "🇧🇧", TwoDigitCode = "BB", Name = "Barbados", PhoneCode = "1246",
                        StatesList = new[] {"Christ Church", "Saint Andrew", "Saint George", "Saint James", "Saint John", "Saint Joseph", "Saint Lucy", "Saint Michael", "Saint Peter", "Saint Philip", "Saint Thomas"},
                        States = ""},
                        new CountryModel {
                        CountryId = 20,  Flag =  "🇧🇾", TwoDigitCode = "BY", Name = "Belarus", PhoneCode = "375",
                        StatesList = new[] {"Brest", "Homyel", "Horad Minsk", "Hrodna", "Mahilyow", "Minsk", "Vitsyebsk"},
                        States = ""},
                        new CountryModel {
                        CountryId = 21,  Flag =  "🇧🇪", TwoDigitCode = "BE", Name = "Belgium", PhoneCode = "32",
                        StatesList = new[] {"Antwerpen", "Brabant Wallon", "Brussels", "Flanders", "Hainaut", "Liege", "Limburg", "Luxembourg", "Namur", "Oost-Vlaanderen", "Vlaams-Brabant", "Wallonia", "West-Vlaanderen"},
                        States = ""},
                        new CountryModel {
                        CountryId = 22,  Flag =  "🇧🇿", TwoDigitCode = "BZ", Name = "Belize", PhoneCode = "501",
                        StatesList = new[] {"Belize", "Cayo", "Corozal", "Orange Walk", "Stann Creek", "Toledo"},
                        States = ""},
                        new CountryModel {
                        CountryId = 23,  Flag =  "🇧🇯", TwoDigitCode = "BJ", Name = "Benin", PhoneCode = "229",
                        StatesList = new[] {"Alibori", "Atakora", "Atlantique", "Borgou", "Collines", "Donga", "Kouffo", "Littoral", "Mono", "Oueme", "Plateau", "Zou"},
                        States = ""},
                        new CountryModel {
                        CountryId = 24,  Flag =  "🇧🇲", TwoDigitCode = "BM", Name = "Bermuda", PhoneCode = "1441",
                        StatesList = new[] {"Devonshire", "Hamilton", "Hamilton", "Paget", "Pembroke", "Saint George", "Saint George's", "Sandys", "Smith's", "Southampton", "Warwick"},
                        States = ""},
                        new CountryModel {
                        CountryId = 25,  Flag =  "🇧🇹", TwoDigitCode = "BT", Name = "Bhutan", PhoneCode = "975",
                        StatesList = new[] {"Bumthang", "Chukha", "Dagana", "Gasa", "Haa", "Lhuntse", "Mongar", "Paro", "Pemagatshel", "Punakha", "Samdrup Jongkhar", "Samtse", "Sarpang", "Thimphu", "Trashigang", "Trashiyangste", "Trongsa", "Tsirang", "Wangdue Phodrang", "Zhemgang"},
                        States = ""},
                        new CountryModel {
                        CountryId = 26,  Flag =  "🇧🇴", TwoDigitCode = "BO", Name = "Bolivia", PhoneCode = "591",
                        StatesList = new[] {"Chuquisaca", "Cochabamba", "Beni", "La Paz", "Oruro", "Pando", "Potosi", "Santa Cruz", "Tarija"},
                        States = ""},
                        new CountryModel {
                        CountryId = 27,  Flag =  "🇧🇦", TwoDigitCode = "BA", Name = "Bosnia and Herzegovina", PhoneCode = "387",
                        StatesList = new[] {"Una-Sana [Federation]", "Posavina [Federation]", "Tuzla [Federation]", "Zenica-Doboj [Federation]", "Bosnian Podrinje [Federation]", "Central Bosnia [Federation]", "Herzegovina-Neretva [Federation]", "West Herzegovina [Federation]", "Sarajevo [Federation]", " West Bosnia [Federation]", "Banja Luka [RS]", "Bijeljina [RS]", "Doboj [RS]", "Fo?a [RS]", "Sarajevo-Romanija [RS]", "Trebinje [RS]", "Vlasenica [RS]"},
                        States = ""},
                        new CountryModel {
                        CountryId = 28,  Flag =  "🇧🇼", TwoDigitCode = "BW", Name = "Botswana", PhoneCode = "267",
                        StatesList = new[] {"Central", "Ghanzi", "Kgalagadi", "Kgatleng", "Kweneng", "North East", "North West", "South East", "Southern"},
                        States = ""},
                        new CountryModel {
                        CountryId = 29,  Flag =  "🇧🇻", TwoDigitCode = "BV", Name = "Bouvet Island", PhoneCode = "0",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 30,  Flag =  "🇧🇷", TwoDigitCode = "BR", Name = "Brazil", PhoneCode = "55",
                        StatesList = new[] {"Acre", "Alagoas", "Amapa", "Amazonas", "Bahia", "Ceara", "Distrito Federal", "Espirito Santo", "Goias", "Maranhao", "Mato Grosso", "Mato Grosso do Sul", "Minas Gerais", "Para", "Paraiba", "Parana", "Pernambuco", "Piaui", "Rio de Janeiro", "Rio Grande do Norte", "Rio Grande do Sul", "Rondonia", "Roraima", "Santa Catarina", "Sao Paulo", "Sergipe", "Tocantins"},
                        States = ""},
                        new CountryModel {
                        CountryId = 31,  Flag =  "🇮🇴", TwoDigitCode = "IO", Name = "British Indian Ocean Territory", PhoneCode = "246",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 32,  Flag =  "🇧🇳", TwoDigitCode = "BN", Name = "Brunei", PhoneCode = "673",
                        StatesList = new[] {"Belait", "Brunei and Muara", "Temburong", "Tutong"},
                        States = ""},
                        new CountryModel {
                        CountryId = 33,  Flag =  "🇧🇬", TwoDigitCode = "BG", Name = "Bulgaria", PhoneCode = "359",
                        StatesList = new[] {"Blagoevgrad", "Burgas", "Dobrich", "Gabrovo", "Khaskovo", "Kurdzhali", "Kyustendil", "Lovech", "Montana", "Pazardzhik", "Pernik", "Pleven", "Plovdiv", "Razgrad", "Ruse", "Shumen", "Silistra", "Sliven", "Smolyan", "Sofiya", "Sofiya-Grad", "Stara Zagora", "Turgovishte", "Varna", "Veliko Turnovo", "Vidin", "Vratsa", "Yambol"},
                        States = ""},
                        new CountryModel {
                        CountryId = 34,  Flag =  "🇧🇫", TwoDigitCode = "BF", Name = "Burkina Faso", PhoneCode = "226",
                        StatesList = new[] {"Bale", "Bam", "Banwa", "Bazega", "Bougouriba", "Boulgou", "Boulkiemde", "Comoe", "Ganzourgou", "Gnagna", "Gourma", "Houet", "Ioba", "Kadiogo", "Kenedougou", "Komondjari", "Kompienga", "Kossi", "Koulpelogo", "Kouritenga", "Kourweogo", "Leraba", "Loroum", "Mouhoun", "Namentenga", "Nahouri", "Nayala", "Noumbiel", "Oubritenga", "Oudalan", "Passore", "Poni", "Sanguie", "Sanmatenga", "Seno", "Sissili", "Soum", "Sourou", "Tapoa", "Tuy", "Yagha", "Yatenga", "Ziro", "Zondoma", "Zoundweogo"},
                        States = ""},
                        new CountryModel {
                        CountryId = 35,  Flag =  "🇧🇮", TwoDigitCode = "BI", Name = "Burundi", PhoneCode = "257",
                        StatesList = new[] {"Bubanza", "Bujumbura Mairie", "Bujumbura Rural", "Bururi", "Cankuzo", "Cibitoke", "Gitega", "Karuzi", "Kayanza", "Kirundo", "Makamba", "Muramvya", "Muyinga", "Mwaro", "Ngozi", "Rutana", "Ruyigi"},
                        States = ""},
                        new CountryModel {
                        CountryId = 36,  Flag =  "🇰🇭", TwoDigitCode = "KH", Name = "Cambodia", PhoneCode = "855",
                        StatesList = new[] {"Banteay Mean Chey", "Batdambang", "Kampong Cham", "Kampong Chhnang", "Kampong Spoe", "Kampong Thum", "Kampot", "Kandal", "Koh Kong", "Kracheh", "Mondol Kiri", "Otdar Mean Chey", "Pouthisat", "Preah Vihear", "Prey Veng", "Rotanakir", "Siem Reab", "Stoeng Treng", "Svay Rieng", "Takao", "Keb", "Pailin", "Phnom Penh", "Preah Seihanu"},
                        States = ""},
                        new CountryModel {
                        CountryId = 37,  Flag =  "🇨🇲", TwoDigitCode = "CM", Name = "Cameroon", PhoneCode = "237",
                        StatesList = new[] {"Adamaoua", "Centre", "Est", "Extreme-Nord", "Littoral", "Nord", "Nord-Ouest", "Ouest", "Sud", "Sud-Ouest"},
                        States = ""},
                        new CountryModel {
                        CountryId = 38,  Flag =  "🇨🇦", TwoDigitCode = "CA", Name = "Canada", PhoneCode = "1",
                        StatesList = new[] {"Alberta", "British Columbia", "Manitoba", "New Brunswick", "Newfoundland and Labrador", "Northwest Territories", "Nova Scotia", "Nunavut", "Ontario", "Prince Edward Island", "Quebec", "Saskatchewan", "Yukon Territory"},
                        States = ""},
                        new CountryModel {
                        CountryId = 39,  Flag =  "🇨🇻", TwoDigitCode = "CV", Name = "Cape Verde", PhoneCode = "238",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 40,  Flag =  "🇰🇾", TwoDigitCode = "KY", Name = "Cayman Islands", PhoneCode = "1345",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 41,  Flag =  "🇨🇫", TwoDigitCode = "CF", Name = "Central African Republic", PhoneCode = "236",
                        StatesList = new[] {"Bamingui-Bangoran", "Bangui", "Basse-Kotto", "Haute-Kotto", "Haut-Mbomou", "Kemo", "Lobaye", "Mambere-Kadei", "Mbomou", "Nana-Grebizi", "Nana-Mambere", "Ombella-Mpoko", "Ouaka", "Ouham", "Ouham-Pende", "Sangha-Mbaere", "Vakaga"},
                        States = ""},
                        new CountryModel {
                        CountryId = 42,  Flag =  "🇹🇩", TwoDigitCode = "TD", Name = "Chad", PhoneCode = "235",
                        StatesList = new[] {"Batha", "Biltine", "Borkou-Ennedi-Tibesti", "Chari-Baguirmi", "Guéra", "Kanem", "Lac", "Logone Occidental", "Logone Oriental", "Mayo-Kebbi", "Moyen-Chari", "Ouaddaï", "Salamat", "Tandjile"},
                        States = ""},
                        new CountryModel {
                        CountryId = 43,  Flag =  "🇨🇱", TwoDigitCode = "CL", Name = "Chile", PhoneCode = "56",
                        StatesList = new[] {"Aysen", "Antofagasta", "Araucania", "Atacama", "Bio-Bio", "Coquimbo", "O'Higgins", "Los Lagos", "Magallanes y la Antartica Chilena", "Maule", "Santiago Region Metropolitana", "Tarapaca", "Valparaiso"},
                        States = ""},
                        new CountryModel {
                        CountryId = 44,  Flag =  "🇨🇳", TwoDigitCode = "CN", Name = "China", PhoneCode = "86",
                        StatesList = new[] {"Anhui", "Fujian", "Gansu", "Guangdong", "Guizhou", "Hainan", "Hebei", "Heilongjiang", "Henan", "Hubei", "Hunan", "Jiangsu", "Jiangxi", "Jilin", "Liaoning", "Qinghai", "Shaanxi", "Shandong", "Shanxi", "Sichuan", "Yunnan", "Zhejiang", "Guangxi", "Nei Mongol", "Ningxia", "Xinjiang", "Xizang (Tibet)", "Beijing", "Chongqing", "Shanghai", "Tianjin"},
                        States = ""},
                        new CountryModel {
                        CountryId = 45,  Flag =  "🇨🇽", TwoDigitCode = "CX", Name = "Christmas Island", PhoneCode = "61",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 46,  Flag =  "🇨🇨", TwoDigitCode = "CC", Name = "Cocos {Keeling} Islands", PhoneCode = "672",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 47,  Flag =  "🇨🇴", TwoDigitCode = "CO", Name = "Colombia", PhoneCode = "57",
                        StatesList = new[] {"Amazonas", "Antioquia", "Arauca", "Atlantico", "Bogota District Capital", "Bolivar", "Boyaca", "Caldas", "Caqueta", "Casanare", "Cauca", "Cesar", "Choco", "Cordoba", "Cundinamarca", "Guainia", "Guaviare", "Huila", "La Guajira", "Magdalena", "Meta", "Narino", "Norte de Santander", "Putumayo", "Quindio", "Risaralda", "San Andres & Providencia", "Santander", "Sucre", "Tolima", "Valle del Cauca", "Vaupes", "Vichada"},
                        States = ""},
                        new CountryModel {
                        CountryId = 48,  Flag =  "🇰🇲", TwoDigitCode = "KM", Name = "Comoros", PhoneCode = "269",
                        StatesList = new[] {"Grande Comore (Njazidja)", "Anjouan (Nzwani)", "Moheli (Mwali)"},
                        States = ""},
                        new CountryModel {
                        CountryId = 49,  Flag =  "🇨🇬", TwoDigitCode = "CG", Name = "Congo", PhoneCode = "242",
                        StatesList = new[] {"Bouenza", "Brazzaville", "Cuvette", "Cuvette-Ouest", "Kouilou", "Lekoumou", "Likouala", "Niari", "Plateaux", "Pool", "Sangha"},
                        States = ""},
                        new CountryModel {
                        CountryId = 50,  Flag =  "🇨🇩", TwoDigitCode = "CD", Name = "Congo The Democratic Republic Of The", PhoneCode = "242",
                        StatesList = new[] {"Bandundu", "Bas-Congo", "Equateur", "Kasai-Occidental", "Kasai-Oriental", "Katanga", "Kinshasa", "Maniema", "Nord-Kivu", "Orientale", "Sud-Kivu"},
                        States = ""},
                        new CountryModel {
                        CountryId = 51,  Flag =  "🇨🇰", TwoDigitCode = "CK", Name = "Cook Islands", PhoneCode = "682",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 52,  Flag =  "🇨🇷", TwoDigitCode = "CR", Name = "Costa Rica", PhoneCode = "506",
                        StatesList = new[] {"Alajuela", "Cartago", "Guanacaste", "Heredia", "Limon", "Puntarenas", "San Jose"},
                        States = ""},
                        new CountryModel {
                        CountryId = 53,  Flag =  "🇨🇮", TwoDigitCode = "CI", Name = "Cote D''Ivoire {Ivory Coast}", PhoneCode = "225",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 54,  Flag =  "🇭🇷", TwoDigitCode = "HR", Name = "Croatia {Hrvatska}", PhoneCode = "385",
                        StatesList = new[] {"Bjelovarsko-Bilogorska", "Brodsko-Posavska", "Dubrovacko-Neretvanska", "Istarska", "Karlovacka", "Koprivnicko-Krizevacka", "Krapinsko-Zagorska", "Licko-Senjska", "Medimurska", "Osjecko-Baranjska", "Pozesko-Slavonska", "Primorsko-Goranska", "Sibensko-Kninska", "Sisacko-Moslavacka", "Splitsko-Dalmatinska", "Varazdinska", "Viroviticko-Podravska", "Vukovarsko-Srijemska", "Zadarska", "Zagreb", "Zagrebacka"},
                        States = ""},
                        new CountryModel {
                        CountryId = 55,  Flag =  "🇨🇺", TwoDigitCode = "CU", Name = "Cuba", PhoneCode = "53",
                        StatesList = new[] {"Camaguey", "Ciego de Avila", "Cienfuegos", "Ciudad de La Habana", "Granma", "Guantanamo", "Holguin", "Isla de la Juventud", "La Habana", "Las Tunas", "Matanzas", "Pinar del Rio", "Sancti Spiritus", "Santiago de Cuba", "Villa Clara"},
                        States = ""},
                        new CountryModel {
                        CountryId = 56,  Flag =  "🇨🇾", TwoDigitCode = "CY", Name = "Cyprus", PhoneCode = "357",
                        StatesList = new[] {"Famagusta", "Kyrenia", "Larnaca", "Limassol", "Nicosia", "Paphos"},
                        States = ""},
                        new CountryModel {
                        CountryId = 57,  Flag =  "🇨🇿", TwoDigitCode = "CZ", Name = "Czech Republic", PhoneCode = "420",
                        StatesList = new[] {"Jihocesky Kraj", "Jihomoravsky Kraj", "Karlovarsky Kraj", "Kralovehradecky Kraj", "Liberecky Kraj", "Moravskoslezsky Kraj", "Olomoucky Kraj", "Pardubicky Kraj", "Plzensky Kraj", "Praha", "Stredocesky Kraj", "Ustecky Kraj", "Vysocina", "Zlinsky Kraj"},
                        States = ""},
                        new CountryModel {
                        CountryId = 58,  Flag =  "🇩🇰", TwoDigitCode = "DK", Name = "Denmark", PhoneCode = "45",
                        StatesList = new[] {"Arhus", "Bornholm", "Frederiksberg", "Frederiksborg", "Fyn", "Kobenhavn", "Kobenhavns", "Nordjylland", "Ribe", "Ringkobing", "Roskilde", "Sonderjylland", "Storstrom", "Vejle", "Vestsjalland", "Viborg"},
                        States = ""},
                        new CountryModel {
                        CountryId = 59,  Flag =  "🇩🇯", TwoDigitCode = "DJ", Name = "Djibouti", PhoneCode = "253",
                        StatesList = new[] {"Ali Sabih", "Dikhil", "Djibouti", "Obock", "Tadjoura"},
                        States = ""},
                        new CountryModel {
                        CountryId = 60,  Flag =  "🇩🇲", TwoDigitCode = "DM", Name = "Dominica", PhoneCode = "1767",
                        StatesList = new[] {"Saint Andrew", "Saint David", "Saint George", "Saint John", "Saint Joseph", "Saint Luke", "Saint Mark", "Saint Patrick", "Saint Paul", "Saint Peter"},
                        States = ""},
                        new CountryModel {
                        CountryId = 61,  Flag =  "🇩🇴", TwoDigitCode = "DO", Name = "Dominican Republic", PhoneCode = "1809",
                        StatesList = new[] {"Azua", "Baoruco", "Barahona", "Dajabon", "Distrito Nacional", "Duarte", "Elias Pina", "El Seibo", "Espaillat", "Hato Mayor", "Independencia", "La Altagracia", "La Romana", "La Vega", "Maria Trinidad Sanchez", "Monsenor Nouel", "Monte Cristi", "Monte Plata", "Pedernales", "Peravia", "Puerto Plata", "Salcedo", "Samana", "Sanchez Ramirez", "San Cristobal", "San Jose de Ocoa", "San Juan", "San Pedro de Macoris", "Santiago", "Santiago Rodriguez", "Santo Domingo", "Valverde"},
                        States = ""},
                        new CountryModel {
                        CountryId = 62,  Flag =  "🇹🇱",   TwoDigitCode = "TP", Name = "East Timor", PhoneCode = "670",
                        StatesList = new[] {"Aileu", "Ainaro", "Baucau", "Bobonaro", "Cova-Lima", "Dili", "Ermera", "Lautem", "Liquica", "Manatuto", "Manufahi", "Oecussi", "Viqueque"},
                        States = ""},
                        new CountryModel {
                        CountryId = 63,  Flag =  "🇪🇨", TwoDigitCode = "EC", Name = "Ecuador", PhoneCode = "593",
                        StatesList = new[] {"Azuay", "Bolivar", "Canar", "Carchi", "Chimborazo", "Cotopaxi", "El Oro", "Esmeraldas", "Galapagos", "Guayas", "Imbabura", "Loja", "Los Rios", "Manabi", "Morona-Santiago", "Napo", "Orellana", "Pastaza", "Pichincha", "Sucumbios", "Tungurahua", "Zamora-Chinchipe"},
                        States = ""},
                        new CountryModel {
                        CountryId = 64,  Flag =  "🇪🇬", TwoDigitCode = "EG", Name = "Egypt", PhoneCode = "20",
                        StatesList = new[] {"Ad Daqahliyah", "Al Bahr al Ahmar", "Al Buhayrah", "Al Fayyum", "Al Gharbiyah", "Al Iskandariyah", "Al Isma'iliyah", "Al Jizah", "Al Minufiyah", "Al Minya", "Al Qahirah", "Al Qalyubiyah", "Al Wadi al Jadid", "Ash Sharqiyah", "As Suways", "Aswan", "Asyut", "Bani Suwayf", "Bur Sa'id", "Dumyat", "Janub Sina'", "Kafr ash Shaykh", "Matruh", "Qina", "Shamal Sina'", "Suhaj"},
                        States = ""},
                        new CountryModel {
                        CountryId = 65,  Flag =  "🇸🇻", TwoDigitCode = "SV", Name = "El Salvador", PhoneCode = "503",
                        StatesList = new[] {"Ahuachapan", "Cabanas", "Chalatenango", "Cuscatlan", "La Libertad", "La Paz", "La Union", "Morazan", "San Miguel", "San Salvador", "Santa Ana", "San Vicente", "Sonsonate", "Usulutan"},
                        States = ""},
                        new CountryModel {
                        CountryId = 66,  Flag =  "🇬🇶", TwoDigitCode = "GQ", Name = "Equatorial Guinea", PhoneCode = "240",
                        StatesList = new[] {"Annobon", "Bioko Norte", "Bioko Sur", "Centro Sur", "Kie-Ntem", "Litoral", "Wele-Nzas"},
                        States = ""},
                        new CountryModel {
                        CountryId = 67,  Flag =  "🇪🇷", TwoDigitCode = "ER", Name = "Eritrea", PhoneCode = "291",
                        StatesList = new[] {"Anseba", "Debub", "Debubawi K'eyih Bahri", "Gash Barka", "Ma'akel", "Semenawi Keyih Bahri"},
                        States = ""},
                        new CountryModel {
                        CountryId = 68,  Flag =  "🇪🇪", TwoDigitCode = "EE", Name = "Estonia", PhoneCode = "372",
                        StatesList = new[] {"Harjumaa (Tallinn)", "Hiiumaa (Kardla)", "Ida-Virumaa (Johvi)", "Jarvamaa (Paide)", "Jogevamaa (Jogeva)", "Laanemaa (Haapsalu)", "Laane-Virumaa (Rakvere)", "Parnumaa (Parnu)", "Polvamaa (Polva)", "Raplamaa (Rapla)", "Saaremaa (Kuressaare)", "Tartumaa (Tartu)", "Valgamaa (Valga)", "Viljandimaa (Viljandi)", "Vorumaa (Voru)"},
                        States = ""},
                        new CountryModel {
                        CountryId = 69,  Flag =  "🇪🇹", TwoDigitCode = "ET", Name = "Ethiopia", PhoneCode = "251",
                        StatesList = new[] {"Addis Ababa", "Afar", "Amhara", "Binshangul Gumuz", "Dire Dawa", "Gambela Hizboch", "Harari", "Oromia", "Somali", "Tigray", "Southern Nations, Nationalities, and Peoples Region"},
                        States = ""},
                        new CountryModel {
                        CountryId = 70,  Flag =  "🇦🇺",   TwoDigitCode = "XA", Name = "External Territories of Australia", PhoneCode = "61",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 71,  Flag =  "🇫🇰", TwoDigitCode = "FK", Name = "Falkland Islands", PhoneCode = "500",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 72,  Flag =  "🇫🇴", TwoDigitCode = "FO", Name = "Faroe Islands", PhoneCode = "298",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 73,  Flag =  "🇫🇯", TwoDigitCode = "FJ", Name = "Fiji Islands", PhoneCode = "679",
                        StatesList = new[] {"Central (Suva)", "Eastern (Levuka)", "Northern (Labasa)", "Rotuma", "Western (Lautoka)"},
                        States = ""},
                        new CountryModel {
                        CountryId = 74,  Flag =  "🇫🇮", TwoDigitCode = "FI", Name = "Finland", PhoneCode = "358",
                        StatesList = new[] {"Aland", "Etela-Suomen Laani", "Ita-Suomen Laani", "Lansi-Suomen Laani", "Lappi", "Oulun Laani"},
                        States = ""},
                        new CountryModel {
                        CountryId = 75,  Flag =  "🇫🇷", TwoDigitCode = "FR", Name = "France", PhoneCode = "33",
                        StatesList = new[] {"Alsace", "Aquitaine", "Auvergne", "Basse-Normandie", "Bourgogne", "Bretagne", "Centre", "Champagne-Ardenne", "Corse", "Franche-Comte", "Haute-Normandie", "Ile-de-France", "Languedoc-Roussillon", "Limousin", "Lorraine", "Midi-Pyrenees", "Nord-Pas-de-Calais", "Pays de la Loire", "Picardie", "Poitou-Charentes", "Provence-Alpes-Cote d'Azur", "Rhone-Alpes"},
                        States = ""},
                        new CountryModel {
                        CountryId = 76,  Flag =  "🇬🇫", TwoDigitCode = "GF", Name = "French Guiana", PhoneCode = "594",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 77,  Flag =  "🇵🇫", TwoDigitCode = "PF", Name = "French Polynesia", PhoneCode = "689",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 78,  Flag =  "🇹🇫", TwoDigitCode = "TF", Name = "French Southern Territories", PhoneCode = "0",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 79,  Flag =  "🇬🇦", TwoDigitCode = "GA", Name = "Gabon", PhoneCode = "241",
                        StatesList = new[] {"Estuaire", "Haut-Ogooue", "Moyen-Ogooue", "Ngounie", "Nyanga", "Ogooue-Ivindo", "Ogooue-Lolo", "Ogooue-Maritime", "Woleu-Ntem"},
                        States = ""},
                        new CountryModel {
                        CountryId = 80,  Flag =  "🇬🇲", TwoDigitCode = "GM", Name = "Gambia", PhoneCode = "220",
                        StatesList = new[] {"Banjul", "Central River", "Lower River", "North Bank", "Upper River", "Western"},
                        States = ""},
                        new CountryModel {
                        CountryId = 81,  Flag =  "🇬🇪", TwoDigitCode = "GE", Name = "Georgia", PhoneCode = "995",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 82,  Flag =  "🇩🇪", TwoDigitCode = "DE", Name = "Germany", PhoneCode = "49",
                        StatesList = new[] {"Baden-Wuerttemberg", "Bayern", "Berlin", "Brandenburg", "Bremen", "Hamburg", "Hessen", "Mecklenburg-Vorpommern", "Niedersachsen", "Nordrhein-Westfalen", "Rheinland-Pfalz", "Saarland", "Sachsen", "Sachsen-Anhalt", "Schleswig-Holstein", "Thueringen"},
                        States = ""},
                        new CountryModel {
                        CountryId = 83,  Flag =  "🇬🇭", TwoDigitCode = "GH", Name = "Ghana", PhoneCode = "233",
                        StatesList = new[] {"Ashanti", "Brong-Ahafo", "Central", "Eastern", "Greater Accra", "Northern", "Upper East", "Upper West", "Volta", "Western"},
                        States = ""},
                        new CountryModel {
                        CountryId = 84,  Flag =  "🇬🇮", TwoDigitCode = "GI", Name = "Gibraltar", PhoneCode = "350",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 85,  Flag =  "🇬🇷", TwoDigitCode = "GR", Name = "Greece", PhoneCode = "30",
                        StatesList = new[] {"Agion Oros", "Achaia", "Aitolia kai Akarmania", "Argolis", "Arkadia", "Arta", "Attiki", "Chalkidiki", "Chanion", "Chios", "Dodekanisos", "Drama", "Evros", "Evrytania", "Evvoia", "Florina", "Fokidos", "Fthiotis", "Grevena", "Ileia", "Imathia", "Ioannina", "Irakleion", "Karditsa", "Kastoria", "Kavala", "Kefallinia", "Kerkyra", "Kilkis", "Korinthia", "Kozani", "Kyklades", "Lakonia", "Larisa", "Lasithi", "Lefkas", "Lesvos", "Magnisia", "Messinia", "Pella", "Pieria", "Preveza", "Rethynnis", "Rodopi", "Samos", "Serrai", "Thesprotia", "Thessaloniki", "Trikala", "Voiotia", "Xanthi", "Zakynthos"},
                        States = ""},
                        new CountryModel {
                        CountryId = 86,  Flag =  "🇬🇱", TwoDigitCode = "GL", Name = "Greenland", PhoneCode = "299",
                        StatesList = new[] {"Avannaa (Nordgronland)", "Tunu (Ostgronland)", "Kitaa (Vestgronland)"},
                        States = ""},
                        new CountryModel {
                        CountryId = 87,  Flag =  "🇬🇩", TwoDigitCode = "GD", Name = "Grenada", PhoneCode = "1473",
                        StatesList = new[] {"Carriacou and Petit Martinique", "Saint Andrew", "Saint David", "Saint George", "Saint John", "Saint Mark", "Saint Patrick"},
                        States = ""},
                        new CountryModel {
                        CountryId = 88,  Flag =  "🇬🇵", TwoDigitCode = "GP", Name = "Guadeloupe", PhoneCode = "590",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 89,  Flag =  "🇬🇺", TwoDigitCode = "GU", Name = "Guam", PhoneCode = "1671",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 90,  Flag =  "🇬🇹", TwoDigitCode = "GT", Name = "Guatemala", PhoneCode = "502",
                        StatesList = new[] {"Alta Verapaz", "Baja Verapaz", "Chimaltenango", "Chiquimula", "El Progreso", "Escuintla", "Guatemala", "Huehuetenango", "Izabal", "Jalapa", "Jutiapa", "Peten", "Quetzaltenango", "Quiche", "Retalhuleu", "Sacatepequez", "San Marcos", "Santa Rosa", "Solola", "Suchitepequez", "Totonicapan", "Zacapa"},
                        States = ""},
                        new CountryModel {
                        CountryId = 91,  Flag =  "🇬🇬", TwoDigitCode = "XU", Name = "Guernsey and Alderney", PhoneCode = "44",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 92,  Flag =  "🇬🇳", TwoDigitCode = "GN", Name = "Guinea", PhoneCode = "224",
                        StatesList = new[] {"Beyla", "Boffa", "Boke", "Conakry", "Coyah", "Dabola", "Dalaba", "Dinguiraye", "Dubreka", "Faranah", "Forecariah", "Fria", "Gaoual", "Gueckedou", "Kankan", "Kerouane", "Kindia", "Kissidougou", "Koubia", "Koundara", "Kouroussa", "Labe", "Lelouma", "Lola", "Macenta", "Mali", "Mamou", "Mandiana", "Nzerekore", "Pita", "Siguiri", "Telimele", "Tougue", "Yomou"},
                        States = ""},
                        new CountryModel {
                        CountryId = 93,  Flag =  "🇬🇼", TwoDigitCode = "GW", Name = "Guinea-Bissau", PhoneCode = "245",
                        StatesList = new[] {"Bafata", "Biombo", "Bissau", "Bolama", "Cacheu", "Gabu", "Oio", "Quinara", "Tombali"},
                        States = ""},
                        new CountryModel {
                        CountryId = 94,  Flag =  "🇬🇾", TwoDigitCode = "GY", Name = "Guyana", PhoneCode = "592",
                        StatesList = new[] {"Barima-Waini", "Cuyuni-Mazaruni", "Demerara-Mahaica", "East Berbice-Corentyne", "Essequibo Islands-West Demerara", "Mahaica-Berbice", "Pomeroon-Supenaam", "Potaro-Siparuni", "Upper Demerara-Berbice", "Upper Takutu-Upper Essequibo"},
                        States = ""},
                        new CountryModel {
                        CountryId = 95,  Flag =  "🇭🇹", TwoDigitCode = "HT", Name = "Haiti", PhoneCode = "509",
                        StatesList = new[] {"Artibonite", "Centre", "Grand 'Anse", "Nord", "Nord-Est", "Nord-Ouest", "Ouest", "Sud", "Sud-Est"},
                        States = ""},
                        new CountryModel {
                        CountryId = 96,  Flag =  "🇭🇲", TwoDigitCode = "HM", Name = "Heard and McDonald Islands", PhoneCode = "0",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 97,  Flag =  "🇭🇳", TwoDigitCode = "HN", Name = "Honduras", PhoneCode = "504",
                        StatesList = new[] {"Atlantida", "Choluteca", "Colon", "Comayagua", "Copan", "Cortes", "El Paraiso", "Francisco Morazan", "Gracias a Dios", "Intibuca", "Islas de la Bahia", "La Paz", "Lempira", "Ocotepeque", "Olancho", "Santa Barbara", "Valle", "Yoro"},
                        States = ""},
                        new CountryModel {
                        CountryId = 98,  Flag =  "🇭🇰", TwoDigitCode = "HK", Name = "Hong Kong", PhoneCode = "852",
                        StatesList = new[] {"Yuen Long District","Tsuen Wan District","Tai Po District","Sai Kung District","Islands District","Central and Western District","Wan Chai","Eastern","Southern","North","Yau Tsim Mong","Sham Shui Po","Kowloon City","Wong Tai Sin","Kwun Tong","Kwai Tsing","Tuen Mun","Sha Tin"},
                        States = ""},
                        new CountryModel {
                        CountryId = 99,  Flag =  "🇭🇺", TwoDigitCode = "HU", Name = "Hungary", PhoneCode = "36",
                        StatesList = new[] {"Bacs-Kiskun", "Baranya", "Bekes", "Borsod-Abauj-Zemplen", "Csongrad", "Fejer", "Gyor-Moson-Sopron", "Hajdu-Bihar", "Heves", "Jasz-Nagykun-Szolnok", "Komarom-Esztergom", "Nograd", "Pest", "Somogy", "Szabolcs-Szatmar-Bereg", "Tolna", "Vas", "Veszprem", "Zala", "Bekescsaba", "Debrecen", "Dunaujvaros", "Eger", "Gyor", "Hodmezovasarhely", "Kaposvar", "Kecskemet", "Miskolc", "Nagykanizsa", "Nyiregyhaza", "Pecs", "Sopron", "Szeged", "Szekesfehervar", "Szolnok", "Szombathely", "Tatabanya", "Veszprem", "Zalaegerszeg"},
                        States = ""},
                        new CountryModel {
                        CountryId = 100, Flag =  "🇮🇸", TwoDigitCode = "IS", Name = "Iceland", PhoneCode = "354",
                        StatesList = new[] {"Austurland", "Hofudhborgarsvaedhi", "Nordhurland Eystra", "Nordhurland Vestra", "Sudhurland", "Sudhurnes", "Vestfirdhir", "Vesturland"},
                        States = ""},
                        new CountryModel {
                        CountryId = 101, Flag =  "🇮🇳", TwoDigitCode = "IN", Name = "India", PhoneCode = "91",
                        StatesList = new[] {"Andaman and Nicobar Islands", "Andhra Pradesh", "Arunachal Pradesh", "Assam", "Bihar", "Chandigarh", "Chhattisgarh", "Dadra and Nagar Haveli", "Daman and Diu", "Delhi", "Goa", "Gujarat", "Haryana", "Himachal Pradesh", "Jammu and Kashmir", "Jharkhand", "Karnataka", "Kerala", "Lakshadweep", "Madhya Pradesh", "Maharashtra", "Manipur", "Meghalaya", "Mizoram", "Nagaland", "Orissa", "Pondicherry", "Punjab", "Rajasthan", "Sikkim", "Tamil Nadu", "Tripura", "Telangana", "Uttaranchal", "Uttar Pradesh", "West Bengal"},
                        States = ""},
                        new CountryModel {
                        CountryId = 102, Flag =  "🇮🇩", TwoDigitCode = "ID", Name = "Indonesia", PhoneCode = "62",
                        StatesList = new[] {"Aceh", "Bali", "Banten", "Bengkulu", "Gorontalo", "Irian Jaya Barat", "Jakarta Raya", "Jambi", "Jawa Barat", "Jawa Tengah", "Jawa Timur", "Kalimantan Barat", "Kalimantan Selatan", "Kalimantan Tengah", "Kalimantan Timur", "Kepulauan Bangka Belitung", "Kepulauan Riau", "Lampung", "Maluku", "Maluku Utara", "Nusa Tenggara Barat", "Nusa Tenggara Timur", "Papua", "Riau", "Sulawesi Barat", "Sulawesi Selatan", "Sulawesi Tengah", "Sulawesi Tenggara", "Sulawesi Utara", "Sumatera Barat", "Sumatera Selatan", "Sumatera Utara", "Yogyakarta"},
                        States = ""},
                        new CountryModel {
                        CountryId = 103, Flag =  "🇮🇷", TwoDigitCode = "IR", Name = "Iran", PhoneCode = "98",
                        StatesList = new[] {"Ardabil", "Azarbayjan-e Gharbi", "Azarbayjan-e Sharqi", "Bushehr", "Chahar Mahall va Bakhtiari", "Esfahan", "Fars", "Gilan", "Golestan", "Hamadan", "Hormozgan", "Ilam", "Kerman", "Kermanshah", "Khorasan-e Janubi", "Khorasan-e Razavi", "Khorasan-e Shemali", "Khuzestan", "Kohgiluyeh va Buyer Ahmad", "Kordestan", "Lorestan", "Markazi", "Mazandaran", "Qazvin", "Qom", "Semnan", "Sistan va Baluchestan", "Tehran", "Yazd", "Zanjan"},
                        States = ""},
                        new CountryModel {
                        CountryId = 104, Flag =  "🇮🇶", TwoDigitCode = "IQ", Name = "Iraq", PhoneCode = "964",
                        StatesList = new[] {"Al Anbar", "Al Basrah", "Al Muthanna", "Al Qadisiyah", "An Najaf", "Arbil", "As Sulaymaniyah", "At Ta'mim", "Babil", "Baghdad", "Dahuk", "Dhi Qar", "Diyala", "Karbala'", "Maysan", "Ninawa", "Salah ad Din", "Wasit"},
                        States = ""},
                        new CountryModel {
                        CountryId = 105, Flag =  "🇮🇪", TwoDigitCode = "IE", Name = "Ireland", PhoneCode = "353",
                        StatesList = new[] {"Carlow", "Cavan", "Clare", "Cork", "Donegal", "Dublin", "Galway", "Kerry", "Kildare", "Kilkenny", "Laois", "Leitrim", "Limerick", "Longford", "Louth", "Mayo", "Meath", "Monaghan", "Offaly", "Roscommon", "Sligo", "Tipperary", "Waterford", "Westmeath", "Wexford", "Wicklow"},
                        States = ""},
                        new CountryModel {
                        CountryId = 106, Flag =  "🇮🇱", TwoDigitCode = "IL", Name = "Israel", PhoneCode = "972",
                        StatesList = new[] {"Central", "Haifa", "Jerusalem", "Northern", "Southern", "Tel Aviv"},
                        States = ""},
                        new CountryModel {
                        CountryId = 107, Flag =  "🇮🇹", TwoDigitCode = "IT", Name = "Italy", PhoneCode = "39",
                        StatesList = new[] {"Abruzzo", "Basilicata", "Calabria", "Campania", "Emilia-Romagna", "Friuli-Venezia Giulia", "Lazio", "Liguria", "Lombardia", "Marche", "Molise", "Piemonte", "Puglia", "Sardegna", "Sicilia", "Toscana", "Trentino-Alto Adige", "Umbria", "Valle d'Aosta", "Veneto"},
                        States = ""},
                        new CountryModel {
                        CountryId = 108, Flag =  "🇯🇲", TwoDigitCode = "JM", Name = "Jamaica", PhoneCode = "1876",
                        StatesList = new[] {"Clarendon", "Hanover", "Kingston", "Manchester", "Portland", "Saint Andrew", "Saint Ann", "Saint Catherine", "Saint Elizabeth", "Saint James", "Saint Mary", "Saint Thomas", "Trelawny", "Westmoreland"},
                        States = ""},
                        new CountryModel {
                        CountryId = 109, Flag =  "🇯🇵", TwoDigitCode = "JP", Name = "Japan", PhoneCode = "81",
                        StatesList = new[] {"Aichi", "Akita", "Aomori", "Chiba", "Ehime", "Fukui", "Fukuoka", "Fukushima", "Gifu", "Gumma", "Hiroshima", "Hokkaido", "Hyogo", "Ibaraki", "Ishikawa", "Iwate", "Kagawa", "Kagoshima", "Kanagawa", "Kochi", "Kumamoto", "Kyoto", "Mie", "Miyagi", "Miyazaki", "Nagano", "Nagasaki", "Nara", "Niigata", "Oita", "Okayama", "Okinawa", "Osaka", "Saga", "Saitama", "Shiga", "Shimane", "Shizuoka", "Tochigi", "Tokushima", "Tokyo", "Tottori", "Toyama", "Wakayama", "Yamagata", "Yamaguchi", "Yamanashi"},
                        States = ""},
                        new CountryModel {
                        CountryId = 110, Flag =  "🇯🇪", TwoDigitCode = "XJ", Name = "Jersey", PhoneCode = "44",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 111, Flag =  "🇯🇴", TwoDigitCode = "JO", Name = "Jordan", PhoneCode = "962",
                        StatesList = new[] {"Ajlun", "Al 'Aqabah", "Al Balqa'", "Al Karak", "Al Mafraq", "'Amman", "At Tafilah", "Az Zarqa'", "Irbid", "Jarash", "Ma'an", "Madaba"},
                        States = ""},
                        new CountryModel {
                        CountryId = 112, Flag =  "🇰🇿", TwoDigitCode = "KZ", Name = "Kazakhstan", PhoneCode = "7",
                        StatesList = new[] {"Almaty Oblysy", "Almaty Qalasy", "Aqmola Oblysy", "Aqtobe Oblysy", "Astana Qalasy", "Atyrau Oblysy", "Batys Qazaqstan Oblysy", "Bayqongyr Qalasy", "Mangghystau Oblysy", "Ongtustik Qazaqstan Oblysy", "Pavlodar Oblysy", "Qaraghandy Oblysy", "Qostanay Oblysy", "Qyzylorda Oblysy", "Shyghys Qazaqstan Oblysy", "Soltustik Qazaqstan Oblysy", "Zhambyl Oblysy"},
                        States = ""},
                        new CountryModel {
                        CountryId = 113, Flag =  "🇰🇪", TwoDigitCode = "KE", Name = "Kenya", PhoneCode = "254",
                        StatesList = new[] {"Central", "Coast", "Eastern", "Nairobi Area", "North Eastern", "Nyanza", "Rift Valley", "Western"},
                        States = ""},
                        new CountryModel {
                        CountryId = 114, Flag =  "🇰🇮", TwoDigitCode = "KI", Name = "Kiribati", PhoneCode = "686",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 115, Flag =  "🇰🇵", TwoDigitCode = "KP", Name = "Korea North", PhoneCode = "850",
                        StatesList = new[] {"Chagang", "North Hamgyong", "South Hamgyong", "North Hwanghae", "South Hwanghae", "Kangwon", "North P'yongan", "South P'yongan", "Yanggang", "Kaesong", "Najin", "Namp'o", "Pyongyang"},
                        States = ""},
                        new CountryModel {
                        CountryId = 116, Flag =  "🇰🇷", TwoDigitCode = "KR", Name = "Korea South", PhoneCode = "82",
                        StatesList = new[] {"Seoul", "Busan City", "Daegu City", "Incheon City", "Gwangju City", "Daejeon City", "Ulsan", "Gyeonggi Province", "Gangwon Province", "North Chungcheong Province", "South Chungcheong Province", "North Jeolla Province", "South Jeolla Province", "North Gyeongsang Province", "South Gyeongsang Province", "Jeju"},
                        States = ""},
                        new CountryModel {
                        CountryId = 117, Flag =  "🇰🇼", TwoDigitCode = "KW", Name = "Kuwait", PhoneCode = "965",
                        StatesList = new[] {"Al Ahmadi", "Al Farwaniyah", "Al Asimah", "Al Jahra", "Hawalli", "Mubarak Al-Kabeer"},
                        States = ""},
                        new CountryModel {
                        CountryId = 118, Flag =  "🇰🇬", TwoDigitCode = "KG", Name = "Kyrgyzstan", PhoneCode = "996",
                        StatesList = new[] {"Batken Oblasty", "Bishkek Shaary", "Chuy Oblasty", "Jalal-Abad Oblasty", "Naryn Oblasty", "Osh Oblasty", "Talas Oblasty", "Ysyk-Kol Oblasty"},
                        States = ""},
                        new CountryModel {
                        CountryId = 119, Flag =  "🇱🇦", TwoDigitCode = "LA", Name = "Laos", PhoneCode = "856",
                        StatesList = new[] {"Attapu", "Bokeo", "Bolikhamxai", "Champasak", "Houaphan", "Khammouan", "Louangnamtha", "Louangphrabang", "Oudomxai", "Phongsali", "Salavan", "Savannakhet", "Viangchan", "Viangchan", "Xaignabouli", "Xaisomboun", "Xekong", "Xiangkhoang"},
                        States = ""},
                        new CountryModel {
                        CountryId = 120, Flag =  "🇱🇻", TwoDigitCode = "LV", Name = "Latvia", PhoneCode = "371",
                        StatesList = new[] {"Aizkraukles Rajons", "Aluksnes Rajons", "Balvu Rajons", "Bauskas Rajons", "Cesu Rajons", "Daugavpils", "Daugavpils Rajons", "Dobeles Rajons", "Gulbenes Rajons", "Jekabpils Rajons", "Jelgava", "Jelgavas Rajons", "Jurmala", "Kraslavas Rajons", "Kuldigas Rajons", "Liepaja", "Liepajas Rajons", "Limbazu Rajons", "Ludzas Rajons", "Madonas Rajons", "Ogres Rajons", "Preilu Rajons", "Rezekne", "Rezeknes Rajons", "Riga", "Rigas Rajons", "Saldus Rajons", "Talsu Rajons", "Tukuma Rajons", "Valkas Rajons", "Valmieras Rajons", "Ventspils", "Ventspils Rajons"},
                        States = ""},
                        new CountryModel {
                        CountryId = 121, Flag =  "🇱🇧", TwoDigitCode = "LB", Name = "Lebanon", PhoneCode = "961",
                        StatesList = new[] {"Beyrouth", "Beqaa", "Liban-Nord", "Liban-Sud", "Mont-Liban", "Nabatiye"},
                        States = ""},
                        new CountryModel {
                        CountryId = 122, Flag =  "🇱🇸", TwoDigitCode = "LS", Name = "Lesotho", PhoneCode = "266",
                        StatesList = new[] {"Berea", "Butha-Buthe", "Leribe", "Mafeteng", "Maseru", "Mohale's Hoek", "Mokhotlong", "Qacha's Nek", "Quthing", "Thaba-Tseka"},
                        States = ""},
                        new CountryModel {
                        CountryId = 123, Flag =  "🇱🇷", TwoDigitCode = "LR", Name = "Liberia", PhoneCode = "231",
                        StatesList = new[] {"Bomi", "Bong", "Gbarpolu", "Grand Bassa", "Grand Cape Mount", "Grand Gedeh", "Grand Kru", "Lofa", "Margibi", "Maryland", "Montserrado", "Nimba", "River Cess", "River Gee", "Sinoe"},
                        States = ""},
                        new CountryModel {
                        CountryId = 124, Flag =  "🇱🇾", TwoDigitCode = "LY", Name = "Libya", PhoneCode = "218",
                        StatesList = new[] {"Ajdabiya", "Al 'Aziziyah", "Al Fatih", "Al Jabal al Akhdar", "Al Jufrah", "Al Khums", "Al Kufrah", "An Nuqat al Khams", "Ash Shati'", "Awbari", "Az Zawiyah", "Banghazi", "Darnah", "Ghadamis", "Gharyan", "Misratah", "Murzuq", "Sabha", "Sawfajjin", "Surt", "Tarabulus", "Tarhunah", "Tubruq", "Yafran", "Zlitan"},
                        States = ""},
                        new CountryModel {
                        CountryId = 125, Flag =  "🇱🇮", TwoDigitCode = "LI", Name = "Liechtenstein", PhoneCode = "423",
                        StatesList = new[] {"Balzers", "Eschen", "Gamprin", "Mauren", "Planken", "Ruggell", "Schaan", "Schellenberg", "Triesen", "Triesenberg", "Vaduz"},
                        States = ""},
                        new CountryModel {
                        CountryId = 126, Flag =  "🇱🇹", TwoDigitCode = "LT", Name = "Lithuania", PhoneCode = "370",
                        StatesList = new[] {"Alytaus", "Kauno", "Klaipedos", "Marijampoles", "Panevezio", "Siauliu", "Taurages", "Telsiu", "Utenos", "Vilniaus"},
                        States = ""},
                        new CountryModel {
                        CountryId = 127, Flag =  "🇱🇺", TwoDigitCode = "LU", Name = "Luxembourg", PhoneCode = "352",
                        StatesList = new[] {"Diekirch", "Grevenmacher", "Luxembourg"},
                        States = ""},
                        new CountryModel {
                        CountryId = 128, Flag =  "🇲🇴", TwoDigitCode = "MO", Name = "Macao", PhoneCode = "853",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 129, Flag =  "🇲🇰", TwoDigitCode = "MK", Name = "Macedonia", PhoneCode = "389",
                        StatesList = new[] {"Aerodrom", "Aracinovo", "Berovo", "Bitola", "Bogdanci", "Bogovinje", "Bosilovo", "Brvenica", "Butel", "Cair", "Caska", "Centar", "Centar Zupa", "Cesinovo", "Cucer-Sandevo", "Debar", "Debartsa", "Delcevo", "Demir Hisar", "Demir Kapija", "Dojran", "Dolneni", "Drugovo", "Gazi Baba", "Gevgelija", "Gjorce Petrov", "Gostivar", "Gradsko", "Ilinden", "Jegunovce", "Karbinci", "Karpos", "Kavadarci", "Kicevo", "Kisela Voda", "Kocani", "Konce", "Kratovo", "Kriva Palanka", "Krivogastani", "Krusevo", "Kumanovo", "Lipkovo", "Lozovo", "Makedonska Kamenica", "Makedonski Brod", "Mavrovo i Rastusa", "Mogila", "Negotino", "Novaci", "Novo Selo", "Ohrid", "Oslomej", "Pehcevo", "Petrovec", "Plasnica", "Prilep", "Probistip", "Radovis", "Rankovce", "Resen", "Rosoman", "Saraj", "Skopje", "Sopiste", "Staro Nagoricane", "Stip", "Struga", "Strumica", "Studenicani", "Suto Orizari", "Sveti Nikole", "Tearce", "Tetovo", "Valandovo", "Vasilevo", "Veles", "Vevcani", "Vinica", "Vranestica", "Vrapciste", "Zajas", "Zelenikovo", "Zelino", "Zrnovci"},
                        States = ""},
                        new CountryModel {
                        CountryId = 130, Flag =  "🇲🇬", TwoDigitCode = "MG", Name = "Madagascar", PhoneCode = "261",
                        StatesList = new[] {"Antananarivo", "Antsiranana", "Fianarantsoa", "Mahajanga", "Toamasina", "Toliara"},
                        States = ""},
                        new CountryModel {
                        CountryId = 131, Flag =  "🇲🇼", TwoDigitCode = "MW", Name = "Malawi", PhoneCode = "265",
                        StatesList = new[] {"Balaka", "Blantyre", "Chikwawa", "Chiradzulu", "Chitipa", "Dedza", "Dowa", "Karonga", "Kasungu", "Likoma", "Lilongwe", "Machinga", "Mangochi", "Mchinji", "Mulanje", "Mwanza", "Mzimba", "Ntcheu", "Nkhata Bay", "Nkhotakota", "Nsanje", "Ntchisi", "Phalombe", "Rumphi", "Salima", "Thyolo", "Zomba"},
                        States = ""},
                        new CountryModel {
                        CountryId = 132, Flag =  "🇲🇾", TwoDigitCode = "MY", Name = "Malaysia", PhoneCode = "60",
                        StatesList = new[] {"Johor", "Kedah", "Kelantan", "Kuala Lumpur", "Labuan", "Malacca", "Negeri Sembilan", "Pahang", "Perak", "Perlis", "Penang", "Sabah", "Sarawak", "Selangor", "Terengganu"},
                        States = ""},
                        new CountryModel {
                        CountryId = 133, Flag =  "🇲🇻", TwoDigitCode = "MV", Name = "Maldives", PhoneCode = "960",
                        StatesList = new[] {"Alifu", "Baa", "Dhaalu", "Faafu", "Gaafu Alifu", "Gaafu Dhaalu", "Gnaviyani", "Haa Alifu", "Haa Dhaalu", "Kaafu", "Laamu", "Lhaviyani", "Maale", "Meemu", "Noonu", "Raa", "Seenu", "Shaviyani", "Thaa", "Vaavu"},
                        States = ""},
                        new CountryModel {
                        CountryId = 134, Flag =  "🇲🇱", TwoDigitCode = "ML", Name = "Mali", PhoneCode = "223",
                        StatesList = new[] {"Bamako (Capital)", "Gao", "Kayes", "Kidal", "Koulikoro", "Mopti", "Segou", "Sikasso", "Tombouctou"},
                        States = ""},
                        new CountryModel {
                        CountryId = 135, Flag =  "🇲🇹", TwoDigitCode = "MT", Name = "Malta", PhoneCode = "356",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 136, Flag =  "🇮🇲", TwoDigitCode = "XM", Name = "Isle of Man", PhoneCode = "44",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 137, Flag =  "🇲🇭", TwoDigitCode = "MH", Name = "Marshall Islands", PhoneCode = "692",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 138, Flag =  "🇲🇶", TwoDigitCode = "MQ", Name = "Martinique", PhoneCode = "596",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 139, Flag =  "🇲🇷", TwoDigitCode = "MR", Name = "Mauritania", PhoneCode = "222",
                        StatesList = new[] {"Adrar", "Assaba", "Brakna", "Dakhlet Nouadhibou", "Gorgol", "Guidimaka", "Hodh Ech Chargui", "Hodh El Gharbi", "Inchiri", "Nouakchott", "Tagant", "Tiris Zemmour", "Trarza"},
                        States = ""},
                        new CountryModel {
                        CountryId = 140, Flag =  "🇲🇺", TwoDigitCode = "MU", Name = "Mauritius", PhoneCode = "230",
                        StatesList = new[] {"Agalega Islands", "Black River", "Cargados Carajos Shoals", "Flacq", "Grand Port", "Moka", "Pamplemousses", "Plaines Wilhems", "Port Louis", "Riviere du Rempart", "Rodrigues", "Savanne"},
                        States = ""},
                        new CountryModel {
                        CountryId = 141, Flag =  "🇾🇹", TwoDigitCode = "YT", Name = "Mayotte", PhoneCode = "269",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 142, Flag =  "🇲🇽", TwoDigitCode = "MX", Name = "Mexico", PhoneCode = "52",
                        StatesList = new[] {"Aguascalientes", "Baja California", "Baja California Sur", "Campeche", "Chiapas", "Chihuahua", "Coahuila de Zaragoza", "Colima", "Distrito Federal", "Durango", "Guanajuato", "Guerrero", "Hidalgo", "Jalisco", "Mexico", "Michoacan de Ocampo", "Morelos", "Nayarit", "Nuevo Leon", "Oaxaca", "Puebla", "Queretaro de Arteaga", "Quintana Roo", "San Luis Potosi", "Sinaloa", "Sonora", "Tabasco", "Tamaulipas", "Tlaxcala", "Veracruz-Llave", "Yucatan", "Zacatecas"},
                        States = ""},
                        new CountryModel {
                        CountryId = 143, Flag =  "🇫🇲", TwoDigitCode = "FM", Name = "Micronesia", PhoneCode = "691",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 144, Flag =  "🇲🇩", TwoDigitCode = "MD", Name = "Moldova", PhoneCode = "373",
                        StatesList = new[] {"Anenii Noi", "Basarabeasca", "Briceni", "Cahul", "Cantemir", "Calarasi", "Causeni", "Cimislia", "Criuleni", "Donduseni", "Drochia", "Dubasari", "Edinet", "Falesti", "Floresti", "Glodeni", "Hincesti", "Ialoveni", "Leova", "Nisporeni", "Ocnita", "Orhei", "Rezina", "Riscani", "Singerei", "Soldanesti", "Soroca", "Stefan-Voda", "Straseni", "Taraclia", "Telenesti", "Ungheni", "Balti", "Bender", "Chisinau", "Gagauzia", "Stinga Nistrului"},
                        States = ""},
                        new CountryModel {
                        CountryId = 145, Flag =  "🇲🇨", TwoDigitCode = "MC", Name = "Monaco", PhoneCode = "377",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 146, Flag =  "🇲🇳", TwoDigitCode = "MN", Name = "Mongolia", PhoneCode = "976",
                        StatesList = new[] {"Arhangay", "Bayanhongor", "Bayan-Olgiy", "Bulgan", "Darhan Uul", "Dornod", "Dornogovi", "Dundgovi", "Dzavhan", "Govi-Altay", "Govi-Sumber", "Hentiy", "Hovd", "Hovsgol", "Omnogovi", "Orhon", "Ovorhangay", "Selenge", "Suhbaatar", "Tov", "Ulaanbaatar", "Uvs"},
                        States = ""},
                        new CountryModel {
                        CountryId = 147, Flag =  "🇲🇸", TwoDigitCode = "MS", Name = "Montserrat", PhoneCode = "1664",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 148, Flag =  "🇲🇦", TwoDigitCode = "MA", Name = "Morocco", PhoneCode = "212",
                        StatesList = new[] {"Agadir", "Al Hoceima", "Azilal", "Beni Mellal", "Ben Slimane", "Boulemane", "Casablanca", "Chaouen", "El Jadida", "El Kelaa des Sraghna", "Er Rachidia", "Essaouira", "Fes", "Figuig", "Guelmim", "Ifrane", "Kenitra", "Khemisset", "Khenifra", "Khouribga", "Laayoune", "Larache", "Marrakech", "Meknes", "Nador", "Ouarzazate", "Oujda", "Rabat-Sale", "Safi", "Settat", "Sidi Kacem", "Tangier", "Tan-Tan", "Taounate", "Taroudannt", "Tata", "Taza", "Tetouan", "Tiznit"},
                        States = ""},
                        new CountryModel {
                        CountryId = 149, Flag =  "🇲🇿", TwoDigitCode = "MZ", Name = "Mozambique", PhoneCode = "258",
                        StatesList = new[] {"Cabo Delgado", "Gaza", "Inhambane", "Manica", "Maputo", "Cidade de Maputo", "Nampula", "Niassa", "Sofala", "Tete", "Zambezia"},
                        States = ""},
                        new CountryModel {
                        CountryId = 150, Flag =  "🇲🇲", TwoDigitCode = "MM", Name = "Myanmar", PhoneCode = "95",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 151, Flag =  "🇳🇦", TwoDigitCode = "NA", Name = "Namibia", PhoneCode = "264",
                        StatesList = new[] {"Caprivi", "Erongo", "Hardap", "Karas", "Khomas", "Kunene", "Ohangwena", "Okavango", "Omaheke", "Omusati", "Oshana", "Oshikoto", "Otjozondjupa"},
                        States = ""},
                        new CountryModel {
                        CountryId = 152, Flag =  "🇳🇷", TwoDigitCode = "NR", Name = "Nauru", PhoneCode = "674",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 153, Flag =  "🇳🇵", TwoDigitCode = "NP", Name = "Nepal", PhoneCode = "977",
                        StatesList = new[] {"Bagmati", "Bheri", "Dhawalagiri", "Gandaki", "Janakpur", "Karnali", "Kosi", "Lumbini", "Mahakali", "Mechi", "Narayani", "Rapti", "Sagarmatha", "Seti"},
                        States = ""},
                        new CountryModel {
                        CountryId = 154, Flag =  "",   TwoDigitCode = "AN", Name = "Netherlands Antilles", PhoneCode = "599",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 155, Flag =  "🇳🇱", TwoDigitCode = "NL", Name = "Netherlands", PhoneCode = "31",
                        StatesList = new[] {"Drenthe", "Flevoland", "Friesland", "Gelderland", "Groningen", "Limburg", "Noord-Brabant", "Noord-Holland", "Overijssel", "Utrecht", "Zeeland", "Zuid-Holland"},
                        States = ""},
                        new CountryModel {
                        CountryId = 156, Flag =  "🇳🇨", TwoDigitCode = "NC", Name = "New Caledonia", PhoneCode = "687",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 157, Flag =  "🇳🇿", TwoDigitCode = "NZ", Name = "New Zealand", PhoneCode = "64",
                        StatesList = new[] {"Auckland", "Bay of Plenty", "Canterbury", "Chatham Islands", "Gisborne", "Hawke's Bay", "Manawatu-Wanganui", "Marlborough", "Nelson", "Northland", "Otago", "Southland", "Taranaki", "Tasman", "Waikato", "Wellington", "West Coast"},
                        States = ""},
                        new CountryModel {
                        CountryId = 158, Flag =  "🇳🇮", TwoDigitCode = "NI", Name = "Nicaragua", PhoneCode = "505",
                        StatesList = new[] {"Atlantico Norte", "Atlantico Sur", "Boaco", "Carazo", "Chinandega", "Chontales", "Esteli", "Granada", "Jinotega", "Leon", "Madriz", "Managua", "Masaya", "Matagalpa", "Nueva Segovia", "Rio San Juan", "Rivas"},
                        States = ""},
                        new CountryModel {
                        CountryId = 159, Flag =  "🇳🇪", TwoDigitCode = "NE", Name = "Niger", PhoneCode = "227",
                        StatesList = new[] {"Agadez", "Diffa", "Dosso", "Maradi", "Niamey", "Tahoua", "Tillaberi", "Zinder"},
                        States = ""},
                        new CountryModel {
                        CountryId = 160, Flag =  "🇳🇬", TwoDigitCode = "NG", Name = "Nigeria", PhoneCode = "234",
                        StatesList = new[] {"Abia", "Abuja Federal Capital", "Adamawa", "Akwa Ibom", "Anambra", "Bauchi", "Bayelsa", "Benue", "Borno", "Cross River", "Delta", "Ebonyi", "Edo", "Ekiti", "Enugu", "Gombe", "Imo", "Jigawa", "Kaduna", "Kano", "Katsina", "Kebbi", "Kogi", "Kwara", "Lagos", "Nassarawa", "Niger", "Ogun", "Ondo", "Osun", "Oyo", "Plateau", "Rivers", "Sokoto", "Taraba", "Yobe", "Zamfara"},
                        States = ""},
                        new CountryModel {
                        CountryId = 161, Flag =  "🇳🇺", TwoDigitCode = "NU", Name = "Niue", PhoneCode = "683",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 162, Flag =  "🇳🇫", TwoDigitCode = "NF", Name = "Norfolk Island", PhoneCode = "672",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 163, Flag =  "🇲🇵", TwoDigitCode = "MP", Name = "Northern Mariana Islands", PhoneCode = "1670",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 164, Flag =  "🇳🇴", TwoDigitCode = "NO", Name = "Norway", PhoneCode = "47",
                        StatesList = new[] {"Akershus", "Aust-Agder", "Buskerud", "Finnmark", "Hedmark", "Hordaland", "More og Romsdal", "Nordland", "Nord-Trondelag", "Oppland", "Oslo", "Ostfold", "Rogaland", "Sogn og Fjordane", "Sor-Trondelag", "Telemark", "Troms", "Vest-Agder", "Vestfold"},
                        States = ""},
                        new CountryModel {
                        CountryId = 165, Flag =  "🇴🇲", TwoDigitCode = "OM", Name = "Oman", PhoneCode = "968",
                        StatesList = new[] {"Ad Dakhiliyah", "Al Batinah", "Al Wusta", "Ash Sharqiyah", "Az Zahirah", "Masqat", "Musandam", "Dhofar"},
                        States = ""},
                        new CountryModel {
                        CountryId = 166, Flag =  "🇵🇰", TwoDigitCode = "PK", Name = "Pakistan", PhoneCode = "92",
                        StatesList = new[] {"Balochistan", "North-West Frontier Province", "Punjab", "Sindh", "Islamabad Capital Territory", "Federally Administered Tribal Areas"},
                        States = ""},
                        new CountryModel {
                        CountryId = 167, Flag =  "🇵🇼", TwoDigitCode = "PW", Name = "Palau", PhoneCode = "680",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 168, Flag =  "🇵🇸", TwoDigitCode = "PS", Name = "Palestine", PhoneCode = "970",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 169, Flag =  "🇵🇦", TwoDigitCode = "PA", Name = "Panama", PhoneCode = "507",
                        StatesList = new[] {"Bocas del Toro", "Chiriqui", "Cocle", "Colon", "Darien", "Herrera", "Los Santos", "Panama", "San Blas", "Veraguas"},
                        States = ""},
                        new CountryModel {
                        CountryId = 170, Flag =  "🇵🇬", TwoDigitCode = "PG", Name = "Papua new Guinea", PhoneCode = "675",
                        StatesList = new[] {"Bougainville", "Central", "Chimbu", "Eastern Highlands", "East New Britain", "East Sepik", "Enga", "Gulf", "Madang", "Manus", "Milne Bay", "Morobe", "National Capital", "New Ireland", "Northern", "Sandaun", "Southern Highlands", "Western", "Western Highlands", "West New Britain"},
                        States = ""},
                        new CountryModel {
                        CountryId = 171, Flag =  "🇵🇾", TwoDigitCode = "PY", Name = "Paraguay", PhoneCode = "595",
                        StatesList = new[] {"Alto Paraguay", "Alto Parana", "Amambay", "Asuncion", "Boqueron", "Caaguazu", "Caazapa", "Canindeyu", "Central", "Concepcion", "Cordillera", "Guaira", "Itapua", "Misiones", "Neembucu", "Paraguari", "Presidente Hayes", "San Pedro"},
                        States = ""},
                        new CountryModel {
                        CountryId = 172, Flag =  "🇵🇪", TwoDigitCode = "PE", Name = "Peru", PhoneCode = "51",
                        StatesList = new[] {"Amazonas", "Ancash", "Apurimac", "Arequipa", "Ayacucho", "Cajamarca", "Callao", "Cusco", "Huancavelica", "Huanuco", "Ica", "Junin", "La Libertad", "Lambayeque", "Lima", "Loreto", "Madre de Dios", "Moquegua", "Pasco", "Piura", "Puno", "San Martin", "Tacna", "Tumbes", "Ucayali"},
                        States = ""},
                        new CountryModel {
                        CountryId = 173, Flag =  "🇵🇭", TwoDigitCode = "PH", Name = "Philippines", PhoneCode = "63",
                        StatesList = new[] {"Abra", "Agusan del Norte", "Agusan del Sur", "Aklan", "Albay", "Antique", "Apayao", "Aurora", "Basilan", "Bataan", "Batanes", "Batangas", "Biliran", "Benguet", "Bohol", "Bukidnon", "Bulacan", "Cagayan", "Camarines Norte", "Camarines Sur", "Camiguin", "Capiz", "Catanduanes", "Cavite", "Cebu", "Compostela", "Davao del Norte", "Davao del Sur", "Davao Oriental", "Eastern Samar", "Guimaras", "Ifugao", "Ilocos Norte", "Ilocos Sur", "Iloilo", "Isabela", "Kalinga", "Laguna", "Lanao del Norte", "Lanao del Sur", "La Union", "Leyte", "Maguindanao", "Marinduque", "Masbate", "Mindoro Occidental", "Mindoro Oriental", "Misamis Occidental", "Misamis Oriental", "Mountain Province", "Negros Occidental", "Negros Oriental", "North Cotabato", "Northern Samar", "Nueva Ecija", "Nueva Vizcaya", "Palawan", "Pampanga", "Pangasinan", "Quezon", "Quirino", "Rizal", "Romblon", "Samar", "Sarangani", "Siquijor", "Sorsogon", "South Cotabato", "Southern Leyte", "Sultan Kudarat", "Sulu", "Surigao del Norte", "Surigao del Sur", "Tarlac", "Tawi-Tawi", "Zambales", "Zamboanga del Norte", "Zamboanga del Sur", "Zamboanga Sibugay"},
                        States = ""},
                        new CountryModel {
                        CountryId = 174, Flag =  "🇵🇳", TwoDigitCode = "PN", Name = "Pitcairn Island", PhoneCode = "0",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 175, Flag =  "🇵🇱", TwoDigitCode = "PL", Name = "Poland", PhoneCode = "48",
                        StatesList = new[] {"Greater Poland (Wielkopolskie)", "Kuyavian-Pomeranian (Kujawsko-Pomorskie)", "Lesser Poland (Malopolskie)", "Lodz (Lodzkie)", "Lower Silesian (Dolnoslaskie)", "Lublin (Lubelskie)", "Lubusz (Lubuskie)", "Masovian (Mazowieckie)", "Opole (Opolskie)", "Podlasie (Podlaskie)", "Pomeranian (Pomorskie)", "Silesian (Slaskie)", "Subcarpathian (Podkarpackie)", "Swietokrzyskie (Swietokrzyskie)", "Warmian-Masurian (Warminsko-Mazurskie)", "West Pomeranian (Zachodniopomorskie)"},
                        States = ""},
                        new CountryModel {
                        CountryId = 176, Flag =  "🇵🇹", TwoDigitCode = "PT", Name = "Portugal", PhoneCode = "351",
                        StatesList = new[] {"Aveiro", "Acores", "Beja", "Braga", "Braganca", "Castelo Branco", "Coimbra", "Evora", "Faro", "Guarda", "Leiria", "Lisboa", "Madeira", "Portalegre", "Porto", "Santarem", "Setubal", "Viana do Castelo", "Vila Real", "Viseu"},
                        States = ""},
                        new CountryModel {
                        CountryId = 177, Flag =  "🇵🇷", TwoDigitCode = "PR", Name = "Puerto Rico", PhoneCode = "1787",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 178, Flag =  "🇶🇦", TwoDigitCode = "QA", Name = "Qatar", PhoneCode = "974",
                        StatesList = new[] {"Ad Dawhah", "Al Ghuwayriyah", "Al Jumayliyah", "Al Khawr", "Al Wakrah", "Ar Rayyan", "Jarayan al Batinah", "Madinat ash Shamal", "Umm Sa'id", "Umm Salal"},
                        States = ""},
                        new CountryModel {
                        CountryId = 179, Flag =  "🇷🇪", TwoDigitCode = "RE", Name = "Reunion", PhoneCode = "262",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 180, Flag =  "🇷🇴", TwoDigitCode = "RO", Name = "Romania", PhoneCode = "40",
                        StatesList = new[] {"Alba", "Arad", "Arges", "Bacau", "Bihor", "Bistrita-Nasaud", "Botosani", "Braila", "Brasov", "Bucuresti", "Buzau", "Calarasi", "Caras-Severin", "Cluj", "Constanta", "Covasna", "Dimbovita", "Dolj", "Galati", "Gorj", "Giurgiu", "Harghita", "Hunedoara", "Ialomita", "Iasi", "Ilfov", "Maramures", "Mehedinti", "Mures", "Neamt", "Olt", "Prahova", "Salaj", "Satu Mare", "Sibiu", "Suceava", "Teleorman", "Timis", "Tulcea", "Vaslui", "Vilcea", "Vrancea"},
                        States = ""},
                        new CountryModel {
                        CountryId = 181, Flag =  "🇷🇺", TwoDigitCode = "RU", Name = "Russia", PhoneCode = "70",
                        StatesList = new[] {"Amur", "Arkhangel'sk", "Astrakhan'", "Belgorod", "Bryansk", "Chelyabinsk", "Chita", "Irkutsk", "Ivanovo", "Kaliningrad", "Kaluga", "Kamchatka", "Kemerovo", "Kirov", "Kostroma", "Kurgan", "Kursk", "Leningrad", "Lipetsk", "Magadan", "Moscow", "Murmansk", "Nizhniy Novgorod", "Novgorod", "Novosibirsk", "Omsk", "Orenburg", "Orel", "Penza", "Perm'", "Pskov", "Rostov", "Ryazan'", "Sakhalin", "Samara", "Saratov", "Smolensk", "Sverdlovsk", "Tambov", "Tomsk", "Tula", "Tver'", "Tyumen'", "Ul'yanovsk", "Vladimir", "Volgograd", "Vologda", "Voronezh", "Yaroslavl'", "Adygeya", "Altay", "Bashkortostan", "Buryatiya", "Chechnya", "Chuvashiya", "Dagestan", "Ingushetiya", "Kabardino-Balkariya", "Kalmykiya", "Karachayevo-Cherkesiya", "Kareliya", "Khakasiya", "Komi", "Mariy-El", "Mordoviya", "Sakha", "North Ossetia", "Tatarstan", "Tyva", "Udmurtiya", "Aga Buryat", "Chukotka", "Evenk", "Khanty-Mansi", "Komi-Permyak", "Koryak", "Nenets", "Taymyr", "Ust'-Orda Buryat", "Yamalo-Nenets", "Altay", "Khabarovsk", "Krasnodar", "Krasnoyarsk", "Primorskiy", "Stavropol'", "Moscow", "St. Petersburg", "Yevrey"},
                        States = ""},
                        new CountryModel {
                        CountryId = 182, Flag =  "🇷🇼", TwoDigitCode = "RW", Name = "Rwanda", PhoneCode = "250",
                        StatesList = new[] {"Butare", "Byumba", "Cyangugu", "Gikongoro", "Gisenyi", "Gitarama", "Kibungo", "Kibuye", "Kigali Rurale", "Kigali-ville", "Umutara", "Ruhengeri"},
                        States = ""},
                        new CountryModel {
                        CountryId = 183, Flag =  "🇸🇭", TwoDigitCode = "SH", Name = "Saint Helena", PhoneCode = "290",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 184, Flag =  "🇰🇳", TwoDigitCode = "KN", Name = "Saint Kitts And Nevis", PhoneCode = "1869",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 185, Flag =  "🇱🇨", TwoDigitCode = "LC", Name = "Saint Lucia", PhoneCode = "1758",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 186, Flag =  "🇵🇲", TwoDigitCode = "PM", Name = "Saint Pierre and Miquelon", PhoneCode = "508",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 187, Flag =  "🇻🇨", TwoDigitCode = "VC", Name = "Saint Vincent And The Grenadines", PhoneCode = "1784",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 188, Flag =  "🇼🇸", TwoDigitCode = "WS", Name = "Samoa", PhoneCode = "684",
                        StatesList = new[] {"A'ana", "Aiga-i-le-Tai", "Atua", "Fa'asaleleaga", "Gaga'emauga", "Gagaifomauga", "Palauli", "Satupa'itea", "Tuamasaga", "Va'a-o-Fonoti", "Vaisigano"},
                        States = ""},
                        new CountryModel {
                        CountryId = 189, Flag =  "🇸🇲", TwoDigitCode = "SM", Name = "San Marino", PhoneCode = "378",
                        StatesList = new[] {"Acquaviva", "Borgo Maggiore", "Chiesanuova", "Domagnano", "Faetano", "Fiorentino", "Montegiardino", "San Marino Citta", "Serravalle"},
                        States = ""},
                        new CountryModel {
                        CountryId = 190, Flag =  "🇸🇹", TwoDigitCode = "ST", Name = "Sao Tome and Principe", PhoneCode = "239",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 191, Flag =  "🇸🇦", TwoDigitCode = "SA", Name = "Saudi Arabia", PhoneCode = "966",
                        StatesList = new[] {"Al Bahah", "Al Hudud ash Shamaliyah", "Al Jawf", "Al Madinah", "Al Qasim", "Ar Riyad", "Ash Sharqiyah", "'Asir", "Ha'il", "Jizan", "Makkah", "Najran", "Tabuk"},
                        States = ""},
                        new CountryModel {
                        CountryId = 192, Flag =  "🇸🇳", TwoDigitCode = "SN", Name = "Senegal", PhoneCode = "221",
                        StatesList = new[] {"Dakar", "Diourbel", "Fatick", "Kaolack", "Kolda", "Louga", "Matam", "Saint-Louis", "Tambacounda", "Thies", "Ziguinchor"},
                        States = ""},
                        new CountryModel {
                        CountryId = 193, Flag =  "🇷🇸", TwoDigitCode = "RS", Name = "Serbia", PhoneCode = "381",
                        StatesList = new[] {"Kosovo", "Montenegro", "Serbia", "Vojvodina"},
                        States = ""},
                        new CountryModel {
                        CountryId = 194, Flag =  "🇸🇨", TwoDigitCode = "SC", Name = "Seychelles", PhoneCode = "248",
                        StatesList = new[] {"Anse aux Pins", "Anse Boileau", "Anse Etoile", "Anse Louis", "Anse Royale", "Baie Lazare", "Baie Sainte Anne", "Beau Vallon", "Bel Air", "Bel Ombre", "Cascade", "Glacis", "Grand' Anse", "Grand' Anse", "La Digue", "La Riviere Anglaise", "Mont Buxton", "Mont Fleuri", "Plaisance", "Pointe La Rue", "Port Glaud", "Saint Louis", "Takamaka"},
                        States = ""},
                        new CountryModel {
                        CountryId = 195, Flag =  "🇸🇱", TwoDigitCode = "SL", Name = "Sierra Leone", PhoneCode = "232",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 196, Flag =  "🇸🇬", TwoDigitCode = "SG", Name = "Singapore", PhoneCode = "65",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 197, Flag =  "🇸🇰", TwoDigitCode = "SK", Name = "Slovakia", PhoneCode = "421",
                        StatesList = new[] {"Banskobystricky", "Bratislavsky", "Kosicky", "Nitriansky", "Presovsky", "Trenciansky", "Trnavsky", "Zilinsky"},
                        States = ""},
                        new CountryModel {
                        CountryId = 198, Flag =  "🇸🇮", TwoDigitCode = "SI", Name = "Slovenia", PhoneCode = "386",
                        StatesList = new[] {"Ajdovscina", "Beltinci", "Benedikt", "Bistrica ob Sotli", "Bled", "Bloke", "Bohinj", "Borovnica", "Bovec", "Braslovce", "Brda", "Brezice", "Brezovica", "Cankova", "Celje", "Cerklje na Gorenjskem", "Cerknica", "Cerkno", "Cerkvenjak", "Crensovci", "Crna na Koroskem", "Crnomelj", "Destrnik", "Divaca", "Dobje", "Dobrepolje", "Dobrna", "Dobrova-Horjul-Polhov Gradec", "Dobrovnik-Dobronak", "Dolenjske Toplice", "Dol pri Ljubljani", "Domzale", "Dornava", "Dravograd", "Duplek", "Gorenja Vas-Poljane", "Gorisnica", "Gornja Radgona", "Gornji Grad", "Gornji Petrovci", "Grad", "Grosuplje", "Hajdina", "Hoce-Slivnica", "Hodos-Hodos", "Horjul", "Hrastnik", "Hrpelje-Kozina", "Idrija", "Ig", "Ilirska Bistrica", "Ivancna Gorica", "Izola-Isola", "Jesenice", "Jezersko", "Jursinci", "Kamnik", "Kanal", "Kidricevo", "Kobarid", "Kobilje", "Kocevje", "Komen", "Komenda", "Koper-Capodistria", "Kostel", "Kozje", "Kranj", "Kranjska Gora", "Krizevci", "Krsko", "Kungota", "Kuzma", "Lasko", "Lenart", "Lendava-Lendva", "Litija", "Ljubljana", "Ljubno", "Ljutomer", "Logatec", "Loska Dolina", "Loski Potok", "Lovrenc na Pohorju", "Luce", "Lukovica", "Majsperk", "Maribor", "Markovci", "Medvode", "Menges", "Metlika", "Mezica", "Miklavz na Dravskem Polju", "Miren-Kostanjevica", "Mirna Pec", "Mislinja", "Moravce", "Moravske Toplice", "Mozirje", "Murska Sobota", "Muta", "Naklo", "Nazarje", "Nova Gorica", "Novo Mesto", "Odranci", "Oplotnica", "Ormoz", "Osilnica", "Pesnica", "Piran-Pirano", "Pivka", "Podcetrtek", "Podlehnik", "Podvelka", "Polzela", "Postojna", "Prebold", "Preddvor", "Prevalje", "Ptuj", "Puconci", "Race-Fram", "Radece", "Radenci", "Radlje ob Dravi", "Radovljica", "Ravne na Koroskem", "Razkrizje", "Ribnica", "Ribnica na Pohorju", "Rogasovci", "Rogaska Slatina", "Rogatec", "Ruse", "Salovci", "Selnica ob Dravi", "Semic", "Sempeter-Vrtojba", "Sencur", "Sentilj", "Sentjernej", "Sentjur pri Celju", "Sevnica", "Sezana", "Skocjan", "Skofja Loka", "Skofljica", "Slovenj Gradec", "Slovenska Bistrica", "Slovenske Konjice", "Smarje pri Jelsah", "Smartno ob Paki", "Smartno pri Litiji", "Sodrazica", "Solcava", "Sostanj", "Starse", "Store", "Sveta Ana", "Sveti Andraz v Slovenskih Goricah", "Sveti Jurij", "Tabor", "Tisina", "Tolmin", "Trbovlje", "Trebnje", "Trnovska Vas", "Trzic", "Trzin", "Turnisce", "Velenje", "Velika Polana", "Velike Lasce", "Verzej", "Videm", "Vipava", "Vitanje", "Vodice", "Vojnik", "Vransko", "Vrhnika", "Vuzenica", "Zagorje ob Savi", "Zalec", "Zavrc", "Zelezniki", "Zetale", "Ziri", "Zirovnica", "Zuzemberk", "Zrece"},
                        States = ""},
                        new CountryModel {
                        CountryId = 199, Flag =  "",   TwoDigitCode = "XG", Name = "Smaller Territories of the UK", PhoneCode = "44",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 200, Flag =  "🇸🇧", TwoDigitCode = "SB", Name = "Solomon Islands", PhoneCode = "677",
                        StatesList = new[] {"Central", "Choiseul", "Guadalcanal", "Honiara", "Isabel", "Makira", "Malaita", "Rennell and Bellona", "Temotu", "Western"},
                        States = ""},
                        new CountryModel {
                        CountryId = 201, Flag =  "🇸🇴", TwoDigitCode = "SO", Name = "Somalia", PhoneCode = "252",
                        StatesList = new[] {"Awdal", "Bakool", "Banaadir", "Bari", "Bay", "Galguduud", "Gedo", "Hiiraan", "Jubbada Dhexe", "Jubbada Hoose", "Mudug", "Nugaal", "Sanaag", "Shabeellaha Dhexe", "Shabeellaha Hoose", "Sool", "Togdheer", "Woqooyi Galbeed"},
                        States = ""},
                        new CountryModel {
                        CountryId = 202, Flag =  "🇿🇦", TwoDigitCode = "ZA", Name = "South Africa", PhoneCode = "27",
                        StatesList = new[] {"Eastern Cape", "Free State", "Gauteng", "KwaZulu-Natal", "Limpopo", "Mpumalanga", "North-West", "Northern Cape", "Western Cape"},
                        States = ""},
                        new CountryModel {
                        CountryId = 203, Flag =  "🇬🇸", TwoDigitCode = "GS", Name = "South Georgia", PhoneCode = "0",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 204, Flag =  "🇸🇸", TwoDigitCode = "SS", Name = "South Sudan", PhoneCode = "211",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 205, Flag =  "🇪🇸", TwoDigitCode = "ES", Name = "Spain", PhoneCode = "34",
                        StatesList = new[] {"Andalucia", "Aragon", "Asturias", "Baleares", "Ceuta", "Canarias", "Cantabria", "Castilla-La Mancha", "Castilla y Leon", "Cataluna", "Comunidad Valenciana", "Extremadura", "Galicia", "La Rioja", "Madrid", "Melilla", "Murcia", "Navarra", "Pais Vasco"},
                        States = ""},
                        new CountryModel {
                        CountryId = 206, Flag =  "🇱🇰", TwoDigitCode = "LK", Name = "Sri Lanka", PhoneCode = "94",
                        StatesList = new[] {"Central", "North Central", "North Eastern", "North Western", "Sabaragamuwa", "Southern", "Uva", "Western"},
                        States = ""},
                        new CountryModel {
                        CountryId = 207, Flag =  "🇸🇩", TwoDigitCode = "SD", Name = "Sudan", PhoneCode = "249",
                        StatesList = new[] {"A'ali an Nil", "Al Bahr al Ahmar", "Al Buhayrat", "Al Jazirah", "Al Khartum", "Al Qadarif", "Al Wahdah", "An Nil al Abyad", "An Nil al Azraq", "Ash Shamaliyah", "Bahr al Jabal", "Gharb al Istiwa'iyah", "Gharb Bahr al Ghazal", "Gharb Darfur", "Gharb Kurdufan", "Janub Darfur", "Janub Kurdufan", "Junqali", "Kassala", "Nahr an Nil", "Shamal Bahr al Ghazal", "Shamal Darfur", "Shamal Kurdufan", "Sharq al Istiwa'iyah", "Sinnar", "Warab"},
                        States = ""},
                        new CountryModel {
                        CountryId = 208, Flag =  "🇸🇷", TwoDigitCode = "SR", Name = "Suriname", PhoneCode = "597",
                        StatesList = new[] {"Brokopondo", "Commewijne", "Coronie", "Marowijne", "Nickerie", "Para", "Paramaribo", "Saramacca", "Sipaliwini", "Wanica"},
                        States = ""},
                        new CountryModel {
                        CountryId = 209, Flag =  "🇸🇯", TwoDigitCode = "SJ", Name = "Svalbard And Jan Mayen Islands", PhoneCode = "47",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 210, Flag =  "🇸🇿", TwoDigitCode = "SZ", Name = "Swaziland", PhoneCode = "268",
                        StatesList = new[] {"Hhohho", "Lubombo", "Manzini", "Shiselweni"},
                        States = ""},
                        new CountryModel {
                        CountryId = 211, Flag =  "🇸🇪", TwoDigitCode = "SE", Name = "Sweden", PhoneCode = "46",
                        StatesList = new[] {"Blekinge", "Dalarnas", "Gavleborgs", "Gotlands", "Hallands", "Jamtlands", "Jonkopings", "Kalmar", "Kronobergs", "Norrbottens", "Orebro", "Ostergotlands", "Skane", "Sodermanlands", "Stockholms", "Uppsala", "Varmlands", "Vasterbottens", "Vasternorrlands", "Vastmanlands", "Vastra Gotalands"},
                        States = ""},
                        new CountryModel {
                        CountryId = 212, Flag =  "🇨🇭", TwoDigitCode = "CH", Name = "Switzerland", PhoneCode = "41",
                        StatesList = new[] {"Aargau", "Appenzell Ausser-Rhoden", "Appenzell Inner-Rhoden", "Basel-Landschaft", "Basel-Stadt", "Bern", "Fribourg", "Geneve", "Glarus", "Graubunden", "Jura", "Luzern", "Neuchatel", "Nidwalden", "Obwalden", "Sankt Gallen", "Schaffhausen", "Schwyz", "Solothurn", "Thurgau", "Ticino", "Uri", "Valais", "Vaud", "Zug", "Zurich"},
                        States = ""},
                        new CountryModel {
                        CountryId = 213, Flag =  "🇸🇾", TwoDigitCode = "SY", Name = "Syria", PhoneCode = "963",
                        StatesList = new[] {"Al Hasakah", "Al Ladhiqiyah", "Al Qunaytirah", "Ar Raqqah", "As Suwayda'", "Dar'a", "Dayr az Zawr", "Dimashq", "Halab", "Hamah", "Hims", "Idlib", "Rif Dimashq", "Tartus"},
                        States = ""},
                        new CountryModel {
                        CountryId = 214, Flag =  "🇹🇼", TwoDigitCode = "TW", Name = "Taiwan", PhoneCode = "886",
                        StatesList = new[] {"Chang-hua", "Chia-i", "Hsin-chu", "Hua-lien", "I-lan", "Kao-hsiung", "Kin-men", "Lien-chiang", "Miao-li", "Nan-t'ou", "P'eng-hu", "P'ing-tung", "T'ai-chung", "T'ai-nan", "T'ai-pei", "T'ai-tung", "T'ao-yuan", "Yun-lin", "Chia-i", "Chi-lung", "Hsin-chu", "T'ai-chung", "T'ai-nan", "Kao-hsiung city", "T'ai-pei city"},
                        States = ""},
                        new CountryModel {
                        CountryId = 215, Flag =  "🇹🇯", TwoDigitCode = "TJ", Name = "Tajikistan", PhoneCode = "992",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 216, Flag =  "🇹🇿", TwoDigitCode = "TZ", Name = "Tanzania", PhoneCode = "255",
                        StatesList = new[] {"Arusha", "Dar es Salaam", "Dodoma", "Iringa", "Kagera", "Kigoma", "Kilimanjaro", "Lindi", "Manyara", "Mara", "Mbeya", "Morogoro", "Mtwara", "Mwanza", "Pemba North", "Pemba South", "Pwani", "Rukwa", "Ruvuma", "Shinyanga", "Singida", "Tabora", "Tanga", "Zanzibar Central/South", "Zanzibar North", "Zanzibar Urban/West"},
                        States = ""},
                        new CountryModel {
                        CountryId = 217, Flag =  "🇹🇭", TwoDigitCode = "TH", Name = "Thailand", PhoneCode = "66",
                        StatesList = new[] {"Amnat Charoen", "Ang Thong", "Buriram", "Chachoengsao", "Chai Nat", "Chaiyaphum", "Chanthaburi", "Chiang Mai", "Chiang Rai", "Chon Buri", "Chumphon", "Kalasin", "Kamphaeng Phet", "Kanchanaburi", "Khon Kaen", "Krabi", "Krung Thep Mahanakhon", "Lampang", "Lamphun", "Loei", "Lop Buri", "Mae Hong Son", "Maha Sarakham", "Mukdahan", "Nakhon Nayok", "Nakhon Pathom", "Nakhon Phanom", "Nakhon Ratchasima", "Nakhon Sawan", "Nakhon Si Thammarat", "Nan", "Narathiwat", "Nong Bua Lamphu", "Nong Khai", "Nonthaburi", "Pathum Thani", "Pattani", "Phangnga", "Phatthalung", "Phayao", "Phetchabun", "Phetchaburi", "Phichit", "Phitsanulok", "Phra Nakhon Si Ayutthaya", "Phrae", "Phuket", "Prachin Buri", "Prachuap Khiri Khan", "Ranong", "Ratchaburi", "Rayong", "Roi Et", "Sa Kaeo", "Sakon Nakhon", "Samut Prakan", "Samut Sakhon", "Samut Songkhram", "Sara Buri", "Satun", "Sing Buri", "Sisaket", "Songkhla", "Sukhothai", "Suphan Buri", "Surat Thani", "Surin", "Tak", "Trang", "Trat", "Ubon Ratchathani", "Udon Thani", "Uthai Thani", "Uttaradit", "Yala", "Yasothon"},
                        States = ""},
                        new CountryModel {
                        CountryId = 218, Flag =  "🇹🇬", TwoDigitCode = "TG", Name = "Togo", PhoneCode = "228",
                        StatesList = new[] {"Kara", "Plateaux", "Savanes", "Centrale", "Maritime"},
                        States = ""},
                        new CountryModel {
                        CountryId = 219, Flag =  "🇹🇰", TwoDigitCode = "TK", Name = "Tokelau", PhoneCode = "690",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 220, Flag =  "🇹🇴", TwoDigitCode = "TO", Name = "Tonga", PhoneCode = "676",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 221, Flag =  "🇹🇹", TwoDigitCode = "TT", Name = "Trinidad And Tobago", PhoneCode = "1868",
                        StatesList = new[] {"Couva", "Diego Martin", "Mayaro", "Penal", "Princes Town", "Sangre Grande", "San Juan", "Siparia", "Tunapuna", "Port-of-Spain", "San Fernando", "Arima", "Point Fortin", "Chaguanas", "Tobago"},
                        States = ""},
                        new CountryModel {
                        CountryId = 222, Flag =  "🇹🇳", TwoDigitCode = "TN", Name = "Tunisia", PhoneCode = "216",
                        StatesList = new[] {"Ariana (Aryanah)", "Beja (Bajah)", "Ben Arous (Bin 'Arus)", "Bizerte (Banzart)", "Gabes (Qabis)", "Gafsa (Qafsah)", "Jendouba (Jundubah)", "Kairouan (Al Qayrawan)", "Kasserine (Al Qasrayn)", "Kebili (Qibili)", "Kef (Al Kaf)", "Mahdia (Al Mahdiyah)", "Manouba (Manubah)", "Medenine (Madanin)", "Monastir (Al Munastir)", "Nabeul (Nabul)", "Sfax (Safaqis)", "Sidi Bou Zid (Sidi Bu Zayd)", "Siliana (Silyanah)", "Sousse (Susah)", "Tataouine (Tatawin)", "Tozeur (Tawzar)", "Tunis", "Zaghouan (Zaghwan)"},
                        States = ""},
                        new CountryModel {
                        CountryId = 223, Flag =  "🇹🇷", TwoDigitCode = "TR", Name = "Turkey", PhoneCode = "90",
                        StatesList = new[] {"Adana", "Adiyaman", "Afyonkarahisar", "Agri", "Aksaray", "Amasya", "Ankara", "Antalya", "Ardahan", "Artvin", "Aydin", "Balikesir", "Bartin", "Batman", "Bayburt", "Bilecik", "Bingol", "Bitlis", "Bolu", "Burdur", "Bursa", "Canakkale", "Cankiri", "Corum", "Denizli", "Diyarbakir", "Duzce", "Edirne", "Elazig", "Erzincan", "Erzurum", "Eskisehir", "Gaziantep", "Giresun", "Gumushane", "Hakkari", "Hatay", "Igdir", "Isparta", "Istanbul", "Izmir", "Kahramanmaras", "Karabuk", "Karaman", "Kars", "Kastamonu", "Kayseri", "Kilis", "Kirikkale", "Kirklareli", "Kirsehir", "Kocaeli", "Konya", "Kutahya", "Malatya", "Manisa", "Mardin", "Mersin", "Mugla", "Mus", "Nevsehir", "Nigde", "Ordu", "Osmaniye", "Rize", "Sakarya", "Samsun", "Sanliurfa", "Siirt", "Sinop", "Sirnak", "Sivas", "Tekirdag", "Tokat", "Trabzon", "Tunceli", "Usak", "Van", "Yalova", "Yozgat", "Zonguldak"},
                        States = ""},
                        new CountryModel {
                        CountryId = 224, Flag =  "🇹🇲", TwoDigitCode = "TM", Name = "Turkmenistan", PhoneCode = "7370",
                        StatesList = new[] {"Ahal Welayaty (Ashgabat)", "Balkan Welayaty (Balkanabat)", "Dashoguz Welayaty", "Lebap Welayaty (Turkmenabat)", "Mary Welayaty"},
                        States = ""},
                        new CountryModel {
                        CountryId = 225, Flag =  "🇹🇨", TwoDigitCode = "TC", Name = "Turks And Caicos Islands", PhoneCode = "1649",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 226, Flag =  "🇹🇻", TwoDigitCode = "TV", Name = "Tuvalu", PhoneCode = "688",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 227, Flag =  "🇺🇬", TwoDigitCode = "UG", Name = "Uganda", PhoneCode = "256",
                        StatesList = new[] {"Adjumani", "Apac", "Arua", "Bugiri", "Bundibugyo", "Bushenyi", "Busia", "Gulu", "Hoima", "Iganga", "Jinja", "Kabale", "Kabarole", "Kaberamaido", "Kalangala", "Kampala", "Kamuli", "Kamwenge", "Kanungu", "Kapchorwa", "Kasese", "Katakwi", "Kayunga", "Kibale", "Kiboga", "Kisoro", "Kitgum", "Kotido", "Kumi", "Kyenjojo", "Lira", "Luwero", "Masaka", "Masindi", "Mayuge", "Mbale", "Mbarara", "Moroto", "Moyo", "Mpigi", "Mubende", "Mukono", "Nakapiripirit", "Nakasongola", "Nebbi", "Ntungamo", "Pader", "Pallisa", "Rakai", "Rukungiri", "Sembabule", "Sironko", "Soroti", "Tororo", "Wakiso", "Yumbe"},
                        States = ""},
                        new CountryModel {
                        CountryId = 228, Flag =  "🇺🇦", TwoDigitCode = "UA", Name = "Ukraine", PhoneCode = "380",
                        StatesList = new[] {"Cherkasy", "Chernihiv", "Chernivtsi", "Crimea", "Dnipropetrovs'k", "Donets'k", "Ivano-Frankivs'k", "Kharkiv", "Kherson", "Khmel'nyts'kyy", "Kirovohrad", "Kiev", "Kyyiv", "Luhans'k", "L'viv", "Mykolayiv", "Odesa", "Poltava", "Rivne", "Sevastopol'", "Sumy", "Ternopil'", "Vinnytsya", "Volyn'", "Zakarpattya", "Zaporizhzhya", "Zhytomyr"},
                        States = ""},
                        new CountryModel {
                        CountryId = 229, Flag =  "🇦🇪", TwoDigitCode = "AE", Name = "United Arab Emirates", PhoneCode = "971",
                        StatesList = new[] {"Abu Dhabi", "'Ajman", "Al Fujayrah", "Sharjah", "Dubai", "Ra's al Khaymah", "Umm al Qaywayn"},
                        States = ""},
                        new CountryModel {
                        CountryId = 230, Flag =  "🇬🇧", TwoDigitCode = "GB", Name = "United Kingdom", PhoneCode = "44",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 231, Flag =  "🇺🇸", TwoDigitCode = "US", Name = "United States", PhoneCode = "1",
                        StatesList = new[] {"Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "District of Columbia", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming"},
                        States = ""},
                        new CountryModel {
                        CountryId = 232, Flag =  "🇺🇲",   TwoDigitCode = "UM", Name = "United States Minor Outlying Islands", PhoneCode = "1",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 233, Flag =  "🇺🇾", TwoDigitCode = "UY", Name = "Uruguay", PhoneCode = "598",
                        StatesList = new[] {"Artigas", "Canelones", "Cerro Largo", "Colonia", "Durazno", "Flores", "Florida", "Lavalleja", "Maldonado", "Montevideo", "Paysandu", "Rio Negro", "Rivera", "Rocha", "Salto", "San Jose", "Soriano", "Tacuarembo", "Treinta y Tres"},
                        States = ""},
                        new CountryModel {
                        CountryId = 234, Flag =  "🇺🇿", TwoDigitCode = "UZ", Name = "Uzbekistan", PhoneCode = "998",
                        StatesList = new[] {"Andijon Viloyati", "Buxoro Viloyati", "Farg'ona Viloyati", "Jizzax Viloyati", "Namangan Viloyati", "Navoiy Viloyati", "Qashqadaryo Viloyati", "Qaraqalpog'iston Respublikasi", "Samarqand Viloyati", "Sirdaryo Viloyati", "Surxondaryo Viloyati", "Toshkent Shahri", "Toshkent Viloyati", "Xorazm Viloyati"},
                        States = ""},
                        new CountryModel {
                        CountryId = 235, Flag =  "🇻🇺", TwoDigitCode = "VU", Name = "Vanuatu", PhoneCode = "678",
                        StatesList = new[] {"Malampa", "Penama", "Sanma", "Shefa", "Tafea", "Torba"},
                        States = ""},
                        new CountryModel {
                        CountryId = 236, Flag =  "🇻🇦",   TwoDigitCode = "VA", Name = "Vatican City State {Holy See}", PhoneCode = "39",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 237, Flag =  "🇻🇪", TwoDigitCode = "VE", Name = "Venezuela", PhoneCode = "58",
                        StatesList = new[] {"Amazonas", "Anzoategui", "Apure", "Aragua", "Barinas", "Bolivar", "Carabobo", "Cojedes", "Delta Amacuro", "Dependencias Federales", "Distrito Federal", "Falcon", "Guarico", "Lara", "Merida", "Miranda", "Monagas", "Nueva Esparta", "Portuguesa", "Sucre", "Tachira", "Trujillo", "Vargas", "Yaracuy", "Zulia"},
                        States = ""},
                        new CountryModel {
                        CountryId = 238, Flag =  "🇻🇳", TwoDigitCode = "VN", Name = "Vietnam", PhoneCode = "84",
                        StatesList = new[] {"An Giang", "Bac Giang", "Bac Kan", "Bac Lieu", "Bac Ninh", "Ba Ria-Vung Tau", "Ben Tre", "Binh Dinh", "Binh Duong", "Binh Phuoc", "Binh Thuan", "Ca Mau", "Cao Bang", "Dac Lak", "Dac Nong", "Dien Bien", "Dong Nai", "Dong Thap", "Gia Lai", "Ha Giang", "Hai Duong", "Ha Nam", "Ha Tay", "Ha Tinh", "Hau Giang", "Hoa Binh", "Hung Yen", "Khanh Hoa", "Kien Giang", "Kon Tum", "Lai Chau", "Lam Dong", "Lang Son", "Lao Cai", "Long An", "Nam Dinh", "Nghe An", "Ninh Binh", "Ninh Thuan", "Phu Tho", "Phu Yen", "Quang Binh", "Quang Nam", "Quang Ngai", "Quang Ninh", "Quang Tri", "Soc Trang", "Son La", "Tay Ninh", "Thai Binh", "Thai Nguyen", "Thanh Hoa", "Thua Thien-Hue", "Tien Giang", "Tra Vinh", "Tuyen Quang", "Vinh Long", "Vinh Phuc", "Yen Bai", "Can Tho", "Da Nang", "Hai Phong", "Hanoi", "Ho Chi Minh"},
                        States = ""},
                        new CountryModel {
                        CountryId = 239, Flag =  "🇻🇬", TwoDigitCode = "VG", Name = "Virgin Islands {British}", PhoneCode = "1284",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 240, Flag =  "🇻🇮", TwoDigitCode = "VI", Name = "Virgin Islands {US}", PhoneCode = "1340",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 241, Flag =  "🇼🇫", TwoDigitCode = "WF", Name = "Wallis And Futuna Islands", PhoneCode = "681",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 242, Flag =  "🇪🇭",   TwoDigitCode = "EH", Name = "Western Sahara", PhoneCode = "212",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 243, Flag =  "🇾🇪", TwoDigitCode = "YE", Name = "Yemen", PhoneCode = "967",
                        StatesList = new[] {"Abyan", "'Adan", "Ad Dali'", "Al Bayda'", "Al Hudaydah", "Al Jawf", "Al Mahrah", "Al Mahwit", "'Amran", "Dhamar", "Hadramawt", "Hajjah", "Ibb", "Lahij", "Ma'rib", "Sa'dah", "San'a'", "Shabwah", "Ta'izz"},
                        States = ""},
                        new CountryModel {
                        CountryId = 244, Flag =  "",   TwoDigitCode = "YU", Name = "Yugoslavia", PhoneCode = "368",
                        StatesList = new[] {""},
                        States = ""},
                        new CountryModel {
                        CountryId = 245, Flag =  "🇿🇲", TwoDigitCode = "ZM", Name = "Zambia", PhoneCode = "260",
                        StatesList = new[] {"Central", "Copperbelt", "Eastern", "Luapula", "Lusaka", "Northern", "North-Western", "Southern", "Western"},
                        States = ""},
                        new CountryModel {
                        CountryId = 246, Flag =  "🇿🇼", TwoDigitCode = "ZW", Name = "Zimbabwe", PhoneCode = "263",
                        StatesList = new[] {"Bulawayo", "Harare", "Manicaland", "Mashonaland Central", "Mashonaland East", "Mashonaland West", "Masvingo", "Matabeleland North", "Matabeleland South", "Midlands"},
                        States = ""}
                    };

                foreach (var item in countryModels)
                {
                    if (item.StatesList.Length > 0)
                    {
                        string states = String.Join('|', item.StatesList.Select(s => s.ToString()).ToArray());
                        item.States = states;
                    }
                }
                return countryModels;
            });
        }

    }
}
