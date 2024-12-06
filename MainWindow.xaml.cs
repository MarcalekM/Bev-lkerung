using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beolvasas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<string, Action> methodMap;
        List<Allampolgar> Lakossag = new();
        public MainWindow()
        {
            InitializeComponent();
            methodMap = new Dictionary<string, Action>
            {
                { "1", Feladat1},
                { "2", Feladat2 },
                { "3", Feladat3 },
                { "4", Feladat4 },
                { "5", Feladat5 },
                { "6", Feladat6 },
                { "7", Feladat7 },
                { "8", Feladat8 },
                { "9", Feladat9},
                { "10", Feladat10 },
                { "11", Feladat11 },
                { "12", Feladat12 },
                { "13", Feladat13 },
                { "14", Feladat14 },
                { "15", Feladat15 },
                { "16", Feladat16 },
                { "17", Feladat17 },
                { "18", Feladat18 },
                { "19", Feladat19 },
                { "20", Feladat20 },
                { "21", Feladat21 },
                { "22", Feladat22 },
                { "23", Feladat23 },
                { "24", Feladat24 },
                { "25", Feladat25 },
                { "26", Feladat26 },
                { "27", Feladat27 },
                { "28", Feladat28 },
                { "29", Feladat29 },
                { "30", Feladat30 },
                { "31", Feladat31 },
                { "32", Feladat32 },
                { "33", Feladat33 },
                { "34", Feladat34 },
                { "35", Feladat35 },
                { "36", Feladat36 },
                { "37", Feladat37 },
                { "38", Feladat38 },
                { "39", Feladat39 },
                { "40", Feladat40 },
                { "41", Feladat41 },
                { "42", Feladat42 },
                { "43", Feladat43 },
                { "44", Feladat44 },
                { "45", Feladat45 }
            };
            using StreamReader sr = new(
                path: @"../../../src/bevölkerung.txt",
                encoding: UTF8Encoding.UTF8);
            _ = sr.ReadLine();
            while (!sr.EndOfStream) Lakossag.Add(new(sr.ReadLine()));
            foreach(var key in methodMap) Sorszam.Items.Add(key.Key.ToString());
            foreach (var lakos in Lakossag) MegoldasTeljes.Items.Add(lakos);
        }

        private void Sorszam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAllData();
            if (methodMap.TryGetValue(Sorszam.SelectedValue.ToString(), out var method))
            {
                method.Invoke();
            }
        }

        private void ClearAllData()
        {
            MegoldasLista.Items.Clear();
            MegoldasMondatos.Content = "";
            MegoldasTeljes.Items.Clear();
        }

        public void Feladat1()
        {
            var feladat = Lakossag.OrderByDescending(l => l.NettoJovedelem).First().NettoJovedelem;
            MegoldasMondatos.Content = $"A legmagasabb éves nettó jövedelem: {feladat}";
        }

        public void Feladat2()
        {
            var feladat = Lakossag.Average(l => l.NettoJovedelem);
            MegoldasMondatos.Content = $"A lakosok átlaeg nettő jövedelme: {feladat}";
        }

        public void Feladat3()
        {
            var feladat = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, g => g.Count());
            foreach (var f in feladat)
            {
                string elem = $"{f.Key} - {f.Value}";
                MegoldasLista.Items.Add(elem);
            }
        }

        public void Feladat4()
        {
            var feladat = Lakossag.Where(l => l.Nemzetiseg.Equals("angolai"));
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }
        public void Feladat5()
        {
            var feladat = Lakossag.Where(l => l.Kor.Equals(Lakossag.Min(l => l.Kor)));
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }
        public void Feladat6()
        {
            var feladat = Lakossag.Where(l => !l.Dohanyzik);
            foreach (var f in feladat)
            {
                string elem = $"{f.Id} - {f.HaviNettoJovedelem}";
                MegoldasLista.Items.Add(elem);
            }
        }

        public void Feladat7()
        {
            var feladat = Lakossag.Where(l => l.Tartomany.Equals("Bajorország") && l.NettoJovedelem > 30000).OrderBy(l => l.IskolaiVegzettseg);
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }

        public void Feladat8()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("férfi"));
            foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(true));
        }

        public void Feladat9()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("nő") && l.Tartomany.Equals("Bajorország"));
            foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(false));
        }
        public void Feladat10()
        {
            var feladat = Lakossag.Where(l => !l.Dohanyzik).OrderBy(l => l.NettoJovedelem).ToList();
            for (int i = 0; i < 10; i++) MegoldasLista.Items.Add(feladat[i].ToString(true));
        }
        public void Feladat11()
        {
            var feladat = Lakossag.OrderByDescending(l => l.Kor).ToList();
            for (int i = 0; i < 5; i++) MegoldasTeljes.Items.Add(feladat[i]);
        }
        public void Feladat12()
        {
            var feladat = Lakossag.Where(l => l.Nemzetiseg.Equals("német")).OrderBy(l => l.Tartomany).DistinctBy(l => l.Tartomany);
            foreach (var fel in feladat)
            {
                MegoldasLista.Items.Add(fel.Tartomany);
                var temp = feladat.Where(f => f.Tartomany.Equals(fel.Tartomany)).ToList();
                foreach (var f in temp) MegoldasLista.Items.Add(f.Id + " - " + (f.AktivSzavazo ? "aktív szavazó" : "nem aktív szavazó"));
            }
            
        }
        public void Feladat13()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("férfi")).Average(l => l.SorFogyasztasEvente);
            MegoldasMondatos.Content = $"A férfiak átlag sörfogyasztása évente: {feladat:0} liter";
        }
        public void Feladat14()
        {
            var feladat = Lakossag.OrderBy(l => l.IskolaiVegzettseg).ToList();
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }
        public void Feladat15()
        {
            var feladat = Lakossag.OrderByDescending(l => l.NettoJovedelem).ToList();
            for (int i = 0; i < 3; i++) MegoldasLista.Items.Add(feladat[i].ToString(false));
            feladat.Reverse();
            for (int i = 0; i < 3; i++) MegoldasLista.Items.Add(feladat[i].ToString(false));
        }
        public void Feladat16()
        {
            float feladat = Lakossag.Where(l => l.AktivSzavazo).Count() / Lakossag.Count() * 100;
            MegoldasMondatos.Content = $"A lakosság {feladat:0}%-a aktív szaavazó";
        }
        public void Feladat17()
        {
            var feladat = Lakossag.Where(l => l.AktivSzavazo).OrderBy(l => l.Tartomany);
            foreach (var f in feladat) MegoldasTeljes.Items.Add(f);
        }
        public void Feladat18()
        {
            var feladat = Lakossag.Average(l => l.Kor);
            MegoldasMondatos.Content = $"A lakosok átlag életkora: {feladat:0} év";
        }
        public void Feladat19()
        {
            List<string> tartomanyok = new();
            var feladat = Lakossag.DistinctBy(l => l.Tartomany);
            foreach (var f in feladat) tartomanyok.Add(f.Tartomany);
            string tartomany = string.Empty;
            double max = 0;
            for (int i = 0; i < tartomanyok.Count; i++)
            {
                if(max < Lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).Average(l => l.NettoJovedelem))
                {
                    max = Lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).Average(l => l.NettoJovedelem);
                    tartomany = tartomanyok[i];
                }
            }
            MegoldasMondatos.Content = $"{tartomany} - {max}";
        }
        public void Feladat20()
        {
            var atlagSuly = Lakossag.Average(l => l.Suly);
            double median;
            var feladat = Lakossag.OrderBy(l => l.Suly).ToList();
            if(feladat.Count() % 2 == 0) median = (feladat[feladat.Count() / 2].Suly + feladat[feladat.Count() / 2 + 1].Suly) / 2;
            else median = feladat[(feladat.Count() -1) / 2 +1].Suly;
            MegoldasMondatos.Content = $"A lakosok átlag súlya: {atlagSuly:0}, a medián: {median}";
        }
        public void Feladat21()
        {
            var aktivSzavazok = Lakossag.Where(l => l.AktivSzavazo).Sum(l => l.SorFogyasztasEvente);
            var nemAktivSzavazok = Lakossag.Where(l => !l.AktivSzavazo).Sum(l => l.SorFogyasztasEvente);
            MegoldasMondatos.Content = $"Az aktív szavazók évente {aktivSzavazok}l, a nem aktív szavazók pedig {nemAktivSzavazok}l sört isznak. {(aktivSzavazok > nemAktivSzavazok ? "Az aktív szavazók isznak többet" : "A nem aktív szavazók isznak többet")}";
        }
        public void Feladat22()
        {
            var ferfiAtlagMagassag = Lakossag.Where(l => l.Nem.Equals("férfi")).Average(l => l.Magassag);
            var noiAtlagMagassag = Lakossag.Where(l => l.Nem.Equals("nő")).Average(l => l.Magassag);
            MegoldasMondatos.Content = $"A férfiak átlag magassága {ferfiAtlagMagassag:0.0} cm, a nők átlag magassága {noiAtlagMagassag:0.0} cm";
        }
        public void Feladat23()
        {
            var feladat = Lakossag.Where(l => l.Nepcsoport != null).GroupBy(l => l.Nepcsoport).ToDictionary(l => l.Key, l => l.Count());
            MegoldasMondatos.Content = $"A {feladat.First().Key} népcsoportba tartoznak a legtöbben: {feladat.First().Value} fő";
        }
        public void Feladat24()
        {
            var dohanyzoAtlagFizetes = Lakossag.Where(l => l.Dohanyzik).Average(l => l.NettoJovedelem);
            var nemDohanyzoAtlagFizetes = Lakossag.Where(l => !l.Dohanyzik).Average(l => l.NettoJovedelem);
            MegoldasMondatos.Content = $"{(dohanyzoAtlagFizetes > nemDohanyzoAtlagFizetes ? "A dohányzók átlag fizetése nagyobb" : "A nem dohányzók átlag fizetése nagyobb")}";
        }
        public void Feladat25()
        {
            var atlag = Lakossag.Average(l => l.KrumpliFogyasztasEvente);
            var feladat = Lakossag.OrderByDescending(l => l.KrumpliFogyasztasEvente).ToList();
            for (var i = 0; i < 15; i++) if (feladat[i].KrumpliFogyasztasEvente > atlag) MegoldasTeljes.Items.Add(feladat[i]);
        }

        public void Feladat26()
        {
            List<string> tartomanyok = new();
            var feladat = Lakossag.DistinctBy(l => l.Tartomany);
            foreach (var f in feladat) tartomanyok.Add(f.Tartomany);
            string tartomany = string.Empty;
            double atlagEletkor = 0;
            for (var i = 0; i < tartomanyok.Count; i++) 
            {
                atlagEletkor = Lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).Average(l => l.Kor);
                tartomany = tartomanyok[i];
                MegoldasLista.Items.Add($"{tartomany} - {atlagEletkor:0}");
            }
        }

        public void Feladat27()
        {
            var feladat = Lakossag.Where(l => l.Kor > 50).ToList();
            foreach(var f in feladat) MegoldasLista.Items.Add(f.ToString(false));
            MegoldasLista.Items.Add(feladat.Count());
        }

        public void Feladat28()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("nő") && l.Dohanyzik).OrderByDescending(l => l.NettoJovedelem).ToList();
            if (feladat.Count() != 0) MegoldasMondatos.Content = $"A dohányzó nők legmagasabb nettó jövedelme: {feladat.First().NettoJovedelem}";
            else MegoldasMondatos.Content = "Nincs találat dohányzó nőkre";
        }

        public void Feladat29()
        {
            List<string> tartomanyok = new();
            var feladat = Lakossag.DistinctBy(l => l.Tartomany);
            foreach (var f in feladat) tartomanyok.Add(f.Tartomany);
            string tartomany = string.Empty;
            double max= 0;
            int id = 0;
            for (int i = 0; i < tartomanyok.Count; i++)
            {
                tartomany = tartomanyok[i];
                max = Lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).MaxBy(l => l.SorFogyasztasEvente).SorFogyasztasEvente;
                id = Lakossag.Where(l => l.Tartomany.Equals(tartomanyok[i])).MaxBy(l => l.SorFogyasztasEvente).Id;
                MegoldasLista.Items.Add($"{tartomany} - {id} - {max}");
            }
        }

        public void Feladat30()
        {
            var legidosebbFerfi = Lakossag.Where(l => l.Nem.Equals("férfi")).OrderByDescending(l => l.Kor).First();
            var legidosebbNo = Lakossag.Where(l => l.Nem.Equals("nő")).OrderByDescending(l => l.Kor).First();
            MegoldasLista.Items.Add(legidosebbFerfi.ToString(true));
            MegoldasLista.Items.Add(legidosebbNo.ToString(true));
        }

        public void Feladat31()
        {
            var feladat = Lakossag.DistinctBy(l => l.Nemzetiseg).OrderByDescending(l => l.Nemzetiseg).ToList();
            foreach (var f in feladat) MegoldasLista.Items.Add($"{f.Nemzetiseg}");
        }

        public void Feladat32()
        {
            var feladat = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.Count()).OrderBy(l => l.Value);
            foreach(var f in feladat) MegoldasLista.Items.Add(f.Key);
        }

        public void Feladat33()
        {
            var feladat = Lakossag.OrderByDescending(l => l.NettoJovedelem).ToList();
            for (var i = 0; i < 3; i++) MegoldasLista.Items.Add($"{feladat[i].Id} - {feladat[i].NettoJovedelem}");
        }

        public void Feladat34()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("férfi") && l.KrumpliFogyasztasEvente > 55).Average(l => l.Suly);
            MegoldasMondatos.Content = $"Az évente 55 kilónál több krumpit fogyasztó férfiak átlag súlya: {feladat:0} kg ";
        }

        public void Feladat35()
        {
            var feladat = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.MinBy(l => l.Kor).Kor);
            foreach (var f in feladat) MegoldasLista.Items.Add($"{f.Key} - {f.Value}");
        }

        public void Feladat36()
        {
            List<string> feladat = new();
            foreach (var l in Lakossag) feladat.Add($"{l.Nemzetiseg} - {l.Tartomany}");
            feladat = feladat.Distinct().Order().ToList();
            foreach(var f in feladat) MegoldasLista.Items.Add(f);
        }

        public void Feladat37()
        {
            var feladat = Lakossag.Where(l => l.NettoJovedelem > Lakossag.Average(l => l.NettoJovedelem));
            MegoldasLista.Items.Add($"A {Lakossag.Average(l => l.NettoJovedelem):0} átlag fizetést meghaladó lakosok száma: {feladat.Count()}");
            foreach (var f in feladat) MegoldasLista.Items.Add(f.ToString(false));
        }

        public void Feladat38()
        {
            var ferfiDB = Lakossag.Where(l => l.Nem.Equals("férfi")).Count();
            var noDB = Lakossag.Where(l => l.Nem.Equals("nő")).Count();
            MegoldasMondatos.Content = $"A férfiak száma {ferfiDB}, a nőké pedig {noDB}";
        }

        public void Feladat39()
        {
            var feladat = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.MaxBy(l => l.NettoJovedelem).NettoJovedelem).OrderByDescending(l => l.Value);
            foreach (var f in feladat) MegoldasLista.Items.Add($"{f.Key} - {f.Value}");
        }

        public void Feladat40()
        {
            var nemet = Lakossag.Where(l => l.Nemzetiseg.Equals("német")).Sum(l => l.HaviNettoJovedelem) / Lakossag.Sum(l => l.HaviNettoJovedelem) * 100;
            var nemNemet = Lakossag.Where(l => !l.Nemzetiseg.Equals("német")).Sum(l => l.HaviNettoJovedelem) / Lakossag.Sum(l => l.HaviNettoJovedelem) * 100;
            MegoldasMondatos.Content = $"Németek havi jövedelme százalékosan: {nemet:0}%, nem németeké: {nemNemet:0}%";
        }

        public void Feladat41()
        {
            var feladat = Lakossag.Where(l => l.Nemzetiseg.Equals("török")).ToList();
            for (int i = 0; i < 10; i++)
            {
                var item = feladat[Random.Shared.Next(0, feladat.Count())];
                MegoldasTeljes.Items.Add(item);
                feladat.Remove(item);
            }
        }

        public void Feladat42()
        {
            var feladat = Lakossag.Where(l => l.SorFogyasztasEvente > Lakossag.Average(l => l.SorFogyasztasEvente)).ToList();
            MegoldasLista.Items.Add($"Az átlagos sörfogyasztás évente {Lakossag.Average(l => l.SorFogyasztasEvente):0}");
            for (int i = 0; i < 5; i++)
            {
                var item = feladat[Random.Shared.Next(0, feladat.Count())];
                MegoldasLista.Items.Add(item.ToString(true));
                feladat.Remove(item);
            }
        }

        public void Feladat43()
        {
            double atlag = Lakossag.Average(l => l.NettoJovedelem);
            var feladat = Lakossag.GroupBy(l => l.Tartomany).ToDictionary(l => l.Key, l => l.MinBy(l => l.NettoJovedelem).NettoJovedelem).Where(l => l.Value > atlag);
            MegoldasLista.Items.Add($"Az átlag: { atlag:0.00}");
            foreach (var f in feladat) MegoldasLista.Items.Add($"{f.Key} - {f.Value}");
        }

        public void Feladat44()
        {
            var feladat = Lakossag.Where(l => l.IskolaiVegzettseg == null).ToList();
            for (int i = 0; i < 3; i++)
            {
                var item = feladat[Random.Shared.Next(0, feladat.Count())];
                MegoldasTeljes.Items.Add(item);
                feladat.Remove(item);
            }
        }

        public void Feladat45()
        {
            var feladat = Lakossag.Where(l => l.Nem.Equals("nő") && l.IskolaiVegzettseg == "Universität" && !l.Nemzetiseg.Equals("bajor")).ToList();
            for (int i = 0; i < 5; i++) MegoldasTeljes.Items.Add(feladat[i]);
            var feladat2 = Lakossag.Where(l => l.Nem.Equals("nő") && l.Nemzetiseg.Equals("német") && l.NettoJovedelem > feladat.First().NettoJovedelem).ToList();
            for (int i = 0; i < 3; i++)
            {
                var item = feladat[Random.Shared.Next(0, feladat.Count())];
                MegoldasLista.Items.Add(item.ToString(false));
                feladat.Remove(item);
            }
        }
    }
}
