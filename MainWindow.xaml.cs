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
        List<Allampolgar> Lakossag = new();
        public MainWindow()
        {
            InitializeComponent();
            using StreamReader sr = new(
                path: @"../../../src/bevölkerung.txt",
                encoding: UTF8Encoding.UTF8);
            _ = sr.ReadLine();
            while (!sr.EndOfStream) Lakossag.Add(new(sr.ReadLine()));

            for (int i = 1; i <= 40; i++) Sorszam.Items.Add(i);
            foreach (var lakos in Lakossag) MegoldasTeljes.Items.Add(lakos);
        }

        private void Sorszam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MegoldasLista.Items.Clear();
            MegoldasMondatos.Content = "";
            MegoldasTeljes.Items.Clear();
            Feladat8();
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
    }
}